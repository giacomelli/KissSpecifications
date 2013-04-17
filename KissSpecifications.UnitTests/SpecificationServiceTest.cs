using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace KissSpecifications.Tests
{
	[TestClass]
	public class SpecificationServiceTest
	{
		#region Tests
		[TestMethod]
		public void FilterSpecificationsAreNotSatisfiedBy_NullObject_AllSpecificationsAreNotSatisfied()
		{
			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<object>(null, CreateSpecifications());
			Assert.AreEqual(2, not.Length);
		}

		[TestMethod]
		public void FilterSpecificationsAreNotSatisfiedBy_WithOneNotSatisfied_ReturnsOneNotSatisfied()
		{
			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<object>(new object(), CreateSpecifications());
			Assert.AreEqual(1, not.Length);
		}

		[TestMethod]
		[ExpectedException(typeof(SpecificationNotSatisfiedException))]
		public void ThrowIfAnySpecificationIsNotSatisfiedBy_WithSecondNotSatisfied_ThrowsExceptionWithSecondSpecification()
		{
			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy<object>(new object(), CreateSpecifications());
		}
		#endregion

		#region Helpers
		private ISpecification<object>[] CreateSpecifications()
		{
			var spec1 = MockRepository.GenerateMock<ISpecification<object>>();
			spec1.Expect(s => s.IsSatisfiedBy(null)).IgnoreArguments().Return(true);
			spec1.Expect(s => s.NotSatisfiedReason).Return("Reason 1");

			var spec2 = MockRepository.GenerateMock<ISpecification<object>>();
			spec2.Expect(s => s.IsSatisfiedBy(null)).IgnoreArguments().Return(false);
			spec2.Expect(s => s.NotSatisfiedReason).Return("Reason 2");


			var specs = new List<ISpecification<object>>();
			specs.Add(spec1);
			specs.Add(spec2);

			return specs.ToArray();
		}
		#endregion
	}
}
