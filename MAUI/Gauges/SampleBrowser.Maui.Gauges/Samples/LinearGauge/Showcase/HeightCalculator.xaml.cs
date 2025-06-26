#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class HeightCalculator : SampleView
{
	public HeightCalculator()
	{
		InitializeComponent();
	}

    #region Height calculator

    private void SfLinearGauge_LabelCreated(object sender, LabelCreatedEventArgs e)
    {
        e.Text = e.Text + " cm";
    }

    private void Pointer1_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
    {
        heightAnnotationLabel.Text = (int)e.Value + " cm";
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        heightCalculatorGauge.Handler?.DisconnectHandler();
    }
    #endregion
}