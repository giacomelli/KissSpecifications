namespace KissSpecifications.Globalization
{
    /// <summary>
    /// Defines a globalization resolver interface.
    /// </summary>
    public interface IGlobalizationResolver
    {
        /// <summary>
        /// Get a localized version of the english text.
        /// </summary>
        /// <param name="englishText">The original english text.</param>
        /// <returns>The localized text.</returns>
        string GetText(string englishText);
    }
}
