namespace EisaAwards.Data
{
    /// <summary>
    /// Entity that represents the EXPERT_GROUPS table.
    /// </summary>
    public class ExpertGroup
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
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the name field for the <see cref="ExpertGroup"/> entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="Member"/> instances.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Member> Members { get; }

        /// <summary>
        /// Gets the collection of <see cref="Product"/> instances.
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ExpertGroup other
                && this.Id == other.Id
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
            return $"( #{this.Id} " + (this.Name ?? $"NO {nameof(this.Name)}!") + " )";
        }
    }
}
