namespace KissSpecifications
{
    /// <summary>
    /// Defines a interface for a basic specification, based on Specification Pattern: http://en.wikipedia.org/wiki/Specification_pattern.    
    /// </summary>
    /// <typeparam name="TTarget">The type of target object that specification can verify.</typeparam>
    public interface ISpecification<TTarget>
    {
        #region Properties
        /// <summary>
        /// Gets the not satisfied reason.
        /// </summary>
        string NotSatisfiedReason { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns>
        ///   <c>true</c> if target object satisfies the specification; otherwise, <c>false</c>.
        /// </returns>
        bool IsSatisfiedBy(TTarget target);
        #endregion
    }
}
