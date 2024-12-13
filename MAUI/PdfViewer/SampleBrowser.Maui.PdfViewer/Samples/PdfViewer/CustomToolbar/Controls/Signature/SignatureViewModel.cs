#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureViewModel : INotifyPropertyChanged
    {
        Color selectedColor = Colors.Black;
        bool isClearButtonVisible;
        bool isClearButtonEnabled;
        bool isCreateButtonEnabled;
        bool isOkButtonVisible;
        bool showUploadButton = true;
        string textViewText = string.Empty;
        ImageSource? uploadTabImageStream;
        public Color SelectedColor
        {
            get { return selectedColor; } 
            set
            {
                if (value != selectedColor)
                {
                    selectedColor = value;
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }

        public bool IsClearButtonVisible
        {
            get { return isClearButtonVisible; }
            set
            {
                if (isClearButtonVisible != value)
                {
                    isClearButtonVisible = value;
                    OnPropertyChanged(nameof(IsClearButtonVisible));
                }
            }
        }

        public bool IsOkButtonVisible
        {
            get { return isOkButtonVisible; }
            set
            {
                if (isOkButtonVisible != value)
                {
                    isOkButtonVisible = value;
                    OnPropertyChanged(nameof(IsOkButtonVisible));
                }
            }
        }

        public bool IsClearButtonEnabled
        {
            get { return isClearButtonEnabled; }
            set
            {
                if (isClearButtonEnabled != value)
                {
                    isClearButtonEnabled = value;
                    OnPropertyChanged(nameof(IsClearButtonEnabled));
                }
            }
        }

        public bool IsCreateButtonEnabled
        {
            get { return isCreateButtonEnabled; }
            set
            {
                if (isCreateButtonEnabled != value)
                {
                    isCreateButtonEnabled = value;
                    OnPropertyChanged(nameof(IsCreateButtonEnabled));
                }
            }
        }

        public bool ShowUploadButton
        {
            get { return showUploadButton; } 
            set
            {
                if (showUploadButton != value)
                {
                    showUploadButton = value;
                    OnPropertyChanged(nameof(ShowUploadButton));
                }
            }
        }

        public ImageSource? UploadTabImageSource
        {
            get { return uploadTabImageStream; }
            set
            {
                if (uploadTabImageStream != value)
                {
                    uploadTabImageStream = value;
                    ShowUploadButton = value == null;
                    if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                    {
                        IsOkButtonVisible = value != null;
                        IsClearButtonVisible = value != null;
                    }
                    else
                    {
                        IsClearButtonEnabled = value != null;
                        IsCreateButtonEnabled = value != null;
                    }
                    OnPropertyChanged(nameof(UploadTabImageSource));
                }
            }
        }

        public string TextViewText
        {
            get { return textViewText; }
            set
            {
                if (value != null)
                {
                    textViewText = value;
                    OnPropertyChanged(nameof(TextViewText));
                    if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                    {
                        IsOkButtonVisible = !string.IsNullOrEmpty(value);
                        IsClearButtonVisible = !string.IsNullOrEmpty(value);
                    }
                    else
                    {
                        IsCreateButtonEnabled = !string.IsNullOrEmpty(value);
                        IsClearButtonEnabled = !string.IsNullOrEmpty(value);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
