using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// 
	/// </summary>
	internal class QueryTranslator: ExpressionVisitor
	{
		private Type _dataObjectType;
		private int? _takeCount;
		private int? _skipCount;
		private readonly List<string> _relatedObjects = new List<string>();
		private Delegate _selectDelegate;
		private readonly StringBuilder _sortExpression = new StringBuilder();
		private readonly IDictionary<string, Delegate> _includeSelectors = new SortedDictionary<string, Delegate>(StringComparer.Ordinal);
		private readonly List<Expression> _predicates = new List<Expression>();
		private string _filterExpression;
		private int? _queryId;

		/// <summary>
		/// Gets the select delegate.
		/// </summary>
		/// <value>The select delegate.</value>
		public Delegate SelectDelegate
		{
			get { return _selectDelegate; }
		}

		/// <summary>
		/// Gets the greedy load selectors.
		/// </summary>
		/// <value>The greedy load selectors.</value>
		public IDictionary<string, Delegate> IncludeSelectors
		{
			get { return _includeSelectors; }
		}


		/// <summary>
		/// Gets the type of the data object.
		/// </summary>
		/// <value>The type of the data object.</value>
		public Type DataObjectType
		{
			get { return _dataObjectType; }
            protected set { _dataObjectType = value; }
		}

		/// <summary>
		/// Gets the take count.
		/// </summary>
		/// <value>The take count.</value>
		public int? TakeCount
		{
		    get { return _takeCount; }
		    set { _takeCount = value; }
		}

	    /// <summary>
		/// Gets the skip count.
		/// </summary>
		/// <value>The skip count.</value>
		public int? SkipCount
		{
			get { return _skipCount; }
		}

		/// <summary>
		/// Gets the related objects.
		/// </summary>
		/// <value>The related objects.</value>
		public IEnumerable<string> RelatedObjects
		{
			get { return _relatedObjects.Distinct().OrderBy(x => x); }
		}

		public int? QueryId
		{
			get
			{
				if (_filterExpression == null)
				{
					_filterExpression = TranslatePredicates(out _queryId);
				}

				return _queryId;
			}
		}

		/// <summary>
		/// Gets the filter expression.
		/// </summary>
		/// <value>The filter expression.</value>
		public string FilterExpression
		{
			get
			{
				return _filterExpression ?? (_filterExpression = TranslatePredicates(out _queryId));
			}
		}

		private string TranslatePredicates(out int? queryId)
		{
			if (_predicates.Count == 0)
			{
				queryId = null;
				return string.Empty;
			}

			Expression expression;
			if (_predicates.Count == 1)
			{
				expression = _predicates.First();
			}
			else
			{
				expression = Expression.And(_predicates[0], _predicates[1]);
				for (var predicateIndex = 2; predicateIndex < _predicates.Count; predicateIndex++)
				{
					expression = Expression.And(expression, _predicates[predicateIndex]);
				}
			}

			expression = PredicateOperandAligner.Align(expression);
			expression = PredicateDenormalizer.Denormalize(expression);

			return PredicateTranslator.Translate(expression, out queryId);
		}

		/// <summary>
		/// Gets the sort expression.
		/// </summary>
		/// <value>The sort expression.</value>
		public string SortExpression
		{
			get { return _sortExpression.ToString(); }
		}

		protected static Expression StripQuotes(Expression expression)
		{
			while (expression.NodeType == ExpressionType.Quote)
			{
				expression = ((UnaryExpression) expression).Operand;
			}
			return expression;
		}

		/// <summary>
		/// Visits the method call.
		/// </summary>
		/// <param name="methodCall">The method call.</param>
		/// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression methodCall)
		{
			if (methodCall.Method.DeclaringType == typeof(Queryable))
				return VisitQueryableMethodCall(methodCall);

			if (methodCall.Method.DeclaringType == typeof(QueryableExtensions))
				return VisitQueryableExtensionsMethodCall(methodCall);

            throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
		}

	    protected virtual Expression VisitQueryableExtensionsMethodCall(MethodCallExpression methodCall)
		{
			switch (methodCall.Method.Name)
			{
				case "Include":
					return VisitIncludeMethodCall(methodCall);
				default:
					throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
			}
		}

        protected virtual Expression VisitIncludeMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			
			var lambda = (LambdaExpression)StripQuotes(methodCall.Arguments[1]);

			var memberPath = MemberEvaluator.Evaluate(lambda.Body);
			var memberPathElements = memberPath.Split('.');
			var memberExpressionPathElements = UnwindMemberAccess(lambda.Body).Reverse().ToList();
			for (var memberPathElementIndex = 0; memberPathElementIndex < memberPathElements.Length; memberPathElementIndex++)
			{
				var partialMemberLambdaBody = memberExpressionPathElements[memberPathElementIndex];
				var partialMemberLambda = Expression.Lambda(partialMemberLambdaBody, lambda.Parameters.ToArray());
				
				ProcessIncludeSelector(partialMemberLambda);
			}
			return methodCall;
		}

        protected virtual IEnumerable<Expression> UnwindMemberAccess(Expression expression)
		{
			while (expression.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = (MemberExpression) expression;
				yield return memberExpression;
				expression = memberExpression.Expression;
			}
		}

        protected virtual void ProcessIncludeSelector(LambdaExpression lambda)
		{
			var memberPath = MemberEvaluator.Evaluate(lambda.Body);

			if (_includeSelectors.ContainsKey(memberPath))
				return;

			if (typeof(IDataObjectCollection).IsAssignableFrom(lambda.Body.Type))
			{
				var memberSelector = lambda.Compile();
				_includeSelectors.Add(memberPath, memberSelector);
				return;
			}

			if (typeof (INotifyPropertyChanged).IsAssignableFrom(lambda.Body.Type))
			{
				_relatedObjects.Add(memberPath);
				var memberSelector = lambda.Compile();
				_includeSelectors.Add(memberPath, memberSelector);
			}
		}

        protected virtual Expression VisitQueryableMethodCall(MethodCallExpression methodCall)
		{
			switch (methodCall.Method.Name)
			{
				case "Where":
					return VisitWhereMethodCall(methodCall);
				case "Take":
					return VisitTakeMethodCall(methodCall);
				case "Skip":
					return VisitSkipMethodCall(methodCall);
				case "Count":
					return VisitCountMethodCall(methodCall);
				case "Select":
					return VisitSelectMethodCall(methodCall);
				case "OrderByDescending":
					return VisitOrderByMethodCall(methodCall, "DESC");
				case "OrderBy":
					return VisitOrderByMethodCall(methodCall, "ASC");
				case "ThenByDescending":
					return VisitOrderByMethodCall(methodCall, "DESC");
				case "ThenBy":
					return VisitOrderByMethodCall(methodCall, "ASC");
				case "First":
					return VisitFirstMethodCall(methodCall);
				case "FirstOrDefault":
					return VisitFirstOrDefaultMethodCall(methodCall);
				case "Cast":
					return VisitCastMethodCall(methodCall);
                case "Any":
			        return VisitAnyMethodCall(methodCall);
				default:
					throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
			}
		}

        protected virtual Expression VisitAnyMethodCall(MethodCallExpression methodCall)
	    {
            Visit(methodCall.Arguments[0]);
            if (methodCall.Arguments.Count > 1)
            {
                var lambda = (LambdaExpression)StripQuotes(methodCall.Arguments[1]);
                _predicates.Add(lambda.Body);
            }
            _takeCount = 1;
            return methodCall;
        }

        protected virtual Expression VisitCastMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			return methodCall;
		}

        protected virtual Expression VisitFirstOrDefaultMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			if (methodCall.Arguments.Count > 1)
			{
				var lambda = (LambdaExpression)StripQuotes(methodCall.Arguments[1]);
				_predicates.Add(lambda.Body);
			}
			_takeCount = 1;
			return methodCall;
		}

		protected virtual Expression VisitFirstMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			if (methodCall.Arguments.Count > 1)
			{
				var lambda = (LambdaExpression) StripQuotes(methodCall.Arguments[1]);
				_predicates.Add(lambda.Body);
			}
			_takeCount = 1;
			return methodCall;
		}

        protected virtual Expression VisitOrderByMethodCall(MethodCallExpression methodCall, string direction)
		{
			Visit(methodCall.Arguments[0]);
			
			if (SortExpression.Length > 0)
			{
				_sortExpression.Append(',');
			}

			_sortExpression.Append('@');
			_sortExpression.Append(MemberEvaluator.Evaluate(methodCall.Arguments[1]));
			_sortExpression.Append(" ");
			_sortExpression.Append(direction);
			return methodCall;
		}

        protected virtual Expression VisitSelectMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			var selectLambda = (LambdaExpression) StripQuotes(methodCall.Arguments[1]);
			_selectDelegate = selectLambda.Compile();
			var selectVisitor = new SelectVisitor();
			selectVisitor.Visit(selectLambda.Body);
			_relatedObjects.AddRange(selectVisitor.RelatedObjects);
			return methodCall;
		}

        protected virtual Expression VisitCountMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			_takeCount = 0;
			return methodCall;
		}

        protected virtual Expression VisitSkipMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			_skipCount = (int)((ConstantExpression)methodCall.Arguments[1]).Value;
			return methodCall;
		}

        protected virtual Expression VisitTakeMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			_takeCount = (int)((ConstantExpression)methodCall.Arguments[1]).Value;
			return methodCall;
		}

        protected virtual Expression VisitWhereMethodCall(MethodCallExpression methodCall)
		{
			Visit(methodCall.Arguments[0]);
			var lambda = (LambdaExpression)StripQuotes(methodCall.Arguments[1]);
			_predicates.Add(lambda.Body);
			return methodCall;
		}

		/// <summary>
		/// Visits the constant.
		/// </summary>
		/// <param name="constant">The constant.</param>
		/// <returns></returns>
		protected override Expression VisitConstant(ConstantExpression constant)
		{
			var queryable = constant.Value as IQueryable;
			if (queryable != null)
			{
				_dataObjectType = queryable.ElementType;
			}

			return constant;
		}
	}
}
