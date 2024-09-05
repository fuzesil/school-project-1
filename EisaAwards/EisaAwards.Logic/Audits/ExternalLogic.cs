namespace EisaAwards.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EisaAwards.Data;
    using EisaAwards.Repository;

    /// <summary>
    /// Contains methods that work with tables: <see cref="Brand"/>, <see cref="Product"/>, <see cref="ExpertGroup"/>.
    /// </summary>
    public abstract class ExternalLogic : IReadExternalData, IReadCommonData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLogic"/> class.
        /// </summary>
        /// <param name="topN">The value of 'N' in "the top N of ..." queries.</param>
        /// <param name="brands">An object for the <see cref="Brand"/> repo.</param>
        /// <param name="products">An object for the <see cref="Product"/> repo.</param>
        /// <param name="expertgroups">An object for the <see cref="ExpertGroup"/> repo.</param>
        protected ExternalLogic(int topN, IBrandRepository brands, IProductRepository products, IExpertGroupRepository expertgroups)
        {
            this.TopN = topN;
            this.Brands = brands;
            this.Products = products;
            this.Expertgroups = expertgroups;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLogic"/> class.
        /// </summary>
        /// <param name="dbContext">The instance of the session to the database.</param>
        protected ExternalLogic(ref Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            this.Brands = new BrandRepository(ref dbContext);
            this.Expertgroups = new ExpertGroupRepository(ref dbContext);
            this.Products = new ProductRepository(ref dbContext);
        }

        /// <summary>
        /// Gets or Sets the value of 'N'.
        /// </summary>
        public int TopN { get; set; }

        /// <summary>
        /// Gets the <see cref="Brand"/> repository instance.
        /// </summary>
        public IBrandRepository Brands { get; private set; }

        /// <summary>
        /// Gets the <see cref="Product"/> repository instance.
        /// </summary>
        public IProductRepository Products { get; private set; }

        /// <summary>
        /// Gets the <see cref="ExpertGroup"/> repository instance.
        /// </summary>
        public IExpertGroupRepository Expertgroups { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<Brand> ListAllBrands(out int count)
        {
            IQueryable<Brand> output = this.Brands.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<ExpertGroup> ListAllExpertgroups(out int count)
        {
            IQueryable<ExpertGroup> output = this.Expertgroups.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Product> ListAllProducts(out int count)
        {
            IQueryable<Product> output = this.Products.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public Brand GetOneBrand(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Brands.GetOne(id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Brands.GetOne(name);
            }

            throw new InvalidOperationException("Neither the ID nor the Name was valid.");
        }

        /// <inheritdoc/>
        public ExpertGroup GetOneExpertGroup(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Expertgroups.GetOne(id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Expertgroups.GetOne(name);
            }

            throw new InvalidOperationException("Neither the ID nor the Name was valid.");
        }

        /// <inheritdoc/>
        public Product GetOneProduct(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Products.GetOne(id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Products.GetOne(name);
            }

            throw new InvalidOperationException("Neither the ID nor the Name was valid.");
        }

        /// <summary>
        /// Returns the top 'N' brands by awards won, where 'N' is an integer.
        /// </summary>
        /// <returns>The sequence of 'M' most awarded brands and count of awards, where 'M' is at least 'N'.</returns>
        public abstract IEnumerable<BrandAndNumber> ListTopBrands();

        /// <summary>
        /// Returns the products in Brand groups from the top 'N' brands.
        /// </summary>
        /// <param name="count">The number of elements in the returned list.</param>
        /// <returns>Brands with their products in groups of the top 'N' brands.</returns>
        public abstract IEnumerable<BrandWithAwards> GetProductsByBrandId(out int count);

        /// <summary>
        /// Returns a list of ExpertGroup - Product pairs
        /// where the product has the highest price amongst other products awarded by that expert group.
        /// </summary>
        /// <returns>List of <see cref="ExpertGroup"/> - <see cref="Product"/> pairs
        /// where that product is the most expensive in relation to that expert group.</returns>
        public abstract IEnumerable<ExpertgroupProduct> GetMaxPriceProdInEveryEG();

        /// <summary>
        /// Asynchronous version of <see cref="GetMaxPriceProdInEveryEG"/>.
        /// </summary>
        /// <returns>Call to the non-async version of this method.</returns>
        public abstract Task<IEnumerable<ExpertgroupProduct>> GetMaxPriceProdInEveryEGAsync();

        /// <summary>
        /// Asynchronous version of <see cref="ListTopBrands"/>.
        /// </summary>
        /// <returns>Call to the non-async version of this method.</returns>
        public abstract Task<IEnumerable<BrandAndNumber>> ListTopBrandsAsync();
    }
}
