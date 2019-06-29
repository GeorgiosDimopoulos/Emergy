using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ActivityTracker.Models;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace ActivityTracker.ViewModels
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