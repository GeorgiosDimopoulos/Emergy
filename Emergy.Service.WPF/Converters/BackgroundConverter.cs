using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Emergy.Service.WPF.Models;

namespace OrderfyServer.Converters
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HospSignalTypes type = (HospSignalTypes)value;

            if (type.Equals(HospSignalTypes.HeartPulse))
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if(type.Equals(HospSignalTypes.Diabetes))
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else if (type.Equals(HospSignalTypes.Fever))
            {
	            return new SolidColorBrush(Colors.Green);
            }
            else if (type.Equals(HospSignalTypes.Simple))
            {
	            return new SolidColorBrush(Colors.Yellow);
            }
            else 
            {
	            return new SolidColorBrush(Colors.Yellow);
            }

		}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
