﻿namespace EisaAwards.Repository
{
    using System.Linq;
    using EisaAwards.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository class for the Brands table.
    /// </summary>
    public class BrandRepository : RepositoryClass<Brand>, IBrandRepository
    {
        // private readonly DbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
        public BrandRepository(ref DbContext dbContext)
            : base(ref dbContext)
        {
            // this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public void Move(int id, int newCountryID, string newAddress)
        {
            Brand brand = this.GetOne(id);
            brand.CountryID = newCountryID;
            brand.Address = newAddress;
            this.Update(brand);
        }

        /// <inheritdoc/>
        public void ChangeName(int id, string newName)
        {
            Brand brand = this.GetOne(id);
            brand.Name = newName;
            this.Update(brand);
        }

        /// <inheritdoc/>
        public void ChangeHomePage(int id, string newHP)
        {
            Brand brand = this.GetOne(id);
            brand.Homepage = newHP;
            this.Update(brand);
        }

        /// <inheritdoc/>
        public override Brand GetOne(int id)
        {
            return this.GetAll().Single(brand => brand.Id == id);
        }

        /// <inheritdoc/>
        public override Brand GetOne(string name)
        {
            return this.GetAll().First(brand => brand.Name.Contains(name));
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
        private Brand GetBrand(int id)
        {
            Brand brand;
            try
            {
                brand = this.GetAll().Single(brand => brand.BrandId == id);
            }
            catch (System.InvalidOperationException ex)
            {
                throw new System.ApplicationException($"No records with the given ID [{id}] found by {nameof(this.GetBrand)}.", ex);
            }

            return brand;
        }
        */
    }
}
