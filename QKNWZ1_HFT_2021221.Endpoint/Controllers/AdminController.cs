using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QKNWZ1_HFT_2021221.Logic;
using QKNWZ1_HFT_2021221.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QKNWZ1_HFT_2021221.Endpoint.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private IAdministrator administrator;
		private IHubContext<SignalRHub> hub;
		public AdminController(IAdministrator administrator, IHubContext<SignalRHub> hub)
		{
			this.administrator = administrator ?? throw new System.ArgumentNullException(nameof(administrator));
			this.hub = hub;
		}

		/*
		// GET: api/<AdminController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<AdminController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<AdminController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<AdminController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<AdminController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
		*/

		// --- BRAND ---
		[HttpPost]
		public void PostBrand([FromBody] Brand brand)
		{
			this.administrator.CreateBrand(brand);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PostBrand), brand);
		}

		[HttpPut]
		public void PutBrand([FromBody] Brand brand)
		{
			this.administrator.UpdateBrand(brand);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PutBrand), brand);
		}

		[HttpDelete("{id}")]
		public void DeleteBrand(int id)
		{
			Brand brand = this.administrator.GetOneBrand(id);
			this.administrator.RemoveBrand(id);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(DeleteBrand), brand);
		}

		// --- Expertgroup ---

		[HttpPost]
		public void PostExpertGroup([FromBody] ExpertGroup eg)
		{
			this.administrator.CreateExpertgroup(eg);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PostExpertGroup), eg);
		}

		[HttpPut]
		public void PutExpertGroup([FromBody] ExpertGroup eg)
		{
			this.administrator.UpdateExpertgroup(eg);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PutExpertGroup), eg);
		}

		[HttpDelete("{id}")]
		public void DeleteExpertGroup(int id)
		{
			ExpertGroup eg = this.administrator.GetOneExpertGroup(id);
			this.administrator.RemoveExpertgroup(id);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(DeleteExpertGroup), eg);
		}

		// --- Member ---

		[HttpPost]
		public void PostMember([FromBody] Member member)
		{
			this.administrator.CreateMember(member);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PostMember), member);
		}

		[HttpPut]
		public void PutMember([FromBody] Member member)
		{
			this.administrator.UpdateMember(member);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PutMember), member);
		}

		[HttpDelete("{id}")]
		public void DeleteMember(int id)
		{
			Member member = this.administrator.GetOneMember(id);
			this.administrator.RemoveMember(id);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(DeleteMember), member);
		}

		// --- Country ---

		[HttpPost]
		public void PostCountry([FromBody] Country country)
		{
			this.administrator.CreateCountry(country);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PostCountry), country);
		}

		[HttpPut]
		public void PutCountry([FromBody] Country country)
		{
			this.administrator.UpdateCountry(country);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(PutCountry), country);
		}

		[HttpDelete("{id}")]
		public void DeleteCountry(int id)
		{
			Country country = this.administrator.GetOneCountry(id);
			this.administrator.RemoveCountry(id);
			this.administrator.SaveChanges();
			this.hub.Clients.All.SendAsync(nameof(DeleteCountry), country);
		}


		[HttpDelete("{id}")]
		public void RemoveBrand(int id) => this.administrator.RemoveBrand(id);

		[HttpDelete("{id}")]
		public void RemoveCountry(int id) => this.administrator.RemoveCountry(id);

		[HttpDelete("{id}")]
		public void RemoveExpertgroup(int id) => this.administrator.RemoveExpertgroup(id);

		[HttpDelete("{id}")]
		public void RemoveMember(int id) => this.administrator.RemoveMember(id);

		[HttpDelete("{id}")]
		public void RemoveProduct(int id) => this.administrator.RemoveProduct(id);

		[HttpPost]
		public void InsertBrand([FromBody] Brand brand) => this.administrator.InsertBrand(brand?.Name, brand.Address, brand.Homepage, brand.CountryId.ToString());

		[HttpPost]
		public void InsertCountry([FromBody] Country country) => this.administrator.InsertCountry(country?.Name, country.CapitalCity, country.CallingCode, country.PPPperCapita);

		[HttpPost]
		public void InsertEG([FromBody] ExpertGroup expertGroup) => this.administrator.InsertEG(expertGroup?.Name);

		[HttpPost]
		public void InsertMember([FromBody] Member member) => this.administrator.InsertMember(member?.ExpertGroupId.ToString(), member.Name, member.Website, member.Publisher, member.ChiefEditor, member.PhoneNumber, member.CountryId.ToString(), member.OfficeLocation);

		[HttpPost]
		public void InsertProduct([FromBody] Product product) => this.administrator.InsertProduct(product?.ExpertGroupId.ToString(), product.Category, product.Name, product.BrandId.ToString(), product.Price, product.LaunchDate, product.EstimatedLifetime);

		[HttpGet]
		public IEnumerable<MemberBrand> ListBrandsAndMembersAtSameAdress() => this.administrator.ListBrandsAndMembersAtSameAddress();

		[HttpPut]
		public void ChangeMemberName([FromBody] Member member) => this.administrator.ChangeMemberName(member?.Id ?? 0, member.Name);

		[HttpPut]
		public void ChangeProductPrice([FromBody] Product product) => this.administrator.ChangeProductPrice(product?.Id ?? 0, product.Price);

		[HttpPut]
		public void ChangeBrandHomePage([FromBody] Brand brand) => this.administrator.ChangeBrandHomePage(brand?.Id ?? 0, brand.Homepage);

		[HttpPut]
		public void ChangeEGName([FromBody] ExpertGroup expertGroup) => this.administrator.ChangeExpertgroupName(expertGroup?.Id ?? 0, expertGroup.Name);

		[HttpPut]
		public void ChangeCountryPPP([FromBody] Country country) => this.administrator.ChangeCountryPPP(country?.Id ?? 0, country.PPPperCapita);

		[HttpPut]
		public void UpdateExpertgroup([FromBody] ExpertGroup eg) => this.administrator.UpdateExpertgroup(eg);
	}
}
