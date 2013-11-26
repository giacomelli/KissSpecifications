using KissSpecifications.Globalization;

namespace KissSpecifications
{
    /// <summary>
    /// KissSpecifications global configuration.
    /// </summary>
    public static class KissSpecificationsConfig
    {
        #region Constructor
        /// <summary>
        /// Initializes static members of the <see cref="KissSpecificationsConfig"/> class.
        /// </summary>
        static KissSpecificationsConfig()
        {
            GlobalizationResolver = new DefaultGlobalizationResolver();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the globalization resolver.
        /// </summary>
        /// <value>
        /// The globalization resolver.
        /// </value>
        public static IGlobalizationResolver GlobalizationResolver { get; set; }
        #endregion
    }
}
