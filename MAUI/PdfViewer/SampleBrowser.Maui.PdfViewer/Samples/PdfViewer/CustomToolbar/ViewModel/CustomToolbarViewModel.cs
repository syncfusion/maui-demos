#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
#if WINDOWS
using Windows.Storage;
#endif

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class CustomToolbarViewModel : INotifyPropertyChanged
    {
        private PdfData _documentData;
        private Thickness _textMarkupListMargin;
        private Thickness _shapeListMargin;
        private Thickness _stampListMargin;
        private Thickness _stickyNoteListMargin;
		private Thickness _fileOperationListMargin;
        private Thickness _colorPaletteMargin;
        private bool _isFilePickerVisible;
        private ICommand? _openCloseFilePickerCommand;
        private ICommand? _zoomInCommand;
        private ICommand? _zoomOutCommand;
        private ICommand? _outlineCommand;
        private ICommand? _backToFileListCommand;
        private ICommand? _editCommand;
        private object? _selectedFile;
        private double _currentZoom = 1;
        private double? _minZoom = null;
        private double? _maxZoom = null;
        private bool _showPasswordDialog = false;
        private bool _showMessageBoxDialog = false;
        private bool _showHyperlinkDialog = false;
        private bool _showOutlineView = false;
        private bool _showMoreView = false;
        private bool _isTextMarkupListVisible = false;
        private bool _isShapeListVisible = false;
        private bool _isStickyNoteListVisible = false;
		private bool _isFileOperationListVisible = false;
        private bool _isStampListVisible = false;
        private bool _isDeleteButtonVisible = false;
        private bool _isEditLayoutVisible = false;
        private bool _isSaveLayoutVisible = false;
        private bool _isStickyNoteMode = false;
        private StickyNoteIcon _stickyNoteIcon = StickyNoteIcon.Note;
        private bool _isFileListViewVisible = false;
        private Color _selectedColor = new();
        bool m_isDocumentLoaded;

        bool isStickyNoteToolbarVisible;
        ContentView bottomToolbarContent;
        ICommand? toolbarCommand;
        ICommand? colorCommand;
        ICommand? opacityCommand;
        ICommand? thicknessCommand;
        bool isColorToolbarVisible;
        bool isAnnotationsToolsVisible = true;
        bool isShapeColorPalleteVisible = false;
        bool isInkColorPalleteVisible = false;
        bool isTextMarkUpColorPalleteVisible = false;
        bool isStickyNoteColorPalleteVisible=false;
        bool isStickyIconChangeButtonVisible = false;
        bool isEraserThicknessToolbarVisible = false;
        bool isDeskTopColorToolbarVisible;
        bool isDeskTopFillColorToolbarVisible;
        bool isStampOpacitySliderbarVisible;
        bool isFillColorToolbarVisible;
        bool isStampOpacityToolbarVisible;
        bool isThicknessToolbarVisible;
        bool isOpacityToolbarVisible;
        bool isStampViewVisible;
        bool isLineAndArrowColorPalleteVisible;
        bool isLockVisible = false;
        bool isUnlockVisible = false;        
        Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer;
        Annotation? selectedAnnotation;
        Color? textMarkupHighlightColor = Colors.Transparent;
        Color? shapeHighlightColor = Colors.Transparent;
        Color? inkHighlightColor = Colors.Transparent;
        Color? inkEraserHighlightColor = Colors.Transparent;
        Color? stickyNoteHighlightColor = Colors.Transparent;
        Color? stampHighlightColor = Colors.Transparent;
        float selectedOpacity;
        float selectedThickness, minimumThickness = 1, maximumThickness = 20, minimumEraserThickness = 10, maximumEraserThickness = 100;
        string textMarkupButtonIcon = string.Empty;
        string shapeButtonIcon = string.Empty;
        string selectedAnnotationIcon = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? StickyNoteSelected;

        /// <summary>
        ///  Returns the Pdf data. 
        /// </summary>
        public PdfData DocumentData => _documentData;

        /// <summary>
        /// Gets or sets the selected PDF file. 
        /// </summary>
        public object? SelectedFile
        {
            get => _selectedFile;
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

        // Initialize the files with some custom values.
        public IList<string> Files
        {
            get
            {
                return new List<string>
                {
                    "PDF Succinctly",
                    "Rotated PDF",
                    "Password protected PDF",
                    "Corrupted PDF",
                    "Single page PDF",
                    "Annotations PDF",
#if !MACCATALYST
                    "Browse files on this device"
#endif
                };
            }
        }

        /// <summary>
        ///  Determines whether the file picker can show in its current state.
        /// </summary>
        public bool IsFilePickerVisible
        {
            get => _isFilePickerVisible;
            set
            {
                if (_isFilePickerVisible != value)
                {
                    _isFilePickerVisible = value;
                    OnPropertyChanged(nameof(IsFilePickerVisible));
                }
            }
        }
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }
        public bool IsFileListViewVisible
        {
            get => _isFileListViewVisible;
            set
            {
                if (_isFileListViewVisible != value)
                {
                    _isFileListViewVisible = value;
                    OnPropertyChanged(nameof(IsFileListViewVisible));
                }
            }
        }

        public bool IsTextMarkupListVisible
        {
            get => _isTextMarkupListVisible;
            set
            {
                if (_isTextMarkupListVisible != value)
                {
                    _isTextMarkupListVisible = value;
                    OnPropertyChanged(nameof(IsTextMarkupListVisible));
                }
            }
        }

        public bool IsShapeListVisible
        {
            get => _isShapeListVisible;
            set
            {
                if (_isShapeListVisible != value)
                {
                    _isShapeListVisible = value;
                    OnPropertyChanged(nameof(IsShapeListVisible));
                }
            }
        }

        public bool IsFileOperationListVisible
        {
            get => _isFileOperationListVisible;
            set
            {
                if (_isFileOperationListVisible != value)
                {
                    _isFileOperationListVisible = value;
                    OnPropertyChanged(nameof(IsFileOperationListVisible));
                }
            }
        }

        public bool IsLineAndArrowColorPalleteVisible
        {
            get => isLineAndArrowColorPalleteVisible;
            set
            {
                if (isLineAndArrowColorPalleteVisible != value)
                {
                    isLineAndArrowColorPalleteVisible = value;
                    OnPropertyChanged(nameof(IsLineAndArrowColorPalleteVisible));
                }
            }
        }
        public bool IsStampListVisible
        {
            get => _isStampListVisible;
            set
            {
                if (_isStampListVisible != value)
                {
                    _isStampListVisible = value;
                    OnPropertyChanged(nameof(IsStampListVisible));
                }
            }
        }

        public bool IsStickyNoteListVisible
        {
            get => _isStickyNoteListVisible;
            set
            {
                if (_isStickyNoteListVisible != value)
                {
                    _isStickyNoteListVisible = value;
                    OnPropertyChanged(nameof(IsStickyNoteListVisible));
                }
            }
        }
        
        public bool IsStickyNoteMode
        {
            get => _isStickyNoteMode;
            set
            {
                if (_isStickyNoteMode != value)
                {
                    _isStickyNoteMode = value;
                }
            }
        }

        public StickyNoteIcon StickyIcon
        {
            get => _stickyNoteIcon;
            set
            {
                if (_stickyNoteIcon != value)
                {
                    _stickyNoteIcon = value;
                }
            }
        }

        public bool IsDeleteButtonVisible
        {
            get => _isDeleteButtonVisible;
            set
            {
                if (_isDeleteButtonVisible != value)
                {
                    _isDeleteButtonVisible = value;
                    OnPropertyChanged(nameof(IsDeleteButtonVisible));
                }
            }
        }

        public bool IsEditLayoutVisible
        {
            get => _isEditLayoutVisible;
            set
            {
                if (_isEditLayoutVisible != value)
                {
                    _isEditLayoutVisible = value;
                    OnPropertyChanged(nameof(IsEditLayoutVisible));
                }
            }
        }

        public bool IsSaveLayoutVisible
        {
            get => _isSaveLayoutVisible;
            set
            {
                if (_isSaveLayoutVisible != value)
                {
                    _isSaveLayoutVisible = value;
                    OnPropertyChanged(nameof(IsSaveLayoutVisible));
                }
            }
        }

        /// <summary>
        ///  Determines whether the password box dialog can show in its current state.
        /// </summary>
        public bool ShowPasswordDialog
        {
            get => _showPasswordDialog;
            set
            {
                if (_showPasswordDialog != value)
                {
                    _showPasswordDialog = value;
                    OnPropertyChanged(nameof(ShowPasswordDialog));
                }
            }
        }

        /// <summary>
        ///  Determines whether the Message box dialog can show in its current state.
        /// </summary>
        public bool ShowMessageBoxDialog
        {
            get => _showMessageBoxDialog;
            set
            {
                if (_showMessageBoxDialog != value)
                {
                    _showMessageBoxDialog = value;
                    OnPropertyChanged(nameof(ShowMessageBoxDialog));
                }
            }
        }

        /// <summary>
        ///  Determines whether the Hyperlink dialog can show in its current state.
        /// </summary>
        public bool ShowHyperlinkDialog
        {
            get => _showHyperlinkDialog;
            set
            {
                if (_showHyperlinkDialog != value)
                {
                    _showHyperlinkDialog = value;
                    OnPropertyChanged(nameof(ShowHyperlinkDialog));
                }
            }
        }

        public bool ShowOutlineView
        {
            get => _showOutlineView;
            set
            {
                if (_showOutlineView != value)
                {
                    _showOutlineView = value;
                    OnPropertyChanged(nameof(ShowOutlineView));
                }
            }
        }
        public bool ShowMoreOptions
        {
            get => _showMoreView;
            set
            {
                if (_showMoreView != value)
                {
                    _showMoreView = value;
                    OnPropertyChanged(nameof(ShowMoreOptions));
                }
            }
        }

        public Thickness ColorPaletteMargin
        {
            get => _colorPaletteMargin;
            set
            {
                if (_colorPaletteMargin != value)
                {
                    _colorPaletteMargin = value;
                    OnPropertyChanged(nameof(ColorPaletteMargin));
                }
            }
        }

        public Thickness ShapeListMargin
        {
            get => _shapeListMargin;
            set
            {
                _shapeListMargin = value;
                OnPropertyChanged(nameof(ShapeListMargin));
            }
        }
        public Thickness TextMarkupListMargin
        {
            get => _textMarkupListMargin;
            set
            {
                _textMarkupListMargin = value;
                OnPropertyChanged(nameof(TextMarkupListMargin));
            }
        }
        public Thickness StampListMargin
        {
            get => _stampListMargin;
            set
            {
                _stampListMargin = value;
                OnPropertyChanged(nameof(StampListMargin));
            }
        }
        public Thickness FileOperationListMargin
        {
            get => _fileOperationListMargin;
            set
            {
                _fileOperationListMargin = value;
                OnPropertyChanged(nameof(FileOperationListMargin));
            }
        }

        public Thickness StickyNoteListMargin
        {
            get => _stickyNoteListMargin;
            set
            {
                _stickyNoteListMargin = value;
                OnPropertyChanged(nameof(StickyNoteListMargin));
            }
        }

        public string TextMarkupButtonIcon
        {
            get => textMarkupButtonIcon;
            set
            {
                if (textMarkupButtonIcon != value)
                {
                    textMarkupButtonIcon = value;
                    OnPropertyChanged(nameof(TextMarkupButtonIcon));
                }
            }
        }

        public string ShapeButtonIcon
        {
            get => shapeButtonIcon;
            set
            {
                if (shapeButtonIcon != value)
                {
                    shapeButtonIcon = value;
                    OnPropertyChanged(nameof(ShapeButtonIcon));
                }
            }
        }

        public string SelectedAnnotationIcon
        {
            get => selectedAnnotationIcon;
            set
            {
                if (selectedAnnotationIcon != value)
                {
                    selectedAnnotationIcon = value;
                    OnPropertyChanged(nameof(SelectedAnnotationIcon));
                }    
            }
        }

        /// <summary>
        /// Gets or sets the current zoom.
        /// </summary>
        public double CurrentZoom
        {
            get => _currentZoom;
            set
            {
                _currentZoom = value;
                OnPropertyChanged("CurrentZoom");
                RefreshZoomCommandCanExecutes();
            }
        }


        /// <summary>
        /// Gets or sets the minimum zoom.
        /// </summary>
        public double? MinZoom
        {
            get => _minZoom;
            set
            {
                _minZoom = value;
                RefreshZoomCommandCanExecutes();
            }
        }

        /// <summary>
        /// Gets or sets the value that indicates whether the document is loaded.
        /// </summary>
        public bool IsDocumentLoaded
        {
            get => m_isDocumentLoaded;
            set
            {
                m_isDocumentLoaded = value;
                RefreshZoomCommandCanExecutes();
                RefreshToolbarCommandCanExecute();
                OnPropertyChanged(nameof(IsDocumentLoaded));
            }
        }

        /// <summary>
        /// Gets or sets the maximum zoom.
        /// </summary>
        public double? MaxZoom
        {
            get => _maxZoom;
            set
            {
                _maxZoom = value;
                RefreshZoomCommandCanExecutes();
            }
        }

        public bool IsLockButtonVisible
        {
            get => isLockVisible;
            set
            {
                isLockVisible = value;
                OnPropertyChanged(nameof(IsLockButtonVisible));
            }
        }

        public bool IsUnlockButtonVisible
        {
            get => isUnlockVisible;
            set
            {
                isUnlockVisible = value;
                OnPropertyChanged(nameof(IsUnlockButtonVisible));
            }
        }

        public bool LockButtonsVisible { get; set; } = false;

        /// <summary>
        /// Gets the command to open or close the file picker.
        /// </summary>
        public ICommand OpenCloseFilePickerCommand
        {
            get
            {
                if (_openCloseFilePickerCommand == null)
                    _openCloseFilePickerCommand = new Command<object>(OpenCloseFilePicker);
                return _openCloseFilePickerCommand;
            }
        }

        /// <summary>
        /// Gets the command to increase the current zoom.
        /// </summary>
        public ICommand ZoomInCommand
        {
            get
            {
                if (_zoomInCommand == null)
                    _zoomInCommand = new Command(() => CurrentZoom += 0.25,
                        canExecute: () => { return CurrentZoom < MaxZoom && IsDocumentLoaded; });
                return _zoomInCommand;
            }
        }

        /// <summary>
        /// Gets the command to decrease the current zoom.
        /// </summary>
        public ICommand ZoomOutCommand
        {
            get
            {
                if (_zoomOutCommand == null)
                    _zoomOutCommand = new Command(() => CurrentZoom -= 0.25,
                        canExecute: () => { return CurrentZoom > MinZoom && IsDocumentLoaded; });
                return _zoomOutCommand;
            }
        }

        public ICommand OutlineCommand
        {
            get
            {
                if (_outlineCommand == null)
                    _outlineCommand = new Command(() => {
                        ShowMoreOptions = false;
                        ShowOutlineView = !ShowOutlineView;
                    });
                return _outlineCommand;
            }
        }

        public ICommand BackToFileListCommand
        {
            get
            {
                if (_backToFileListCommand == null)
                    _backToFileListCommand = new Command(() =>
                    {
                        IsFileListViewVisible = true;
                        pdfViewer.UnloadDocument();
                    });
                return _backToFileListCommand;
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public CustomToolbarViewModel(Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer)
        {
            _documentData = new PdfData();
#if ANDROID || IOS
            _isFileListViewVisible = true;
#else
            _documentData.FileName = "PDF_Succinctly1.pdf";
#endif
            this.pdfViewer = pdfViewer;
            bottomToolbarContent = new AnnotationToolbar(this);
        }

        internal void CloseAllDialogs()
        {
            ShowPasswordDialog = false;
            ShowHyperlinkDialog = false;
            ShowOutlineView = false;
            IsFilePickerVisible = false;
            ShowMoreOptions = false;
            IsStampViewVisible = false;
        }

        /// <summary>
        /// Refreshes the can executes of the zoom commands.
        /// </summary>
        void RefreshZoomCommandCanExecutes()
        {
            if (ZoomInCommand is Command zoomInCommand)
                zoomInCommand.ChangeCanExecute();
            if (ZoomOutCommand is Command zoomOutCommand)
                zoomOutCommand.ChangeCanExecute();
        }

        void RefreshToolbarCommandCanExecute()
        {
            if (ToolbarCommand is Command toolbarCommand)
                toolbarCommand.ChangeCanExecute();
        }

        public void OnPropertyChanged(string name)
        {
#if ANDROID
            // There is a known exception in Android platform when the PropertyChanged event is invoked. 
            // As there is no fix for this issue yet, caught the exception.
            // Issue link: https://github.com/dotnet/maui/issues/10867
            try
            {
#endif
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
#if ANDROID
            }
            catch (Java.Lang.NullPointerException) { }
#endif
        }

        /// <summary>
        /// Executes the open or close the file picker command.
        /// </summary>
        void OpenCloseFilePicker(object commandParameter)
        {
            bool isFilePickerOpen = IsFilePickerVisible;
            CloseAllDialogs();
            IsShapeListVisible = false;
            IsStickyNoteListVisible = false;
            IsTextMarkupListVisible = false;
            IsStampListVisible = false;
            IsFilePickerVisible = !isFilePickerOpen;
        }
        internal void OpenCloseFile()
        {
            bool isFilePickerOpen = IsFilePickerVisible;
            CloseAllDialogs();
            IsShapeListVisible = false;
            IsTextMarkupListVisible = false;
            IsStampListVisible = false;
            IsFilePickerVisible = !isFilePickerOpen;
        }

        /// <summary>
        /// Updates the selected PDF file name.
        /// </summary>
        async internal void UpdateFileName(string? file)
        {
            switch (file)
            {
                case "PDF Succinctly":
                    _documentData.FileName = "PDF_Succinctly1.pdf";
                    break;
                case "Rotated PDF":
                    _documentData.FileName = "rotated_document.pdf";
                    break;
                case "Password protected PDF":
                    _documentData.FileName = "password_protected_document.pdf";
                    break;
                case "Corrupted PDF":
                    _documentData.FileName = "corrupted_document.pdf";
                    break;
                case "Single page PDF":
                    _documentData.FileName = "Invoice.pdf";
                    break;
                case "Annotations PDF":
                    _documentData.FileName = "Annotations.pdf";
                    break;
                case "Browse files on this device":
                    // Create file picker with file type as PDF.
                    FilePickerFileType pdfFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>{
                        { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                        { DevicePlatform.Android, new[] { "application/pdf" } },
                        { DevicePlatform.WinUI, new[] { "pdf" } },
                        { DevicePlatform.MacCatalyst, new[] { "pdf" } },
                    });
                    // Provide picker title if required.
                    PickOptions options = new()
                    {
                        PickerTitle = "Please select a PDF file",
                        FileTypes = pdfFileType,
                    };
                    await PickFile(options);
                    break;
            }
        }

        /// <summary>
        /// Picks the file from local storage and store as stream.
        /// </summary>
        public async Task<FileResult?> PickFile(PickOptions options)
        {
            try
            {
                // Pick the file from local storage.
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    IsFileListViewVisible = false;
                    // Store the resultant PDF as stream.
                    if (result.FileName != null)
                    {
                        if (result.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                            _documentData.DocumentStream = await result.OpenReadAsync();
                        else if (result.FileName.EndsWith(".xfdf", StringComparison.OrdinalIgnoreCase))
                        {
                            Stream importStream = await result.OpenReadAsync();
                            pdfViewer.ImportAnnotations(importStream, Syncfusion.Pdf.Parsing.AnnotationDataFormat.XFdf);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                // Display error when file picker failed to open files.
                string message;
                if (ex != null && string.IsNullOrEmpty(ex.Message) == false)
                    message = ex.Message;
                else
                    message = "File open failed.";
                Application.Current?.MainPage?.DisplayAlert("Error", message, "OK");
            }
            return null;
        }

        public ICommand ToolbarCommand
        {
            get
            {
                if (toolbarCommand == null)
                {
                    toolbarCommand = new Command(
                        (parameter) => { OnToolbarCommand(parameter); },
                        canExecute: (o) =>
                        {
                            // If document is unloaded file button is need to enabled.
                            if (o is string parameterString && parameterString == "FileOperation")
                            {
                                return true;
                            }
                            return IsDocumentLoaded;
                        });
                }
                return toolbarCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new Command((parameter) => { OnEditCommand(parameter); }, canExecute: (o) => 
                    {
                        if (SelectedAnnotation == null)
                            return true;
                        else
                            return !SelectedAnnotation.IsLocked;
                    });
                return _editCommand;
            }
        }
        public ICommand ThicknessCommand
        {
            get
            {
                return thicknessCommand ?? (thicknessCommand = new Command(OnThicknessCommand));
            }
        }
        public ICommand OpacityCommand
        {
            get
            {
                return opacityCommand ?? (opacityCommand = new Command(OnOpacityCommand));
            }
        }
        public ICommand ColorCommand
        {
            get
            {
                return colorCommand ?? (colorCommand = new Command(OnColorCommand));
            }
        }

        internal Annotation? SelectedAnnotation
        {
            get
            {
                return selectedAnnotation!;
            }
            set
            {
                if (selectedAnnotation != value)
                {
                    selectedAnnotation = value;
                    if (selectedAnnotation == null)
                    {
#if ANDROID || IOS
                        BottomToolbarContent = new AnnotationToolbar(this);
                        IsStickyNoteToolbarVisible = false;
#endif
                        IsDeleteButtonVisible = false;
                        IsEditLayoutVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacityToolbarVisible = false;
                        IsAnnotationsToolsVisible = true;
                        IsLockButtonVisible = IsUnlockButtonVisible = false;
                        HideOverlayToolbars();
                    }
                    else
                    {
                        CloseAllDialogs();
                        SetSliderValues();
                        SetSelectedAnnotationIcon();
#if ANDROID || IOS
                        if (selectedAnnotation is ShapeAnnotation)
                            BottomToolbarContent = new ShapesPropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is InkAnnotation)
                            BottomToolbarContent = new InkToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is HighlightAnnotation || selectedAnnotation is UnderlineAnnotation || selectedAnnotation is StrikeOutAnnotation || selectedAnnotation is SquigglyAnnotation)
                            BottomToolbarContent = new TextMarkUpPropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if(selectedAnnotation is StampAnnotation)
                            BottomToolbarContent = new StampPropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is StickyNoteAnnotation)
                            BottomToolbarContent = new StickyNotePropertyToolbar(this) { IsDeleteButtonVisible = true};
#endif
                        
                        if (selectedAnnotation is StickyNoteAnnotation stickyNoteAnnotation)
                        {
                            IsStickyIconChangeButtonVisible = true;
                            StickyIcon = stickyNoteAnnotation.Icon;
                            SetSelectedAnnotationIcon();
                        }
                        if (selectedAnnotation.IsLocked)
                        {
                            IsEditLayoutVisible = false;
                            IsDeleteButtonVisible = false;
                            IsLockButtonVisible = false;
                            IsUnlockButtonVisible = true;
                        }
                        else
                        {
                            IsEditLayoutVisible = true;
                            IsDeleteButtonVisible = true;
                            IsLockButtonVisible = true;
                            IsUnlockButtonVisible = false;
                        }
                        IsAnnotationsToolsVisible = false;
                    }
                }
            }
        }

        public bool IsEraserThicknessToolbarVisible
        {
            get
            {
                return isEraserThicknessToolbarVisible;
            }
            set
            {
                if (isEraserThicknessToolbarVisible != value)
                {
                    isEraserThicknessToolbarVisible = value;
                    OnPropertyChanged(nameof(IsEraserThicknessToolbarVisible));
                }
            }
        }
        public bool IsStickyNoteToolbarVisible
        {
            get
            {
                return isStickyNoteToolbarVisible;
            }
            set
            {
                if (isStickyNoteToolbarVisible != value)
                {
                    isStickyNoteToolbarVisible = value;
                    OnPropertyChanged(nameof(IsStickyNoteToolbarVisible));
                }
            }
        }

        public bool IsAnnotationsToolsVisible
        {
            get
            {
                return isAnnotationsToolsVisible;
            }
            set
            {
                if (isAnnotationsToolsVisible != value)
                {
                    isAnnotationsToolsVisible = value;
                    OnPropertyChanged(nameof(IsAnnotationsToolsVisible));
                }
            }
        }

        public bool IsStampOpacitySliderbarVisible
        {
            get
            {
                return isStampOpacitySliderbarVisible;
            }
            set
            {
                if (isStampOpacitySliderbarVisible != value)
                {
                    isStampOpacitySliderbarVisible = value;
                    OnPropertyChanged(nameof(IsStampOpacitySliderbarVisible));
                }
            }
        }
        public bool IsFillColorToolbarVisible
        {
            get
            {
                return isFillColorToolbarVisible;
            }
            set
            {
                if (isFillColorToolbarVisible != value)
                {
                    isFillColorToolbarVisible = value;
                    OnPropertyChanged(nameof(IsFillColorToolbarVisible));
                }
            }
        }
        public bool IsDeskTopColorToolbarVisible
        {
            get
            {
                return isDeskTopColorToolbarVisible;
            }
            set
            {
                if (isDeskTopColorToolbarVisible != value)
                {
                    isDeskTopColorToolbarVisible = value;
                    OnPropertyChanged(nameof(IsDeskTopColorToolbarVisible));
                }
            }
        }
        public bool IsShapeColorPalleteVisible
        {
            get
            {
                return isShapeColorPalleteVisible;
            }
            set
            {
                if (isShapeColorPalleteVisible != value)
                {
                    isShapeColorPalleteVisible = value;
                    OnPropertyChanged(nameof(IsShapeColorPalleteVisible));
                }
            }
        }
        public bool IsTextMarkUpColorPalleteVisible
        {
            get
            {
                return isTextMarkUpColorPalleteVisible;
            }
            set
            {
                if (isTextMarkUpColorPalleteVisible != value)
                {
                    isTextMarkUpColorPalleteVisible = value;
                    OnPropertyChanged(nameof(IsTextMarkUpColorPalleteVisible));
                }
            }
        }
        public bool IsInkColorPalleteVisible
        {
            get
            {
                return isInkColorPalleteVisible;
            }
            set
            {
                if (isInkColorPalleteVisible != value)
                {
                    isInkColorPalleteVisible = value;
                    OnPropertyChanged(nameof(IsInkColorPalleteVisible));
                }
            }
        }
        public bool IsStickyNoteColorPalleteVisible
        {
            get
            {
                return isStickyNoteColorPalleteVisible;
            }
            set
            {
                if (isStickyNoteColorPalleteVisible != value)
                {
                    isStickyNoteColorPalleteVisible = value;
                    OnPropertyChanged(nameof(IsStickyNoteColorPalleteVisible));
                }
            }
        }
        public bool IsStickyIconChangeButtonVisible
        {
            get
            {
                return isStickyIconChangeButtonVisible;
            }
            set
            {
                if (isStickyIconChangeButtonVisible != value)
                {
                    isStickyIconChangeButtonVisible = value;
                    OnPropertyChanged(nameof(IsStickyIconChangeButtonVisible));
                }
            }
        }
        public bool IsDeskTopFillColorToolbarVisible
        {
            get
            {
                return isDeskTopFillColorToolbarVisible;
            }
            set
            {
                if (isDeskTopFillColorToolbarVisible != value)
                {
                    isDeskTopFillColorToolbarVisible = value;
                    OnPropertyChanged(nameof(IsDeskTopFillColorToolbarVisible));
                }
            }
        }
        public bool IsColorToolbarVisible
        {
            get
            {
                return isColorToolbarVisible;
            }
            set
            {
                if (isColorToolbarVisible != value)
                {
                    isColorToolbarVisible = value;
                    OnPropertyChanged(nameof(IsColorToolbarVisible));
                }
            }
        }
        public bool IsStampOpacityToolbarVisible
        {
            get
            {
                return isStampOpacityToolbarVisible;
            }
            set
            {
                if (isStampOpacityToolbarVisible != value)
                {
                    isStampOpacityToolbarVisible = value;
                    OnPropertyChanged(nameof(IsStampOpacityToolbarVisible));
                }
            }
        }
        public bool IsThicknessToolbarVisible
        {
            get
            {
                return isThicknessToolbarVisible;
            }
            set
            {
                if (isThicknessToolbarVisible != value)
                {
                    isThicknessToolbarVisible = value;
                    OnPropertyChanged(nameof(IsThicknessToolbarVisible));
                }
            }
        }
      
        public bool IsOpacityToolbarVisible
        {
            get
            {
                return isOpacityToolbarVisible;
            }
            set
            {
                if (isOpacityToolbarVisible != value)
                {
                    isOpacityToolbarVisible = value;
                    OnPropertyChanged(nameof(IsOpacityToolbarVisible));
                }
            }
        }

        public bool IsStampViewVisible
        {
            get
            {
                return isStampViewVisible;
            }
            set
            {
                if (isStampViewVisible != value)
                {
                    isStampViewVisible = value;
                    OnPropertyChanged(nameof(IsStampViewVisible));
                }
            }
        }


        public Color? TextMarkupHighlightColor
        {
            get
            {
                return textMarkupHighlightColor;
            }
            set
            {
                if (textMarkupHighlightColor != value)
                {
                    textMarkupHighlightColor = value;
                    OnPropertyChanged(nameof(TextMarkupHighlightColor));
                }
            }
        }

        public Color? ShapeHighlightColor
        {
            get
            {
                return shapeHighlightColor;
            }
            set
            {
                if (shapeHighlightColor != value)
                {
                    shapeHighlightColor = value;
                    OnPropertyChanged(nameof(ShapeHighlightColor));
                }
            }
        }

        public Color? InkHighlightColor
        {
            get
            {
                return inkHighlightColor;
            }
            set
            {
                if (inkHighlightColor != value)
                {
                    inkHighlightColor = value;
                    OnPropertyChanged(nameof(InkHighlightColor));
                }
            }
        }

        public Color? InkEraserHighlightColor
        {
            get
            {
                return inkEraserHighlightColor;
            }
            set
            {
                if (inkEraserHighlightColor != value)
                {
                    inkEraserHighlightColor = value;
                    OnPropertyChanged(nameof(InkEraserHighlightColor));
                }
            }
        }

        public Color? StampHighlightColor
        {
            get
            {
                return stampHighlightColor;
            }
            set
            {
                if (stampHighlightColor != value)
                {
                    stampHighlightColor = value;
                    OnPropertyChanged(nameof(StampHighlightColor));
                }
            }
        }

        public Color? StickyNoteHighlightColor
        {
            get
            {
                return stickyNoteHighlightColor;
            }
            set
            {
                if (stickyNoteHighlightColor != value)
                {
                    stickyNoteHighlightColor = value;
                    OnPropertyChanged(nameof(StickyNoteHighlightColor));
                }
            }
        }

        public float SelectedOpacity
        {
            get
            {
                return selectedOpacity;
            }
            set
            {
                if (selectedOpacity != value)
                {
                    selectedOpacity = value;
                    OnPropertyChanged(nameof(SelectedOpacity));
                }
            }
        }

        public float SelectedThickness
        {
            get
            {
                return selectedThickness;
            }
            set
            {
                if (selectedThickness != value)
                {
                    selectedThickness = value;
                    OnPropertyChanged(nameof(SelectedThickness));
                }
            }
        }

        public float MinimumThickness
        {
            get
            {
                return minimumThickness;
            }
            set
            {
                if (minimumThickness != value)
                {
                    minimumThickness = value;
                    OnPropertyChanged(nameof(MinimumThickness));
                }
            }
        }

        public float MinimumEraserThickness
        {
            get
            {
                return minimumEraserThickness;
            }
            set
            {
                if (minimumEraserThickness != value)
                {
                    minimumEraserThickness = value;
                    OnPropertyChanged(nameof(MinimumEraserThickness));
                }
            }
        }
        public float MaximumThickness
        {
            get
            {
                return maximumThickness;
            }
            set
            {
                if (maximumThickness != value)
                {
                    maximumThickness = value;
                    OnPropertyChanged(nameof(MaximumThickness));
                }
            }
        }

        public float MaximumEraserThickness
        {
            get
            {
                return maximumEraserThickness;
            }
            set
            {
                if (maximumEraserThickness != value)
                {
                    maximumEraserThickness = value;
                    OnPropertyChanged(nameof(MaximumEraserThickness));
                }
            }
        }
        public ContentView BottomToolbarContent
        {
            get
            {
                return bottomToolbarContent;
            }
            set
            {
                bottomToolbarContent = value;
                CloseAllDialogs();
                OnPropertyChanged(nameof(BottomToolbarContent));
            }
        }

        void SetSliderValues()
        {
            if (selectedAnnotation != null)
            {
                SelectedOpacity = selectedAnnotation.Opacity;
                if (selectedAnnotation is ShapeAnnotation shape)
                {
                    SelectedThickness = shape.BorderWidth;
                }
                else if (selectedAnnotation is InkAnnotation ink)
                {
                    SelectedColor = ink.Color;
                    SelectedThickness = ink.BorderWidth;
                }
                else if (selectedAnnotation is UnderlineAnnotation underline)
                {
                    SelectedColor = underline.Color;
                }
                else if (selectedAnnotation is StrikeOutAnnotation strikeOut)
                {
                    SelectedColor = strikeOut.Color;
                }
                else if (selectedAnnotation is HighlightAnnotation highlight)
                {
                    SelectedColor = highlight.Color;
                }
                else if (selectedAnnotation is SquigglyAnnotation squiggly)
                {
                    SelectedColor = squiggly.Color;
                }
                else if (selectedAnnotation is StickyNoteAnnotation stickyNote)
                {
                    SelectedColor = stickyNote.Color;
                }
            }
            else
            {
                switch (pdfViewer.AnnotationMode)
                {
                    case AnnotationMode.Square:
                        SelectedThickness = pdfViewer.AnnotationSettings.Square.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Square.Opacity;
                        break;

                    case AnnotationMode.Circle:
                        SelectedThickness = pdfViewer.AnnotationSettings.Circle.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Circle.Opacity;
                        break;

                    case AnnotationMode.Line:
                        SelectedThickness = pdfViewer.AnnotationSettings.Line.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Line.Opacity;
                        break;

                    case AnnotationMode.Arrow:
                        SelectedThickness = pdfViewer.AnnotationSettings.Arrow.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Arrow.Opacity;
                        break;

                    case AnnotationMode.Ink:
                        SelectedThickness = pdfViewer.AnnotationSettings.Ink.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Ink.Opacity;
                        SelectedColor = pdfViewer.AnnotationSettings.Ink.Color;
                        break;

                    case AnnotationMode.InkEraser:
                        SelectedThickness = pdfViewer.AnnotationSettings.InkEraser.Thickness;
                        break;

                    case AnnotationMode.Highlight:
                        SelectedOpacity = pdfViewer.AnnotationSettings.Highlight.Opacity;
                        SelectedColor = pdfViewer.AnnotationSettings.Highlight.Color;
                        break;

                    case AnnotationMode.Underline:
                        SelectedOpacity = pdfViewer.AnnotationSettings.Underline.Opacity;
                        SelectedColor = pdfViewer.AnnotationSettings.Underline.Color;
                        break;

                    case AnnotationMode.StrikeOut:
                        SelectedOpacity = pdfViewer.AnnotationSettings.StrikeOut.Opacity;
                        SelectedColor = pdfViewer.AnnotationSettings.StrikeOut.Color;
                        break;

                    case AnnotationMode.Squiggly:
                        SelectedOpacity = pdfViewer.AnnotationSettings.Squiggly.Opacity;
                        SelectedColor = pdfViewer.AnnotationSettings.Squiggly.Color;
                        break;
                   
                }
            }
        }

        internal void DeselectSelectedAnnotation()
        {
            if (SelectedAnnotation != null)
                pdfViewer?.DeselectAnnotation(SelectedAnnotation);
        }

        internal void HideOverlayToolbars()
        {
            IsColorToolbarVisible = false;
            IsOpacityToolbarVisible = false;
            IsThicknessToolbarVisible = false;
            IsFillColorToolbarVisible = false;
            IsStampOpacityToolbarVisible = false;
        }

        void OnEditCommand(object parameter)
        {
            if (parameter.Equals("Thickness"))
            {
                if (pdfViewer.AnnotationMode == AnnotationMode.InkEraser)
                {
                    MinimumThickness = 10;
                    MaximumThickness = 100;
                }
                else
                {
                    MinimumThickness = 1;
                    MaximumThickness = 20;
                }
                IsThicknessToolbarVisible = !IsThicknessToolbarVisible;
                IsColorToolbarVisible = false;
                IsFillColorToolbarVisible = false;
            }
            else if (parameter.Equals("StampOpacity"))
            {
                IsThicknessToolbarVisible = false;
                IsStampOpacityToolbarVisible = !IsStampOpacityToolbarVisible;
            }
            else if (parameter.Equals("Opacity"))
            {
                IsOpacityToolbarVisible = !IsOpacityToolbarVisible;
            }
            else if (parameter.Equals("Color"))
            {
                IsFillColorToolbarVisible = false;
                IsStickyNoteToolbarVisible = false;
                IsColorToolbarVisible = !IsColorToolbarVisible;
                IsThicknessToolbarVisible = false;
            }
            else if (parameter.Equals("Fill"))
            {
                IsColorToolbarVisible = false;

                IsFillColorToolbarVisible = !IsFillColorToolbarVisible;
                IsThicknessToolbarVisible = false;
            }
            else if (parameter.Equals("Delete"))
            {
                if (SelectedAnnotation != null)
                    pdfViewer.RemoveAnnotation(SelectedAnnotation);
            }
            else if (parameter.Equals("StickyIconChange"))
            {
#if ANDROID || IOS
                IsStickyNoteToolbarVisible = !IsStickyNoteToolbarVisible;
                IsColorToolbarVisible = false;
                IsOpacityToolbarVisible = false;
#else
                IsStickyNoteListVisible = !IsStickyNoteListVisible;
#endif
                IsTextMarkupListVisible = false;
                IsStampListVisible = false;
                IsShapeListVisible = false;
                CloseAllDialogs();
            }
        }

        void OnToolbarCommand(object parameter)
        {
            if (parameter is string)
            {
                if (parameter.Equals("TextMarkup"))
                {
#if ANDROID || IOS
                    BottomToolbarContent = new TextMarkupToolbar(this);
#endif
                    if (pdfViewer.AnnotationMode == AnnotationMode.None)
                        IsTextMarkupListVisible = !IsTextMarkupListVisible;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
                    IsFileOperationListVisible = false;
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStampListVisible = false;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("Ink"))
                {
                    IsFileOperationListVisible = false;
                    IsTextMarkupListVisible = false;
                    IsShapeListVisible = false;
                    IsStampListVisible = false;
                    IsStickyNoteMode = false;
                    pdfViewer.AnnotationMode = pdfViewer.AnnotationMode == AnnotationMode.Ink ? AnnotationMode.None : AnnotationMode.Ink;
#if ANDROID || IOS
                    BottomToolbarContent = new InkToolbar(this);
#endif
                    ClearButtonHighlights();
                    InkHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    CloseAllDialogs();
                    SetSliderValues();
                }
                else if (parameter.Equals("InkEraser"))
                {
                    IsFileOperationListVisible = false;
                    IsTextMarkupListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
                    IsShapeListVisible = false;
                    IsStampListVisible = false;
                    pdfViewer.AnnotationMode = pdfViewer.AnnotationMode == AnnotationMode.InkEraser ? AnnotationMode.None : AnnotationMode.InkEraser;
                    ClearButtonHighlights();
                    InkEraserHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
#if ANDROID || IOS
                    if (pdfViewer.AnnotationMode == AnnotationMode.None)
                    {
                        BottomToolbarContent = new AnnotationToolbar(this);
                        MinimumThickness = 1;
                        MaximumThickness = 20;
                    }
                    else
                    {
                        BottomToolbarContent = new EraserToolbar(this);
                        MinimumThickness = 10;
                        MaximumThickness = 100;
                    }
#endif
                    CloseAllDialogs();
                    SetSliderValues();
                }
                else if (parameter.Equals("Shapes"))
                {
#if ANDROID || IOS
                    BottomToolbarContent = new ShapesToolbar(this);
#endif
                    if (pdfViewer.AnnotationMode == AnnotationMode.None)
                        IsShapeListVisible = !IsShapeListVisible;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
                    IsStickyNoteListVisible = false;
					IsFileOperationListVisible = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("FileOperation"))
                {
                    IsFileOperationListVisible = !IsFileOperationListVisible;
                    IsShapeListVisible = false;
                    IsStickyNoteMode = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ShapeHighlightColor = Colors.Transparent;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("Back"))
                {
#if ANDROID || IOS
                    CloseAllDialogs();
                    HideOverlayToolbars();
                    IsStickyNoteToolbarVisible = false;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    if (selectedAnnotation != null)
                        pdfViewer.DeselectAnnotation(selectedAnnotation);
                    else
                    {
                        if (bottomToolbarContent is ShapesToolbar || bottomToolbarContent is InkToolbar || bottomToolbarContent is TextMarkupToolbar || bottomToolbarContent is EraserToolbar || bottomToolbarContent is StickyNotePropertyToolbar)
                            BottomToolbarContent = new AnnotationToolbar(this);
                        else if (bottomToolbarContent is ShapesPropertyToolbar)
                            BottomToolbarContent = new ShapesToolbar(this);
                        else if (bottomToolbarContent is TextMarkUpPropertyToolbar)
                            BottomToolbarContent = new TextMarkupToolbar(this);
                    }
#endif
                }
                else if (parameter.Equals("Thickness"))
                {
                    if(pdfViewer.AnnotationMode == AnnotationMode.InkEraser) 
                    {
                        MinimumThickness = 10;
                        MaximumThickness = 100;
                    }
                    else
                    {
                        MinimumThickness = 1;
                        MaximumThickness = 20;
                    }
                    IsThicknessToolbarVisible = !IsThicknessToolbarVisible;
                    IsColorToolbarVisible = false;
                    IsFillColorToolbarVisible = false;
                }
                else if (parameter.Equals("StampOpacity"))
                {
                    IsThicknessToolbarVisible = false;
                    IsStampOpacityToolbarVisible = !IsStampOpacityToolbarVisible;
                }
                else if (parameter.Equals("Opacity"))
                {
                    IsOpacityToolbarVisible = !IsOpacityToolbarVisible;
                }
                else if (parameter.Equals("Color"))
                {
                    IsFillColorToolbarVisible = false;
                    IsStickyNoteToolbarVisible = false;
                    IsColorToolbarVisible = !IsColorToolbarVisible;
                    IsThicknessToolbarVisible = false;
                }
                else if (parameter.Equals("Fill"))
                {
                    IsColorToolbarVisible = false;

                    IsFillColorToolbarVisible = !IsFillColorToolbarVisible;
                    IsThicknessToolbarVisible = false;
                }
                else if (parameter.Equals("LockUnlockButton"))
                {
                    if (selectedAnnotation != null)
                    {
                        selectedAnnotation.IsLocked = !selectedAnnotation.IsLocked;
                        if (selectedAnnotation.IsLocked)
                        {
                            IsLockButtonVisible = false;
                            IsUnlockButtonVisible = true;
                            IsEditLayoutVisible = false;
                            IsDeleteButtonVisible = false;
                        }
                        else
                        {
                            IsLockButtonVisible = true;
                            IsUnlockButtonVisible = false;
                            IsEditLayoutVisible = true;
                            IsDeleteButtonVisible = true;
                        }
                    }
                    if (EditCommand is Command editCommand)
                        editCommand.ChangeCanExecute();
                }

                else if (parameter.Equals("Square") || parameter.Equals("Circle") || parameter.Equals("Line") || parameter.Equals("Arrow"))
                {
                    if (parameter is string annotationMode)
                        pdfViewer.AnnotationMode = (Syncfusion.Maui.PdfViewer.AnnotationMode)Enum.Parse(typeof(Syncfusion.Maui.PdfViewer.AnnotationMode), annotationMode, true);
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
#if ANDROID || IOS
                    BottomToolbarContent = new ShapesPropertyToolbar(this);
#endif
                    ClearButtonHighlights();
                    ShapeHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    SetSliderValues();
                }
                else if (parameter.Equals("Squiggly") || parameter.Equals("StrikeOut") || parameter.Equals("Underline") || parameter.Equals("Highlight"))
                {
                    if (parameter is string annotationMode)
                        pdfViewer.AnnotationMode = (Syncfusion.Maui.PdfViewer.AnnotationMode)Enum.Parse(typeof(Syncfusion.Maui.PdfViewer.AnnotationMode), annotationMode, true);
                    IsTextMarkupListVisible = false;
#if ANDROID || IOS
                    BottomToolbarContent = new TextMarkUpPropertyToolbar(this);
#endif
                    ClearButtonHighlights();
                    TextMarkupHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    SetSliderValues();
                }
                else if (parameter.Equals("Delete"))
                {
                    if (SelectedAnnotation != null)
                        pdfViewer.RemoveAnnotation(SelectedAnnotation);
                }
                else if (parameter.Equals("More"))
                {
                    bool showMoreOptions = ShowMoreOptions;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    BottomToolbarContent = new AnnotationToolbar(this);
                    CloseAllDialogs();
                    ShowMoreOptions = !showMoreOptions;
                }
                else if (parameter.Equals("Stamp"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
#if ANDROID || IOS
                    bool isStampViewVisible = IsStampViewVisible;
                    CloseAllDialogs();
                    IsStampViewVisible = !isStampViewVisible;
#elif MACCATALYST || WINDOWS
                    bool isStampListVisible = IsStampListVisible;
                    CloseAllDialogs();
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = !isStampListVisible;
#endif
                    StampHighlightColor = IsStampListVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                }
                else if (parameter.Equals("StickyNote"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    var stickyHighlightColor = stickyNoteHighlightColor;
                    ClearButtonHighlights();
                    StickyNoteHighlightColor = stickyHighlightColor == Colors.Transparent ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
#if ANDROID || IOS
                    BottomToolbarContent = new StickyNotePropertyToolbar(this);
#endif
                    IsStickyNoteMode = !IsStickyNoteMode;
                    StickyNoteSelected?.Invoke(this, EventArgs.Empty);
                    StickyIcon = StickyNoteIcon.Note;
                    SelectedColor = Colors.Yellow; //  Reset the selector color to default sticky note color
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    IsShapeListVisible = false;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("StickyIconChange"))
                {
#if ANDROID || IOS
                    IsStickyNoteToolbarVisible = !IsStickyNoteToolbarVisible;
                    IsColorToolbarVisible = false;
                    IsOpacityToolbarVisible = false;
#else
                    IsStickyNoteListVisible = !IsStickyNoteListVisible;
#endif
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    IsShapeListVisible = false;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("Note") || parameter.Equals("Comment") || parameter.Equals("Key") || parameter.Equals("Insert") || parameter.Equals("Help")
                    || parameter.Equals("Paragraph") || parameter.Equals("New Paragraph"))
                {
                    if (parameter is string annotationMode)
                    {
                        if (annotationMode.Equals("New Paragraph"))
                            annotationMode = "NewParagraph";
                        StickyIcon = (Syncfusion.Maui.PdfViewer.StickyNoteIcon)Enum.Parse(typeof(Syncfusion.Maui.PdfViewer.StickyNoteIcon), annotationMode, true);
                    }
                    if(SelectedAnnotation is StickyNoteAnnotation stickyNoteAnnotation)
                    {
                        stickyNoteAnnotation.Icon = StickyIcon;
                    }
                }
                else if (parameter.Equals("ColorPalette"))
                {
                    IsDeleteButtonVisible = pdfViewer.AnnotationMode == AnnotationMode.None;
                    if (pdfViewer.AnnotationMode == AnnotationMode.Square || pdfViewer.AnnotationMode == AnnotationMode.Circle || SelectedAnnotation is SquareAnnotation || SelectedAnnotation is CircleAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsShapeColorPalleteVisible = !IsShapeColorPalleteVisible;
                    }
                    else if (pdfViewer.AnnotationMode == AnnotationMode.Line || pdfViewer.AnnotationMode == AnnotationMode.Arrow || SelectedAnnotation is LineAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = !IsLineAndArrowColorPalleteVisible;
                    }
                    else if (pdfViewer.AnnotationMode == AnnotationMode.Squiggly || pdfViewer.AnnotationMode == AnnotationMode.Highlight || pdfViewer.AnnotationMode == AnnotationMode.StrikeOut || pdfViewer.AnnotationMode == AnnotationMode.Underline || SelectedAnnotation is UnderlineAnnotation || SelectedAnnotation is SquigglyAnnotation || SelectedAnnotation is HighlightAnnotation || SelectedAnnotation is StrikeOutAnnotation)
                    {
                        IsShapeColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible=false;
                        IsTextMarkUpColorPalleteVisible = !IsTextMarkUpColorPalleteVisible;
                    }
                    else if (pdfViewer.AnnotationMode == AnnotationMode.Ink || SelectedAnnotation is InkAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsInkColorPalleteVisible = !IsInkColorPalleteVisible;
                    }
                    if (SelectedAnnotation is StampAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsStampOpacitySliderbarVisible = !IsStampOpacitySliderbarVisible;
                    }
                    else if (SelectedAnnotation is StickyNoteAnnotation)
                    {
                        IsStickyNoteColorPalleteVisible = !IsStickyNoteColorPalleteVisible;
                        IsStickyNoteListVisible = false;
                        IsTextMarkUpColorPalleteVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsEraserThicknessToolbarVisible=false;
                    }
                    else if(pdfViewer.AnnotationMode == AnnotationMode.InkEraser)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsEraserThicknessToolbarVisible = !IsEraserThicknessToolbarVisible;
                    }
                }
                else if (parameter.Equals("Eraser"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.InkEraser;
                    SetSliderValues();
                }
                if (!parameter.Equals("ColorPalette"))
                {
                    IsShapeColorPalleteVisible = false;
                    IsInkColorPalleteVisible = false;
                    IsTextMarkUpColorPalleteVisible = false;
                    IsStampOpacitySliderbarVisible = false;
                    IsStickyNoteColorPalleteVisible = false;
                    IsLineAndArrowColorPalleteVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                }
            }
            SetSelectedAnnotationIcon();
            if (SelectedAnnotation == null)
                IsEditLayoutVisible = pdfViewer.AnnotationMode != AnnotationMode.None;
        }

        internal void ResetLayouts()
        {
            ClearButtonHighlights();
#if ANDROID || IOS
            CloseAllDialogs();
            IsStampViewVisible = false;
#elif MACCATALYST || WINDOWS
                    CloseAllDialogs();
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
#endif
        }

        internal void ClearButtonHighlights()
        {
            InkHighlightColor = Colors.Transparent;
            ShapeHighlightColor = Colors.Transparent;
            TextMarkupHighlightColor = Colors.Transparent;
            StickyNoteHighlightColor = Colors.Transparent;
            InkEraserHighlightColor = Colors.Transparent;
            StampHighlightColor = Colors.Transparent;
        }

        public void SetSelectedAnnotationIcon()
        {
            if (pdfViewer.AnnotationMode == AnnotationMode.Highlight || SelectedAnnotation is HighlightAnnotation)
                SelectedAnnotationIcon = "\uE760";
            if (pdfViewer.AnnotationMode == AnnotationMode.Underline || SelectedAnnotation is UnderlineAnnotation)
                SelectedAnnotationIcon = "\uE762";
            if (pdfViewer.AnnotationMode == AnnotationMode.StrikeOut || SelectedAnnotation is StrikeOutAnnotation)
                SelectedAnnotationIcon = "\uE763";
            if (pdfViewer.AnnotationMode == AnnotationMode.Squiggly || SelectedAnnotation is SquigglyAnnotation)
                SelectedAnnotationIcon = "\uE765";
            if (pdfViewer.AnnotationMode == AnnotationMode.Square || SelectedAnnotation is SquareAnnotation)
                SelectedAnnotationIcon = "\uE731";
            if (pdfViewer.AnnotationMode == AnnotationMode.Circle || SelectedAnnotation is CircleAnnotation)
                SelectedAnnotationIcon = "\uE73f";
            if (pdfViewer.AnnotationMode == AnnotationMode.Line || (SelectedAnnotation is LineAnnotation line && line.LineEndingStyle == LineEndingStyle.None))
                SelectedAnnotationIcon = "\uE73d";
            if (pdfViewer.AnnotationMode == AnnotationMode.Arrow || (SelectedAnnotation is LineAnnotation arrow && arrow.LineEndingStyle == LineEndingStyle.Open))
                SelectedAnnotationIcon = "\uE73c";
            if (pdfViewer.AnnotationMode == AnnotationMode.Ink || SelectedAnnotation is InkAnnotation)
                SelectedAnnotationIcon = "\uE766";
            if (pdfViewer.AnnotationMode == AnnotationMode.InkEraser)
                SelectedAnnotationIcon = "\uE764";
            if (SelectedAnnotation is StampAnnotation)
                SelectedAnnotationIcon = "\uE761";
            if (SelectedAnnotation is StickyNoteAnnotation || IsStickyNoteMode)
            {
                if (StickyIcon == StickyNoteIcon.Note)
                    SelectedAnnotationIcon = "\uE775";
                else if (StickyIcon == StickyNoteIcon.Comment)
                    SelectedAnnotationIcon = "\uE711";
                else if (StickyIcon == StickyNoteIcon.Key)
                    SelectedAnnotationIcon = "\uE777";
                else if (StickyIcon == StickyNoteIcon.Help)
                    SelectedAnnotationIcon = "\uE778";
                else if (StickyIcon == StickyNoteIcon.Insert)
                    SelectedAnnotationIcon = "\uE779";
                else if (StickyIcon == StickyNoteIcon.Paragraph)
                    SelectedAnnotationIcon = "\uE776";
                else if (StickyIcon == StickyNoteIcon.NewParagraph)
                    SelectedAnnotationIcon = "\uE77a";
            }
        }

        void OnOpacityCommand(object parameter)
        {
            if (parameter is float opacity)
            {
                if (selectedAnnotation != null)
                {
                    if (IsFillColorToolbarVisible || isDeskTopFillColorToolbarVisible)
                    {
                        if (selectedAnnotation is ShapeAnnotation shape)
                        {
                            Color existingFillColor = shape.FillColor;
                            Color modifiedFillColor = existingFillColor.WithAlpha(opacity);
                            shape.FillColor = modifiedFillColor;
                        }
                    }
                    else if (IsColorToolbarVisible || IsStampOpacityToolbarVisible || isDeskTopColorToolbarVisible || isStampOpacitySliderbarVisible)
                    {
                        selectedAnnotation.Opacity = opacity;
                        SelectedOpacity = opacity;
                    }
                }
                else
                {
                    if (isColorToolbarVisible || isDeskTopColorToolbarVisible || isInkColorPalleteVisible || isLineAndArrowColorPalleteVisible)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                pdfViewer.AnnotationSettings.Square.Opacity = opacity;
                                break;

                            case AnnotationMode.Circle:
                                pdfViewer.AnnotationSettings.Circle.Opacity = opacity;
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.Opacity = opacity;
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.Opacity = opacity;
                                break;
                            case AnnotationMode.Squiggly:
                                pdfViewer.AnnotationSettings.Squiggly.Opacity = opacity;
                                break;

                            case AnnotationMode.StrikeOut:
                                pdfViewer.AnnotationSettings.StrikeOut.Opacity = opacity;
                                break;

                            case AnnotationMode.Highlight:
                                pdfViewer.AnnotationSettings.Highlight.Opacity = opacity;
                                break;

                            case AnnotationMode.Underline:
                                pdfViewer.AnnotationSettings.Underline.Opacity = opacity;
                                break;
                            case AnnotationMode.Ink:
                                pdfViewer.AnnotationSettings.Ink.Opacity = opacity;
                                break;
                        }
                        SelectedOpacity = opacity;
                    }
                    else if (isFillColorToolbarVisible || isDeskTopFillColorToolbarVisible)
                    {

                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                Color existingSquareFillColor = pdfViewer.AnnotationSettings.Square.FillColor;
                                Color modifiedSquareFillColor = existingSquareFillColor.WithAlpha(opacity);
                                pdfViewer.AnnotationSettings.Square.FillColor = modifiedSquareFillColor;
                                break;

                            case AnnotationMode.Circle:
                                Color existingCircleFillColor = pdfViewer.AnnotationSettings.Circle.FillColor;
                                Color modifiedCircleFillColor = existingCircleFillColor.WithAlpha(opacity);
                                pdfViewer.AnnotationSettings.Circle.FillColor = modifiedCircleFillColor;
                                break;

                            case AnnotationMode.Line:
                                Color existingLineFillColor = pdfViewer.AnnotationSettings.Line.FillColor;
                                Color modifiedLineFillColor = existingLineFillColor.WithAlpha(opacity);
                                pdfViewer.AnnotationSettings.Line.FillColor = modifiedLineFillColor;
                                break;

                            case AnnotationMode.Arrow:
                                Color existingArrowFillColor = pdfViewer.AnnotationSettings.Arrow.FillColor;
                                Color modifiedArrowFillColor = existingArrowFillColor.WithAlpha(opacity);
                                pdfViewer.AnnotationSettings.Arrow.FillColor = modifiedArrowFillColor;
                                break;
                        }
                    }
                }
            }
        }
        void OnThicknessCommand(object parameter)
        {
            if (parameter is float thickness)
            {
                if (selectedAnnotation != null)
                {
                    if (selectedAnnotation is ShapeAnnotation shape)
                    {
                        shape.BorderWidth = thickness;
                    }
                    else if (selectedAnnotation is InkAnnotation ink)
                    {
                        ink.BorderWidth = thickness;
                    }
                    SelectedThickness = thickness;
                }
                else
                {
                    if (IsThicknessToolbarVisible || IsDeskTopColorToolbarVisible || IsInkColorPalleteVisible || IsLineAndArrowColorPalleteVisible)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                pdfViewer.AnnotationSettings.Square.BorderWidth = thickness;
                                break;

                            case AnnotationMode.Circle:
                                pdfViewer.AnnotationSettings.Circle.BorderWidth = thickness;
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.BorderWidth = thickness;
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.BorderWidth = thickness;
                                break;
                            case AnnotationMode.Ink:
                                pdfViewer.AnnotationSettings.Ink.BorderWidth = thickness;
                                break;
                            case AnnotationMode.InkEraser:
                                pdfViewer.AnnotationSettings.InkEraser.Thickness = thickness;
                                break;
                        }
                        SelectedThickness = thickness;
                    }
                }
            }
        }
        void OnColorCommand(object parameter)
        {
            if (parameter is Color color)
            {
                if (selectedAnnotation != null)
                {
                    if (isFillColorToolbarVisible || isDeskTopFillColorToolbarVisible)
                    {
                        if (selectedAnnotation is ShapeAnnotation shape)
                            shape.FillColor = color;
                    }
                    else if (isColorToolbarVisible || isDeskTopColorToolbarVisible || isInkColorPalleteVisible || isLineAndArrowColorPalleteVisible)
                    {
                        selectedAnnotation.Color = color;
                        SelectedColor = color;
                    }
                }
                else
                {
                    if (isFillColorToolbarVisible || isDeskTopFillColorToolbarVisible)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                pdfViewer.AnnotationSettings.Square.FillColor = color;
                                break;

                            case AnnotationMode.Circle:
                                pdfViewer.AnnotationSettings.Circle.FillColor = color;
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.FillColor = color;
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.FillColor = color;
                                break;
                        }
                    }
                    else if (isColorToolbarVisible || isDeskTopColorToolbarVisible || isInkColorPalleteVisible || IsLineAndArrowColorPalleteVisible)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                pdfViewer.AnnotationSettings.Square.Color = color;
                                break;

                            case AnnotationMode.Circle:
                                pdfViewer.AnnotationSettings.Circle.Color = color;
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.Color = color;
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.Color = color;
                                break;

                            case AnnotationMode.Highlight:
                                pdfViewer.AnnotationSettings.Highlight.Color = color;
                                SelectedColor = color;
                                break;

                            case AnnotationMode.Underline:
                                pdfViewer.AnnotationSettings.Underline.Color = color;
                                SelectedColor = color;
                                break;

                            case AnnotationMode.StrikeOut:
                                pdfViewer.AnnotationSettings.StrikeOut.Color = color;
                                SelectedColor = color;
                                break;

                            case AnnotationMode.Squiggly:
                                pdfViewer.AnnotationSettings.Squiggly.Color = color;
                                SelectedColor = color;
                                break;

                            case AnnotationMode.Ink:
                                pdfViewer.AnnotationSettings.Ink.Color = color;
                                SelectedColor = color;
                                break;
                        }
                    }
                }
            }
        }

#if WINDOWS
        async Task<string> WriteFile(Stream stream, string fileName)
        {
            string filePath = string.Empty;
            if (stream is MemoryStream memoryStream)
            {
                memoryStream.Position = 0;
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder?.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                if (file != null)
                {
                    filePath = file.Path;
                    Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    Stream st = fileStream.AsStreamForWrite();
                    st.SetLength(0);
                    st.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                    st.Flush();
                    st.Dispose();
                    fileStream.Dispose();
                }
            }
            return filePath;
        }
#else
        async Task<string> WriteFile(Stream stream, string fileName)
        {
            string filePath = string.Empty;
            if (stream != null)
            {
                stream.Position = 0;
#if ANDROID
                PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                filePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory!.AbsolutePath, fileName);
#else
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
#endif
                using (FileStream file = File.Create(filePath))
                {
                    await stream.CopyToAsync(file);
                }
            }
            return filePath;
        }
#endif

        internal async void SaveDocument()
        {
            IsFileOperationListVisible = false;
            ShowMoreOptions = false;
            MemoryStream stream = new MemoryStream();
            await pdfViewer.SaveDocumentAsync(stream);
#if WINDOWS
            string filePath = await WriteFile(stream, "SavedDocument.pdf");
#else
            string filePath =await WriteFile(stream, "SavedDocument.pdf");
#endif
            await Application.Current!.MainPage!.DisplayAlert("Document saved", $"The PDF has been saved to the file {filePath}", "OK");
        }
        internal async void ImportAnnotations()
        {
            IsFileOperationListVisible = false;
            ShowMoreOptions = false;
            string basePath = "SampleBrowser.Maui.Resources.Pdf";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.PdfViewer.Samples.PdfViewer.CustomToolbar.XFDF";
            Stream? stream = typeof(CustomToolbarViewModel).GetTypeInfo().Assembly.GetManifestResourceStream($"{basePath}.Annotations.xfdf");
            if (stream != null)
            {
                await pdfViewer.ImportAnnotationsAsync(stream, Syncfusion.Pdf.Parsing.AnnotationDataFormat.XFdf);
            }
        }
        
        internal async void ExportAnnotations()
        {
            IsFileOperationListVisible = false;
            ShowMoreOptions = false;
            MemoryStream stream = new MemoryStream();
            await pdfViewer.ExportAnnotationsAsync(stream, Syncfusion.Pdf.Parsing.AnnotationDataFormat.XFdf);
#if WINDOWS
            string filePath = await WriteFile(stream, "ExportedAnnotations.xfdf");
#else
            string filePath =await WriteFile(stream, "ExportedAnnotations.xfdf");
#endif
            await Application.Current!.MainPage!.DisplayAlert("Annotations exported", $"The annotations are exported to the file {filePath}", "OK");
        }
        internal bool AnnotationModeIsInkEraser()
        {
            return pdfViewer.AnnotationMode == AnnotationMode.InkEraser ? true : false;
        }
    }
}