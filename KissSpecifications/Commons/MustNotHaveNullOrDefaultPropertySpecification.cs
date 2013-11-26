using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using HelperSharp;
using KissSpecifications;

namespace KissSpecifications.Commons
{  
    /// <summary>
    ///  Target must not have the specified property with a null value or default value.
    /// </summary>
    /// <typeparam name="TTarget">The target.</typeparam>
    public class MustNotHaveNullOrDefaultPropertySpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants
        /// <summary>
        /// The default text for not satisfied reason.
        /// </summary>
        public const string NotSatisfiedReasonText = "The '{0}' property of '{1}' must not have null or default value.";
        #endregion

        #region Fields
        /// <summary>
        /// The properties name.
        /// </summary>
        private string[] m_propertiesName;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MustNotHaveNullOrDefaultPropertySpecification`1"/> class.
        /// </summary>
        /// <param name="propertiesName">The properties name.</param>
        public MustNotHaveNullOrDefaultPropertySpecification(params string[] propertiesName)
        {
            m_propertiesName = propertiesName;
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
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Check if target is null is charge of this specification.")]
        public override bool IsSatisfiedBy(TTarget target)
        {
            foreach (var propertyName in m_propertiesName)
            {
                var propertyValue = target == null ? null : ReflectionHelper.GetPropertyValue(target, propertyName);

                if (MustHaveNullOrDefaultPropertySpecification<TTarget>.IsNullOrDefault(propertyValue))
                {
                    var globalizationResolver = KissSpecificationsConfig.GlobalizationResolver;

                    NotSatisfiedReason = globalizationResolver.GetText(MustNotHaveNullOrDefaultPropertySpecification<TTarget>.NotSatisfiedReasonText)
                        .With(globalizationResolver.GetText(propertyName), globalizationResolver.GetText(typeof(TTarget).Name));

                    return false;
                }
            }

            return true;
        }        
        #endregion
    }
}