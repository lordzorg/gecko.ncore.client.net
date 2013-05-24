using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PredicateTranslator
    {
		/// <summary>
		/// Translates the specified expression.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="queryId">The query id.</param>
		/// <returns></returns>
        public static string Translate(Expression expression, out int? queryId)
        {
            var visitor = new PredicateVisitor();
            visitor.Visit(expression);
        	queryId = visitor.QueryId;
            return visitor.FilterExpression;
        }

        private class PredicateVisitor: ExpressionVisitor
        {
            private readonly StringBuilder _filterExpression;
        	private int? _queryId;

        	public PredicateVisitor()
            {
                _filterExpression = new StringBuilder();
            }

        	public string FilterExpression
        	{
				get { return _filterExpression.ToString(); }
        	}

        	public int? QueryId
        	{
				get { return _queryId; }
        	}

            /// <summary>
            /// Visits the binary.
            /// </summary>
            /// <param name="binary">The binary.</param>
            /// <returns></returns>
            protected override Expression VisitBinary(BinaryExpression binary)
            {
                Visit(binary.Left);
                switch (binary.NodeType)
                {
                    case ExpressionType.And:
                        _filterExpression.Append(" AND ");
                        break;
                    case ExpressionType.AndAlso:
                        _filterExpression.Append(" AND ");
                        break;
                    case ExpressionType.Or:
                        _filterExpression.Append(" OR ");
                        break;
                    case ExpressionType.OrElse:
                        _filterExpression.Append(" OR ");
                        break;
                    case ExpressionType.Equal:
                        _filterExpression.Append("=");
                        break;
                    case ExpressionType.NotEqual:
                        _filterExpression.Append("!=");
                        break;
                    case ExpressionType.LessThan:
                        _filterExpression.Append("<");
                        break;
                    case ExpressionType.LessThanOrEqual:
                        _filterExpression.Append("<=");
                        break;
                    case ExpressionType.GreaterThan:
                        _filterExpression.Append(">");
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        _filterExpression.Append(">=");
                        break;
                    default:
                        throw new NotSupportedException(string.Format("The binary operator {0} is not supported", binary.NodeType));
                }
                Visit(binary.Right);
                return binary;
            }

            /// <summary>
            /// Visits the constant.
            /// </summary>
            /// <param name="constant">The constant.</param>
            /// <returns></returns>
            protected override Expression VisitConstant(ConstantExpression constant)
            {
                if (constant.Value == null)
                {
                    _filterExpression.Append("@");
                    return constant;
                }

                if (constant.Value is DateTime)
                {
                    var dateTime = (DateTime)constant.Value;
                    _filterExpression.Append(dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
                    return constant;
                }

            	if (constant.Value.GetType().IsEnum)
            	{
            		_filterExpression.Append(Convert.ToInt16(constant.Value));
            		return constant;
            	}
                
                _filterExpression.Append(constant.Value);
                
                return constant;
            }

            /// <summary>
            /// Visits the member.
            /// </summary>
            /// <param name="member">The member.</param>
            /// <returns></returns>
            protected override Expression VisitMember(MemberExpression member)
            {
                if (member.Member.DeclaringType == typeof(QueryContext))
                {
                    _filterExpression.Append(QueryContextEvaluator.Evaluate(member));
                    return member;
                }

                _filterExpression.Append(MemberEvaluator.Evaluate(member));
                return member;
            }

            /// <summary>
            /// Visits the unary.
            /// </summary>
            /// <param name="unary">The unary.</param>
            /// <returns></returns>
            protected override Expression VisitUnary(UnaryExpression unary)
            {
                switch (unary.NodeType)
                {
                    case ExpressionType.Convert:
                        Visit(unary.Operand);
                        break;
                    default:
                        throw new NotSupportedException(string.Format("The unary operator {0} is not supported.", unary.NodeType));
                }
                return unary;
            }

            /// <summary>
            /// Visits the method call.
            /// </summary>
            /// <param name="methodCall">The method call.</param>
            /// <returns></returns>
            protected override Expression VisitMethodCall(MethodCallExpression methodCall)
            {
            	var declaringType = methodCall.Method.DeclaringType;

            	if (declaringType == typeof(QueryContext))
                    return VisitQueryContextMethodCall(methodCall);

                if (declaringType == typeof(string))
                    return VisitStringMethodCall(methodCall);

                if (declaringType == typeof(Enumerable))
                    return VisitStaticEnumerableMethodCall(methodCall);

				if (declaringType.IsGenericType && declaringType.GetGenericTypeDefinition() == typeof(DirectPredicate<>))
					return VisitDirectPredicateMethodCall(methodCall);

                if (typeof(IEnumerable).IsAssignableFrom(declaringType))
                    return VisitInstanceEnumerableMethodCall(methodCall);

                throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, declaringType));
            }

        	private Expression VisitDirectPredicateMethodCall(MethodCallExpression methodCall)
        	{
				if (methodCall.Method.Name != "Member")
					throw new NotSupportedException();

        		var dataObjectType = methodCall.Method.DeclaringType.GetGenericArguments().First();
        		_filterExpression.Append("!");
        		_filterExpression.Append(dataObjectType.Name);
        		_filterExpression.Append(".");
        		_filterExpression.Append(MemberEvaluator.Evaluate(methodCall.Arguments.First()));
        		return methodCall;
        	}

            private Expression VisitStringMethodCall(MethodCallExpression methodCall)
            {
                _filterExpression.Append(MemberEvaluator.Evaluate(methodCall.Object));
                _filterExpression.Append("=");
                switch (methodCall.Method.Name)
                {
                    case "StartsWith":
                        return VisitStringStartsWithMethodCall(methodCall);
                    case "EndsWith":
                        return VisitStringEndsWithMethodCall(methodCall);
                    case "Contains":
                        return VisitStringContainsMethodCall(methodCall);
                    default:
                        throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
                }
            }

            private Expression VisitStringContainsMethodCall(MethodCallExpression methodCall)
            {
                Visit(methodCall.Arguments[0]);
                return methodCall;
            }

            private Expression VisitStringEndsWithMethodCall(MethodCallExpression methodCall)
            {
                Visit(methodCall.Arguments[0]);
                return methodCall;
            }

            private Expression VisitStringStartsWithMethodCall(MethodCallExpression methodCall)
            {
                Visit(methodCall.Arguments[0]);
                return methodCall;
            }

        	private Expression VisitInstanceEnumerableMethodCall(MethodCallExpression methodCall)
            {
                switch (methodCall.Method.Name)
                {
                    case "Contains":
                        return VisitInstanceEnumerableContainsMethodCall(methodCall);
                	default:
                        throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
                }
            }

        	private Expression VisitInstanceEnumerableContainsMethodCall(MethodCallExpression methodCall)
        	{
        		var member = MemberEvaluator.Evaluate(methodCall.Arguments[0]);
        		_filterExpression.Append(member);
        		_filterExpression.Append("=");
        		var enumerable = (IEnumerable) ((ConstantExpression) methodCall.Object).Value;
        		var operandValues = (from object operandValue in enumerable select (operandValue ?? "@").ToString()).ToArray();
        		_filterExpression.Append(operandValues.Any() ? string.Join(",", operandValues) : "@");
        		return methodCall;
        	}

        	private Expression VisitStaticEnumerableMethodCall(MethodCallExpression methodCall)
            {
                var memberExpression = methodCall.Arguments[0] as MemberExpression;
                if (memberExpression != null && memberExpression.Member.DeclaringType == typeof(QueryContext))
                {
                    _filterExpression.Append(QueryContextEvaluator.Evaluate(methodCall));
                    return methodCall;
                }

                switch (methodCall.Method.Name)
                {
                    case "Contains":
                        return VisitStaticEnumerableContainsMethodCall(methodCall);
                	case "Any":
                		return VisitStaticEnumerableAnyMethodCall(methodCall);
                	default:
						throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
				}
            }

        	private Expression VisitStaticEnumerableAnyMethodCall(MethodCallExpression methodCall)
        	{
				if (methodCall.Arguments.Count != 2)
					throw new NotSupportedException();

        		var member = MemberEvaluator.Evaluate(methodCall.Arguments[0]);
        		_filterExpression.Append(member);
        		_filterExpression.Append(".");
        		int? queryId;
        		var predicate = Translate(methodCall.Arguments[1], out queryId);
        		_filterExpression.Append(predicate);
        		return methodCall;
        	}

        	private Expression VisitStaticEnumerableContainsMethodCall(MethodCallExpression methodCall)
        	{
        		var member = MemberEvaluator.Evaluate(methodCall.Arguments[1]);
        		_filterExpression.Append(member);
        		_filterExpression.Append("=");
        		var enumerable = (IEnumerable) ((ConstantExpression) methodCall.Arguments[0]).Value;
                var operandValues = (from object operandValue in enumerable select (operandValue ?? "@").ToString()).ToArray();
        		_filterExpression.Append(operandValues.Any() ? string.Join(",", operandValues) : "@");
        		return methodCall;
        	}

        	private Expression VisitQueryContextMethodCall(MethodCallExpression methodCall)
            {
            	switch (methodCall.Method.Name)
            	{
            		case "StoredQuery":
            			return VisitQueryContextStoredQueryMethodCall(methodCall);
            		default:
						_filterExpression.Append(QueryContextEvaluator.Evaluate(methodCall));
						return methodCall;
				}
            }

        	private Expression VisitQueryContextStoredQueryMethodCall(MethodCallExpression methodCall)
        	{
        		_queryId = (int) ((ConstantExpression) methodCall.Arguments[1]).Value;
        		return methodCall;
        	}
        }
    }
}
