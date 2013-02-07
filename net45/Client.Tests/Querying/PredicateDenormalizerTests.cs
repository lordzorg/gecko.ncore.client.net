using System;
using System.Linq.Expressions;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Querying
{
    [TestClass]
    public class PredicateDenormalizerTests
    {
        [TestMethod]
        public void LeftOr()
        {
            var a = true;
            var b = true;
            var c = true;

            // (a || b) && c
            // ((a && c) || (b && c)
            Expression<Func<bool>> initialExpression = () => (a || b) && c;
            Expression<Func<bool>> expectedExpression = () => (a && c) || (b && c);

            var actualExpression = PredicateDenormalizer.Denormalize(initialExpression);

            Assert.AreEqual(expectedExpression.ToString(), actualExpression.ToString());
        }

        [TestMethod]
        public void RightOr()
        {
            var a = true;
            var b = true;
            var c = true;

            Expression<Func<bool>> initialExpression = () => a && (b || c);
            Expression<Func<bool>> expectedExpression = () => (a && b) || (a && c);

            var actualExpression = PredicateDenormalizer.Denormalize(initialExpression);

            Assert.AreEqual(expectedExpression.ToString(), actualExpression.ToString());
        }

    }
}
