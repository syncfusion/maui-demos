#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.PdfViewer.SfPdfViewer;
using Syncfusion.Maui.Sliders;
using Syncfusion.Maui.TabView;
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class FreeTextFillColorPalatte : ContentView
{
    Ellipse? selectedColorButtonHighlight;
    Ellipse? selectedColorButtonHighlightStroke;
    Ellipse? selectedFontColorHighlight;

    Button? PreButton = null;
    internal void Initialize()
    {
        InitializeComponent();
        colorPaletteBorder.Content = MyGrid;
        this.Content = colorPaletteBorder;
        tabView.SelectionChanged += OnSelectionChanged;
        tabView.LayoutChanged += TabView_LayoutChanged;
        this.PropertyChanged += FreeTextFillColorPalatte_PropertyChanged;
    }

    private void TabView_LayoutChanged(object? sender, EventArgs e)
    {
        if (sender is View view && view.Parent is Grid grid)
        {
            if(Text.IsSelected)
            {
                colorPaletteBorder.HeightRequest = 320;
                Fill.TextColor = Color.FromArgb("#49454F");
                Stroke.TextColor = Color.FromArgb("#49454F");
                Text.TextColor = Color.FromArgb("#6750A4");
            }
        }
    }

    private void FreeTextFillColorPalatte_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                if (PreButton != null && selectedFontColorHighlight != null)
                {
                    PreButton.HeightRequest = 30;
                    PreButton.WidthRequest = 30;
                    PreButton.CornerRadius = 15;
                    selectedFontColorHighlight.Stroke = Brush.Transparent;
                    if (selectedFontColorHighlight.Parent != null)
                    {
                        FontColor.Children.Remove(selectedFontColorHighlight);
                    }
                    selectedFontColorHighlight = null;
                }
            }
        }
    }

#if MACCATALYST
    Frame colorPaletteBorder = new Frame()
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
        WidthRequest = 290,
    };
#else
    Border colorPaletteBorder = new Border()
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
                    colorPaletteBorder.HeightRequest = 370;
                    Fill.TextColor = Color.FromArgb("#6750A4");
                    Stroke.TextColor = Color.FromArgb("#49454F");
                    Text.TextColor = Color.FromArgb("#49454F");
                }
                else if (Stroke.IsSelected)
                {
                    colorPaletteBorder.HeightRequest = 390;
                    Stroke.TextColor = Color.FromArgb("#6750A4");
                    Fill.TextColor = Color.FromArgb("#49454F");
                    Text.TextColor = Color.FromArgb("#49454F");
                }
                else if(Text.IsSelected)
                {
                    colorPaletteBorder.HeightRequest = 320;
                    Fill.TextColor = Color.FromArgb("#49454F");
                    Stroke.TextColor = Color.FromArgb("#49454F");
                    Text.TextColor= Color.FromArgb("#6750A4");
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
                bindingContext.IsFreetextFillColorChanging = true;
                bindingContext.IsFreetextStrokeColorChanging = false;
                bindingContext.IsFreetextFontColorChanging = false;
                bindingContext.IsFreetextColorPalatteisible = false;
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }

        }
        if (selectedColorButtonHighlight.Parent == null)
        {
            ColorFill.Children.Add(selectedColorButtonHighlight);
        }
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.IsFreeTextFillColorVisble = false;
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
                bindingContext.IsFreetextStrokeColorChanging = true;
                bindingContext.IsFreetextFillColorChanging = false;
                bindingContext.IsFreetextFontColorChanging = false;
                bindingContext.IsDeskTopFillColorToolbarVisible = false;
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }
        }
        if (selectedColorButtonHighlightStroke.Parent == null)
        {
            ColorStroke.Children.Add(selectedColorButtonHighlightStroke);
        }
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.IsFreeTextFillColorVisble = false;
        }
    }
    private void ShapeStrokeOpacitySlidervalue_Chnaged(object sender, EventArgs e)
    {
        float opacity = (float)shapeStrokeOpacitySlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsFreetextStrokeColorChanging = true;
            bindingContext.IsFreetextFillColorChanging = false;
            bindingContext.IsFreetextFontColorChanging = false;
            bindingContext.OpacityCommand.Execute(opacity);
        }
    }

    private void ShapeFillColorOpacitySlidervalue_Chnaged(object sender, EventArgs e)
    {
        float opacity = (float)shapeFillColorOpacitySlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsFreetextFillColorChanging = true;
            bindingContext.IsFreetextStrokeColorChanging = false;
            bindingContext.IsFreetextFontColorChanging = false;
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
            button.BackgroundColor = Colors.Transparent;
            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                bindingContext.IsFreetextStrokeColorChanging = true;
                bindingContext.IsFreetextFillColorChanging = false;
                bindingContext.IsFreetextFontColorChanging = false;
                bindingContext.IsDeskTopFillColorToolbarVisible = false;
                bindingContext.IsDeskTopColorToolbarVisible = false;
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }
        }
    }

    private void FillNoColor_Clicked(object sender, EventArgs e)
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
                bindingContext.IsFreetextStrokeColorChanging = false;
                bindingContext.IsFreetextFillColorChanging = true;
                bindingContext.IsFreetextFontColorChanging = false;
                bindingContext.IsDeskTopFillColorToolbarVisible = false;
                bindingContext.ColorCommand.Execute(Colors.Transparent);
            }
        }
    }

   private void textOpacitySliderValueChanged(object sender, EventArgs e)
    {
        float opacity = (float)textOpacitySlider.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsFreetextFillColorChanging = false;
            bindingContext.IsFreetextStrokeColorChanging = false;
            bindingContext.IsFreetextFontColorChanging = true;
            bindingContext.OpacityCommand.Execute(opacity);
        }
    }
    private void BorderNoColor_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 30;
            PreButton.WidthRequest = 30;
            PreButton.CornerRadius = 15;
            ColorFill.Children.Remove(selectedColorButtonHighlightStroke);
            selectedColorButtonHighlight = null;
        }
        if (sender is Button button)
        {
            float opacity = 0;
            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                bindingContext.IsFreetextFillColorChanging = false;
                bindingContext.IsFreetextFontColorChanging = false;
                bindingContext.IsDeskTopFillColorToolbarVisible = false;
                bindingContext.IsFreetextStrokeColorChanging = true;
                bindingContext.OpacityCommand.Execute(opacity);
            }
        }
    }

    private void FontColorButton_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 30;
            PreButton.WidthRequest = 30;
            PreButton.CornerRadius = 15;
        }
        PreButton = sender as Button;
        if (selectedFontColorHighlight == null)
        {
            selectedFontColorHighlight = new Ellipse();
            selectedFontColorHighlight.WidthRequest = 30;
            selectedFontColorHighlight.HeightRequest = 30;
            selectedFontColorHighlight.VerticalOptions = LayoutOptions.Center;
            selectedFontColorHighlight.HorizontalOptions = LayoutOptions.Center;
            selectedFontColorHighlight.Stroke = Brush.Black;
            selectedFontColorHighlight.StrokeThickness = 2;
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
            Grid.SetColumn(selectedFontColorHighlight, column);
            Grid.SetRow(selectedFontColorHighlight, row);
            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                bindingContext.IsFreetextFillColorChanging = false;
                bindingContext.IsFreetextStrokeColorChanging = false;
                bindingContext.IsFreetextColorPalatteisible = false;
                bindingContext.IsFreetextFontColorChanging = true;
                bindingContext.ColorCommand.Execute(button.BackgroundColor);
            }

        }
        if (selectedFontColorHighlight.Parent == null)
        {
            FontColor.Children.Add(selectedFontColorHighlight);
        }
        if(BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.IsFreeTextFillColorVisble = false;
        }
    }
}