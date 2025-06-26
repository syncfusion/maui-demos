#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfColorPicker;

public partial class ColorPickerDrawingPad : SampleView
{
    public ColorPickerDrawingPad()
	{
		InitializeComponent();
#if ANDROID
        Update();
#endif
    }

    private async void Update()
    {
        await Task.Delay(100);
        colorPicker.PaletteColumnCount = 5;
    }

    private void PenGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        signaturePad.MaximumStrokeThickness = 2;
    }

    private void BrushGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        signaturePad.MaximumStrokeThickness = 7;
    }

    private void Reset_Tapped(object sender, TappedEventArgs e)
    {
        signaturePad.Clear();
    }
}