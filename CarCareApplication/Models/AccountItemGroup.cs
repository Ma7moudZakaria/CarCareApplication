using System.Collections.Generic;

namespace CarCareApplication.Models
{
    public class AccountItemGroup : List<AccountItem>
    {
        public string Header { get; set; }

        public AccountItemGroup(string header, List<AccountItem> items) : base(items)
        {
            Header = header;
        }
    }

    public class AccountItem : BindableBase
    {
        public string Title { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }
    }
}
