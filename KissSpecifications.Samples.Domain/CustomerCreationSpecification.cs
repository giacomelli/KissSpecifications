using System;
using System.Globalization;

namespace KissSpecifications.Samples.Domain
{
	[SpecificationGroups ("Sell")]
	public class CustomerCreationSpecification : SpecificationBase<Customer>
	{
		#region Methods

		public override bool IsSatisfiedBy (Customer target)
		{
			if (target == null)
			{
				NotSatisfiedReason = "Customer should be specified.";
				return false;
			}

			var nameSpec = new CustomerNameSpecification ();

			if (!nameSpec.IsSatisfiedBy (target.Name))
			{
				NotSatisfiedReason = nameSpec.NotSatisfiedReason;
				return false;
			}

			return true;
		}

		#endregion
	}
}
