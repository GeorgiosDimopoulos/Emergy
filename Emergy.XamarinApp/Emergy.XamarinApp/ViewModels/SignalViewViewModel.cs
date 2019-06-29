using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Microsoft.AspNet.SignalR.Client;

namespace Emergy.XamarinApp.Models
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
            Signals = await (Application.Current as App).SyncSignals.ToCollectionAsync();
        }
        
	    public event PropertyChangedEventHandler PropertyChanged;

	    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
		    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
    }
}