#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Backdrop.SfBackdropPage;

public partial class InitialPage : SampleView
{
    public InitialPage()
	{
		InitializeComponent();
    }

    private void OpenBackdropPage(object sender, EventArgs e)
    {
        if (App.Current != null && App.Current.Windows != null && App.Current.Windows.Count > 0)
        {
            var page = App.Current.Windows[0].Page as NavigationPage;
            if (page != null)
            {
                page.BarBackgroundColor = App.Current.RequestedTheme == AppTheme.Light ? Color.FromArgb("#6750A4") : Color.FromArgb("#D0BCFF");
                page.BarTextColor = App.Current.RequestedTheme == AppTheme.Light ? Colors.White : Color.FromArgb("#381E72");
            }
        }

        Navigation.PushAsync(new Backdrop(), true);
    }
}