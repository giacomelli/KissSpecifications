using System;
using System.Collections.Generic;
using KissSpecifications.Commons;
using KissSpecifications.UnitTests.Stubs;
using NUnit.Framework;

namespace KissSpecifications.UnitTests.Commons
{
    [TestFixture]
    public class MustNotHaveNullOrDefaultPropertySpecificationTest
    {
        [Test]
        public void IsSatisfiedBy_WithoutStringValue_False()
        {
            var target = new MustNotHaveNullOrDefaultPropertySpecification<Exception>("Message");
            var entity = new Exception("");

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Message' property of 'Exception' must not have null or default value.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_WithoutIntValue_False()
        {
            var target = new MustNotHaveNullOrDefaultPropertySpecification<string>("Length");
            var entity = "";

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Length' property of 'String' must not have null or default value.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_WithoutListValue_False()
        {
            var target = new MustNotHaveNullOrDefaultPropertySpecification<EntityStub>("Children");
            var entity = new EntityStub() { Children = new List<int>()  };

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Children' property of 'EntityStub' must not have null or default value.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_WithStringValueNotEmptyOrNull_True()
        {
            var target = new MustNotHaveNullOrDefaultPropertySpecification<Exception>("Message");
            var entity = new Exception("1");

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }

        [Test]
        public void IsSatisfiedBy_WithIntValueNotEmptyOrNull_True()
        {            
            var target = new MustNotHaveNullOrDefaultPropertySpecification<string>("Length");
            var entity = "1";

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }

        [Test]
        public void IsSatisfiedBy_WithListValueNotEmptyOrNull_True()
        {
            var target = new MustNotHaveNullOrDefaultPropertySpecification<EntityStub>("Children");
            var entity = new EntityStub() { Children = new List<int>() { 1 }};

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }
    }
}