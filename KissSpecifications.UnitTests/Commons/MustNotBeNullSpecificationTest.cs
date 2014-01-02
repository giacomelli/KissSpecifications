using System;
using System.Collections.Generic;
using KissSpecifications.Commons;
using KissSpecifications.UnitTests.Stubs;
using NUnit.Framework;

namespace KissSpecifications.UnitTests.Commons
{
    [TestFixture]
    public class MustNotBeNullSpecificationTest
    {
        [Test]
        public void IsSatisfiedBy_Null_False()
        {
            var target = new MustNotBeNullSpecification<Exception>();
            
            Assert.IsFalse(target.IsSatisfiedBy(null));
            Assert.AreEqual("The 'Exception' must not be null.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_NotNull_True()
        {
            var target = new MustNotBeNullSpecification<Exception>();

            Assert.IsTrue(target.IsSatisfiedBy(new Exception()));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }
    }
}