namespace QKNWZ1_HFT_2021221.Repository
{
	using Models;

	/// <summary>
	/// Declares basic operations applicable to <see cref="Member"/> objects only.
	/// </summary>
	public interface IMemberRepository : IRepository<Member>
	{
		/// <summary>
		/// Updates the <see cref="Member.Name"/> field (i.e. a new name can be specified).
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/> of the object to be updated.</param>
		/// <param name="name">The new value of <see cref="Member.Name"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangeName(int id, string name);

		/// <summary>
		/// Updates the <see cref="Member.Website"/> field (i.e. a new website can be specified).
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/> of the object to be updated.</param>
		/// <param name="website">The new value of <see cref="Member.Website"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangeWebsite(int id, string website);

		/// <summary>
		/// Updates the <see cref="Member.ChiefEditor"/> field (i.e. a new Chief Editor can be specified).
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/> of the object to be updated.</param>
		/// <param name="chiefEditor">The new value of <see cref="Member.ChiefEditor"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangeChiefEditor(int id, string chiefEditor);

		/// <summary>
		/// Updates the <see cref="Member.PhoneNumber"/> field (i.e. a new phone number can be specified).
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/> of the object to be updated.</param>
		/// <param name="phoneNumber">The new value of <see cref="Member.PhoneNumber"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangePhoneNumber(int id, string phoneNumber);

		/// <summary>
		/// Updates the <see cref="Member.Publisher"/> field (i.e. a new publisher can be specified).
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/> of the object to be updated.</param>
		/// <param name="publisher">The new value of <see cref="Member.Publisher"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangePublisher(int id, string publisher);

		/// <summary>
		/// Updates the <see cref="Member.OfficeLocation"/> field (i.e. a new office location (address) can be specified).
		/// </summary>
		/// <param name="id">The ID [<see cref="Member.Id"/>] of the <see cref="Member"/> record to be updated.</param>
		/// <param name="officeLocation">The new value of <see cref="Member.OfficeLocation"/> (i.e. new office location for the chosen record).</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool ChangeOfficeLocation(int id, string officeLocation);

		/// <summary>
		/// Updates the <see cref="Member.CountryId"/> and the <see cref="Member.OfficeLocation"/> of the only object.
		/// </summary>
		/// <param name="id">The value of <see cref="Member.Id"/> of the object to be updated.</param>
		/// <param name="countryId">The new value of <see cref="Member.CountryId"/>.</param>
		/// <param name="officeLocation">The new value of <see cref="Member.OfficeLocation"/>.</param>
		/// <returns>The value returned by <see cref="IRepository{T}.Update(T)"/> called by this method.</returns>
		bool Move(int id, int countryId, string officeLocation);
	}
}
