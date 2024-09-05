using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QKNWZ1_HFT_2021221.Models;
using QKNWZ1_HFT_2021221.Repository;

[assembly: System.CLSCompliant(false)]
namespace QKNWZ1_HFT_2021221.Logic
{
	/// <summary>
	/// Implements the 'Delete' and 'Insert' operations defined by <see cref="IAdministrator"/>.
	/// </summary>
	public class AdminLogic : IAdministrator /*, IReadInternalData, IReadExternalData, IReadCommonData */
	{
		/* private static AdminLogic singletonAL; */
		private readonly IBrandRepository brands;
		private readonly ICountryRepository countries;
		private readonly IExpertGroupRepository expertgroups;
		private readonly IMemberRepository members;
		private readonly IProductRepository products;

		/// <summary>
		/// Initializes a new instance of the <see cref="AdminLogic"/> class.
		/// </summary>
		/// <param name="brands">Repository for the 'Brands' table.</param>
		/// <param name="countries">Repository for the 'Countries' table.</param>
		/// <param name="expertgroups">Repository for the 'ExpertGroups' table.</param>
		/// <param name="members">Repository for the 'Members' table.</param>
		/// <param name="products">Repository for the 'Products' table.</param>
		public AdminLogic(
			IBrandRepository brands = null,
			ICountryRepository countries = null,
			IExpertGroupRepository expertgroups = null,
			IMemberRepository members = null,
			IProductRepository products = null)
		{
			this.brands = brands;
			this.countries = countries;
			this.expertgroups = expertgroups;
			this.members = members;
			this.products = products;
		}

		/*
		/// <summary>
		/// Initializes a new instance of the <see cref="AdminLogic"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of the session to the database.</param>
		public AdminLogic(Microsoft.EntityFrameworkCore.DbContext dbContext)
		{
			this.brands = new BrandRepository(dbContext);
			this.countries = new CountryRepository(dbContext);
			this.expertgroups = new ExpertGroupRepository(dbContext);
			this.members = new MemberRepository(dbContext);
			this.products = new ProductRepository(dbContext);
		}

		/// <summary>
		/// Return an instance of the class if it isn't already instantiated.
		/// </summary>
		/// <param name="brands">Repository for the 'Brands' table.</param>
		/// <param name="countries">Repository for the 'Countries' table.</param>
		/// <param name="expertgroups">Repository for the 'ExpertGroups' table.</param>
		/// <param name="members">Repository for the 'Members' table.</param>
		/// <param name="products">Repository for the 'Products' table.</param>
		/// <returns>One instance of the <see cref="AdminLogic"/> class.</returns>
		public static AdminLogic Initialize(
			IBrandRepository brands = null,
			ICountryRepository countries = null,
			IExpertGroupRepository expertgroups = null,
			IMemberRepository members = null,
			IProductRepository products = null)
		{
			if (singletonAL == null)
			{
				singletonAL = new AdminLogic(brands, countries, expertgroups, members, products);
			}

			return singletonAL;
		}
		*/

		/// <inheritdoc/>
		public IEnumerable<MemberBrand> ListBrandsAndMembersAtSameAddress()
		{
			/*
			var q =
				from brand in this.brands.ReadAll()
				join country in this.countries.ReadAll() on brand.CountryId equals country.Id
				join member in this.members.ReadAll() on country.Id equals member.CountryId
				where brand.CountryId == member.CountryId
					&& (brand.Address.Contains(member.OfficeLocation) || member.OfficeLocation.Contains(brand.Address))
				select new MemberBrand { Brand = brand, Member = member, };
			*/

			var query = this.brands.ReadAll()
				.Join(this.members.ReadAll(), brand => brand.CountryId, member => member.CountryId, (brand, member) => new { brand, member })
				.Where(mb => mb.member.CountryId == mb.brand.CountryId
				&& (mb.brand.Address.Contains(mb.member.OfficeLocation)
					|| mb.member.OfficeLocation.Contains(mb.brand.Address)))
				.Select(mb => new MemberBrand { Member = mb.member, Brand = mb.brand });
			return query.Distinct().ToList();

			/*
			return this.brands.ReadAll()
				 .Join(this.countries.ReadAll(), brand => brand.CountryID, country => country.CountryID, (brand, country) => new { brand, country })
				 .Join(this.members.ReadAll(), joinBC => joinBC.country.CountryID, member => member.CountryID, (joinBC, member) => new { BC = joinBC, member })
				 .Where(item => (item.BC.brand.CountryID == item.member.CountryID) && (item.BC.brand.Address.Contains(item.member.OfficeLocation) || item.member.OfficeLocation.Contains(item.BC.brand.Address)))
				 .Select(result => new MemberBrand { Brand = result.BC.brand, Member = result.member });
			*/
		}

		/// <inheritdoc/>
		public Task<IEnumerable<MemberBrand>> ListBrandsAndMembersAtSameAddressAsync() =>
			Task.Run(this.ListBrandsAndMembersAtSameAddress);

		/// <inheritdoc/>
		public bool InsertBrand(string name, string address, string homepage, string country)
		{
			Brand brand = new() { Name = name, Address = address, Homepage = homepage, };

			if (string.IsNullOrWhiteSpace(country))
			{
				throw new System.ArgumentException("Cannot accept null/empty/white-space-only parametre.", nameof(country));
			}
			brand.CountryId = int.TryParse(country, out int countryId) && this.countries.Read(countryId) is { } countryById and not default(Country)
				? countryById.Id
				: this.countries.Read(country) is Country countryByName and not default(Country)
					? countryByName.Id
					: throw new System.ArgumentException("Could not find the foreign object based on the parametre.", nameof(country));

			return this.brands.Create(brand);
		}

		/// <inheritdoc/>
		public bool InsertCountry(string name, string capitalCity, int callingCode, int ppp) => this.countries.Create(name, capitalCity, callingCode, ppp);

		/// <inheritdoc/>
		public bool InsertEG(string name) => this.expertgroups.Create(name);

		/// <inheritdoc/>
		public bool InsertMember(string expertGroup, string name, string website, string publisher, string editor, string phone, string country, string office)
		{
			Member member = new() { Name = name, Website = website, Publisher = publisher, ChiefEditor = editor, PhoneNumber = phone, OfficeLocation = office, };

			if (string.IsNullOrWhiteSpace(country))
			{
				throw new System.ArgumentException("Cannot accept null/empty/white-space-only parametre.", nameof(country));
			}
			member.CountryId = int.TryParse(country, out int countryId) && this.countries.Read(countryId) is Country countryById and not default(Country)
				? countryById.Id
				: this.countries.Read(country) is Country countryByName and not default(Country)
					? countryByName.Id
					: throw new System.ArgumentException("Could not find the foreign object based on the parametre.", nameof(country));

			if (string.IsNullOrWhiteSpace(expertGroup))
			{
				throw new System.ArgumentException("Cannot accept null/empty/white-space-only parametre.", nameof(country));
			}
			member.ExpertGroupId = int.TryParse(expertGroup, out int egId) && this.expertgroups.Read(egId) is ExpertGroup egById and not default(ExpertGroup)
				? egById.Id
				: this.expertgroups.Read(expertGroup) is ExpertGroup egByName and not default(ExpertGroup)
					? egByName.Id
					: throw new System.ArgumentException("Could not find the foreign object based on the parametre.", nameof(country));

			return this.members.Create(member);
		}

		/// <inheritdoc/>
		public bool InsertProduct(string expertGroup, string category, string name, string brand, int price, System.DateTime launchDate, int estimatedLifetime)
		{
			Product product = new() { Category = category, Name = name, Price = price, EstimatedLifetime = estimatedLifetime, LaunchDate = launchDate, };

			if (string.IsNullOrWhiteSpace(expertGroup))
			{
				throw new System.ArgumentException("Cannot accept null/empty/white-space-only parametre.", nameof(expertGroup));
			}
			product.ExpertGroupId = int.TryParse(expertGroup, out int egId) && this.expertgroups.Read(egId) is ExpertGroup egById and not default(ExpertGroup)
				? egById.Id
				: this.expertgroups.Read(expertGroup) is ExpertGroup egByName and not default(ExpertGroup)
					? egByName.Id
					: throw new System.ArgumentException("Could not validate this parametre.", nameof(expertGroup));

			if (string.IsNullOrWhiteSpace(brand))
			{
				throw new System.ArgumentException("Cannot accept null/empty/white-space-only parametre.", nameof(expertGroup));
			}
			product.BrandId =
				int.TryParse(brand, out int brandId) && this.brands.Read(brandId) is Brand brandById and not default(Brand)
				? brandById.Id
				: this.brands.Read(expertGroup) is Brand brandByName and not default(Brand)
					? brandByName.Id
					: throw new System.ArgumentException("Could not validate this parametre.", nameof(expertGroup));

			return this.products.Create(product);
		}

		/// <inheritdoc/>
		public bool RemoveBrand(int id) => this.brands.Delete(id);

		/// <inheritdoc/>
		public bool RemoveCountry(int id) => this.countries.Delete(id);

		/// <inheritdoc/>
		public bool RemoveExpertgroup(int id) => this.expertgroups.Delete(id);

		/// <inheritdoc/>
		public bool RemoveMember(int id) => this.members.Delete(id);

		/// <inheritdoc/>
		public bool RemoveProduct(int id) => this.products.Delete(id);

		/// <inheritdoc/>
		public bool ChangeExpertgroupName(int id, string name) => this.expertgroups.ChangeName(id, name);

		/// <inheritdoc/>
		public bool ChangeBrandHomePage(int id, string homepage) => this.brands.ChangeHomePage(id, homepage);

		/// <inheritdoc/>
		public bool ChangeCountryPPP(int id, int ppp) => this.countries.ChangePPP(id, ppp);

		/// <inheritdoc/>
		public bool ChangeMemberName(int id, string name) => this.members.ChangeName(id, name);

		/// <inheritdoc/>
		public bool ChangeProductPrice(int id, int price) => this.products.ChangePrice(id, price);

		/// <inheritdoc/>
		public string GetPendingChanges()
		{
			return string.Empty; // not necessary
		}

		/// <inheritdoc/>
		public int SaveChanges()
		{
			int num = 0;
			num += (this.brands as IChangeManager).SaveChanges();
			num += (this.countries as IChangeManager).SaveChanges();
			num += (this.expertgroups as IChangeManager).SaveChanges();
			num += (this.members as IChangeManager).SaveChanges();
			num += (this.products as IChangeManager).SaveChanges();
			return num;
		}

		public Brand GetOneBrand(int id) => this.brands.Read(id);
		public void CreateBrand(Brand brand) => this.brands.Create(brand);
		public void UpdateBrand(Brand brand) => this.brands.Update(brand);

		public Country GetOneCountry(int id) => this.countries.Read(id);
		public void CreateCountry(Country country) => this.countries.Create(country);
		public void UpdateCountry(Country country) => this.countries.Update(country);

		public ExpertGroup GetOneExpertGroup(int id) => this.expertgroups.Read(id);
		public void CreateExpertgroup(ExpertGroup expertgroup) => this.expertgroups.Create(expertgroup);
		public void UpdateExpertgroup(ExpertGroup expertgroup) => this.expertgroups.Update(expertgroup);

		public Member GetOneMember(int id) => this.members.Read(id);
		public void CreateMember(Member member) => this.members.Create(member);
		public void UpdateMember(Member member) => this.members.Update(member);
	}
}
