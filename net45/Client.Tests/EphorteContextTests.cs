using System;
using System.Collections.Generic;
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

			ephorteContext.Query<Case>().Single();
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void QuerySingleOperator_With0Results_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>();

			var objectModelAdapterStub = MockRepository.GenerateStub<IObjectModelAdapter>();
			objectModelAdapterStub.StubQuery().Return(cases);

			var ephorteContext = GetEphorteContext(objectModelAdapterStub);

			ephorteContext.Query<Case>().Single();
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

			ephorteContext.Query<Case>().SingleOrDefault();
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

		private static EphorteContext GetEphorteContext(IObjectModelAdapter objectModelAdapterStub)
		{
			return new EphorteContext(objectModelAdapterStub, null, null, null);
		}
	}
}