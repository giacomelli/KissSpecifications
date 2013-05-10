using System.Collections.Generic;
using Rhino.Mocks;
using NUnit.Framework;
using TestSharp;

namespace KissSpecifications.Tests
{
	[TestFixture]
	public class SpecificationServiceTest
	{
		#region Tests
		[Test]
		public void FilterSpecificationsAreNotSatisfiedBy_NullObject_AllSpecificationsAreNotSatisfied()
		{
			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<object>(null, CreateSpecifications());
			Assert.AreEqual(2, not.Length);
		}

		[Test]
		public void FilterSpecificationsAreNotSatisfiedBy_WithOneNotSatisfied_ReturnsOneNotSatisfied()
		{
			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<object>(new object(), CreateSpecifications());
			Assert.AreEqual(1, not.Length);
		}

		[Test]
		[ExpectedException(typeof(SpecificationNotSatisfiedException))]
		public void ThrowIfAnySpecificationIsNotSatisfiedBy_WithSecondNotSatisfied_ThrowsExceptionWithSecondSpecification()
		{
			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy<object>(new object(), CreateSpecifications());
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedByAny_OneNotSatisfiedBy_Throw()
		{
			var spec1 = MockRepository.GenerateMock<ISpecification<int>>();
			spec1.Expect(s => s.IsSatisfiedBy(1)).Return(true);
			spec1.Expect(s => s.IsSatisfiedBy(2)).Return(true);
			spec1.Expect(s => s.IsSatisfiedBy(3)).Return(true);

			var spec2 = MockRepository.GenerateMock<ISpecification<int>>();
			spec2.Expect(s => s.IsSatisfiedBy(2)).Return(false);
			spec2.Expect(s => s.NotSatisfiedReason).Return("2 é inválido");

			var spec3 = MockRepository.GenerateMock<ISpecification<int>>();
			spec3.Expect(s => s.IsSatisfiedBy(3)).Return(false);

			ExceptionAssert.IsThrowing(new SpecificationNotSatisfiedException("2 é inválido"), () =>
			{
				SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny(new int[] { 1, 2, 3 }, spec1, spec2, spec3);
			});
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedByAny_AllSatisfiedBy_NotThrow()
		{
			var spec1 = MockRepository.GenerateMock<ISpecification<int>>();
			spec1.Expect(s => s.IsSatisfiedBy(1)).Return(true);
			spec1.Expect(s => s.IsSatisfiedBy(2)).Return(true);
			spec1.Expect(s => s.IsSatisfiedBy(3)).Return(true);

			var spec2 = MockRepository.GenerateMock<ISpecification<int>>();
			spec2.Expect(s => s.IsSatisfiedBy(1)).Return(true);
			spec2.Expect(s => s.IsSatisfiedBy(2)).Return(true);
			spec2.Expect(s => s.IsSatisfiedBy(3)).Return(true);

			var spec3 = MockRepository.GenerateMock<ISpecification<int>>();
			spec3.Expect(s => s.IsSatisfiedBy(1)).Return(true);
			spec3.Expect(s => s.IsSatisfiedBy(2)).Return(true);
			spec3.Expect(s => s.IsSatisfiedBy(3)).Return(true);

			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny(new int[] { 1, 2, 3 }, spec1, spec2, spec3);
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
