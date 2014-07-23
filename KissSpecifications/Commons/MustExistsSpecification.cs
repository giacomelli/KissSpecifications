using System;
using System.Linq.Expressions;
using HelperSharp;

namespace KissSpecifications.Commons
{
    /// <summary>
    ///  The target's property must exists.
    /// </summary>
    /// <typeparam name="TTarget">The target.</typeparam>
    /// <typeparam name="TProperty">The target's property.</typeparam>
    public class MustExistsSpecification<TTarget, TProperty> : SpecificationBase<TTarget>
    {
        #region Constants
        /// <summary>
        /// The default text for not satisfied reason.
        /// </summary>
        public const string NotSatisfiedReasonText = "The '{0}' with value '{1}' not exists.";
        #endregion

        #region Fields
        private Expression<Func<TTarget, object>> m_property;
        private Expression<Func<TProperty, bool>> m_exists;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MustExistsSpecification`2"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="exists">The function to verify if property value exists.</param>
        public MustExistsSpecification(Expression<Func<TTarget, object>> property, Expression<Func<TProperty, bool>> exists)
        {
            m_property = property;
            m_exists = exists;
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
        public override bool IsSatisfiedBy(TTarget target)
        {
            var propertyValue = m_property.Compile()(target);

            if (!m_exists.Compile()((TProperty)propertyValue))
            {
                var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;
                var memberExpression = ExpressionHelper.GetMemberExpression(m_property);

                NotSatisfiedReason = globalizationResolver
                    .GetText(NotSatisfiedReasonText)
                    .With(globalizationResolver.GetText(memberExpression.Member.Name), propertyValue);

                return false;
            }

            return true;
        }
        #endregion
    }
}
