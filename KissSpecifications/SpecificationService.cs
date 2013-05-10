using System.Collections.Generic;

namespace KissSpecifications
{
	/// <summary>
	/// The infrastructure service for Specifications.
	/// </summary>
	public static class SpecificationService
	{
		#region Methods
		/// <summary>
		/// Filter the specified specifications considering only that are safisfied by target object.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="target">The target object to validate.</param>
		/// <param name="specifications">The specifications to filter.</param>
		/// <returns>The specifications filtered.</returns>
		public static ISpecification<TTarget>[] FilterSpecificationsAreNotSatisfiedBy<TTarget>(TTarget target, params ISpecification<TTarget>[] specifications)
		{
			var filteredSpecifications = new List<ISpecification<TTarget>>();

			foreach (var spec in specifications)
			{
				if (target == null || !spec.IsSatisfiedBy(target))
				{
					filteredSpecifications.Add(spec);
				}
			}

			return filteredSpecifications.ToArray();
		}

		/// <summary>
		/// If any specification was not satisfied by the target object specified, a SpecificationNotSatisfiedException 
		/// will be raide with NotSatisfiedReason for the first not satisfied specification.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="target">The target object to validate.</param>
		/// <param name="specifications">The specifications to validate.</param>		
		public static void ThrowIfAnySpecificationIsNotSatisfiedBy<TTarget>(TTarget target, params ISpecification<TTarget>[] specifications)
		{
			foreach (var spec in specifications)
			{
				if (!spec.IsSatisfiedBy(target))
				{
					throw new SpecificationNotSatisfiedException(spec.NotSatisfiedReason);
				}
			}
		}

		/// <summary>
		/// If any specification was not satisfied by the any of target objects specified, a SpecificationNotSatisfiedException 
		/// will be raide with NotSatisfiedReason for the first not satisfied specification.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="targets">The target objects to validate.</param>
		/// <param name="specifications">The specifications to validate.</param>	
		public static void ThrowIfAnySpecificationIsNotSatisfiedByAny<TTarget>(IEnumerable<TTarget> targets, params ISpecification<TTarget>[] specifications)
		{
			foreach (var target in targets)
			{
				ThrowIfAnySpecificationIsNotSatisfiedBy(target, specifications);
			}
		}
		#endregion
	}
}
