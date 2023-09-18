#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class StampDialogMobile : ContentView
{
    Ellipse? selectedColorButtonHighlight;
    Button? PreButton;

    public event EventHandler<CustomStampEventArgs?>? CustomStampCreated;
    public StampDialogMobile()
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
        else if(e.PropertyName == "IsVisible" && this.IsVisible == false)
        {
            InitializeDialog();
        }
    }

    private void OnBackButtonClicked(object sender, EventArgs e)
    {
		this.IsVisible = false;
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

    private void ColorHighlightButton_Clicked(object sender, EventArgs e)
    {
        if (PreButton != null)
        {
            PreButton.HeightRequest = 35;
            PreButton.WidthRequest = 35;
            PreButton.CornerRadius = 20;
        }
        PreButton = sender as Button;
        if (selectedColorButtonHighlight == null)
        {
            selectedColorButtonHighlight = new Ellipse();
            selectedColorButtonHighlight.WidthRequest = 35;
            selectedColorButtonHighlight.HeightRequest = 35;
            selectedColorButtonHighlight.VerticalOptions = LayoutOptions.Center;
            selectedColorButtonHighlight.HorizontalOptions = LayoutOptions.Center;
            selectedColorButtonHighlight.Stroke = Brush.Black;
            selectedColorButtonHighlight.StrokeThickness = 2;
        }
        if (sender is Button button)
        {
            button.HeightRequest = 28;
            button.WidthRequest = 28;
            button.CornerRadius = 14;
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

    private void OnApplyButtonClicked(object sender, EventArgs e)
    {
        CustomStampEventArgs customStampEventArgs = new CustomStampEventArgs(border);
        CustomStampCreated?.Invoke(this, customStampEventArgs);
    }

    void InitializeDialog()
    {
        stampColor.Children.Remove(selectedColorButtonHighlight);
        label.Text = "Text";
        border.Stroke = Color.FromArgb("#A007A3");
        border.BackgroundColor = Color.FromArgb("#FADFFB");
        label.TextColor = Color.FromArgb("#A007A3");
        border.WidthRequest = 80;
        createEntry.Text = "";
        if(PreButton != null)
        {
            PreButton.HeightRequest = 35;
            PreButton.WidthRequest = 35;
            PreButton.CornerRadius = 20;
        }
    }
}