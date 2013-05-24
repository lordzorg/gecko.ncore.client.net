using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
    internal class WildcardInserter
    {
        public static Expression Insert(Expression expression)
        {
            var binaryExpressionVisitor = new WildcardVisitor();
            return binaryExpressionVisitor.Visit(expression);
        }

        private class WildcardVisitor : ExpressionVisitor
        {

            protected override Expression VisitMethodCall(MethodCallExpression methodCall)
            {
                if (methodCall.Method.DeclaringType == typeof(string))
                {
                    return VisitStringMethodCall(methodCall);
                }

                return methodCall;
            }


            private Expression VisitStringMethodCall(MethodCallExpression methodCall)
            {
                switch (methodCall.Method.Name)
                {
                    case "StartsWith":
                        return VisitStringStartsWithMethodCall(methodCall);
                    case "EndsWith":
                        return VisitStringEndsWithMethodCall(methodCall);
                    case "Contains":
                        return VisitStringContainsMethodCall(methodCall);
                    default:
                        return methodCall;
                }
            }

            private Expression VisitStringContainsMethodCall(MethodCallExpression methodCall)
            {
                if (methodCall.Arguments[0] != null && methodCall.Arguments[0].NodeType == ExpressionType.Constant && (methodCall.Arguments[0] as ConstantExpression).Value is string)
                {
                    return Expression.Equal(methodCall.Object, Expression.Constant("*" + (methodCall.Arguments[0] as ConstantExpression).Value + "*"));
                }
                return methodCall;
            }

            private Expression VisitStringEndsWithMethodCall(MethodCallExpression methodCall)
            {
                if (methodCall.Arguments[0] != null && methodCall.Arguments[0].NodeType == ExpressionType.Constant && (methodCall.Arguments[0] as ConstantExpression).Value is string)
                {
                    return Expression.Equal(methodCall.Object, Expression.Constant("*" + (methodCall.Arguments[0] as ConstantExpression).Value));
                }
                return methodCall;
            }

            private Expression VisitStringStartsWithMethodCall(MethodCallExpression methodCall)
            {
                if (methodCall.Arguments[0] != null && methodCall.Arguments[0].NodeType == ExpressionType.Constant && (methodCall.Arguments[0] as ConstantExpression).Value is string)
                {
                    return Expression.Equal(methodCall.Object, Expression.Constant((methodCall.Arguments[0] as ConstantExpression).Value + "*"));
                }
                return methodCall;
            }

        }

    }
}
