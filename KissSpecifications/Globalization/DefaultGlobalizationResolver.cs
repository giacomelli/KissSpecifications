namespace KissSpecifications.Globalization
{
    /// <summary>
    /// The default IGlobalizationResolver.
    /// <remarks>
    /// This resolver does not perform any globalization.
    /// </remarks>
    /// </summary>
    public class DefaultGlobalizationResolver : IGlobalizationResolver
    {
        /// <summary>
        /// Get a localized version of the english text.
        /// </summary>
        /// <param name="englishText">The original english text.</param>
        /// <returns>
        /// The localized text.
        /// </returns>
        public string GetText(string englishText)
        {
            return englishText;   
        }
    }
}
