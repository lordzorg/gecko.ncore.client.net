using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gecko.NCore.Client.Properties;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.StateTracking;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// Provides <see cref="IQueryable"/> instances.
	/// </summary>
	internal class AsyncDataObjectQueryProvider: DataObjectQueryProvider
	{
		private readonly IStateManager _stateManager;
		private readonly IAsyncObjectModelAdapter _objectModelAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncDataObjectQueryProvider"/> class.
		/// </summary>
		/// <param name="stateManager">The state manager.</param>
		/// <param name="objectModelAdapter">The object model.</param>
		public AsyncDataObjectQueryProvider(IStateManager stateManager, IAsyncObjectModelAdapter objectModelAdapter)
            : base(stateManager, objectModelAdapter)
		{
			_stateManager = stateManager;
			_objectModelAdapter = objectModelAdapter;
		}

        public async Task<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            var result = await ExecuteCoreAsync(expression);
            return (TResult) result;
        }

	    private async Task<object> ExecuteCoreAsync(Expression expression)
	    {
            expression = ExpressionEvaluator.PartialEval(expression);
            var queryTranslater = new QueryTranslator();
            expression = queryTranslater.Visit(expression);


            if (queryTranslater.QueryId.HasValue && !string.IsNullOrEmpty(queryTranslater.FilterExpression))
                throw new InvalidOperationException(Resources.DataObjectQueryProvider_ExecuteCore_A_stored_query_cannot_contain_additional_predicates);

            var methodCall = expression as MethodCallExpression;
            if (methodCall != null && methodCall.Method.DeclaringType == typeof(AsyncQueryableExtensions))
            {
                switch (methodCall.Method.Name)
                {
                    case "CountAsync":
                        return await GetTotalCountAsync(queryTranslater);
                    case "FirstAsync":
                        return await GetFirstAsync(queryTranslater);
                    case "FirstOrDefaultAsync":
                        return await GetFirstOrDefaultAsync(queryTranslater);
                    case "AnyAsync":
                        return await GetAnyAsync(queryTranslater);
                    case "ToListAsync":
                        return await GetListAsync(queryTranslater);
                    case "ToArrayAsync":
                        return await GetArrayAsync(queryTranslater);
                }
            }

	        return await GetDataObjectsAsync(queryTranslater);
        }

        private async Task<object> GetFirstOrDefaultAsync(QueryTranslator queryTranslater)
        {
            var result = await GetDataObjectsAsync(queryTranslater);
            return result.Cast<object>().FirstOrDefault();
        }

        private async Task<object> GetFirstAsync(QueryTranslator queryTranslater)
        {
            var result = await GetDataObjectsAsync(queryTranslater);
            return result.Cast<object>().First();
        }

        private async Task<IEnumerable> GetListAsync(QueryTranslator queryTranslator)
        {
            var result = await GetDataObjectsAsync(queryTranslator);
            return result;
        }

	    private async Task<IEnumerable> GetArrayAsync(QueryTranslator queryTranslator)
	    {
	        var result = await GetDataObjectsAsync(queryTranslator);
	        return result;
	    }

	    private async Task<int> GetTotalCountAsync(QueryTranslator queryTranslator)
        {
            if (queryTranslator.QueryId.HasValue)
                return await _objectModelAdapter.StoredQueryCountAsync(queryTranslator.DataObjectType.Name, queryTranslator.QueryId.Value, queryTranslator.SortExpression);

            return await _objectModelAdapter.QueryCountAsync(queryTranslator.DataObjectType.Name, queryTranslator.FilterExpression, queryTranslator.SortExpression);
        }

        private async Task<bool> GetAnyAsync(QueryTranslator queryTranslator)
        {
            if (queryTranslator.QueryId.HasValue)
                return await _objectModelAdapter.StoredQueryCountAsync(queryTranslator.DataObjectType.Name, queryTranslator.QueryId.Value, queryTranslator.SortExpression) > 0;

            return await _objectModelAdapter.QueryCountAsync(queryTranslator.DataObjectType.Name, queryTranslator.FilterExpression, queryTranslator.SortExpression) > 0;
        }

        private async Task<IEnumerable> GetDataObjectsAsync(QueryTranslator queryTranslater)
        {
            IEnumerable<object> queryResult;
            if (queryTranslater.QueryId.HasValue)
            {
                queryResult = await _objectModelAdapter.StoredQueryAsync(queryTranslater.DataObjectType.Name, queryTranslater.QueryId.Value, queryTranslater.SortExpression, queryTranslater.RelatedObjects, queryTranslater.TakeCount, queryTranslater.SkipCount);
            }
            else
            {
                queryResult = await _objectModelAdapter.QueryAsync(queryTranslater.DataObjectType.Name, queryTranslater.FilterExpression, queryTranslater.SortExpression, queryTranslater.RelatedObjects, queryTranslater.TakeCount, queryTranslater.SkipCount);
            }

            if (queryTranslater.SelectDelegate == null)
            {
                var listType = typeof(List<>).MakeGenericType(queryTranslater.DataObjectType);
                var result = (IList)Activator.CreateInstance(listType);
                foreach (var dataObject in queryResult)
                {
                    _stateManager.WeakAttach(dataObject);
                    ProcessIncludeSelectors(dataObject, queryTranslater.IncludeSelectors);
                    result.Add(dataObject);
                }
                return result;
            }
            else
            {
                var listType = typeof (List<>).MakeGenericType(queryTranslater.SelectDelegate.Method.ReturnType);
                var result = (IList)Activator.CreateInstance(listType);
                foreach (var dataObject in queryResult)
                {
                    _stateManager.WeakAttach(dataObject);
                    ProcessIncludeSelectors(dataObject, queryTranslater.IncludeSelectors);
                    var projectedDataObject = queryTranslater.SelectDelegate.DynamicInvoke(dataObject);
                    result.Add(projectedDataObject);
                }
                return result;
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