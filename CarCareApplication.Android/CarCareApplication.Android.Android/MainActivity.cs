using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCareApplication.Droid
{
    [Activity(Label = "CarCareApplication", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.DecorView.LayoutDirection = Preferences.Get("AppLang", "ar-EG") == "ar-EG" ? LayoutDirection.Rtl : LayoutDirection.Ltr;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        long lastPress;

        public override void OnBackPressed()
        {
            if (Shell.Current.Navigation.NavigationStack.Count == 1)
            {
                // source https://stackoverflow.com/a/27124904/3814729
                long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                // source https://stackoverflow.com/a/14006485/3814729
                if (currentTime - lastPress > 5000)
                {
                    Toast.MakeText(this, Window.DecorView.LayoutDirection == LayoutDirection.Rtl ? "أضغط على رجوع مره آخرى للخروج" : "Press back again to exit", ToastLength.Long).Show();
                    lastPress = currentTime;
                }
                else
                {
                    base.OnBackPressed();
                }
            }
            else
            {
                base.OnBackPressed();
            }

        }
    }
}