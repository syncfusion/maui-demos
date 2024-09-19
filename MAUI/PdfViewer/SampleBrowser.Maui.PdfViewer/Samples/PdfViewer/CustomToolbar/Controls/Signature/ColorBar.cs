#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.Themes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class ColorBar : Grid
    {
        Ellipse selectedColorButtonHighlight;

        private static readonly BindableProperty SignatureViewColorSelectorColorProperty = BindableProperty.Create("IntegratedSignatureViewColorSelectorColorProperty", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#1C1B1F"));

        internal ColorBar()
        {
            ApplyDynamicStyles();
            selectedColorButtonHighlight = new Ellipse()
            {
                WidthRequest = 32,
                HeightRequest = 32,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                StrokeThickness = 2
            };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                selectedColorButtonHighlight.SetAppThemeColor(Ellipse.StrokeProperty,
                    (Color)Application.Current.Resources["PrimaryBackgroundLight"],
                    (Color)Application.Current.Resources["PrimaryBackground"]);
            }

            HeightRequest = 64;
            ColumnSpacing = 10;
            ColumnDefinitions.Add(new ColumnDefinition { Width = 32 });
            ColumnDefinitions.Add(new ColumnDefinition { Width = 32 });
            ColumnDefinitions.Add(new ColumnDefinition { Width = 32 });
            ColumnDefinitions.Add(new ColumnDefinition { Width = 32 });
            TapGestureRecognizer colorButtonGesture = new TapGestureRecognizer();
            colorButtonGesture.Tapped += ColorButtonGesture_Tapped;
            Ellipse colorOne = new Ellipse
            {
                BackgroundColor = Color.FromArgb("#49454F"),
                WidthRequest = 24,
                HeightRequest = 24,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            colorOne.GestureRecognizers.Add(colorButtonGesture);
            Ellipse colorTwo = new Ellipse
            {
                BackgroundColor = Color.FromArgb("#3678DA"),
                WidthRequest = 24,
                HeightRequest = 24,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            colorTwo.GestureRecognizers.Add(colorButtonGesture);
            Ellipse colorThree = new Ellipse
            {
                BackgroundColor = Color.FromArgb("#01A512"),
                WidthRequest = 24,
                HeightRequest = 24,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            colorThree.GestureRecognizers.Add(colorButtonGesture);
            Ellipse colorFour = new Ellipse
            {
                BackgroundColor = Color.FromArgb("#C43939"),
                WidthRequest = 24,
                HeightRequest = 24,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            colorFour.GestureRecognizers.Add(colorButtonGesture);
            Grid.SetColumn(colorOne, 0);
            Grid.SetColumn(colorTwo, 1);
            Grid.SetColumn(colorThree, 2);
            Grid.SetColumn(colorFour, 3);
            Grid.SetColumn(selectedColorButtonHighlight, 0);
            Children.Add(selectedColorButtonHighlight);
            Children.Add(colorOne);
            Children.Add(colorTwo);
            Children.Add(colorThree);
            Children.Add(colorFour);
        }

        private void ApplyDynamicStyles()
        {
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {
            
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureViewColorSelectorColorProperty, "SfPdfViewerSignatureViewColorSelectorColor");
        }

        private void ColorButtonGesture_Tapped(object? sender, TappedEventArgs e)
        {
            if (sender is Ellipse selectedButton)
            {
                int column = Grid.GetColumn(selectedButton);
                int row = Grid.GetRow(selectedButton);
                Grid.SetColumn(selectedColorButtonHighlight, column);
                Grid.SetRow(selectedColorButtonHighlight, row);
                if (BindingContext is SignatureViewModel viewModel)
                    viewModel.SelectedColor = selectedButton.BackgroundColor;
            }
        }
    }
}
