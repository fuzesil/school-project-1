using System.Linq;
using Microsoft.EntityFrameworkCore;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.Repository
{
	/// <summary>
	/// Repository class for the <see cref="Product"/> entity.
	/// </summary>
	public class ProductRepository : RepositoryBase<Product>, IProductRepository
	{
		// private readonly DbContext db;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductRepository"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
		public ProductRepository(DbContext dbContext)
			: base(dbContext)
		{
			// this.db = db ?? throw new System.ArgumentNullException(nameof(db));
		}

		/// <inheritdoc/>
		public bool Create(int expertgroupId, string category, string name, int brandId, int price, System.DateTime launchDate, int estimatedLifetime)
		{
			Product product = new()
			{
				ExpertGroupId = expertgroupId,
				Category = category,
				Name = name,
				BrandId = brandId,
				Price = price,
				LaunchDate = launchDate,
				EstimatedLifetime = estimatedLifetime,
			};
			return this.Create(product);
		}

		/// <inheritdoc/>
		public bool ChangePrice(int id, int price)
		{
			var thisProduct = this.Read(id);
			thisProduct.Price = price;
			return this.Update(thisProduct);
		}

		/// <inheritdoc/>
		public override Product Read(int id) => this.ReadAll().SingleOrDefault(product => product.Id == id);

		/// <inheritdoc/>
		public override Product Read(string name) => this.ReadAll().SingleOrDefault(product => product.Name.Contains(name));

		///// <inheritdoc/>
		//public override Product GetOne(string name)
		//{
		//    return this.GetAll().FirstOrDefault(product => product.Name.Contains(name));
		//}

		/*
		/// <inheritdoc/>
		public override bool Delete(int id) => base.Delete(this.Read(id));
		*/
		/*
		/// <summary>
		/// Returns 1 record or throws exception.
		/// </summary>
		/// <param name="id">The ID of the record to be returned.</param>
		/// <returns>The one record with the matching ID.</returns>
		private Product GetProduct(int id)
		{
			Product thisProduct;
			try
			{
				thisProduct = this.GetAll().Single(item => item.ProductID == id);
			}
			catch (System.InvalidOperationException ex)
			{
				throw new System.ApplicationException($"No records with the given ID [{id}] found by {nameof(this.GetProduct)}.", ex);
			}

			return thisProduct;
		}
		*/
	}
}
