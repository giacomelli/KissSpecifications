using System;
using System.Linq;

namespace KissSpecifications
{
	/// <summary>
	/// Specification groups attribute.
	/// </summary>
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = false)]
	public class SpecificationGroupsAttribute : Attribute
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="KissSpecifications.SpecificationGroupsAttribute"/> class.
		/// </summary>
		public SpecificationGroupsAttribute ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KissSpecifications.SpecificationGroupsAttribute"/> class.
		/// </summary>
		/// <param name="groupKeys">A collection of keys that identify the groups where this specification participates.</param>
		public SpecificationGroupsAttribute (params Object[] groupKeys)
		{
			if (groupKeys == null)
			{
				throw new ArgumentNullException ("groupKeys");
			}

			this.GroupKeys = groupKeys;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the group keys.
		/// </summary>
		/// <value>The group keys.</value>
		public Object[] GroupKeys { get; set; }

		#endregion
	}
}

