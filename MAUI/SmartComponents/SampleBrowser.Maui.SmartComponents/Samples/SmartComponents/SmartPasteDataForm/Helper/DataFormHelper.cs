#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using Syncfusion.Maui.Popup;

    internal static class DataFormHelper
    {
        /// <summary>
        /// Method to get the dynamic color.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <returns>The color.</returns>
        internal static Color GetDynamicColor(string? resourceName = null)
        {
            if (Application.Current == null)
            {
                return Colors.Transparent;
            }

            var requestedTheme = Application.Current.RequestedTheme;
            if (resourceName != null && Application.Current.Resources.TryGetValue(resourceName, out var colorValue) && colorValue is Color color)
            {
                return color;
            }
            else if (requestedTheme == AppTheme.Light)
            {
                return Color.FromRgb(0xFF, 0xFF, 0xFF);
            }
            else if (requestedTheme == AppTheme.Dark)
            {
                return Color.FromRgb(0x38, 0x1E, 0x72);
            }

            return Colors.Transparent;
        }

        /// <summary>
        /// Method to get the Ok button style.
        /// </summary>
        /// <returns>The button style.</returns>
        internal static Style GetOkButtonStyle()
        {
            return new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter { Property = Button.CornerRadiusProperty, Value = 20 },
                    new Setter { Property = Button.BorderColorProperty, Value = Color.FromArgb("#6750A4") },
                    new Setter { Property = Button.BorderWidthProperty, Value = 1 },
                    new Setter { Property = VisualElement.BackgroundColorProperty, Value = GetDynamicColor("SfDataFormFocusedEditorStroke") },
                    new Setter { Property = Button.TextColorProperty, Value = GetDynamicColor() },
                    new Setter { Property = Button.FontSizeProperty, Value = 14 },
                }
            };
        }

        /// <summary>
        /// Method to get the footer template.
        /// </summary>
        /// <param name="popup">The pop up.</param>
        /// <returns>The data template.</returns>
        internal static DataTemplate GetFooterTemplate(SfPopup popup)
        {
            var footerTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    ColumnSpacing = 12,
                    Padding = new Thickness(24)
                };

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                var oKButton = new Button
                {
                    Text = "OK",
                    Style = GetOkButtonStyle(),
                    WidthRequest = 96,
                    HeightRequest = 40
                };

                oKButton.Clicked += (sender, args) =>
                {
                    popup.Dismiss();
                };

                grid.Children.Add(oKButton);
                Grid.SetColumn(oKButton, 1);

                return grid;
            });

            return footerTemplate;
        }

        /// <summary>
        /// Method to get the content template.
        /// </summary>
        /// <param name="popup">The pop up.</param>
        /// <param name="isValidate">Check whether is validate.</param>
        /// <returns>The data template.</returns>
        internal static DataTemplate GetContentTemplate(SfPopup popup, bool? isValidate = false)
        {
            if (!isValidate.HasValue)
            {
                return new DataTemplate();
            }

            var footerTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.1, GridUnitType.Star) });
                var label = new Label
                {
                    LineBreakMode = LineBreakMode.WordWrap,
                    Padding = (bool)isValidate ? new Thickness(24, 0, 0, 0) : new Thickness(24, 24, 0, 0),
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start
                };

                label.BindingContext = popup;
                label.SetBinding(Label.TextProperty, "Message");
                var stackLayout = new StackLayout
                {
                    Margin = (bool)!isValidate ? new Thickness(0, 10, 0, 0) : new Thickness(0, 2, 0, 0),
                    HeightRequest = 1,
                };

                stackLayout.SetDynamicResource(VisualElement.BackgroundColorProperty, "SfDataFormDisabledEditorStroke");
                grid.Children.Add(label);
                grid.Children.Add(stackLayout);
                Grid.SetRow(label, 0);
                Grid.SetRow(stackLayout, 1);

                return grid;
            });

            return footerTemplate;
        }
    }
}
