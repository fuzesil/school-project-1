﻿namespace EisaAwards.Repository
{
    using System.Linq;
    using EisaAwards.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository class for the <see cref="Product"/> entity.
    /// </summary>
    public class ProductRepository : RepositoryClass<Product>, IProductRepository
    {
        private readonly DbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="db">The <see cref="DbContext"/> parametre.</param>
        public ProductRepository(DbContext db)
            : base(db)
        {
            this.db = db ?? throw new System.ArgumentNullException(nameof(db));
        }

        /// <inheritdoc/>
        public void ChangePrice(int id, int newprice)
        {
            Product thisProduct = this.GetOne(id);
            thisProduct.Price = newprice;
            this.Update(thisProduct);
        }

        /// <inheritdoc/>
        public override Product GetOne(int id)
        {
            return this.GetAll().Single(product => product.ProductID == id);
        }

        /// <inheritdoc/>
        public override Product GetOne(string name)
        {
            return this.GetAll().First(product => product.Name == name);
        }

        /// <inheritdoc/>
        public override void Remove(int id)
        {
            this.Remove(this.GetOne(id));
        }

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
