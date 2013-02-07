using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
    internal class PredicateDenormalizer
    {
        /// <summary>
        /// Aligns the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static Expression Denormalize(Expression expression)
        {
            var binaryExpressionVisitor = new PredicateExpressionVisitor();
            return binaryExpressionVisitor.Visit(expression);
        }

        //  BinaryExpression &&
        //    Left "Test*" == d.Innholdsbeskrivelse
        //    Right (d.HarRestanse == Restansetype.Ingen || d.HarRestanse == Restansetype.PåloggetBruker)

        //  BinaryExpression ||
        //    Left "Test*" == d.Innholdsbeskrivelse && d.HarRestanse == Restansetype.Ingen
        //    Right "Test*" == d.Innholdsbeskrivelse && d.HarRestanse == Restansetype.PåloggetBruker
        class PredicateExpressionVisitor : ExpressionVisitor
        {
            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (!IsAnd(node))
                    return base.VisitBinary(node);

                var left = node.Left;
                var right = node.Right;

                if (IsOr(left))
                {
                    //  (a || b) && c
                    //  ((a && c) || (b && c)

                    var binaryLeft = (BinaryExpression) left;

                    var a = binaryLeft.Left;
                    var b = binaryLeft.Right;
                    var c = right;

                    var x = VisitBinary(Expression.MakeBinary(ExpressionType.AndAlso, a, c));
                    var y = VisitBinary(Expression.MakeBinary(ExpressionType.AndAlso, b, c));

                    return Expression.MakeBinary(ExpressionType.OrElse, x, y);
                }

                if (IsOr(right))
                {
                    //  a && (b || c)
                    //  ((a && b) || (a && c)

                    var binaryRight = (BinaryExpression)right;

                    var a = left;
                    var b = binaryRight.Left;
                    var c = binaryRight.Right;

                    var x = VisitBinary(Expression.MakeBinary(ExpressionType.AndAlso, a, b));
                    var y = VisitBinary(Expression.MakeBinary(ExpressionType.AndAlso, a, c));

                    return Expression.MakeBinary(ExpressionType.OrElse, x, y);
                }

                return base.VisitBinary(node);
            }

            private static bool IsAnd(Expression node)
            {
                return node.NodeType == ExpressionType.And || node.NodeType == ExpressionType.AndAlso;
            }

            private static bool IsOr(Expression node)
            {
                return node.NodeType == ExpressionType.Or || node.NodeType == ExpressionType.OrElse;
            }
        }
    }
}
