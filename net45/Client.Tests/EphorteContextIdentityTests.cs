using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests
{
	[TestClass]
	public class EphorteContextIdentityTests
	{
		private EphorteContextIdentity _target;

		private const string SampleExternalSystemName = "foobar";

		[TestMethod]
		// ReSharper disable InconsistentNaming
		public void InitializedFromEmptyConstructor_WhenConfigIsSet_ShouldSetExternalSystemNameFromConfig()
		// ReSharper restore InconsistentNaming
		{
			WhenConfigIsSet();

			_target = new EphorteContextIdentity();

			Assert.AreEqual(SampleExternalSystemName, _target.ExternalSystemName);
		}

		[TestMethod]
		// ReSharper disable InconsistentNaming
		public void InitializedFromEmptyConstructor_WhenConfigIsNotSet_ShouldSetExternalSystemNameFromConfig()
		// ReSharper restore InconsistentNaming
		{
			WhenConfigIsNotSet();
			
			_target = new EphorteContextIdentity();

			Assert.IsNull(_target.ExternalSystemName);
		}

		private static void WhenConfigIsNotSet()
		{
			ConfigurationManager.AppSettings["ExternalSystemName"] = null;
		}

		private static void WhenConfigIsSet()
		{
			ConfigurationManager.AppSettings["ExternalSystemName"] = SampleExternalSystemName;
		}
	}
}
