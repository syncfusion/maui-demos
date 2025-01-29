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
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Themes;
using System;
using System.IO;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class ImageSignatureCreateView : SfView, ISignatureCreateView
    {
        string? imageFilePath;
        Stream? imageStream;

        private static readonly BindableProperty SignatureUploadButtonColorProperty = BindableProperty.Create("SignatureUploadButtonColor", typeof(Color), typeof(ImageSignatureCreateView), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureUploadBorderColorProperty = BindableProperty.Create("SignatureUploadBorderColor", typeof(Color), typeof(ImageSignatureCreateView), defaultValue: Color.FromArgb("#CAC4D0"));
        private static readonly BindableProperty DragandDropTextColorProperty = BindableProperty.Create("DragandDropTextColor", typeof(Color), typeof(ImageSignatureCreateView), defaultValue: Color.FromArgb("#49454F"));

        internal Color SignatureUploadButtonColor
        {
            get => (Color)GetValue(SignatureUploadButtonColorProperty);
            set => SetValue(SignatureUploadButtonColorProperty, value);
        }

        internal Color SignatureUploadBorderColor
        {
            get => (Color)GetValue(SignatureUploadBorderColorProperty);
            set => SetValue(SignatureUploadBorderColorProperty, value);
        }

        internal Color DragandDropTextColor
        {
            get => (Color)GetValue(DragandDropTextColorProperty);
            set => SetValue(DragandDropTextColorProperty, value);
        }

        public ImageSignatureCreateView()
        {
            ApplyDynamicStyles();
            Padding = 15;
            Grid contentGrid = new Grid();

            Button uploadButton = new Button()
            {
                Text = "Upload an Image",
                HeightRequest = 40,
                WidthRequest = 140,
                TextColor = SignatureUploadButtonColor,
                BackgroundColor = Colors.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 20,
                BorderColor = SignatureUploadButtonColor,
                BorderWidth = 1
            };

            if (Application.Current != null && Application.Current.Resources != null)
            {
                uploadButton.SetAppThemeColor(Button.TextColorProperty,
                    (Color)Application.Current.Resources["FlatButtonForegroundLight"],
                    (Color)Application.Current.Resources["FlatButtonForeground"]);
                uploadButton.SetAppThemeColor(Button.BorderColorProperty,
                   (Color)Application.Current.Resources["FlatButtonForegroundLight"],
                    (Color)Application.Current.Resources["FlatButtonForeground"]);
            }

            uploadButton.SetBinding(Button.IsVisibleProperty, new Binding(nameof(SignatureViewModel.ShowUploadButton)));
            uploadButton.Clicked += UploadButton_Clicked;

            Image image = new Image();
            image.SetBinding(Image.SourceProperty, new Binding(nameof(SignatureViewModel.UploadTabImageSource)));

            contentGrid.Children.Add(image);
            contentGrid.Children.Add(uploadButton);
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                Children.Add(contentGrid);
            }
            else
            {
#if WINDOWS
                Label label = new Label()
                {
                    Text = "Drag and drop an image here",
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 70, 0, 0)
                };
                if (Application.Current != null && Application.Current.Resources != null)
                {
                    label.SetAppThemeColor(Label.TextColorProperty,
                        (Color)Application.Current.Resources["IconColourLight"],
                        (Color)Application.Current.Resources["IconColour"]);
                }
                label.SetBinding(Label.IsVisibleProperty, new Binding(nameof(SignatureViewModel.ShowUploadButton)));
                contentGrid.Children.Add(label);
                contentGrid.HandlerChanged += ContentGrid_HandlerChanged;
                DropGestureRecognizer dropGesture = new DropGestureRecognizer();
                dropGesture.AllowDrop = true;
                contentGrid.GestureRecognizers.Add(dropGesture);
#endif
                Border border = new Border()
                {
                    Padding = 10,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(8)
                    },
                    Stroke = SignatureUploadBorderColor,
                    Content = contentGrid
                };
                Children.Add(border);
            }
        }

#if WINDOWS
        private void ContentGrid_HandlerChanged(object? sender, EventArgs e)
        {
            if (sender is Grid grid && grid.Handler != null && grid.Handler.PlatformView is Microsoft.UI.Xaml.UIElement element)
            {
                element.AllowDrop = true;
                element.Drop += Element_Drop;
            }
        }

        private async void Element_Drop(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            var items = await e.DataView.GetStorageItemsAsync();
            if (items.Count > 0)
            {
                var storageFile = items[0] as Windows.Storage.StorageFile;
                if (storageFile != null)
                {
                    imageFilePath = storageFile.Path;
                    imageStream = new MemoryStream(File.ReadAllBytes(imageFilePath));
                    if (BindingContext is SignatureViewModel viewModel)
                        viewModel.UploadTabImageSource = new FileImageSource()
                        {
                            File = storageFile.Path,
                        };
                }
            }
        }
#endif

        private async void UploadButton_Clicked(object? sender, System.EventArgs e)
        {
            FileResult? imageFileResult = await MediaPicker.Default.PickPhotoAsync();
            if (imageFileResult != null)
            {
                imageStream = await imageFileResult.OpenReadAsync();
                imageFilePath = imageFileResult.FullPath;
                if (BindingContext is SignatureViewModel viewModel)
                    viewModel.UploadTabImageSource = new FileImageSource()
                    {
                        File = imageFilePath,
                    };
            }
        }

        void ISignatureCreateView.Reset()
        {
            if (BindingContext is SignatureViewModel viewModel)
                viewModel.UploadTabImageSource = null;
        }

        SignatureItem ISignatureCreateView.GetSignatureItem()
        {
            Image image = new Image()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Source = new FileImageSource { File = imageFilePath }
            };
            ImageSignatureItem signatureItem = new ImageSignatureItem(imageStream, imageFilePath, image);
            return signatureItem;
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureUploadButtonColorProperty, "SfPdfViewerSignatureViewUploadTabButtonColor");
            this.SetDynamicResource(SignatureUploadBorderColorProperty, "SfPdfViewerSignatureViewUploadTabButtonBorderColor");
            this.SetDynamicResource(DragandDropTextColorProperty, "SfPdfViewerSignatureViewUploadTabDragandDropTextColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }

        private void ApplyDynamicStyles()
        {
            
        }
    }
}

