using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Gecko.NCore.Client.Functions;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.ObjectModel.V3.En;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Task = System.Threading.Tasks.Task;

namespace Gecko.NCore.Client.Tests
{
	[TestClass]
	public class AsyncEphorteContextTests
	{
		private IAsyncObjectModelAdapter _objectModelAdapterMock;
		private IAsyncFunctionsAdapter _functionsAdapterMock;

		[TestInitialize]
		public void TestInit()
		{
			_objectModelAdapterMock = MockRepository.GenerateMock<IAsyncObjectModelAdapter>();
			_functionsAdapterMock = MockRepository.GenerateMock<IAsyncFunctionsAdapter>();
		}

		[TestMethod]
		public void Ctor_WithoutDocumentsAdapterProvided_DocumentAccessShouldThrow()
		{
			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();

			Action act = () => ctx.Documents.Checkout(123, "A", 1);

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutObjectModelAdapterProvided_AnyStateManagementShouldThrow()
		{
			var ctx = CreateAsyncEphorteContextWithFunctionsAdapter();

			Action act = () => ctx.Attach(new object());

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutFunctionsAdapterProvided_FunctionsAccessShouldThrow()
		{
			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();

			Action act = () => ctx.Functions.Execute("fooo");

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void Ctor_WithoutMetadataProvided_MetadataAccessShouldThrow()
		{
			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();

			Action act = () => ctx.Metadata.GetMetadata("foo", new object[0]);

			act.ShouldThrow<NotSupportedException>();
		}

		[TestMethod]
		public void ToListAsync_ShouldReturnResults()
		{
			var fakeResults = new List<Case>
				                  {
					                  new Case()
				                  };

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryAsync()
				.Return(Task.Run<IEnumerable<object>>(() => fakeResults));

			var queryable = ctx.Query<Case>();

			var list = queryable.ToListAsync();

			var res = list.Result;

			Assert.AreEqual(1, res.Count);
		}

		[TestMethod]
		public void ToArrayAsync_ShouldReturnArrayOfResult()
		{
			var fakeResult = new List<Case> {new Case()};

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();

			_objectModelAdapterMock.StubQueryAsync()
			                       .Return(Task.Run<IEnumerable<object>>(() => fakeResult));

			var queryable = ctx.Query<Case>();

			var resultArray = queryable.ToArrayAsync().Result;

			Assert.AreEqual(1, resultArray.Length);
		}

		[TestMethod]
		public void AnyAsync_WhenMoreThanZeroResults_IsTrue()
		{
			const int fakeCount = 1;

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryCountAsync()
				.Return(Task.Run(() => fakeCount));

			var queryable = ctx.Query<Case>();

			var list = queryable.AnyAsync(x => true);

			var any = list.Result;

			Assert.IsTrue(any);
		}

		[TestMethod]
		public void AnyAsync_ZeroResults_IsFalse()
		{
			const int fakeCount = 0;

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryCountAsync()
				.Return(Task.Run(() => fakeCount));

			var queryable = ctx.Query<Case>();

			var list = queryable.AnyAsync();

			var any = list.Result;
			Assert.IsFalse(any);
		}

		[TestMethod]
		public void FirstAsync_WithZeroElements_ThrowsException()
		{
			var fakeResult = new List<object>();

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryAsync()
				.Return(Task.Run<IEnumerable<object>>(() => fakeResult));

			var queryable = ctx.Query<Case>();

			try
			{
#pragma warning disable 168
				var result = queryable.FirstAsync().Result;
#pragma warning restore 168

				Assert.Fail();
			}
			catch (AggregateException e)
			{
				if (e.InnerException.GetType() != typeof(InvalidOperationException))
					Assert.Fail();
			}
		}

		[TestMethod]
		public void FirstAsync_WithMoreThan1Elements_ReturnFirst()
		{
			var fakeResult = new List<object>
				                 {
					                 new Case{ Id = 123},
					                 new Case{ Id = 234}
				                 };

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryAsync()
				.Return(Task.Run<IEnumerable<object>>(() => fakeResult));

			var queryable = ctx.Query<Case>();

			var firstCase = queryable.FirstAsync().Result;

			Assert.AreEqual(123, firstCase.Id);
		}

		[TestMethod]
		public void FirstOrDefaultAsync_WithZeroElements_ReturnsNull()
		{
			var fakeResult = new List<object>();

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryAsync()
				.Return(Task.Run<IEnumerable<object>>(() => fakeResult));

			var queryable = ctx.Query<Case>();

			var result = queryable.FirstOrDefaultAsync().Result;
			Assert.IsNull(result);
		}

		[TestMethod]
		public void FirstOrDefaultAsync_WithMoreThan1Elements_ReturnFirst()
		{
			var fakeResult = new List<object>
								 {
									 new Case{ Id = 123},
									 new Case{ Id = 234}
								 };

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryAsync()
				.Return(Task.Run<IEnumerable<object>>(() => fakeResult));

			var queryable = ctx.Query<Case>();

			var firstCase = queryable.FirstOrDefaultAsync().Result;

			Assert.AreEqual(123, firstCase.Id);
		}

		[TestMethod]
		public void CountAsync_ShouldReturnCount()
		{
			const int fakeCount = 15;

			var ctx = CreateAsyncEphorteContextWithObjectModelAdapter();
			_objectModelAdapterMock
				.StubQueryCountAsync()
				.Return(Task.Run(() => fakeCount));

			var queryable = ctx.Query<Case>();

			var count = queryable.CountAsync().Result;

			Assert.AreEqual(15, count);
		}

		[TestMethod]
		public void QuerySingleAsyncOperator_WithMoreThan1Result_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>
				            {
					            new Case(),
					            new Case()
				            };

			_objectModelAdapterMock.StubQueryAsync().Return(Task.Run<IEnumerable<object>>(() => cases));

			var ephorteContext = CreateAsyncEphorteContextWithObjectModelAdapter();

			try
			{
#pragma warning disable 168
				var result = ephorteContext.Query<Case>().SingleAsync().Result;
#pragma warning restore 168
				Assert.Fail();
			}
			catch (AggregateException e)
			{
				if (e.InnerException.GetType() != typeof(InvalidOperationException))
					Assert.Fail();
			}
		}

		[TestMethod]
		public void QuerySingleAsyncOperator_With0Results_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>();

			_objectModelAdapterMock.StubQueryAsync().Return(Task.Run<IEnumerable<object>>(() => cases));

			var ephorteContext = CreateAsyncEphorteContextWithObjectModelAdapter();

			try
			{
#pragma warning disable 168
				var result = ephorteContext.Query<Case>().SingleAsync().Result;
#pragma warning restore 168
				Assert.Fail();
			}
			catch (AggregateException e)
			{
				if (e.InnerException.GetType() != typeof(InvalidOperationException))
					Assert.Fail();
			}
		}

		[TestMethod]
		public void QuerySingleAsyncOperator_With1Result_ShouldReturnTheSingleValue()
		{
			var cases = new List<Case>
							{
								new Case()
							};

			_objectModelAdapterMock.StubQueryAsync().Return(Task.Run<IEnumerable<object>>(() => cases));

			var ephorteContext = CreateAsyncEphorteContextWithObjectModelAdapter();

			var @case = ephorteContext.Query<Case>().SingleAsync().Result;

			Assert.IsNotNull(@case);
		}

		[TestMethod]
		public void QuerySingleOrDefaultAsyncOperator_WithMoreThan1Result_ShouldThrowInvalidOperationException()
		{
			var cases = new List<Case>
				            {
					            new Case(),
					            new Case()
				            };

			_objectModelAdapterMock.StubQueryAsync().Return(Task.Run<IEnumerable<object>>(() => cases));

			var ephorteContext = CreateAsyncEphorteContextWithObjectModelAdapter();

			try
			{
#pragma warning disable 168
				var result = ephorteContext.Query<Case>().SingleOrDefaultAsync().Result;
#pragma warning restore 168
				Assert.Fail();
			}
			catch (AggregateException e)
			{
				if (e.InnerException.GetType() != typeof(InvalidOperationException))
					Assert.Fail();
			}
		}

		[TestMethod]
		public void QuerySingleOrDefaultAsyncOperator_With0Results_ShouldReturnNull()
		{
			var cases = new List<Case>();

			_objectModelAdapterMock.StubQueryAsync().Return(Task.Run<IEnumerable<object>>(() => cases));

			var ephorteContext = CreateAsyncEphorteContextWithObjectModelAdapter();

			var result = ephorteContext.Query<Case>().SingleOrDefaultAsync().Result;

			result.Should().BeNull();
		}

		[TestMethod]
		public void QuerySingleOrDefaultAsyncOperator_With1Result_ShouldReturnTheSingleValue()
		{
			var cases = new List<Case>
							{
								new Case()
							};

			_objectModelAdapterMock.StubQueryAsync().Return(Task.Run<IEnumerable<object>>(() => cases));

			var ephorteContext = CreateAsyncEphorteContextWithObjectModelAdapter();

			var @case = ephorteContext.Query<Case>().SingleOrDefaultAsync().Result;

			@case.Should().NotBeNull();
		}

		private EphorteContext CreateAsyncEphorteContextWithObjectModelAdapter()
		{
			return new EphorteContext(_objectModelAdapterMock);
		}

		private EphorteContext CreateAsyncEphorteContextWithFunctionsAdapter()
		{
			return new EphorteContext(functionsAdapter: _functionsAdapterMock);
		}
	}
}
