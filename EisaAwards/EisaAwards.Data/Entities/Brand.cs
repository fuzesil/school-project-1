namespace EisaAwards.Data
{
    /// <summary>
    /// The entity class representing the Manufacturers table.
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Brand"/> class.
        /// </summary>
        public Brand()
        {
            this.Products = new System.Collections.Generic.HashSet<Product>();
        }

        /// <summary>
        /// Gets or Sets the primary key for the <see cref="Brand"/> entity.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="Brand"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Home Page field for the <see cref="Brand"/> entity.
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or Sets the H.O. location field for the <see cref="Brand"/> entity.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets the foreign key for the <see cref="Brand"/> entity pointing to the <see cref="Data.Country"/> entity.
        /// </summary>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Data.Country"/> entity.
        /// </summary>
        public virtual Country Country { get; }

        /// <summary>
        /// Gets the navigation property (collection) to the <see cref="Product"/> entity.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Brand other)
            {
                return this.BrandId == other.BrandId
                    && this.CountryID == other.CountryID
                    && ((this.Homepage is null && other.Homepage is null) || this.Homepage == other.Homepage)
                    && ((this.Address is null && other.Address is null) || this.Address == other.Address)
                    && ((this.Name is null && other.Name is null) || this.Name == other.Name);
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.BrandId;
        }

        /// <summary>
        /// Returns a custom string of the properties of the current <see cref="Brand"/> object.
        /// </summary>
        /// <returns>The custom string that represents the current object.</returns>
        public override string ToString()
        {
            return $"( #{this.BrandId} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " | "
                + (this.Homepage ?? $"NO {nameof(this.Homepage)}!") + " | "
                + (this.Address ?? $"NO {nameof(this.Address)}!") + ", "
                + (this.Country?.Name?.ToUpperInvariant() ?? $" #{this.CountryID} )");
        }
    }
}
