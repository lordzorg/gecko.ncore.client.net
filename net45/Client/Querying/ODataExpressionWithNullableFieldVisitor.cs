using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// This visitor handles Web Api Odata expression trees with BinaryExpressions involving a nullable field
	/// 
	/// For the expression 'RegistryEntry.CaseId == 5' where CaseId is a nullable field the following expression is passed in from Web Api Odata:
	/// 
	/// ((CaseId == 5) == true)
	/// 
	/// This visitor strips away the outer '== true' BinaryExpression and keep evaluating the left expression.
	/// </summary>
	internal class ODataExpressionWithNullableFieldVisitor: ExpressionVisitor
	{
		public static Expression StripAwayOuterTrueCheck(Expression expression)
		{
			var visitor = new ODataExpressionWithNullableFieldVisitor();
			return visitor.Visit(expression);
		}

		protected override Expression VisitBinary(BinaryExpression binary)
		{
			if (binary.NodeType != ExpressionType.Equal)
				return base.VisitBinary(binary);

			var right = binary.Right as ConstantExpression;
			if (right == null)
				return base.VisitBinary(binary);

			var rightConstantValue = right.Value;
			if (rightConstantValue == null || rightConstantValue.GetType() != typeof(bool))
				return base.VisitBinary(binary);

			var rightBool = (bool) rightConstantValue;
			if (!rightBool)
				return base.VisitBinary(binary);

			var left = binary.Left as BinaryExpression;
			if (left == null)
				return base.VisitBinary(binary);

			return base.VisitBinary(left);
		}
	}
}
