#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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
        if (Application.Current != null && Application.Current.MainPage != null)
        {
            var page = Application.Current.MainPage as NavigationPage;
            if (page != null)
            {
                page.BarBackgroundColor = Color.FromArgb("#6200EE");
                page.BarTextColor = Colors.White;
            }
        }

        Navigation.PushAsync(new Backdrop(), true);
    }
}