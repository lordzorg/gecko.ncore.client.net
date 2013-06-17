using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.ObjectModel.V3.En;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Gecko.NCore.Client.Tests
{
	[TestClass]
	public class EphorteContextTests
	{
		[TestCleanup]
		public void Cleanup()
		{
			// Clearing NCoreversion to avoid affecting other tests
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = null;
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void QuerySingleOperator_WithMoreThan1Result_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>
				            {
					            new Case(),
					            new Case()
				            };

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			ephorteContext.Query<Case>().Single();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void QuerySingleOperator_With0Results_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>();

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			ephorteContext.Query<Case>().Single();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		public void QuerySingleOperator_With1Result_ShouldReturnTheSingleValue()
		{
			var cases = new List<Case>
				            {
					            new Case()
				            };

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

			var @case = ephorteContext.Query<Case>().Single();

			Assert.IsNotNull(@case);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void QuerySingleOrDefaultOperator_WithMoreThan1Result_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>
				            {
					            new Case(),
					            new Case()
				            };

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			ephorteContext.Query<Case>().SingleOrDefault();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		public void QuerySingleOrDefaultOperator_With0Results_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>();

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

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

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

			var @case = ephorteContext.Query<Case>().SingleOrDefault();

			Assert.IsNotNull(@case);
		}

		[TestMethod]
		public void Query_WithLegacyFieldQuotingEnabled_ShouldNotQuoteValues()
		{
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = "2.0.0"; // <-- Field quoting not available

			var objectModelAdapterMock = MockRepository.GenerateMock<IObjectModelAdapter>();
			objectModelAdapterMock.StubQuery().Return(new List<RegistryEntry>());

			var ephorteContext = GetEphorteContext(objectModelAdapterMock);

			var query =
				from re in ephorteContext.Query<RegistryEntry>()
				where re.RecordStatusId == "I"
				select re;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var lastFilterQueryArgument = objectModelAdapterMock.GetQueryFilterArguments();

			Assert.AreEqual("RecordStatusId=I", lastFilterQueryArgument);
		}

		[TestMethod]
		public void Query_WithLegacyFieldQuotingDisabled_ShouldQuoteValues()
		{
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = "3.1.3"; // <-- Field quoting available

			var objectModelAdapterMock = MockRepository.GenerateMock<IObjectModelAdapter>();
			objectModelAdapterMock.StubQuery().Return(new List<RegistryEntry>());

			var ephorteContext = GetEphorteContext(objectModelAdapterMock);

			var query =
				from re in ephorteContext.Query<RegistryEntry>()
				where re.RecordStatusId == "I"

				select re;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var lastFilterQueryArgument = objectModelAdapterMock.GetQueryFilterArguments();

			Assert.AreEqual("RecordStatusId='I'", lastFilterQueryArgument);
		}

		[TestMethod]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void Query_WithInvalidConfiguredNCoreVersion_ShouldThrowException()
		{
			ConfigurationManager.AppSettings["EphorteContext:NCoreVersion"] = "SomeInvalidVersion";

			var objectModelAdapterMock = MockRepository.GenerateMock<IObjectModelAdapter>();

			var ephorteContext = GetEphorteContext(objectModelAdapterMock);

			var query =
				from re in ephorteContext.Query<RegistryEntry>()
				where re.RecordStatusId == "I"

				select re;

			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		private static EphorteContext GetEphorteContext(IObjectModelAdapter objectModelAdapterStub)
		{
			return new EphorteContext(objectModelAdapterStub, null, null, null);
		}
	}
}