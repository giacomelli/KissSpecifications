using System;
using HelperSharp;
using KissSpecifications.Commons;
using NUnit.Framework;

namespace KissSpecifications.UnitTests.Commons
{
    [TestFixture]
    public class MustHaveUniqueTextSpecificationTest
    {
        [Test]
        public void IsSatisfiedBy_ExistsSameName_False()
        {
            var target = new MustHaveUniqueTextSpecification<Exception>(t => t.Message, text => new Exception(text));

            Assert.IsFalse(target.IsSatisfiedBy(new Exception("TEST")));
            Assert.AreEqual(MustHaveUniqueTextSpecification<int>.NotSatisfiedReasonText.With("exception", "TEST"), target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_NotNull_True()
        {
            var target = new MustHaveUniqueTextSpecification<Exception>(t => t.Message, text => null);

            Assert.IsTrue(target.IsSatisfiedBy(new Exception("TEST")));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }
    }
}