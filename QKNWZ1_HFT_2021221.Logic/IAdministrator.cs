using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.Logic
{
	/// <summary>
	/// Declares read and write operations on the database.
	/// </summary>
	public interface IAdministrator
	{
		/// <summary>
		/// Marks a <see cref="Brand"/> for deletion from the database.
		/// </summary>
		/// <param name="id">The value of <see cref="Brand.Id"/>.</param>
		bool RemoveBrand(int id);

		/// <summary>
		/// Marks a <see cref="Country"/> for deletion from the database.
		/// </summary>
		/// <param name="id">The value of <see cref="Country.Id"/>.</param>
		bool RemoveCountry(int id);

		/// <summary>
		/// Marks a <see cref="ExpertGroup"/> for deletion from the database.
		/// </summary>
		/// <param name="id">The value of <see cref="ExpertGroup.Id"/>.</param>
		bool RemoveExpertgroup(int id);

		/// <summary>
		/// Marks a <see cref="Member"/> for deletion from the database.
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/>.</param>
		bool RemoveMember(int id);

		/// <summary>
		/// Marks a <see cref="Product"/> for deletion from the database.
		/// </summary>
		/// <param name="id">The value of <see cref="Product.Id"/>.</param>
		bool RemoveProduct(int id);

		/// <summary>
		/// Creates a new <see cref="Brand"/> for insertion into the database.
		/// </summary>
		/// <param name="name">The value of the new <see cref="Brand.Name"/>.</param>
		/// <param name="country">The value of the <see cref="Brand.CountryId"/> <b>OR</b> <see cref="Country.Name"/>.</param>
		/// <param name="address">The value of the new <see cref="Brand.Address"/>.</param>
		/// <param name="homepage">The value of the new <see cref="Brand.Homepage"/>.</param>
		bool InsertBrand(string name, string address, string homepage, string country);

		/// <summary>
		/// Creates a new <see cref="Country"/> for insertion into the database.
		/// </summary>
		/// <param name="name">The value of the new <see cref="Country.Name"/>.</param>
		/// <param name="capitalCity">The value of the new <see cref="Country.CapitalCity"/>.</param>
		/// <param name="callingCode">The value of the new <see cref="Country.Name"/> (1-3 digits, without '+' or '00').</param>
		/// <param name="ppp">The value of the new <see cref="Country.PPPperCapita"/>.</param>
		bool InsertCountry(string name, string capitalCity, int callingCode, int ppp);

		/// <summary>
		/// Creates a new <see cref="ExpertGroup"/> for insertion into the database.
		/// </summary>
		/// <param name="name">The value of the new <see cref="ExpertGroup.Name"/>.</param>
		bool InsertEG(string name);

		/// <summary>
		/// Creates a new <see cref="Brand"/> for insertion into the database.
		/// </summary>
		/// <param name="expertGroup">The value of the <see cref="ExpertGroup.Id"/> <b>OR</b> <see cref="ExpertGroup.Name"/>.</param>
		/// <param name="name">The value of <see cref="Member.Name"/> for the new object.</param>
		/// <param name="website">The value of <see cref="Member.Website"/> for the new object.</param>
		/// <param name="publisher">The value of <see cref="Member.Publisher"/> for the new object.</param>
		/// <param name="editor">The value of <see cref="Member.ChiefEditor"/> for the new object.</param>
		/// <param name="phone">The value of <see cref="Member.PhoneNumber"/> for the new object.</param>
		/// <param name="country">The value of the <see cref="Brand.CountryId"/> <b>OR</b> <see cref="Country.Name"/>.</param>
		/// <param name="office">The value of <see cref="Member.OfficeLocation"/> for the new object.</param>
		bool InsertMember(string expertGroup, string name, string website, string publisher, string editor, string phone, string country, string office);

		/// <summary>
		/// Adds one <see cref="Product"/> record to its table.
		/// </summary>
		/// <param name="expertGroup">The value of the <see cref="ExpertGroup.Id"/> <b>OR</b> <see cref="ExpertGroup.Name"/>.</param>
		/// <param name="category">The value of <see cref="Product.Category"/> for the new object.</param>
		/// <param name="name">The value of <see cref="Product.Name"/> for the new object.</param>
		/// <param name="brand">The value of the <see cref="Brand.Id"/> <b>OR</b> <see cref="Brand.Name"/>.</param>
		/// <param name="price">The value of <see cref="Product.Price"/> for the new object.</param>
		/// <param name="launchDate">The value of <see cref="Product.LaunchDate"/> for the new object.</param>
		/// <param name="estimatedLifetime">The value of <see cref="Product.EstimatedLifetime"/> for the new object.</param>
		bool InsertProduct(string expertGroup, string category, string name, string brand, int price, System.DateTime launchDate, int estimatedLifetime);

		/// <summary>
		/// Returns a <see cref="MemberBrand"/> sequence of <see cref="Brand"/>-<see cref="Member"/> pairs that are located at similar addresses.
		/// </summary>
		/// <returns>A sequence of <see cref="MemberBrand"/> objects where <see cref="Brand.Address"/> and <see cref="Member.OfficeLocation"/> are similar.</returns>
		System.Collections.Generic.IEnumerable<MemberBrand> ListBrandsAndMembersAtSameAddress();

		/// <summary>
		/// Asynchronously calls to <see cref="ListBrandsAndMembersAtSameAddress"/>.
		/// </summary>
		/// <returns>Call to the non-async method. </returns>
		System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<MemberBrand>> ListBrandsAndMembersAtSameAddressAsync();

		/// <summary>
		/// Marks <see cref="Member.Name"/> for update.
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/>.</param>
		/// <param name="name">The new value of <see cref="Member.Name"/>.</param>
		bool ChangeMemberName(int id, string name);

		/// <summary>
		/// Marks <see cref="Product.Price"/> for update.
		/// </summary>
		/// <param name="id">The value of <see cref="Product.Id"/>.</param>
		/// <param name="price">The new value of <see cref="Product.Price"/>.</param>
		bool ChangeProductPrice(int id, int price);

		/// <summary>
		/// Marks <see cref="Brand.Homepage"/> for update.
		/// </summary>
		/// <param name="id">The value of <see cref="Brand.Id"/>.</param>
		/// <param name="homepage">The new value of <see cref="Brand.Homepage"/>.</param>
		bool ChangeBrandHomePage(int id, string homepage);

		/// <summary>
		/// Marks <see cref="ExpertGroup.Name"/> for update.
		/// </summary>
		/// <param name="id">The value of <see cref="ExpertGroup.Id"/>.</param>
		/// <param name="name">The new value of <see cref="ExpertGroup.Name"/>.</param>
		bool ChangeExpertgroupName(int id, string name);

		/// <summary>
		/// Marks <see cref="Country.PPPperCapita"/> for update.
		/// </summary>
		/// <param name="id">The value of <see cref="Country.Id"/>.</param>
		/// <param name="ppp">The new value of <see cref="Country.PPPperCapita"/>.</param>
		bool ChangeCountryPPP(int id, int ppp);

		/// <summary>
		/// Calls the implementation of <see cref="Repository.IChangeManager.GetPendingChanges"/>.
		/// </summary>
		/// <returns>The return value of <see cref="Repository.IChangeManager.GetPendingChanges"/>.</returns>
		string GetPendingChanges();

		/// <summary>
		/// Calls the implementation of <see cref="Repository.IChangeManager.SaveChanges"/>.
		/// </summary>
		/// <returns>The return value of <see cref="Repository.IChangeManager.SaveChanges"/> <b>or</b> -1 if the call fails.</returns>
		int SaveChanges();

		Brand GetOneBrand(int id);
		void CreateBrand(Brand brand);
		void UpdateBrand(Brand brand);

		ExpertGroup GetOneExpertGroup(int id);
		void CreateExpertgroup(ExpertGroup expertgroup);
		void UpdateExpertgroup(ExpertGroup expertgroup);

		Country GetOneCountry(int id);
		void CreateCountry(Country country);
		void UpdateCountry(Country country);

		Member GetOneMember(int id);
		void CreateMember(Member member);
		void UpdateMember(Member member);
	}
}
