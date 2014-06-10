using System;

namespace KissSpecifications.Samples.Domain
{
	[SpecificationGroups (SampleSpecificationGroup.Save, "Sell")]
	public class CustomerAgeSpecification : ISpecification<Customer>
	{
		public bool IsSatisfiedBy (Customer target)
		{
			var satisfied = true;

			if (target.Age < 18)
			{
				satisfied = false;
			}

			return satisfied;
		}

		public string NotSatisfiedReason
		{
			get
			{
				return "Customer must be at least 18 yers old.";
			}
		}
	}
}

