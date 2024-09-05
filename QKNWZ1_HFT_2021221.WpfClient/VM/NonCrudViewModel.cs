using System;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;

namespace QKNWZ1_HFT_2021221.WpfClient.VM
{
	public partial class NonCrudViewModel : ObservableObject
	{
		[ObservableProperty]
		private INotifyCollectionChanged collection;

		public NonCrudViewModel(INotifyCollectionChanged collection) => this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
	}
}
