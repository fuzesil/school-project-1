namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// A custom type that stores two <see cref="int"/> values.<br/>
	/// Useful when an object having an <see cref="int"/> ID is associated with the <see cref="int"/> result of a function, but instead of copying the object itself, its unique identifier (primary key) is sufficient.
	/// </summary>
	public class NumberAndId : System.IEquatable<NumberAndId>
	{
		/// <summary>
		/// Gets or Sets the result of a function related to the <see cref="NumberAndId.Id"/> member.
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Gets or Sets the ID of an object in relation to the <see cref="NumberAndId.Number"/> member.
		/// </summary>
		public int Id { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is NumberAndId other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(NumberAndId other)
		{
			return other is not null
				&& this.Id == other.Id
				&& this.Number == other.Number;
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Id + this.Number;

		/// <inheritdoc/>
		public override string ToString()
		{
			var stringBuilder = new System.Text.StringBuilder("Number_Id : " + System.Environment.NewLine)
				.AppendLine(nameof(this.Number) + " = " + this.Number.ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + " , ")
				.AppendLine(nameof(this.Id) + " = " + this.Id.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			return stringBuilder.ToString();
		}
	}
}
