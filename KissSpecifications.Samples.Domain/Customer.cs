namespace KissSpecifications.Samples.Domain
{
	public class Customer
	{
		// TODO: Uncomment the attribute below to see the Specification working with DataAnnotations.
		//[Specification(typeof(CustomerNameSpecification))]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		/// <value>The age.</value>
		public int Age { get; set; }
	}
}
