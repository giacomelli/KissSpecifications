using System.Collections.Generic;
using Rhino.Mocks;
using NUnit.Framework;
using TestSharp;
using KissSpecifications.Samples.Domain;

namespace KissSpecifications.UnitTests
{
	[TestFixture]
	public class SpecificationServiceTest
	{
		#region Tests

		[Test]
		public void FilterSpecificationsAreNotSatisfiedBy_NullObject_AllSpecificationsAreNotSatisfied ()
		{
			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<object> (null, CreateSpecifications ());
			Assert.AreEqual (2, not.Length);
		}

		[Test]
		public void FilterSpecificationsAreNotSatisfiedBy_WithOneNotSatisfied_ReturnsOneNotSatisfied ()
		{
			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<object> (new object (), CreateSpecifications ());
			Assert.AreEqual (1, not.Length);
		}

		[Test]
		[ExpectedException (typeof(SpecificationNotSatisfiedException))]
		public void ThrowIfAnySpecificationIsNotSatisfiedBy_WithSecondNotSatisfied_ThrowsExceptionWithSecondSpecification ()
		{
			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy<object> (new object (), CreateSpecifications ());
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedByAny_OneNotSatisfiedBy_Throw ()
		{
			var spec1 = MockRepository.GenerateMock<ISpecification<int>> ();
			spec1.Expect (s => s.IsSatisfiedBy (1)).Return (true);
			spec1.Expect (s => s.IsSatisfiedBy (2)).Return (true);
			spec1.Expect (s => s.IsSatisfiedBy (3)).Return (true);

			var spec2 = MockRepository.GenerateMock<ISpecification<int>> ();
			spec2.Expect (s => s.IsSatisfiedBy (2)).Return (false);
			spec2.Expect (s => s.NotSatisfiedReason).Return ("2 é inválido");

			var spec3 = MockRepository.GenerateMock<ISpecification<int>> ();
			spec3.Expect (s => s.IsSatisfiedBy (3)).Return (false);

			ExceptionAssert.IsThrowing (new SpecificationNotSatisfiedException ("2 é inválido"), () =>
			{
				SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny (new int[] { 1, 2, 3 }, spec1, spec2, spec3);
			});
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedByAny_AllSatisfiedBy_NotThrow ()
		{
			var spec1 = MockRepository.GenerateMock<ISpecification<int>> ();
			spec1.Expect (s => s.IsSatisfiedBy (1)).Return (true);
			spec1.Expect (s => s.IsSatisfiedBy (2)).Return (true);
			spec1.Expect (s => s.IsSatisfiedBy (3)).Return (true);

			var spec2 = MockRepository.GenerateMock<ISpecification<int>> ();
			spec2.Expect (s => s.IsSatisfiedBy (1)).Return (true);
			spec2.Expect (s => s.IsSatisfiedBy (2)).Return (true);
			spec2.Expect (s => s.IsSatisfiedBy (3)).Return (true);

			var spec3 = MockRepository.GenerateMock<ISpecification<int>> ();
			spec3.Expect (s => s.IsSatisfiedBy (1)).Return (true);
			spec3.Expect (s => s.IsSatisfiedBy (2)).Return (true);
			spec3.Expect (s => s.IsSatisfiedBy (3)).Return (true);

			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny (new int[] { 1, 2, 3 }, spec1, spec2, spec3);
		}

		[Test]
		public void FilterSpecificationsAreNotSatisfiedBy_UseGroup_OneNotSatisfied ()
		{
			var Customer = new Customer (){ Age = 10,  Name = "Test name" };

			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<Customer> (Customer, "Sell");

			Assert.AreEqual (1, not.Length);

			not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<Customer> (Customer, SampleSpecificationGroup.Save);

			Assert.AreEqual (1, not.Length);
		}

		[Test]
		public void FilterSpecificationsAreNotSatisfiedBy_UseGroup_AllSatisfied ()
		{
			var Customer = new Customer (){ Age = 18, Name = "Test name" };

			var not = SpecificationService.FilterSpecificationsAreNotSatisfiedBy<Customer> (Customer, "Sell");

			Assert.AreEqual (0, not.Length);
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedBy_UseGroup_ThrowsException ()
		{
			var customer = new Customer (){ Age = 15, Name = "Test name" };

			ExceptionAssert.IsThrowing (new SpecificationNotSatisfiedException ("Customer must be at least 18 yers old."), () =>
			{
				SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy (customer, "Sell");
			});
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedBy_UseGroup_AllSatisfied ()
		{
			var customer = new Customer (){ Age = 18, Name = "Test name" };

			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy (customer, "Sell");
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedByAny_UseGroup_ThrowsException ()
		{
			var customers = new List<Customer> () {
				new Customer (){ Age = 18, Name = "Test name1" },
				new Customer (){ Age = 11, Name = "Test name2" },
				new Customer (){ Age = 20, Name = "Test name3" }
			};

			ExceptionAssert.IsThrowing (new SpecificationNotSatisfiedException ("Customer must be at least 18 yers old."), () =>
			{
				SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny (customers, "Sell");
			});
		}

		[Test]
		public void ThrowIfAnySpecificationIsNotSatisfiedByAny_UseGroup_AllSatisfied ()
		{
			var customers = new List<Customer> () {
				new Customer (){ Age = 18, Name = "Test name1" },
				new Customer (){ Age = 19, Name = "Test name2" },
				new Customer (){ Age = 20, Name = "Test name3" }
			};

			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny (customers, "Sell");
		}

		#endregion

		#region Helpers

		private ISpecification<object>[] CreateSpecifications ()
		{
			var spec1 = MockRepository.GenerateMock<ISpecification<object>> ();
			spec1.Expect (s => s.IsSatisfiedBy (null)).IgnoreArguments ().Return (true);
			spec1.Expect (s => s.NotSatisfiedReason).Return ("Reason 1");

			var spec2 = MockRepository.GenerateMock<ISpecification<object>> ();
			spec2.Expect (s => s.IsSatisfiedBy (null)).IgnoreArguments ().Return (false);
			spec2.Expect (s => s.NotSatisfiedReason).Return ("Reason 2");


			var specs = new List<ISpecification<object>> ();
			specs.Add (spec1);
			specs.Add (spec2);

			return specs.ToArray ();
		}

		#endregion
	}
}
