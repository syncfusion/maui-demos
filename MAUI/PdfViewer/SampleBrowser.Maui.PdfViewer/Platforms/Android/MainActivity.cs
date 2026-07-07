using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.View;

namespace SampleBrowser.Maui.PdfViewer;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
#if NET10_0_OR_GREATER
        WindowCompat.SetDecorFitsSystemWindows(Window, true);
#endif
    }
}
