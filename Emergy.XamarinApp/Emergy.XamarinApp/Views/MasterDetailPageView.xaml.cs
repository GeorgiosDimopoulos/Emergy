using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Emergy.XamarinApp.Views
{
    public partial class MasterDetailPageView : MasterDetailPage
    {
        ToggleBarView tb;
        public MasterDetailPageView()
        {
            InitializeComponent();
            tb = new ToggleBarView();
            Master = tb;
            tb.lv.ItemSelected += OnItemSelected;

        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (tb.lv.SelectedItem.ToString().Equals("Δημιουργία Λογαριασμού"))
            {

                AccountView a = new AccountView();
                Detail = new NavigationPage(a);
                IsPresented = false;
            } else if (tb.lv.SelectedItem.ToString().Equals("Σήμα Κινδύνου"))
            {

                TabbedPageView a = new TabbedPageView();
                Detail = new NavigationPage(a);
                IsPresented = false;
            }
        }

    }
}
