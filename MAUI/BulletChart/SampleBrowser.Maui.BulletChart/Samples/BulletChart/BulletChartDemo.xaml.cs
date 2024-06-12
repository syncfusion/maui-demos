#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.BulletChart.BulletChart;

public partial class BulletChartDemo : SampleView
{
    public BulletChartDemo()
    {
        InitializeComponent();

#if IOS
        contentView.HeightRequest = 650;
#endif
    }

    private void Uri_Tapped(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://en.wikipedia.org/wiki/Bullet_graph");
            Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception)
        {
        }
    }
}