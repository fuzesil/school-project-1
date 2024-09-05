namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// A custom type that holds a <see cref="Models.Brand"/> object and an associated <see cref="int"/> value, e.g. the result of an aggregate function.
	/// </summary>
	public class BrandAndNumber : System.IEquatable<BrandAndNumber>
	{
		/// <summary>
		/// Gets or Sets the <see cref="Models.Brand"/> object.
		/// </summary>
		[Stringable]
		public Brand Brand { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="int"/> value associated with the <see cref="Models.Brand"/> object.
		/// </summary>
		[Stringable]
		public int Number { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is BrandAndNumber other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(BrandAndNumber other)
		{
			return other is not null
				&& this.Number == other.Number
				&& this.Brand.Equals(other.Brand);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Brand.GetHashCode() + this.Number;

		/// <inheritdoc/>
		public override string ToString() //=> this.StringableToString();
		{
			var stringBuilder = new System.Text.StringBuilder("* Brand_And_Number :" + System.Environment.NewLine)
				.AppendLine(this.Number.ToString(System.Globalization.NumberFormatInfo.InvariantInfo))
				.AppendLine(this.Brand.ToString());
			return stringBuilder.ToString();
		}
	}
}
