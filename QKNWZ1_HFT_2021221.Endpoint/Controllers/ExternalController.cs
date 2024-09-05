using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QKNWZ1_HFT_2021221.Logic;
using QKNWZ1_HFT_2021221.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QKNWZ1_HFT_2021221.Endpoint.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class ExternalController : ControllerBase
	{
		public ExternalController(IExternalLogic externalLogic) => this.externalLogic = externalLogic ?? throw new System.ArgumentNullException(nameof(externalLogic));
		public IExternalLogic externalLogic { get; set; }

		[HttpGet]
		public IEnumerable<Brand> GetBrandTable() => this.externalLogic.ListAllBrands(out int _);

		[HttpGet("{id:int}")]
		public Brand GetOneBrand(int id) => this.externalLogic.GetOneBrand(id);

		[HttpGet("{name:alpha}")]
		public Brand GetOneBrand(string name) => this.externalLogic.GetOneBrand(name);

		[HttpGet]
		public IEnumerable<Product> GetProductTable() => this.externalLogic.ListAllProducts(out int _);

		[HttpGet("{id:int}")]
		public Product GetOneProduct(int id) => this.externalLogic.GetOneProduct(id);

		[HttpGet("{name:alpha}")]
		public Product GetOneProduct(string name) => this.externalLogic.GetOneProduct(name);

		[HttpGet]
		public IEnumerable<BrandAndNumber> ListTopBrands() => this.externalLogic.ListTopBrands();

		[HttpGet]
		public IEnumerable<BrandWithAwards> GetProductsByBrandId() => this.externalLogic.GetProductsByBrandId(out int _);

		[HttpGet]
		public IEnumerable<ExpertgroupProduct> GetMaxPriceProdInEveryEG() => this.externalLogic.GetMaxPriceProdInEveryEG();

		[HttpGet]
		public IEnumerable<ExpertGroup> GetExpertGroupTable() => this.externalLogic.ListAllExpertgroups(out int _);

		[HttpGet("{id:int}")]
		public ExpertGroup GetOneExpertGroup(int id) => this.externalLogic.GetOneExpertGroup(id);

		[HttpGet("{name:alpha}")]
		public ExpertGroup GetOneExpertGroup(string name) => this.externalLogic.GetOneExpertGroup(name);
	}
}
