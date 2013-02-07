using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gecko.NCore.Client.ObjectModel;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Querying
{
	[TestClass]
	public class PredicateTranslatorTests
	{
		[TestMethod]
		public void Any_ShouldConvert_To_Where()
		{
			Expression<Func<Dummy, bool>> predicateExpression = (d => d.SomeValueList.Any(x => x.SomeInt == 100));
			int? queryId;
			var translatedPredicate = PredicateTranslator.Translate(predicateExpression, out queryId);
			Assert.AreEqual(translatedPredicate, "!Dummy.SomeInt=100");
		}

		[TestMethod]
		public void TranslateEnum_ShouldConvertToValueA()
		{
			Expression<Func<Dummy, bool>> predicateExpression = (d => d.SomeEnum == EnumDummy.A);
			int? queryId;
			var translatedPredicate = PredicateTranslator.Translate(predicateExpression, out queryId);
			Assert.AreEqual(translatedPredicate, "SomeEnum=0");
		}

		[TestMethod]
		public void TranslateEnum_ShouldConvertToValueB()
		{
			Expression<Func<Dummy, bool>> predicateExpression = (d => d.SomeEnum == EnumDummy.B);
			int? queryId;
			var translatedPredicate = PredicateTranslator.Translate(predicateExpression, out queryId);
			Assert.AreEqual(translatedPredicate, "SomeEnum=1");
		}

		public class Dummy
		{
			public EnumDummy SomeEnum { get; set; }

			public int SomeInt { get; set; }

			private IDataObjectCollection<Dummy> _someValueList;

			public IDataObjectCollection<Dummy> SomeValueList
			{
				get { return _someValueList ?? (_someValueList = new TypedDataObjectCollection<Dummy>(x => x.SomeInt == SomeInt)); }
			}
		}

		public enum EnumDummy
		{
			A = 0,
			B = 1
		}
	}
}
