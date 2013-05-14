using System;
using NUnit.Framework;

namespace KissSpecifications.UnitTests
{
	[TestFixture]
	public class SpecificationAttributeTest
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_SpecificationIsNull_Exception()
		{
			new SpecificationAttribute(null);
		}

		[Test]
		public void IsValid_ValueIsNull_False()
		{
			var target = new SpecificationAttribute(typeof(TestSatisfiedSpecification));
			Assert.IsFalse(target.IsValid(null));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void IsValid_NotISpecification_Exception()
		{
			var target = new SpecificationAttribute(typeof(int));
			Assert.IsFalse(target.IsValid(1));
		}

		[Test]
		public void IsValid_SpecificationIsNotSatisfied_FalseAndErrorMessageFilled()
		{
			var target = new SpecificationAttribute(typeof(TestNotSatisfiedSpecification));
			Assert.IsFalse(target.IsValid(1));
			Assert.AreEqual("MSG", target.ErrorMessage);
		}

		[Test]
		public void IsValid_SpecificationIsSatisfied_True()
		{
			var target = new SpecificationAttribute(typeof(TestSatisfiedSpecification));
			Assert.IsTrue(target.IsValid(1));
			Assert.IsNull(target.ErrorMessage);
		}

		#region Helpers
		public class TestNotSatisfiedSpecification : ISpecification<int>
		{
			public string NotSatisfiedReason
			{
				get { return "MSG"; }
			}

			public bool IsSatisfiedBy(int obj)
			{
				return false;
			}
		}

		public class TestSatisfiedSpecification : ISpecification<int>
		{
			public string NotSatisfiedReason
			{
				get { return null; }
			}

			public bool IsSatisfiedBy(int obj)
			{
				return true;
			}
		}
		#endregion
	}
}
