using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// The entity class representing the COUNTRIES table.
	/// </summary>
	public class Country : System.IEquatable<Country>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Country"/> class.
		/// </summary>
		public Country()
		{
			this.Brands = new HashSet<Brand>();
			this.Members = new HashSet<Member>();
		}

		/// <summary>
		/// Gets or Sets the primary key for the <see cref="Country"/> entity.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Stringable]
		public int Id { get; set; }

		/// <summary>
		/// Gets or Sets the Name field for the <see cref="Country"/> entity.
		/// </summary>
		[Required]
		[Stringable]
		public string Name { get; set; }

		/// <summary>
		/// Gets or Sets the capital city field for the <see cref="Country"/> entity.
		/// </summary>
		[Stringable]
		public string CapitalCity { get; set; }

		/// <summary>
		/// Gets or Sets the calling code field for the <see cref="Country"/> entity.
		/// </summary>
		[Stringable]
		public int CallingCode { get; set; }

		/// <summary>
		/// Gets or Sets the PPP per person for the <see cref="Country"/> entity.
		/// </summary>
		/// <remarks>Perhaps a better indicator would be <see href="https://en.wikipedia.org/wiki/List_of_countries_by_wealth_per_adult">WealthPerAdult</see>.</remarks>
		[Stringable]
		public int PPPperCapita { get; set; }

		/// <summary>
		/// Gets the generic collection type navigational property for the <see cref="Brand"/> entity.
		/// </summary>
		[NotMapped]
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<Brand> Brands { get; }

		/// <summary>
		/// Gets the generic collection type navigational property for the <see cref="Member"/> entity.
		/// </summary>
		[NotMapped]
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ICollection<Member> Members { get; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is Country other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(Country other)
		{
			return other is not null
				&& this.Id == other.Id
				&& this.CallingCode == other.CallingCode
				&& this.PPPperCapita == other.PPPperCapita
				&& ((this.Name is null && other.Name is null) || this.Name == other.Name)
				&& ((this.CapitalCity is null && other.CapitalCity is null) || this.CapitalCity == other.CapitalCity);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Id;

		/// <inheritdoc/>
		public override string ToString() => this.StringableToString();
		//{
		//    return $"( #{this.Id} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
		//        + (this.CapitalCity ?? $"NO {nameof(this.CapitalCity)}!") + $" | {this.CallingCode} | "
		//        + this.PPPperCapita.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " )";
		//}
	}
}
