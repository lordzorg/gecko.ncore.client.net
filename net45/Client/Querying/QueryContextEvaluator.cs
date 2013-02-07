using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
    internal class QueryContextEvaluator
    {
        public static string Evaluate(Expression expression)
        {
            var filterExpression = new StringBuilder();
            var queryContextVisitor = new QueryContextVisitor(filterExpression);
            queryContextVisitor.Visit(expression);
            return filterExpression.ToString();
        }

        private class QueryContextVisitor: ExpressionVisitor
        {
            private readonly StringBuilder _filterExpression;

            public QueryContextVisitor(StringBuilder filterExpression)
            {
                _filterExpression = filterExpression;
            }

            protected override Expression VisitMethodCall(MethodCallExpression methodCall)
            {
                if (methodCall.Method.DeclaringType != typeof(Enumerable))
					throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));

                if (methodCall.Method.Name != "Contains")
					throw new NotSupportedException(string.Format(Resources.VisitMethodCall_The_method_call_0_on_type_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));

                var memberExpression = methodCall.Arguments[0] as MemberExpression;
                if (memberExpression == null || memberExpression.Member.DeclaringType != typeof(QueryContext))
                    throw new NotSupportedException();
                
                switch (memberExpression.Member.Name)
                {
                    case "WritableAdministrativeUnitIds":
                        _filterExpression.Append(MemberEvaluator.Evaluate(methodCall.Arguments[1]));
                        _filterExpression.Append("=");
                        _filterExpression.Append("|AI_RED|");
                        break;
                    case "ActiveAdministrativeUnitSubHierarchyIds":
                        _filterExpression.Append(MemberEvaluator.Evaluate(methodCall.Arguments[1]));
                        _filterExpression.Append("=");
                        _filterExpression.Append("|AI_UND|");
                        break;
                    case "ActiveAdministrativeUnitHierarchyIds":
                        _filterExpression.Append(MemberEvaluator.Evaluate(methodCall.Arguments[1]));
                        _filterExpression.Append("=");
                        _filterExpression.Append("|AI_FULL|");
                        break;
                    default:
                        throw new NotSupportedException(string.Format(Resources.QueryContextVisitor_VisitMethodCall_The_method_0_on_1_is_not_supported, methodCall.Method.Name, methodCall.Method.DeclaringType));
                }
                return methodCall;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.DeclaringType != typeof(QueryContext))
					throw new NotSupportedException(string.Format(Resources.QueryContextVisitor_VisitMember_The_member_0_on_type_1_is_not_supported, node.Member.Name, node.Member.DeclaringType));

                switch (node.Member.Name)
                {
                    case "ActiveUserId":
                        _filterExpression.Append("|PE_ID|");
                        return node;
                    case "ActiveUserNameId":
                        _filterExpression.Append("|PN_ID|");
                        return node;
                    case "ActiveAdministrativeUnitId":
                        _filterExpression.Append("|AI_ID|");
                        return node;
                    case "ActiveRegistryManagementUnitId":
                        _filterExpression.Append("|JE_JENHET|");
                        return node;
					case "ActiveClassificationSystemId":
                		_filterExpression.Append("|ORDN_PRI|");
                		return node;
					case "ActiveMunicipalityId":
                		_filterExpression.Append("|KNR|");
                		return node;
                    default:
                        throw new NotSupportedException(string.Format(Resources.QueryContextVisitor_VisitMember_The_member_0_on_type_1_is_not_supported, node.Member.Name, node.Member.DeclaringType));
                }
            }
        }
    }
}
