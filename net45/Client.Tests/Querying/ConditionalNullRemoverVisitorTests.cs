using System;
using System.Linq.Expressions;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Querying
{
	[TestClass]
	public class ConditionalNullRemoverVisitorTests
	{
		[TestMethod]
		public void RemoveReferenceTypeNullChecks_WhenConditionalExpTestIsAnEqualityCheck_And_RightChildOfTestExpIsConstantExpOfNull_And_IfTrueValueIsConstantOfNull_ShouldReturnIfFalseExpression()
		{
			var t = new OuterType
				        {
					InnerType = new InnerType
						            {
							            Value = 123
						            }
				};

			Expression<Func<bool>> initialExpression = () => (t.InnerType == null ? null : t.InnerType.Value) == (int?) 123;
			Expression<Func<bool>> expectedExpression = () => t.InnerType.Value == (int?)123;

			Expression actualExpression = ConditionalNullRemoverVisitor.RemoveReferenceTypeNullChecks(initialExpression);

			Assert.AreEqual(expectedExpression.ToString(), actualExpression.ToString());
		}

		[TestMethod]
		public void RemoveReferenceTypeNullChecks_RightChildOfTestExpIsNOTConstantExpOfNull_ShouldReturnOriginalExpression()
		{
			var t = new OuterType
				        {
				InnerType = new InnerType
				{
					Value = 123
				}
			};

			Expression<Func<bool>> initialExpression = () => (t.InnerType == new InnerType() ? null : t.InnerType.Value) == (int?)123;

			Expression actualExpression = ConditionalNullRemoverVisitor.RemoveReferenceTypeNullChecks(initialExpression);

			Assert.AreEqual(initialExpression.ToString(), actualExpression.ToString());
		}

		[TestMethod]
		public void RemoveReferenceTypeNullChecks_IfTrueValueIsNOTConstantOfNull_ShouldReturnOriginalExpression()
		{
			var t = new OuterType
				        {
				InnerType = new InnerType
				{
					Value = 123
				}
			};

			Expression<Func<bool>> initialExpression = () => (t.InnerType == null ? 999 : t.InnerType.Value) == (int?)123;

			Expression actualExpression = ConditionalNullRemoverVisitor.RemoveReferenceTypeNullChecks(initialExpression);

			Assert.AreEqual(initialExpression.ToString(), actualExpression.ToString());
		}

		[TestMethod]
		public void RemoveReferenceTypeNullChecks_WhenConditionalExpTestIsANotEqualityCheck_And_RightChildOfTestExpIsConstantExpOfNull_And_IfFalseValueIsConstantOfNull_ShouldReturnIfFalseExpression()
		{
			var t = new OuterType
				        {
				InnerType = new InnerType
				{
					Value = 123
				}
			};

			Expression<Func<bool>> initialExpression = () => (t.InnerType != null ? t.InnerType.Value: null) == (int?)123;
			Expression<Func<bool>> expectedExpression = () => t.InnerType.Value == (int?)123;

			Expression actualExpression = ConditionalNullRemoverVisitor.RemoveReferenceTypeNullChecks(initialExpression);

			Assert.AreEqual(expectedExpression.ToString(), actualExpression.ToString());
		}

		class OuterType
		{
			public InnerType InnerType { get; set; }
		}

		class InnerType
		{
			public int? Value { get; set; }
		}
	}
}