using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Gecko.NCore.Client.Properties;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// Provides <see cref="IQueryable"/> instances.
	/// </summary>
	internal class DataObjectQueryProvider: IQueryProvider
	{
		private readonly IStateManager _stateManager;
		private readonly IObjectModelAdapter _objectModelAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataObjectQueryProvider"/> class.
		/// </summary>
		/// <param name="stateManager">The state manager.</param>
		/// <param name="objectModelAdapter">The object model.</param>
		public DataObjectQueryProvider(IStateManager stateManager, IObjectModelAdapter objectModelAdapter)
		{
			_stateManager = stateManager;
			_objectModelAdapter = objectModelAdapter;
		}

		/// <summary>
		/// Constructs an <see cref="T:System.Linq.IQueryable"/> object that can evaluate the query represented by a specified expression tree.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Linq.IQueryable"/> that can evaluate the query represented by the specified expression tree.
		/// </returns>
		/// <param name="expression">An expression tree that represents a LINQ query.</param>
		public virtual IQueryable CreateQuery(Expression expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			var elementType = TypeSystem.ResolveElementType(expression.Type);
			try
			{
				return (IQueryable)Activator.CreateInstance(typeof(DataObjectQuery<>).MakeGenericType(elementType), new object[] { this, expression });
			}
			catch (TargetInvocationException tie)
			{
				throw tie.InnerException;
			}
		}

		/// <summary>
		/// Constructs an <see cref="T:System.Linq.IQueryable`1"/> object that can evaluate the query represented by a specified expression tree.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Linq.IQueryable`1"/> that can evaluate the query represented by the specified expression tree.
		/// </returns>
		/// <param name="expression">An expression tree that represents a LINQ query.</param><typeparam name="TElement">The type of the elements of the <see cref="T:System.Linq.IQueryable`1"/> that is returned.</typeparam>
		public virtual IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			return new DataObjectQuery<TElement>(this, expression);
		}

		/// <summary>
		/// Executes the query represented by a specified expression tree.
		/// </summary>
		/// <returns>
		/// The value that results from executing the specified query.
		/// </returns>
		/// <param name="expression">An expression tree that represents a LINQ query.</param>
		public object Execute(Expression expression)
		{
			return ExecuteCore(expression);
		}

		/// <summary>
		/// Executes the strongly-typed query represented by a specified expression tree.
		/// </summary>
		/// <returns>
		/// The value that results from executing the specified query.
		/// </returns>
		/// <param name="expression">An expression tree that represents a LINQ query.</param><typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
		public TResult Execute<TResult>(Expression expression)
		{
			var result = ExecuteCore(expression);
			return (TResult)result;
		}

	    private object ExecuteCore(Expression expression)
		{
			expression = ExpressionEvaluator.PartialEval(expression);
			var queryTranslater = new QueryTranslator();
			expression = queryTranslater.Visit(expression);


			if (queryTranslater.QueryId.HasValue && !string.IsNullOrEmpty(queryTranslater.FilterExpression))
				throw new InvalidOperationException(Resources.DataObjectQueryProvider_ExecuteCore_A_stored_query_cannot_contain_additional_predicates);

			var methodCall = expression as MethodCallExpression;
            if (methodCall != null && methodCall.Method.DeclaringType == typeof(Queryable))
            {
                switch (methodCall.Method.Name)
                {
                    case "Count":
                        return GetTotalCount(queryTranslater);
                    case "First":
                        return GetFirst(queryTranslater);
                    case "FirstOrDefault":
                        return GetFirstOrDefault(queryTranslater);
                    case "Any":
                        return GetAny(queryTranslater);
                }
            }

	        return GetDataObjects(queryTranslater);
		}

		private object GetFirstOrDefault(QueryTranslator queryTranslater)
		{
			return GetDataObjects(queryTranslater).FirstOrDefault();
		}

		private object GetFirst(QueryTranslator queryTranslater)
		{
			return GetDataObjects(queryTranslater).First();
		}

		private int GetTotalCount(QueryTranslator queryTranslator)
		{
			if (queryTranslator.QueryId.HasValue)
				return _objectModelAdapter.StoredQueryCount(queryTranslator.DataObjectType.Name, queryTranslator.QueryId.Value, queryTranslator.SortExpression);

			return _objectModelAdapter.QueryCount(queryTranslator.DataObjectType.Name, queryTranslator.FilterExpression, queryTranslator.SortExpression);
		}

        private bool GetAny(QueryTranslator queryTranslator)
        {
            if (queryTranslator.QueryId.HasValue)
                return _objectModelAdapter.StoredQueryCount(queryTranslator.DataObjectType.Name, queryTranslator.QueryId.Value, queryTranslator.SortExpression) > 0;

            return _objectModelAdapter.QueryCount(queryTranslator.DataObjectType.Name, queryTranslator.FilterExpression, queryTranslator.SortExpression) > 0;
        }

        private IEnumerable<object> GetDataObjects(QueryTranslator queryTranslater)
		{
			IEnumerable<object> queryResult;
			
			if (queryTranslater.QueryId.HasValue)
			{
				queryResult = _objectModelAdapter.StoredQuery(queryTranslater.DataObjectType.Name, queryTranslater.QueryId.Value, queryTranslater.SortExpression, queryTranslater.RelatedObjects, queryTranslater.TakeCount, queryTranslater.SkipCount);	
			}
			else
			{
				queryResult = _objectModelAdapter.Query(queryTranslater.DataObjectType.Name, queryTranslater.FilterExpression, queryTranslater.SortExpression, queryTranslater.RelatedObjects, queryTranslater.TakeCount, queryTranslater.SkipCount);
			}

			if (queryTranslater.SelectDelegate == null)
			{
				foreach (var dataObject in queryResult)
				{
					_stateManager.WeakAttach(dataObject);
					ProcessIncludeSelectors(dataObject, queryTranslater.IncludeSelectors);
					yield return dataObject;
				}
			}
			else
			{
				foreach (var dataObject in queryResult)
				{
					_stateManager.WeakAttach(dataObject);
					ProcessIncludeSelectors(dataObject, queryTranslater.IncludeSelectors);
					var projectedDataObject = queryTranslater.SelectDelegate.DynamicInvoke(dataObject);
					yield return projectedDataObject;
				}
			}
		}

        private void ProcessIncludeSelectors(object dataObject, IDictionary<string, Delegate> includeSelectors)
		{
			var includeTargets = new Dictionary<string, object>();
			foreach (var includeSelector in includeSelectors)
			{
				object includeTarget;
				if (includeTargets.TryGetValue(includeSelector.Key, out includeTarget))
					continue;

				includeTarget = includeSelector.Value.DynamicInvoke(dataObject);
				includeTargets[includeSelector.Key] = includeTarget;

				if (includeTarget == null)
				{
					var includeSelectorPath = includeSelector.Key + ".";
					foreach (var includeSelectorKey in includeSelectors.Keys)
					{
						if (includeSelectorKey.StartsWith(includeSelectorPath))
						{
							includeTargets[includeSelectorKey] = null;
						}
					}
				}

				var dataObjectCollection = includeTarget as IDataObjectCollection;
				if (dataObjectCollection != null)
				{
					dataObjectCollection.QueryProvider = this;
					if (!dataObjectCollection.IsLoaded)
					{
						dataObjectCollection.Load();
					}
					continue;
				}

				var notifyPropertyChanged = includeTarget as INotifyPropertyChanged;
				if (notifyPropertyChanged != null)
				{
					_stateManager.WeakAttach(notifyPropertyChanged);
				}
			}
		}
	}
}