using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace KissSpecifications
{
	/// <summary>
	/// The infrastructure service for Specifications.
	/// </summary>
	public static class SpecificationService
	{
		#region Fields

		/// <summary>
		/// Stores the already localized specification types, in the client assemby, for a specific type.
		/// </summary>
		private static Dictionary<Type, Type[]>	cachedSpecificationTypes = new Dictionary<Type, Type[]> ();

		#endregion

		#region Methods

		/// <summary>
		/// Filter the specified specifications considering only that are not satisfied by target object.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="target">The target object to validate.</param>
		/// <param name="specifications">The specifications to filter.</param>
		/// <returns>The specifications filtered.</returns>
		public static ISpecification<TTarget>[] FilterSpecificationsAreNotSatisfiedBy<TTarget> (TTarget target, params ISpecification<TTarget>[] specifications)
		{
			var filteredSpecifications = new List<ISpecification<TTarget>> ();

			foreach (var spec in specifications)
			{
				if (target == null || !spec.IsSatisfiedBy (target))
				{
					filteredSpecifications.Add (spec);
				}
			}

			return filteredSpecifications.ToArray ();
		}

		/// <summary>
		/// Filter the specified groups of specification, considering only the specifications that are not satisfied by target object.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="target">The target object to validate.</param>
		/// <param name="groupKeys">Keys to the specification groups where the target must be validated.</param>
		/// <returns>The specifications where the target are not satisfied.</returns>
		public static ISpecification<TTarget>[] FilterSpecificationsAreNotSatisfiedBy<TTarget> (TTarget target, params Object[] groupKeys)
		{
			var specificationsToVerify = CreateSpecificationsFor<TTarget> (groupKeys);

			return FilterSpecificationsAreNotSatisfiedBy<TTarget> (target, specificationsToVerify.ToArray ());
		}

		/// <summary>
		/// If any specification was not satisfied by the target object specified, a SpecificationNotSatisfiedException 
		/// will be throw with NotSatisfiedReason for the first not satisfied specification.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="target">The target object to validate.</param>
		/// <param name="specifications">The specifications to validate.</param>
		public static void ThrowIfAnySpecificationIsNotSatisfiedBy<TTarget> (TTarget target, params ISpecification<TTarget>[] specifications)
		{
			foreach (var spec in specifications)
			{
				if (!spec.IsSatisfiedBy (target))
				{
					throw new SpecificationNotSatisfiedException (spec.NotSatisfiedReason);
				}
			}
		}

		/// <summary>
		/// If any specification, of the given groups, was not satisfied by the target object specified, 
		/// a SpecificationNotSatisfiedException will be throw with NotSatisfiedReason for the first not 
		/// satisfied specification.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="target">The target object to validate.</param>
		/// <param name="groupKeys">Keys to the specification groups where the target must be validated.</param>
		public static void ThrowIfAnySpecificationIsNotSatisfiedBy<TTarget> (TTarget target, params Object[] groupKeys)
		{
			var specificationsToVerify = CreateSpecificationsFor<TTarget> (groupKeys);

			ThrowIfAnySpecificationIsNotSatisfiedBy<TTarget> (target, specificationsToVerify);
		}

		/// <summary>
		/// If any specification was not satisfied by the any of target objects specified, a SpecificationNotSatisfiedException 
		/// will be throw with NotSatisfiedReason for the first not satisfied specification.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="targets">The target objects to validate.</param>
		/// <param name="specifications">The specifications to validate.</param>
		public static void ThrowIfAnySpecificationIsNotSatisfiedByAny<TTarget> (IEnumerable<TTarget> targets, params ISpecification<TTarget>[] specifications)
		{
			foreach (var target in targets)
			{
				ThrowIfAnySpecificationIsNotSatisfiedBy (target, specifications);
			}
		}

		/// <summary>
		/// If any specification, of the given groups, was not satisfied by the any of target objects
		/// specified, a SpecificationNotSatisfiedException will be throw with NotSatisfiedReason for
		/// the first not satisfied specification.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="targets">The target objects to validate.</param>
		/// <param name="groupKeys">Keys to the specification groups where the targets must be validated.</param>
		public static void ThrowIfAnySpecificationIsNotSatisfiedByAny<TTarget> (IEnumerable<TTarget> targets, params Object[] groupKeys)
		{
			var specificationsToVerify = CreateSpecificationsFor<TTarget> (groupKeys);

			foreach (var target in targets)
			{
				ThrowIfAnySpecificationIsNotSatisfiedBy (target, specificationsToVerify);
			}
		}

		#endregion

		#region Helper

		/// <summary>
		/// Locate and create a set of specifications for a target, considering only
		/// specifications owned by one of the specified groups.
		/// </summary>
		/// <typeparam name="TTarget">The type of object to be validate.</typeparam>
		/// <param name="groupKeys">Keys to the specification groups.</param>
		/// <returns>The specifications for a target owned by at leaste one specified group.</returns>
		private static ISpecification<TTarget>[] CreateSpecificationsFor<TTarget> (params Object[] groupKeys)
		{
			var specifications = new List<ISpecification<TTarget>> ();
			var specificationTypes = FindSpecificatiosTypeFor<TTarget> ();

			foreach (var type in specificationTypes)
			{
				var specificationGroupsAttribute = (SpecificationGroupsAttribute)type
					.GetCustomAttributes (typeof(SpecificationGroupsAttribute), false)
					.FirstOrDefault ();

				if (specificationGroupsAttribute != null && specificationGroupsAttribute.GroupKeys.Any (gk => groupKeys.Any (g => g.Equals (gk))))
				{
					specifications.Add ((ISpecification<TTarget>)Activator.CreateInstance (type));
				}
			}

			return specifications.ToArray ();
		}

		/// <summary>
		/// Finds the implemented specificatios types, looking for a specific target.
		/// </summary>
		/// <typeparam name="TTarget">The target type in the specifications.</typeparam>
		/// <returns>The specificatio types for a specific target.</returns>
		private static Type[] FindSpecificatiosTypeFor<TTarget> ()
		{
			var targetType = typeof(TTarget);

			lock (cachedSpecificationTypes)
			{

				if (!cachedSpecificationTypes.ContainsKey (targetType))
				{
					var specificationType = typeof(ISpecification<TTarget>);

					cachedSpecificationTypes [targetType] = targetType.Assembly.GetTypes ()
					.Where (t => specificationType.IsAssignableFrom (t) && t.IsClass && !t.IsInterface)
					.ToArray ();
				}

			}

			return cachedSpecificationTypes [targetType];
		}

		#endregion
	}
}
