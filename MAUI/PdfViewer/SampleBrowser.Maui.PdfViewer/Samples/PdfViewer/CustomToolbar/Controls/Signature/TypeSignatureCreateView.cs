#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class TypeSignatureCreateView : VerticalStackLayout, ISignatureCreateView
    {
        List<SignatureTextView> textViews;
        readonly List<string> fontFamilies = new List<string>() { "Boogaloo", "Handlee", "Kaushan Script", "Pinyon Script" };
        int selectedFontFamilyIndex = -1;
        Entry textEntry;
        SfTextInputLayout inputLayout;

        private static readonly BindableProperty TypeSignatureLayoutStrokeColorProperty = BindableProperty.Create("TypeSignatureBackgroundColorProperty", typeof(Color), typeof(TypeSignatureCreateView), defaultValue: Color.FromArgb("#79747E"));
        private static readonly BindableProperty TextViewGridBorderColorProperty = BindableProperty.Create("TypeSignatureBackgroundColorProperty", typeof(Color), typeof(TypeSignatureCreateView), defaultValue: Color.FromArgb("#CAC4D0"));
        private static readonly BindableProperty TextLayoutTextColorProperty = BindableProperty.Create("TextLayoutTextColorProperty", typeof(Color), typeof(TypeSignatureCreateView), defaultValue: Colors.Black);

        internal Color TypeSignatureLayoutStrokeColor
        {
            get => (Color)GetValue(TypeSignatureLayoutStrokeColorProperty);
            set => SetValue(TypeSignatureLayoutStrokeColorProperty, value);
        }

        internal Color TextViewGridBorderColor
        {
            get => (Color)GetValue(TextViewGridBorderColorProperty);
            set => SetValue(TextViewGridBorderColorProperty, value);
        }

        internal Color TextLayoutTextColor
        {
            get => (Color)GetValue(TextLayoutTextColorProperty);
            set => SetValue(TextLayoutTextColorProperty, value);
        }

        public TypeSignatureCreateView()
        {
            ApplyDynamicStyles();
            Padding = 15;
            Spacing = 10;
            textViews = new List<SignatureTextView>();

            inputLayout = new SfTextInputLayout();
            inputLayout.ContainerBackground = Colors.Transparent;
            textEntry = new Entry();
            textEntry.TextColor = TextLayoutTextColor;
            textEntry.TextChanged += TextEntry_TextChanged;
            textEntry.Focused += TextEntry_Focused;
            textEntry.FontSize = 16;
            textEntry.FontFamily = "Roboto";
            inputLayout.HorizontalOptions = LayoutOptions.Center;
            inputLayout.VerticalOptions = LayoutOptions.Center;
            string? signatureHint = "Signature";
            if (signatureHint != null)
                inputLayout.Hint = signatureHint;
            inputLayout.Stroke = TypeSignatureLayoutStrokeColor;
            inputLayout.HeightRequest = 56;
#if ANDROID
            inputLayout.InputViewPadding = new Thickness(20, 10, 20, 5);
#else
            inputLayout.InputViewPadding = new Thickness(20, 5, 20, 5);
#endif
            inputLayout.ContainerType = ContainerType.Outlined;
            inputLayout.ReserveSpaceForAssistiveLabels = false;
            inputLayout.Content = textEntry;
            Children.Add(inputLayout);
            AddSignatureTextViews();
        }

        private void ApplyDynamicStyles()
        {
            
        }

        private void TextEntry_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (BindingContext is SignatureViewModel viewModel)
                viewModel.TextViewText = textEntry.Text;
        }

        private void TextEntry_Focused(object? sender, FocusEventArgs e)
        {
            inputLayout.Hint = "Type your signature";
        }

        void AddSignatureTextViews()
        {
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                foreach (string fontFamily in fontFamilies)
                {
                    SignatureTextView textView = new SignatureTextView(fontFamily);
                    textView.Tapped += TextView_Tapped;
                    textViews.Add(textView);
                    Children.Add(textView);
                }
            }
            else
            {
                Grid textViewGrid = new Grid()
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                };
                textViewGrid.AddRowDefinition(new RowDefinition());
                textViewGrid.AddRowDefinition(new RowDefinition());
                textViewGrid.AddColumnDefinition(new ColumnDefinition());
                textViewGrid.AddColumnDefinition(new ColumnDefinition());
                Border border = new Border()
                {
                    Padding = 10,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(8)
                    },
                    Stroke = TextViewGridBorderColor,
                    Content = textViewGrid
                };
                int row = 0, column = 0;
                for (int i = 0; i < fontFamilies.Count; i++)
                {
                    SignatureTextView textView = new SignatureTextView(fontFamilies[i]);
                    textView.Tapped += TextView_Tapped;
                    textViews.Add(textView);
                    textViewGrid.Add(textView, column, row);
                    column++;
                    if (column > 1)
                    {
                        column = 0;
                        row = 1;
                    }
                }
                Children.Add(border);
            }
        }

        private void TextView_Tapped(object? sender, EventArgs e)
        {
            if (sender is SignatureTextView tappedTextView)
            {
                foreach (SignatureTextView textView in textViews)
                {
                    if (textView == tappedTextView)
                        textView.Select();
                    else
                        textView.Deselect();
                }
                selectedFontFamilyIndex = textViews.IndexOf(tappedTextView);
            }
        }

        void ISignatureCreateView.Reset()
        {
            textEntry.Text = string.Empty;
            if (BindingContext is SignatureViewModel viewModel)
                viewModel.TextViewText = "Signature";
            inputLayout.Hint = "Signature";
            textEntry.Unfocus();           
        }

        SignatureItem ISignatureCreateView.GetSignatureItem()
        {
            Color selectedColor = BindingContext is SignatureViewModel viewModel ? viewModel.SelectedColor : Colors.Black;
            Label label = new Label
            {
                Text = textEntry.Text,
                TextColor = selectedColor,
                FontSize = 27,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };
            if (selectedFontFamilyIndex >= 0)
                label.FontFamily = fontFamilies[selectedFontFamilyIndex];
            return new TypeSignatureItem(label);
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(TypeSignatureLayoutStrokeColorProperty, "SfPdfViewerSignatureViewTypeTabTextEntryBorderColor");
            this.SetDynamicResource(TextViewGridBorderColorProperty, "SfPdfViewerSignatureViewTypeTabSignatureLabelBorderColor");
            this.SetDynamicResource(TextLayoutTextColorProperty, "SfPdfViewerTextLayoutTextColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
