#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
public partial class ThicknessSlider : ContentView
{
	internal void Initialize()
    {
        InitializeComponent();
        Thicknessborder.Content = MyGrid;
        this.Content = Thicknessborder;
    }
    Border Thicknessborder = new Border()
    {
        BackgroundColor = Color.FromArgb("#EEE8F4"),
        Stroke = Color.FromArgb("#26000000"),
        StrokeThickness = 1,
        VerticalOptions = LayoutOptions.Start,
        HorizontalOptions = LayoutOptions.Start,
        StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(12)
        },
        Shadow = new Shadow
        {
            Offset = new Point(-1, 0),
            Brush = Color.FromRgba("#000000"),
            Radius = 5,
            Opacity = 0.3f
        },
        WidthRequest = 280,
    };

    private void ThicknessValue_Changed(object sender, EventArgs e)
    {
        float thickness = (float)thicknessSlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDeskTopColorToolbarVisible = true;
            bindingContext.IsDeskTopFillColorToolbarVisible = false;
            bindingContext.IsFreetextStrokeColorChanging = true;
            bindingContext.ThicknessCommand.Execute(thickness);
        }
    }
}