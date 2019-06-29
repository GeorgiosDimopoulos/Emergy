using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Emergy.Service.WPF;
using Microsoft.WindowsAzure.MobileServices;
using System.Windows;
using Emergy.Service.WPF.Models;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Maps.MapControl.WPF;

namespace Emergy.Service.WPF.ViewModels
{
    class SignalViewViewModel : INotifyPropertyChanged
    {
        private MobileServiceCollection<Signal, Signal> _signals;

	    

        public MobileServiceCollection<Signal, Signal> Signals
        {
            get
            {
                return _signals;
            }
            set
            {
                _signals = value;
                OnPropertyChanged(nameof(Signals));
            }
        }

        private HubConnection SignalHubConnection { get; set; }
        private IHubProxy SignalHubProxy { get; set; }


        public SignalViewViewModel()
        {
            FetchData();
            SignalHubConnection = new HubConnection("http://emergy.azurewebsites.net/");

            SignalHubProxy = SignalHubConnection.CreateHubProxy("SignalHub");

            SignalHubProxy.On("RefreshSignals", OnRefreshSignalsHandler);

            SignalHubConnection.Start().ContinueWith(x => Debug.WriteLine("Conn done"));

           
        }

       
        private async void OnRefreshSignalsHandler()
        {
            await FetchData();
        }

        public async Task FetchData()
        {
	        Signals = await (Application.Current as App).SyncSignals.Where(x => x.Own == ServiceOwn.FireDep)
		        .ToCollectionAsync();
	        foreach (var signal in Signals)
	        {
				Location location = new Location(latitude:signal.Latitude,longitude:signal.Longitude);
		        signal.Location = location;
		        DateTimeOffset time = signal.Time;
		        signal.Time = time;
	        }
        }
		
	    public event PropertyChangedEventHandler PropertyChanged;

	    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
    }
}
