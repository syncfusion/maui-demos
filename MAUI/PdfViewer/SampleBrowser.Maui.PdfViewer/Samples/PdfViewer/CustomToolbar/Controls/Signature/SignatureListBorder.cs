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
    internal class SignatureListBorder : Border, ILongPressGestureListener, ITapGestureListener
    {
        private static readonly BindableProperty SignatureListViewBorderStrokeColorProperty = BindableProperty.Create("SignatureListViewBorderStrokeColorProperty", typeof(Color), typeof(SignatureListBorder), defaultValue: Color.FromArgb("#CAC4D0"));
        private static readonly BindableProperty SignatureListViewBorderBackgroundColorProperty = BindableProperty.Create("SignatureListViewBorderBackgroundColorProperty", typeof(Color), typeof(SignatureListBorder), defaultValue: Colors.Transparent);
        private static readonly BindableProperty SignatureListViewTappedBackgroundColorProperty = BindableProperty.Create("SignatureListViewTappedBackgroundColorProperty", typeof(Color), typeof(SignatureListBorder), defaultValue: Color.FromRgba(0, 0, 0, 12));
        private static readonly BindableProperty SignatureListViewLongPressedBackgroundColorProperty = BindableProperty.Create("SignatureListViewLongPressedBackgroundColorProperty", typeof(Color), typeof(SignatureListBorder), defaultValue: Color.FromArgb("#F6EDFF"));
        private static readonly BindableProperty SignatureListViewLongPressedStrokeColorProperty = BindableProperty.Create("SignatureListViewLongPressedStrokeColorProperty", typeof(Color), typeof(SignatureListBorder), defaultValue: Color.FromArgb("#6750A4"));

        internal Color SignatureListViewBorderStrokeColor
        {
            get => (Color)GetValue(SignatureListViewBorderStrokeColorProperty);
            set => SetValue(SignatureListViewBorderStrokeColorProperty, value);
        }

        internal Color SignatureListViewBorderBackgroundColor
        {
            get => (Color)GetValue(SignatureListViewBorderBackgroundColorProperty);
            set => SetValue(SignatureListViewBorderBackgroundColorProperty, value);
        }

        internal Color SignatureListViewTappedBackgroundColor
        {
            get => (Color)GetValue(SignatureListViewTappedBackgroundColorProperty);
            set => SetValue(SignatureListViewTappedBackgroundColorProperty, value);
        }

        internal Color SignatureListViewLongPressedBackgroundColor
        {
            get => (Color)GetValue(SignatureListViewLongPressedBackgroundColorProperty);
            set => SetValue(SignatureListViewLongPressedBackgroundColorProperty, value);
        }

        internal Color SignatureListViewLongPressedStrokeColor
        {
            get => (Color)GetValue(SignatureListViewLongPressedStrokeColorProperty);
            set => SetValue(SignatureListViewLongPressedStrokeColorProperty, value);
        }


        private static SignatureListBorder? currentLongPressedItem;
        private static SignatureListBorder? currentTappedItem;
        internal event EventHandler? LongPressed;
        internal event EventHandler? Tapped;
        internal SignatureListBorder()
        {
            ApplyDynamicStyles();
            Margin = new Thickness(0, 0, 0, 5);
            Stroke = SignatureListViewBorderStrokeColor;
            Background = SignatureListViewBorderBackgroundColor;
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(4)
            };
            this.AddGestureListener(this);
        }

        private void ApplyDynamicStyles()
        {
            
        }

        void ILongPressGestureListener.OnLongPress(LongPressEventArgs e)
        {
            if (currentLongPressedItem != null && currentLongPressedItem != this)
            {
                currentLongPressedItem.ResetBorder();
                if (currentTappedItem != null)
                    currentTappedItem.Background = Colors.Transparent;
            }
            currentLongPressedItem = this;
            StrokeDashArray = new DoubleCollection { 3, 2 };
            Stroke = SignatureListViewLongPressedStrokeColor;
            Background = SignatureListViewLongPressedBackgroundColor;
            LongPressed?.Invoke(this, EventArgs.Empty);
        }

        private void ResetBorder()
        {
            StrokeDashArray = new DoubleCollection { 0, 0 };
            Stroke = SignatureListViewBorderStrokeColor;
            Background = SignatureListViewBorderBackgroundColor;
        }

        public void OnTap(TapEventArgs e)
        {
            if (currentTappedItem != null && currentTappedItem != this)
            {
                currentTappedItem.ResetBorder();
                if (currentLongPressedItem != null)
                    currentLongPressedItem.ResetBorder();
            }
            currentTappedItem = this;
            Background = SignatureListViewTappedBackgroundColor;
            Tapped?.Invoke(this, EventArgs.Empty);
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureListViewBorderStrokeColorProperty, "SfPdfViewerSignatureListViewItemBorderColor");
            this.SetDynamicResource(SignatureListViewBorderBackgroundColorProperty, "SfPdfViewerSignatureListViewItemBackgroundColor");
            this.SetDynamicResource(SignatureListViewLongPressedBackgroundColorProperty, "SfPdfViewerSignatureListViewLongPressedItemBackgroundColor");
            this.SetDynamicResource(SignatureListViewLongPressedStrokeColorProperty, "SfPdfViewerSignatureListViewLongPressedSItemBorderColor");
            this.SetDynamicResource(SignatureListViewTappedBackgroundColorProperty, "SfPdfViewerSignatureListViewTappedItemBackgroundColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
