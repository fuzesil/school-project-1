namespace EisaAwards.Logic
{
    using System.Linq;

    /// <summary>
    /// A custom type that stores a <see cref="Data.Brand"/> entity type, <see cref="int"/> Count of awards,
    /// and <see cref="System.Collections.Generic.IEnumerable{T}"/> (T = <see cref="string"/>) Awarded product list.
    /// </summary>
    public class BrandWithAwards
    {
        /// <summary>
        /// Gets or Sets the name of a groupped Brand.
        /// </summary>
        public Data.Brand Brand { get; set; }

        /// <summary>
        /// Gets or Sets the Count of awards won by a groupped Brand.
        /// </summary>
        public int AwardCount { get; set; }

        /// <summary>
        /// Gets or Sets the sequence of awarded Products from the groupped Brand.
        /// </summary>
        public System.Collections.Generic.IEnumerable<Data.Product> WinningProducts { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is BrandWithAwards other
                && this.AwardCount == other.AwardCount
                && this.Brand.Equals(other.Brand)
                && this.IsSameWinningProducts(other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.AwardCount + this.Brand.GetHashCode();
        }

        /// <summary>
        /// Returns a custom <see cref="string"/> that represents the current item.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the current item.</returns>
        public override string ToString()
        {
            string output = $" -- {this.AwardCount} awards won by \n{this.Brand}\n - with products:";
            foreach (var product in this.WinningProducts)
            {
                output += $"\n{product}";
            }

            return output + "\n";
        }

        private bool IsSameWinningProducts(BrandWithAwards other)
        {
            bool isWPequ = !(this.WinningProducts is null || other.WinningProducts is null);
            if (!isWPequ)
            {
                return false;
            }

            int otherWPcount = other.WinningProducts.Count();
            isWPequ = this.WinningProducts.Count() == otherWPcount;

            if (!isWPequ)
            {
                return false;
            }

            for (int i = 0; i < otherWPcount; i++)
            {
                if (!this.WinningProducts.ElementAt(i).Equals(other.WinningProducts.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
