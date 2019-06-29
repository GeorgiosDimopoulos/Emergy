using System.Windows;

namespace Emergy.Service.WPF.Views
{
	/// <summary>
	/// Interaction logic for EditDepartment.xaml
	/// </summary>
	public partial class EditDepartment : Window
	{
		public EditDepartment()
		{
			InitializeComponent();
		}

		private void BtnDialogOk_OnClicklogOk_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		
	}
}
