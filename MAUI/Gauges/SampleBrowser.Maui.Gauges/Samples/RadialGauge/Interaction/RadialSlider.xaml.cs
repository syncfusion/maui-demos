#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
using ValueChangedEventArgs = Syncfusion.Maui.Gauges.ValueChangedEventArgs;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class RadialSlider : SampleView
{
	public RadialSlider()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        stateGauge.Handler?.DisconnectHandler();
    }

    private void markerPointer_ValueChanging(object sender, ValueChangingEventArgs e)
    {
        if (Math.Abs(e.NewValue - e.OldValue) > 20)
            e.Cancel = true;
        else
        {
            double value = e.NewValue;
            value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);

            annotationLabel.Text = value.ToString() + "%";
        }
    }
}