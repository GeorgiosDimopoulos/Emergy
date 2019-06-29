using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Emergy.Service.WPF.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace Emergy.Service.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        public MobileServiceClient Client { get; set; }
        public IMobileServiceTable<Models.Service> SyncServices { get; set; }
        public IMobileServiceTable<Signal> SyncSignals { get; set; }
        public IMobileServiceTable<User> SyncUsers { get; set; }

        protected async override void OnStartup(StartupEventArgs e)
        {
            Client = new MobileServiceClient("http://emergy.azurewebsites.net/");

            SyncSignals = Client.GetTable<Signal>();
            SyncUsers = Client.GetTable<User>();
            SyncServices = Client.GetTable<Models.Service>();
            var lst = await SyncUsers.ToCollectionAsync();
        }
    }
}
