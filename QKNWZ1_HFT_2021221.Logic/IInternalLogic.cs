namespace QKNWZ1_HFT_2021221.Logic
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Models;

	/// <summary>
	/// Declares read-only operations on <see cref="Country"/> and <see cref="Member"/> objects.
	/// </summary>
	public interface IInternalLogic : ICommonLogic
	{
		/// <summary>
		/// Returns the list of all records in the table.
		/// </summary>
		/// <param name="count">Number of records in the table.</param>
		/// <returns>List of all records in the table.</returns>
		IEnumerable<Member> ListAllMembers(out int count);

		/// <summary>Returns the one <see cref="Member"/> with the matching <see cref="Member.Id"/>.</summary>
		/// <param name="id">The primary key to search for, <see cref="Member.Id"/>.</param>
		/// <returns>One <see cref="Member"/> having the given <paramref name="id"/>.</returns>
		Member GetOneMember(int id);

		/// <summary>Returns the first <see cref="Member"/> with the matching <see cref="Member.Name"/>.</summary>
		/// <param name="name">The value to search for in <see cref="Member.Name"/>.</param>
		/// <returns>One <see cref="Member"/> having the given <paramref name="name"/>.</returns>
		Member GetOneMember(string name);

		/// <summary>
		/// Returns the list of all records in the table.
		/// </summary>
		/// <param name="count">Number of records in the table.</param>
		/// <returns>List of all records in the table.</returns>
		IEnumerable<Country> ListAllCountries(out int count);

		/// <summary>Returns the one <see cref="Country"/> with the matching <see cref="Country.Id"/>.</summary>
		/// <param name="id">The primary key to search for, <see cref="Country.Id"/>.</param>
		/// <returns>One <see cref="Country"/> having the given <paramref name="id"/>.</returns>
		Country GetOneCountry(int id);

		/// <summary>Returns the first <see cref="Country"/> with the matching <see cref="Country.Name"/>.</summary>
		/// <param name="name">The value to search for in <see cref="Country.Name"/>.</param>
		/// <returns>One <see cref="Country"/> having the given <paramref name="name"/>.</returns>
		Country GetOneCountry(string name);

		/// <summary>
		/// Returns the list of countries with values in the PPP/C column above the calculated average of that column.
		/// </summary>
		/// <param name="count">Number of elements in the returned list.</param>
		/// <returns>List of countries with PPP/C above average.</returns>
		IEnumerable<Country> ListCountriesAboveAveragePPP(out int count);

		/// <summary>
		/// Returns a sequence of <see cref="Member"/> objects that are either
		/// IN (if <paramref name="isContained"/> <c>==</c> <see langword="true"/>) or OUT OF the capital city of their country.
		/// </summary>
		/// <param name="isContained">Chooses between the selection method.
		/// <see langword="true"/> for seeking members <b>in</b> capital cities, or <see langword="false"/> for the opposite.</param>
		/// <returns>A sequence of <see cref="Member"/>s IN / OUT OF capital cities.</returns>
		IEnumerable<MemberCountry> ListMembersInCapitalCity(bool isContained);

		/// <summary>
		/// Asynchronous version of <see cref="ListMembersInCapitalCity(bool)"/>.
		/// </summary>
		/// <param name="isContained">Chooses between the selection method.
		/// 'True' for seeking members IN capital cities. 'False' for the opposite.</param>
		/// <returns>List of members IN / OUT OF capital cities.</returns>
		Task<IEnumerable<MemberCountry>> ListMembersInCapitalCityAsync(bool isContained);

		/// <summary>
		/// Asynchronous version of <see cref="GetRichestMemberInExpertGroup"/>.
		/// </summary>
		/// <returns>Call to the non-async method.</returns>
		Task<IEnumerable<ExpertgroupMemberCountry>> GetRichestMemberInExpertGroupAsync();

		/// <summary>
		/// Returns the list of members in the specified country.
		/// </summary>
		/// <param name="id">ID of the country of interest.</param>
		/// <param name="count">Number of elements in the returned list.</param>
		/// <returns>The sequence of member records that satisfy the country ID specified.</returns>
		IEnumerable<Member> CountMembersInCountry(int id, out int count);

		/// <summary>
		/// Returns the sequence of <see cref="ExpertgroupMemberCountry"/> elements
		/// where the member is the richest one (by its nation's PPP) in a particular expert group.
		/// </summary>
		/// <returns>The sequence of ExpertGroup - Member - Country triples where the member is the richest one in the group.</returns>
		IEnumerable<ExpertgroupMemberCountry> GetRichestMemberInExpertGroup();
	}
}
