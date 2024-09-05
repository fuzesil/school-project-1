using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.WpfClient.VM
{
	public partial class InternalViewModel : ObservableRecipient
	{
		private readonly (string, string) url;

		[ObservableProperty]
		private string input;

		public ImmutableRestCollection<ExpertGroup> Expertgroups { get; set; }
		public ImmutableRestCollection<Member> Members { get; set; }
		public ImmutableRestCollection<Country> Countries { get; set; }

		public static bool IsInDesignMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		public InternalViewModel()
		{
			if (IsInDesignMode) return;
			this.url = ("http://localhost:49943/", "Internal/");
			this.Expertgroups = new(this.url.Item1, this.url.Item2);
			this.Members = new(this.url.Item1, this.url.Item2);
			this.Countries = new(this.url.Item1, this.url.Item2);
		}

		[RelayCommand]
		private void GetOneExpertgroup() => this.Expertgroups.GetSingle("GetOneExpertgroup/" + this.input);

		[RelayCommand]
		private void GetAllExpertgroups() => this.Expertgroups.ReInit();

		[RelayCommand]
		private void GetOneCountry() => this.Countries.GetSingle("GetOneCountry/" + this.input);

		[RelayCommand]
		private void GetAllCountries() => this.Countries.ReInit();

		[RelayCommand]
		private void GetOneMember() => this.Members.GetSingle("GetOneMember/" + this.input);

		[RelayCommand]
		private void GetAllMembers() => this.Members.ReInit();

		[RelayCommand]
		private void MembersInCapital() =>
			new NonCrudWindow(new ImmutableRestCollection<MemberCountry>(this.url.Item1, this.url.Item2, specialAction: "ListMembersInCapitalCity/true"))
			.ShowDialog();

		[RelayCommand]
		private void RichestMemberInExpertgroup() =>
			new NonCrudWindow(new ImmutableRestCollection<ExpertgroupMemberCountry>(this.url.Item1, this.url.Item2, specialAction: "GetRichestMemberInExpertGroup"))
			.ShowDialog();
	}
}
