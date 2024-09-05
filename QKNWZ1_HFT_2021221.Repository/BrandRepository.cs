using System.Linq;
using Microsoft.EntityFrameworkCore;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.Repository
{
	/// <summary>
	/// Implements basic operations only applicable to <see cref="Brand"/> objects.
	/// </summary>
	public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BrandRepository"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
		public BrandRepository(DbContext dbContext)
			: base(dbContext)
		{
		}

		/// <inheritdoc/>
		public bool Create(string name, string homepage, string address, int countryId)
		{
			Brand brand = new() { Name = name, Homepage = homepage, Address = address, };
			return base.Create(brand);
		}

		/// <inheritdoc/>
		public override Brand Read(int id) => this.ReadAll().SingleOrDefault(brand => brand.Id == id);

		/// <inheritdoc/>
		public override Brand Read(string name) => this.ReadAll().SingleOrDefault(brand => brand.Name.Contains(name));

		/*
		/// <inheritdoc/>
		public override bool Delete(int id) => this.Delete(this.Read(id));
		*/

		/// <inheritdoc/>
		public bool ChangeHomePage(int id, string homePage)
		{
			var brand = this.Read(id);
			brand.Homepage = homePage;
			return this.Update(brand);
		}

		/// <inheritdoc/>
		public bool ChangeName(int id, string name)
		{
			var brand = this.Read(id);
			brand.Name = name;
			return this.Update(brand);
		}

		/// <inheritdoc/>
		public bool Move(int id, int countryId, string address)
		{
			var brand = this.Read(id);
			brand.Address = address;
			brand.CountryId = countryId;
			return this.Update(brand);
		}
	}
}
