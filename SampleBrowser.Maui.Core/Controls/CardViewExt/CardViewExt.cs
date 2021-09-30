#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui.Core
{
    public class CardViewExt : ContentView
    {
        private Image expandImageButton;
        private Label titleLabel;

        private Frame outerFrame;
        private Grid parentGrid;

        private VerticalStackLayout parentStack;
        private Grid titleLayout;
        private TapGestureRecognizer tapGestureRecognizer;

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
            
            this.expandImageButton = new Image();
            this.expandImageButton.WidthRequest = 25;
            this.expandImageButton.HeightRequest = 25;
            this.expandImageButton.Source = "expandicon";
            if (Device.RuntimePlatform == "Android")
            {
                this.expandImageButton.Margin = new Microsoft.Maui.Thickness(0, 3, 5, 0);
            }
            else
            {
                this.expandImageButton.Margin = new Microsoft.Maui.Thickness(0, 3, -18, 0);
            }
            this.expandImageButton.HorizontalOptions = LayoutOptions.End;
            this.expandImageButton.BackgroundColor = Colors.Transparent;

            this.titleLabel = new Label();
            this.titleLabel.HorizontalOptions = LayoutOptions.Start;
            this.titleLabel.HorizontalTextAlignment = Microsoft.Maui.TextAlignment.Start;
            this.titleLabel.Text = this.Title;
            this.titleLabel.TextColor = Colors.Black;
            this.titleLabel.FontFamily = "Roboto";
            this.titleLabel.FontSize = 16;

            this.MainContent = new ContentView();
            this.MainContent.VerticalOptions = LayoutOptions.Fill;
            this.MainContent.HorizontalOptions = LayoutOptions.Fill;

            this.titleLayout = new Grid();
            this.titleLayout.HorizontalOptions = LayoutOptions.Fill;    
            this.titleLayout.HeightRequest = 40;
            this.titleLayout.Children.Add(this.titleLabel);
            this.titleLayout.Children.Add(this.expandImageButton);
            this.titleLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = Microsoft.Maui.GridLength.Star });
            this.titleLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = 20 });
            this.titleLayout.GestureRecognizers.Add(this.tapGestureRecognizer);

            this.parentStack = new VerticalStackLayout();
            this.parentStack.Padding = new Microsoft.Maui.Thickness(10, 10, 10, 10);
            this.parentStack.Spacing = 5;
            this.parentStack.Children.Add(this.titleLayout);

            this.outerFrame = new Frame();
            this.outerFrame.CornerRadius = 8;
            this.outerFrame.HorizontalOptions = LayoutOptions.Fill;
            this.outerFrame.VerticalOptions = LayoutOptions.Fill;
            this.outerFrame.HasShadow = false;
            this.outerFrame.BorderColor = Color.FromArgb("#E0E0E0");
            this.outerFrame.BackgroundColor = Colors.Transparent;

            this.parentGrid = new Grid()
            {
                Padding = new Microsoft.Maui.Thickness(9,10,9,4),
                Children = { this.outerFrame, this.parentStack },
            };

            this.Content = this.parentGrid;
        }

        private void TapGestureRecognizer_Tapped(object? sender, EventArgs e)
        {
            this.parentStack.Children.Remove(this.MainContent);
            Navigation.PushAsync(new PopUpPageExt(this.MainContent, this) { Title = this.Title }, true);
        }

        public void OnAppearing()
        {
            if (this.MainContent != null && !this.parentStack.Children.Contains(this.MainContent))
            {
                this.parentStack.Children.Add(this.MainContent);
                this.InvalidateLayout();
            }
        }
    }
}
