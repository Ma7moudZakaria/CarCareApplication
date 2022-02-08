using System;
using System.Globalization;
using Xamarin.Forms;

namespace CarCareApplication.Converters
{
    public class BasePriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return value;
            return culture.Name.Equals("ar-EG") ? $"{value} ج.م" : $"{value} EGP";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
