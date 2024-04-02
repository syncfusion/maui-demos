#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class StampDialog : ContentView
{
    Ellipse? selectedColorButtonHighlight;
    Button? PreButton;

    public event EventHandler<CustomStampEventArgs?>? CustomStampCreated;

    internal void Initialize()
    {
        InitializeComponent();
        this.PropertyChanged += StampDialogPropertyChanged;
    }

    private async void StampDialogPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsVisible" && this.IsVisible == true)
        {
            await Task.Delay(250);
            createEntry?.Focus();
        }
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            label.Text = "Text";
        }
        else
        {
            label.Text = e.NewTextValue;
        }
        double textWidth = MeasureTextWidth(label.Text, label.FontSize);
        double newBorderWidth = textWidth + 50;
        border.WidthRequest = newBorderWidth;
        layout.WidthRequest = newBorderWidth;
    }

    private double MeasureTextWidth(string text, double fontSize)
    {
        label.Text = text;
        SizeRequest sizeRequest = label.Measure(double.PositiveInfinity, double.PositiveInfinity);
        return sizeRequest.Request.Width;
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        this.IsVisible = false;
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.StampHighlightColor = Colors.Transparent;
        }
    }

    private void OnApplyClicked(object sender, EventArgs e)
    {
        CustomStampEventArgs customStampEventArgs = new CustomStampEventArgs(border, label);
        CustomStampCreated?.Invoke(this, customStampEventArgs);
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ResetLayouts();
        }
    }

    private void ColorHighlightButton_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 40;
            PreButton.WidthRequest = 40;
            PreButton.CornerRadius = 20;
        }
        PreButton = sender as Button;
        if (selectedColorButtonHighlight == null)
        {
            selectedColorButtonHighlight = new Ellipse();
            selectedColorButtonHighlight.WidthRequest = 40;
            selectedColorButtonHighlight.HeightRequest = 40;
            selectedColorButtonHighlight.VerticalOptions = LayoutOptions.Center;
            selectedColorButtonHighlight.HorizontalOptions = LayoutOptions.Center;
            selectedColorButtonHighlight.Stroke = Brush.Black;
            selectedColorButtonHighlight.StrokeThickness = 2;
        }
        if (sender is Button button)
        {
            button.HeightRequest = 32;
            button.WidthRequest = 32;
            button.CornerRadius = 16;
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.Center;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            Grid.SetColumn(selectedColorButtonHighlight, column);
            Grid.SetRow(selectedColorButtonHighlight, row);
            border.Stroke = button.BackgroundColor;
            border.BackgroundColor = GetLightenColor(button.BackgroundColor, (float)0.85);
            label.TextColor = button.BackgroundColor;
        }
        if (selectedColorButtonHighlight.Parent == null)
        {
            stampColor.Children.Add(selectedColorButtonHighlight);
        }
    }

    internal Color GetLightenColor(Color color, float lighteningFactor)
    {
        float R = (float)color.Red;
        float G = (float)color.Green;
        float B = (float)color.Blue;

        if (lighteningFactor < -1)
            lighteningFactor = -1;
        else if (lighteningFactor > 1)
            lighteningFactor = 1;

        if (lighteningFactor < 0)
        {
            lighteningFactor = 1 + lighteningFactor;
            R *= lighteningFactor;
            G *= lighteningFactor;
            B *= lighteningFactor;
        }
        else
        {
            R = (1 - R) * lighteningFactor + R;
            G = (1 - G) * lighteningFactor + G;
            B = (1 - B) * lighteningFactor + B;
        }
        return Color.FromRgba((float)R, (float)G, (float)B, color.Alpha);
    }
}

public class StampImage : Image
{
    public Stream? ImageStream { get; set; }

    public StampImage()
    {
        this.HorizontalOptions = LayoutOptions.Center;
    }
}

public class CustomStampEventArgs : EventArgs
{
    public Border? StampView { get; set; }
    public Label? CustomStampLabel { get; set; }

    public CustomStampEventArgs(Border? stampView, Label? customStampLabel)
    {
        StampView = stampView;
        CustomStampLabel = customStampLabel;
    }

    public CustomStampEventArgs(Border? stampeView)
    {
        StampView = stampeView;
    }
}