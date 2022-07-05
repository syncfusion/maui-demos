#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Core
{
    public class CardViewExt : ContentView
    {
        internal Image expandImageButton;
        private readonly Label titleLabel;

        private readonly Frame outerFrame;
        private readonly Grid parentGrid;

        private readonly Grid parentStack;
        internal Grid titleLayout;
        private readonly TapGestureRecognizer tapGestureRecognizer;

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty MainContentProperty =
            BindableProperty.Create(nameof(MainContent), typeof(View), typeof(CardViewExt), null);

        public View MainContent
        {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(CardViewExt), String.Empty, propertyChanged: OnTitleChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as CardViewExt)?.UpdateTitle((string)newValue);

        void UpdateTitle(string newValue)
        {
            this.titleLabel.Text = newValue;
            this.InvalidateLayout();


        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public CardViewExt()
        {
            this.tapGestureRecognizer = new TapGestureRecognizer();
            this.tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

            this.expandImageButton = new Image
            {
                WidthRequest = 25,
                HeightRequest = 25,
                Source = "expandicon.png"
            };

            if (!string.IsNullOrEmpty(RunTimeDevice.PlatformInfo))
            {
                if (RunTimeDevice.PlatformInfo.Equals("Android", StringComparison.OrdinalIgnoreCase))
                {
                    this.expandImageButton.Margin = new Microsoft.Maui.Thickness(0, 3, 5, 0);
                }
                else if (RunTimeDevice.PlatformInfo.Equals("MacCatalyst", StringComparison.OrdinalIgnoreCase))
                {
                    this.expandImageButton.Margin = new Microsoft.Maui.Thickness(0, 3, 0, 0);
                }
                else if (RunTimeDevice.PlatformInfo.Equals("ios", StringComparison.OrdinalIgnoreCase))
                {
                    this.expandImageButton.Margin = new Microsoft.Maui.Thickness(0, 3, 0, 0);
                }
            }

            this.expandImageButton.HorizontalOptions = LayoutOptions.End;
            this.expandImageButton.BackgroundColor = Colors.Transparent;

            this.titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = Microsoft.Maui.TextAlignment.Start,
                Text = this.Title,
                TextColor = Colors.Black,
                FontFamily = "Roboto",
                FontSize = 16
            };
            if (!string.IsNullOrEmpty(RunTimeDevice.PlatformInfo))
            {
                if (RunTimeDevice.PlatformInfo.Equals("Android", StringComparison.OrdinalIgnoreCase))
                {
                    this.titleLabel.Margin = new Microsoft.Maui.Thickness(0, -3, 0, 0);
                }
                else if (RunTimeDevice.PlatformInfo.Equals("ios", StringComparison.OrdinalIgnoreCase) || RunTimeDevice.PlatformInfo.Equals("MacCatalyst", StringComparison.OrdinalIgnoreCase))
                {
                    this.titleLabel.Margin = new Microsoft.Maui.Thickness(0, -18, 0, 0);
                }
            }

            this.MainContent = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };

            this.titleLayout = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 40
            };
            this.titleLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = Microsoft.Maui.GridLength.Star });
            this.titleLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = Microsoft.Maui.GridLength.Auto });
            this.titleLayout.Add(this.titleLabel, 0, 0);
            this.titleLayout.Add(this.expandImageButton, 1, 0);
            this.titleLayout.GestureRecognizers.Add(this.tapGestureRecognizer);

            this.parentStack = new Grid
            {
                Padding = new Microsoft.Maui.Thickness(10, 10, 10, 10)
            };
            this.parentStack.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });
            this.parentStack.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Star });
            this.parentStack.RowSpacing = 5;
            this.parentStack.Add(this.titleLayout, 0, 0);

            this.outerFrame = new Frame
            {
                CornerRadius = 8,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                HasShadow = false,
                BorderColor = Color.FromArgb("#E0E0E0"),
                BackgroundColor = Colors.Transparent
            };

            this.parentGrid = new Grid()
            {
                Children = { this.outerFrame, this.parentStack },
            };

            if (RunTimeDevice.IsMobileDevice())
            {
                this.parentGrid.Padding = new Microsoft.Maui.Thickness(9, 10, 9, 4);
            }
            else
            {
                this.parentGrid.Padding = new Microsoft.Maui.Thickness(5);
            }

            this.Content = this.parentGrid;

            if (!RunTimeDevice.IsMobileDevice())
            {
                this.WidthRequest = 450;
            }
        }

        private void TapGestureRecognizer_Tapped(object? sender, EventArgs e)
        {

            if (RunTimeDevice.IsMobileDevice())
            {
                this.parentStack.Children.Remove(this.MainContent);
                Navigation.PushAsync(new PopUpPageExt(this.MainContent, this) { Title = this.Title }, true);
            }
        }

        public void OnAppearing()
        {
            if (this.MainContent != null && !this.parentStack.Children.Contains(this.MainContent))
            {
                this.parentStack.Add(this.MainContent, 0, 1);
            }
        }
    }
}
