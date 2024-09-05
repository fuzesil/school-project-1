namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// A custom type that holds one <see cref="Models.Brand"/> and one <see cref="Models.Member"/> object.
	/// </summary>
	public class MemberBrand : System.IEquatable<MemberBrand>
	{
		/// <summary>
		/// Gets or Sets the <see cref="Models.Brand"/> member.
		/// </summary>
		public Brand Brand { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="Models.Brand"/> member.
		/// </summary>
		public Member Member { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is MemberBrand other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(MemberBrand other)
		{
			return other is not null
				&& this.Brand.Equals(other.Brand)
				&& this.Member.Equals(other.Member);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Brand.GetHashCode() + this.Member.GetHashCode();

		/// <inheritdoc/>
		public override string ToString()
		{
			var sb = new System.Text.StringBuilder("* Member_Brand : " + System.Environment.NewLine)
				.AppendLine(this.Brand.ToString() + " , ")
				.AppendLine(this.Member.ToString());
			return sb.ToString();
			// return $"[\n BRAND: {{ {this.Brand} }}\n MEMBER: {{ {this.Member} }}\n]";
		}
	}
}
