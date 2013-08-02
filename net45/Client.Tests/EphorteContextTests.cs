using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using Gecko.NCore.Client.Functions;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.ObjectModel.V3.En;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using FluentAssertions;

namespace Gecko.NCore.Client.Tests
{
	[TestClass]
	public class EphorteContextTests
	{
		private IObjectModelAdapter _objectModelAdapterMock;
		private IFunctionsAdapter _functionsAdapterMock;

		[TestInitialize]
		public void TestInit()
		{
			_objectModelAdapterMock = MockRepository.GenerateMock<IObjectModelAdapter>();
			_functionsAdapterMock = MockRepository.GenerateMock<IFunctionsAdapter>();
		}

		[TestCleanup]
		public void Cleanup()
		{
			// Clearing NCoreversion to avoid affecting other tests
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = null;
		}

		[TestMethod]
		public void Ctor_WithInvalidConfiguredNCoreVersion_ShouldThrowException()
		{
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = "SomeInvalidVersion";

			Action act = () => CreateEphorteContextWithObjectModelAdapter();

			act.ShouldThrow<ConfigurationErrorsException>();
		}

		[TestMethod]
		public void Ctor_WithoutObjectModelAdapterProvided_QueryingShouldThrow()
		{
			var ctx = CreateEphorteContextWithFunctionsAdapter();

			Action act = () => ctx.Query<Case>();

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutObjectModelAdapterProvided_AnyStateManagementShouldThrow()
		{
			var functionsAdapterStub = MockRepository.GenerateStub<IFunctionsAdapter>();
			var ctx = new EphorteContext(functionsAdapter: functionsAdapterStub);

			Action act = () => ctx.Attach(new object());

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutDocumentsAdapterProvided_DocumentAccessShouldThrow()
		{
			var ctx = CreateEphorteContextWithObjectModelAdapter();

			Action act = () => ctx.Documents.Checkout(123, "A", 1);

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutFunctionsAdapterProvided_FunctionsAccessShouldThrow()
		{
			var ctx = CreateEphorteContextWithObjectModelAdapter();

			Action act = () => ctx.Functions.Execute("fooo");

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutMetadataProvided_MetadataAccessShouldThrow()
		{
			var ctx = CreateEphorteContextWithObjectModelAdapter();

			Action act = () => ctx.Metadata.GetMetadata("foo", new object[0]);

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void QuerySingleOperator_WithMoreThan1Result_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>
				            {
					            new Case(),
					            new Case()
				            };

			_objectModelAdapterMock.StubQuery().Return(cases);

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			Action act = () => ephorteContext.Query<Case>().Single();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			act.ShouldThrow<InvalidOperationException>();
		}

		[TestMethod]
		public void QuerySingleOperator_With0Results_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>();

			_objectModelAdapterMock.StubQuery().Return(cases);

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			Action act = () => ephorteContext.Query<Case>().Single();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			act.ShouldThrow<InvalidOperationException>();
		}

		[TestMethod]
		public void QuerySingleOperator_With1Result_ShouldReturnTheSingleValue()
		{
			var cases = new List<Case>
				            {
					            new Case()
				            };

			_objectModelAdapterMock.StubQuery().Return(cases);

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

			var @case = ephorteContext.Query<Case>().Single();

			Assert.IsNotNull(@case);
		}

		[TestMethod]
		public void QuerySingleOrDefaultOperator_WithMoreThan1Result_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>
				            {
					            new Case(),
					            new Case()
				            };

			_objectModelAdapterMock.StubQuery().Return(cases);

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			Action act = () => ephorteContext.Query<Case>().SingleOrDefault();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			act.ShouldThrow<InvalidOperationException>();
		}

		[TestMethod]
		public void QuerySingleOrDefaultOperator_With0Results_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>();

			_objectModelAdapterMock.StubQuery().Return(cases);

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

			var @case = ephorteContext.Query<Case>().SingleOrDefault();

			Assert.IsNull(@case);
		}

		[TestMethod]
		public void QuerySingleOrDefaultOperator_With1Result_ShouldReturnTheSingleOrDefaultValue()
		{
			var cases = new List<Case>
				            {
					            new Case()
				            };

			_objectModelAdapterMock.StubQuery().Return(cases);

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

			var @case = ephorteContext.Query<Case>().SingleOrDefault();

			Assert.IsNotNull(@case);
		}

		[TestMethod]
		public void Query_WithLegacyFieldQuotingEnabled_ShouldNotQuoteValues()
		{
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = "2.0.0"; // <-- Field quoting not available

			_objectModelAdapterMock.StubQuery().Return(new List<RegistryEntry>());

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

			var query =
				from re in ephorteContext.Query<RegistryEntry>()
				where re.RecordStatusId == "I"
				select re;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var lastFilterQueryArgument = _objectModelAdapterMock.GetQueryFilterArguments();

			Assert.AreEqual("RecordStatusId=I", lastFilterQueryArgument);
		}

		[TestMethod]
		public void Query_WithLegacyFieldQuotingDisabled_ShouldQuoteValues()
		{
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = "3.1.3"; // <-- Field quoting available

			_objectModelAdapterMock.StubQuery().Return(new List<RegistryEntry>());

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

			var query =
				from re in ephorteContext.Query<RegistryEntry>()
				where re.RecordStatusId == "I"

				select re;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var lastFilterQueryArgument = _objectModelAdapterMock.GetQueryFilterArguments();

			Assert.AreEqual("RecordStatusId='I'", lastFilterQueryArgument);
		}

		/// <summary>
		/// Expression visualization:
		/// Assume the type Case is used
		/// 1 AndExpression (type: bool) // Could be any BinaryExpression
		///	2	Left: EqualExpression (type: bool)
		///	3		Left: EqualExpression (type: bool?)
		///	4			Left: MemberAccessExpression (type: int?) Ex: Case.OfficerNameId
		///	5			Right: ConstantExpression (type: int?) Ex: 123
		///	6		Right: ConstantExpression (type: bool?) Ex: True
		///	7	Right: ConstantExpression (type: bool) Ex: True (this could also be any other expression)
		/// 
		/// OData creates an expression on the form ((CaseId == 5) == true) when accessing nullable fields.
		/// We created the ODataExpressionWithNullableFieldVisitor to remove the '== true'-part.
		/// This resulted in an issue where the owning BinaryExpression (AndExpression in the sample above)
		/// would fail when constructing the new modified BinaryExpression using Expression.MakeBinary().
		/// It failed because Expression.MakeBinary requires that left and right must be of a compatible type.
		/// 
		/// In the sample above that recreates what OData will pass in the line 3 EqualExpression would replace the
		/// line2 EqualExpression by the ODataExpressionWithNuyllableFieldVisitor.
		/// This would cause left side in the AndExpression to become nullable bool and the right would be non-nullable.
		/// This caused Expression.MakeBinary to fail.
		/// 
		/// We fix this by introducing an Expression.Convert to match up the types of left and right side
		/// for the AndExpression.
		/// </summary>
		[TestMethod]
		public void Query_WithChildrenTypeMimatchForBinaryExpression_ShouldNotFail()
		{
			_objectModelAdapterMock.StubQuery().Return(new List<Case>());

			var ephorteContext = CreateEphorteContextWithObjectModelAdapter();

			var queryable = ephorteContext.Query<Case>();

			var item = Expression.Parameter(typeof(Case), "case");
			var memberAccess = Expression.Property(item, "OfficerNameId");
			var const250 = Expression.Constant(123, typeof(int?));
			
			// We need to lift the equal expression so that it also get the type <Nullable<Boolean>>
			var equal2 = Expression.Equal(memberAccess, const250, true, null);

			var trueNullableConstant = Expression.Constant(true, typeof(bool?));
			var equal = Expression.Equal(equal2, trueNullableConstant);

			var trueConstant = Expression.Constant(true, typeof (bool));
			var and = Expression.And(equal, trueConstant);

			var lambda = Expression.Lambda<Func<Case, bool>>(and, item);

			queryable = queryable.Where(lambda);

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			queryable.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		private EphorteContext CreateEphorteContextWithObjectModelAdapter()
		{
			return new EphorteContext(_objectModelAdapterMock);
		}

		private EphorteContext CreateEphorteContextWithFunctionsAdapter()
		{
			return new EphorteContext(functionsAdapter: _functionsAdapterMock);
		}
	}
}