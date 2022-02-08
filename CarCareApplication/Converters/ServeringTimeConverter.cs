using System;
using System.Globalization;
using Xamarin.Forms;

namespace CarCareApplication.Converters
{
    public class ServeringTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var time = TimeSpan.FromMinutes((int)value);

            string formattedtime = $"{(int)time.TotalHours:00}:{time.Minutes:00}";

            return culture.Name.Equals("ar-EG") ? $"{formattedtime} ساعة" : $"{formattedtime} Hour";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
