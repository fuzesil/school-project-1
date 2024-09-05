using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QKNWZ1_HFT_2021221.Models;
using QKNWZ1_HFT_2021221.Repository;

namespace QKNWZ1_HFT_2021221.Logic
{
	/// <summary>
	/// Contains additional functions that work with tables: Brands, Products, ExpertGroups.
	/// </summary>
	public class ExternalAuditLogic : IExternalLogic
	{
		/* private static ExternalAuditLogic singletonEAL;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalAuditLogic"/> class.
		/// </summary>
		/// <param name="topN">The value of 'N' in "the top N" queries.</param>
		/// <param name="brands">The Brands table.</param>
		/// <param name="products">The Products table.</param>
		/// <param name="expertgroups">The ExpertGroups table.</param>
		public ExternalAuditLogic(int topN, IBrandRepository brands, IProductRepository products, IExpertGroupRepository expertgroups)
			: base(topN, brands, products, expertgroups)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalAuditLogic"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of the session to the database.</param>
		public ExternalAuditLogic(ref Microsoft.EntityFrameworkCore.DbContext dbContext)
			: base(dbContext)
		{
		}

		/// <summary>
		/// Creates an instance of <see cref="ExternalAuditLogic"/>.
		/// </summary>
		/// <exception cref="ArgumentNullException">Repository arguments mustn't be null.</exception>
		/// <exception cref="ArgumentException">Argument mustn't be less than 1.</exception>
		/// <param name="limiter">The value of 'N' in "the top N" queries.</param>
		/// <param name="brandRepository">The Brands table.</param>
		/// <param name="productRepository">The Products table.</param>
		/// <param name="expertGroupRepository">The ExpertGroups table.</param>
		/// <returns>An instance of this class.</returns>
		public static ExternalAuditLogic Initialize(
			int limiter,
			IBrandRepository brandRepository,
			IProductRepository productRepository,
			IExpertGroupRepository expertGroupRepository)
		{
			if (limiter < 1)
			{
				throw new ArgumentException("This cannot be less than 1", nameof(limiter));
			}

			if (singletonEAL == null)
			{
				singletonEAL = new ExternalAuditLogic(limiter, brandRepository, productRepository, expertGroupRepository);
			}

			return singletonEAL;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalLogic"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of the session to the database.</param>
		/// <param name="topN">The value to be assigned to <see cref="TopN"/>.</param>
		protected ExternalAuditLogic(Microsoft.EntityFrameworkCore.DbContext dbContext, int topN = 2)
		{
			this.TopN = topN;
			this.Brands = new BrandRepository(dbContext);
			this.Expertgroups = new ExpertGroupRepository(dbContext);
			this.Products = new ProductRepository(dbContext);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalAuditLogic"/> class.
		/// </summary>
		/// <param name="topN">The value of 'N' in "the top N of ..." queries.</param>
		/// <param name="brands">An object for the <see cref="Brand"/> repo.</param>
		/// <param name="products">An object for the <see cref="Product"/> repo.</param>
		/// <param name="expertgroups">An object for the <see cref="ExpertGroup"/> repo.</param>
		public ExternalAuditLogic(int topN, IBrandRepository brands, IProductRepository products, IExpertGroupRepository expertgroups)
		{
			this.TopN = topN;
			this.Brands = brands;
			this.Products = products;
			this.Expertgroups = expertgroups;
		}
		*/

		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalAuditLogic"/> class. The <see cref="TopN"/> field is set to 3.
		/// </summary>
		/// <param name="topN">The value of 'N' in "the top N of ..." queries.</param>
		/// <param name="brands">An object for the <see cref="Brand"/> repo.</param>
		/// <param name="products">An object for the <see cref="Product"/> repo.</param>
		/// <param name="expertgroups">An object for the <see cref="ExpertGroup"/> repo.</param>
		public ExternalAuditLogic(IBrandRepository brands, IProductRepository products, IExpertGroupRepository expertgroups, int topN = 3)
		{
			this.TopN = topN;
			this.Brands = brands;
			this.Products = products;
			this.Expertgroups = expertgroups;
		}


		/// <summary> Gets or Sets the value of 'N'. </summary>
		public int TopN { get; set; }

		/// <summary> Gets the <see cref="Brand"/> repository instance. </summary>
		public IBrandRepository Brands { get; private set; }

		/// <summary> Gets the <see cref="Product"/> repository instance. </summary>
		public IProductRepository Products { get; private set; }

		/// <summary> Gets the <see cref="ExpertGroup"/> repository instance. </summary>
		public IExpertGroupRepository Expertgroups { get; private set; }

		/// <inheritdoc/>
		public IEnumerable<Brand> ListAllBrands(out int count)
		{
			var output = this.Brands.ReadAll();
			count = output.Count();
			return output.ToList();
		}

		/// <inheritdoc/>
		public IEnumerable<ExpertGroup> ListAllExpertgroups(out int count)
		{
			var output = this.Expertgroups.ReadAll();
			count = output.Count();
			return output.ToList();
		}

		/// <inheritdoc/>
		public IEnumerable<Product> ListAllProducts(out int count)
		{
			var output = this.Products.ReadAll();
			count = output.Count();
			return output.ToList();
		}

		/// <inheritdoc/>
		public Brand GetOneBrand(int id) => this.Brands.Read(id);

		/// <inheritdoc/>
		public Brand GetOneBrand(string name) => this.Brands.Read(name);

		/// <inheritdoc/>
		public ExpertGroup GetOneExpertGroup(int id) => this.Expertgroups.Read(id);

		/// <inheritdoc/>
		public ExpertGroup GetOneExpertGroup(string name) => this.Expertgroups.Read(name);

		/// <inheritdoc/>
		public Product GetOneProduct(int id) => this.Products.Read(id);

		/// <inheritdoc/>
		public Product GetOneProduct(string name) => this.Products.Read(name);

		/// <inheritdoc/>
		public IEnumerable<BrandAndNumber> ListTopBrands()
		{
			return this.GetBrandsAndAwardCounts()
				.OrderByDescending(brandAndNumber => brandAndNumber.Number)
				.ToList();
		}

		/// <inheritdoc/>
		public Task<IEnumerable<BrandAndNumber>> ListTopBrandsAsync() => Task.Run(this.ListTopBrands);

		/// <inheritdoc/>
		public IEnumerable<BrandWithAwards> GetProductsByBrandId(out int count)
		{
			var output = this.GetBrandsAndAwardCounts()
				.Select(brandAndNumber => new BrandWithAwards
				{
					AwardCount = brandAndNumber.Number,
					Brand = brandAndNumber.Brand,
					WinningProducts = this.Products.ReadAll()
						.Where(product => product.BrandId == brandAndNumber.Brand.Id),
				})
				.ToList();

			count = output.Count;
			return output.OrderByDescending(brandWithAwards => brandWithAwards.AwardCount);
		}

		/// <inheritdoc/>
		public IEnumerable<ExpertgroupProduct> GetMaxPriceProdInEveryEG()
		{
			/*var q = from product in this.Products.ReadAll()
					group product by product.ExpertGroupId into prodGrp
					let egIdWithMaxPrice = new
					{
						egID = prodGrp.Key,
						MaxPrice = prodGrp.Max(product => product.Price),
					}
					join product in this.Products.ReadAll() on
					new { egIdWithMaxPrice.MaxPrice, egIdWithMaxPrice.egID } equals
					new { MaxPrice = product.Price, egID = product.ExpertGroupId }
					join expertgroup in this.Expertgroups.ReadAll() on egIdWithMaxPrice.egID equals expertgroup.Id
					select new ExpertgroupProduct
					{
						ExpertGroup = expertgroup,
						Product = product,
					};
			return q.ToList();*/
			var query =
				this.Products.ReadAll()
				.GroupBy(product => product.ExpertGroupId)
				.Select(grp => new
				{
					ExpertGroupID = grp.Key,
					MaxPrice = grp.Max(product => product.Price),
				})
				.Join(
					  this.Products.ReadAll(),
					  group => group.MaxPrice,
					  prod => prod.Price,
					  (group, prod) => new { group, prod, })
				.Join(
					this.Expertgroups.ReadAll(),
					groupedAndProd => groupedAndProd.group.ExpertGroupID,
					eg => eg.Id,
					(groupedAndProd, eg) =>
						new ExpertgroupProduct { ExpertGroup = eg, Product = groupedAndProd.prod, });
			return query.ToList();
		}

		/// <inheritdoc/>
		public Task<IEnumerable<ExpertgroupProduct>> GetMaxPriceProdInEveryEGAsync() => Task.Run(this.GetMaxPriceProdInEveryEG);

		/// <summary>
		/// Returns a sequence of <see cref="BrandAndNumber"/> type elements; that is,
		/// a <see cref="Brand"/> object and the corresponding <see cref="int"/> Count of awards it won,
		/// given that it is in the top 'N' (defined in <see cref="TopN"/>) brands.
		/// </summary>
		/// <returns>The ID (first item) and the Count (second item) of top 'N' brands.</returns>
		private IEnumerable<BrandAndNumber> GetBrandsAndAwardCounts()
		{
			var topAwardNumbers = this.Products.ReadAll()
				.GroupBy(prod => prod.BrandId)
				.Select(group => group.Count())
				.Distinct()
				.OrderByDescending(grpCnt => grpCnt)
				.Take(this.TopN)
				.ToArray();
			/*
			int[] topBrandCounts =
				(from product in this.Products.ReadAll()
				group product by product.BrandId into prodGrp
				let prodCount = prodGrp.Count()
				orderby prodCount descending
				select prodCount)
				.Distinct()
				.Take(this.TopN)
				.ToArray();

			var q = from product in this.Products.ReadAll()
					group product by product.BrandId into prodGrp
					let prodCount = prodGrp.Count()
					where topBrandCounts.Contains(prodCount)
					join brand in this.Brands.ReadAll() on prodGrp.Key equals brand.Id
					select new BrandAndNumber { Brand = brand, Number = prodCount };
			return q.ToList();
			*/
			var result = this.Products.ReadAll()
				.GroupBy(prod => prod.BrandId)
				.Select(group => new { brandID = group.Key, Count = group.Count() })
				.Where(group => topAwardNumbers.Contains(group.Count))
				//// .Select(grp => new Tuple<int, int>(grp.brandID, grp.Count))
				// .Select(group => new NumberAndId { Id = group.brandID, Number = group.Count })
				.Join(
					this.Brands.ReadAll(),
					group => group.brandID,
					brand => brand.Id,
					(group, brand) =>
						new BrandAndNumber { Brand = brand, Number = group.Count });

			return result.ToList();
		}
	}
}
