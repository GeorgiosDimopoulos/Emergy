using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Emergy.ServiceWPF.Views;
using System.ComponentModel;
using Emergy.Service.WPF.Models;
using Emergy.Service.WPF.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.WindowsAzure.MobileServices;

namespace Emergy.Service.WPF.Views
{
	/// <summary>
	/// Interaction logic for SignalView.xaml
	/// </summary>
	public partial class SignalView : Window, INotifyPropertyChanged
	{
		
		public SignalView()
		{
			InitializeComponent();			
		}

		//public async override void EndInit()
		//{
		//	base.EndInit();
		//	var list = await  (Application.Current as App).SyncSignals.ToCollectionAsync();
		//}

		private void Back_OnClickack_Click(object sender, RoutedEventArgs e)
		{			
			LoginInPage lpInPage = new LoginInPage();
			lpInPage.Show();
			this.Close();
		}

		private void EditDepartment_OnClickditDepartment_OnClick(object sender, RoutedEventArgs e)
		{
			EditDepartment editDepartment = new EditDepartment();
			editDepartment.Show();
			//this.Close();
		}

		private void Accidents_OnClick(object sender, RoutedEventArgs e)
		{
			AccidentButton.IsCheckable = true;
			AccidentButton.IsChecked= true;
			MapButton.IsChecked = false;
			MapButton.IsCheckable= false;
			AccidentsList.Visibility = Visibility.Visible;
			Map.Visibility = Visibility.Hidden;
		}		

		private void Maps_OnClick(object sender, RoutedEventArgs e)
		{
			AccidentButton.IsCheckable = false;
			AccidentButton.IsChecked = false;
			MapButton.IsChecked = true;
			MapButton.IsCheckable= true;
			AccidentsList.Visibility = Visibility.Hidden;
			Map.Visibility = Visibility.Visible;
		}

		private async void DeleteAllSignals_OnClick(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("θέλετε να διαγράψετε όλα τα γεγονότα;", "Διαγραφή γεγονότων",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				//changes = true;
				foreach (var item in ((SignalViewViewModel)this.DataContext).Signals)
				{
					await(Application.Current as App).SyncSignals.DeleteAsync(item);
				}
				((SignalViewViewModel)this.DataContext).Signals.Clear();				
			}
		}		

		private async void DeleteSignal_OnClick(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("θέλετε να διαγράψετε το γεγονός;", "Διαγραφή γεγονότος",
				MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				var tempSignal = (Models.Signal) (AccidentListView.SelectedItem);
				await(Application.Current as App).SyncSignals.DeleteAsync(tempSignal);
				((SignalViewViewModel) this.DataContext).Signals.Remove(tempSignal);
			}
		}		

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				
			}
			remove
			{
				
			}
		}


		private void FindAccidentLocation(object sender, MouseButtonEventArgs e)
		{
			Location location = ((Signal) ((Pushpin) sender).DataContext).Location;
			var result = MessageBox.Show("Τοποθεσία συμβάντος: "+ location, "Εύρεση τοποθεσίας",
				MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
