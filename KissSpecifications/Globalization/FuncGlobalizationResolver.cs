using System;

namespace KissSpecifications.Globalization
{
    /// <summary>
    /// An IGlobalizationResolver's implementation using a function to delegate the globalization.
    /// </summary>
    public class FuncGlobalizationResolver : IGlobalizationResolver
    {
        #region Fields
        /// <summary>
        /// The delegation function for globalization.
        /// </summary>
        private Func<string, string> m_globalize;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FuncGlobalizationResolver"/> class.
        /// </summary>
        /// <param name="globalize">The delegation function for globalization.</param>
        public FuncGlobalizationResolver(Func<string, string> globalize)
        {
            m_globalize = globalize;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get a localized version of the english text.
        /// </summary>
        /// <param name="englishText">The original english text.</param>
        /// <returns>
        /// The localized text.
        /// </returns>
        public string GetText(string englishText)
        {
            return m_globalize(englishText);
        }
        #endregion
    }
}
