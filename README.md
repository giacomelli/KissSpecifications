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

Features
========
- ISpecification : interface to define any kind of specification
- SpecificationBase<TTarget> : base class to easy create new specifications
- SpecificationService : Service class with features to validate specifications
- SpecificationGroups : attribute to define groups for a specification
- Commons specifications
	- MustHaveNullOrDefaultPropertySpecification 
	- MustNotBeNullSpecification 
	- MustNotHaveEmptyPropertyTextSpecification
	- MustNotHaveNullOrDefaultPropertySpecification
- Globalization
	- IGlobalizationResolver interface to define any kind of globalization for commons specifications  

Using
========
* Create your specification classes:

```csharp

public class CustomerMustHaveValidNameSpecification : SpecificationBase<string>
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

Create Groups of specification
========

Defining groups
--------
You can use any object to declare a group key for your specification, but is always better to define constants or enumerators.

```csharp

/// <summary>
/// Just a enum to define my specification groups.
/// </summary>
public enum SampleSpecificationGroup
{
	Save = 0,
	SendEmail = 1,
}


[SpecificationGroups (SampleSpecificationGroup.Save, "Sell")]
public class CustomerMustHaveNameSpecification : ISpecification<Customer>
{
	...
}

[SpecificationGroups (1, 5)]
public class CustomerMustHaveValidAccountSpecification : ISpecification<Customer>
{
	...
}

[SpecificationGroups (SampleSpecificationGroup.SendEmail)]
public class CustomerMustHaveEmailSpecification : ISpecification<Customer>
{
	...
}

[SpecificationGroups (Constants.AConstantValue, "Sell", 1)]
public class CustomerCanNotHaveDebitSpecification : ISpecification<Customer>
{
	...
}

```

Validate
--------

* Call the SpecificationService on your Domain method defining groups that you want to validate:

```csharp

public static class CustomerService
{

	public static void CreateCustomer(Customer customer)
	{
		SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy(customer, SampleSpecificationGroup.Save);

		// TODO: Logic to create customer...
	}
}

```

Best practices
========
Naming
--------
Let everyone know exactly what you are trying to specify! Create meaningful names for your specification classes.
It will keep your code readable and easy to maintain.

```csharp

public static class CustomerService
{

	public static void CreateCustomer(Customer customer)
	{
		SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy(
			customer, 
			new CustomerMustHaveNameSpecification(),
			new CustomerMustHaveValidAccountSpecification(),
			new CustomerMustHaveEmailSpecification(),
			new CustomerCanNotHaveDebitSpecification());

		// TODO: Logic to create customer...
	}
}

```

License
======

Licensed under the The MIT License (MIT).
In others words, you can use this library for developement any kind of software: open source, commercial, proprietary and alien.


Change Log
======
 - 1.1.7 Added MustHaveUniqueTextSpecification.
 - 1.1.6 Added specification groups feature.
 - 1.1.5 Added MustNotBeNullSpecification.
 - 1.1.4 
	* KissSpecificationsConfig
	* IGlobalizationProvider
	* Commons specifications
		* MustHaveNullOrDefaultPropertySpecification
		* MustNotHaveEmptyPropertyTextSpecification
		* MustNotHaveNullOrDefaultPropertySpecification
 - 1.1.0 Added SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny.
 - 1.0.0 First version.