#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Inputs;
namespace SampleBrowser.Maui.LiquidGlass.SfColorPicker;

public partial class ColorPickerPage : SampleView
{
    public string[] ColorModeList { get; set; } = new string[] { "Spectrum", "Palette" };
    public ColorPickerPage()
    {
        InitializeComponent();
        colorModeCombobox.ItemsSource = ColorModeList;
    }

    private void colorModeCombobox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (colorModeCombobox.SelectedItem != null)
        {
            if (colorModeCombobox.SelectedIndex == 0)
            {
                colorPicker.ColorMode = ColorPickerMode.Spectrum;
            }
            else if (colorModeCombobox.SelectedIndex == 1)
            {
                colorPicker.ColorMode = ColorPickerMode.Palette;
            }
        }
    }

    private void ShowInputAreaCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            colorPicker.ShowInputArea = true;
        }
        else
        {
            colorPicker.ShowInputArea = false;
        }
    }

    private void ModePickerCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            colorPicker.IsColorModeSwitcherVisible = true;
        }
        else
        {
            colorPicker.IsColorModeSwitcherVisible = false;
        }
    }
    private void ShowSliderCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            colorPicker.ShowAlphaSlider = true;
        }
        else
        {
            colorPicker.ShowAlphaSlider = false;
        }
    }
    private void ShowRecentColorsCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            colorPicker.ShowRecentColors = true;
        }
        else
        {
            colorPicker.ShowRecentColors = false;
        }
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        colorPicker.ClearRecentColors();
    }

    private void ShowRoundedPallette_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            colorPicker.PaletteCellCornerRadius = 15;
        }
        else
        {

            colorPicker.PaletteCellCornerRadius = 0;
        }
    }
}
