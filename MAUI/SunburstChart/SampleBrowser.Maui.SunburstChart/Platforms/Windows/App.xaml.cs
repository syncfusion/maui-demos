﻿using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SampleBrowser.Maui.SunburstChart.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        foreach (var item in Application.Windows)
        {
            var platformWindow = (item?.Handler?.PlatformView as Microsoft.UI.Xaml.Window);

            if (platformWindow != null)
            {
                platformWindow.ExtendsContentIntoTitleBar = false;
                platformWindow.Title = ".NET MAUI Circular Chart Demo";
            }
        }
    }
}

