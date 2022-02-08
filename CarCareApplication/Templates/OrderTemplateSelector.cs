using CarCareApplication.Models;
using CarCareApplication.Resources;
using Xamarin.Forms;

namespace CarCareApplication.Templates
{
    public class OrderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClientOrder { get; set; }
        public DataTemplate AdminOrder { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            => GlobalResources.Current.ApplicationUser.UserType.Equals(UserType.Customer) ? ClientOrder : AdminOrder;

    }
}
