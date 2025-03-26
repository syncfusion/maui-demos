#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.SignaturePad;
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class FreeHandSignatureCreateView : SfView, ISignatureCreateView
    {
        SfSignaturePad signaturePad;

        private static readonly BindableProperty DrawTabBackgroundColorProperty = BindableProperty.Create("DrawTabBackgroundColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Colors.White);

        internal Color DrawTabBackgroundColor
        {
            get => (Color)GetValue(DrawTabBackgroundColorProperty);
            set => SetValue(DrawTabBackgroundColorProperty, value);
        }

        internal FreeHandSignatureCreateView()
        {
            ApplyDynamicStyles();
            signaturePad = new SfSignaturePad();
            signaturePad.BackgroundColor = DrawTabBackgroundColor;
            signaturePad.MaximumStrokeThickness = signaturePad.MinimumStrokeThickness = 3;
            signaturePad.SetBinding(SfSignaturePad.StrokeColorProperty, new Binding(nameof(SignatureViewModel.SelectedColor)));
            signaturePad.DrawCompleted += SignaturePad_DrawCompleted;
            Children.Add(signaturePad);
        }

        private void ApplyDynamicStyles()
        {
            
        }

        private void SignaturePad_DrawCompleted(object? sender, System.EventArgs e)
        {
            if (BindingContext is SignatureViewModel viewModel)
            {
                if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                {
                    viewModel.IsClearButtonVisible = true;
                    viewModel.IsOkButtonVisible = true;
                }
                else
                {
                    viewModel.IsClearButtonEnabled = true;
                    viewModel.IsCreateButtonEnabled = true;
                }
            }
        }

        void ISignatureCreateView.Reset()
        {
            signaturePad.Clear();
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonVisible = false;
                viewModel.IsOkButtonVisible = false;
            }
        }

        SignatureItem ISignatureCreateView.GetSignatureItem()
        {
            Image inkImage = new Image()
            {
                Source = signaturePad.ToImageSource(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            ImageSource? imageSource = signaturePad.ToImageSource();
            signaturePad.Clear();
            Color selectedColor = BindingContext is SignatureViewModel viewModel ? viewModel.SelectedColor : Colors.Black;
            return new DrawSignatureItem(selectedColor, imageSource, inkImage);
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(DrawTabBackgroundColorProperty, "SfPdfViewerSignatureViewDrawTabBackgroundColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
