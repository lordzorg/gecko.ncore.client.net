using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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