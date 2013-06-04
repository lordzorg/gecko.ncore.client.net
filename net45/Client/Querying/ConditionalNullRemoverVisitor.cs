using System.Linq.Expressions;

namespace Gecko.NCore.Client.Querying
{
	/// <summary>
	/// The web api odata service adds a null check whenever you use a nested field within a predicate.
	/// Example:
	/// http://localhost:27369/odata/RegistryEntryDocument?$filter=(RegistryEntry/CaseId%20eq%205)
	/// 
	/// This would result in in the following expression:
	/// .If ($$it.RegistryEntry == null) {
	/// 	return null
	/// } .Else {
	/// 	return ($$it.RegistryEntry).CaseId
	/// } == .Constant System.Nullable`1[System.Int32](5)
	/// 
	/// This expression tree visitor will use the 'IfFalse'-branch if it encounters a ConditionalExpresion where
	/// 1. the test is a an equality or non-equality test
	/// 2. the right side of the test is a Null constant
	/// 3. if the test is an equality test and the IfTrue-branch is a Null constant
	///		- return the IfFalse expression
	/// 4. if the test is a not-equal test and the IfFalse-branch is a Null constnat
	///		- return the IfTrue expression
	/// 
	/// See the tests for further details
	/// </summary>
	internal class ConditionalNullRemoverVisitor : ExpressionVisitor
	{
		public static Expression RemoveReferenceTypeNullChecks(Expression expression)
		{
			var visitor = new ConditionalNullRemoverVisitor();
			return visitor.Visit(expression);
		}

		protected override Expression VisitConditional(ConditionalExpression c)
		{
			var testExpression = c.Test as BinaryExpression;
			if (testExpression == null)
				return c;

			var rightConstantExpression = testExpression.Right as ConstantExpression;
			if (rightConstantExpression == null)
				return c;

			if (rightConstantExpression.Value != null)
				return c;

			if (testExpression.NodeType == ExpressionType.Equal)
			{
				var ifTrueConstantExpression = c.IfTrue as ConstantExpression;
				if (ifTrueConstantExpression == null)
					return c;

				if (ifTrueConstantExpression.Value == null)
					return c.IfFalse;
			}
			else if (testExpression.NodeType == ExpressionType.NotEqual)
			{
				var ifFalseConstantExpression = c.IfFalse as ConstantExpression;
				if (ifFalseConstantExpression == null)
					return c;

				if (ifFalseConstantExpression.Value == null)
					return c.IfTrue;
			}

			return c;
		}
	}
}