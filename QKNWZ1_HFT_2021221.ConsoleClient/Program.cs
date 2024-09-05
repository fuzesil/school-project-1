using System;

[assembly: CLSCompliant(false)]
namespace QKNWZ1_HFT_2021221.ConsoleClient
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.Title = "EISA Awards Auditing Application - by QKNWZ1";
			Console.WriteLine("** Welcome! **\nPlease wait, the initialisation process has begun.");
			System.Threading.Thread.Sleep(7000);

			var restService = new RestService("http://localhost:49943");

			var consoleRunner = ConsoleRunner.Initialize(restService);

			ConsoleTools.ConsoleMenu adminMenu = new ConsoleTools.ConsoleMenu(args, 1)
				 .Add("Add a new brand to the 'Brands' table.", consoleRunner.AddNewBrand)
				 .Add("Add a new country to the 'Countries' table.", consoleRunner.AddNewCountry)
				 .Add("Add a new expert group to the 'ExpertGroups' table.", consoleRunner.AddNewEG)
				 .Add("Add a new member to the 'Members' table.", consoleRunner.AddNewMember)
				 .Add("Add a new product to the 'Products' table.", consoleRunner.AddNewProduct)
				 .Add("Remove a brand from the 'Brands' table.", consoleRunner.RemoveBrand)
				 .Add("Remove a country from the 'Countries' table.", consoleRunner.RemoveCountry)
				 .Add("Remove an expert group from the 'ExpertGroups' table.", consoleRunner.RemoveEG)
				 .Add("Remove a member from the 'Members' table.", consoleRunner.RemoveMember)
				 .Add("Remove a product from the 'Products' table.", consoleRunner.RemoveProduct)
				 .Add("Change the web home page of a selected Brand", consoleRunner.ChangeBrandHomePage)
				 .Add("Change the PPP/C of a selected Country", consoleRunner.ChangeCountryPPP)
				 .Add("Change the name of a selected ExpertGroup", consoleRunner.ChangeEGName)
				 .Add("Change the name of a selected Member", consoleRunner.ChangeMemberName)
				 .Add("Change the price of a selected product", consoleRunner.ChangeProductPrice)
				 .Add(" - Show brands and members that are located close to each other.", consoleRunner.ListBrandsAndMembersAtSameAddress)
				 .Add("<< Main menu", ConsoleTools.ConsoleMenu.Close)
				 .Configure(config =>
				 {
					 //config.Selector = "--> ";
					 config.EnableFilter = true;
					 config.EnableWriteTitle = true;
					 config.Title = "EISA Database Administrators' menu";
				 });
			ConsoleTools.ConsoleMenu internalauditMenu = new ConsoleTools.ConsoleMenu(args, 1)
				.Add("Print the 'Countries' table.", consoleRunner.ListAllCountries)
				.Add("Fetch one record from the 'Countries' table.", consoleRunner.FetchOneCountry)
				.Add("Print the 'ExpertGroups' table.", consoleRunner.ListAllExpertgroupsInternally)
				.Add("Fetch one record from the 'ExpertGroups' table.", consoleRunner.FetchOneEGInternally)
				.Add("Print the 'Members' table.", consoleRunner.ListAllMembers)
				.Add("Fetch one record from the 'Members' table.", consoleRunner.FetchOneMember)
				.Add(" - Show the avg. Purchase Price Parity / Capita & the above-average countries", consoleRunner.AverageCountries)
				.Add(" - Show members that are/aren't located in a capital city (separately)", consoleRunner.MembersInCapitals)
				.Add(" - Show members from the given country", consoleRunner.CountMembersInCountry)
				.Add(" - Show the richest members (by its country) in each expert group", consoleRunner.GetRichestMemberInExpertGroup)
				.Add("<< Main menu", ConsoleTools.ConsoleMenu.Close)
				.Configure(config =>
				{
					//config.Selector = "--> ";
					config.EnableFilter = true;
					config.EnableWriteTitle = true;
					config.Title = "EISA Internal Audits' menu";
				});
			ConsoleTools.ConsoleMenu externalauditMenu = new ConsoleTools.ConsoleMenu(args, 1)
				.Add("Print the 'Brands' table.", consoleRunner.ListAllBrands)
				.Add("Fetch one record from the 'Brands' table.", consoleRunner.FetchOneBrand)
				.Add("Print the 'ExpertGroups' table.", consoleRunner.ListAllExpertgroupsExternally)
				.Add("Fetch one record from the 'ExpertGroups' table.", consoleRunner.FetchOneEGExternally)
				.Add("Print the 'Products' table.", consoleRunner.ListAllProducts)
				.Add("Fetch one record from the 'Products' table.", consoleRunner.FetchOneProduct)
				.Add(" - Show the most awarded brands & their (awarded) products", consoleRunner.ListTopBrandsAndProducts)
				.Add(" - Show the most expensive product for each expert group", consoleRunner.MaxPriceProductInEveryEG)
				.Add("<< Main menu", ConsoleTools.ConsoleMenu.Close)
				.Configure(config =>
				{
					//config.Selector = "--> ";
					config.EnableFilter = true;
					config.EnableWriteTitle = true;
					config.Title = "EISA External Audits' menu";
				});

			ConsoleTools.ConsoleMenu mainMenu = new ConsoleTools.ConsoleMenu(args, 0)
				.Add("Enter the Internal Audits' menu, tables: ExpertGroups, Countries, Members", internalauditMenu.Show)
				.Add("Enter the External Audits' menu, tables: ExpertGroups, Brands, Products", externalauditMenu.Show)
				.Add("Enter the Database Administrators' menu: Insert, Update, Remove in all tables", adminMenu.Show)
				.Add("Exit", ConsoleTools.ConsoleMenu.Close)
				.Configure(config =>
				{
					//config.Selector = "--> ";
					config.EnableWriteTitle = true;
					config.EnableFilter = true;
					config.Title = "Main menu";
				});

			mainMenu.Show();
		}
	}
}
