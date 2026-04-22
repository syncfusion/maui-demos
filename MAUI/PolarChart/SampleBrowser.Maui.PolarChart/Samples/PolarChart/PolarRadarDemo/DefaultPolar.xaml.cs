#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.PolarChart.SfPolarChart;

public partial class DefaultPolar : SampleView
{
    public DefaultPolar()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        Chart.Handler?.DisconnectHandler();
    }

    private void Switch_StateChanged(object? sender, SwitchStateChangedEventArgs e)
    {
        bool state = e.NewValue ?? false;

        if (area1 is null || area2 is null || area3 is null ||
             line1 is null || line2 is null || line3 is null)
        {
            return;
        }

        area1.IsClosed = area2.IsClosed = area3.IsClosed = state;
        line1.IsClosed = line2.IsClosed = line3.IsClosed = state;
    }

    private void Type_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (sender is not SfComboBox comboBox)
            return;

        int selectedIndex = comboBox.SelectedIndex;
        if (area1 is null || area2 is null || area3 is null ||
            line1 is null || line2 is null || line3 is null)
        {
            return;
        }

        if (selectedIndex == 0)
        {
            area1.IsVisible = area2.IsVisible = area3.IsVisible = true;
            line1.IsVisible = line2.IsVisible = line3.IsVisible = false;

        }
        else if (selectedIndex == 1)
        {
            line1.IsVisible = line2.IsVisible = line3.IsVisible = true;
            area1.IsVisible = area2.IsVisible = area3.IsVisible = false;
        }
    }

    private void Angle_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (sender is not SfComboBox comboBox)
            return;

        int selectedIndex = comboBox.SelectedIndex;
        if (selectedIndex == 0)
        {
            Chart.StartAngle = ChartPolarAngle.Rotate0;
        }
        else if (selectedIndex == 1)
        {
            Chart.StartAngle = ChartPolarAngle.Rotate90;
        }
        else if (selectedIndex == 2)
        {
            Chart.StartAngle = ChartPolarAngle.Rotate180;
        }
        else
        {
            Chart.StartAngle = ChartPolarAngle.Rotate270;
        }
    }
}