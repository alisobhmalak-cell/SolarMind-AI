using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Solar_Mind_AI
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Android.OS.Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Window != null)
            {
                // 1. تمديد صورة الخلفية لتغطية الشاشة بالكامل (Full Screen)
                Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

                // 2. جعل أيقونات شريط الإشعارات (الساعة والبطارية) باللون الغامق عشان تبان
                Window.DecorView.SystemUiVisibility = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
            }
        }
    }
}
