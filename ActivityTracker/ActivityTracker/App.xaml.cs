using ActivityTracker.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace ActivityTracker
{
    public partial class App : Application
    {
        public MobileServiceClient Client { get; set; }
        public IMobileServiceTable<Signal> SyncSignals { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Client = new MobileServiceClient("http://emergy.azurewebsites.net/");

            SyncSignals = Client.GetTable<Signal>();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}