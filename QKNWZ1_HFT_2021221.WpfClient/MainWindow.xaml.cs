using System.Windows;
using System.Windows.Input;

namespace WpfClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly System.Text.RegularExpressions.Regex regex = new("[^a-zA-Z0-9]+");

		public MainWindow()
		{
			System.Threading.Thread.Sleep(2_500);
			this.InitializeComponent();
		}

		private void TextBlock_InputValidation(object sender, TextCompositionEventArgs e) => e.Handled = this.regex.IsMatch(e.Text);
	}
}
