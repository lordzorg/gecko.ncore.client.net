using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.ObjectModel.V3.En.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class DataObjectAccess
	{
		public TestContext TestContext { get; set; }

		[TestMethod]
		public void IsPropertyReadOnly_ShouldReturn_False()
		{
			var dataObject = new Case();
			var requiredFlags = new CasePropertyFlags();
			var readOnlyFlags = new CasePropertyFlags { Title = true };
			var objectRights = new ObjectRights();
			
			var dataObjectAccess = new En.DataObjectAccess(dataObject, requiredFlags, readOnlyFlags, objectRights);

			Assert.IsFalse(dataObjectAccess.IsPropertyReadOnly("Title"));
		}

		[TestMethod]
		public void IsPropertyReadOnly_ShouldReturn_True()
		{
			var dataObject = new Case();
			var requiredFlags = new CasePropertyFlags();
			var readOnlyFlags = new CasePropertyFlags { Title = false };
			var objectRights = new ObjectRights();

			var dataObjectAccess = new En.DataObjectAccess(dataObject, requiredFlags, readOnlyFlags, objectRights);

			Assert.IsTrue(dataObjectAccess.IsPropertyReadOnly("Title"));
		}
	}
}
