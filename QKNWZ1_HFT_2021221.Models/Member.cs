using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// Entity class representing the MEMBERS table.
	/// </summary>
	public class Member : System.IEquatable<Member>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Member"/> class.
		/// </summary>
		public Member()
		{
		}

		/// <summary>
		/// Gets or Sets the primary key for the <see cref="Member"/> entity.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Stringable]
		public int Id { get; set; }

		/// <summary>
		/// Gets or Sets the name field for the <see cref="Member"/> entity.
		/// </summary>
		[Required]
		[Stringable]
		public string Name { get; set; }

		/// <summary>
		/// Gets or Sets the website field for the <see cref="Member"/> entity.
		/// </summary>
		[Stringable]
		public string Website { get; set; }

		/// <summary>
		/// Gets or Sets the Editor-in-Chief field for the <see cref="Member"/> entity.
		/// </summary>
		[Stringable]
		public string ChiefEditor { get; set; }

		/// <summary>
		/// Gets or Sets the phone number field for the <see cref="Member"/> entity.
		/// </summary>
		[Stringable]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or Sets the office location field for the <see cref="Member"/> entity.
		/// </summary>
		[Stringable]
		public string OfficeLocation { get; set; }

		/// <summary>
		/// Gets or Sets the publisher field for the <see cref="Member"/> entity.
		/// </summary>
		[Stringable]
		public string Publisher { get; set; }

		/// <summary>
		/// Gets or Sets the foreign key pointing to the <see cref="Models.ExpertGroup"/> entity.
		/// </summary>
		[Stringable]
		public int ExpertGroupId { get; set; }

		/// <summary>
		/// Gets or Sets the foreign key pointing to the <see cref="Models.Country"/> entity.
		/// </summary>
		[Stringable]
		public int CountryId { get; set; }

		/// <summary>
		/// Gets the navigation property to the <see cref="Models.ExpertGroup"/> entity.
		/// </summary>
		[NotMapped]
		[System.Text.Json.Serialization.JsonIgnore]
		public virtual ExpertGroup ExpertGroup { get; }

		/// <summary>
		/// Gets the navigation property to the <see cref="Models.Country"/> entity.
		/// </summary>
		[NotMapped]
		public virtual Country Country { get; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is Member other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(Member other)
		{
			return other is not null
				&& this.Id == other.Id
				&& this.CountryId == other.CountryId
				&& this.ExpertGroupId == other.ExpertGroupId
				&& ((this.OfficeLocation is null && other.OfficeLocation is null) || this.OfficeLocation == other.OfficeLocation)
				&& ((this.ChiefEditor is null && other.ChiefEditor is null) || this.ChiefEditor == other.ChiefEditor)
				&& ((this.PhoneNumber is null && other.PhoneNumber is null) || this.PhoneNumber == other.PhoneNumber)
				&& ((this.Publisher is null && other.Publisher is null) || this.Publisher == other.Publisher)
				&& ((this.Website is null && other.Website is null) || this.Website == other.Website)
				&& ((this.Name is null && other.Name is null) || this.Name == other.Name);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => this.Id;

		/// <inheritdoc/>
		public override string ToString() => this.StringableToString();
		//{
		//    return $"( #{this.Id} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
		//        //+ (this.ExpertGroup?.Name ?? $"EG: #{this.ExpertGroupID}") + " | "
		//        + (this.Publisher ?? $"NO {nameof(this.Publisher)}!") + " | "
		//        + (this.Website ?? $"NO {nameof(this.Website)}!") + " | "
		//        //+ (this.Country?.CallingCode != null ? $"+{this.Country.CallingCode}" : " --")
		//        + (this.PhoneNumber ?? $"NO {nameof(this.PhoneNumber)}!") + " | "
		//        + (this.ChiefEditor ?? $"NO {nameof(this.ChiefEditor)}!") + " | "
		//        + (this.OfficeLocation ?? $"NO {nameof(this.OfficeLocation)}!") + " )";
		//        //+ (this.Country?.Name?.ToUpperInvariant() ?? $" #{this.CountryID}") + " )";
		//}

	}
}
