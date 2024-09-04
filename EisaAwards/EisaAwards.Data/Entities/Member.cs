namespace EisaAwards.Data
{
    /// <summary>
    /// Entity class representing the Members table.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Gets or Sets the primary key for the <see cref="Member"/> entity.
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="Member"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the website field for the <see cref="Member"/> entity.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or Sets the Editor-in-Chief field for the <see cref="Member"/> entity.
        /// </summary>
        public string ChiefEditor { get; set; }

        /// <summary>
        /// Gets or Sets the phone number field for the <see cref="Member"/> entity.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets the office location field for the <see cref="Member"/> entity.
        /// </summary>
        public string OfficeLocation { get; set; }

        /// <summary>
        /// Gets or Sets the publisher field for the <see cref="Member"/> entity.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public int ExpertGroupID { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.Country"/> entity.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public virtual ExpertGroup ExpertGroup { get; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.Country"/> entity.
        /// </summary>
        public virtual Country Country { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Member other)
            {
                return this.MemberID == other.MemberID
                    && this.CountryID == other.CountryID
                    && this.ExpertGroupID == other.ExpertGroupID
                    && ((this.OfficeLocation is null && other.OfficeLocation is null) || this.OfficeLocation == other.OfficeLocation)
                    && ((this.ChiefEditor is null && other.ChiefEditor is null) || this.ChiefEditor == other.ChiefEditor)
                    && ((this.PhoneNumber is null && other.PhoneNumber is null) || this.PhoneNumber == other.PhoneNumber)
                    && ((this.Publisher is null && other.Publisher is null) || this.Publisher == other.Publisher)
                    && ((this.Website is null && other.Website is null) || this.Website == other.Website)
                    && ((this.Name is null && other.Name is null) || this.Name == other.Name);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.MemberID;
        }

        /// <summary>
        /// Returns a custom string of the properties of the current <see cref="Member"/> object.
        /// </summary>
        /// <returns>The custom string that represents the current object.</returns>
        public override string ToString()
        {
            return $"( #{this.MemberID} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
                + (this.ExpertGroup?.Name ?? $"EG: #{this.ExpertGroupID}") + " | "
                + (this.Publisher ?? $"NO {nameof(this.Publisher)}!") + " | "
                + (this.Website ?? $"NO {nameof(this.Website)}!") + " | "
                + (this.Country?.CallingCode != null ? $"+{this.Country.CallingCode}" : " --")
                + (this.PhoneNumber ?? $"NO {nameof(this.PhoneNumber)}!") + " | "
                + (this.ChiefEditor ?? $"NO {nameof(this.ChiefEditor)}!") + " | "
                + (this.OfficeLocation ?? $"NO {nameof(this.OfficeLocation)}!") + ", "
                + (this.Country?.Name?.ToUpperInvariant() ?? $" #{this.CountryID}") + " )";
        }
    }
}
