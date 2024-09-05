using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// Entity class representing the PRODUCTS table.
	/// </summary>
	public class Product : System.IEquatable<Product>
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Product"/> class.
		/// </summary>
		public Product()
		{
		}

		/// <summary>
		/// Gets or Sets the primary key for the <see cref="Product"/> entity.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Stringable]
		public int Id { get; set; }

		/// <summary>
		/// Gets or Sets the name field for the <see cref="Product"/> entity.
		/// </summary>
		[Required]
		[Stringable]
		public string Name { get; set; }

		/// <summary>
		/// Gets or Sets the award category field for the <see cref="Product"/> entity.
		/// </summary>
		[Stringable]
		public string Category { get; set; }

		/// <summary>
		/// Gets or Sets the price field for the <see cref="Product"/> entity.
		/// </summary>
		[Stringable]
		public int Price { get; set; }

		/// <summary>
		/// Gets or Sets the date of launch field for the <see cref="Product"/> entity.
		/// </summary>
		[Stringable]
		public System.DateTime LaunchDate { get; set; }

		/// <summary>
		/// Gets or Sets the estimated lifetime field for the <see cref="Product"/> entity.
		/// </summary>
		[Stringable]
		public int EstimatedLifetime { get; set; }

		/// <summary>
		/// Gets or Sets the foreign key pointing to the <see cref="Models.Brand"/> entity.
		/// </summary>
		[Stringable]
		public int BrandId { get; set; }

		/// <summary>
		/// Gets or Sets the foreign key pointing to the <see cref="Models.ExpertGroup"/> entity.
		/// </summary>
		[Stringable]
		public int ExpertGroupId { get; set; }

		/// <summary>
		/// Gets the navigation property to the <see cref="Models.Brand"/> entity.
		/// </summary>
		[System.Text.Json.Serialization.JsonIgnore]
		[NotMapped]
		public virtual Brand Brand { get; }

		/// <summary>
		/// Gets the navigation property to the <see cref="Models.ExpertGroup"/> entity.
		/// </summary>
		[System.Text.Json.Serialization.JsonIgnore]
		[NotMapped]
		public virtual ExpertGroup ExpertGroup { get; }

		/// <inheritdoc/>
		public bool Equals(Product other)
		{
			return other is not null
				&& this.Id == other.Id
				&& this.BrandId == other.BrandId
				&& this.ExpertGroupId == other.ExpertGroupId
				&& this.Price == other.Price
				&& this.EstimatedLifetime == other.EstimatedLifetime
				&& this.LaunchDate == other.LaunchDate
				&& ((this.Name is null && other.Name is null) || this.Name == other.Name);
		}

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is Product other && this.Equals(other);

		/// <inheritdoc/>
		public override int GetHashCode() => this.Id;

		/// <inheritdoc/>
		public override string ToString() => this.StringableToString();
		//{
		//    return $"( #{this.Id} "
		//        //+ (this.Brand?.Name?.ToUpperInvariant() ?? $" Brand #{this.BrandId}") + " - "
		//        + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
		//        //+ (this.ExpertGroup?.Name ?? $"EG: #{this.ExpertGroupID}") + " | "
		//        + (this.Category ?? $"NO {nameof(this.Category)}!") + " | "
		//        + this.Price.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " | "
		//        + this.EstimatedLifetime + " )";
		//}
	}
}
