using System;
using System.ComponentModel.DataAnnotations;

namespace KissSpecifications
{
	/// <summary>
	/// DataAnnotation attribute using a specification.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public sealed class SpecificationAttribute : ValidationAttribute
	{
		#region Constructors
		/// <summary>
		/// Initializes a new  <see cref="SpecificationAttribute"/> instance.
		/// </summary>
		/// <param name="specification">The specification type.</param>
		public SpecificationAttribute(Type specification)
		{
			if (specification == null)
			{
				throw new ArgumentNullException("specification");
			}

			this.Specification = specification;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the specification type.
		/// </summary>
		public Type Specification { get; private set; }
		#endregion

		#region Methods
		/// <summary>
		/// Determines whether the specified value of the object is valid.
		/// </summary>
		/// <param name="value">The value of the object to validate.</param>
		/// <returns>
		/// true if the specified value is valid; otherwise, false.
		/// </returns>
		public override bool IsValid(object value)
		{			
			bool isValid = false;

			if (value != null)
			{
				var specOjb = Activator.CreateInstance(Specification);

				var isSatisfiedByMethod = Specification.GetMethod("IsSatisfiedBy");

				if (isSatisfiedByMethod == null)
				{
					throw new InvalidOperationException("The argument specified on SpecificationAttribute's constructor is not a ISpecification<TTarget>. Only objects that implement a specification can be used.");
				}
				else
				{
					isValid = (bool)isSatisfiedByMethod.Invoke(specOjb, new object[] { value });

					if (!isValid)
					{
						var notSatisfiedReasonProperty = Specification.GetProperty("NotSatisfiedReason");
						ErrorMessage = (string)notSatisfiedReasonProperty.GetValue(specOjb, new object[0]);
					}
				}
			}

			return isValid;
		}

		#endregion
	}
}
