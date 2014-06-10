using System;

namespace KissSpecifications.Samples.Domain
{
	public static class CustomerService
	{
		public static void CreateCustomer (Customer customer)
		{
			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy (customer, new CustomerCreationSpecification ());

			// TODO: Logic to create customer...
		}
	}
}
