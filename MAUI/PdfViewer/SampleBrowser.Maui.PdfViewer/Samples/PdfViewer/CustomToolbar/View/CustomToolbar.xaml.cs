#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using System.IO;
using System.Reflection;
using Syncfusion.Maui.Core.Converters;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomToolbar : SampleView
{
    private string? previousDocument = string.Empty;
    //It is used to delay the current thread's execution, until the user enters the password.
    ManualResetEvent manualResetEvent = new ManualResetEvent(false);
    ToolbarView? toolbar;
    /// <summary>
    /// Used to check whether undo executed or not when turning annotation mode as polyline or Polygon to None
    /// </summary>
    bool undoAlreadyExecuted = false;
#if ANDROID || IOS
    private ViewCell? lastCell;
#endif
    public Stream CustomStampImageStream { get; set; } = new MemoryStream();
    public double CustomStampWidth { get; set; }
    public double CustomStampHeight { get; set; }
    public bool CustomStamp { get; set; }
    public SearchView SearchView { get; set; }
    public StampView? StampView { get; set; }
    SignatureViewOverlay? signatureViewOverlay;

    CustomToolbarViewModel viewModel;
    public CustomToolbar()
    {
        InitializeComponent();
        viewModel = new CustomToolbarViewModel(PdfViewer, "customToolbar");
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        BindingContext = viewModel;
        AddDocumentItems();
        UpdateVisibility();
#if ANDROID || IOS
        SearchView = MobileSearchView;
        toolbar = MobileToolbar;
        StampView = MobileStampView;
#else
        fileListView.IsVisible = false;
        SearchView = DesktopSearchView;
        StampView = DesktopStampView;
        toolbar = DesktopToolbar;
#endif
        PdfViewer.UndoCommand!.CanExecuteChanged += UndoCommand_CanExecuteChanged;
        PdfViewer.PropertyChanged += PdfViewer_PropertyModified;
        PdfViewer.AnnotationAdded += PdfViewer_AnnotationAdded;
        UpdateToolbarProperties();
        DesktopToolbar.SetBindings();
        SearchView.SearchHelper = PdfViewer;
        SearchView.NoMatchesFound += NoMatchesFound;
        StampView!.StampHelper = PdfViewer;
        StampView.StampSelected += StampView_StampSelected;
        viewModel.StickyNoteSelected += ViewModel_StickyNoteSelected;
#if WINDOWS
        //Note: Due to known MAUI issue (#13714) in Entry control when the property "IsVisible=false" by default, the mentioned property is set after the control is loaded.
        if (SearchView.SearchInputEntry != null)
            SearchView.SearchInputEntry.Loaded += SearchInputEntryLoaded;
#endif
        PdfViewer.SignatureModalViewAppearing += PdfViewer_SignatureModalViewAppearing;
#if !WINDOWS && !MACCATALYST
        PdfViewer.CustomStampModalViewAppearing += PdfViewer_AnnotationModalViewAppearing;
        PdfViewer.FreeTextModalViewAppearing += PdfViewer_AnnotationModalViewAppearing;
        PdfViewer.StickyNoteModalViewAppearing += PdfViewer_AnnotationModalViewAppearing;

        PdfViewer.CustomStampModalViewDisappearing += PdfViewer_ModalViewDisappearing;
        PdfViewer.FreeTextModalViewDisappearing += PdfViewer_ModalViewDisappearing;
        PdfViewer.SignatureModalViewDisappearing += PdfViewer_ModalViewDisappearing;
        PdfViewer.StickyNoteModalViewDisappearing += PdfViewer_ModalViewDisappearing;
#endif
    }
    private void UpdateVisibility()
    {

#if WINDOWS || MACCATALYST
        bottomToolbar.IsVisible = false;
#else
        bool isOutlineViewVisible = PdfViewer.IsOutlineViewVisible;
        var isVisible = new InvertedBoolConverter().Convert(isOutlineViewVisible, typeof(bool), null, null);
        if (isVisible != null)
            bottomToolbar.IsVisible = (bool)(isVisible);
#endif
    }
    private void PdfViewer_SignatureModalViewAppearing(object? sender, FormFieldModalViewAppearingEventArgs e)
    {
#if !WINDOWS && !MACCATALYST
        MobileToolbar.IsVisible = false;
        bottomToolbar.IsVisible = false;
#endif
        e.Cancel = true;
        if (e.FormField is SignatureFormField signatureField && signatureField.Signature == null)
            ShowSignatureDialog(signatureField);
    }

#if !WINDOWS && !MACCATALYST
    private void PdfViewer_AnnotationModalViewAppearing(object? sender, AnnotationModalViewAppearingEventArgs e)
    {
        MobileToolbar.IsVisible = false;
        bottomToolbar.IsVisible = false;
    }

    private void PdfViewer_ModalViewDisappearing(object? sender, EventArgs e)
    {
        MobileToolbar.IsVisible = true;
        bottomToolbar.IsVisible = true;
    }

#endif

        public void AddDocumentItems()
    {
        documentOne.Children.Add(CreateView("PDF Succinctly"));
        documentTwo.Children.Add(CreateView("Rotated PDF"));
        documentThree.Children.Add(CreateView("Password protected PDF"));
        documentFour.Children.Add(CreateView("Corrupted PDF"));
        documentFive.Children.Add(CreateView("Single page PDF"));
        documentSix.Children.Add(CreateView("Annotations PDF"));
#if !MACCATALYST
        customDocument.Children.Add(CreateView("Browse files on this device"));
#endif
    }

    private void PdfViewer_PropertyModified(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AnnotationMode))
        {
            if ((PdfViewer.AnnotationMode != AnnotationMode.Polygon && PdfViewer.AnnotationMode != AnnotationMode.Polyline) && undoAlreadyExecuted)
            {
                viewModel.IsSaveLayoutVisible = true;
            }
            else if (PdfViewer.AnnotationMode == AnnotationMode.Polygon || PdfViewer.AnnotationMode == AnnotationMode.Polyline)
            {
                viewModel.IsSaveLayoutVisible = false;
            }
        }
        if (PdfViewer.ZoomMode == ZoomMode.Default && PdfViewer.ZoomFactor!=1)
        {
            viewModel.ZoomMobileVisible = true;
        }
        else if (PdfViewer.ZoomMode!=ZoomMode.Default)
        {
            viewModel.ZoomMobileVisible= false;
        }
    }

    void InitializeSignatureDialog()
    {
        if (signatureViewOverlay == null)
        {
            signatureViewOverlay = new SignatureViewOverlay();
            signatureViewOverlay.CloseRequested += SignatureViewOverlay_CloseRequested;
            signatureViewOverlay.SignatureCreated += SignatureViewOverlay_SignatureCreated;
        }
    }

    private void SignatureViewOverlay_CloseRequested(object? sender, EventArgs e)
    {
        viewModel.IsSignatureViewVisible = false;
    }

    internal void ShowSignatureDialog(SignatureFormField? signatureField = null)
    {
        InitializeSignatureDialog();
        if (signatureViewOverlay != null)
        {
            if (signatureViewOverlay.Parent == null)
                mainGrid.Children.Add(signatureViewOverlay);
            if (signatureField == null)
            {
                signatureViewOverlay.ShowSignatureListDialog();
                signatureViewOverlay.SetMarginBinding(BindingContext);
            }
            else
            {
                signatureViewOverlay.SetSignatureFieldToSign(signatureField);
                signatureViewOverlay.ShowSignaturePad();
            }
        }
    }

    private void SignatureViewOverlay_SignatureCreated(object? sender, SignatureCreationCompletedEventArgs e)
    {
        if (e.CanShowToast)
            ShowToast("Tap to add signature");
    }

    private Grid CreateView(string iconName)
    {
        GestureGrid childRow = new GestureGrid();
        childRow.WidthRequest = 220;
        childRow.HeightRequest = 40;
        childRow.PointerPressed += ListView_ItemTapped;
        Color lightThemeColor = new Color();
        Color darkThemeColor = new Color();
        if (Application.Current != null)
        {
            lightThemeColor = (Color)Application.Current.Resources["IconColourLight"];
            darkThemeColor = (Color)Application.Current.Resources["IconColour"];
        }
        Label iconNameLabel = new Label()
        {
            Padding = new Thickness(16, 0, 0, 0),
            Margin = new Thickness(5, 0, 0, 0),
            TextColor = Color.FromArgb("#49454F"),
            FontSize = 15,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Text = iconName,
        };
        iconNameLabel.SetAppThemeColor(Label.TextColorProperty, lightThemeColor, darkThemeColor);
        childRow.Children.Add(iconNameLabel);
        return childRow;
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(viewModel.ColorPaletteMargin))
        {
            View? view = null;
            if (viewModel.IsInkColorPalleteVisible)
                view = inkColorPalette;
            else if (viewModel.IsStampOpacitySliderbarVisible)
                view = stampOpacityBar;
            else if (viewModel.IsTextMarkUpColorPalleteVisible)
                view = textMarkupColorPalette;
            else if (viewModel.IsShapeColorPalleteVisible)
                view = shapeColorPalette;
            else if (viewModel.IsLineAndArrowColorPalleteVisible)
                view = lineAndArrowColorPalette;
            else if (viewModel.IsEraserThicknessToolbarVisible)
                view = eraserThicknessBar;
            else if (viewModel.IsFreeTextFillColorVisble)
                view = freeTextColorPalette;
            if (view != null)
            {
                Size sizeRequest = view.Measure(double.PositiveInfinity, double.PositiveInfinity);
                if (viewModel.ColorPaletteMargin.Left + sizeRequest.Width <= Width)
                    viewModel.ColorPaletteMargin = new Thickness(viewModel.ColorPaletteMargin.Left, viewModel.ColorPaletteMargin.Top, viewModel.ColorPaletteMargin.Right, viewModel.ColorPaletteMargin.Bottom);
            }
        }
        else if (e.PropertyName == nameof(viewModel.IsSignatureViewVisible))
        {
            if (viewModel.IsSignatureViewVisible)
                ShowSignatureDialog();
            else
                signatureViewOverlay?.Hide();
        }
        InitializeControlIfNull(e.PropertyName);
    }

    private void InitializeControlIfNull(string? propertyName)
    {
        if (propertyName == nameof(viewModel.IsFreeTextFillColorVisble))
        {
            if (viewModel.IsFreeTextFillColorVisble && freeTextColorPalette.Content == null)
                freeTextColorPalette.Initialize();
        }
        else if(propertyName==nameof(viewModel.IsFreeTextFontSizeListViewVisible))
        {
            if(viewModel.IsFreeTextFontSizeListViewVisible && freeTextFontSizeList.Content == null)
                freeTextFontSizeList.Initialize();
        }
        else if(propertyName==nameof(viewModel.IsThicknessSliderbarVisible))
        {
            if(viewModel.IsThicknessSliderbarVisible && thicknessBar.Content==null)
                thicknessBar.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsEraserThicknessToolbarVisible))
        {
            if (viewModel.IsEraserThicknessToolbarVisible && eraserThicknessBar.Content == null)
                eraserThicknessBar.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsLineAndArrowColorPalleteVisible))
        {
            if (viewModel.IsLineAndArrowColorPalleteVisible && lineAndArrowColorPalette.Content == null)
                lineAndArrowColorPalette.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsShapeColorPalleteVisible))
        {
            if (viewModel.IsShapeColorPalleteVisible && shapeColorPalette.Content == null)
                shapeColorPalette.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsTextMarkUpColorPalleteVisible))
        {
            if (viewModel.IsTextMarkUpColorPalleteVisible && textMarkupColorPalette.Content == null)
                textMarkupColorPalette.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsStampOpacitySliderbarVisible))
        {
            if (viewModel.IsStampOpacitySliderbarVisible && stampOpacityBar.Content == null)
                stampOpacityBar.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsInkColorPalleteVisible))
        {
            if (viewModel.IsInkColorPalleteVisible && inkColorPalette.Content == null)
                inkColorPalette.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsStickyNoteColorPalleteVisible))
        {
            if (viewModel.IsStickyNoteColorPalleteVisible && stickyNoteColorPalette.Content == null)
                stickyNoteColorPalette.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsTextMarkupListVisible))
        {
            if (viewModel.IsTextMarkupListVisible && textmarkupView.Content == null)
                textmarkupView.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsShapeListVisible))
        {
            if (viewModel.IsShapeListVisible && shapeListView.Content == null)
                shapeListView.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsStickyNoteListVisible))
        {
            if (viewModel.IsStickyNoteListVisible && DesktopStickyNoteListView.Content == null)
                DesktopStickyNoteListView.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsStampListVisible))
        {
            if (viewModel.IsStampListVisible && DesktopStampView.Content == null)
                DesktopStampView.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsStampViewVisible))
        {
            if (viewModel.IsStampViewVisible && MobileStampView.Content == null)
                MobileStampView.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsColorToolbarVisible))
        {
            if (viewModel.IsColorToolbarVisible && colorToolBar.Content == null)
                colorToolBar.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsFillColorToolbarVisible))
        {
            if (viewModel.IsFillColorToolbarVisible && fillColorToolBar.Content == null)
                fillColorToolBar.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsStickyNoteToolbarVisible))
        {
            if (viewModel.IsStickyNoteToolbarVisible && stickyNoteToolbar.Content == null)
                stickyNoteToolbar.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsFileOperationListVisible))
        {
            if (viewModel.IsFileOperationListVisible && menuView.Content == null)
                menuView.Initialize();
        }
        else if (propertyName == nameof(viewModel.ShowDesktopMoreOptions))
        {
            if (viewModel.ShowDesktopMoreOptions && desktopMoreView.Content == null)
            {
                desktopMoreView.Initialize();
            }
                

        }
        else if (propertyName == nameof(viewModel.ShowZoomLevel))
        {
            if(viewModel.ShowZoomLevel && desktopZoomView.Content == null)
            {
                desktopZoomView.Initialize();
            }
        }
    }

    private void ViewModel_StickyNoteSelected(object? sender, EventArgs e)
    {
        PdfViewer.AnnotationMode = AnnotationMode.None;
        if (BindingContext is CustomToolbarViewModel viewModel && viewModel.IsStickyNoteMode)
        {
            ShowToast("Tap on the page to insert the sticky note");
        }
    }

    private void StampView_StampSelected(object? sender, EventArgs e)
    {
        PdfViewer.AnnotationMode = AnnotationMode.None;
        ShowToast("Tap on the page to insert the selected stamp");
    }

    async void ShowToast(string text)
    {
        toast.Opacity = 1;
        toastText.Text = text;
        await toast.FadeTo(0, 2000, Easing.SinIn);
    }

    private void UndoCommand_CanExecuteChanged(object? sender, EventArgs e)
    {
        if (PdfViewer.AnnotationMode != AnnotationMode.Polyline && PdfViewer.AnnotationMode != AnnotationMode.Polygon)
            viewModel.IsSaveLayoutVisible = true;
        undoAlreadyExecuted = true;
    }

    void UpdateToolbarProperties()
    {
        if (toolbar != null)
        {
            toolbar.ParentView = this;
            toolbar.PdfViewer = PdfViewer;
        }
    }

    private void SearchInputEntryLoaded(object? sender, EventArgs e)
    {
        if (SearchView != null)
            SearchView.IsVisible = false;
    }

    private void NoMatchesFound(object? sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Search Result", "No matches were found"));
    }

    internal void HideOverlayToolbars()
    {
        viewModel.HideOverlayToolbars();
    }

    /// <summary>
    /// Handles when the message box close button is clicked.
    /// </summary>
    private void MessageBox_CloseClicked(object? sender, CloseClickedEventArgs? e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            if (e?.Title == "Incorrect Password")
                bindingContext.UpdateFileName("Password protected PDF");
            else if (e?.Title == "Error")
            {
                bindingContext.IsFileListViewVisible = true;
                bindingContext.ShowMessageBoxDialog = false;
            }
        }
    }

    internal void CloseAllDialogs()
    {
        viewModel.CloseAllDialogs();
    }

    ///// <summary>
    ///// Handles when leaving the current page
    ///// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        PdfViewer?.UnloadDocument();
        PdfViewer?.Handler?.DisconnectHandler();
    }

    /// <summary>
    /// Handles when a Pdf is tapped.
    /// </summary>
    private async void PdfViewer_Tapped(object sender, GestureEventArgs e)
    {
#if ANDROID
        if (Platform.CurrentActivity?.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        if (StampView != null && e != null)
        {
            PointF point = new PointF(e.PagePosition.X, e.PagePosition.Y);
            if (StampView.StampMode)
            {
                if (!CustomStamp)
                {
                    StampType type = StampView.stampType;
                    StampAnnotation builtStamp = new StampAnnotation(type, e.PageNumber, point);
                    RectF bounds = new RectF(builtStamp.Bounds.X - builtStamp.Bounds.Width / 2, builtStamp.Bounds.Y - builtStamp.Bounds.Height / 2, builtStamp.Bounds.Width, builtStamp.Bounds.Height);
                    builtStamp.Bounds = bounds;
                    PdfViewer.AddAnnotation(builtStamp);
                }
                else if (CustomStamp && CustomStampImageStream != null)
                {
                    StampAnnotation customStamp = new StampAnnotation(CustomStampImageStream, e.PageNumber, new RectF((float)(point.X - CustomStampWidth / 2), (float)(point.Y - CustomStampHeight / 2), (float)CustomStampWidth, (float)CustomStampHeight));
                    PdfViewer.AddAnnotation(customStamp);
                    CustomStamp = false;
                }
                StampView.StampMode = false;
            }
        }
        // Adding sticky note annotation on the tapped position of the page
        if (e != null)
        {
            if (this.BindingContext is CustomToolbarViewModel bindingContextForSticky)
            {
                if (bindingContextForSticky.IsStickyNoteMode)
                {
                    PointF point = new PointF(e.PagePosition.X, e.PagePosition.Y);
                    StickyNoteAnnotation builtStickyNote = new StickyNoteAnnotation(bindingContextForSticky.StickyIcon, string.Empty, e.PageNumber, point);
                    PdfViewer.AddAnnotation(builtStickyNote);
                    bindingContextForSticky.IsStickyNoteMode = false;
                }
            }
        }
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.HideOverlayToolbars();
            bindingContext.IsFilePickerVisible = false;
            bindingContext.ShowMoreOptions = false;
            bindingContext.IsStampViewVisible = false;
            bindingContext.StampHighlightColor = Colors.Transparent;
            bindingContext.ShapeHighlightColor = Colors.Transparent;
            bindingContext.TextMarkupHighlightColor = Colors.Transparent;
            bindingContext.StickyNoteHighlightColor = Colors.Transparent;
            bindingContext.ShowPageLayoutOptions = false;
            bindingContext.ShowZoomMobileView = false;
            bindingContext.MoreOptionsHighlightColor = Colors.Transparent;
            bindingContext.PageLayoutHighlightColor = Colors.Transparent;
            bindingContext.ZoomMobileHighlightColor=Colors.Transparent;
            bindingContext.IsFreeTextFontSizeListViewVisible = false;
            bindingContext.IsThicknessSliderbarVisible = false;
            bindingContext.FreeTextFontSizeHighlightColor = Colors.Transparent;
#if WINDOWS || MACCATALYST
            bindingContext.IsShapeListVisible = false;
            bindingContext.IsStickyNoteListVisible = false;
            bindingContext.ShowZoomLevel=false;
            bindingContext.IsTextMarkupListVisible = false;
            bindingContext.IsStampListVisible = false;
            bindingContext.IsInkColorPalleteVisible = false;
            bindingContext.IsShapeColorPalleteVisible = false;
            bindingContext.IsLineAndArrowColorPalleteVisible = false;
            bindingContext.IsTextMarkUpColorPalleteVisible = false;
            bindingContext.IsStampOpacitySliderbarVisible = false;
            bindingContext.IsStickyNoteColorPalleteVisible = false;
            bindingContext.IsFileOperationListVisible = false;
            bindingContext.IsFreetextColorPalatteisible = false;
            bindingContext.IsFreeTextFillColorVisble = false;
            bindingContext.IsFreeTextSliderVisible = false;
            bindingContext.IsFreeTextFontListVisible = false;
            bindingContext.ColorPaletteHighlightColor = Colors.Transparent;
            bindingContext.IsEraserThicknessToolbarVisible = false;
            bindingContext.StickyNoteIconChangeHighlightColor = Colors.Transparent;
            bindingContext.FileOperationHighlightColor = Colors.Transparent;
#endif
        }
        if (e != null && SignatureHelper.CurrentSignatureItem != null)
        {
            int pageNumber = e.PageNumber;
            if (SignatureHelper.CurrentSignatureItem is DrawSignatureItem drawSignature)
            {
                if (drawSignature.ImageSource is StreamImageSource streamImageSource)
                {
                    var stream = await streamImageSource.Stream(CancellationToken.None);
                    stream.Position = 0;
                    float imageWidth, imageHeight;
#if NET8_0_OR_GREATER
                    Microsoft.Maui.Graphics.IImage platformImage = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
                    imageWidth = platformImage.Width;
                    imageHeight = platformImage.Height;
#endif
                    if (imageWidth < imageHeight)
                    {
                        if (imageWidth > 200)
                        {
                            imageHeight = 200 * (imageHeight / imageWidth);
                            imageWidth = 200;
                        }
                    }
                    else
                    {
                        if (imageHeight > 200)
                        {
                            imageWidth = 200 * (imageWidth / imageHeight);
                            imageHeight = 200;
                        }
                    }
                    stream.Position = 0;
                    StampAnnotation stamp = new StampAnnotation(stream, pageNumber, new Rect(e.PagePosition.X - imageWidth/2, e.PagePosition.Y - imageHeight/2, imageWidth, imageHeight));
                    stamp.IsSignature = true;
                    PdfViewer.AddAnnotation(stamp);                }
            }
            else if (SignatureHelper.CurrentSignatureItem is TypeSignatureItem typeSignature)
            {
                
            }
            else if (SignatureHelper.CurrentSignatureItem is ImageSignatureItem imageSignature)
            {
                if (imageSignature.ImageStream != null)
                {
                    StampAnnotation stamp = new StampAnnotation(imageSignature.ImageStream, pageNumber, new Rect(e.PagePosition.X - imageSignature.ImageSize.Width/2, e.PagePosition.Y - imageSignature.ImageSize.Height/2, imageSignature.ImageSize.Width, imageSignature.ImageSize.Height));
                    stamp.IsSignature = true;
                    PdfViewer.AddAnnotation(stamp);
                }
            }
            SignatureHelper.CurrentSignatureItem = null;
        }
    }

    /// <summary>
    /// Handles when a file picker content is tapped.
    /// </summary>
    private void ListView_ItemTapped(object? sender, EventArgs e)
    {
        GestureGrid? grid = sender as GestureGrid;
        string currentTappedDocument = "";
        if (grid!.Children[0] is Label iconName)
        {
            currentTappedDocument = iconName.Text;
        }
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            if (bindingContext.IsFilePickerVisible)
            {
                if (!passwordDialog.IsVisible || previousDocument != currentTappedDocument)
                    bindingContext.UpdateFileName(currentTappedDocument);
                bindingContext.IsFilePickerVisible = false;
                bindingContext.HideOverlayToolbars();
#if ANDROID || IOS
                bindingContext.BottomToolbarContent = new AnnotationToolbar(viewModel);
#endif
                previousDocument = currentTappedDocument;
            }
            bindingContext.IsFileOperationListVisible = false;
        }
    }

    /// <summary>
    /// Handles password requested event.
    /// </summary>
    private void PdfViewer_PasswordRequested(object sender, PasswordRequestedEventArgs e)
    {
        e.Handled = true;

        //Show the password dialog.
        passwordDialog.Dispatcher.Dispatch(() =>
        {
            passwordDialog.IsVisible = true;
#if WINDOWS
            passwordDialog.passwordEntry?.Focus();
#endif
        });

        //Block the current thread until user enters the password.
        manualResetEvent.WaitOne();
        manualResetEvent.Reset();

        if (!string.IsNullOrEmpty(passwordDialog.Password))
        {
            e.Password = passwordDialog.Password;
            passwordDialog.Password = null;
        }
    }

    /// <summary>
    /// Handles hyperlink tapped event.
    /// </summary>
    private void PdfViewer_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
    {
        SearchView?.Close();
        CloseAllDialogs();
        e.Handled = true;
        if (e.Uri != null)
        {
            MainThread.BeginInvokeOnMainThread(() => hyperlinkDialog.Show("Open Web Page", "Do you want to open\n" + e.Uri + "?", e.Uri));
        }
    }

    /// <summary>
    /// Handles when the password dialog box is closed.
    /// </summary>
    private void passwordDialog_PasswordDialogClosed(object sender, EventArgs e)
    {
        //Release the current waiting thread to execute.
        manualResetEvent.Set();
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            if (sender is PasswordDialogBox passwordDialog && passwordDialog.Password == null)
            {
                bindingContext.IsFileListViewVisible = true;
            }
        }
    }

    /// <summary>
    /// Handles when a document fails to load.
    /// </summary>
    private void PdfViewer_DocumentLoadFailed(object sender, DocumentLoadFailedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            toolbar!.SearchButton!.IsEnabled = false;
            SearchView?.Close();
            CloseAllDialogs();
        });
        e.Handled = true;
        if (e.Message == "Invalid cross reference table.")
        {
            if (BindingContext is CustomToolbarViewModel viewModel)
            {
                viewModel.ShowMessageBoxDialog = true;
            }
            MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Error", "Failed to load the PDF document."));
        }
        else if (e.Message == "Can't open an encrypted document. The password is invalid." && !passwordDialog.IsVisible)
            MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Incorrect Password", "The password you entered is incorrect. Please try again.", "OK"));
    }

    /// <summary>
    /// Handles when a file picker content is tapped.
    /// </summary>
    private void ViewCell_Tapped(object sender, EventArgs e)
    {
#if ANDROID || IOS
        if (lastCell != null)
            lastCell.View.BackgroundColor = Colors.Transparent;
        var viewCell = (ViewCell)sender;
        if (viewCell.View != null)
        {
            viewCell.View.BackgroundColor = Color.FromArgb("#0A000000");
            lastCell = viewCell;
        }
#endif
    }

    /// <summary>
    /// Handles when a document is loaded.
    /// </summary>
    private void PdfViewer_DocumentLoaded(object sender, EventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDocumentLoaded = true;
            bindingContext.IsEditLayoutVisible = false;
            bindingContext.IsTextMarkupListVisible = false;
            bindingContext.IsShapeListVisible = false;
            bindingContext.IsStickyNoteListVisible = false;
            bindingContext.IsStampListVisible = false;

        }
        MainThread.BeginInvokeOnMainThread(() =>
        {
            toolbar!.SearchButton!.IsEnabled = true;

        });
    }

    /// <summary>
    /// Handles when a document is unloaded.
    /// </summary>
    private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
    {
        viewModel.IsDocumentLoaded = false;
        viewModel.IsSaveLayoutVisible = false;
        toolbar!.SearchButton!.IsEnabled = false;
        SearchView?.Close();
        viewModel.SelectedAnnotation = null;
        viewModel.HideOverlayToolbars();
        viewModel.ShowMoreOptions = false;
        viewModel.IsStampViewVisible = false;
#if WINDOWS || MACCATALYST
        viewModel.IsShapeListVisible = false;
        viewModel.IsTextMarkupListVisible = false;
        viewModel.IsStampListVisible = false;
        viewModel.IsInkColorPalleteVisible = false;
        viewModel.IsShapeColorPalleteVisible = false;
        viewModel.IsLineAndArrowColorPalleteVisible = false;
        viewModel.IsTextMarkUpColorPalleteVisible = false;
        viewModel.IsStampOpacitySliderbarVisible = false;
#else
        viewModel.BottomToolbarContent = new AnnotationToolbar(viewModel);
#endif
    }

    private void PdfViewer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PdfViewer.IsOutlineViewVisible) && !PdfViewer.IsOutlineViewVisible)
        {
            toolbar!.PageNumberEntry?.HideKeyboard();
        }
        if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
        {
            if (e.PropertyName == nameof(PdfViewer.IsOutlineViewVisible))
                toolbar!.IsVisible = !PdfViewer.IsOutlineViewVisible;
            else if (e.PropertyName == nameof(PdfViewer.AnnotationMode))
            {
                toolbar!.IsVisible = PdfViewer.AnnotationMode != AnnotationMode.Signature;
                bottomToolbar!.IsVisible = PdfViewer.AnnotationMode != AnnotationMode.Signature;
            }
        }
        if (e.PropertyName == nameof(PdfViewer.AnnotationMode))
        {
            if (PdfViewer.AnnotationMode == AnnotationMode.None)
                viewModel.ClearButtonHighlights();
        }
    }

    private void PdfViewer_AnnotationSelected(object sender, AnnotationEventArgs e)
    {
        viewModel.SelectedAnnotation = e.Annotation;
        if (viewModel.SelectedAnnotation is FreeTextAnnotation)
        {
            viewModel.IsFreetextToolsVisible = false;
            viewModel.IsEditLayoutVisible = true;
#if ANDROID || IOS
            viewModel.BottomToolbarContent = new FreetextPropertyToolbar(viewModel);
#endif
        }
        SearchView!.Close();
    }

    private void PdfViewer_AnnotationDeselected(object sender, AnnotationEventArgs e)
    {
        if (viewModel.SelectedAnnotation is FreeTextAnnotation)
        {
#if ANDROID || IOS
            viewModel.BottomToolbarContent = new AnnotationToolbar(viewModel);
            viewModel.IsFontSliderVisible = false;
#endif
        }
        viewModel.SelectedAnnotation = null;
    }

    private void PdfViewer_AnnotationAdded(object? sender, AnnotationEventArgs e)
    {
        if (e.Annotation is FreeTextAnnotation)
        {
#if ANDROID || (IOS && !MACCATALYST)
            PdfViewer.AnnotationMode = AnnotationMode.None;
            viewModel.ClearButtonHighlights();
            viewModel.BottomToolbarContent = new AnnotationToolbar(viewModel);
            viewModel.IsFontSliderVisible = false;
#endif
        }
    }

    private void saveButton_Clicked(object? sender, EventArgs e)
    {
        viewModel?.SaveDocument();
    }

    private void printButton_Clicked(object sender, EventArgs e)
    {
        viewModel.PrintDocument();
    }

    private void thicknessSlider_ValueChanged(object sender, EventArgs e)
    {
        float thickness = (float)ThicknessSlider.Value;
        viewModel.ThicknessCommand.Execute(thickness);
    }

    private void stampOpacitySlider_ValueChanged(object sender, EventArgs e)
    {
        viewModel.OpacityCommand.Execute((float)StampOpacitySlider.Value);
    }

    private void importButton_Clicked(object sender, EventArgs e)
    {
        viewModel?.ImportAnnotations();
    }

    private void exportButton_Clicked(object sender, EventArgs e)
    {
        viewModel?.ExportAnnotations();
    }

    private void FileListView_FileSelected(object sender, FileSelectedEventArgs e)
    {
        viewModel.AssignPdfInfoAndLoad(e.FileName!);
    }

    private void OnCreateStampClicked(object sender, StampDialogEventArgs e)
    {
        if (stampDialog.Content == null)
            stampDialog.Initialize();
        if (e.IsVisible == true)
            stampDialog.IsVisible = false;
        else
            stampDialog.IsVisible = true;
        viewModel.IsStampListVisible = false;
    }

    private async void OnCustomStampCreated(object sender, CustomStampEventArgs e)
    {
        Label customStampLabel = e.CustomStampLabel ?? new Label(); // Initialize only if it's null
        Stream memoryStream = new MemoryStream();
        StampImage image = new StampImage();
#if NET7_0_OR_GREATER
        IScreenshotResult? imageStream = await e.StampView!.CaptureAsync();
        await imageStream!.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        image.ImageStream = memoryStream;
        Stream streamFromScreenshot = await imageStream.OpenReadAsync();
        image.Source = ImageSource.FromStream(() => streamFromScreenshot);
#else
        Stream imageStream = await e.StampView!.GetStreamAsync(Syncfusion.Maui.Core.ImageFileFormat.Png);
        await imageStream.CopyToAsync(memoryStream);
        memoryStream.Position = imageStream.Position = 0;
        image.ImageStream = memoryStream;
        image.Source = ImageSource.FromStream(() => imageStream);
#endif
        image.HeightRequest = 50;
        image.Aspect = Aspect.AspectFit;
#if WINDOWS || MACCATALYST
        image.HorizontalOptions = LayoutOptions.Start;
#endif
        image.Margin = new Thickness(5);
        StampView?.CustomStampListLayout?.Children.Add(CreateView(image, customStampLabel));
        if (sender is View view)
            view.IsVisible = false;
        ShowToast("The stamp is created and added to custom stamps menu");
    }


    private View CreateView(StampImage Image, Label customStampLabel)
    {
        GestureGrid customStamp = new GestureGrid();
        if (Image.Bounds.Width < 200)
            customStamp.WidthRequest = 195;
        else
            customStamp.WidthRequest = Image.Bounds.Width;
#if MACCATALYST
        Image.WidthRequest = GetImageWidth(customStampLabel);
#endif
        customStamp.PointerPressed += OnCustomStampTapped;
        customStamp.Children.Add(Image);
        return customStamp;
    }

    private double GetImageWidth(Label customStampLabel)
    {
        double defaultWidth = 40;
        double totalWidth = defaultWidth; // Start with the default width
        for (int i = 0; i < customStampLabel.Text.Length; i++)
        {
            double textWidthContribution = 6.2;
            totalWidth += textWidthContribution;
        }
        return totalWidth;
    }

    private async void OnCustomStampTapped(object? sender, EventArgs? e)
    {
        ShowToast("Tap on the page to insert the custom stamp");
        GestureGrid? grid = sender as GestureGrid;
        if (grid!.Children[0] is StampImage image)
        {
            CustomStampImageStream = new MemoryStream();
            if (image != null && image.ImageStream != null)
            {
                await image.ImageStream.CopyToAsync(CustomStampImageStream);
                image.ImageStream.Position = CustomStampImageStream.Position = 0;
                CustomStampWidth = image.Bounds.Width;
                CustomStampHeight = image.Bounds.Height;
            }
            CustomStamp = true;
            StampView!.StampMode = true;
            viewModel.IsStampListVisible = false;
            viewModel.IsStampViewVisible = false;
        }
    }

    private void OnCreateStampMobileClicked(object sender, StampDialogMobileEventArgs e)
    {
        if (stampDialogMobile.Content == null)
            stampDialogMobile.Initialize();
        if (e.IsVisible == true)
            stampDialogMobile.IsVisible = false;
        else
            stampDialogMobile.IsVisible = true;
        viewModel.IsStampViewVisible = false;
    }

    internal void ShapeHighlightDisapper()
    {
        shapeListView.DisappearHighlight();
    }

    internal void TextMarkUpHighlightDisappear()
    {
        textmarkupView.DisappearHighlight();
    }

    private void TextSelection_Changed(object sender, TextSelectionChangedEventArgs e)
    {
        SearchView.Close();
    }

    private void FontSizeSliderBar_ValueChangeEnd(object sender, EventArgs e)
    {
        float thickness = (float)FontSizeSliderBar.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.FontSizeCommand.Execute(thickness);
        }
    }

    private void continuousPage_Clicked(object sender, EventArgs e)
    {
        viewModel.ShowMoreOptions = false;
        PdfViewer.PageLayoutMode = PageLayoutMode.Continuous;
        viewModel.ShowPageLayoutOptions = false;
        viewModel.ShowZoomMobileView = false;
        viewModel.PageLayoutHighlightColor = Colors.Transparent;
        pageByPage.BackgroundColor = Colors.Transparent;
        continousPage.BackgroundColor = Color.FromArgb("#1449454f");
    }

    private void singlePage_Clicked(object sender, EventArgs e)
    {
        viewModel.ShowMoreOptions = false;
        PdfViewer.PageLayoutMode = PageLayoutMode.Single;
        viewModel.ShowPageLayoutOptions = false;
        viewModel.ShowZoomMobileView = false;
        viewModel.PageLayoutHighlightColor = Colors.Transparent;
        pageByPage.BackgroundColor = Color.FromArgb("#1449454f");
        continousPage.BackgroundColor = Colors.Transparent;
    }
    private void Fittopage_Clicked(object sender, EventArgs e)
    {
        viewModel.ShowMoreOptions = false;
        PdfViewer.ZoomMode = ZoomMode.FitToPage;
        viewModel.ShowPageLayoutOptions = false;
        viewModel.ShowZoomMobileView = false;
        viewModel.ZoomHighlightColor = Colors.Transparent;
        FitToPage.BackgroundColor = Color.FromArgb("#1449454f");
        FitToPage.BackgroundColor = Colors.Transparent;
        FitToWidth.BackgroundColor = Colors.Transparent;
    }
    private void Fittowidth_Clicked(object sender, EventArgs e)
    {
        viewModel.ShowMoreOptions = false;
        PdfViewer.ZoomMode = ZoomMode.FitToWidth;
        viewModel.ShowPageLayoutOptions = false;
        viewModel.ShowZoomMobileView = false;
        viewModel.ZoomHighlightColor = Colors.Transparent;
        FitToWidth.BackgroundColor = Color.FromArgb("#1449454f");
        FitToPage.BackgroundColor = Colors.Transparent;
        FitToWidth.BackgroundColor = Colors.Transparent;

    }
}