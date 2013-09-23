KissSpecifications
==================
[![Build Status](https://travis-ci.org/giacomelli/KissSpecifications.png?branch=master)](https://travis-ci.org/giacomelli/KissSpecifications)

A KISS approach for specification pattern.
http://en.wikipedia.org/wiki/Specification_pattern


Setup
========

NuGet
--------
PM> Install-Package KissSpecifications



Using
========
* Create your specification classes:

```csharp

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

```

```csharp

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

```

* Call the SpecificationService on your Domain method:

```csharp

public static class CustomerService
{

	public static void CreateCustomer(Customer customer)
	{
		SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy(customer, new CustomerCreationSpecification());

		// TODO: Logic to create customer...
	}
}

```
