#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.IO;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using System.ComponentModel;
using System.Windows.Input;
#if WINDOWS
using Windows.Storage;
#endif

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class CustomToolbarViewModel : INotifyPropertyChanged
    {
        private PdfData _documentData;
        private PdfFileData? fileData;
        private Thickness _textMarkupListMargin;
        private Thickness _shapeListMargin;
        private Thickness _stampListMargin;
        private Thickness _signatureListMargin;
        private Thickness _stickyNoteListMargin;
        private Thickness _fileOperationListMargin;
        private Thickness _colorPaletteMargin;
        private Thickness _freeTextFontSizeListMargin;
        private Thickness _thicknessButtonMargin;
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
        private Thickness _freeTextSliderMargin;
        private Thickness _freeTextFontMargin;
        private Thickness _freeTextColorMargin;
        private Thickness _freeTextFillColorMargin;
        private bool _showPasswordDialog = false;
        private bool _showMessageBoxDialog = false;
        private bool _showHyperlinkDialog = false;
        private bool _showOutlineView = false;
        private bool _showMoreView = false;
        private bool _showZoomView = false;
        private bool _showPageLayoutOptions = false;
        private bool _showZoomMobileView = false;
        private bool _showDesktopMoreView = false;
        private bool _isTextMarkupListVisible = false;
        private bool _isShapeListVisible = false;
        private bool _isStickyNoteListVisible = false;
        private bool _isFileOperationListVisible = false;
        private bool _isStampListVisible = false;
        private bool _isDeleteButtonVisible = false;
        private bool _isEditLayoutVisible = false;
        private bool _isSaveLayoutVisible = false;
        private bool _isStickyNoteMode = false;
        private bool _isStrokeOnlyShape = false;
        private bool _isfreeSliderVisible = false;
        private bool _isfreeTextFontListVisible = false;
        private bool _isfreeTextColorPalatteVisible = false;
        private bool _isfreeTextFillColorVisible = false;
        private bool _isfreetextFontColorChanging = false;
        private bool _isfreeTextFillChanging = false;
        private bool _isfreeTextstrokechanging = false;
        private bool _isfreeTextToolsVisible = false;
        private bool _isfontSliderVisible = false;
        private bool _isFreeTextFontColorPalatteVisible = false;
        private bool _isSecondaryToolbarVisible = false;
        private StickyNoteIcon _stickyNoteIcon = StickyNoteIcon.Note;
        private bool _isFileListViewVisible = false;
        private bool _isFreeTextFontSizeListViewVisible = false;
        private bool _isColorPaletteButtonVisible=false;
        private bool _isCloseButtoonVisible = false;
        private Color _selectedColor = new();
        bool m_isDocumentLoaded;
        bool _isReadOnly = false;
        bool _isEditMode = false;
        bool _isFileOpenVisible = true;
        bool _isPageLayoutVisible = true;
        bool isStickyNoteToolbarVisible;
        ContentView bottomToolbarContent;
        ICommand? toolbarCommand;
        ICommand? colorCommand;
        ICommand? opacityCommand;
        ICommand? thicknessCommand;
        ICommand? fontSizeCommand;
        bool isColorToolbarVisible;
        bool isAnnotationsToolsVisible = true;
        bool isShapeColorPalleteVisible = false;
        bool isInkColorPalleteVisible = false;
        bool isTextMarkUpColorPalleteVisible = false;
        bool isStickyNoteColorPalleteVisible = false;
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
        bool isSignatureViewVisible;
        bool zoomMobileShow = false;
        bool isLineAndArrowColorPalleteVisible;
        bool isLockVisible = false;
        bool isUnlockVisible = false;
        bool isStickyRelatedIcon = false;
        bool isThicknessSliderVisible=false;
        bool isThicknessButtonVisible=false;
        bool isFontSizeButtonViewVisible=false;
        Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer;
        Annotation? selectedAnnotation;
        Color? textMarkupHighlightColor = Colors.Transparent;
        Color? shapeHighlightColor = Colors.Transparent;
        Color? inkHighlightColor = Colors.Transparent;
        Color? inkEraserHighlightColor = Colors.Transparent;
        Color? stickyNoteHighlightColor = Colors.Transparent;
        Color? stampHighlightColor = Colors.Transparent;
        Color? freeTextHighlightColor = Colors.Transparent;
        Color? colorPaletteHighlightColor = Colors.Transparent;
        Color? pageLayoutHighlightColor = Colors.Transparent;
        Color? zoomMobileHighlightColor = Colors.Transparent;
        Color? moreOptionsHighlightColor = Colors.Transparent;
        Color? zoomHighlightColor = Colors.Transparent;
        Color? stickyNoteIconChangeHighlightColor = Colors.Transparent;
        Color? fileOperationHighlightColor = Colors.Transparent;
        Color? signatureHighlightColor = Colors.Transparent;
        Color? annotationHighlightColor= Colors.Transparent;
        Color? freeTextFontSizeHighlightColor=Colors.Transparent;
        Color? thicknessButtonHighlightColor = Colors.Transparent;
        float selectedOpacity;
        float selectedFontSize;
        float selectedFillColorOpacity = 1;
        float selectedThickness, minimumThickness = 1, maximumThickness = 20, minimumEraserThickness = 10, maximumEraserThickness = 100;
        string textMarkupButtonIcon = "\ue72e";
        string shapeButtonIcon = "\ue73b";
        string selectedAnnotationIcon = string.Empty;
        internal string _sampleName;
        int freeTextFontSize = 12;
        string freeTextFontSizeLabelText="12px";

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? StickyNoteSelected;
        internal event EventHandler? SignatureButtonClicked;

        /// <summary>
        ///  Returns the Pdf data. 
        /// </summary>
        public PdfData DocumentData => _documentData;

        /// <summary>
        /// Gets or sets the read only value
        /// </summary>
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }

        /// <summary>
        /// Gets or sets the read only sample value
        /// </summary>
        public bool IsFileOpenVisible
        {
            get => _isFileOpenVisible;
            set
            {
                if (_isFileOpenVisible != value)
                {
                    _isFileOpenVisible = value;
                    OnPropertyChanged(nameof(IsFileOpenVisible));
                }
            }
        }

        public bool IsPageLayoutVisible
        {
            get => _isPageLayoutVisible;
            set
            {
                if (_isPageLayoutVisible != value)
                {
                    _isPageLayoutVisible = value;
                    OnPropertyChanged(nameof(IsPageLayoutVisible));
                }
            }
        }

        /// <summary>
        /// Gets or sets the edit mode 
        /// </summary>
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (_isEditMode != value)
                {
                    _isEditMode = value;
                    OnPropertyChanged(nameof(IsEditMode));
                }
            }
        }

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

        public bool IsFontSliderVisible
        {
            get => _isfontSliderVisible;
            set
            {
                if (_isfontSliderVisible != value)
                {
                    _isfontSliderVisible = value;
                    OnPropertyChanged(nameof(IsFontSliderVisible));
                }
            }
        }

        public bool IsSecondaryToolbarVisible
        {
            get => _isSecondaryToolbarVisible;
            set
            {
                if (_isSecondaryToolbarVisible != value)
                {
                    _isSecondaryToolbarVisible = value;
                    OnPropertyChanged(nameof(IsSecondaryToolbarVisible));
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
                    if (_isFileOperationListVisible == false)
                        FileOperationHighlightColor = Colors.Transparent;
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

        public bool IsFontSizeButtonViewVisible
        {
            get => isFontSizeButtonViewVisible;
            set
            {
                if(isFontSizeButtonViewVisible!=value)
                {
                    isFontSizeButtonViewVisible = value;
                    OnPropertyChanged(nameof(IsFontSizeButtonViewVisible));
                }
            }
        }

        public Color? SignatureHighlightColor
        {
            get
            {
                return signatureHighlightColor;
            }
            set
            {
                if (signatureHighlightColor != value)
                {
                    signatureHighlightColor = value;
                    OnPropertyChanged(nameof(SignatureHighlightColor));
                }
            }
        }

        public Color? AnnotationHighlightColor
        {
            get
            {
                return annotationHighlightColor;
            }
            set
            {
                if (annotationHighlightColor != value)
                {
                    annotationHighlightColor = value;
                    OnPropertyChanged(nameof(AnnotationHighlightColor));
                }
            }
        }

        public Color? FreeTextFontSizeHighlightColor
        {
            get
            {
                return freeTextFontSizeHighlightColor;
            }
            set
            {
                if(freeTextFontSizeHighlightColor != value)
                {
                    freeTextFontSizeHighlightColor = value;
                    OnPropertyChanged(nameof(FreeTextFontSizeHighlightColor));
                }
            }
        }

        public Color? ThicknessButtonHighlightColor
        { 
           get
            {
                return thicknessButtonHighlightColor;
            }
            set
            {
                if(thicknessButtonHighlightColor!=value)
                {
                    thicknessButtonHighlightColor=value;
                }
            }
        }

        public bool IsThicknessButtonVisible
        {
            get => isThicknessButtonVisible;
            set
            {
                if(isThicknessButtonVisible != value)
                {
                    isThicknessButtonVisible = value;
                    OnPropertyChanged(nameof(IsThicknessButtonVisible));
                }
            }
        }

        public bool IsColorPaletteButtonVisible
        {
            get => _isColorPaletteButtonVisible;
            set
            {
                if(_isColorPaletteButtonVisible!= value)
                {
                    _isColorPaletteButtonVisible = value;
                    OnPropertyChanged(nameof(IsColorPaletteButtonVisible));
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

        public int FreeTextFontSize
        {
            get => freeTextFontSize;
            set
            {
                if (freeTextFontSize != value)
                {
                    freeTextFontSize = value;
                    OnPropertyChanged(nameof(FreeTextFontSize));
                }
            }
        }

        public Color? FreeTextHighlightColor
        {
            get
            {
                return freeTextHighlightColor;
            }
            set
            {
                if (freeTextHighlightColor != value)
                {
                    freeTextHighlightColor = value;
                    OnPropertyChanged(nameof(FreeTextHighlightColor));
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

        public bool IsFreeTextSliderVisible
        {
            get => _isfreeSliderVisible;
            set
            {
                if (_isfreeSliderVisible != value)
                {
                    _isfreeSliderVisible = value;
                    OnPropertyChanged(nameof(IsFreeTextSliderVisible));
                }
            }
        }

        public bool IsFreetextToolsVisible
        {
            get => _isfreeTextToolsVisible;
            set
            {
                if (_isfreeTextToolsVisible != value)
                {
                    _isfreeTextToolsVisible = value;
                    OnPropertyChanged(nameof(IsFreetextToolsVisible));
                }
            }
        }

        public bool IsFreeTextFontListVisible
        {
            get => _isfreeTextFontListVisible;
            set
            {
                if (_isfreeTextFontListVisible != value)
                {
                    _isfreeTextFontListVisible = value;
                    OnPropertyChanged(nameof(IsFreeTextFontListVisible));
                }
            }
        }

        public bool IsFreetextColorPalatteisible
        {
            get => _isfreeTextColorPalatteVisible;
            set
            {
                if (_isfreeTextColorPalatteVisible != value)
                {
                    _isfreeTextColorPalatteVisible = value;
                    OnPropertyChanged(nameof(IsFreetextColorPalatteisible));
                }
            }
        }

        public bool IsFreeTextFontColorPalatteVisible
        {
            get => _isFreeTextFontColorPalatteVisible;
            set
            {
                if (_isFreeTextFontColorPalatteVisible != value)
                {
                    _isFreeTextFontColorPalatteVisible = value;
                    OnPropertyChanged(nameof(_isFreeTextFontColorPalatteVisible));
                }
            }
        }

        public bool IsFreeTextFillColorVisble
        {
            get => _isfreeTextFillColorVisible;
            set
            {
                if (_isfreeTextFillColorVisible != value)
                {
                    _isfreeTextFillColorVisible = value;
                    OnPropertyChanged(nameof(IsFreeTextFillColorVisble));
                }
            }
        }

        public bool IsFreetextFontColorChanging
        {
            get => _isfreetextFontColorChanging;
            set
            {
                if (_isfreetextFontColorChanging != value)
                {
                    _isfreetextFontColorChanging = value;
                    OnPropertyChanged(nameof(IsFreetextFontColorChanging));
                }
            }
        }

        public bool IsFreetextFillColorChanging
        {
            get => _isfreeTextFillChanging;
            set
            {
                if (_isfreeTextFillChanging != value)
                {
                    _isfreeTextFillChanging = value;
                    OnPropertyChanged(nameof(IsFreetextFillColorChanging));
                }
            }
        }

        public bool IsFreetextStrokeColorChanging
        {
            get => _isfreeTextstrokechanging;
            set
            {
                if (_isfreeTextstrokechanging != value)
                {
                    _isfreeTextstrokechanging = value;
                    OnPropertyChanged(nameof(IsFreetextStrokeColorChanging));
                }
            }
        }
        public bool IsCloseButtonVisible
        {
            get => _isCloseButtoonVisible;
            set
            {
                if(_isCloseButtoonVisible !=value)
                {
                    _isCloseButtoonVisible = value;
                    OnPropertyChanged(nameof(IsCloseButtonVisible));
                }
            }
        }

        public Thickness FreeTextSliderMargin
        {
            get => _freeTextSliderMargin;
            set
            {
                if (_freeTextSliderMargin != value)
                {
                    _freeTextSliderMargin = value;
                    OnPropertyChanged(nameof(FreeTextSliderMargin));
                }
            }
        }

        public Thickness FreeTextFontMargin
        {
            get => _freeTextFontMargin;
            set
            {
                if (_freeTextFontMargin != value)
                {
                    _freeTextFontMargin = value;
                    OnPropertyChanged(nameof(FreeTextFontMargin));
                }
            }
        }

        public Thickness FreeTextColorMargin
        {
            get => _freeTextColorMargin;
            set
            {
                if (_freeTextColorMargin != value)
                {
                    _freeTextColorMargin = value;
                    OnPropertyChanged(nameof(FreeTextColorMargin));
                }
            }
        }

        public Thickness FreeTextFillColorMargin
        {
            get => _freeTextFillColorMargin;
            set
            {
                if (_freeTextFillColorMargin != value)
                {
                    _freeTextFillColorMargin = value;
                    OnPropertyChanged(nameof(FreeTextFillColorMargin));
                }
            }
        }

        public float SelectedFontSize
        {
            get => selectedFontSize;
            set
            {
                if (selectedFontSize != value)
                {
                    selectedFontSize = value;
                    OnPropertyChanged(nameof(SelectedFontSize));
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
        public bool ShowZoomLevel
        {
            get => _showZoomView;
            set
            {
                if (_showZoomView != value)
                {
                    _showZoomView = value;
                    OnPropertyChanged(nameof(ShowZoomLevel));
                }
            }
        }

        public bool ShowPageLayoutOptions
        {
            get => _showPageLayoutOptions;
            set
            {
                if (_showPageLayoutOptions != value)
                {
                    _showPageLayoutOptions = value;
                    OnPropertyChanged(nameof(ShowPageLayoutOptions));
                }
            }
        }
        public bool ShowZoomMobileView
        {
            get => _showZoomMobileView;
            set
            {
                if (_showZoomMobileView != value)
                {
                    _showZoomMobileView = value;
                    OnPropertyChanged(nameof(ShowZoomMobileView));
                }
            }
        }

        public bool ShowDesktopMoreOptions
        {
            get => _showDesktopMoreView;
            set
            {
                if (_showDesktopMoreView != value)
                {
                    _showDesktopMoreView = value;
                    OnPropertyChanged(nameof(ShowDesktopMoreOptions));
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
        public Thickness ThicknessButtonMargin
        {
            get => _thicknessButtonMargin;
            set
            {
                _thicknessButtonMargin = value;
                OnPropertyChanged(nameof(ThicknessButtonMargin));
            }
        }

        public Thickness FreeTextFontSizeListMargin
        {
            get => _freeTextFontSizeListMargin;
            set
            {
                _freeTextFontSizeListMargin = value;
                OnPropertyChanged(nameof(FreeTextFontSizeListMargin));
            }
        }

        public bool IsFreeTextFontSizeListViewVisible
        {
            get => _isFreeTextFontSizeListViewVisible;
            set
            {
                _isFreeTextFontSizeListViewVisible = value;
                OnPropertyChanged(nameof(IsFreeTextFontSizeListViewVisible));
            }
        }

        public string FreeTextFontSizeLabelText
        {
            get => freeTextFontSizeLabelText;
            set
            {
                freeTextFontSizeLabelText = value;
                OnPropertyChanged(nameof(FreeTextFontSizeLabelText));
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

        public Thickness SignatureListMargin
        {
            get => _signatureListMargin;
            set
            {
                _signatureListMargin = value;
                OnPropertyChanged(nameof(SignatureListMargin));
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

        public ICommand FontSizeCommand
        {
            get
            {
                return fontSizeCommand ?? (fontSizeCommand = new Command(OnFontSizeCommand));
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
                        ShowZoomLevel = false;
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

        public bool IsStrokeOnlyShape
        {
            get => _isStrokeOnlyShape;
            set
            {
                if (_isStrokeOnlyShape != value)
                {
                    _isStrokeOnlyShape = value;
                    OnPropertyChanged(nameof(IsStrokeOnlyShape));
                }
            }
        }

        public PdfFileData? FileData
        {
            get { return fileData; }
            set
            {
                if (fileData != value)
                {
                    fileData = value;
                    OnPropertyChanged(nameof(FileData));
                }
            }
        }
        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public CustomToolbarViewModel(Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer, string sampleName)
        {
            _documentData = new PdfData();
            _sampleName = sampleName;
#if ANDROID || IOS
            _isFileListViewVisible = true;
            if (_sampleName == "readOnly")
            {
                AssignPdfInfoAndLoad("restrictedDocument.pdf");
                IsAnnotationsToolsVisible = false;
            }
#else
            if (_sampleName == "readOnly")
            {
                AssignPdfInfoAndLoad("restrictedDocument.pdf");
                IsAnnotationsToolsVisible = false;
            }
            else
            {
                AssignPdfInfoAndLoad("PDF_Succinctly1.pdf");
            }
#endif
            this.pdfViewer = pdfViewer;
            AnnotationToolbar annotationToolbar = new AnnotationToolbar(this);
            bottomToolbarContent = annotationToolbar;
        }

        private void AnnotationToolbar_SignatureButtonClicked(object? sender, EventArgs e)
        {
            SignatureButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        internal void AssignPdfInfoAndLoad(string fileName)
        {
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
            if (string.IsNullOrEmpty(fileName) == false)
                FileData = new PdfFileData(fileName, this.GetType().Assembly.GetManifestResourceStream(basePath + fileName));
        }

        internal void CloseAllDialogs()
        {
            ShowPasswordDialog = false;
            ShowHyperlinkDialog = false;
            ShowOutlineView = false;
            IsFilePickerVisible = false;
            ShowMoreOptions = false;
            ShowZoomLevel = false;
            IsStampViewVisible = false;
            IsSignatureViewVisible = false;
            ShowDesktopMoreOptions = false;
            ShowPageLayoutOptions = false;
            ShowZoomMobileView = false;
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
                    AssignPdfInfoAndLoad("PDF_Succinctly1.pdf");
                    break;
                case "Rotated PDF":
                    AssignPdfInfoAndLoad("rotated_document.pdf");
                    break;
                case "Password protected PDF":
                    AssignPdfInfoAndLoad("password_protected_document.pdf");
                    break;
                case "Corrupted PDF":
                    AssignPdfInfoAndLoad("corrupted_document.pdf");
                    break;
                case "Single page PDF":
                    AssignPdfInfoAndLoad("Invoice.pdf");
                    break;
                case "Annotations PDF":
                    AssignPdfInfoAndLoad("Annotations.pdf");
                    break;
                case "Browse files on this device":
                    // Create file picker with file type as PDF.
                    var fileData = await FileService.OpenFile("pdf");
                    if (fileData != null)
                    {
                        IsFileListViewVisible = false;
                        FileData = fileData;
                    }
                    break;
            }
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
                            return !(SelectedAnnotation.IsLocked || AnnotationSettingsIsLocked());
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
                        if (IsReadOnly == true)
                        {
                            IsAnnotationsToolsVisible = false;
                        }
                        else
                        {
                            IsAnnotationsToolsVisible = true;
                        }
                        IsLockButtonVisible = IsUnlockButtonVisible = false;
                        HideOverlayToolbars();
                    }
                    else
                    {
                        CloseAllDialogs();
                        SetSliderValues();
                        SetSelectedAnnotationIcon();
#if ANDROID || IOS
                        if (selectedAnnotation is SquareAnnotation || selectedAnnotation is CircleAnnotation || selectedAnnotation is PolygonAnnotation)
                            IsStrokeOnlyShape = true;
                        else
                            IsStrokeOnlyShape = false;
                        if (selectedAnnotation is ShapeAnnotation)
                            BottomToolbarContent = new ShapesPropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is InkAnnotation)
                            BottomToolbarContent = new InkToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is HighlightAnnotation || selectedAnnotation is UnderlineAnnotation || selectedAnnotation is StrikeOutAnnotation || selectedAnnotation is SquigglyAnnotation)
                            BottomToolbarContent = new TextMarkUpPropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is StampAnnotation)
                            BottomToolbarContent = new StampPropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is StickyNoteAnnotation)
                            BottomToolbarContent = new StickyNotePropertyToolbar(this) { IsDeleteButtonVisible = true };
                        else if (selectedAnnotation is FreeTextAnnotation)
                            BottomToolbarContent = new FreetextPropertyToolbar(this) { IsDeleteButtonVisible = true };
#endif
                        if (selectedAnnotation is StickyNoteAnnotation stickyNoteAnnotation)
                        {
                            IsThicknessButtonVisible = false;
                            IsStickyIconChangeButtonVisible = true;
                            IsFontSizeButtonViewVisible = false;
                            IsColorPaletteButtonVisible = true;
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
                        else if(selectedAnnotation is FreeTextAnnotation freeTextAnnotation)
                        {
                            IsThicknessButtonVisible = true;
                            IsColorPaletteButtonVisible = true;
                            IsFontSizeButtonViewVisible = true;
                            IsStickyIconChangeButtonVisible = false;
                        }
                        else if(selectedAnnotation is InkAnnotation inkAnnotation)
                        {
                            IsColorPaletteButtonVisible = true;
                            IsThicknessButtonVisible = true;
                            IsFontSizeButtonViewVisible = false;
                            IsStickyIconChangeButtonVisible = false;
                        }
                        else if(selectedAnnotation is ShapeAnnotation shapeAnnotation)
                        {
                            IsThicknessButtonVisible = true;
                            IsColorPaletteButtonVisible = true;
                            IsFontSizeButtonViewVisible = false;
                            IsStickyIconChangeButtonVisible = false;
                        }
                        else
                        {
                            IsStickyIconChangeButtonVisible= false;
                            IsThicknessButtonVisible = false;
                            IsColorPaletteButtonVisible = true;
                            IsFontSizeButtonViewVisible = false;
                        }
                        IsEditLayoutVisible = true;
                        IsDeleteButtonVisible = true;
                        IsLockButtonVisible = true;
                        IsUnlockButtonVisible = false;
                        IsSecondaryToolbarVisible = true;
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

        public bool IsThicknessSliderbarVisible
        {
            get
            {
                return isThicknessSliderVisible;
            }
            set
            {
                if (isThicknessSliderVisible != value)
                {
                    isThicknessSliderVisible = value;
                    OnPropertyChanged(nameof(IsThicknessSliderbarVisible));
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

        public bool IsSignatureViewVisible
        {
            get
            {
                return isSignatureViewVisible;
            }
            set
            {
                if (isSignatureViewVisible != value)
                {
                    isSignatureViewVisible = value;
                    OnPropertyChanged(nameof(IsSignatureViewVisible));
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
        public bool ZoomMobileVisible
        {
            get => zoomMobileShow;
            set
            {
                if (zoomMobileShow != value)
                {
                    zoomMobileShow = value;
                    OnPropertyChanged(nameof(ZoomMobileVisible));
                };
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

        public Color? FileOperationHighlightColor
        {
            get
            {
                return fileOperationHighlightColor;
            }
            set
            {
                if (fileOperationHighlightColor != value)
                {
                    fileOperationHighlightColor = value;
                    OnPropertyChanged(nameof(FileOperationHighlightColor));
                }
            }
        }

        public Color? ColorPaletteHighlightColor
        {
            get
            {
                return colorPaletteHighlightColor;
            }
            set
            {
                if (colorPaletteHighlightColor != value)
                {
                    colorPaletteHighlightColor = value;
                    OnPropertyChanged(nameof(ColorPaletteHighlightColor));
                }
            }
        }

        public Color? PageLayoutHighlightColor
        {
            get => pageLayoutHighlightColor;
            set
            {
                if (pageLayoutHighlightColor != value)
                {
                    pageLayoutHighlightColor = value;
                    OnPropertyChanged(nameof(PageLayoutHighlightColor));
                }
            }
        }
        public Color? ZoomMobileHighlightColor
        {
            get => zoomMobileHighlightColor;
            set
            {
                if (zoomMobileHighlightColor != value)
                {
                    zoomMobileHighlightColor = value;
                    OnPropertyChanged(nameof(ZoomMobileHighlightColor));
                }
            }
        }

        public Color? MoreOptionsHighlightColor
        {
            get => moreOptionsHighlightColor;
            set
            {
                if (moreOptionsHighlightColor != value)
                {
                    moreOptionsHighlightColor = value;
                    OnPropertyChanged(nameof(MoreOptionsHighlightColor));
                }
            }
        }
        public Color? ZoomHighlightColor
        {
            get => zoomHighlightColor;
            set
            {
                if(zoomHighlightColor != value)
                {
                    zoomHighlightColor = value;
                    OnPropertyChanged(nameof(ZoomHighlightColor));
                }
            }
        }

        public Color? StickyNoteIconChangeHighlightColor
        {
            get
            {
                return stickyNoteIconChangeHighlightColor;
            }
            set
            {
                if (stickyNoteIconChangeHighlightColor != value)
                {
                    stickyNoteIconChangeHighlightColor = value;
                    OnPropertyChanged(nameof(StickyNoteIconChangeHighlightColor));
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

        public float SelectedFillColorOpacity
        {
            get
            {
                return selectedFillColorOpacity;
            }
            set
            {
                if (selectedFillColorOpacity != value)
                {
                    selectedFillColorOpacity = value;
                    OnPropertyChanged(nameof(SelectedFillColorOpacity));
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

        internal bool AnnotationSettingsIsLocked()
        {
            return (pdfViewer.AnnotationSettings.IsLocked || (pdfViewer.AnnotationSettings.Highlight.IsLocked && SelectedAnnotation is HighlightAnnotation) || (pdfViewer.AnnotationSettings.Underline.IsLocked && SelectedAnnotation is UnderlineAnnotation) || (pdfViewer.AnnotationSettings.Squiggly.IsLocked && SelectedAnnotation is SquigglyAnnotation) || (pdfViewer.AnnotationSettings.StrikeOut.IsLocked && SelectedAnnotation is StrikeOutAnnotation) || (pdfViewer.AnnotationSettings.Ink.IsLocked && SelectedAnnotation is InkAnnotation) || (pdfViewer.AnnotationSettings.Square.IsLocked && SelectedAnnotation is SquareAnnotation) || (pdfViewer.AnnotationSettings.Stamp.IsLocked && SelectedAnnotation is StampAnnotation) || (pdfViewer.AnnotationSettings.FreeText.IsLocked && SelectedAnnotation is FreeTextAnnotation) || (pdfViewer.AnnotationSettings.Circle.IsLocked && SelectedAnnotation is CircleAnnotation) || (pdfViewer.AnnotationSettings.StickyNote.IsLocked && SelectedAnnotation is StickyNoteAnnotation) || (pdfViewer.AnnotationSettings.Arrow.IsLocked && SelectedAnnotation is LineAnnotation) || (pdfViewer.AnnotationSettings.Line.IsLocked && SelectedAnnotation is LineAnnotation) || (pdfViewer.AnnotationSettings.Polygon.IsLocked && SelectedAnnotation is PolygonAnnotation) || (pdfViewer.AnnotationSettings.Polyline.IsLocked && SelectedAnnotation is PolylineAnnotation));
        }

        void SetSliderValues()
        {
            if (selectedAnnotation != null)
            {
                SelectedOpacity = selectedAnnotation.Opacity;
                SelectedFillColorOpacity = 1;
                if (selectedAnnotation is ShapeAnnotation shape)
                {
                    SelectedThickness = shape.BorderWidth;
                    SelectedOpacity = shape.Color.Alpha;
                }
                else if (selectedAnnotation is InkAnnotation ink)
                {
                    SelectedColor = ink.Color;
                    SelectedThickness = ink.BorderWidth;
                }
                else if (selectedAnnotation is FreeTextAnnotation freetext)
                {
                    SelectedOpacity = freetext.Opacity;
                    SelectedColor = freetext.Color;
                    SelectedThickness = freetext.BorderWidth;
                    SelectedFontSize = (float)freetext.FontSize;
                    FreeTextFontSize = (int)freetext.FontSize;
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

                    case AnnotationMode.Polyline:
                        SelectedThickness = pdfViewer.AnnotationSettings.Polyline.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Polyline.Opacity;
                        break;

                    case AnnotationMode.Polygon:
                        SelectedThickness = pdfViewer.AnnotationSettings.Polygon.BorderWidth;
                        SelectedOpacity = pdfViewer.AnnotationSettings.Polygon.Opacity;
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
                    case AnnotationMode.FreeText:
                        SelectedOpacity = pdfViewer.AnnotationSettings.FreeText.Color.Alpha;
                        SelectedColor = pdfViewer.AnnotationSettings.FreeText.Color;
                        SelectedThickness = pdfViewer.AnnotationSettings.FreeText.BorderWidth;
                        SelectedFontSize = (float)pdfViewer.AnnotationSettings.FreeText.FontSize;
                        FreeTextFontSize = (int)pdfViewer.AnnotationSettings.FreeText.FontSize;
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
            IsFillColorToolbarVisible = false;
            IsStampOpacityToolbarVisible = false;
            IsSignatureViewVisible = false;
            IsFontSliderVisible = false;
            IsFreeTextFontListVisible = false;
            IsThicknessSliderbarVisible = false;
            ShowDesktopMoreOptions = false;
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
                IsFontSliderVisible = false;
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
            else if (parameter.Equals("FontColor"))
            {
                IsFillColorToolbarVisible = false;
                IsStickyNoteToolbarVisible = false;
                IsFreeTextFontColorPalatteVisible = true;
                IsColorToolbarVisible = !IsColorToolbarVisible;
                IsThicknessToolbarVisible = false;
                IsFontSliderVisible = false;
                IsFreetextFillColorChanging = false;
                IsFreetextStrokeColorChanging = false;
            }
            else if (parameter.Equals("Color"))
            {
                IsFillColorToolbarVisible = false;
                IsStickyNoteToolbarVisible = false;
                IsFreeTextFontColorPalatteVisible = false;
                IsColorToolbarVisible = !IsColorToolbarVisible;
                IsThicknessToolbarVisible = false;
                IsFontSliderVisible = false;
                IsFreetextFillColorChanging = false;
                IsFreetextStrokeColorChanging = false;
            }
            else if (parameter.Equals("StrokeColor"))
            {
                IsFreetextFillColorChanging = false;
                IsFreetextStrokeColorChanging = true;
                IsFillColorToolbarVisible = false;
                IsStickyNoteToolbarVisible = false;
                IsFreeTextFontColorPalatteVisible = false;
                IsColorToolbarVisible = !IsColorToolbarVisible;
                IsThicknessToolbarVisible = false;
                IsFontSliderVisible = false;
            }
            else if (parameter.Equals("Fill"))
            {
                IsColorToolbarVisible = false;
                IsFreetextFillColorChanging = true;
                IsFreetextStrokeColorChanging = false;
                IsFreeTextFontColorPalatteVisible = false;
                IsFillColorToolbarVisible = !IsFillColorToolbarVisible;
                IsThicknessToolbarVisible = false;
                IsFontSliderVisible = false;
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
                    IsTextMarkupListVisible = !IsTextMarkupListVisible;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
                    TextMarkupHighlightColor = IsTextMarkupListVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    IsFileOperationListVisible = false;
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyIconChangeButtonVisible = false;
                    IsStampListVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsThicknessButtonVisible = false;
                    IsColorPaletteButtonVisible = false;
                    IsFontSizeButtonViewVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsThicknessSliderbarVisible = false;
                    IsStickyNoteMode = false;
                    FileOperationHighlightColor = Colors.Transparent;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("FreeTextColor"))
                {
                    IsFreetextColorPalatteisible = !IsFreetextColorPalatteisible;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextFontListVisible = false;
                    IsFreeTextFontSizeListViewVisible=false;
                    IsFreeTextSliderVisible = false;
                }
                else if (parameter.Equals("FreeText"))
                {
#if ANDROID || (IOS && !MACCATALYST)
                    pdfViewer.AnnotationMode = AnnotationMode.FreeText;
                    BottomToolbarContent = new FreetextPropertyToolbar(this);
#elif MACCATALYST || WINDOWS
                    if(pdfViewer.AnnotationMode == AnnotationMode.FreeText)
                    {
                        pdfViewer.AnnotationMode = AnnotationMode.None;
                        ClearButtonHighlights();
                    }
                    else
                    {
                        ClearButtonHighlights();
                        pdfViewer.AnnotationMode = AnnotationMode.FreeText;
                        ClearButtonHighlights();
                    }
                    
                    FreeTextHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent: Color.FromRgba(0, 0, 0, 20);
                    TextMarkupButtonIcon="\ue72e";
                    ShapeButtonIcon="\ue73b";
                    IsFreetextToolsVisible = true;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextSliderVisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextFontListVisible = false;
                    IsColorToolbarVisible = false;
                    IsFillColorToolbarVisible = false;
                    IsTextMarkupListVisible = false;
                    IsShapeListVisible = false;
                    IsStampListVisible = false;
                    IsStickyIconChangeButtonVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsInkColorPalleteVisible = false;
                    IsEditLayoutVisible = false;
                    IsDeleteButtonVisible = false;
                    FileOperationHighlightColor = Colors.Transparent;
                    IsFileOperationListVisible = false;
                    IsColorPaletteButtonVisible = true;
                    IsThicknessButtonVisible=true;
                    IsFontSizeButtonViewVisible=true;
                    IsColorPaletteButtonVisible=true;
                    IsFreeTextFontSizeListViewVisible=false;
                    IsThicknessSliderbarVisible=false;
                    CloseAllDialogs();
                    IsStickyNoteMode = false;
#endif
                    SetSliderValues();
                }
                else if(parameter.Equals("FreeTextFontSizeList"))
                {
                    if (SelectedAnnotation is FreeTextAnnotation freeTextAnnotation || pdfViewer.AnnotationMode==AnnotationMode.FreeText)
                    {
                        IsFreeTextFontSizeListViewVisible = !IsFreeTextFontSizeListViewVisible;
                        ClearButtonHighlights();
                        FreeTextFontSizeHighlightColor = IsFreeTextFontSizeListViewVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                        IsFileOperationListVisible = false;
                        IsShapeListVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampListVisible = false;
                        IsFreeTextFontListVisible = false;
                        IsFreetextColorPalatteisible = false;
                        IsFreeTextFillColorVisble = false;
                        IsFreeTextSliderVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsThicknessSliderbarVisible = false;
                        IsColorPaletteButtonVisible = true;
                        IsThicknessButtonVisible = true;
                        FileOperationHighlightColor = Colors.Transparent;
                    }
                }
                else if(parameter.Equals("ThicknessButton"))
                {
                    ClearButtonHighlights();
                    if (pdfViewer.AnnotationMode == AnnotationMode.Square || pdfViewer.AnnotationMode == AnnotationMode.Circle || pdfViewer.AnnotationMode == AnnotationMode.Polygon || pdfViewer.AnnotationMode == AnnotationMode.Line || pdfViewer.AnnotationMode == AnnotationMode.Arrow || pdfViewer.AnnotationMode == AnnotationMode.Polyline)
                    {
                        IsThicknessButtonVisible = true;
                        IsColorPaletteButtonVisible = true;
                        IsFontSizeButtonViewVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible= false;
                        IsThicknessSliderbarVisible = !IsThicknessSliderbarVisible;
                        ThicknessButtonHighlightColor = IsThicknessSliderbarVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    }
                    //940434 - Thickness palette does not open when using Shape annotation thickness icon[Windows, MAC]
                    else if (SelectedAnnotation is SquareAnnotation || SelectedAnnotation is CircleAnnotation || SelectedAnnotation is PolygonAnnotation || SelectedAnnotation is UnderlineAnnotation || SelectedAnnotation is SquigglyAnnotation || SelectedAnnotation is HighlightAnnotation || SelectedAnnotation is StrikeOutAnnotation || SelectedAnnotation is LineAnnotation || SelectedAnnotation is PolylineAnnotation)
                    {
                        IsColorPaletteButtonVisible = true;
                        IsThicknessButtonVisible = true;
                        IsFontSizeButtonViewVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsThicknessSliderbarVisible = !IsThicknessSliderbarVisible;
                        ThicknessButtonHighlightColor = IsThicknessSliderbarVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    }
                    else if (SelectedAnnotation is FreeTextAnnotation || pdfViewer.AnnotationMode==AnnotationMode.FreeText)
                    {
                        IsThicknessButtonVisible = true;
                        IsColorPaletteButtonVisible= true;
                        IsFontSizeButtonViewVisible= true;
                        IsEraserThicknessToolbarVisible = false;
                        IsThicknessSliderbarVisible = !IsThicknessSliderbarVisible;
                        ThicknessButtonHighlightColor = IsThicknessSliderbarVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    }
                    else if(SelectedAnnotation is InkAnnotation || pdfViewer.AnnotationMode==AnnotationMode.Ink)
                    {
                        IsInkColorPalleteVisible = false;
                        IsColorPaletteButtonVisible = true;
                        IsThicknessButtonVisible = true;
                        IsFontSizeButtonViewVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsThicknessSliderbarVisible = !IsThicknessSliderbarVisible;
                        ThicknessButtonHighlightColor = IsThicknessSliderbarVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    }
                    else if(pdfViewer.AnnotationMode==AnnotationMode.InkEraser)
                    {
                        IsColorPaletteButtonVisible = false;
                        IsFontSizeButtonViewVisible = false;
                        IsThicknessButtonVisible = true;
                        IsThicknessSliderbarVisible = false;
                        IsEraserThicknessToolbarVisible = !IsEraserThicknessToolbarVisible;
                        ThicknessButtonHighlightColor = IsEraserThicknessToolbarVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    }
                    IsFreeTextFontSizeListViewVisible= false;   
                    IsFileOperationListVisible = false;
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStampListVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    FileOperationHighlightColor = Colors.Transparent;
                }
                else if(parameter.Equals("10") || parameter.Equals("11") || parameter.Equals("12") || parameter.Equals("13") || parameter.Equals("14") || parameter.Equals("15") || parameter.Equals("16"))
                {
                    if(SelectedAnnotation is FreeTextAnnotation freetext)
                    {
                        switch (parameter)
                        {
                            case "10":
                                freetext.FontSize = 10;
                                break;
                            case "11":
                                freetext.FontSize = 11;
                                break;
                            case "12":
                                freetext.FontSize = 12;
                                break;
                            case "13":
                                freetext.FontSize = 13;
                                break;
                            case "14":
                                freetext.FontSize = 14;
                                break;
                            case "15":
                                freetext.FontSize = 15;
                                break;
                            case "16":
                                freetext.FontSize = 16;
                                break;
                        }
                       FreeTextFontSize=(int)freetext.FontSize;
                    }
                    else if(pdfViewer.AnnotationMode==AnnotationMode.FreeText)
                    {
                        switch(parameter)
                        {
                            case "10":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 10;
                                break;
                            case "11":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 11;
                                break;
                            case "12":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 12;
                                break;
                            case "13":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 13;
                                break;
                            case "14":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 14;
                                break;
                            case "15":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 15;
                                break;
                            case "16":
                                pdfViewer.AnnotationSettings.FreeText.FontSize = 16;
                                break;
                        }
                        FreeTextFontSize = (int)pdfViewer.AnnotationSettings.FreeText.FontSize;
                    }
                    FreeTextFontSizeLabelText = FreeTextFontSize.ToString() + "px";
                    ClearButtonHighlights();
                    FreeTextFontSizeHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    IsFreeTextFontSizeListViewVisible = false;
                    IsStrokeOnlyShape = false;
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsThicknessSliderbarVisible = false;
                    IsStickyNoteMode = false;
                    FileOperationHighlightColor = Colors.Transparent;
                    SetSliderValues();
                }
                else if (parameter.Equals("freeTextStroke"))
                {
                    IsFreeTextSliderVisible = !IsFreeTextSliderVisible;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextFontListVisible = false;
                    IsFontSliderVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    IsThicknessSliderbarVisible = false;
                }
                else if (parameter.Equals("FreeTextClose"))
                {
                    IsFreetextToolsVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextFontListVisible = false;
                    IsFreeTextSliderVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    IsThicknessSliderbarVisible = false;
                }
                else if (parameter.Equals("FontSize"))
                {
                    IsFreeTextFontListVisible = !IsFreeTextFontListVisible;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    IsThicknessSliderbarVisible = false;
#if ANDROID || IOS
                    IsFontSliderVisible = !IsFontSliderVisible;
                    IsFillColorToolbarVisible = false;
                    IsColorToolbarVisible = false;
                    IsThicknessToolbarVisible = false;
#endif
                }
                else if (parameter.Equals("Ink"))
                {
                    TextMarkupButtonIcon = "\ue72e";
                    ShapeButtonIcon= "\ue73b";
                    IsFileOperationListVisible = false;
                    IsTextMarkupListVisible = false;
                    IsShapeListVisible = false;
                    IsStampListVisible = false;
                    IsStickyNoteMode = false;
                    IsStickyIconChangeButtonVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsFontSizeButtonViewVisible=false;
                    IsEraserThicknessToolbarVisible=false;
                    IsColorPaletteButtonVisible = true;
                    IsThicknessButtonVisible = true;
                    IsThicknessSliderbarVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    ClearButtonHighlights();
                    FileOperationHighlightColor = Colors.Transparent;
                    pdfViewer.AnnotationMode = pdfViewer.AnnotationMode == AnnotationMode.Ink ? AnnotationMode.None : AnnotationMode.Ink;
#if ANDROID || IOS
                    BottomToolbarContent = new InkToolbar(this);
#endif
                    InkHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    CloseAllDialogs();
                    SetSliderValues();
                }
                else if (parameter.Equals("InkEraser"))
                {
                    TextMarkupButtonIcon = "\ue72e";
                    ShapeButtonIcon= "\ue73b";
                    IsFileOperationListVisible = false;
                    IsTextMarkupListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
                    IsShapeListVisible = false;
                    IsStampListVisible = false;
                    IsStickyIconChangeButtonVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    IsThicknessSliderbarVisible = false;
                    IsThicknessButtonVisible = true;
                    IsColorPaletteButtonVisible=false;
                    IsFontSizeButtonViewVisible = false;
                    pdfViewer.AnnotationMode = pdfViewer.AnnotationMode == AnnotationMode.InkEraser ? AnnotationMode.None : AnnotationMode.InkEraser;
                    ClearButtonHighlights();
                    FileOperationHighlightColor = Colors.Transparent;
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
                    IsShapeListVisible = !IsShapeListVisible;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
                    ShapeHighlightColor = IsShapeListVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    TextMarkupButtonIcon = "\ue72e";
                    ShapeButtonIcon= "\ue73b";
                    IsStickyNoteListVisible = false;
                    IsFileOperationListVisible = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsThicknessButtonVisible= false;
                    IsFreeTextSliderVisible = false;
                    IsFontSizeButtonViewVisible = false;
                    IsColorPaletteButtonVisible = false;
                    IsFreeTextFontSizeListViewVisible=false;
                    IsStickyNoteMode = false;
                    FileOperationHighlightColor = Colors.Transparent;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("FileOperation"))
                {
                    IsFileOperationListVisible = !IsFileOperationListVisible;
                    FileOperationHighlightColor = IsFileOperationListVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    IsShapeListVisible = false;
                    IsStickyNoteMode = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsFreeTextFontSizeListViewVisible=false;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ShapeHighlightColor = Colors.Transparent;
                    ClearButtonHighlights();
                    CloseAllDialogs();
                    ClearButtonHighlights();
                }
                else if (parameter.Equals("Back"))
                {
#if ANDROID || IOS
                    CloseAllDialogs();
                    HideOverlayToolbars();
                    IsStickyNoteToolbarVisible = false;
                    IsStickyNoteMode = false;
                    IsThicknessToolbarVisible = false;
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    if (selectedAnnotation != null)
                        pdfViewer.DeselectAnnotation(selectedAnnotation);
                    else
                    {
                        if (bottomToolbarContent is ShapesToolbar || bottomToolbarContent is InkToolbar || bottomToolbarContent is TextMarkupToolbar || bottomToolbarContent is EraserToolbar || bottomToolbarContent is StickyNotePropertyToolbar || BottomToolbarContent is FreetextPropertyToolbar)
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
                    IsFontSliderVisible = false;
                }
                else if (parameter.Equals("StampOpacity"))
                {
                    IsThicknessSliderbarVisible = false;
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
                    IsFreeTextFontColorPalatteVisible = false;
                    IsColorToolbarVisible = !IsColorToolbarVisible;
                    IsThicknessSliderbarVisible = false;
                }
                else if (parameter.Equals("Fill"))
                {
                    IsColorToolbarVisible = false;
                    IsFreeTextFontColorPalatteVisible = false;
                    IsFillColorToolbarVisible = !IsFillColorToolbarVisible;
                    IsThicknessSliderbarVisible = false;
                }
                else if (parameter.Equals("LockUnlockButton"))
                {
                    if (selectedAnnotation != null)
                    {
                        //934549 - [WinUI] Color and delete buttons are shown for locked annotations, 934555 - [WinUI] Unlock button is not showing when a locked annotation is selected
                        if (AnnotationSettingsIsLocked())
                        {
                            IsThicknessButtonVisible = false;
                            IsColorPaletteButtonVisible = false;
                            IsDeleteButtonVisible = false;
                            IsFontSizeButtonViewVisible = false;
                            IsStickyIconChangeButtonVisible = false;
                            IsUnlockButtonVisible = false;
                            IsLockButtonVisible = true;
                        }
                        else
                        {
                            selectedAnnotation.IsLocked = !selectedAnnotation.IsLocked;
                            if (selectedAnnotation.IsLocked)
                            {
                                IsLockButtonVisible = false;
                                IsUnlockButtonVisible = true;
                                IsEditLayoutVisible = false;
                                IsThicknessButtonVisible = false;
                                IsColorPaletteButtonVisible = false;
                                IsDeleteButtonVisible = false;
                                IsFontSizeButtonViewVisible = false;
                                IsStickyIconChangeButtonVisible = false;
                            }
                            else
                            {
                                IsLockButtonVisible = true;
                                IsUnlockButtonVisible = false;
                                IsEditLayoutVisible = true;
                                IsDeleteButtonVisible = true;
                                if (SelectedAnnotation is HighlightAnnotation || SelectedAnnotation is SquigglyAnnotation || SelectedAnnotation is StrikeOutAnnotation || SelectedAnnotation is UnderlineAnnotation)
                                {
                                    IsThicknessButtonVisible = false;
                                    IsColorPaletteButtonVisible = true;
                                    IsFontSizeButtonViewVisible = false;
                                    IsStickyIconChangeButtonVisible = false;
                                }
                                if (SelectedAnnotation is LineAnnotation || SelectedAnnotation is PolygonAnnotation || SelectedAnnotation is PolylineAnnotation || SelectedAnnotation is CircleAnnotation || SelectedAnnotation is SquareAnnotation || SelectedAnnotation is InkAnnotation)
                                {
                                    IsThicknessButtonVisible = true;
                                    IsColorPaletteButtonVisible = true;
                                    IsFontSizeButtonViewVisible = false;
                                    IsStickyIconChangeButtonVisible = false;
                                }
                                if (SelectedAnnotation is StampAnnotation)
                                {
                                    IsThicknessButtonVisible = false;
                                    IsColorPaletteButtonVisible = true;
                                    IsFontSizeButtonViewVisible = false;
                                    IsStickyIconChangeButtonVisible = false;
                                }
                                if (SelectedAnnotation is FreeTextAnnotation)
                                {
                                    IsThicknessButtonVisible = true;
                                    IsColorPaletteButtonVisible = true;
                                    IsFontSizeButtonViewVisible = true;
                                    IsStickyIconChangeButtonVisible = false;
                                }
                                if (SelectedAnnotation is StickyNoteAnnotation stickyNoteAnnotation)
                                {
                                    IsThicknessButtonVisible = false;
                                    IsColorPaletteButtonVisible = true;
                                    IsFontSizeButtonViewVisible = false;
                                    IsStickyIconChangeButtonVisible = true;
                                    StickyIcon = stickyNoteAnnotation.Icon;
                                }
                            }
                        }
                    }
                    if (EditCommand is Command editCommand)
                        editCommand.ChangeCanExecute();
                }

                else if (parameter.Equals("Square") || parameter.Equals("Circle") || parameter.Equals("Line") || parameter.Equals("Arrow") || parameter.Equals("Polyline") || parameter.Equals("Polygon") || parameter.Equals("Cloud"))
                {
                    pdfViewer.AnnotationSettings.Polygon.BorderStyle = BorderStyle.Solid;
                    pdfViewer.AnnotationSettings.Square.BorderStyle = BorderStyle.Solid;
                    if (parameter.Equals("Cloud"))
                    {
                        pdfViewer.AnnotationMode = AnnotationMode.Polygon;
                        pdfViewer.AnnotationSettings.Polygon.BorderStyle = BorderStyle.Cloudy;
                    }
                    else if(parameter is string annotationMode)
                        pdfViewer.AnnotationMode = (Syncfusion.Maui.PdfViewer.AnnotationMode)Enum.Parse(typeof(Syncfusion.Maui.PdfViewer.AnnotationMode), annotationMode, true);
                    
                    if (pdfViewer.AnnotationMode == AnnotationMode.Polyline || pdfViewer.AnnotationMode == AnnotationMode.Line || pdfViewer.AnnotationMode == AnnotationMode.Arrow)
                        IsStrokeOnlyShape = false;
                    else
                        IsStrokeOnlyShape = true;
                    IsFontSizeButtonViewVisible = false;
                    IsColorPaletteButtonVisible = true;
                    IsThicknessButtonVisible = true;
                    IsThicknessSliderbarVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
#if ANDROID || IOS
                    BottomToolbarContent = new ShapesPropertyToolbar(this);
#endif
                    ClearButtonHighlights();
                    ShapeHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    if(parameter.Equals("Square"))
                    {
                        ShapeButtonIcon = "\uE731";
                    }
                    else if(parameter.Equals("Circle"))
                    {
                        ShapeButtonIcon = "\uE73f";
                    }
                    else if(parameter.Equals("Line"))
                    {
                        ShapeButtonIcon = "\uE73d";
                    }
                    else if(parameter.Equals("Arrow"))
                    {
                        ShapeButtonIcon = "\uE73c";
                    }
                    else if(parameter.Equals("Polyline"))
                    {
                        ShapeButtonIcon = "\uE786";
                    }
                    else if(parameter.Equals("Polygon"))
                    {
                        ShapeButtonIcon = "\uE789";
                    }
                    else if(parameter.Equals("Cloud"))
                    {
                        ShapeButtonIcon = "\uE783";
                    }
                    SetSliderValues();
                }
                else if (parameter.Equals("Squiggly") || parameter.Equals("StrikeOut") || parameter.Equals("Underline") || parameter.Equals("Highlight"))
                {
                    if (parameter is string annotationMode)
                        pdfViewer.AnnotationMode = (Syncfusion.Maui.PdfViewer.AnnotationMode)Enum.Parse(typeof(Syncfusion.Maui.PdfViewer.AnnotationMode), annotationMode, true);
                    IsTextMarkupListVisible = false;
                    IsColorPaletteButtonVisible = true;
                    IsThicknessButtonVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    IsFontSizeButtonViewVisible = false;
                    IsStrokeOnlyShape = false;
#if ANDROID || IOS
                    BottomToolbarContent = new TextMarkUpPropertyToolbar(this);
#endif
                    ClearButtonHighlights();
                    TextMarkupHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.None ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    if(parameter.Equals("Squiggly"))
                    {
                        TextMarkupButtonIcon = "\uE765";
                    }
                    else if(parameter.Equals("StrikeOut"))
                    {
                        TextMarkupButtonIcon = "\uE763";
                    }
                    else if(parameter.Equals("Underline"))
                    {
                        TextMarkupButtonIcon = "\uE762";
                    }
                    else if(parameter.Equals("Highlight"))
                    {
                        TextMarkupButtonIcon = "\uE760";
                    }
                    SetSliderValues();
                }
                else if (parameter.Equals("Delete"))
                {
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextFontListVisible = false;
                    IsFreeTextSliderVisible = false;
                    IsFontSliderVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    if (SelectedAnnotation != null)
                        pdfViewer.RemoveAnnotation(SelectedAnnotation);
                }
                else if (parameter.Equals("Zoom"))
                {
                    ClearButtonHighlights();
#if WINDOWS || MACCATALYST
                var showZoomLevel=ShowZoomLevel;
                CloseAllDialogs();
                ShowZoomLevel=!showZoomLevel;
                IsStickyNoteMode = false;
#endif

                }
                else if (parameter.Equals("More"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
                    FileOperationHighlightColor = Colors.Transparent;
                    IsFileOperationListVisible = false;
#if !WINDOWS && !MACCATALYST
                    bool showMoreOptions = ShowMoreOptions;
                    BottomToolbarContent = new AnnotationToolbar(this);
                    CloseAllDialogs();
                    ShowMoreOptions = !showMoreOptions;
                    MoreOptionsHighlightColor = !ShowMoreOptions ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
#else
                    bool showMoreOptions = ShowDesktopMoreOptions;
                    CloseAllDialogs();
                    ShowDesktopMoreOptions = !showMoreOptions;
                    IsStickyNoteMode = false;
#endif
                }
                else if (parameter.Equals("ZoomMobile"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
#if !WINDOWS && !MACCATALYST
                    bool showZoomView = ShowZoomMobileView;
                    BottomToolbarContent = new AnnotationToolbar(this);
                    CloseAllDialogs();
                    ShowZoomMobileView = !showZoomView;
                    ZoomMobileHighlightColor = !ShowZoomMobileView ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
#endif

                }
                else if (parameter.Equals("PageLayout"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
                    IsStickyNoteMode = false;
#if !WINDOWS && !MACCATALYST
                    bool showLayoutOptions = ShowPageLayoutOptions;
                    BottomToolbarContent = new AnnotationToolbar(this);
                    CloseAllDialogs();
                    ShowPageLayoutOptions = !showLayoutOptions;
                    PageLayoutHighlightColor = !ShowPageLayoutOptions ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
#endif
                }
                else if (parameter.Equals("SearchButton"))
                {
                    ClearButtonHighlights();
                    IsStickyNoteMode = false;
                }
                else if (parameter.Equals("Stamp"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    ClearButtonHighlights();
#if ANDROID || IOS
                    bool isStampViewVisible = IsStampViewVisible;
                    CloseAllDialogs();
                    IsStampViewVisible = !isStampViewVisible;
                    IsFileOperationListVisible = false;
#elif MACCATALYST || WINDOWS
                    bool isStampListVisible = IsStampListVisible;
                    CloseAllDialogs();
                    TextMarkupButtonIcon="\ue72e";
                    ShapeButtonIcon="\ue73b";
                    IsShapeListVisible = false;
                    IsStickyNoteListVisible = false;
                    IsStickyNoteMode = false;
                    IsStickyIconChangeButtonVisible = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = !isStampListVisible;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsFileOperationListVisible = false;
                    IsThicknessSliderbarVisible = false;
                    IsThicknessButtonVisible = false;
                    IsFreeTextFontSizeListViewVisible =false;
                    IsEraserThicknessToolbarVisible = false;
                    IsColorPaletteButtonVisible = true;
                    FileOperationHighlightColor = Colors.Transparent;
#endif
                    StampHighlightColor = IsStampListVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                }
                else if (parameter.Equals("StickyNote"))
                {
                    ClearButtonHighlights();
#if ANDROID || IOS
                    BottomToolbarContent = new StickyNotePropertyToolbar(this);
#endif
                    IsStickyNoteMode = !IsStickyNoteMode;
                    StickyNoteSelected?.Invoke(this, EventArgs.Empty);
                    pdfViewer.AnnotationMode = pdfViewer.AnnotationMode == AnnotationMode.None ? AnnotationMode.StickyNote : AnnotationMode.None;
                    StickyNoteHighlightColor = pdfViewer.AnnotationMode == AnnotationMode.StickyNote ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    StickyIcon = StickyNoteIcon.Note;
                    SelectedColor = Colors.Yellow; //  Reset the selector color to default sticky note color
                    TextMarkupButtonIcon = "\ue72e";
                    ShapeButtonIcon= "\ue73b";
                    IsStickyIconChangeButtonVisible = true;
                    IsColorPaletteButtonVisible = true;
                    IsFontSizeButtonViewVisible = false;
                    IsTextMarkupListVisible = false;
                    IsStampListVisible = false;
                    IsShapeListVisible = false;
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    FileOperationHighlightColor = Colors.Transparent;
                    IsFileOperationListVisible = false;
                    IsFreeTextFontSizeListViewVisible = false;
                    IsThicknessButtonVisible = false;
                    IsEraserThicknessToolbarVisible = false;
                    CloseAllDialogs();
                }
                else if (parameter.Equals("StickyIconChange"))
                {
                    ColorPaletteHighlightColor = Colors.Transparent;
                    StickyNoteIconChangeHighlightColor = IsStickyNoteListVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    IsFreeTextFontListVisible = false;
                    IsFreetextColorPalatteisible = false;
                    IsFreeTextFillColorVisble = false;
                    IsFreeTextSliderVisible = false;
                    IsFreeTextFontSizeListViewVisible=false;
                    IsStickyNoteMode = false;
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
                    isStickyRelatedIcon = true;
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
                    if (SelectedAnnotation is StickyNoteAnnotation stickyNoteAnnotation)
                    {
                        stickyNoteAnnotation.Icon = StickyIcon;
                    }
                    isStickyRelatedIcon = true;
                }
                else if (parameter.Equals("FreeTextColor"))
                {
                    IsStickyNoteColorPalleteVisible = true;
                }
                else if (parameter.Equals("ColorPalette"))
                {
                    StickyNoteIconChangeHighlightColor = Colors.Transparent;
                    IsDeleteButtonVisible = pdfViewer.AnnotationMode == AnnotationMode.None;
                    if (pdfViewer.AnnotationMode == AnnotationMode.Square || pdfViewer.AnnotationMode == AnnotationMode.Circle || pdfViewer.AnnotationMode == AnnotationMode.Polygon || SelectedAnnotation is SquareAnnotation || SelectedAnnotation is CircleAnnotation || SelectedAnnotation is PolygonAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsThicknessSliderbarVisible=false;
                        IsShapeColorPalleteVisible = !IsShapeColorPalleteVisible;
                        ColorPaletteHighlightColor = !IsShapeColorPalleteVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    }
                    else if (pdfViewer.AnnotationMode == AnnotationMode.Line || pdfViewer.AnnotationMode == AnnotationMode.Arrow || pdfViewer.AnnotationMode == AnnotationMode.Polyline || SelectedAnnotation is LineAnnotation || SelectedAnnotation is PolylineAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsThicknessSliderbarVisible = false;
                        IsLineAndArrowColorPalleteVisible = !IsLineAndArrowColorPalleteVisible;
                        ColorPaletteHighlightColor = !IsLineAndArrowColorPalleteVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
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
                        IsEraserThicknessToolbarVisible = false;
                        IsStrokeOnlyShape = false;
                        IsThicknessSliderbarVisible = false;
                        IsTextMarkUpColorPalleteVisible = !IsTextMarkUpColorPalleteVisible;
                        ColorPaletteHighlightColor = !IsTextMarkUpColorPalleteVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
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
                        IsThicknessSliderbarVisible = false;
                        IsInkColorPalleteVisible = !IsInkColorPalleteVisible;
                        ColorPaletteHighlightColor = !IsInkColorPalleteVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
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
                        ColorPaletteHighlightColor = !IsStampOpacitySliderbarVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
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
                        IsEraserThicknessToolbarVisible = false;
                        IsThicknessSliderbarVisible=false;
                        ColorPaletteHighlightColor = !IsStickyNoteColorPalleteVisible ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    }
                    else if (pdfViewer.AnnotationMode == AnnotationMode.FreeText || SelectedAnnotation is FreeTextAnnotation)
                    {
                        IsTextMarkUpColorPalleteVisible = false;
                        IsInkColorPalleteVisible = false;
                        IsStickyNoteColorPalleteVisible = false;
                        IsLineAndArrowColorPalleteVisible = false;
                        IsStickyNoteListVisible = false;
                        IsStampOpacitySliderbarVisible = false;
                        IsStickyIconChangeButtonVisible = false;
                        IsShapeColorPalleteVisible = false;
                        IsThicknessSliderbarVisible = false;
                        IsFreeTextFontSizeListViewVisible = false;
                        IsEraserThicknessToolbarVisible = false;
                        IsFreeTextFillColorVisble = !IsFreeTextFillColorVisble;
                        ColorPaletteHighlightColor = !IsFreeTextFillColorVisble ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                    }
                }
                else if (parameter.Equals("Eraser"))
                {
                    pdfViewer.AnnotationMode = AnnotationMode.InkEraser;
                    SetSliderValues();
                }
                if (!parameter.Equals("ColorPalette") && !parameter.Equals("ThicknessButton") && !parameter.Equals("LockUnlockButton"))
                {
                    HideDeskTopOverLays();
                }
                if (parameter.Equals("FreeTextFill"))
                {
                    IsFreeTextFontColorPalatteVisible = false;
                    IsFreeTextFillColorVisble = !IsFreeTextFillColorVisble;
                    ColorPaletteHighlightColor = !IsFreeTextFillColorVisble ? Colors.Transparent : Color.FromRgba(0, 0, 0, 20);
                }
                if (parameter.Equals("Annotations"))
                {
                    CloseAllDialogs();
                    ResetLayouts();
                    pdfViewer.AnnotationMode = AnnotationMode.None;
                    IsEditLayoutVisible = false;
                    IsDeleteButtonVisible = false;
                    IsFileOperationListVisible = false;
                    FileOperationHighlightColor = Colors.Transparent;
                    IsCloseButtonVisible = true;
                    IsSecondaryToolbarVisible = !IsSecondaryToolbarVisible;
                    AnnotationHighlightColor = IsSecondaryToolbarVisible ? Color.FromRgba(0,0,0,20) : Colors.Transparent;
                }
                else if(parameter.Equals("Close"))
                {
                    CloseAllDialogs();
                    ResetLayouts();
                    pdfViewer.AnnotationMode= AnnotationMode.None;
                    IsEditLayoutVisible = false;
                    IsDeleteButtonVisible = false;
                    IsSecondaryToolbarVisible = false;
                    IsCloseButtonVisible= false;
                    AnnotationHighlightColor= Colors.Transparent;
                }
                if (parameter.Equals("Continuous page"))
                {
                    pdfViewer.PageLayoutMode = PageLayoutMode.Continuous;
                    ShowDesktopMoreOptions = false;
                    ShowPageLayoutOptions = false;
                    ShowZoomMobileView= false;  
                }
                else if (parameter.Equals("Page by page"))
                {
                    pdfViewer.PageLayoutMode = PageLayoutMode.Single;
                    ShowDesktopMoreOptions = false;
                    ShowPageLayoutOptions = false;
                    ShowZoomMobileView = false;
                }
                else if(parameter.Equals("25%"))
                {
                    pdfViewer.ZoomFactor = 0.25;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                else if (parameter.Equals("50%"))
                {
                    pdfViewer.ZoomFactor = 0.5;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                else if (parameter.Equals("100%"))
                {
                    pdfViewer.ZoomFactor = 1;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                else if (parameter.Equals("125%"))
                {
                    pdfViewer.ZoomFactor = 1.25;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                else if (parameter.Equals("150%"))
                {
                    pdfViewer.ZoomFactor = 1.5;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                else if (parameter.Equals("\ue79b"))
                {
                    pdfViewer.ZoomMode=ZoomMode.FitToWidth;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                else if (parameter.Equals("\ue79d"))
                {
                    pdfViewer.ZoomMode = ZoomMode.FitToPage;
                    ShowZoomLevel = false;
                    ZoomHighlightColor = Colors.Transparent;
                }
                if (parameter.Equals("Signature"))
                {
                    bool isSignatureViewVisible = IsSignatureViewVisible;
                    CloseAllDialogs();
                    IsSignatureViewVisible = !isSignatureViewVisible;
                    IsFileOperationListVisible = false;
                    IsStickyNoteMode = false;
                    ClearButtonHighlights();
                }
                if (parameter.Equals("Outline"))
                {
                    FileOperationHighlightColor = Colors.Transparent;
                    IsFileOperationListVisible = false;
                    IsStickyNoteMode = false;
                }
                if (parameter.Equals("Search"))
                {
                    FileOperationHighlightColor = Colors.Transparent;
                    IsFileOperationListVisible = false;
                    IsStickyNoteMode = false;
                }
            }
            SetSelectedAnnotationIcon();
            if (SelectedAnnotation == null)
                IsEditLayoutVisible = pdfViewer.AnnotationMode != AnnotationMode.None;
            if (pdfViewer.AnnotationMode == AnnotationMode.FreeText)
                IsEditLayoutVisible = true;
            else if (pdfViewer.AnnotationMode == AnnotationMode.Signature)
                IsEditLayoutVisible = false;
            else if(SelectedAnnotation is FreeTextAnnotation freeTextAnnotation)
                IsEditLayoutVisible = true;
        }

        internal void CloseFreeTextColorPallete()
        {
            IsFreetextStrokeColorChanging = false;
            IsFreetextFontColorChanging = false;
            IsFreetextFillColorChanging = false;
        }
        internal void HideDeskTopOverLays()
        {
            IsShapeColorPalleteVisible = false;
            IsInkColorPalleteVisible = false;
            IsTextMarkUpColorPalleteVisible = false;
            IsStampOpacitySliderbarVisible = false;
            IsStickyNoteColorPalleteVisible = false;
            IsLineAndArrowColorPalleteVisible = false;
            if (!isStickyRelatedIcon)
                IsEditLayoutVisible = false;
            isStickyRelatedIcon = false;
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
                    IsColorToolbarVisible = false;
#endif
        }

        internal void ClearButtonHighlights()
        {
            InkHighlightColor = Colors.Transparent;
            ShapeHighlightColor = Colors.Transparent;
            TextMarkupHighlightColor = Colors.Transparent;
            StickyNoteHighlightColor = Colors.Transparent;
            InkEraserHighlightColor = Colors.Transparent;
            FreeTextHighlightColor = Colors.Transparent;
            StampHighlightColor = Colors.Transparent;
            ColorPaletteHighlightColor = Colors.Transparent;
            MoreOptionsHighlightColor = Colors.Transparent;
            ZoomHighlightColor = Colors.Transparent;
            PageLayoutHighlightColor = Colors.Transparent;
            ZoomMobileHighlightColor = Colors.Transparent;
            SignatureHighlightColor = Colors.Transparent; 
            FreeTextFontSizeHighlightColor = Colors.Transparent;
            ThicknessButtonHighlightColor= Colors.Transparent;  
        }

        internal void LockAllAnnotationsAndFields(bool setLock)
        {
            pdfViewer.AnnotationSettings.IsLocked = setLock;
            if (pdfViewer.FormFields?.Count > 0)
            {
                foreach (var field in pdfViewer.FormFields)
                {
                    field.ReadOnly = setLock;
                }
            }
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
            if (pdfViewer.AnnotationMode == AnnotationMode.Polyline || (SelectedAnnotation is PolylineAnnotation))
                SelectedAnnotationIcon = "\uE786";
            if (pdfViewer.AnnotationMode == AnnotationMode.Polygon || (SelectedAnnotation is PolygonAnnotation))
            {
                if ((SelectedAnnotation is PolygonAnnotation polygon && polygon.BorderStyle == BorderStyle.Cloudy) || pdfViewer.AnnotationSettings.Polygon.BorderStyle == BorderStyle.Cloudy)
                {
                    SelectedAnnotationIcon = "\uE783";
                }
                else
                    SelectedAnnotationIcon = "\uE789";
            }
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
                            SelectedFillColorOpacity = opacity;
                        }
                        if (selectedAnnotation is FreeTextAnnotation freeText)
                        {
                            Color existingFillColor = freeText.FillColor;
                            Color modifiedFillColor = existingFillColor.WithAlpha(opacity);
                            freeText.FillColor = modifiedFillColor;
                            SelectedFillColorOpacity = opacity;
                        }
                    }
                    else if ((IsFreetextFillColorChanging || IsFreetextStrokeColorChanging) && selectedAnnotation is FreeTextAnnotation freeText)
                    {
                        if (IsFreetextFillColorChanging)
                        {
                            Color existingFillColor = freeText.FillColor;
                            Color modifiedFillColor = existingFillColor.WithAlpha(opacity);
                            freeText.FillColor = modifiedFillColor;
                            SelectedFillColorOpacity = opacity;
                        }
                        else if (IsFreetextStrokeColorChanging)
                        {
                            Color existingBorderColor = freeText.BorderColor;
                            Color modifiedBorderColor = existingBorderColor.WithAlpha(opacity);
                            freeText.BorderColor = modifiedBorderColor;
                            SelectedOpacity = opacity;
                        }

                    }
                    else if (IsColorToolbarVisible || IsStampOpacityToolbarVisible || isDeskTopColorToolbarVisible || isStampOpacitySliderbarVisible || IsFreetextColorPalatteisible || IsFreeTextFontColorPalatteVisible)
                    {
                        if (selectedAnnotation is ShapeAnnotation shape)
                        {
                            shape.Color = shape.Color.WithAlpha(opacity);
                        }
                        else if (selectedAnnotation is FreeTextAnnotation freetext)
                        {
                            if (IsFreeTextFontColorPalatteVisible)
                            {
                                Color existingFontColor = freetext.Color;
                                Color modifiedFontColor = existingFontColor.WithAlpha(opacity);
                                freetext.Color = modifiedFontColor;
                            }
                            else
                            {
                                Color existingBorderColor = freetext.BorderColor;
                                Color modifiedBorderColor = existingBorderColor.WithAlpha(opacity);
                                freetext.BorderColor = modifiedBorderColor;
                            }
                            SelectedOpacity = opacity;
                        }
                        else
                            selectedAnnotation.Opacity = opacity;
                        SelectedOpacity = opacity;
                    }
                }
                else
                {
                    if (isColorToolbarVisible || isDeskTopColorToolbarVisible || isInkColorPalleteVisible || isLineAndArrowColorPalleteVisible || IsFreetextStrokeColorChanging)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                pdfViewer.AnnotationSettings.Square.Color = pdfViewer.AnnotationSettings.Square.Color.WithAlpha(opacity);
                                break;

                            case AnnotationMode.Circle:

                                pdfViewer.AnnotationSettings.Circle.Color = pdfViewer.AnnotationSettings.Circle.Color.WithAlpha(opacity);
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.Color = pdfViewer.AnnotationSettings.Line.Color.WithAlpha(opacity);
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.Color = pdfViewer.AnnotationSettings.Arrow.Color.WithAlpha(opacity);
                                break;

                            case AnnotationMode.Polygon:
                                pdfViewer.AnnotationSettings.Polygon.Color = pdfViewer.AnnotationSettings.Polygon.Color.WithAlpha(opacity);
                                break;

                            case AnnotationMode.Polyline:
                                pdfViewer.AnnotationSettings.Polyline.Color = pdfViewer.AnnotationSettings.Polyline.Color.WithAlpha(opacity);
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
                            case AnnotationMode.FreeText:
                                if (IsFreeTextFontColorPalatteVisible)
                                    pdfViewer.AnnotationSettings.FreeText.Color = pdfViewer.AnnotationSettings.FreeText.Color.WithAlpha(opacity);
                                else
                                    pdfViewer.AnnotationSettings.FreeText.BorderColor = pdfViewer.AnnotationSettings.FreeText.BorderColor.WithAlpha(opacity);
                                break;
                        }
                        SelectedOpacity = opacity;
                    }
                    else if (isFillColorToolbarVisible || isDeskTopFillColorToolbarVisible || IsFreetextFillColorChanging)
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
                            case AnnotationMode.Polygon:
                                Color existingPolygonFillColor = pdfViewer.AnnotationSettings.Polygon.FillColor;
                                Color modifiedPolygonFillColor = existingPolygonFillColor.WithAlpha(opacity);
                                pdfViewer.AnnotationSettings.Polygon.FillColor = modifiedPolygonFillColor;
                                break;
                            case AnnotationMode.FreeText:
                                Color existingFreetextFillColor = pdfViewer.AnnotationSettings.FreeText.FillColor;
                                Color modifiedFreetextFillColor = existingFreetextFillColor.WithAlpha(opacity);
                                pdfViewer.AnnotationSettings.FreeText.FillColor = modifiedFreetextFillColor;
                                break;
                        }
                        SelectedFillColorOpacity = opacity;
                    }
                }
            }
        }
        void OnFontSizeCommand(object parameter)
        {
            if (parameter is string size)
            {
                float.TryParse(size, out float fontSize);
                if (SelectedAnnotation != null)
                {
                    if (SelectedAnnotation is FreeTextAnnotation freeText)
                        freeText.FontSize = fontSize;
                }
                else
                {
                    pdfViewer.AnnotationSettings.FreeText.FontSize = fontSize;
                }
            }
            else if (parameter is int fontSize)
            {
                if (SelectedAnnotation != null)
                {
                    if (SelectedAnnotation is FreeTextAnnotation freeText)
                        freeText.FontSize = fontSize;
                }
                else
                {
                    pdfViewer.AnnotationSettings.FreeText.FontSize = fontSize;
                }
            }
            else if (parameter is float fSize)
            {
                if (SelectedAnnotation != null)
                {
                    if (SelectedAnnotation is FreeTextAnnotation freeText)
                        freeText.FontSize = fSize;
                }
                else
                {
                    pdfViewer.AnnotationSettings.FreeText.FontSize = fSize;
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
                    else if (selectedAnnotation is FreeTextAnnotation freeTextAnnotation)
                    {
                        freeTextAnnotation.BorderWidth = thickness;
                    }
                    SelectedThickness = thickness;
                }
                else
                {
                    if (IsThicknessSliderbarVisible || IsThicknessToolbarVisible ||IsDeskTopColorToolbarVisible || IsInkColorPalleteVisible || IsLineAndArrowColorPalleteVisible || IsFreeTextSliderVisible || IsFreeTextFillColorVisble)
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

                            case AnnotationMode.Polyline:
                                pdfViewer.AnnotationSettings.Polyline.BorderWidth = thickness;
                                break;

                            case AnnotationMode.Polygon:
                                pdfViewer.AnnotationSettings.Polygon.BorderWidth = thickness;
                                break;

                            case AnnotationMode.Ink:
                                pdfViewer.AnnotationSettings.Ink.BorderWidth = thickness;
                                break;
                            case AnnotationMode.InkEraser:
                                pdfViewer.AnnotationSettings.InkEraser.Thickness = thickness;
                                break;
                            case AnnotationMode.FreeText:
                                pdfViewer.AnnotationSettings.FreeText.BorderWidth = thickness;
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
                        {
                            if (color == Colors.Transparent)
                            {
                                shape.FillColor = color;
                                SelectedFillColorOpacity = color.Alpha;
                            }
                            else
                                shape.FillColor = color.WithAlpha(SelectedFillColorOpacity);
                        }
                        else if (selectedAnnotation is FreeTextAnnotation freetext)
                        {
                            freetext.FillColor = color.WithAlpha(SelectedFillColorOpacity);
                        }
                    }
                    else if (IsFreetextStrokeColorChanging || IsFreetextFontColorChanging || IsFreetextFillColorChanging)
                    {
                        if (selectedAnnotation is FreeTextAnnotation freetext)
                        {
                            if (IsFreetextStrokeColorChanging)
                                freetext.BorderColor = color.WithAlpha(SelectedOpacity);
                            else if (IsFreetextFontColorChanging)
                                freetext.Color = color;
                            else if (IsFreetextFillColorChanging)
                                freetext.FillColor = IsColorTransparent(color) ? color.WithAlpha(color.Alpha) : color.WithAlpha(SelectedFillColorOpacity);
                        }

                    }
                    else if (isColorToolbarVisible || isDeskTopColorToolbarVisible || isInkColorPalleteVisible || isLineAndArrowColorPalleteVisible || IsFreetextColorPalatteisible)
                    {
                        selectedAnnotation.Color = color.WithAlpha(SelectedOpacity);
                        SelectedColor = color.WithAlpha(SelectedOpacity);
                    }
                }
                else
                {
                    if (isFillColorToolbarVisible || isDeskTopFillColorToolbarVisible || IsFreetextFillColorChanging || IsFreetextFontColorChanging)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:
                                pdfViewer.AnnotationSettings.Square.FillColor = IsColorTransparent(color)
                                    ? color.WithAlpha(color.Alpha)
                                    : color.WithAlpha(SelectedFillColorOpacity);
                                break;

                            case AnnotationMode.Circle:
                                pdfViewer.AnnotationSettings.Circle.FillColor = IsColorTransparent(color)
                                    ? color.WithAlpha(color.Alpha)
                                    : color.WithAlpha(SelectedFillColorOpacity);
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.FillColor = IsColorTransparent(color)
                                    ? color.WithAlpha(color.Alpha)
                                    : color.WithAlpha(SelectedFillColorOpacity);
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.FillColor = IsColorTransparent(color)
                                    ? color.WithAlpha(color.Alpha)
                                    : color.WithAlpha(SelectedFillColorOpacity);
                                break;

                            case AnnotationMode.Polygon:
                                pdfViewer.AnnotationSettings.Polygon.FillColor = IsColorTransparent(color)
                                    ? color.WithAlpha(color.Alpha)
                                    : color.WithAlpha(SelectedFillColorOpacity);
                                break;
                            case AnnotationMode.FreeText:
                                if (IsFreetextFillColorChanging)
                                    pdfViewer.AnnotationSettings.FreeText.FillColor = IsColorTransparent(color)
                                    ? color.WithAlpha(color.Alpha)
                                    : color.WithAlpha(SelectedFillColorOpacity);
                                else if (IsFreetextFontColorChanging)
                                    pdfViewer.AnnotationSettings.FreeText.Color = color;
                                break;
                        }
                    }
                    else if (IsFreetextStrokeColorChanging)
                    {
                        pdfViewer.AnnotationSettings.FreeText.BorderColor = color.WithAlpha(SelectedOpacity);
                    }
                    else if (isColorToolbarVisible || isDeskTopColorToolbarVisible || isInkColorPalleteVisible || IsLineAndArrowColorPalleteVisible || IsFreetextColorPalatteisible)
                    {
                        switch (pdfViewer.AnnotationMode)
                        {
                            case AnnotationMode.Square:

                                pdfViewer.AnnotationSettings.Square.Color = color.WithAlpha(SelectedOpacity);
                                break;

                            case AnnotationMode.Circle:
                                pdfViewer.AnnotationSettings.Circle.Color = color.WithAlpha(SelectedOpacity);
                                break;

                            case AnnotationMode.Line:
                                pdfViewer.AnnotationSettings.Line.Color = color.WithAlpha(SelectedOpacity);
                                break;

                            case AnnotationMode.Arrow:
                                pdfViewer.AnnotationSettings.Arrow.Color = color.WithAlpha(SelectedOpacity);
                                break;

                            case AnnotationMode.Polyline:
                                pdfViewer.AnnotationSettings.Polyline.Color = color.WithAlpha(SelectedOpacity);
                                break;

                            case AnnotationMode.Polygon:
                                pdfViewer.AnnotationSettings.Polygon.Color = color.WithAlpha(SelectedOpacity);
                                break;

                            case AnnotationMode.Highlight:
                                pdfViewer.AnnotationSettings.Highlight.Color = color.WithAlpha(SelectedOpacity);
                                SelectedColor = color;
                                break;

                            case AnnotationMode.Underline:
                                pdfViewer.AnnotationSettings.Underline.Color = color.WithAlpha(SelectedOpacity);
                                SelectedColor = color;
                                break;

                            case AnnotationMode.StrikeOut:
                                pdfViewer.AnnotationSettings.StrikeOut.Color = color.WithAlpha(SelectedOpacity);
                                SelectedColor = color;
                                break;

                            case AnnotationMode.Squiggly:
                                pdfViewer.AnnotationSettings.Squiggly.Color = color.WithAlpha(SelectedOpacity);
                                SelectedColor = color;
                                break;

                            case AnnotationMode.Ink:
                                pdfViewer.AnnotationSettings.Ink.Color = color.WithAlpha(SelectedOpacity);
                                SelectedColor = color;
                                break;

                            case AnnotationMode.FreeText:
                                pdfViewer.AnnotationSettings.FreeText.Color = color;
                                SelectedColor = color;
                                break;
                            case AnnotationMode.StickyNote:
                                pdfViewer.AnnotationSettings.StickyNote.Color = color;
                                SelectedColor = color;
                                break;
                        }
                    }
                }
            }
        }

        private bool IsColorTransparent(Color color)
        {
            if (color.Alpha == 0)
                SelectedFillColorOpacity = 0;
            return color.Alpha == 0;
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
            IsFilePickerVisible = false;
            ShowMoreOptions = false;
            ShowZoomLevel = false;  
            Stream outStream = new MemoryStream();
            await pdfViewer.SaveDocumentAsync(outStream);
            try
            {
                if (FileData != null && FileData.FileName != null)
                {
                    string? filePath = await FileService.SaveAsAsync(FileData.FileName, outStream);
                    await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
                }
            }
            catch (Exception exception)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
            }
        }
        internal async void ImportAnnotations()
        {
            IsFileOperationListVisible = false;
            ShowMoreOptions = false;
            ShowZoomLevel = false;
            var fileData = await FileService.OpenFile("xfdf");
            if (fileData != null)
            {
                // Handle errors in importing. Please note that the form field names and values in the XFDF file should correspond to the form field names and values in the PDF document.
                // Therefore, not any XFDF file can be imported to any PDF file.
                try
                {
                    if (fileData.Stream != null)
                        await pdfViewer.ImportAnnotationsAsync(fileData.Stream, Syncfusion.Pdf.Parsing.AnnotationDataFormat.XFdf);
                }
                catch (Exception exception)
                {
                    await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not imported. {exception.Message}", "OK");
                }
            }
        }

        internal async void ExportAnnotations()
        {
            IsFileOperationListVisible = false;
            ShowMoreOptions = false;
            ShowZoomLevel = false;
            Stream outStream = new MemoryStream();
            await pdfViewer.ExportAnnotationsAsync(outStream, Syncfusion.Pdf.Parsing.AnnotationDataFormat.XFdf);
            try
            {
                if (FileData != null && FileData.FileName != null)
                {
                    string exportedPdfFileName = FileData.FileName.Replace(".pdf", ".xfdf");
                    string? filePath = await FileService.SaveAsAsync(exportedPdfFileName, outStream);
                    await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
                }
            }
            catch (Exception exception)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
            }
        }
        internal bool AnnotationModeIsInkEraser()
        {
            return pdfViewer.AnnotationMode == AnnotationMode.InkEraser ? true : false;
        }
        internal void PrintDocument()
        {
            IsFileOperationListVisible = false;
            ShowMoreOptions = false;
            ShowZoomLevel = false;
            pdfViewer.PrintDocument();
        }
    }
}