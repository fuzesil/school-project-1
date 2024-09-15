namespace EisaAwards.Data
{
    /// <summary>
    /// The entity class representing the COUNTRIES table.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        public Country()
        {
            this.Brands = new System.Collections.Generic.HashSet<Brand>();
            this.Members = new System.Collections.Generic.HashSet<Member>();
        }

        /// <summary>
        /// Gets or Sets the primary key for the Country table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Name field for the Country table.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the capital city field for the Country table.
        /// </summary>
        public string CapitalCity { get; set; }

        /// <summary>
        /// Gets or Sets the calling code of a country.
        /// </summary>
        public int CallingCode { get; set; }

        /// <summary>
        /// Gets or Sets the PPP per person for the country.
        /// </summary>
        public int PPPperCapita { get; set; }

        /// <summary>
        /// Gets the generic collection type navigational property for the Manufacturer entity.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Brand> Brands { get; }

        /// <summary>
        /// Gets the generic collection type navigational property for the Member entity.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Member> Members { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Country other
                && this.Id == other.Id
                && this.CallingCode == other.CallingCode
                && this.PPPperCapita == other.PPPperCapita
                && ((this.Name is null && other.Name is null) || this.Name == other.Name)
                && ((this.CapitalCity is null && other.CapitalCity is null) || this.CapitalCity == other.CapitalCity);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"( #{this.Id} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
                + (this.CapitalCity ?? $"NO {nameof(this.CapitalCity)}!") + $" | {this.CallingCode} | "
                + this.PPPperCapita.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " )";
        }
    }
}
