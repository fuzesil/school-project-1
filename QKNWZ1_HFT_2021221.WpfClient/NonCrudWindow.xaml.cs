using System.Collections.Specialized;
using System.Windows;
using QKNWZ1_HFT_2021221.WpfClient.VM;

namespace QKNWZ1_HFT_2021221.WpfClient
{
	/// <summary>
	/// Interaction logic for NonCrudWindow.xaml
	/// </summary>
	public partial class NonCrudWindow : Window
	{
		public NonCrudWindow(INotifyCollectionChanged coll)
		{
			this.DataContext = new NonCrudViewModel(coll);
			this.InitializeComponent();
		}
	}
}
