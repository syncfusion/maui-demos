#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class AxisBackground : SampleView
{
	public AxisBackground()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        axisBackgroundGauge.Handler?.DisconnectHandler();
    }

    private void RadialAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
    {
        if (e.Text == "90")
        {
            e.Text = "E";
        }
        else if (e.Text == "360")
        {
            e.Text = string.Empty;
        }
        else
        {
            if (e.Text == "0")
            {
                e.Text = "N";
            }
            else if (e.Text == "180")
            {
                e.Text = "S";
            }
            else if (e.Text == "270")
            {
                e.Text = "W";
            }
        }
    }
}