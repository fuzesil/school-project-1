using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QKNWZ1_HFT_2021221.Models;
using QKNWZ1_HFT_2021221.Repository;

namespace QKNWZ1_HFT_2021221.Logic
{
	/// <summary>
	/// Contains additional functions that work with tables: Members, Countries, ExpertGroups.
	/// </summary>
	public class InternalAuditLogic : IInternalLogic
	{
		/* private static InternalAuditLogic singletonIAL; */

		/// <summary>
		/// Initializes a new instance of the <see cref="InternalAuditLogic"/> class.
		/// </summary>
		/// <param name="members">An object for the <see cref="Member"/> repo.</param>
		/// <param name="countries">An object for the <see cref="Country"/> repo.</param>
		/// <param name="expertgroups">An object for the <see cref="ExpertGroup"/> repo.</param>
		/// <param name="topN">The value of <c>N</c> in <i>the top N of ...</i> queries. <br/> By default, N = 3.</param>
		public InternalAuditLogic(IMemberRepository members, ICountryRepository countries, IExpertGroupRepository expertgroups, int topN = 3)
		{
			this.TopN = topN;
			this.Members = members;
			this.Countries = countries;
			this.Expertgroups = expertgroups;
		}

		/*
		/// <summary>
		/// Initializes a new instance of the <see cref="InternalAuditLogic"/> class.
		/// </summary>
		/// <param name="members">An object for the <see cref="Member"/> repo.</param>
		/// <param name="countries">An object for the <see cref="Country"/> repo.</param>
		/// <param name="expertgroups">An object for the <see cref="ExpertGroup"/> repo.</param>
		public InternalAuditLogic(IMemberRepository members, ICountryRepository countries, IExpertGroupRepository expertgroups)
		{
			this.TopN = 3;
			this.Members = members;
			this.Countries = countries;
			this.Expertgroups = expertgroups;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InternalAuditLogic"/> class.
		/// </summary>
		/// <param name="topN">The value of 'N' in "the top N" queries.</param>
		/// <param name="members">The Members table.</param>
		/// <param name="countries">The Countries table.</param>
		/// <param name="expertgroups">The ExpertGroups table.</param>
		public InternalAuditLogic(int topN, IMemberRepository members, ICountryRepository countries, IExpertGroupRepository expertgroups)
			: base(topN, members, countries, expertgroups)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InternalAuditLogic"/> class.
		/// </summary>
		/// <param name="dbContext">The instance of the session to the database.</param>
		public InternalAuditLogic(ref Microsoft.EntityFrameworkCore.DbContext dbContext)
			: base(ref dbContext)
		{
		}

		/// <summary>
		/// Sets the properties for <see cref="InternalAuditLogic"/>.
		/// </summary>
		/// <exception cref="ArgumentNullException">Repository arguments mustn't be null.</exception>
		/// <exception cref="ArgumentException">Argument mustn't be less than 1.</exception>
		/// <param name="limiter">The value of 'N' in "the top N" queries.</param>
		/// <param name="memberRepository">The Members table.</param>
		/// <param name="countryRepository">The Countries table.</param>
		/// <param name="expertGroupRepository">The ExpertGroups table.</param>
		/// <returns>An instance of this class.</returns>
		public static InternalAuditLogic Initialize(
			int limiter = 1,
			IMemberRepository memberRepository = null,
			ICountryRepository countryRepository = null,
			IExpertGroupRepository expertGroupRepository = null)
		{
			if (limiter < 1)
			{
				throw new ArgumentException("This cannot be less than 1", nameof(limiter));
			}

			if (singletonIAL == null)
			{
				singletonIAL = new InternalAuditLogic(limiter, memberRepository, countryRepository, expertGroupRepository);
			}

			return singletonIAL;
		}
		*/

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

		/// <inheritdoc/>
		public Country GetOneCountry(int id) => id > 0 ? this.Countries.Read(id) : default;

		/// <inheritdoc/>
		public Country GetOneCountry(string name) => string.IsNullOrWhiteSpace(name) ? default : this.Countries.Read(name);

		/// <inheritdoc/>
		public ExpertGroup GetOneExpertGroup(int id) => id > 0 ? this.Expertgroups.Read(id) : default;

		/// <inheritdoc/>
		public ExpertGroup GetOneExpertGroup(string name) => string.IsNullOrWhiteSpace(name) ? default : this.Expertgroups.Read(name);

		/// <inheritdoc/>
		public Member GetOneMember(int id) => id > 0 ? this.Members.Read(id) : default;

		/// <inheritdoc/>
		public Member GetOneMember(string name) => string.IsNullOrWhiteSpace(name) ? default : this.Members.Read(name);

		/// <summary>
		/// Returns the average value of the PPP/C column in the <see cref="Country"/> table.
		/// </summary>
		/// <returns>Average of PPP/C column in <see cref="Country"/> table.</returns>
		public double GetAveragePPP() => this.Countries.ReadAll().Average(item => item.PPPperCapita);

		/// <inheritdoc/>
		public IEnumerable<Country> ListAllCountries(out int count)
		{
			var countries = this.Countries.ReadAll();
			count = countries.Count();
			return countries.ToList();
		}

		/// <inheritdoc/>
		public IEnumerable<ExpertGroup> ListAllExpertgroups(out int count)
		{
			var expertGroups = this.Expertgroups.ReadAll();
			count = expertGroups.Count();
			return expertGroups.ToList();
		}

		/// <inheritdoc/>
		public IEnumerable<Member> ListAllMembers(out int count)
		{
			var members = this.Members.ReadAll();
			count = members.Count();
			return members.ToList();
		}

		/// <inheritdoc/>
		public IEnumerable<Country> ListCountriesAboveAveragePPP(out int count)
		{
			var output = from country in this.Countries.ReadAll()
						 where country.PPPperCapita > this.GetAveragePPP()
						 orderby country.PPPperCapita
						 select country;
			count = output.Count();
			return output.ToList();
		}

		/// <inheritdoc/>
		public IEnumerable<MemberCountry> ListMembersInCapitalCity(bool isContained = true)
		{
			var q = from member in this.Members.ReadAll()
					join country in this.Countries.ReadAll() on member.CountryId equals country.Id
					where isContained ? member.OfficeLocation.Contains(country.CapitalCity) : !member.OfficeLocation.Contains(country.CapitalCity)
					select new MemberCountry { Member = member, Country = country };
			/*
			return this.Members.ReadAll()
				   .Join(this.Countries.ReadAll(), member => member.CountryID, country => country.CountryID, (member, country) => new MemberCountry { Member = member, Country = country })
				   .Where(mc => isContained ? mc.Member.OfficeLocation.Contains(mc.Country.CapitalCity)
											: !mc.Member.OfficeLocation.Contains(mc.Country.CapitalCity))
				   .ToList();
			*/
			return q.ToList();
		}

		/// <inheritdoc/>
		public Task<IEnumerable<MemberCountry>> ListMembersInCapitalCityAsync(bool isContained) => Task.Run(() => this.ListMembersInCapitalCity(isContained));

		/// <inheritdoc/>
		public Task<IEnumerable<ExpertgroupMemberCountry>> GetRichestMemberInExpertGroupAsync() => Task.Run(this.GetRichestMemberInExpertGroup);

		/// <inheritdoc/>
		public IEnumerable<Member> CountMembersInCountry(int id, out int count)
		{
			var output = this.Members.ReadAll().Where(member => member.CountryId == id);
			count = output.Count();
			return output;
		}

		/// <inheritdoc/>
		public IEnumerable<ExpertgroupMemberCountry> GetRichestMemberInExpertGroup()
		{
			/*return
				(from country in this.Countries.ReadAll()
				join member in this.Members.ReadAll() on country.Id equals member.CountryId
				let cm = new { country, member }
				group cm by cm.member.ExpertGroupId into cmGrp
				let cmGrpMax = new
				{
					egID = cmGrp.Key,
					MaxPPP = cmGrp.Max(cm => cm.country.PPPperCapita),
				}
				join country in this.Countries.ReadAll() on cmGrpMax.MaxPPP equals country.PPPperCapita
				join member in this.Members.ReadAll() on country.Id equals member.CountryId
				where member.ExpertGroupId == cmGrp.Key
				join expertgroup in this.Expertgroups.ReadAll() on member.ExpertGroupId equals expertgroup.Id
				select new ExpertgroupMemberCountry { Expertgroup = expertgroup, Member = member, Country = country })
				.ToList();*/

			var query =
				this.Countries.ReadAll()
				.Join(
					this.Members.ReadAll(),
					country => country.Id,
					member => member.CountryId,
					(country, member) => new { country, member })
				.GroupBy(cm => cm.member.ExpertGroupId)
				.Select(group => new
				{
					EGid = group.Key,
					MaxPPP = group.Max(cm => cm.country.PPPperCapita),
				})
				//// [countries + members] we have the EGid and the richest country's PPP/C by now
				.Join(
					this.Countries.ReadAll(),
					egID_maxPPP => egID_maxPPP.MaxPPP,
					country => country.PPPperCapita,
					(egID_maxPPP, country) => new { egID_maxPPP, country })
				.Where(countryjoin => countryjoin.country.PPPperCapita == countryjoin.egID_maxPPP.MaxPPP)
				//// [(egID + maxPPP) + countries] we have countries having the same PPP/C as the selected greatest from earlier, plus egID
				.Join(
					this.Members.ReadAll(),
					countryjoin => countryjoin.country.Id,
					member => member.CountryId,
					(group, member) => new { group, member })
				.Where(membergroup =>
					membergroup.member.ExpertGroupId == membergroup.group.egID_maxPPP.EGid
					&& membergroup.member.CountryId == membergroup.group.country.Id)
				//// && membergroup.member.Country.PPPperCapita == membergroup.group.MaxPPP)
				// now we're joined with members based on country (ID) and EG membership (ID)
				.Join(
					this.Expertgroups.ReadAll(),
					memgroup => memgroup.member.ExpertGroupId,
					eg => eg.Id,
					(memgroup, eg) =>
					new ExpertgroupMemberCountry
					{
						ExpertGroup = eg,
						Member = memgroup.member,
						Country = memgroup.group.country,
					});
			return query.ToList();
			/*
			IEnumerable<Data.Member> chosenMembers = this.Countries.ReadAll()
					 .Join(this.Members.ReadAll(), country => country.Id, member => member.CountryId, (country, member) => new { country, member })
					 .GroupBy(join => join.member.ExpertGroupId)
					 .Select(group => new { EGid = group.Key, MaxPPP = group.Max(item => item.country.PPPperCapita) })
					 .Join(this.Members.ReadAll(), group => group.EGid, member => member.ExpertGroupId, (group, member) => new { group, member })
					 .Where(trijoin => trijoin.member.ExpertGroupId == trijoin.group.EGid && trijoin.member.Country.PPPperCapita == trijoin.group.MaxPPP)
					 .Select(trijoin => trijoin.member);

			return this.Countries.ReadAll()
				 .Join(this.Members.ReadAll(), country => country.Id, member => member.CountryId, (country, member) => new { country, member })
				 .Join(this.Expertgroups.ReadAll(), join => join.member.ExpertGroupId, eg => eg.Id, (join, eg) => new { join, eg })
				 .Where(result => chosenMembers.Contains(result.join.member))
				 .Select(outtype => new ExpertgroupMemberCountry { Expertgroup = outtype.eg, Member = outtype.join.member, Country = outtype.join.country })
				 .ToList();
			*/
		}

		/*
		public int CountMembersInCapitalCity(bool isContained)
		{
			return this.Members.ReadAll()
				.Where(member => isContained
				? member.OfficeLocation.Contains(member.Country.CapitalCity)
				: !member.OfficeLocation.Contains(member.Country.CapitalCity))
				.Count();

			// members.ReadAll().Count() - GetMembersInCapitalCity().Count();
		}
		*/

		/*
		/// <summary>
		/// Returns the list of IDs and Counts of the top 'N' brands.
		/// </summary>
		/// <param name="count">The number of items in the returned ist.</param>
		/// <returns>List of IDs and Counts of select brands.</returns>
		public IEnumerable<NameAndValue> ListTopMemberRegions(out int count)
		{
			IEnumerable<int> countTopMemberRegion = this.Members.ReadAll()
				.GroupBy(member => member.CountryID)
				.Select(group => group.Count())
				.Distinct()
				.OrderByDescending(grpCnt => grpCnt)
				.Take(this.TopN)
				.ToList();

			IEnumerable<NameAndValue> output = this.Members.ReadAll()
				.GroupBy(member => member.CountryID)
				.Select(group => new { brandID = group.Key, Count = group.Count() })
				.Where(group => countTopMemberRegion.Contains(group.Count))
				.Join(this.Countries.ReadAll(), group => group.brandID, country => country.CountryID, (group, country) => new { country.Name, group.Count })
				.Select(inp => new NameAndValue { Name = inp.Name, Value = inp.Count })
				.OrderByDescending(inp => inp.Value)
				.ToList();

			count = output.Count();
			return output;
		}
		*/
	}
}
