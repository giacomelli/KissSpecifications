using System;
using KissSpecifications.Commons;
using NUnit.Framework;

namespace KissSpecifications.UnitTests.Commons
{
    [TestFixture]
    public class MustExistsSpecificationTest
    {
        [Test]
        public void IsSatisfiedBy_NotExists_False()
        {
            var target = new MustExistsSpecification<Exception, string>(t => t.Message, (message) => false);

            Assert.IsFalse(target.IsSatisfiedBy(new Exception("TEST")));
            Assert.AreEqual("The 'Message' with value 'TEST' not exists.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_Exists_True()
        {
            var target = new MustExistsSpecification<Exception, string>(t => t.Message, (message) => true);

            Assert.IsTrue(target.IsSatisfiedBy(new Exception()));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }
    }
}