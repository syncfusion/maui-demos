#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.Themes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SwipeUpView : SfView
    {
        VerticalStackLayout? mainStackLayout;
        ContentView? contentView;
        SwipeUpViewContentBorder? contentBorder;

        private static readonly BindableProperty SwipeUpBoxViewBackgroundColorProperty = BindableProperty.Create("SwipeUpBoxViewBackgroundColorProperty", typeof(Color), typeof(SwipeUpView), defaultValue: Color.FromArgb("#49454F"));

        internal Color SwipeUpBoxViewBackgroundColor
        {
            get => (Color)GetValue(SwipeUpBoxViewBackgroundColorProperty);
            set => SetValue(SwipeUpBoxViewBackgroundColorProperty, value);
        }

        internal SwipeUpView()
        {
            ApplyDynamicStyles();
            BackgroundColor = Colors.Transparent;
            Margin = new Thickness(0, 0, 0, 63 );
            contentBorder = new SwipeUpViewContentBorder();

            SwipeGestureRecognizer swipeGestureUP = new SwipeGestureRecognizer();
            swipeGestureUP.Direction = SwipeDirection.Up;
            swipeGestureUP.Swiped += SwipeGesture_Swiped;
#if !IOS
            contentBorder.GestureRecognizers.Add(swipeGestureUP);
#endif

            SwipeGestureRecognizer swipeGestureDown = new SwipeGestureRecognizer();
            swipeGestureDown.Direction = SwipeDirection.Down;
            swipeGestureDown.Swiped += SwipeGesture_Swiped;
#if !IOS
            contentBorder.GestureRecognizers.Add(swipeGestureDown);
#endif

            mainStackLayout = new VerticalStackLayout();
            Border boxView = new Border
            {
                Background = SwipeUpBoxViewBackgroundColor,
                Padding = 0,
                Opacity = 0.25,
                StrokeShape = new RoundRectangle() { CornerRadius = 3 },
                HeightRequest = 6,
                WidthRequest = 50,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 5, 0, 0),
            };
            mainStackLayout.Children.Add(boxView);
            contentView = new ContentView();
            mainStackLayout.Children.Add(contentView);
            contentBorder.Content = mainStackLayout;
            Children.Add(contentBorder);
        }

        private void SwipeGesture_Swiped(object? sender, SwipedEventArgs e)
        {
            if (e.Direction == SwipeDirection.Up)
            {
                var roundRectangle = new RoundRectangle();
                roundRectangle.CornerRadius = new CornerRadius(0);
                if (contentBorder != null)
                {
                    contentBorder.StrokeShape = roundRectangle;
                    contentBorder.HeightRequest = this.Height;
                }
            }
            if (e.Direction == SwipeDirection.Down)
            {
                var roundRectangle = new RoundRectangle();
                roundRectangle.CornerRadius = new CornerRadius(12, 12, 0, 0);
                if (contentBorder != null)
                {
                    contentBorder.StrokeShape = roundRectangle;
                    contentBorder.HeightRequest = 349;
                }
            }
        }

        internal void AddContent(View view, View? parent = null)
        {
            if(contentView != null)
                contentView.Content = view;
        }

        private void ApplyDynamicStyles()
        {
            
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SwipeUpBoxViewBackgroundColorProperty, "SfPdfViewerSignatureListHeaderBorderColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }

    internal class SwipeUpViewContentBorder : Border
    {
        RoundRectangle roundRectangle;
        private static readonly BindableProperty SwipeUpContentBackgroundColorProperty = BindableProperty.Create("SwipeUpContentBackgroundColorProperty", typeof(Color), typeof(SwipeUpView), defaultValue: Color.FromArgb("#EEE8F4"));
        private static readonly BindableProperty SwipeUpContentBorderColorProperty = BindableProperty.Create("SwipeUpContentBorderColorProperty", typeof(Color), typeof(SwipeUpView), defaultValue: Color.FromArgb("#33000000"));
        public SwipeUpViewContentBorder()
        {
            ApplyDynamicStyles();
            if (Application.Current != null && Application.Current.Resources != null)
            {
                this.SetAppThemeColor(Border.BackgroundColorProperty,
                    (Color)Application.Current.Resources["SampleBrowserBackgroundLight"],
                    (Color)Application.Current.Resources["BackgroundDark"]);
                this.SetAppThemeColor(Border.StrokeProperty,
                    (Color)Application.Current.Resources["BorderLight"],
                    (Color)Application.Current.Resources["Border"]);
            }
            VerticalOptions = LayoutOptions.End;
            HeightRequest = 380;
            roundRectangle = new RoundRectangle();
            roundRectangle.CornerRadius = new CornerRadius(12, 12, 0, 0);
            IsVisible = DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS;
            VerticalOptions = LayoutOptions.End;
            StrokeShape = roundRectangle;
            Shadow shadow = new Shadow();
            shadow.Brush = Colors.Black;
            shadow.Radius = 6;
            shadow.Opacity = 0.3f;
            Shadow = shadow;
        }
        private void ApplyDynamicStyles()
        {
            
        }
        internal Color SwipeUpContentBackgroundColor
        {
            get => (Color)GetValue(SwipeUpContentBackgroundColorProperty);
            set => SetValue(SwipeUpContentBackgroundColorProperty, value);
        }

        internal Color SwipeUpContentBorderColor
        {
            get => (Color)GetValue(SwipeUpContentBorderColorProperty);
            set => SetValue(SwipeUpContentBorderColorProperty, value);
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SwipeUpContentBackgroundColorProperty, "SfPdfViewerSignatureListViewBackgroundColor");
            this.SetDynamicResource(SwipeUpContentBorderColorProperty, "SfPdfViewerSignatureListViewBorderColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
