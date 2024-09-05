namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// A custom type that holds one <see cref="Models.Member"/> and one <see cref="Models.Country"/> object.
	/// </summary>
	public class MemberCountry : System.IEquatable<MemberCountry>
	{
		/// <summary>
		/// Gets or Sets the <see cref="Models.Member"/> type member.
		/// </summary>
		public Member Member { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="Models.Country"/> type member.
		/// </summary>
		public Country Country { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is MemberCountry other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(MemberCountry other)
		{
			return other is not null
				&& this.Country.Equals(other.Country)
				&& this.Member.Equals(other.Member);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Country.GetHashCode() + this.Member.GetHashCode();

		/// <inheritdoc/>
		public override string ToString()
		{
			var sb = new System.Text.StringBuilder("Member_Country : " + System.Environment.NewLine)
				.AppendLine(this.Member.ToString() + " , ")
				.AppendLine(this.Country.ToString());
			return sb.ToString();
			// return $"[[ {nameof(this.Member)} = {this.Member}\n {nameof(this.Country)} = {this.Country} ]]\n";
		}
	}
}
