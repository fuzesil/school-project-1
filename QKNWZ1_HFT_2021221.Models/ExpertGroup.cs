using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// Entity that represents the EXPERT_GROUPS table.
	/// </summary>
	public class ExpertGroup : System.IEquatable<ExpertGroup>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExpertGroup"/> class.
		/// </summary>
		public ExpertGroup()
		{
			this.Members = new System.Collections.Generic.HashSet<Member>();
			this.Products = new System.Collections.Generic.HashSet<Product>();
		}

		/// <summary>
		/// Gets or Sets the primary key for the <see cref="ExpertGroup"/> entity.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Stringable]
		public int Id { get; set; }

		/// <summary>
		/// Gets or Sets the name field for the <see cref="ExpertGroup"/> entity.
		/// </summary>
		[Required]
		[Stringable]
		public string Name { get; set; }

		/// <summary>
		/// Gets the collection of <see cref="Member"/> objects.
		/// </summary>
		//[System.Text.Json.Serialization.JsonIgnore]
		[NotMapped]
		public virtual System.Collections.Generic.ICollection<Member> Members { get; }

		/// <summary>
		/// Gets the collection of <see cref="Product"/> objects.
		/// </summary>
		//[System.Text.Json.Serialization.JsonIgnore]
		[NotMapped]
		public virtual System.Collections.Generic.ICollection<Product> Products { get; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is ExpertGroup other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(ExpertGroup other)
		{
			return other is not null
				&& this.Id == other.Id
				&& ((this.Name is null && other.Name is null) || this.Name == other.Name);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Id;

		/// <inheritdoc/>
		public override string ToString() => this.StringableToString();
		//{
		//    return $"( #{this.Id} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " )";
		//}
	}
}
