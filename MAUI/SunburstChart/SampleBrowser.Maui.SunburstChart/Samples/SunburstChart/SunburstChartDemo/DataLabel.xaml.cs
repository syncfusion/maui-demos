#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.SunburstChart;
using MAUIPicker = Microsoft.Maui.Controls.Picker;
namespace SampleBrowser.Maui.SunburstChart.SfSunburstChart;

public partial class DataLabel : SampleView
{
    public DataLabel()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        sunburstChart.Handler?.DisconnectHandler();
    }

    private void ModePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (MAUIPicker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex == 0)
        {
            dataLabel.RotationMode = SunburstLabelRotationMode.Angle;
        }
        else
        {
            dataLabel.RotationMode = SunburstLabelRotationMode.Normal;
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (MAUIPicker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex == 0)
        {
            dataLabel.OverFlowMode = SunburstLabelOverflowMode.Trim;
        }
        else
        {
            dataLabel.OverFlowMode = SunburstLabelOverflowMode.Hide;
        }
    }
}