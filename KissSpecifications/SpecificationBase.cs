namespace KissSpecifications
{
	/// <summary>
	/// A base class for specifications.
	/// </summary>
	/// <typeparam name="TTarget">The type of target object that specification can verify.</typeparam>
	public abstract class SpecificationBase<TTarget> : ISpecification<TTarget>
	{
		/// <summary>
		/// Gets the not satisfied reason.
		/// </summary>
		public string NotSatisfiedReason { get; protected set; }


		/// <summary>
		/// Determines whether the target object satisfiy the specification.
		/// </summary>
		/// <param name="target">The target object to be validated.</param>
		/// <returns>
		///   <c>true</c> if target object satisfy the specification; otherwise, <c>false</c>.
		/// </returns>
		public abstract bool IsSatisfiedBy(TTarget target);
	}
}
