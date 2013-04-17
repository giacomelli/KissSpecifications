using System;
using System.Globalization;

namespace KissSpecifications.Samples.Domain
{
	public class CustomerNameSpecification : SpecificationBase<string>
	{
		#region Constants
		public const int MinCustomerName = 5;
		public const int MaxCustomerName = 30;
		#endregion

		#region Methods
		public override bool IsSatisfiedBy(string target)
		{
			if (String.IsNullOrWhiteSpace(target))
			{
				NotSatisfiedReason = "Customer name should be specified.";
				return false;
			}

			if (target.Length < MinCustomerName)
			{
				NotSatisfiedReason = String.Format(CultureInfo.CurrentUICulture, "The minimum length for customer name is {0} chars.", MinCustomerName);
				return false;
			}

			if (target.Length > MaxCustomerName)
			{
				NotSatisfiedReason = String.Format(CultureInfo.CurrentUICulture, "The maximum length for customer name is {0} chars.", MaxCustomerName);
				return false;
			}

			return true;
		}
		#endregion
	}
}
