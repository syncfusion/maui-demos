#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class CustomLabel : SampleView
{
    /// <summary>
    /// Check the application theme is light or dark.
    /// </summary>
    private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

	public CustomLabel()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        customlabelsGauge.Handler?.DisconnectHandler();
    }

    private void RadialAxis_LabelCreated1(object sender, LabelCreatedEventArgs e)
    {
        if (e.Text == "10")
        {
            e.Text = "NE";
        }
        else if (e.Text == "20")
        {
            e.Text = "E";
        }
        else if (e.Text == "30")
        {
            e.Text = "SE";
        }
        else if (e.Text == "40")
        {
            e.Text = "S";
        }
        else if (e.Text == "50")
        {
            e.Text = "SW";
        }
        else if (e.Text == "60")
        {
            e.Text = "W";
        }
        else if (e.Text == "70")
        {
            e.Text = "NW";
        }
        else if (e.Text == "80")
        {
            e.Text = "N";
            e.Style = new GaugeLabelStyle()
            {
                FontAttributes = Microsoft.Maui.Controls.FontAttributes.Bold,
                TextColor = isLightTheme ? Colors.Black : Colors.White,
            };
        }
    }
}