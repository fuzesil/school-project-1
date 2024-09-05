namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// Custom type that holds one <see cref="Models.ExpertGroup"/> and one <see cref="Models.Product"/> member.
	/// </summary>
	public class ExpertgroupProduct : System.IEquatable<ExpertgroupProduct>
	{
		/// <summary>
		/// Gets or Sets the <see cref="Models.ExpertGroup"/> member of this complex type.
		/// </summary>
		public ExpertGroup ExpertGroup { get; set; }

		/// <summary>
		/// Gets or Sets the <see cref="Models.Product"/> member of this complex type.
		/// </summary>
		public Product Product { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is ExpertgroupProduct other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(ExpertgroupProduct other)
		{
			return other is not null
				&& this.ExpertGroup.Equals(other.ExpertGroup)
				&& this.Product.Equals(other.Product);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.ExpertGroup.GetHashCode() + this.Product.GetHashCode();

		/// <inheritdoc/>
		public override string ToString()
		{
			var sb = new System.Text.StringBuilder("* ExpertGroup_Product :" + System.Environment.NewLine)
				.AppendLine(this.ExpertGroup.ToString() + " , ")
				.AppendLine(this.Product.ToString());
			return sb.ToString();
			// return $"[[ {nameof(this.ExpertGroup)} = {this.ExpertGroup}\n {nameof(this.Product)} = {this.Product} ]]\n";
		}
	}
}
