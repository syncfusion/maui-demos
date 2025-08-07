#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Themes;
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureViewOverlay : SfView
    {
        ObservableCollection<SignatureItem>? signatureList = new ObservableCollection<SignatureItem>();
        IntegratedSignatureView? integratedSignatureView;        
#if !WINDOWS && !MACCATALYST
        SignatureSwipeView? signatureSwipeView;
#else
        SignatureListDialog? signatureListDialog;
        TapGestureRecognizer? tapGesture;
        Grid? tapDetector;
#endif
        SignaturePadDialog? signaturePadDialog;
        IIntegratedSigatureDialog integratedSignatureDialog;
        SignatureFormField? fieldToSign;

        internal double SignatureListBottomMarginForMobile = 63;

        private static readonly BindableProperty SignatureTapDetectorBackgroundColorProperty = BindableProperty.Create("SignatureTapDetectorBackgroundColorProperty", typeof(Color), typeof(SignatureViewOverlay), defaultValue: Color.FromRgba(0, 0, 0, 1));

        internal Color SignatureTapDetectorBackgroundColor
        {
            get => (Color)GetValue(SignatureTapDetectorBackgroundColorProperty);
            set => SetValue(SignatureTapDetectorBackgroundColorProperty, value);
        }

        internal event EventHandler<SignatureCreationCompletedEventArgs>? SignatureCreated;
        internal event EventHandler? CloseRequested;

        public SignatureViewOverlay()
        {
            ApplyDynamicStyles();
            BindingContext = new SignatureViewModel();
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                integratedSignatureView = new IntegratedSignatureView();
                integratedSignatureDialog = integratedSignatureView;
            }
            else
            {
                signaturePadDialog = new SignaturePadDialog();
                integratedSignatureDialog = signaturePadDialog;
            }
            integratedSignatureDialog.SignatureCreated += OnSignatureCreated;
            integratedSignatureDialog.CloseRequested += OnCloseRequested;
#if MACCATALYST || WINDOWS
            tapDetector = new Grid();
            tapDetector.BackgroundColor = SignatureTapDetectorBackgroundColor;
            tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TapGestureRecognizer_Tapped;
            tapDetector.GestureRecognizers.Add(tapGesture);
            Children.Add(tapDetector);
#endif
        }

#if MACCATALYST || WINDOWS
        private void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
        {
            Hide();
        }
#endif

        private void ApplyDynamicStyles()
        {
            
        }

        internal void ShowSignaturePad()
        {
            ShowIntegratedSignatureView();
        }

        void ShowIntegratedSignatureView()
        {
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                if (integratedSignatureView != null && !Children.Contains(integratedSignatureView))
                    Children.Add(integratedSignatureView);
            }
            else if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
#if !WINDOWS && !MACCATALYST
                if (signatureSwipeView != null && Children.Contains(signatureSwipeView))
                    Children.Remove(signatureSwipeView);
#else
                if (signatureListDialog != null && Children.Contains(signatureListDialog))
                    Children.Remove(signatureListDialog);
#endif
                if (signaturePadDialog != null && !Children.Contains(signaturePadDialog))
                    Children.Add(signaturePadDialog);
            }
            else
            {
                if (signaturePadDialog != null && !Children.Contains(signaturePadDialog))
                    Children.Add(signaturePadDialog);
            }
        }
        internal void ShowSignatureListDialog()
        {
            if (signatureList != null)
            {
#if !WINDOWS && !MACCATALYST
                if (signatureSwipeView == null)
                {
                    signatureSwipeView = new SignatureSwipeView(signatureList);
                    signatureSwipeView.Margin = new Microsoft.Maui.Thickness(0,0,0, SignatureListBottomMarginForMobile);
                    signatureSwipeView.OnCreateRequested += OnCreateRequested;
                    signatureSwipeView.SignatureSelected += OnSignatureSelected;
                }
                if (integratedSignatureView != null && Children.Contains(integratedSignatureView))
                    Children.Remove(integratedSignatureView);
                if (!Children.Contains(signatureSwipeView))
                    Children.Add(signatureSwipeView);
                if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
                {
                    if (signaturePadDialog != null && Children.Contains(signaturePadDialog))
                        Children.Remove(signaturePadDialog);
                }
#else
                if (signatureListDialog == null)
                {
                    signatureListDialog = new SignatureListDialog(signatureList);
                    signatureListDialog.OnCreateRequested += OnCreateRequested;
                    signatureListDialog.SetBinding(SignatureListDialog.MarginProperty, new Binding(nameof(CustomToolbarViewModel.SignatureListMargin)));
                }
                if (signaturePadDialog != null && Children.Contains(signaturePadDialog))
                    Children.Remove(signaturePadDialog);
                if (!Children.Contains(signatureListDialog))
                    Children.Add(signatureListDialog);
#endif
            }
        }

        internal void SetMarginBinding(object bindingContext)
        {
#if WINDOWS || MACCATALYST
            if (signatureListDialog != null && bindingContext is CustomToolbarViewModel customToolbarViewModel)
            {
                signatureListDialog.BindingContext = bindingContext;
                signatureListDialog.SetBinding(SignatureListDialog.MarginProperty, new Binding(nameof(customToolbarViewModel.SignatureListMargin)));
            }
#endif
        }

        internal void SetSignatureFieldToSign(SignatureFormField? signatureField)
        {
            fieldToSign = signatureField;
        }

        private void OnSignatureSelected(object? sender, EventArgs e)
        {
            Hide();
        }

        private void OnCloseRequested(object? sender, EventArgs e)
        {
            Hide();
        }

        private void OnCreateRequested(object? sender, EventArgs e)
        {
            ShowIntegratedSignatureView();
        }

        internal void Hide()
        {
            if (Parent is Grid mainGrid)
            {
                mainGrid.Children.Remove(this);
                CloseRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnSignatureCreated(object? sender, EventArgs e)
        {
            if (SignatureHelper.CurrentSignatureItem != null)
            {
                if (SignatureHelper.ShouldSaveSignature)
                {
                    if (signatureList == null)
                        signatureList = new ObservableCollection<SignatureItem>();
                    signatureList.Add(SignatureHelper.CurrentSignatureItem);
                }
                if (fieldToSign != null)
                {
                    int pageNumber = fieldToSign.PageNumber;
                    if (SignatureHelper.CurrentSignatureItem is DrawSignatureItem drawSignature)
                    {
                            InkAnnotation ink = new InkAnnotation(drawSignature.InkPoints, pageNumber);
                            ink.IsSignature = true;
                            ink.Color = drawSignature.Color;
                            ink.BorderWidth = 3;
                            fieldToSign.Signature = ink;
                            drawSignature.Dispose();
                    }
                    else if (SignatureHelper.CurrentSignatureItem is TypeSignatureItem typeSignature)
                    {
                        
                    }
                    else if (SignatureHelper.CurrentSignatureItem is ImageSignatureItem imageSignature)
                    {
                        if (imageSignature.ImageStream != null)
                        {
                            StampAnnotation stamp = new StampAnnotation(imageSignature.ImageStream, pageNumber, new Rect(0, 0, imageSignature.ImageSize.Width, imageSignature.ImageSize.Height));
                            stamp.IsSignature = true;
                            fieldToSign.Signature = stamp;
                        }
                    }
                    SignatureHelper.CurrentSignatureItem = null;
                }
                SignatureCreated?.Invoke(this, new SignatureCreationCompletedEventArgs(fieldToSign == null));
               fieldToSign = null;
            }
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureTapDetectorBackgroundColorProperty, "SfPdfViewerSignatureTapDetectorBackgroundColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }

    internal class SignatureCreationCompletedEventArgs : EventArgs
    {
        internal bool CanShowToast { get;  }

        public SignatureCreationCompletedEventArgs(bool canShowToast)
        {
            CanShowToast = canShowToast;
        }
    }
}
