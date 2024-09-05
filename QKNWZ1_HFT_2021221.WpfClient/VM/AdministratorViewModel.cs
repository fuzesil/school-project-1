using System;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QKNWZ1_HFT_2021221.Models;

namespace QKNWZ1_HFT_2021221.WpfClient.VM
{
	public partial class AdministratorViewModel : ObservableRecipient
	{
		private ExpertGroup selectedExpertgroup;
		private Country selectedCountry;
		private Member selectedMember;

		[ObservableProperty]
		private string errorMessage;

		public AdministratorViewModel()
		{
			string baseUrl = "http://localhost:49943/", endpoint = "Internal/";
			if (!IsInDesignMode)
			{
				this.Expertgroups = new(baseUrl, endpoint, "hub");
				this.Countries = new(baseUrl, endpoint, "hub");
				this.Members = new(baseUrl, endpoint, "hub");
			}
			// --- EG --- //
			this.CreateExpertgroupCommand = new RelayCommand(this.CreateExpertgroup);

			this.UpdateExpertgroupCommand = new RelayCommand(
				this.UpdateExpertgroup,
				() => this.SelectedExpertgroup is not null);

			this.DeleteExpertgroupCommand = new RelayCommand(
				this.DeleteExpertgroup,
				() => this.SelectedExpertgroup is not null);

			// --- country --- //
			this.CreateCountryCommand = new RelayCommand(this.CreateCountry);

			this.UpdateCountryCommand = new RelayCommand(
				this.UpdateCountry,
				() => this.SelectedCountry is not null);

			this.DeleteCountryCommand = new RelayCommand(
				this.DeleteCountry,
				() => this.SelectedCountry is not null);

			// --- member --- //
			this.CreateMemberCommand = new RelayCommand(this.CreateMember);

			this.UpdateMemberCommand = new RelayCommand(
				this.UpdateMember,
				() => this.SelectedMember is not null);

			this.DeleteMemberCommand = new RelayCommand(
				this.DeleteMember,
				() => this.SelectedMember is not null);

			this.PickCountryForMemberCommand = new RelayCommand(
				() =>
				{
					this.SelectedMember.CountryId = this.SelectedCountry.Id;
					this.OnPropertyChanged(nameof(this.SelectedMember));
				},
				() => this.SelectedCountry is not null);

			this.PickExpertgroupForMemberCommand = new RelayCommand(
				() =>
				{
					this.SelectedMember.ExpertGroupId = this.SelectedExpertgroup.Id;
					this.OnPropertyChanged(nameof(this.SelectedMember));
				},
				() => this.SelectedExpertgroup is not null);
		}

		private void CreateExpertgroup()
		{
			ExpertGroup eg = new() { Name = this.SelectedExpertgroup?.Name ?? "_new_" };
			this.Expertgroups.Add(eg);
			this.selectedExpertgroup = null;
			this.OnPropertyChanged(nameof(this.SelectedExpertgroup));
		}

		private void UpdateExpertgroup()
		{
			//ExpertGroup eg = new() { Name = this.SelectedExpertgroup.Name };
			try { this.Expertgroups.Update(this.selectedExpertgroup); }
			catch (ArgumentException ex) { this.errorMessage = ex.Message; }
			finally
			{
				//this.selectedExpertgroup = null;
				//this.OnPropertyChanged(nameof(this.SelectedExpertgroup));
			}
		}
		private void DeleteExpertgroup()
		{
			this.Expertgroups.Delete(this.SelectedExpertgroup.Id);
			this.selectedExpertgroup = null;
			this.OnPropertyChanged(nameof(this.SelectedExpertgroup));
		}
		private void CreateCountry()
		{
			Country c = new()
			{
				Name = this.SelectedCountry?.Name ?? "NewCountry",
				CapitalCity = this.SelectedCountry?.CapitalCity ?? "_capital_",
				CallingCode = this.SelectedCountry?.CallingCode ?? -1,
				PPPperCapita = this.SelectedCountry?.PPPperCapita ?? -1,
			};
			this.Countries.Add(c);
			this.selectedCountry = null;
			this.OnPropertyChanged(nameof(this.SelectedCountry));
		}
		private void UpdateCountry()
		{
			//Country c = new()
			//{
			//	Name = this.SelectedCountry.Name,
			//	CapitalCity = this.SelectedCountry.CapitalCity,
			//	CallingCode = this.SelectedCountry.CallingCode,
			//	PPPperCapita = this.SelectedCountry.PPPperCapita,
			//};
			try { this.Countries.Update(this.selectedCountry); }
			catch (ArgumentException ex) { this.errorMessage = ex.Message; }
			finally
			{
				//this.selectedCountry = null;
				//this.OnPropertyChanged(nameof(this.SelectedCountry));
			}
		}
		private void DeleteCountry()
		{
			this.Countries.Delete(this.SelectedCountry.Id);
			this.selectedCountry = null;
			this.OnPropertyChanged(nameof(this.SelectedCountry));
		}
		private void CreateMember()
		{
			Member m = new()
			{
				Name = this.SelectedMember?.Name ?? "_newMember_",
				Publisher = this.SelectedMember?.Publisher ?? "_publisher_",
				OfficeLocation = this.SelectedMember?.OfficeLocation ?? "_officeLocation_",
				Website = this.SelectedMember?.Website ?? "_website_",
				ChiefEditor = this.SelectedMember?.ChiefEditor ?? "_chiefEditor_",
				PhoneNumber = this.SelectedMember?.PhoneNumber ?? "_phoneNumber_",
				CountryId = this.SelectedMember?.CountryId ?? 1,
				ExpertGroupId = this.SelectedMember?.ExpertGroupId ?? 1,
			};
			this.Members.Add(m);
			this.selectedMember = null;
			this.OnPropertyChanged(nameof(this.SelectedMember));
		}
		private void UpdateMember()
		{
			//Member m = new()
			//{
			//	Name = this.SelectedMember.Name,
			//	Publisher = this.SelectedMember.Publisher,
			//	OfficeLocation = this.SelectedMember.OfficeLocation,
			//	Website = this.SelectedMember.Website,
			//	ChiefEditor = this.SelectedMember.ChiefEditor,
			//	PhoneNumber = this.SelectedMember.PhoneNumber,
			//	CountryId = this.SelectedMember.CountryId,
			//	ExpertGroupId = this.SelectedMember.ExpertGroupId,
			//};
			try { this.Members.Update(this.selectedMember); }
			catch (ArgumentException ex) { this.errorMessage = ex.Message; }
			finally
			{
				//this.selectedMember = null;
				//this.OnPropertyChanged(nameof(this.SelectedMember));
			}
		}
		private void DeleteMember()
		{
			this.Members.Delete(this.SelectedMember.Id);
			this.selectedMember = null;
			this.OnPropertyChanged(nameof(this.SelectedMember));
		}

		//public string ErrorMessage
		//{
		//    get => this.errorMessage;
		//    set => this.SetProperty(ref this.errorMessage, value);
		//}

		public static bool IsInDesignMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		public RestCollection<Member> Members { get; set; }
		public RestCollection<Country> Countries { get; set; }
		public RestCollection<ExpertGroup> Expertgroups { get; set; }

		public IRelayCommand CreateExpertgroupCommand { get; set; }
		public IRelayCommand DeleteExpertgroupCommand { get; set; }
		public IRelayCommand UpdateExpertgroupCommand { get; set; }

		public IRelayCommand CreateCountryCommand { get; set; }
		public IRelayCommand DeleteCountryCommand { get; set; }
		public IRelayCommand UpdateCountryCommand { get; set; }

		public IRelayCommand CreateMemberCommand { get; set; }
		public IRelayCommand DeleteMemberCommand { get; set; }
		public IRelayCommand UpdateMemberCommand { get; set; }

		public IRelayCommand PickCountryForMemberCommand { get; set; }
		public IRelayCommand PickExpertgroupForMemberCommand { get; set; }

		public ExpertGroup SelectedExpertgroup
		{
			get => this.selectedExpertgroup;
			set
			{
				if (value is null) return;
				this.selectedExpertgroup = value;
				this.OnPropertyChanged();
				this.DeleteExpertgroupCommand?.NotifyCanExecuteChanged();
				this.UpdateExpertgroupCommand?.NotifyCanExecuteChanged();
				this.PickExpertgroupForMemberCommand?.NotifyCanExecuteChanged();
			}
		}

		public Country SelectedCountry
		{
			get => this.selectedCountry;
			set
			{
				if (value is null) return;
				this.selectedCountry = value;
				this.OnPropertyChanged();
				this.UpdateCountryCommand?.NotifyCanExecuteChanged();
				this.DeleteCountryCommand?.NotifyCanExecuteChanged();
				this.PickCountryForMemberCommand?.NotifyCanExecuteChanged();
			}
		}

		public Member SelectedMember
		{
			get => this.selectedMember;
			set
			{
				if (value is null) return;
				this.selectedMember = value;
				this.OnPropertyChanged();
				this.UpdateMemberCommand?.NotifyCanExecuteChanged();
				this.DeleteMemberCommand?.NotifyCanExecuteChanged();
			}
		}
	}
}
