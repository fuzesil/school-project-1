using System.Linq;

namespace QKNWZ1_HFT_2021221.Models
{
	/// <summary>
	/// A custom type that stores a <see cref="Models.Brand"/> object, <see cref="int"/> Count of awards,
	/// and <see cref="System.Collections.Generic.IEnumerable{T}"/> (T is <see cref="Product"/>) Winner products' list.
	/// </summary>
	public class BrandWithAwards : System.IEquatable<BrandWithAwards>
	{
		/// <summary>
		/// Gets or Sets the <see cref="Models.Brand"/> object of interest.
		/// </summary>
		public Brand Brand { get; set; }

		/// <summary>
		/// Gets or Sets the number of awards that <see cref="Brand"/> received.
		/// </summary>
		public int AwardCount { get; set; }

		/// <summary>
		/// Gets or Sets the sequence of awarded <see cref="Product"/>s belonging to the <see cref="Brand"/>.
		/// </summary>
		public System.Collections.Generic.IEnumerable<Product> WinningProducts { get; set; }

		/// <inheritdoc/>
		public override bool Equals(object obj) => obj is BrandWithAwards other && this.Equals(other);

		/// <inheritdoc/>
		public bool Equals(BrandWithAwards other)
		{
			return other is not null
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
		/// Returns a custom <see cref="string"/> that represents the current <see cref="BrandWithAwards"/> object.
		/// </summary>
		/// <returns>The <see cref="string"/> representation of the current object.</returns>
		public override string ToString()
		{
			var sb = new System.Text.StringBuilder($" --> '{this.Brand.Name}' won {this.AwardCount} awards with products:");
			sb.AppendLine();
			foreach (var product in this.WinningProducts)
			{
				sb.AppendLine(product.ToString());
			}

			return sb.ToString();
		}

		/// <summary>
		/// Examines whether two <see cref="WinningProducts"/> are both <see langword="null"/> -OR- <see cref="Enumerable.SequenceEqual{TSource}(System.Collections.Generic.IEnumerable{TSource}, System.Collections.Generic.IEnumerable{TSource})"/> returns <see langword="true"/>.
		/// </summary>
		/// <param name="other"></param>
		/// <returns><see langword="true"/> when the <see cref="WinningProducts"/> of both <see langword="this"/> and <paramref name="other"/> are <see langword="null"/> OR they are sequentially <see langword="equal"/>.</returns>
		private bool IsSameWinningProducts(BrandWithAwards other) =>
			(this.WinningProducts is null && other.WinningProducts is null) || this.WinningProducts.SequenceEqual(other.WinningProducts);
	}
}
