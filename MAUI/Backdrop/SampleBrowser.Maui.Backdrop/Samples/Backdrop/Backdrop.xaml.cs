#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Backdrop.SfBackdropPage;
using Syncfusion.Maui.Backdrop;
using Syncfusion.Maui.Buttons;

public partial class Backdrop : SfBackdropPage
{
    string edgeShapeSide;

    /// <summary>
    /// Check the application theme is light or dark.
    /// </summary>
    private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

    public Backdrop()
	{
		InitializeComponent();
        edgeShapeSide = "Both";
    }

    private void curveButton_Clicked(object sender, EventArgs e)
    {
        this.FrontLayer.EdgeShape = EdgeShape.Curve;
        this.curveButton.IsChecked = true;
        this.flatButton.IsChecked = false;
    }

    private void flatButton_Clicked(object sender, EventArgs e)
    {
        this.FrontLayer.EdgeShape = EdgeShape.Flat;
        this.flatButton.IsChecked = true;
        this.curveButton.IsChecked = false;
    }

    private void bothButton_Clicked(object sender, EventArgs e)
    {
        edgeShapeSide = "Both";
        FrontLayer.LeftCornerRadius = cornerRadiusSlider.Value;
        FrontLayer.RightCornerRadius = cornerRadiusSlider.Value;
        this.bothButton.IsChecked = true;
        this.leftButton.IsChecked = false;
        this.rightButton.IsChecked = false;
    }

    private void leftButton_Clicked(object sender, EventArgs e)
    {
        edgeShapeSide = "Left";
        FrontLayer.RightCornerRadius = 0;
        FrontLayer.LeftCornerRadius = cornerRadiusSlider.Value;
        this.bothButton.IsChecked = false;
        this.leftButton.IsChecked = true;
        this.rightButton.IsChecked = false;
    }

    private void rightButton_Clicked(object sender, EventArgs e)
    {
        edgeShapeSide = "Right";
        FrontLayer.LeftCornerRadius = 0;
        FrontLayer.RightCornerRadius = cornerRadiusSlider.Value;
        this.bothButton.IsChecked = false;
        this.leftButton.IsChecked = false;
        this.rightButton.IsChecked = true;
    }

    private void autoButton_Clicked(object sender, EventArgs e)
    {
        this.BackLayerRevealOption = RevealOption.Auto;
        this.autoButton.IsChecked = true;
        this.fillButton.IsChecked = false;
    }

    private void fillButton_Clicked(object sender, EventArgs e)
    {
        this.BackLayerRevealOption = RevealOption.Fill;
        this.fillButton.IsChecked = true;
        this.autoButton.IsChecked = false;
    }

    private void cornerRadiusSlider_ValueChanged(object sender, Syncfusion.Maui.Sliders.SliderValueChangedEventArgs e)
    {
        switch (edgeShapeSide)
        {
            case "Right":
                FrontLayer.RightCornerRadius = e.NewValue;
                break;
            case "Left":
                FrontLayer.LeftCornerRadius = e.NewValue;
                break;
            case "Both":
                FrontLayer.RightCornerRadius = e.NewValue;
                FrontLayer.LeftCornerRadius = e.NewValue;
                break;
        }
    }
    private void BackButtonLabel_Tapped(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}