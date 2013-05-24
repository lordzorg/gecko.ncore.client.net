using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gecko.NCore.Client.Querying
{
    internal class ConstantQuotifier
    {
        public static Expression Quotify(Expression expression)
        {
            var binaryExpressionVisitor = new QuotifyVisitor();
            return binaryExpressionVisitor.Visit(expression);
        }

        private class QuotifyVisitor : ExpressionVisitor
        {
            protected override Expression VisitConstant(ConstantExpression constant)
            {
                if (constant.Value == null)
                {
                    return constant;
                }

                if (constant.Value is string)
                {
                    return Expression.Constant(NCoreUtility.QuoteField(constant.Value as string));
                }

                if (constant.Value is IEnumerable<string>)
                {
                    var c = Expression.Constant((constant.Value as IEnumerable<string>).Select(NCoreUtility.QuoteField).ToList());
                    return c;
                }

                return constant;
            }
        }
    }
}
