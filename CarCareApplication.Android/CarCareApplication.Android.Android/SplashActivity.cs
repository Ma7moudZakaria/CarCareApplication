using Android.App;
using Android.OS;
using System.Threading.Tasks;

namespace CarCare.Droid
{
    [Activity(Label = "CarCare", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, Icon = "@drawable/logo")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            Task.Delay(3000);
            StartActivity(typeof(MainActivity));
        }
    }
}