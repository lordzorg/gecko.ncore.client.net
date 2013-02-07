using System;
using System.Linq;
using System.Linq.Expressions;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Querying
{
    [TestClass]
    public class SelectVisitorTests
    {
        [TestMethod]
        public void Exploration()
        {
            Expression<Func<DummyA, object>> expression = (a) => new { Truls =  a.B1, Morten = a.B1.Tittel};

            var visitor = new SelectVisitor();
            visitor.Visit(expression.Body);

            Assert.AreEqual(1, visitor.RelatedObjects.Count());
            Assert.AreEqual("B1", visitor.RelatedObjects.ElementAt(0));
        }
    }
}
