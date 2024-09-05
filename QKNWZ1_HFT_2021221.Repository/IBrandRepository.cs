namespace QKNWZ1_HFT_2021221.Repository
{
	using Models;

	/// <summary>
	/// Declares basic operations applicable to <see cref="Brand"/> objects only.
	/// </summary>
	public interface IBrandRepository : IRepository<Brand>
	{
		/// <summary>
		/// Creates a new <see cref="Brand"/> with the given data.
		/// </summary>
		/// <param name="name">The <see cref="Brand.Name"/> of the newly created object.</param>
		/// <param name="homepage">The <see cref="Brand.Homepage"/> of the newly created object.</param>
		/// <param name="address">The <see cref="Brand.Address"/> of the newly created object.</param>
		/// <param name="countryId">The <see cref="Brand.CountryId"/> of the newly created object.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Create(T)"/>.</returns>
		bool Create(string name, string homepage, string address, int countryId);

		/// <summary>
		/// Updates <see cref="Brand.Homepage"/> for the object where <see cref="Brand.Id"/> <c>==</c> <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Brand.Id"/> of the object to modify.</param>
		/// <param name="homePage">The new value of <see cref="Brand.Homepage"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool ChangeHomePage(int id, string homePage);

		/// <summary>
		/// Updates <see cref="Brand.Name"/> for the object where <see cref="Brand.Id"/> <c>==</c> <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Brand.Id"/> of the object to modify.</param>
		/// <param name="name">The new value for <see cref="Brand.Name"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool ChangeName(int id, string name);

		/// <summary>
		/// Updates the <see cref="Brand"/>'s fields of location (<see cref="Brand.CountryId"/> and <see cref="Brand.Address"/>)
		/// and returns whether the modifications are ready (but not saved yet).
		/// </summary>
		/// <param name="id">The value of <see cref="Brand.Id"/> of the object to update.</param>
		/// <param name="countryId">The new value for <see cref="Brand.CountryId"/>.</param>
		/// <param name="address">The new value for <see cref="Brand.Address"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool Move(int id, int countryId, string address);
	}
}
