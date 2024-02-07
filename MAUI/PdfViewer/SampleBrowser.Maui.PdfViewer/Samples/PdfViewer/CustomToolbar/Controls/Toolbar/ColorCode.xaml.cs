#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.PdfViewer.SfPdfViewer;
using Syncfusion.Maui.Sliders;
using Syncfusion.Maui.TabView;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class ColorCode : ContentView
{
    Ellipse? selectedColorButtonHighlight;
    Ellipse? selectedColorButtonHighlightStroke;

    Button? PreButton = null;
    public ColorCode()
    {
        InitializeComponent();

        Colorpaletteborder.Content = MyGrid;
        this.Content = Colorpaletteborder;
        tabView.SelectionChanged += OnSelectionChanged;
        this.PropertyChanged += ColorCode_PropertyChanged;
    }

    private void ColorCode_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsVisible))
        {
            if (IsVisible == false)
            {
                if (PreButton != null && selectedColorButtonHighlight != null)
                {
                    PreButton.HeightRequest = 30;
                    PreButton.WidthRequest = 30;
                    PreButton.CornerRadius = 15;
                    selectedColorButtonHighlight.Stroke = Brush.Transparent;
                    if (selectedColorButtonHighlight.Parent != null)
                    {
                        ColorFill.Children.Remove(selectedColorButtonHighlight);
                    }
                    selectedColorButtonHighlight = null;
                }
                if (PreButton != null && selectedColorButtonHighlightStroke != null)
                {
                    PreButton.HeightRequest = 30;
                    PreButton.WidthRequest = 30;
                    PreButton.CornerRadius = 15;
                    selectedColorButtonHighlightStroke.Stroke = Brush.Transparent;
                    if (selectedColorButtonHighlightStroke.Parent != null)
                    {
                        ColorStroke.Children.Remove(selectedColorButtonHighlightStroke);
                    }
                    selectedColorButtonHighlightStroke = null;
                }
            }
        }
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

    private void OnSelectionChanged(object? sender, TabSelectionChangedEventArgs e)
    {
        if (e.OldIndex != e.NewIndex)
        {
            if (sender is View view && view.Parent is Grid grid)
            {
                if (Fill.IsSelected)
                {
                    grid.HeightRequest = 350;
                    Fill.TextColor = Color.FromArgb("#6750A4");
                    Stroke.TextColor = Color.FromArgb("#49454F");
                }
                else if (Stroke.IsSelected)
                {
                    grid.HeightRequest = 370;
                    Stroke.TextColor = Color.FromArgb("#6750A4");
                    Fill.TextColor = Color.FromArgb("#49454F");
                }
                int row = Grid.GetRow(grid);
                Grid.SetRow(view, row);
            }
        }
    }

    private void ColorFillButton_Clicked(object sender, EventArgs e)
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
                bindingContext.IsDeskTopColorToolbarVisible = false;
                bindingContext.IsDeskTopFillColorToolbarVisible = true;
                bindingContext.CloseFreeTextColorPallete();
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }
                
        }
        if (selectedColorButtonHighlight.Parent == null)
        {
            ColorFill.Children.Add(selectedColorButtonHighlight);
        }
    }

    private void ColorStrokeButton_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 30;
            PreButton.WidthRequest = 30;
            PreButton.CornerRadius = 15;
        }
        PreButton = sender as Button;
        if (selectedColorButtonHighlightStroke == null)
        {
            selectedColorButtonHighlightStroke = new Ellipse();
            selectedColorButtonHighlightStroke.WidthRequest = 30;
            selectedColorButtonHighlightStroke.HeightRequest = 30;
            selectedColorButtonHighlightStroke.VerticalOptions = LayoutOptions.Center;
            selectedColorButtonHighlightStroke.HorizontalOptions = LayoutOptions.Center;
            selectedColorButtonHighlightStroke.Stroke = Brush.Black;
            selectedColorButtonHighlightStroke.StrokeThickness = 2;
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
            Grid.SetColumn(selectedColorButtonHighlightStroke, column);
            Grid.SetRow(selectedColorButtonHighlightStroke, row);
            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                bindingContext.IsDeskTopColorToolbarVisible = true;
                bindingContext.IsDeskTopFillColorToolbarVisible = false;
                bindingContext.CloseFreeTextColorPallete();
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }
        }
        if (selectedColorButtonHighlightStroke.Parent == null)
        {
            ColorStroke.Children.Add(selectedColorButtonHighlightStroke);
        }
    }

    private void thicknessSlider_valueChanged(object sender, EventArgs e)
    {
        float thickness = (float)desktopThicknessSlider.Value;

        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDeskTopColorToolbarVisible = true;
            bindingContext.IsDeskTopFillColorToolbarVisible = false;
            bindingContext.CloseFreeTextColorPallete();
            bindingContext.ThicknessCommand.Execute(thickness);
        }
            
    }

    private void ShapeStrokeOpacitySlidervalue_Chnaged(object sender, EventArgs e)
    {
        float opacity = (float)shapeStrokeOpacitySlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDeskTopColorToolbarVisible = true;
            bindingContext.IsDeskTopFillColorToolbarVisible = false;
            bindingContext.CloseFreeTextColorPallete();
            bindingContext.OpacityCommand.Execute(opacity);
        }
    }

    private void ShapeFillColorOpacitySlidervalue_Chnaged(object sender, EventArgs e)
    {
        float opacity = (float)shapeFillColorOpacitySlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDeskTopColorToolbarVisible = false;
            bindingContext.IsDeskTopFillColorToolbarVisible = true;
            bindingContext.CloseFreeTextColorPallete();
            bindingContext.OpacityCommand.Execute(opacity);
        }
    }

    private void NoColorButton_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 30;
            PreButton.WidthRequest = 30;
            PreButton.CornerRadius = 15;
            ColorFill.Children.Remove(selectedColorButtonHighlight);
            selectedColorButtonHighlight = null;
        }
        if (sender is Button button)
        {
            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                bindingContext.IsDeskTopColorToolbarVisible = false;
                bindingContext.IsDeskTopFillColorToolbarVisible = true;
                bindingContext.CloseFreeTextColorPallete();
                bindingContext.ColorCommand.Execute(Colors.Transparent);
            }
        }
    }
}