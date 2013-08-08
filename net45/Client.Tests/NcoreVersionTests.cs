using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests
{
	[TestClass]
	public class NcoreVersionTests
	{
		[TestMethod]
		public void Latest_ShouldReturnNcoreVersionOfMaxVersion()
		{
			var latest = NcoreVersion.Latest;

			Assert.AreEqual(new Version(int.MaxValue, int.MaxValue, int.MaxValue), latest.Version);
		}
	}
}
