using System;
using System.Linq.Expressions;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Querying
{
    [TestClass]
    public class MemberEvaluatorTests
    {
        [TestMethod]
        public void Evaluate_Nested_Member()
        {
            Expression<Func<DummyA, DummyA>> expression = a => a.B1.A;

            var actualMemberName = MemberEvaluator.Evaluate(expression);
            Assert.AreEqual("B1.A", actualMemberName);
        }

        [TestMethod]
        public void Evaluate_Member()
        {
            Expression<Func<DummyA, string>> expression = (a) => a.B1.Tittel;

            var actualMemberName = MemberEvaluator.Evaluate(expression);
            Assert.AreEqual("B1.Tittel", actualMemberName);
        }
    }
}
