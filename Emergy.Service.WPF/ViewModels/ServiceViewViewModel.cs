using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Emergy.Service.WPF.Models;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.WindowsAzure.MobileServices;

namespace Emergy.Service.WPF.ViewModels
{
    class ServiceViewViewModel : INotifyPropertyChanged
    {
	    public ServiceViewViewModel()
	    {
		    FetchData();
	    }

		private MobileServiceCollection<Models.Service, Models.Service> _services;

		public MobileServiceCollection<Models.Service, Models.Service> Services
        {
            get
            {
                return _services;
            }
            set
            {
                _services = value;
                OnPropertyChanged(nameof(Services));
            }
        }
	    
        public async Task FetchData()
        {
            Services = await (System.Windows.Application.Current as App).SyncServices.ToCollectionAsync();
	        foreach (var service in Services)
	        {
		        Location location = new Location(latitude: service.Latitude, longitude: service.Lognitude);
		        service.ServiceLocation = location;		       
	        }
		}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
