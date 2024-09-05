using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.WpfClient.VM
{
	public partial class ExternalViewModel : ObservableRecipient
	{
		private readonly (string, string) url;

		[ObservableProperty]
		private string input;

		public ImmutableRestCollection<Brand> Brands { get; set; }
		public ImmutableRestCollection<Product> Products { get; set; }
		public ImmutableRestCollection<ExpertGroup> Expertgroups { get; set; }

		public static bool IsInDesignMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		public ExternalViewModel()
		{
			if (IsInDesignMode) return;
			this.url = ("http://localhost:49943/", "External/");
			this.Brands = new(this.url.Item1, this.url.Item2);
			this.Products = new(this.url.Item1, this.url.Item2);
			this.Expertgroups = new(this.url.Item1, this.url.Item2);
		}

		[RelayCommand]
		private void GetOneBrand() => this.Brands.GetSingle("GetOneBrand/" + this.input);

		[RelayCommand]
		private void GetAllBrands() => this.Brands.ReInit();

		[RelayCommand]
		private void GetOneProduct() => this.Products.GetSingle("GetOneProduct/" + this.input);

		[RelayCommand]
		private void GetAllProducts() => this.Products.ReInit();

		[RelayCommand]
		private void GetOneExpertgroup() => this.Expertgroups.GetSingle("GetOneExpertgroup/" + this.input);

		[RelayCommand]
		private void GetAllExpertgroups() => this.Expertgroups.ReInit();

		[RelayCommand]
		private void GetProductsByBrandId() =>
			new NonCrudWindow(new ImmutableRestCollection<BrandWithAwards>(this.url.Item1, this.url.Item2, specialAction: "GetProductsByBrandId"))
			.ShowDialog();

		[RelayCommand]
		private void GetMaxPriceProdInEveryEG() =>
			new NonCrudWindow(new ImmutableRestCollection<ExpertgroupProduct>(this.url.Item1, this.url.Item2, specialAction: "GetMaxPriceProdInEveryEG"))
			.ShowDialog();
	}
}
