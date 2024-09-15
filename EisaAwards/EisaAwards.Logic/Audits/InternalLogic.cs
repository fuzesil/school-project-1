namespace EisaAwards.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EisaAwards.Data;
    using EisaAwards.Repository;

    /// <summary>
    /// Contains functions that work with tables: <see cref="Member"/>, <see cref="Country"/>, <see cref="ExpertGroup"/>.
    /// </summary>
    public abstract class InternalLogic : IReadInternalData, IReadCommonData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLogic"/> class.
        /// </summary>
        /// <param name="topN">The value of 'N' in "the top N of ..." queries.</param>
        /// <param name="members">An object for the <see cref="Member"/> repo.</param>
        /// <param name="countries">An object for the <see cref="Country"/> repo.</param>
        /// <param name="expertgroups">An object for the <see cref="ExpertGroup"/> repo.</param>
        protected InternalLogic(int topN, IMemberRepository members, ICountryRepository countries, IExpertGroupRepository expertgroups)
        {
            this.TopN = topN;
            this.Members = members;
            this.Countries = countries;
            this.Expertgroups = expertgroups;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalLogic"/> class.
        /// </summary>
        /// <param name="dbContext">The instance of the session to the database.</param>
        protected InternalLogic(ref Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            this.Countries = new CountryRepository(ref dbContext);
            this.Expertgroups = new ExpertGroupRepository(ref dbContext);
            this.Members = new MemberRepository(ref dbContext);
        }

        /// <summary>
        /// Gets or Sets the value of 'N'.
        /// </summary>
        public int TopN { get; set; }

        /// <summary>
        /// Gets the <see cref="Member"/> repository instance.
        /// </summary>
        public IMemberRepository Members { get; private set; }

        /// <summary>
        /// Gets the <see cref="Country"/> repository instance.
        /// </summary>
        public ICountryRepository Countries { get; private set; }

        /// <summary>
        /// Gets the <see cref="ExpertGroup"/> repository instance.
        /// </summary>
        public IExpertGroupRepository Expertgroups { get; private set; }

        /// <summary>
        /// Returns the average value of the PPP/C column in the <see cref="Country"/> table.
        /// </summary>
        /// <returns>Average of PPP/C column in <see cref="Country"/> table.</returns>
        public double GetAveragePPP()
        {
            return this.Countries.GetAll().Average(item => item.PPPperCapita);
        }

        /// <inheritdoc/>
        public IEnumerable<Country> ListAllCountries(out int count)
        {
            IQueryable<Country> output = this.Countries.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<ExpertGroup> ListAllExpertgroups(out int count)
        {
            IQueryable<ExpertGroup> output = this.Expertgroups.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Member> ListAllMembers(out int count)
        {
            IQueryable<Member> output = this.Members.GetAll();
            count = output.Count();
            return output.ToList();
        }

        /// <inheritdoc/>
        public Country GetOneCountry(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Countries.GetOne(id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Countries.GetOne(name);
            }

            throw new System.InvalidOperationException("Neither the ID nor the Name was valid.");
        }

        /// <inheritdoc/>
        public ExpertGroup GetOneExpertGroup(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Expertgroups.GetOne(id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Expertgroups.GetOne(name);
            }

            throw new System.InvalidOperationException("Neither the ID nor the Name was valid.");
        }

        /// <inheritdoc/>
        public Member GetOneMember(int id, string name = "")
        {
            if (id > 0)
            {
                return this.Members.GetOne(id);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                return this.Members.GetOne(name);
            }

            throw new System.InvalidOperationException("Neither the ID nor the Name was vaid.");
        }

        /// <summary>
        /// Returns the list of countries with values in the PPP/C column above the calculated average of that column.
        /// </summary>
        /// <param name="count">Number of elements in the returned list.</param>
        /// <returns>List of countries with PPP/C above average.</returns>
        public abstract IEnumerable<Country> ListCountriesAboveAveragePPP(out int count);

        /// <summary>
        /// Returns a list of countries that are either IN (if <paramref name="isContained"/> = true) or OUT OF the capital city of their country.
        /// </summary>
        /// <param name="isContained">Chooses between the selection method.
        /// 'True' for seeking members IN capital cities. 'False' for the opposite.</param>
        /// <returns>List of members IN / OUT OF capital cities.</returns>
        public abstract IEnumerable<MemberCountry> ListMembersInCapitalCity(bool isContained);

        /// <summary>
        /// Asynchronous version of <see cref="ListMembersInCapitalCity(bool)"/>.
        /// </summary>
        /// <param name="isContained">Chooses between the selection method.
        /// 'True' for seeking members IN capital cities. 'False' for the opposite.</param>
        /// <returns>List of members IN / OUT OF capital cities.</returns>
        public abstract Task<IEnumerable<MemberCountry>> ListMembersInCapitalCityAsync(bool isContained);

        /// <summary>
        /// Asynchronous version of <see cref="GetRichestMemberInExpertGroup"/>.
        /// </summary>
        /// <returns>Call to the non-async method.</returns>
        public abstract Task<IEnumerable<ExpertgroupMemberCountry>> GetRichestMemberInExpertGroupAsync();

        /// <summary>
        /// Returns the list of members in the specified country.
        /// </summary>
        /// <param name="id">ID of the country of interest.</param>
        /// <param name="count">Number of elements in the returned list.</param>
        /// <returns>The sequence of member records that satisfy the country ID specified.</returns>
        public abstract IEnumerable<Member> CountMembersInCountry(int id, out int count);

        /// <summary>
        /// Returns the sequence of <see cref="ExpertgroupMemberCountry"/> elements
        /// where the member is the richest one (by its nation's PPP) in a particular expert group.
        /// </summary>
        /// <returns>The sequence of ExpertGroup - Member - Country triples where the member is the richest one in the group.</returns>
        public abstract IEnumerable<ExpertgroupMemberCountry> GetRichestMemberInExpertGroup();
    }
}
