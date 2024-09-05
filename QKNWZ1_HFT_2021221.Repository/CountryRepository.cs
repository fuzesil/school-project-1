using System.Linq;
using Microsoft.EntityFrameworkCore;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.Repository
{
	/// <summary>
	/// Implements basic operations only applicable to <see cref="Country"/> objects.
	/// </summary>
	public class CountryRepository : RepositoryBase<Country>, ICountryRepository
	{
		// private readonly DbContext db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CountryRepository"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of type <see cref="DbContext"/> that represents the connection to the database.</param>
		public CountryRepository(DbContext dbContext)
			: base(dbContext)
		{
			// this.db = db ?? throw new System.ArgumentNullException(nameof(db));
		}

		/// <inheritdoc/>
		public bool ChangeName(int id, string name)
		{
			var thisCountry = this.Read(id);
			thisCountry.Name = name;
			return this.Update(thisCountry);
		}

		/// <inheritdoc/>
		public bool ChangeCapitalCity(int id, string capitalCity)
		{
			var thisCountry = this.Read(id);
			thisCountry.CapitalCity = capitalCity;
			return this.Update(thisCountry);
		}

		/// <inheritdoc/>
		public bool ChangePPP(int id, int ppp)
		{
			var thisCountry = this.Read(id);
			thisCountry.PPPperCapita = ppp;
			return this.Update(thisCountry);
		}

		/// <inheritdoc/>
		public double GetAveragePPP() => this.ReadAll().Average(country => country.PPPperCapita);

		/// <inheritdoc/>
		public bool Create(string name, string capitalCity, int callingCode, int pppPerCapita)
		{
			Country country = new()
			{
				Name = name,
				CapitalCity = capitalCity,
				CallingCode = callingCode,
				PPPperCapita = pppPerCapita,
			};
			return this.Create(country);
		}

		/// <inheritdoc/>
		public override Country Read(int id) => this.ReadAll().SingleOrDefault(country => country.Id == id);

		/// <inheritdoc/>
		public override Country Read(string name) => this.ReadAll().FirstOrDefault(country => country.Name.Contains(name));

		/*
		/// <inheritdoc/>
		public override bool Delete(int id) => base.Delete(this.Read(id));
		*/
		/*
		/// <summary>
		/// Returns 1 record or throws exception.
		/// </summary>
		/// <param name="id">The ID of the record to be returned.</param>
		/// <returns>The one record with the matching ID.</returns>
		private Country GetCountry(int id)
		{
			Country country;
			try
			{
				country = this.GetAll().Single(country => country.CountryID == id);
			}
			catch (System.InvalidOperationException ex)
			{
				throw new System.ApplicationException($"No record with the given ID [{id}] found by {nameof(this.GetCountry)}.", ex);
			}

			return country;
		}
		*/
	}
}
