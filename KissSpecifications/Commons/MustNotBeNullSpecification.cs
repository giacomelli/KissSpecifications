using HelperSharp;

namespace KissSpecifications.Commons
{
    /// <summary>
    /// Target must not be null.
    /// </summary>
    /// <typeparam name="TTarget">The target.</typeparam>
    public class MustNotBeNullSpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants
        /// <summary>
        /// The default text for not satisfied reason.
        /// </summary>
        public const string NotSatisfiedReasonText = "The '{0}' must not be null.";
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns>
        ///   <c>true</c> if target object satisfies the specification; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsSatisfiedBy(TTarget target)
        {
            if (target == null)
            {
                var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;

                NotSatisfiedReason = globalizationResolver.GetText(MustNotBeNullSpecification<TTarget>.NotSatisfiedReasonText)
                        .With(globalizationResolver.GetText(typeof(TTarget).Name));

                return false;
            }

            return true;
        }
        #endregion
    }
}
