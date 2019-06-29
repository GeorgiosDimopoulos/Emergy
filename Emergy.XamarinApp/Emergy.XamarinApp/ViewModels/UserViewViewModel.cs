using Emergy.XamarinApp.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Emergy.XamarinApp.ViewModels
{
    class UserViewViewModel : INotifyPropertyChanged
    {
        private MobileServiceCollection<User, User> _users;

        public MobileServiceCollection<User, User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserViewViewModel()
        {
            FetchData();
        }

        public async Task FetchData()
        {
            Users = await (Application.Current as App).SyncUsers.ToCollectionAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
