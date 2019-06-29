using System;
using System.Diagnostics;
using System.Windows;
using Emergy.Service.WPF;
using Emergy.Service.WPF.Models;
using Emergy.Service.WPF.Views;
using Microsoft.WindowsAzure.MobileServices;

namespace Emergy.ServiceWPF.Views
{
	/// <summary>
    /// Interaction logic for LoginInPage.xaml
    /// </summary>
    public partial class LoginInPage : Window
    {
        public LoginInPage()
        {
            InitializeComponent();
        }

	    private  void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
	        SignalView signalView = new SignalView();
            signalView.Show();
            this.Hide();
	    }

	    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
	    {
			System.Windows.Application.Current.Shutdown();
		}
	}
}
