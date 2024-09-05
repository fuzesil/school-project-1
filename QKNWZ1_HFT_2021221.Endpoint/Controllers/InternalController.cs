using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QKNWZ1_HFT_2021221.Logic;
using QKNWZ1_HFT_2021221.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QKNWZ1_HFT_2021221.Endpoint.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class InternalController : ControllerBase
	{
		public InternalController(IInternalLogic internalLogic) => this.internalLogic = internalLogic ?? throw new System.ArgumentNullException(nameof(internalLogic));
		public IInternalLogic internalLogic { get; set; }

		[HttpGet]
		public IEnumerable<Member> GetMemberTable() => this.internalLogic.ListAllMembers(out int _);

		[HttpGet("{id:int}")]
		public Member GetOneMember(int id) => this.internalLogic.GetOneMember(id);

		[HttpGet("{name:alpha}")]
		public Member GetOneMember(string name) => this.internalLogic.GetOneMember(name);

		[HttpGet]
		public IEnumerable<Country> GetCountryTable() => this.internalLogic.ListAllCountries(out int _);

		[HttpGet("{id:int}")]
		public Country GetOneCountry(int id) => this.internalLogic.GetOneCountry(id);

		[HttpGet("{name:alpha}")]
		public Country GetOneCountry(string name) => this.internalLogic.GetOneCountry(name);

		[HttpGet]
		public IEnumerable<Country> ListCountriesAboveAveragePPP() => this.internalLogic.ListCountriesAboveAveragePPP(out int _);

		[HttpGet("{isContained:bool}")]
		public IEnumerable<MemberCountry> ListMembersInCapitalCity(bool isContained) => this.internalLogic.ListMembersInCapitalCity(isContained);

		[HttpGet("{id:int}")]
		public IEnumerable<Member> CountMembersInCountry(int id) => this.internalLogic.CountMembersInCountry(id, out int _);

		[HttpGet]
		public IEnumerable<ExpertgroupMemberCountry> GetRichestMemberInExpertGroup() => this.internalLogic.GetRichestMemberInExpertGroup();

		[HttpGet]
		public IEnumerable<ExpertGroup> GetExpertgroupTable() => this.internalLogic.ListAllExpertgroups(out int _);

		[HttpGet("{id:int}")]
		public ExpertGroup GetOneExpertGroup(int id) => this.internalLogic.GetOneExpertGroup(id);

		[HttpGet("{name:alpha}")]
		public ExpertGroup GetOneExpertGroup(string name) => this.internalLogic.GetOneExpertGroup(name);
	}
}
