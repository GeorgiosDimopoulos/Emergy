using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emergy.XamarinApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTabbedPage : TabbedPage
    {
        public UserTabbedPage()
        {
            try
            {
                Title = "Emergy";
                InitializeComponent();
            }
            catch (Exception e)
            {
                
            }
        }
    }
}