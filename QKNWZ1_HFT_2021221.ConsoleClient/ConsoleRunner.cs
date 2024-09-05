using System;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.ConsoleClient
{
	/// <summary>
	/// Acts as a bridge between the business logic and <see cref="Console"/>.
	/// </summary>
	public class ConsoleRunner
	{
		private static ConsoleRunner _singletonConsoleRunner;
		private readonly RestService restService;

		private ConsoleRunner(RestService restService)
		{
			this.restService = restService;
			/*
			this.adminLogic = new Logic.AdminLogic(ref dbContext);
			this.adminLogic = new Logic.AdminLogic(
				new BrandRepository(dbContext),
				new CountryRepository(dbContext),
				new ExpertGroupRepository(dbContext),
				new MemberRepository(dbContext),
				new ProductRepository(dbContext));
			*/
		}

		/// <summary>
		/// Initialises the singleton instance of the <see cref="ConsoleRunner"/> helper class.
		/// </summary>
		/// <param name="restService">.</param>
		/// <returns>The only one instance of this class.</returns>
		public static ConsoleRunner Initialize(RestService restService)
		{
			return restService is null
				? throw new ArgumentNullException(nameof(restService))
				: _singletonConsoleRunner ??= new ConsoleRunner(restService);
		}

		/// <summary>
		/// Prints Brands and Members at similar places.
		/// </summary>
		public void ListBrandsAndMembersAtSameAddress()
		{
			//this.adminLogic.ListBrandsAndMembersAtSameAddress()
			this.restService.GetMultiple<MemberBrand>("Admin/ListBrandsAndMembersAtSameAddress")
				.PrintToConsole("Brands and Members in close proximity");
		}

		/*
		/// <summary>
		/// Asynchronously prints Brands and Members at similar places.
		/// </summary>
		public static void ListBrandsAndMembersAtSameAddressAsync()
		{
			//this.adminLogic.ListBrandsAndMembersAtSameAddressAsync().Result.PrintToConsole("Brands and Members in close proximity");
			Console.WriteLine("Out of order.");
		}
		*/

		/// <summary>
		/// Adds a new brand to the 'Brands' table.
		/// </summary>
		public void AddNewBrand()
		{
			string[] input = { "name", "country", "address", "homepage" };
			for (var i = 0; i < input.Length; i++)
			{
				Console.WriteLine("Input the required data for " + input[i]);
				input[i] = Console.ReadLine();
			}
			var brand = new Brand
			{
				Name = input[0],
				CountryId = int.Parse(input[1], System.Globalization.NumberFormatInfo.InvariantInfo),
				Address = input[2],
				Homepage = input[3],
			};
			//this.adminLogic.InsertBrand(input[0], input[1], input[2], input[3]);
			this.restService.Post(brand, "Admin/CreateBrand");
		}

		/// <summary>
		/// Adds a new country to the 'Countries' table.
		/// </summary>
		public void AddNewCountry()
		{
			string[] input = { "name", "capital city", "calling code", "PPP/C in USD", };
			for (var i = 0; i < input.Length; i++)
			{
				Console.WriteLine("Input the required data for " + input[i]);
				input[i] = Console.ReadLine();
			}

			var country = new Country
			{
				Name = input[0],
				CapitalCity = input[1],
				CallingCode = int.Parse(input[2], System.Globalization.NumberFormatInfo.InvariantInfo),
				PPPperCapita = int.Parse(input[3], System.Globalization.NumberFormatInfo.InvariantInfo),
			};
			this.restService.Post(country, "Admin/CreateCountry");
		}

		/// <summary>
		/// Adds a new expert group to the 'ExpertGroups' table.
		/// </summary>
		public void AddNewEG()
		{
			string name;
			Console.WriteLine($"Input the required data for {nameof(name)}");
			name = Console.ReadLine();
			this.restService.Post(new ExpertGroup { Name = name }, "Admin/CreateExpertGroup");
		}

		/// <summary>
		/// Adds a new member to the 'Members' table.
		/// </summary>
		public void AddNewMember()
		{
			string[] input = { "name", "website", "publisher", "editor", "phone number without +/00/country code", "office", };
			for (var i = 0; i < input.Length; i++)
			{
				Console.WriteLine("Input the required data for " + input[i]);
				input[i] = Console.ReadLine();
			}
			this.restService.Post(
				new Member { Name = input[0], Website = input[1], Publisher = input[2], ChiefEditor = input[3], PhoneNumber = input[4], OfficeLocation = input[5], },
				"Admin/CreateMember");
			//this.adminLogic.InsertMember(input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7]);
		}

		/// <summary>
		/// Adds a new product to the 'Products' table.
		/// </summary>
		public void AddNewProduct()
		{
			string[] input = { "name", "Category (award)", "price in USD", "estimated lifetime in Years, rounded", "date of launch YYYY-MM-DD", };
			for (var i = 0; i < input.Length; i++)
			{
				Console.WriteLine("Input the required data for " + input[i]);
				input[i] = Console.ReadLine();
			}
			this.restService.Post(
				new Product
				{
					Name = input[0],
					Category = input[1],
					Price = int.Parse(input[2], System.Globalization.NumberFormatInfo.InvariantInfo),
					EstimatedLifetime = int.Parse(input[3], System.Globalization.NumberFormatInfo.InvariantInfo),
					LaunchDate = DateTime.Parse(input[4], System.Globalization.NumberFormatInfo.InvariantInfo),
				},
				"Admin/CreateProduct");
			/* this.adminLogic.InsertProduct(
							input[0],
							input[1],
							int.Parse(input[2], System.Globalization.NumberFormatInfo.InvariantInfo),
							int.Parse(input[3], System.Globalization.NumberFormatInfo.InvariantInfo),
							DateTime.Parse(input[4], System.Globalization.NumberFormatInfo.InvariantInfo),
							input[5],
							input[6]);
			*/
		}

		/// <summary>
		/// Remove a brand from the 'Brands' table.
		/// </summary>
		public void RemoveBrand()
		{
			Console.WriteLine("Input the ID of the brand to be removed.");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					this.restService.Delete(id, "Admin/RemoveBrand");
				}
				else
				{
					Console.WriteLine("ID cannot be parsed.");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Remove a country from the 'Countries' table.
		/// </summary>
		public void RemoveCountry()
		{
			Console.WriteLine("Input the ID of the country to be removed.");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					this.restService.Delete(id, "Admin/RemoveCountry");
				}
				else
				{
					Console.WriteLine("ID cannot be parsed.");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Removes an expert group from the 'ExpertGroups' table.
		/// </summary>
		public void RemoveEG()
		{
			Console.WriteLine("Input the ID of the expert group to be removed.");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					this.restService.Delete(id, "Admin/RemoveExpertgroup");
				}
				else
				{
					Console.WriteLine("ID cannot be parsed.");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Remove a member from the 'Members' table.
		/// </summary>
		public void RemoveMember()
		{
			Console.WriteLine("Input the ID of the member to be removed.");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					this.restService.Delete(id, "Admin/RemoveMember");
				}
				else
				{
					Console.WriteLine("ID cannot be parsed.");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Removes a product from the 'Products' table.
		/// </summary>
		public void RemoveProduct()
		{
			Console.WriteLine("Input the ID of the product to be removed.");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					this.restService.Delete(id, "Admin/RemoveProduct");
				}
				else
				{
					Console.WriteLine("ID cannot be parsed.");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Updates the name of the selected ExpertGroup record.
		/// </summary>
		public void ChangeEGName()
		{
			Console.WriteLine("\nEnter ID :");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					Console.WriteLine("\nEnter new Name :");
					userInput = Console.ReadLine();
					ExpertGroup expertGroup = new() { Id = id, Name = userInput };
					//this.adminLogic.ChangeEGName(id, userInput);
					this.restService.Put(expertGroup, "Admin/ChangeEGName");
				}
				else
				{
					Console.WriteLine("ID parsing failed!\t(Press a key.)");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Updates the web home page of the selected Brand record.
		/// </summary>
		public void ChangeBrandHomePage()
		{
			Console.WriteLine("\nEnter ID :");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					Console.WriteLine("\nEnter new web Home Page :");
					userInput = Console.ReadLine();
					//this.adminLogic.ChangeBrandHomePage(id, userParam);
					Brand brand = new() { Id = id, Homepage = userInput };
					this.restService.Put(brand, "Admin/ChangeBrandHomepage");
				}
				else
				{
					Console.WriteLine("ID parsing failed!\t(Press a key.)");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Updates the PPP/C for the selected Country record.
		/// </summary>
		public void ChangeCountryPPP()
		{
			Console.WriteLine("\nEnter ID :");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					Console.WriteLine("\nEnter new Price :");
					userInput = Console.ReadLine();
					if (int.TryParse(userInput, out var newPPP))
					{
						//this.adminLogic.ChangeCountryPPP(id, newPPP);
						Country country = new() { Id = id, PPPperCapita = newPPP };
						this.restService.Put(country, "Admin/ChangeCountryPPP");
					}
					else
					{
						Console.WriteLine("New price parsing failed!\t(Press a key.)");
					}
				}
				else
				{
					Console.WriteLine("ID parsing failed!\t(Press a key.)");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Updates the new name of the selected Member record.
		/// </summary>
		public void ChangeMemberName()
		{
			Console.WriteLine("\nEnter ID :");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					Console.WriteLine("\nEnter new Name :");
					userInput = Console.ReadLine();
					//this.adminLogic.ChangeMemberName(id, userInput);
					Member member = new() { Id = id, Name = userInput, };
					this.restService.Put(member, "Admin/ChangeMemberName");
				}
				else
				{
					Console.WriteLine("ID parsing failed!\t(Press a key.)");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Updates the price of a given product.
		/// </summary>
		public void ChangeProductPrice()
		{
			Console.WriteLine("\nEnter ID :");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					Console.WriteLine("\nEnter new Price :");
					userInput = Console.ReadLine();
					if (int.TryParse(userInput, out var newPrice))
					{
						//this.adminLogic.ChangeProductPrice(id, newPrice);
						Product product = new() { Id = id, Price = newPrice, };
						this.restService.Put(product, "Admin/ChangeProductPrice");
					}
					else
					{
						Console.WriteLine("New price parsing failed!\t(Press a key.)");
					}
				}
				else
				{
					Console.WriteLine("ID parsing failed!\t(Press a key.)");
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/*
		/// <summary>
		/// Gets the singleton instance of the class and calls the constructor.
		/// </summary>
		/// <param name="context">Database context needed to initialise logic classes.</param>
		/// <returns>A <see cref="AuditWorker"/> instance.</returns>
		public static AuditWorker Initialize(Microsoft.EntityFrameworkCore.DbContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (auditWorker is null)
			{
				auditWorker = new AuditWorker(ref context);
			}

			return auditWorker;
		}
		*/

		/// <summary>
		/// Prints a list of members that are/aren't located in a capital city (separately) and the numbers of each.
		/// </summary>
		public void MembersInCapitals()
		{
			this.restService.GetMultiple<Member>("Admin/ListMembersInCapitalCity/true").PrintToConsole("Members IN a capital city");
			this.restService.GetMultiple<Member>("Admin/ListMembersInCapitalCity/false").PrintToConsole("Members IN a capital city");
		}

		/*
		/// <summary>
		/// Asynchronously prints a list of members that are/aren't located in a capital city (separately) and the numbers of each.
		/// </summary>
		public void MembersInCapitalsAsync()
		{
			this.internalAudit.ListMembersInCapitalCityAsync(true).Result.PrintToConsole("Members IN a capital city");
			this.internalAudit.ListMembersInCapitalCityAsync(false).Result.PrintToConsole("Members OUT OF a capital city");
		}
		*/

		/// <summary>
		/// Prints the average Purchase Price Parity / Capita and the above-average countries.
		/// </summary>
		public void AverageCountries()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("The average of PPP/C is ");
			Console.ResetColor();
			Console.WriteLine(this.restService.GetSingle<int>("Admin/GetAveragePPP"));
			this.restService.GetMultiple<Country>("Admin/ListCountriesAboveAveragePPP").PrintToConsole($"Countries with above-average PPP/C", true);
		}

		/// <summary>
		/// Prints the top 'N' most awarded brands and their (awarded) products.
		/// </summary>
		public void ListTopBrandsAndProducts()
		{
			this.restService.GetMultiple<Brand>("External/ListTopBrands").PrintToConsole("Top 3 brands");
			this.restService.GetMultiple<BrandWithAwards>("External/GetProductsByBrandId").PrintToConsole("The top brands and their products", true);
			/*
			string userInput = Console.ReadLine();
			if (int.TryParse(userInput, out int topn))
			{
				this.externalAudit.TopN = topn;
				this.externalAudit.ListTopBrands().PrintToConsole("(Name of brand, Count of awards)");
				this.externalAudit.GetProductsByBrandId(out int count).PrintToConsole($"The {count} most awarded brands and their products");
			}
			else
			{
				Console.WriteLine("Rank parsing failed!\t(Press a key.)");
				Console.ReadKey();
			}
			*/
		}

		/*
		/// <summary>
		/// Asynchronously prints the top 'N' most awarded brands.
		/// </summary>
		public void ListTopBrandsAsync()
		{
			Console.WriteLine("What should be the rank 'N' as in 'the top N brands' ?");
			string userInput = Console.ReadLine();
			if (int.TryParse(userInput, out int topn))
			{
				this.externalAudit.TopN = topn;
				this.externalAudit.ListTopBrandsAsync().Result.PrintToConsole("(Name of brand, Count of awards)");
			}
			else
			{
				Console.WriteLine("Rank parsing failed!\t(Press a key.)");
				Console.ReadKey();
			}
		}
		*/

		/// <summary>
		/// Prints the 'Brands' table.
		/// </summary>
		public void ListAllBrands()
		{
			//this.externalAudit.ListAllBrands(out int count).PrintToConsole($"{count} records in the 'BRANDS' table");
			this.restService.GetMultiple<Brand>("External/ListAllBrands").PrintToConsole("All Brands", true);
		}

		/// <summary>
		/// Print the 'Countries' table.
		/// </summary>
		public void ListAllCountries()
		{
			//this.internalAudit.ListAllCountries(out int count).PrintToConsole($"{count} records in 'Countries' table");
			this.restService.GetMultiple<Country>("Internal/ListAllCountries").PrintToConsole("All Countries", true);
		}

		/// <summary>
		/// Print the 'ExpertGroups' table.
		/// </summary>
		public void ListAllExpertgroupsInternally()
		{
			//this.internalAudit.ListAllExpertgroups(out int count).PrintToConsole($"{count} records in 'ExpertGroups' table");
			this.restService.GetMultiple<ExpertGroup>("Internal/ListAllExpertgroups").PrintToConsole("All Expert Groups", true);
		}

		/// <summary>
		/// Print the 'ExpertGroups' table.
		/// </summary>
		public void ListAllExpertgroupsExternally() => this.restService
			.GetMultiple<ExpertGroup>("External/ListAllExpertgroups")
			.PrintToConsole("All Expert Groups", true);

		/// <summary>
		/// Print the 'Members' table.
		/// </summary>
		public void ListAllMembers() => this.restService
			.GetMultiple<Member>("Internal/ListAllMembers")
			.PrintToConsole("All Members", true);

		/// <summary>
		/// Prints the 'Products' table.
		/// </summary>
		public void ListAllProducts() => this.restService
			.GetMultiple<Product>("External/ListAllProducts")
			.PrintToConsole("All Products", true);

		/// <summary>
		/// Fetches one record from the 'Brands' table.
		/// </summary>
		public void FetchOneBrand()
		{
			Console.WriteLine("\nEnter Brand ID or Name :");
			var userInput = Console.ReadLine();
			try
			{
				Console.WriteLine(int.TryParse(userInput, out var id)
					? this.restService.GetById<Brand>(id, "Internal/GetOneBrand")
					: this.restService.GetSingle<Brand>($"Internal/GetOneBrand/0/{userInput}")
				);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			_ = Console.ReadKey();
		}

		/// <summary>
		/// Fetches one record from the 'Countries' table.
		/// </summary>
		public void FetchOneCountry()
		{
			Console.WriteLine("\nEnter ID or Name :");
			var userInput = Console.ReadLine();
			try
			{
				Console.WriteLine(int.TryParse(userInput, out var id)
					? this.restService.GetById<Country>(id, "Internal/GetOneCountry")
					: this.restService.GetSingle<Country>($"Internal/GetOneCountry/0/{userInput}")
				);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Fetches one record from the 'ExpertGroups' table.
		/// </summary>
		public void FetchOneEGExternally()
		{
			Console.WriteLine("\nEnter ID or Name :");
			var userInput = Console.ReadLine();
			try
			{
				Console.WriteLine(int.TryParse(userInput, out var id)
					? this.restService.GetById<ExpertGroup>(id, "External/GetOneExpertgroup")
					: this.restService.GetSingle<ExpertGroup>($"External/GetOneExpertroup/{userInput}")
				);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Fetches one record from the 'ExpertGroups' table.
		/// </summary>
		public void FetchOneEGInternally()
		{
			Console.WriteLine("\nEnter ID or Name :");
			var userInput = Console.ReadLine();
			try
			{
				Console.WriteLine(int.TryParse(userInput, out var id)
					? this.restService.GetById<ExpertGroup>(id, "Internal/GetOneExpertgroup")
					: this.restService.GetSingle<ExpertGroup>($"Internal/GetOneExpertgroup/{userInput}")
				);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Fetches one record from the 'Members' table.
		/// </summary>
		public void FetchOneMember()
		{
			Console.WriteLine("\nEnter ID or Name :");
			var userInput = Console.ReadLine();
			try
			{
				Console.WriteLine(int.TryParse(userInput, out var id)
					? this.restService.GetById<Member>(id, "Internal/GetOneMember")
					: this.restService.GetSingle<Member>($"Internal/GetOneMember/0/{userInput}")
				);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Fetches one record from the 'Products' table.
		/// </summary>
		public void FetchOneProduct()
		{
			Console.WriteLine("\nEnter ID or Name :");
			var userInput = Console.ReadLine();
			try
			{
				Console.WriteLine(int.TryParse(userInput, out var id)
					? this.restService.GetById<Product>(id, "External/GetOneProduct")
					: this.restService.GetSingle<Product>($"External/GetOneProduct/0/{userInput}")
				);
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Prints the list of members in a given country.
		/// </summary>
		public void CountMembersInCountry()
		{
			Console.WriteLine("Enter the ID or Name of the Country of interest:");
			var userInput = Console.ReadLine();
			try
			{
				if (int.TryParse(userInput, out var id))
				{
					this.restService
						.GetMultiple<Member>($"Internal/CountMembersInCountry/{id}")
						.PrintToConsole("Members in that Country", true);
				}
				else
				{
					Console.WriteLine("Please enter an integer.");
					/*
					var temp = this.internalAudit.GetOneCountry(-1, userInput);
					this.internalAudit.CountMembersInCountry(temp.Id, out int count).PrintToConsole($"{count} members in {temp.Name}");
					*/
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
		}

		/// <summary>
		/// Prints the list of members in a given expert group.
		/// </summary>
		public void GetRichestMemberInExpertGroup() =>
			this.restService.GetMultiple<ExpertgroupMemberCountry>("Internal/GetRichestMemberInExpertGroup")
			.PrintToConsole("Richest members in each expert group");

		/*
		/// <summary>
		/// Asynchronously prints the list of members in a given expert group.
		/// </summary>
		public void GetRichestMemberInExpertGroupAsync()
		{
			this.internalAudit.GetRichestMemberInExpertGroupAsync().Result.PrintToConsole($"Richest members in expert group");
		}
		*/

		/// <summary>
		/// Prints the most expensive product for each expert group.
		/// </summary>
		public void MaxPriceProductInEveryEG() =>
			this.restService.GetMultiple<ExpertgroupProduct>("External/GetMaxPriceProdInEveryEG")
			.PrintToConsole("Max Price in Each Category");

		/*
		/// <summary>
		/// Asynchronously prints the most expensive product for each expert group.
		/// </summary>
		public void GetMaxPriceProdInEveryEGAsync()
		{
			this.externalAudit.GetMaxPriceProdInEveryEGAsync().Result.PrintToConsole("Max Price in Each Category");
		}*/
	}
}
