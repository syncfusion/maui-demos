#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class TemperatureMeter : SampleView
{
	public TemperatureMeter()
	{
		InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        temperatureMeterGauge.Handler?.DisconnectHandler();
    }


    #region Temperature meter

    private void SfLinearGauge_LabelCreated_1(object sender, LabelCreatedEventArgs e)
    {
        e.Text = e.Text + "°C";
    }

    #endregion

}