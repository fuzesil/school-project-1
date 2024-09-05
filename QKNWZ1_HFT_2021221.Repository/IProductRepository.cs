namespace QKNWZ1_HFT_2021221.Repository
{
	using Models;

	/// <summary>
	/// Declares basic operations applicable to <see cref="Product"/> objects only.
	/// </summary>
	public interface IProductRepository : IRepository<Product>
	{
		/// <summary>
		/// Updates the <see cref="Product.Price"/> for the object having the primary key <paramref name="id"/> <c>==</c> <see cref="Product.Id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Product.Id"/> of the object to be updated.</param>
		/// <param name="price">The new value of <see cref="Product.Price"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangePrice(int id, int price);

		/// <summary>
		/// Creates a new <see cref="Product"/> from the input parametres.
		/// </summary>
		/// <param name="expertgroupId">The value of the new <see cref="Product.ExpertGroupId"/>.</param>
		/// <param name="category">The value of the new <see cref="Product.Category"/>.</param>
		/// <param name="name">The value of the new <see cref="Product.Name"/>.</param>
		/// <param name="brandId">The value of the new <see cref="Product.BrandId"/>.</param>
		/// <param name="price">The value of the new <see cref="Product.Price"/>.</param>
		/// <param name="launchDate">The value of the new <see cref="Product.LaunchDate"/>.</param>
		/// <param name="estimatedLifetime">The value of the new <see cref="Product.EstimatedLifetime"/>.</param>
		/// <returns>The return value of <see cref="IRepository{T}.Create(T)"/>.</returns>
		bool Create(int expertgroupId, string category, string name, int brandId, int price, System.DateTime launchDate, int estimatedLifetime);
	}
}
