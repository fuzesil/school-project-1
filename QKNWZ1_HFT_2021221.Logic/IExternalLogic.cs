namespace QKNWZ1_HFT_2021221.Logic
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Models;

	/// <summary>
	/// Declares read-only operations on <see cref="Brand"/> and <see cref="Product"/> objects.
	/// </summary>
	public interface IExternalLogic : ICommonLogic
	{
		/// <summary>
		/// Returns the sequence of all <see cref="Brand"/> objects from the database.
		/// </summary>
		/// <param name="count">The count of total elements in the resulting sequence.</param>
		/// <returns>The sequence of all <see cref="Brand"/> objects from the database.</returns>
		IEnumerable<Brand> ListAllBrands(out int count);

		/// <summary>
		/// Returns the <see cref="Brand"/> having the given <see cref="Brand.Id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Brand.Id"/> in the object to retrieve.</param>
		/// <returns>The matching <see cref="Brand"/> object.</returns>
		Brand GetOneBrand(int id);

		/// <summary>
		/// Returns the <see cref="Brand"/> having the given <see cref="Brand.Name"/>.
		/// </summary>
		/// <param name="name">The value of <see cref="Brand.Name"/> in the record to retrieve.</param>
		/// <returns>The matching <see cref="Brand"/> object.</returns>
		Brand GetOneBrand(string name);

		/// <summary>
		/// Returns the sequence of all <see cref="Product"/> objects from the database.
		/// </summary>
		/// <param name="count">The count of total elements in the resulting sequence.</param>
		/// <returns>The sequence of all <see cref="Product"/> objects from the database.</returns>
		IEnumerable<Product> ListAllProducts(out int count);

		/// <summary>
		/// Returns the <see cref="Product"/> having the given <see cref="Product.Id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Product.Id"/> in the object to retrieve.</param>
		/// <returns>The matching <see cref="Product"/> object.</returns>
		Product GetOneProduct(int id);

		/// <summary>
		/// Returns the <see cref="Product"/> having the given <see cref="Product.Name"/>.
		/// </summary>
		/// <param name="name">The value of <see cref="Product.Name"/> in the record to retrieve.</param>
		/// <returns>The matching <see cref="Product"/> object.</returns>
		Product GetOneProduct(string name);

		/// <summary>
		/// Returns the top 'N' brands by awards won, where 'N' is an <see cref="int"/>.
		/// </summary>
		/// <returns>The sequence of 'M' most awarded brands and count of awards, where 'M' is at least 'N'.</returns>
		IEnumerable<BrandAndNumber> ListTopBrands();

		/// <summary>
		/// Returns a <see cref="BrandWithAwards"/> sequence that holds
		/// the top <see cref="Brand"/>s and their <see cref="Product"/>s.
		/// </summary>
		/// <param name="count">The number of elements in the returned sequence.</param>
		/// <returns>Brands with their products in groups of the top 'N' brands.</returns>
		IEnumerable<BrandWithAwards> GetProductsByBrandId(out int count);

		/// <summary>
		/// Returns a list of ExpertGroup - Product pairs
		/// where the product has the highest price amongst other products awarded by that expert group.
		/// </summary>
		/// <returns>List of <see cref="ExpertGroup"/> - <see cref="Product"/> pairs
		/// where that product is the most expensive in relation to that expert group.</returns>
		IEnumerable<ExpertgroupProduct> GetMaxPriceProdInEveryEG();

		/// <summary>
		/// The <see langword="async"/> version of <see cref="GetMaxPriceProdInEveryEG"/>.
		/// </summary>
		/// <returns>Call to the non-<see langword="async"/> version of this method.</returns>
		Task<IEnumerable<ExpertgroupProduct>> GetMaxPriceProdInEveryEGAsync();

		/// <summary>
		/// The <see langword="async"/> version of <see cref="ListTopBrands"/>.
		/// </summary>
		/// <returns>Call to the non-<see langword="async"/> version of this method.</returns>
		Task<IEnumerable<BrandAndNumber>> ListTopBrandsAsync();
	}
}
