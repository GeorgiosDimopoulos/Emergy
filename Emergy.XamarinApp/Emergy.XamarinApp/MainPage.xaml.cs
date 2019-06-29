using Plugin.Geolocator;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Emergy.XamarinApp
{
    public partial class MainPage : ContentPage
    {
        static string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        string location = string.Empty;
        private double lat;
        private double lng;
        Geocoder geocoder;




        public MainPage()
        {
            InitializeComponent();
            geocoder = new Geocoder();
        }        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Indicator.IsRunning = true;
            Indicator.IsVisible = true;

            try
            {
                var locator = CrossGeolocator.Current;

                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(timeoutMilliseconds: 100000);

                lat = position.Latitude;
                lng = position.Longitude;

                mpla.Text = "Position Status: " + position.Timestamp
                        + "\nPosition Latitude: " + position.Latitude
                        + "\nPosition Longitude: " + position.Longitude;
                GetAddress();
            }
            catch (Exception ex)
            {
                coordsLabel.Text = "Unable to get address, may need to increase timeout";
            }

            Indicator.IsRunning = false;
            Indicator.IsVisible = false;
        }
        
        public async void GetAddress()
        {
            var positionmpla = new Position(37.962518, 23.690770);
            var possibleAddresses = await geocoder.GetAddressesForPositionAsync(positionmpla);
            foreach (var address in possibleAddresses)
            {
                coordsLabel.Text += address + "\n";
                Debug.WriteLine(address);
            }
        }

     }
}
