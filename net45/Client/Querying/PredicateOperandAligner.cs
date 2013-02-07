using System;
using System.Linq.Expressions;
using Gecko.NCore.Client.Properties;

namespace Gecko.NCore.Client.Querying
{
    /// <summary>
    /// 
    /// </summary>
    internal class PredicateOperandAligner
    {
        /// <summary>
        /// Aligns the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static Expression Align(Expression expression)
        {
            var binaryExpressionVisitor = new PredicateVisitor();
            return binaryExpressionVisitor.Visit(expression);
        }

        class PredicateVisitor: ExpressionVisitor
        {
            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.Constant)
                    throw new NotSupportedException(Resources.PredicateVisitor_VisitBinary_Binary_conditions_with_two_constants_as_operands_are_not_supported);

                if (node.Left.NodeType == ExpressionType.Constant)
                    return Expression.MakeBinary(node.NodeType, Visit(node.Right), Visit(node.Left), node.IsLiftedToNull, node.Method, node.Conversion);

                return Expression.MakeBinary(node.NodeType, Visit(node.Left), Visit(node.Right), node.IsLiftedToNull, node.Method, node.Conversion);
            }
        }
    }
}
