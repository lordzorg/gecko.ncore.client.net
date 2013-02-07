using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Gecko.NCore.Client.ObjectModel;

namespace Gecko.NCore.Client.Querying
{
    internal static class MemberEvaluator
    {
        public static string Evaluate(Expression expression)
        {
            var members = new List<string>();
            var memberExpressionVisitor = new MemberExpressionVisitor(members);
            memberExpressionVisitor.Visit(expression);
            return string.Join(".", members.ToArray());
        }


        private class MemberExpressionVisitor: ExpressionVisitor
        {
            private readonly IList<string> _member;

            public MemberExpressionVisitor(IList<string> member)
            {
                _member = member;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
				switch (node.Expression.NodeType)
                {
                    case ExpressionType.Parameter:
						var propertyInfo = node.Member as PropertyInfo;
						if (propertyInfo != null && typeof(IDataObjectCollection).IsAssignableFrom(propertyInfo.PropertyType))
						{
							var elementType = propertyInfo.PropertyType.GetGenericArguments()[0];
							_member.Insert(0, "!"+elementType.Name);
							return node;
						}

						_member.Insert(0, node.Member.Name);
                        return node;
                    case ExpressionType.MemberAccess:
                        if (node.Member.DeclaringType.IsGenericType && node.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>) && node.Member.Name == "Value")
                            return base.Visit(node.Expression);

                        _member.Insert(0, node.Member.Name);
                        return base.VisitMember(node);
                    default:
                        throw new NotSupportedException(string.Format("The member '{0}' is not supported", node.Member.Name));
                }
            }
        }
    }
}