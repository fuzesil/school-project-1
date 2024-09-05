namespace QKNWZ1_HFT_2021221.Repository
{
	using Models;

	/// <summary>
	/// Declares basic operations applicable to <see cref="Country"/> objects only.
	/// </summary>
	public interface ICountryRepository : IRepository<Country>
	{
		/// <summary>
		/// Creates a new <see cref="Country"/> object from the input data.
		/// </summary>
		/// <param name="name">The value of the new <see cref="Country.Name"/>.</param>
		/// <param name="capitalCity">The value of the new <see cref="Country.CapitalCity"/>.</param>
		/// <param name="callingCode">The value of the new <see cref="Country.CallingCode"/>.</param>
		/// <param name="pppPerCapita">The value of the new <see cref="Country.PPPperCapita"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Create(T)"/>.</returns>
		bool Create(string name, string capitalCity, int callingCode, int pppPerCapita);

		/// <summary>
		/// Updates <see cref="Country.PPPperCapita"/> for the object where <see cref="Country.Id"/> <c>==</c> <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Country.Id"/> of the object to modify.</param>
		/// <param name="ppp">The new value of <see cref="Country.PPPperCapita"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool ChangePPP(int id, int ppp);

		/// <summary>
		/// Updates <see cref="Country.Name"/> for the object where <see cref="Country.Id"/> <c>==</c> <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Country.Id"/> of the object to modify.</param>
		/// <param name="name">The new value for <see cref="Country.Name"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool ChangeName(int id, string name);

		/// <summary>
		/// Updates <see cref="Country.CapitalCity"/> for the object where <see cref="Country.Id"/> <c>==</c> <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The value of <see cref="Country.Id"/> of the object to modify.</param>
		/// <param name="capitalCity">The new value for <see cref="Country.CapitalCity"/>.</param>
		/// <returns>The value returned by calling <see cref="IRepository{T}.Update(T)"/>.</returns>
		bool ChangeCapitalCity(int id, string capitalCity);

		/// <summary>
		/// Returns the <see cref="System.Linq.Queryable.Average(System.Linq.IQueryable{int})"/> of <see cref="Country.PPPperCapita"/>.
		/// </summary>
		/// <returns>The average of <see cref="Country.PPPperCapita"/>.</returns>
		double GetAveragePPP();
	}
}
