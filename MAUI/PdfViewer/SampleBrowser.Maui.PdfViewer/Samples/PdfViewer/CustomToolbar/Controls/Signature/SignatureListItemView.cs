#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Themes;
using Syncfusion.Maui.Core.Internals;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureListItemView : Grid, ITouchListener
    {
        internal event EventHandler? DeleteClicked;
        Button deleteButton;

        private static readonly BindableProperty SignatureListDeleteButtonColorProperty = BindableProperty.Create("SignatureListDeleteButtonColor", typeof(Color), typeof(SignatureListItemView), defaultValue: Color.FromArgb("#49454F"));

        internal Color SignatureListDeleteButtonColor
        {
            get => (Color)GetValue(SignatureListDeleteButtonColorProperty);
            set => SetValue(SignatureListDeleteButtonColorProperty, value);
        }
        public SignatureListItemView()
        {
            ApplyDynamicStyles();
            ContentView image = new ContentView();
            image.Padding = new Thickness(8, 0, 40, 0);
            image.SetBinding(ContentView.ContentProperty, new Binding(nameof(SignatureItem.View)));
            Children.Add(image);

            deleteButton = new Button()
            {
                BackgroundColor = Colors.Transparent,
                CornerRadius = 0,
                BorderWidth = 0,
                FontFamily = "MauiMaterialAssets",
                FontSize = 16,
                TextColor = SignatureListDeleteButtonColor,
                Text = "\uE70F",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 10, 0),
                Padding = 0,
                WidthRequest = 20,
                IsVisible = false
            };
            deleteButton.Clicked += DeleteButton_Clicked;
            Children.Add(deleteButton);
            this.AddTouchListener(this);
        }

        private void DeleteButton_Clicked(object? sender, System.EventArgs e)
        {
            DeleteClicked?.Invoke(this, e);
        }

        public void OnTouch(Syncfusion.Maui.Core.Internals.PointerEventArgs e)
        {
            if (e.Action == PointerActions.Entered)
            {
                deleteButton.IsVisible = true;
            }
            else if (e.Action == PointerActions.Exited)
            {
                deleteButton.IsVisible = false;
            }
        }

        private void ApplyDynamicStyles()
        {
            
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureListDeleteButtonColorProperty, "SfPdfViewerSignatureListDeleteButtonColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
