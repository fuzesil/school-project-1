namespace EisaAwards.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using EisaAwards.Repository;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminLogic"/> class.
        /// </summary>
        /// <param name="dbContext">The instance of the session to the database.</param>
        public AdminLogic(ref Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            this.brands = new BrandRepository(ref dbContext);
            this.countries = new CountryRepository(ref dbContext);
            this.expertgroups = new ExpertGroupRepository(ref dbContext);
            this.members = new MemberRepository(ref dbContext);
            this.products = new ProductRepository(ref dbContext);
        }

        /*
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
        public IEnumerable<MemberBrand> ListBrandsAndMembersAtSameAdress()
        {
            var q = from brand in this.brands.GetAll()
                    join country in this.countries.GetAll() on brand.CountryID equals country.Id
                    join member in this.members.GetAll() on country.Id equals member.CountryID
                    where brand.CountryID == member.CountryID && (brand.Address.Contains(member.OfficeLocation) || member.OfficeLocation.Contains(brand.Address))
                    select new MemberBrand { Brand = brand, Member = member };
            return q.ToList();
            /*
            return this.brands.GetAll()
                 .Join(this.countries.GetAll(), brand => brand.CountryID, country => country.CountryID, (brand, country) => new { brand, country })
                 .Join(this.members.GetAll(), joinBC => joinBC.country.CountryID, member => member.CountryID, (joinBC, member) => new { BC = joinBC, member })
                 .Where(item => (item.BC.brand.CountryID == item.member.CountryID) && (item.BC.brand.Address.Contains(item.member.OfficeLocation) || item.member.OfficeLocation.Contains(item.BC.brand.Address)))
                 .Select(result => new MemberBrand { Brand = result.BC.brand, Member = result.member });
            */
        }

        /// <inheritdoc/>
        public System.Threading.Tasks.Task<IEnumerable<MemberBrand>> ListBrandsAndMembersAtSameAdressAsync()
        {
            return System.Threading.Tasks.Task.Run(this.ListBrandsAndMembersAtSameAdress);
        }

        /// <inheritdoc/>
        public void InsertBrand(string name, string country, string address, string homepage)
        {
            Data.Brand newbrand = new ()
            {
                Name = name,
                CountryID = this.countries.GetOne(country).Id,
                Address = address,
                Homepage = homepage,
            };
            this.brands.Insert(newbrand);
        }

        /// <inheritdoc/>
        public void InsertCountry(string name, string capital, int callingcode, int ppp)
        {
            Data.Country newcountry = new ()
            {
                Name = name,
                CapitalCity = capital,
                CallingCode = callingcode,
                PPPperCapita = ppp,
            };
            this.countries.Insert(newcountry);
        }

        /// <inheritdoc/>
        public void InsertEG(string name)
        {
            Data.ExpertGroup newEG = new ()
            {
                Name = name,
            };
            this.expertgroups.Insert(newEG);
        }

        /// <inheritdoc/>
        public void InsertMember(string name, string website, string publisher, string editor, string phone, string country, string office, string expertgroup)
        {
            Data.Member newmember = new ()
            {
                ChiefEditor = editor,
                CountryID = this.countries.GetOne(country).Id,
                ExpertGroupID = this.expertgroups.GetOne(expertgroup).Id,
                Name = name,
                OfficeLocation = office,
                PhoneNumber = phone,
                Publisher = publisher,
                Website = website,
            };
            this.members.Insert(newmember);
        }

        /// <inheritdoc/>
        public void InsertProduct(string name, string brandName, int price, int estLifetime, System.DateTime launch, string expGrp, string award)
        {
            Data.Product newproduct = new ()
            {
                Name = name,
                BrandId = this.brands.GetOne(brandName).Id,
                EstimatedLifetime = estLifetime,
                Price = price,
                LaunchDate = launch,
                ExpertGroupID = this.expertgroups.GetOne(expGrp).Id,
                Category = award,
            };
            this.products.Insert(newproduct);
        }

        /// <inheritdoc/>
        public void RemoveBrand(int id)
        {
            this.brands.Remove(id);
        }

        /// <inheritdoc/>
        public void RemoveCountry(int id)
        {
            this.countries.Remove(id);
        }

        /// <inheritdoc/>
        public void RemoveEG(int id)
        {
            this.expertgroups.Remove(id);
        }

        /// <inheritdoc/>
        public void RemoveMember(int id)
        {
            this.members.Remove(id);
        }

        /// <inheritdoc/>
        public void RemoveProduct(int id)
        {
            this.products.Remove(id);
        }

        /// <inheritdoc/>
        public void ChangeEGName(int id, string name)
        {
            this.expertgroups.ChangeName(id, name);
        }

        /// <inheritdoc/>
        public void ChangeBrandHomePage(int id, string newHP)
        {
            this.brands.ChangeHomePage(id, newHP);
        }

        /// <inheritdoc/>
        public void ChangeCountryPPP(int id, int newPPP)
        {
            this.countries.ChangePPP(id, newPPP);
        }

        /// <inheritdoc/>
        public void ChangeMemberName(int id, string newName)
        {
            this.members.ChangeName(id, newName);
        }

        /// <inheritdoc/>
        public void ChangeProductPrice(int id, int newPrice)
        {
            this.products.ChangePrice(id, newPrice);
        }
    }
}
