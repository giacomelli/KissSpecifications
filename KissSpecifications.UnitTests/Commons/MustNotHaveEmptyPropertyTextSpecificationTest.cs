using System;
using KissSpecifications.Commons;
using NUnit.Framework;

namespace KissSpecifications.UnitTests.Commons
{
    [TestFixture]
    public class MustNotHaveEmptyPropertyTextSpecification
    {
        [Test]
        public void IsSatisfiedBy_EmptyOrNull_False()
        {
            var target = new MustNotHaveEmptyPropertyTextSpecification<Exception>("Message");
            var entity = new Exception("");

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Message' property of 'Exception' is required.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_NotEmptyOrNull_True()
        {
            var target = new MustNotHaveEmptyPropertyTextSpecification<Exception>("Message");
            var entity = new Exception("Test");

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }
    }
}
