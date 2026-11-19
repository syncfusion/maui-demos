using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.NavigationDrawer;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.NavigationDrawer.NavigationDrawer
{
    public partial class GettingStartedMobile : SampleView
    {
        public GettingStartedMobile()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
        {
            navigationDrawer.ToggleSecondaryDrawer();
        }

        private void CloseIcon_Tapped(object? sender, TappedEventArgs e)
        {
            navigationDrawer.SecondaryDrawerSettings.IsOpen = false;
        }
    }
}
