namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// A custom type that holds one <see cref="Models.ExpertGroup"/>, one <see cref="Models.Member"/>, and one <see cref="Models.Country"/> object. More complex operations fetch one of each to store all relevant information.
	/// </summary>
	public class ExpertgroupMemberCountry : System.IEquatable<ExpertgroupMemberCountry>
	{
		/// <summary>
		/// Gets or Sets the <see cref="Models.Country"/> member.
		/// </summary>
		public Country Country { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="Models.ExpertGroup"/> member.
		/// </summary>
		public ExpertGroup ExpertGroup { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="Models.Member"/> member.
		/// </summary>
		public Member Member { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is ExpertgroupMemberCountry other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(ExpertgroupMemberCountry other)
		{
			return other is not null
				&& this.Country.Equals(other.Country)
				&& this.ExpertGroup.Equals(other.ExpertGroup)
				&& this.Member.Equals(other.Member);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Country.GetHashCode() + this.ExpertGroup.GetHashCode() + this.Member.GetHashCode();

		/// <inheritdoc/>
		public override string ToString()
		{
			var sb = new System.Text.StringBuilder("* ExpertGroup_Memeber_Country : " + System.Environment.NewLine)
				.AppendLine(this.ExpertGroup.ToString() + " , ")
				.AppendLine(this.Member.ToString() + " , ")
				.AppendLine(this.Country.ToString());
			return sb.ToString();
			// return $"[[ {nameof(this.ExpertGroup)} = {this.ExpertGroup}\n {nameof(this.Member)} = {this.Member} {nameof(this.Country)} = {this.Country} ]]\n";
		}
	}
}
