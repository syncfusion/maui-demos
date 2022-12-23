#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;
using System.Windows.Input;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class CustomToolbarViewModel : INotifyPropertyChanged
    {
        private PdfData _documentData;
        private int _pageNumber;
        private bool _isFilePickerVisible;
        private ICommand? _openCloseFilePickerCommand;
        private ICommand? _zoomInCommand;
        private ICommand? _zoomOutCommand;
        private object? _selectedFile;
        private double _currentZoom = 1;
        private double? _minZoom = null;
        private double? _maxZoom = null;
        private bool _canZoomIn = true;
        private bool _canZoomOut = true;
        private bool _canGoToPreviousPage = true;
        private bool _canGoToNextPage = true;
        private bool _showPasswordDialog = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public PdfData DocumentData
        {
            get
            {
                return _documentData;
            }
        }

        public object? SelectedFile
        {
            get
            {
                return _selectedFile;
            }
            set
            {
                _selectedFile = value;
                if (value != null)
                {
                    IsFilePickerVisible = false;
                    UpdateFileName(value.ToString());
                }
            }
        }

        public IList<string> Files
        {
            get
            {
                return new List<string>
                {
                    "Rotated PDF",
                    "Encrypted PDF",
                    "Corrupted PDF",
                    "Single page PDF",
#if !MACCATALYST
                    "Browse files on this device"
#endif
                };
            }
        }

        public bool IsFilePickerVisible
        {
            get
            {
                return _isFilePickerVisible;
            }
            set
            {
                _isFilePickerVisible = value;
                OnPropertyChanged("IsFilePickerVisible");
            }
        }

        public bool CanZoomIn
        {
            get
            {
                return _canZoomIn;
            }
            set
            {
                _canZoomIn = value;
                OnPropertyChanged("CanZoomIn");
            }
        }

        public bool ShowPasswordDialog
        {
            get
            {
                return _showPasswordDialog;
            }
            set
            {
                _showPasswordDialog = value;
                OnPropertyChanged("ShowPasswordDialog");
            }
        }

        public bool CanZoomOut
        {
            get
            {
                return _canZoomOut;
            }
            set
            {
                _canZoomOut = value;
                OnPropertyChanged("CanZoomOut");
            }
        }

        public bool CanGoToPreviousPage
        {
            get
            {
                return _canGoToPreviousPage;
            }
            set
            {
                _canGoToPreviousPage = value;
                OnPropertyChanged("CanGoToPreviousPage");
            }
        }

        public bool CanGoToNextPage
        {
            get
            {
                return _canGoToNextPage;
            }
            set
            {
                _canGoToNextPage = value;
                OnPropertyChanged("CanGoToNextPage");
            }
        }
        public double CurrentZoom
        {
            get
            {
                return _currentZoom;
            }
            set
            {
                _currentZoom = value;
                OnPropertyChanged("CurrentZoom");
                ValidateZoomChange();
            }
        }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
                OnPropertyChanged("PageNumber");
                ValidatePageNumber();
            }
        }

        public double? MinZoom
        {
            get
            {
                return _minZoom;
            }
            set
            {
                _minZoom = value;
                ValidateZoomChange();
            }
        }

        public double? MaxZoom
        {
            get
            {
                return _maxZoom;
            }
            set
            {
                _maxZoom = value;
                ValidateZoomChange();
            }
        }

        public ICommand OpenCloseFilePickerCommand
        {
            get
            {
                if (_openCloseFilePickerCommand == null)
                    _openCloseFilePickerCommand = new Command<object>(OpenCloseFilePicker);
                return _openCloseFilePickerCommand;
            }
        }

        public ICommand ZoomInCommand
        {
            get
            {
                if (_zoomInCommand == null)
                    _zoomInCommand = new Command<object>(ZoomIn);
                return _zoomInCommand;
            }
        }

        public ICommand ZoomOutCommand
        {
            get
            {
                if (_zoomOutCommand == null)
                    _zoomOutCommand = new Command<object>(ZoomOut);
                return _zoomOutCommand;
            }
        }

        public CustomToolbarViewModel()
        {
            _documentData = new PdfData();
            _documentData.FileName = "PDF_Succinctly.pdf";
        }

        public void ValidateZoomChange()
        {
            if (IsFilePickerVisible)
                IsFilePickerVisible = false;

            if (_minZoom == null || _maxZoom == null)
                return;
            if (_currentZoom > _minZoom && _currentZoom < _maxZoom)
            {
                CanZoomIn = true;
                CanZoomOut = true;
            }
            else if (_currentZoom <= _minZoom && _currentZoom >= _maxZoom)
            {
                CanZoomIn = false;
                CanZoomOut = false;
            }
            else if (_currentZoom <= _minZoom)
                CanZoomOut = false;
            else if (_currentZoom >= _maxZoom)
                CanZoomIn = false;
        }

        public void ValidatePageNumber()
        {
            if (IsFilePickerVisible)
                IsFilePickerVisible = false;

            if (_documentData.PageCount<=1)
            {
                CanGoToPreviousPage = false;
                CanGoToNextPage = false;
            }
            if (_pageNumber <= 1)
                CanGoToPreviousPage = false;
            else
                CanGoToPreviousPage = true;
            if (_pageNumber >= _documentData.PageCount)
                CanGoToNextPage = false;
            else
                CanGoToNextPage = true;
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        void OpenCloseFilePicker(object commandParameter)
        {
            if (IsFilePickerVisible == true)
                IsFilePickerVisible = false;
            else
                IsFilePickerVisible = true;
        }

        void ZoomIn(object commandParameter)
        {
            CurrentZoom += 0.25;
        }

        void ZoomOut(object commandParameter)
        {
            CurrentZoom -= 0.25;
        }

        async internal void UpdateFileName(string? file)
        {
            ShowPasswordDialog = false;
            switch (file)
            {
                case "Rotated PDF":
                    _documentData.FileName = "rotated_document.pdf";
                    break;
                case "Encrypted PDF":
                    ShowPasswordDialog = true;
                    _documentData.FileName = "encrypted_document.pdf";
                    break;
                case "Corrupted PDF":
                    _documentData.FileName = "corrupted_document.pdf";
                    break;
                case "Single page PDF":
                    _documentData.FileName = "Invoice.pdf";
                    break;
                case "Browse files on this device":
                    FilePickerFileType pdfFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>{
                        { DevicePlatform.iOS, new[] { "public.pdf" } },
                        { DevicePlatform.Android, new[] { "application/pdf" } },
                        { DevicePlatform.WinUI, new[] { "pdf" } },
                        { DevicePlatform.MacCatalyst, new[] { "pdf" } },
                    });
                    PickOptions options = new()
                    {
                        PickerTitle = "Please select a PDF file",
                        FileTypes = pdfFileType,
                    };
                    await PickAndShow(options);
                    break;
            }
        }

        public async Task<FileResult?> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    _documentData.DocumentStream = await result.OpenReadAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                string message;
                if (ex != null && string.IsNullOrEmpty(ex.Message) == false)
                    message = ex.Message;
                else
                    message = "File open failed.";
                Application.Current?.MainPage?.DisplayAlert("Error", message, "OK");
            }
            return null;
        }
    }
}