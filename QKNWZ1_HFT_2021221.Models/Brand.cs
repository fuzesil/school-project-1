using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// The entity class representing the BRANDS table.
	/// </summary>
	public class Brand : System.IEquatable<Brand>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Brand"/> class.
		/// </summary>
		public Brand() => this.Products = new HashSet<Product>();

		/// <summary>Gets or Sets the primary key for the <see cref="Brand"/> entity.</summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Stringable]
		public int Id { get; set; }

		/// <summary>Gets or Sets the name field for the <see cref="Brand"/> entity.</summary>
		[Required]
		[MaxLength(255)]
		[Stringable]
		public string Name { get; set; }

		/// <summary>Gets or Sets the Home Page field for the <see cref="Brand"/> entity.</summary>
		[Stringable]
		public string Homepage { get; set; }

		/// <summary>Gets or Sets the H.O. location field for the <see cref="Brand"/> entity.</summary>
		[Stringable]
		public string Address { get; set; }

		/// <summary>Gets or Sets the foreign key of the <see cref="Brand"/> entity pointing to the <see cref="Models.Country"/> entity.</summary>
		[Stringable]
		public int CountryId { get; set; }

		/// <summary>
		/// Gets the navigation property to the <see cref="Models.Country"/> entity.
		/// </summary>
		[NotMapped]
		public virtual Country Country { get; }

		/// <summary>
		/// Gets the navigation property (collection) to the <see cref="Product"/> entity.
		/// </summary>
		//[System.Text.Json.Serialization.JsonIgnore]
		[NotMapped]
		public virtual ICollection<Product> Products { get; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is Brand other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(Brand other)
		{
			return other is not null
				&& this.Id == other.Id
				&& this.CountryId == other.CountryId
				&& ((this.Homepage is null && other.Homepage is null) || this.Homepage == other.Homepage)
				&& ((this.Address is null && other.Address is null) || this.Address == other.Address)
				&& ((this.Name is null && other.Name is null) || this.Name == other.Name);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Id;

		/// <inheritdoc/>
		public override string ToString() => this.StringableToString();
		//{
		//    return $"( #{this.Id} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
		//        + (this.Homepage ?? $"NO {nameof(this.Homepage)}!") + " | "
		//        + (this.Address ?? $"NO {nameof(this.Address)}!") + ", "
		//        /*+ (this.Country?.Name?.ToUpperInvariant() ?? $" #{this.CountryID} )")*/;
		//}
	}
}
