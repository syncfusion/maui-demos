#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class VolumeSettings : SampleView
{
	public VolumeSettings()
	{
		InitializeComponent();
	}


    #region Volume settings

    private void BarPointer_ValueChanging(object sender, ValueChangingEventArgs e)
    {
        if (e.NewValue > 100)
        {
            e.Cancel = true;
            return;
        }

        if (sender is BarPointer pointer)
        {
            string text = AutomationProperties.GetHelpText(pointer);
            double value = e.NewValue;
            value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);
            if (text == "firstVolumeLabel")
            {
                firstVolumeLabel.Text = value.ToString() + "%";
                firstLabelIcon.Text = value == 0 ? "\uE70A" : "\uE706";
            }
            else if (text == "secondVolumeLabel")
            {
                secondVolumeLabel.Text = value.ToString() + "%";
                secondLabelIcon.Text = value == 0 ? "\uE70B" : "\uE709";
            }
            else if (text == "thirdVolumeLabel")
            {
                thirdVolumeLabel.Text = value.ToString() + "%";
                thirdLabelIcon.Text = value == 0 ? "\uE70C" : "\uE707";
            }
        }
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        volumeSettingsGauge1.Handler?.DisconnectHandler();
        volumeSettingsGauge2.Handler?.DisconnectHandler();
        volumeSettingsGauge3.Handler?.DisconnectHandler();
    }
    #endregion
}