using System;
using System.Collections.Generic;
using KissSpecifications.Commons;
using KissSpecifications.UnitTests.Stubs;
using NUnit.Framework;

namespace KissSpecifications.UnitTests.Commons
{
    [TestFixture]
    public class MustHaveNullOrDefaultPropertySpecificationTest
    {
        [Test]
        public void IsSatisfiedBy_WithStringValue_False()
        {
            var target = new MustHaveNullOrDefaultPropertySpecification<Exception>("Message");
            var entity = new Exception("Teste");

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Message' property of 'Exception' must be null or default value.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_WithIntValue_False()
        {
            var target = new MustHaveNullOrDefaultPropertySpecification<string>("Length");
            var entity = "1";

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Length' property of 'String' must be null or default value.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_WithListValue_False()
        {
            var target = new MustHaveNullOrDefaultPropertySpecification<EntityStub>("Children");
            var entity = new EntityStub() { Children = new List<int>() { 1 } };

            Assert.IsFalse(target.IsSatisfiedBy(entity));
            Assert.AreEqual("The 'Children' property of 'EntityStub' must be null or default value.", target.NotSatisfiedReason);
        }

        [Test]
        public void IsSatisfiedBy_WithStringValueNotEmptyOrNull_True()
        {
            var target = new MustHaveNullOrDefaultPropertySpecification<Exception>("Message");
            var entity = new Exception("");

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }

        [Test]
        public void IsSatisfiedBy_WithIntValueNotEmptyOrNull_True()
        {            
            var target = new MustHaveNullOrDefaultPropertySpecification<string>("Length");
            var entity = "";

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }

        [Test]
        public void IsSatisfiedBy_WithListValueNotEmptyOrNull_True()
        {
            var target = new MustHaveNullOrDefaultPropertySpecification<EntityStub>("Children");
            var entity = new EntityStub() { Children = new List<int>()};

            Assert.IsTrue(target.IsSatisfiedBy(entity));
            Assert.IsTrue(String.IsNullOrEmpty(target.NotSatisfiedReason));
        }
    }
}