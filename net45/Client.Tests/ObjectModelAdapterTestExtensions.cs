using System.Collections.Generic;
using System.Threading.Tasks;
using Gecko.NCore.Client.ObjectModel;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Gecko.NCore.Client.Tests
{
	static class ObjectModelAdapterTestExtensions
	{
		public static IMethodOptions<IEnumerable<object>> StubQuery(this IObjectModelAdapter @this)
		{
			return @this.Stub(x => x.Query(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<IEnumerable<string>>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything));
		}

		public static IList<object[]> GetQueryArguments(this IObjectModelAdapter @this)
		{
			return @this.GetArgumentsForCallsMadeOn(x => x.Query(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<IEnumerable<string>>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Anything));
		}

		public static string GetQueryFilterArguments(this IObjectModelAdapter @this)
		{
			return (string) @this.GetQueryArguments()[0][1];
		}
	}

	static class AsyncObjectModelAdapterTestExtensions
	{
		public static IMethodOptions<Task<IEnumerable<object>>> StubQueryAsync(this IAsyncObjectModelAdapter @this)
		{
			return @this.Stub(x => x.QueryAsync(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<IEnumerable<string>>.Is.Anything, Arg<int?>.Is.Anything, Arg<int?>.Is.Anything));
		}

		public static IMethodOptions<Task<int>> StubQueryCountAsync(this IAsyncObjectModelAdapter @this)
		{
			return @this.Stub(x => x.QueryCountAsync(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything));
		}
	}
}
