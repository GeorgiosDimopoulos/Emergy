using Emergy.XamarinApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Emergy.XamarinApp.Views
{
    public partial class AccountView : ContentPage
    {
        public AccountView()
        {
            InitializeComponent();
        }

        private async void CreateButton_OnClicked(object sender, EventArgs e)
        {
            User user = new Models.User
            {
                Id = Guid.NewGuid().ToString("N"),
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
               
            };
            
            await (Application.Current as App).SyncUsers.InsertAsync(user);

        }

        private void PasswordEntry_OnCompleted(object sender, EventArgs e)
        {
            if (PasswordEntry.Text.Length < 6)
            {
                PasswordEntry.Text = "";
                PasswordEntry.Placeholder = "Τουλ. 6 χαρακτήρες";
                PasswordEntry.PlaceholderColor = Color.Red;
            }

        }


        private void PasswordEntry_OnFocused(object sender, FocusEventArgs e)
        {
            PasswordEntry.BackgroundColor = Color.Transparent;
            PasswordEntry.Placeholder = "";
            PasswordEntry.Text = "";
        }

        private void UsernameEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            if (!UsernameEntry.Text.Contains("@"))
            {
                UsernameEntry.Text = "";
                UsernameEntry.Placeholder = "Μη έγκυρο e-mail";
                UsernameEntry.PlaceholderColor = Color.Red;
            }
        }

        private void UsernameEntry_OnFocused(object sender, FocusEventArgs e)
        {
            UsernameEntry.BackgroundColor = Color.Transparent;
            UsernameEntry.Placeholder = "";
            UsernameEntry.Text = "";
        }
    }
}
