using System;
using System.Runtime.Serialization;

namespace KissSpecifications
{
	/// <summary>
	/// Exception raised when a specification is not satisfied.
	/// </summary>
	[Serializable]
	public class SpecificationNotSatisfiedException : Exception
	{
		#region Constructors
		/// <summary>
		/// Initializes a new <see cref="SpecificationNotSatisfiedException"/> instance.
		/// </summary>
		public SpecificationNotSatisfiedException()
		{
		}

		/// <summary>
		/// Initializes a new <see cref="SpecificationNotSatisfiedException"/> instance.
		/// </summary>
		/// <param name="notSatisfiedReason">The not satisfied reason.</param>
		public SpecificationNotSatisfiedException(string notSatisfiedReason)
			: base(notSatisfiedReason)
		{
		}

		/// <summary>
		/// Initializes a new <see cref="SpecificationNotSatisfiedException"/> instance.
		/// </summary>
		/// <param name="notSatisfiedReason">The not satisfied reason.</param>
		/// <param name="innerException">The inner exception.</param>
		public SpecificationNotSatisfiedException(string notSatisfiedReason, Exception innerException)
			: base(notSatisfiedReason, innerException)
		{
		}

		/// <summary>
		/// Initializes a new <see cref="SpecificationNotSatisfiedException"/> instance.
		/// </summary>
		/// <param name="serializationInfo">The serialization info.</param>
		/// <param name="streamingContext">The streaming context.</param>
		protected SpecificationNotSatisfiedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{

		}
		#endregion
	}
}
