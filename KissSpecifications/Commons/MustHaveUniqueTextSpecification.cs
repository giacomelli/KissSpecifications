using System;
using System.Diagnostics.CodeAnalysis;
using HelperSharp;

namespace KissSpecifications.Commons
{
    /// <summary>
    ///  Target must have a unique value for the specified property.
    /// </summary>
    /// <typeparam name="TTarget">The target.</typeparam>
    public class MustHaveUniqueTextSpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants
        /// <summary>
        /// The default text for not satisfied reason.
        /// </summary>
        public const string NotSatisfiedReasonText = "There is already a {0} with the name '{1}'";
        #endregion

        #region Fields
        private Func<TTarget, string> m_getProperty;
        private Func<string, TTarget> m_getByName;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MustHaveUniqueTextSpecification{TTarget}"/> class.
        /// </summary>
        /// <param name="getProperty">The text property that must have a unique value.</param>
        /// <param name="getByName">The function used to find other target with the same text property value.</param>
        public MustHaveUniqueTextSpecification(Func<TTarget, string> getProperty, Func<string, TTarget> getByName)
        {
            m_getProperty = getProperty;
            m_getByName = getByName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns>
        ///   <c>true</c> if target object satisfies the specification; otherwise, <c>false</c>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public override bool IsSatisfiedBy(TTarget target)
        {
            var entityType = typeof(TTarget);
            var entityName = typeof(TTarget).Name;
            var name = m_getProperty(target);

            var otherEntityWithSameName = m_getByName(name);

            if (otherEntityWithSameName != null && !otherEntityWithSameName.Equals(target))
            {
                var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;

                NotSatisfiedReason = globalizationResolver
                    .GetText(NotSatisfiedReasonText)
                    .With(globalizationResolver.GetText(typeof(TTarget).Name).ToLowerInvariant(), name);

                return false;
            }

            return true;
        }
        #endregion
    }
}