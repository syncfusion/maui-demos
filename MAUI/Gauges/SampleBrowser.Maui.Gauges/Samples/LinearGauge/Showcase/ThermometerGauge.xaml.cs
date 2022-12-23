#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class ThermometerGauge : SampleView
{
	public ThermometerGauge()
	{
		InitializeComponent();
	}

    #region Thermometer

    private void thermoMeterShape_ValueChanging(object sender, ValueChangingEventArgs e)
    {
        if (e.NewValue > 50 || e.NewValue < -18)
        {
            e.Cancel = true;
        }

        if (e.NewValue > 38)
        {
            thermoMeterShape.Fill = new SolidColorBrush(Color.FromRgb(255, 123, 123));
        }
        else
        {
            thermoMeterShape.Fill = new SolidColorBrush(Color.FromRgb(0, 116, 227));
        }
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        thermometerGauge1.Handler?.DisconnectHandler();
        thermometerGauge2.Handler?.DisconnectHandler();
    }

    #endregion
}