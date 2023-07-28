#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Backdrop.SfBackdropPage;
using Syncfusion.Maui.Backdrop;
using Syncfusion.Maui.Sliders;

public partial class Backdrop : SfBackdropPage
{
    string edgeShapeSide;
    Color pressedColor;
    Color normalColor;
    public Backdrop()
	{
		InitializeComponent();
        edgeShapeSide = "Both";
        pressedColor = Color.FromArgb("#3DFFFFFF");
        normalColor = Color.FromArgb("#1F000000");
    }

    private void curveButton_Clicked(object sender, EventArgs e)
    {
        this.FrontLayer.EdgeShape = EdgeShape.Curve;
        this.curveButton.Background = pressedColor;
        this.flatButton.Background = normalColor;
    }

    private void flatButton_Clicked(object sender, EventArgs e)
    {
        this.FrontLayer.EdgeShape = EdgeShape.Flat;
        this.flatButton.Background = pressedColor;
        this.curveButton.Background = normalColor;
    }

    private void bothButton_Clicked(object sender, EventArgs e)
    {
        edgeShapeSide = "Both";
        FrontLayer.LeftCornerRadius = cornerRadiusSlider.Value;
        FrontLayer.RightCornerRadius = cornerRadiusSlider.Value;
        this.bothButton.Background = pressedColor;
        this.leftButton.Background = this.rightButton.Background = normalColor;
    }

    private void leftButton_Clicked(object sender, EventArgs e)
    {
        edgeShapeSide = "Left";
        FrontLayer.RightCornerRadius = 0;
        FrontLayer.LeftCornerRadius = cornerRadiusSlider.Value;
        this.leftButton.Background = pressedColor;
        this.rightButton.Background = this.bothButton.Background = normalColor;
    }

    private void rightButton_Clicked(object sender, EventArgs e)
    {
        edgeShapeSide = "Right";
        FrontLayer.LeftCornerRadius = 0;
        FrontLayer.RightCornerRadius = cornerRadiusSlider.Value;
        this.rightButton.Background = pressedColor;
        this.leftButton.Background = this.bothButton.Background = normalColor;
    }

    private void autoButton_Clicked(object sender, EventArgs e)
    {
        this.BackLayerRevealOption = RevealOption.Auto;
        this.autoButton.Background = pressedColor;
        this.fillButton.Background = normalColor;
    }

    private void fillButton_Clicked(object sender, EventArgs e)
    {
        this.BackLayerRevealOption = RevealOption.Fill;
        this.fillButton.Background = pressedColor;
        this.autoButton.Background = normalColor;
    }

    private void cornerRadiusSlider_ValueChanged(object sender, SliderValueChangedEventArgs e)
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