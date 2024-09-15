namespace EisaAwards.Data
{
    /// <summary>
    /// Entity class representing the PRODUCTS table.
    /// </summary>
    public class Product
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="Product"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the award category field for the <see cref="Product"/> entity.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets the price field for the <see cref="Product"/> entity.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or Sets the date of launch field for the <see cref="Product"/> entity.
        /// </summary>
        public System.DateTime LaunchDate { get; set; }

        /// <summary>
        /// Gets or Sets the estimated lifetime field for the <see cref="Product"/> entity.
        /// </summary>
        public int EstimatedLifetime { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.Country"/> entity.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key pointing to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public int ExpertGroupID { get; set; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.Country"/> entity.
        /// </summary>
        public virtual Brand Brand { get; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.ExpertGroup"/> entity.
        /// </summary>
        public virtual ExpertGroup ExpertGroup { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Product other
                && this.Id == other.Id
                && this.BrandId == other.BrandId
                && this.ExpertGroupID == other.ExpertGroupID
                && this.Price == other.Price
                && this.EstimatedLifetime == other.EstimatedLifetime
                && this.LaunchDate == other.LaunchDate
                && ((this.Name is null && other.Name is null) || this.Name == other.Name);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"( #{this.Id} "
                + (this.Brand?.Name?.ToUpperInvariant() ?? $" Brand #{this.BrandId}") + " - "
                + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
                + (this.ExpertGroup?.Name ?? $"EG: #{this.ExpertGroupID}") + " | "
                + (this.Category ?? $"NO {nameof(this.Category)}!") + " | "
                + this.Price.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " | "
                + this.EstimatedLifetime + " )";
        }
    }
}
