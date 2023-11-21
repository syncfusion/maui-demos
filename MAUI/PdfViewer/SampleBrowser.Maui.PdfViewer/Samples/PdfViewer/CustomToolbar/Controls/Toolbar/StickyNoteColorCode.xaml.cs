#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class StickyNoteColorCode : ContentView
{
    Ellipse? selectedColorButtonHighlight;

    Button? PreButton = null;
    public StickyNoteColorCode()
    {
        InitializeComponent();
        Colorpaletteborder.Content = MyGrid;
        this.Content = Colorpaletteborder;
    }

#if MACCATALYST
    Frame Colorpaletteborder = new Frame()
    {
        BackgroundColor = Color.FromArgb("#EEE8F4"),
        BorderColor = Color.FromArgb("#26000000"),
        Padding = new Thickness(0),
        VerticalOptions = LayoutOptions.Start,
        HorizontalOptions = LayoutOptions.Start,
        CornerRadius = 12,
        Shadow = new Shadow
        {
            Offset = new Point(-1, 0),
            Brush = Color.FromRgba("#000000"),
            Radius = 8,
            Opacity = 0.5f
        },
        WidthRequest = 280,
    };
#else
    Border Colorpaletteborder = new Border()
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
            Radius = 8,
            Opacity = 0.5f
        },
        WidthRequest = 280,
    };
#endif
    private void ColorHeighlightButton_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 30;
            PreButton.WidthRequest = 30;
            PreButton.CornerRadius = 15;
        }
        PreButton = sender as Button;
        if (selectedColorButtonHighlight == null)
        {
            selectedColorButtonHighlight = new Ellipse();
            selectedColorButtonHighlight.WidthRequest = 30;
            selectedColorButtonHighlight.HeightRequest = 30;
            selectedColorButtonHighlight.VerticalOptions = LayoutOptions.Center;
            selectedColorButtonHighlight.HorizontalOptions = LayoutOptions.Center;
            selectedColorButtonHighlight.Stroke = Brush.Black;
            selectedColorButtonHighlight.StrokeThickness = 2;
        }
        if (sender is Button button)
        {
            button.HeightRequest = 22;
            button.WidthRequest = 22;
            button.CornerRadius = 11;
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.Center;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            Grid.SetColumn(selectedColorButtonHighlight, column);
            Grid.SetRow(selectedColorButtonHighlight, row);
            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                bindingContext.IsDeskTopColorToolbarVisible = true;
                bindingContext.IsDeskTopFillColorToolbarVisible = false;
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }
        }
        if (selectedColorButtonHighlight.Parent == null)
        {
            MyTextMarkup.Children.Add(selectedColorButtonHighlight);
        }
    }

    private void StickyNoteOpacitySliderChanged(object sender, EventArgs e)
    {
        float opacity = (float)stickyNoteOpacitySlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDeskTopColorToolbarVisible = true;
            bindingContext.IsDeskTopFillColorToolbarVisible = false;
            bindingContext.OpacityCommand.Execute(opacity);
        }
    }
}