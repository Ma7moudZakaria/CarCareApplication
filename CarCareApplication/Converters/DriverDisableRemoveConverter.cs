using CarCareApplication.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CarCareApplication.Converters
{
    public class DriverDisableRemoveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return value;
            if (GlobalResources.Current.ApplicationUser.UserType == Models.UserType.Driver) return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
