using System;
using System.Diagnostics;
using ActivityTracker.Models;
using Plugin.Geolocator;
using Xamarin.Forms;
using Exception = System.Exception;

namespace ActivityTracker
{
    public partial class MainPage : ContentPage
    {
        private int Heartrate;
        private int Temperature;

        public MainPage()
        {
            InitializeComponent();

            Heartrate = 60;
            Temperature = 37;
            PulseLabel.Text = Heartrate.ToString();
            Temperature = 37;
            TemperatureLabel.Text = Temperature.ToString();
        }
        
        private void Minus_OnClicked(object sender, EventArgs e)
        {
            Heartrate -= 10;
            PulseLabel.Text = Heartrate.ToString();
            if (Heartrate < 20)
                SendSignal(1);
        }

        private void Diabetes_OnClicked(object sender, EventArgs e)
        {
            SendSignal(2);
        }

        private void PlusTemp_OnClicked(object sender, EventArgs e)
        {
            Temperature++;
            TemperatureLabel.Text = Temperature.ToString();
            if (Temperature > 45)
                SendSignal(3);
        }

        private void MinusTemp_OnClicked(object sender, EventArgs e)
        {
            Temperature--;
            TemperatureLabel.Text = Temperature.ToString();
            if (Temperature < 32)
                SendSignal(3);
        }

        private async void SendSignal(int type)
        {
            stacklayout.IsVisible = false;
            LoadingLabel.Text = "Εύρεση τοποθεσίας...";
            LoadingStackLayout.IsVisible = true;
            Indicator.IsRunning = true;
            Indicator.IsVisible = true;

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(100000);

                LoadingLabel.Text = "Αποστολή σήματος κινδύνου...";

                Signal signal = new Signal();
                signal.Id = Guid.NewGuid().ToString("N");
                signal.Latitude = position.Latitude;
                signal.Longitude = position.Longitude;
                signal.Own = ServiceOwn.FireDep;
                if(type == 1)
                    signal.Types = HospSignalTypes.HeartPulse;
                else if(type == 2)
                    signal.Types = HospSignalTypes.Diabetes;
                else
                    signal.Types = HospSignalTypes.Fever;

                await (Application.Current as App).SyncSignals.InsertAsync(signal);

                LoadingStackLayout.IsVisible = false;
                Indicator.IsRunning = false;
                Indicator.IsVisible = false;

                await DisplayAlert("Επιτυχία!", "Το σήμα κινδύνου στάλθηκε επιτυχώς!", "ΟΚ");
                stacklayout.IsVisible = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                LoadingStackLayout.IsVisible = false;
                Indicator.IsRunning = false;
                Indicator.IsVisible = false;

                if (!await DisplayAlert("Αποτυχία!", "Η αποστολή σήματος κινδύνου απέτυχε! Ελέγξτε αν είναι ενεργοποιημένη η τοποθεσίας σας.", "ΟΚ", "Προσπαθήστε ξανά!"))
                    SendSignal(type);
                stacklayout.IsVisible = true;
            }
        }
        
        private void Plus_OnClicked(object sender, EventArgs e)
        {
            Heartrate += 10;
            PulseLabel.Text = Heartrate.ToString();
            if (Heartrate > 100)
                SendSignal(1);
        }
    }
}