#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Themes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureTextView : Border, ITapGestureListener
    {
        private static readonly BindableProperty SignatureTextNormalStrokeColorProperty = BindableProperty.Create("SignatureTextNormalStrokeColorProperty", typeof(Color), typeof(SignatureTextView), defaultValue: Color.FromArgb("#CAC4D0"));
        private static readonly BindableProperty SignatureTextSelectedStrokeColorProperty = BindableProperty.Create("SignatureTextSelectedStrokeColorProperty", typeof(Color), typeof(SignatureTextView), defaultValue: Color.FromArgb("#6750A4"));
#if ANDROID || IOS
        private static readonly BindableProperty SignatureTextSelectedBackgroundColorProperty = BindableProperty.Create("SignatureTextSelectedBackgroundColorProperty", typeof(Color), typeof(SignatureTextView), defaultValue: Color.FromRgba("#F6EDFF"));
#else
        private static readonly BindableProperty SignatureTextSelectedBackgroundColorProperty = BindableProperty.Create("SignatureTextSelectedBackgroundColorProperty", typeof(Color), typeof(SignatureTextView), defaultValue: Color.FromRgba(169, 169, 169, 45));
#endif

        internal Color SignatureTextNormalStrokeColor
        {
            get => (Color)GetValue(SignatureTextNormalStrokeColorProperty);
            set => SetValue(SignatureTextNormalStrokeColorProperty, value);
        }

        internal Color SignatureTextSelectedStrokeColor
        {
            get => (Color)GetValue(SignatureTextSelectedStrokeColorProperty);
            set => SetValue(SignatureTextSelectedStrokeColorProperty, value);
        }

        internal Color SignatureTextSelectedBackgroundColor
        {
            get => (Color)GetValue(SignatureTextSelectedBackgroundColorProperty);
            set => SetValue(SignatureTextSelectedBackgroundColorProperty, value);
        }

        internal event EventHandler? Tapped;
        public SignatureTextView(string fontFamily)
        {
            ApplyDynamicStyles();
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(5, 5, 5, 5)
            };
            Stroke = SignatureTextNormalStrokeColor;
            Label label = new Label
            {
                HeightRequest = 76,
                FontSize = 36,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontFamily = fontFamily,
            };
            label.SetBinding(Label.TextColorProperty, new Binding(nameof(SignatureViewModel.SelectedColor)));
            label.SetBinding(Label.TextProperty, new Binding(nameof(SignatureViewModel.TextViewText)));
            Content = label;
            this.AddGestureListener(this);
        }

        private void ApplyDynamicStyles()
        {
            
        }

        internal void Select()
        {
            Stroke = SignatureTextSelectedStrokeColor;
#if !WINDOWS && !MACCATALYST
            BackgroundColor = SignatureTextSelectedBackgroundColor;
#else
            BackgroundColor = SignatureTextSelectedBackgroundColor;
#endif
            StrokeDashArray = new DoubleCollection { 3, 2 };
        }

        internal void Deselect()
        {
            Stroke = SignatureTextNormalStrokeColor;
            BackgroundColor = Colors.Transparent;
            StrokeDashArray = new DoubleCollection { 0, 0 };
        }

        public void OnTap(TapEventArgs e)
        {
            Tapped?.Invoke(this, EventArgs.Empty);
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureTextNormalStrokeColorProperty, "SfPdfViewerSignatureTextNormalStrokeColor");
            this.SetDynamicResource(SignatureTextSelectedStrokeColorProperty, "SfPdfViewerSignatureTextSelectedStrokeColor");
            this.SetDynamicResource(SignatureTextSelectedBackgroundColorProperty, "SfPdfViewerSignatureTextSelectedBackgroundColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
