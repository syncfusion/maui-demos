#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core.Converters;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.PdfViewer;
using System.Reflection;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Annotations : SampleView
{
    private string? previousDocument = string.Empty;
    AnnotationToolbarView? toolbar;
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
    public StampView? StampView { get; set; }

    CustomToolbarViewModel viewModel;
    SignatureViewOverlay? signatureViewOverlay;
    public Annotations()
    {
        InitializeComponent();
        viewModel = new CustomToolbarViewModel(PdfViewer, "annotation");
        viewModel.LockButtonsVisible = true;
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        PdfViewer.PropertyChanged += PdfViewer_PropertyChanged;
        PdfViewer.AnnotationAdded += PdfViewer_AnnotationAdded;
        BindingContext = viewModel;
        AddItems();
        UpdateVisibility();
#if ANDROID || IOS
        toolbar = MobileToolbar;
        StampView = MobileStampView;
        fileOperationGrid.IsVisible = false;
#else
        StampView = DesktopStampView;
        toolbar = DesktopToolbar;
        DesktopToolbar.StampViewDesktop = DesktopStampView;
#endif
        PdfViewer.UndoCommand!.CanExecuteChanged += UndoCommand_CanExecuteChanged;
        UpdateToolbarProperties();
        DesktopToolbar.SetBindings();
        StampView!.StampHelper = PdfViewer;
        StampView.StampSelected += StampView_StampSelected;
        viewModel.StickyNoteSelected += ViewModel_StickyNoteSelected;

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
        string basePath = "SampleBrowser.Maui.Resources.Pdf.";
        if (BaseConfig.IsIndividualSB)
            basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
        Stream? documentStream = this.GetType().Assembly.GetManifestResourceStream(basePath + "Annotations1.pdf");
        PdfViewer?.LoadDocumentAsync(documentStream, flattenOptions: FlattenOptions.Unsupported);
        viewModel.FileData = new PdfFileData("Annotations1.pdf", documentStream);
    }
    private void UpdateVisibility()
    {
#if WINDOWS || MACCATALYST
        bottomToolbar.IsVisible = false;
        grid.IsVisible = false;
#else
        bool isBottomToolbarVisible = PdfViewer.IsOutlineViewVisible;
        var isVisible = new InvertedBoolConverter().Convert(isBottomToolbarVisible, typeof(bool), null, null);
        grid.IsVisible = viewModel.IsFileOperationListVisible;
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

    private void PdfViewer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AnnotationMode))
        {
            if (PdfViewer.AnnotationMode != AnnotationMode.Signature)
            {
                SignatureHelper.CurrentSignatureItem = null;
            }
            if ((PdfViewer.AnnotationMode != AnnotationMode.Polygon && PdfViewer.AnnotationMode != AnnotationMode.Polyline) && undoAlreadyExecuted)
            {
                viewModel.IsSaveLayoutVisible = true;
            }
            else if (PdfViewer.AnnotationMode == AnnotationMode.Polygon || PdfViewer.AnnotationMode == AnnotationMode.Polyline)
            {
                viewModel.IsSaveLayoutVisible = false;
            }
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
            {
                if (e.PropertyName == nameof(PdfViewer.AnnotationMode))
                {
                    toolbar!.IsVisible = PdfViewer.AnnotationMode != AnnotationMode.Signature;
                    bottomToolbar!.IsVisible = PdfViewer.AnnotationMode != AnnotationMode.Signature;
                }
            }
        }
    }

    public void AddItems()
    {
        saveLayout.Children.Add(CreateView("\uE75f", "Save", 16));
        importLayout.Children.Add(CreateView("\uE782", "Import Annotation", 19));
        exportLayout.Children.Add(CreateView("\uE781", "Export Annotation", 19));
        printLayout.Children.Add(CreateView("\uE77f", "Print", 19));
    }

    void InitializeSignatureDialog()
    {
        if (signatureViewOverlay == null)
        {
            signatureViewOverlay = new SignatureViewOverlay();
            signatureViewOverlay.SignatureListBottomMarginForMobile = 47;
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

    private Grid CreateView(string icon, string iconName, int iconSize)
    {
        GestureGrid childRow = new GestureGrid();
        childRow.WidthRequest = 200;
        childRow.HeightRequest = 40;
        childRow.PointerPressed += ItemClicked;
        Color lightColor = new Color();
        Color darkColor = new Color();
        if (Application.Current != null)
        {
            lightColor = (Color)Application.Current.Resources["IconColourLight"];
            darkColor = (Color)Application.Current.Resources["IconColour"];
        }
        Label iconLabel = new Label()
        {
            Padding = new Thickness(20, 0, 0, 0),
            TextColor = new Color(0, 0, 0, 0.6f),
            FontSize = iconSize,
            FontFamily = "MauiMaterialAssets",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Text = icon,
        };
        iconLabel.SetAppThemeColor(Label.TextColorProperty, lightColor, darkColor);
        childRow.Children.Add(iconLabel);
        Label iconNameLabel = new Label()
        {
            Padding = new Thickness(15, 0, 0, 0),
            Margin = new Thickness(35, 0, 0, 0),
            TextColor = new Color(0, 0, 0, 0.6f),
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            Text = iconName,
        };
        iconNameLabel.SetAppThemeColor(Label.TextColorProperty, lightColor, darkColor);
        childRow.Children.Add(iconNameLabel);
        return childRow;
    }
    private void ItemClicked(object? sender, EventArgs e)
    {
        GestureGrid? grid = sender as GestureGrid;
        string operation = "";
        if (grid!.Children[1] is Label iconName)
        {
            operation = iconName.Text;
        }
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            if (operation.Contains("Save"))
                bindingContext.SaveDocument();
            else if (operation.Contains("Import"))
                bindingContext.ImportAnnotations();
            else if (operation.Contains("Export"))
                bindingContext.ExportAnnotations();
            else if (operation.Contains("Print"))
                bindingContext.PrintDocument();
            bindingContext.IsFileOperationListVisible = false;
        }
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
                view = freeTextAnnotationColorPalatte;
            if (view != null)
            {
                Size sizeRequest = view.Measure(double.PositiveInfinity, double.PositiveInfinity);
                if (viewModel.ColorPaletteMargin.Left + sizeRequest.Width <= Width)
                    viewModel.ColorPaletteMargin = new Thickness(viewModel.ColorPaletteMargin.Left, viewModel.ColorPaletteMargin.Top, viewModel.ColorPaletteMargin.Right, viewModel.ColorPaletteMargin.Bottom);
            }
        }
        else if (e.PropertyName == nameof(viewModel.IsFileOperationListVisible))
        {
#if ANDROID || IOS
            grid.IsVisible = viewModel.IsFileOperationListVisible;
#endif
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
            if (viewModel.IsFreeTextFillColorVisble && freeTextAnnotationColorPalatte.Content == null)
                freeTextAnnotationColorPalatte.Initialize();
        }
        else if (propertyName == nameof(viewModel.IsEraserThicknessToolbarVisible))
        {
            if (viewModel.IsEraserThicknessToolbarVisible && eraserThicknessBar.Content == null)
                eraserThicknessBar.Initialize();
        }
        else if(propertyName==nameof(viewModel.IsFreeTextFontSizeListViewVisible))
        {
            if(viewModel.IsFreeTextFontSizeListViewVisible && freeTextMarkupView.Content==null)
                freeTextMarkupView.Initialize();
        }
        else if(propertyName==nameof(viewModel.IsThicknessSliderbarVisible))
        {
            if(viewModel.IsThicknessSliderbarVisible && thicknessBar.Content == null)
                thicknessBar.Initialize();
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
        PdfViewer.AnnotationMode = PdfViewer.AnnotationMode == AnnotationMode.None ? AnnotationMode.Stamp : AnnotationMode.None;
        ShowToast("Tap on the page to insert the selected stamp");
    }

    async void ShowToast(string text)
    {
        toast.Opacity = 1;
        toastText.Text = text;
        toast.InputTransparent = true;
        await toast.FadeTo(0, 2000, Easing.SinIn);
    }

    private void UndoCommand_CanExecuteChanged(object? sender, EventArgs e)
    {
        if (PdfViewer.AnnotationMode != AnnotationMode.Polygon && PdfViewer.AnnotationMode != AnnotationMode.Polyline)
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

    internal void CloseAllDialogs()
    {
        viewModel.CloseAllDialogs();
    }

    internal void HideOverlayToolbars()
    {
        viewModel.HideOverlayToolbars();
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
        //887093 - [Sample browser] More than one annotations are added at the same time
        if (StampView != null && e != null && PdfViewer.AnnotationMode == AnnotationMode.Stamp)
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
        if (e != null && PdfViewer.AnnotationMode == AnnotationMode.StickyNote)
        {
            if (this.BindingContext is CustomToolbarViewModel bindingContextForSticky)
            {
                if (bindingContextForSticky.IsStickyNoteMode)
                {
                    PointF point = new PointF(e.PagePosition.X, e.PagePosition.Y);
                    StickyNoteAnnotation builtStickyNote = new StickyNoteAnnotation(bindingContextForSticky.StickyIcon, "", e.PageNumber, point);
                    PdfViewer.AddAnnotation(builtStickyNote);
                    bindingContextForSticky.IsStickyNoteMode = false;
                }
            }
        }
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.HideOverlayToolbars();
            bindingContext.IsFileListViewVisible = false;
            bindingContext.ShowMoreOptions = false;
            bindingContext.IsStampViewVisible = false;
            bindingContext.StampHighlightColor = Colors.Transparent;
            bindingContext.StickyNoteHighlightColor = Colors.Transparent;
            bindingContext.IsFreeTextFontSizeListViewVisible = false;
            bindingContext.FreeTextFontSizeHighlightColor = Colors.Transparent;
#if WINDOWS || MACCATALYST
            bindingContext.IsShapeListVisible = false;
            bindingContext.IsStickyNoteListVisible = false;
            bindingContext.IsTextMarkupListVisible = false;
            bindingContext.IsStampListVisible = false;
            bindingContext.IsInkColorPalleteVisible = false;
            bindingContext.IsShapeColorPalleteVisible = false;
            bindingContext.IsLineAndArrowColorPalleteVisible = false;
            bindingContext.IsTextMarkUpColorPalleteVisible = false;
            bindingContext.IsStampOpacitySliderbarVisible = false;
            bindingContext.IsStickyNoteColorPalleteVisible = false;
            bindingContext.IsFreetextColorPalatteisible = false;
            bindingContext.IsFreeTextFillColorVisble = false;
            bindingContext.IsFreeTextSliderVisible = false;
            bindingContext.IsFreeTextFontListVisible = false;
            bindingContext.ColorPaletteHighlightColor = Colors.Transparent;
            bindingContext.IsEraserThicknessToolbarVisible = false;
            bindingContext.StickyNoteIconChangeHighlightColor = Colors.Transparent;
            bindingContext.FileOperationHighlightColor = Colors.Transparent;
            bindingContext.InkHighlightColor = Colors.Transparent;
            bindingContext.ShapeHighlightColor = Colors.Transparent;
            bindingContext.TextMarkupHighlightColor = Colors.Transparent;
            bindingContext.StickyNoteHighlightColor = Colors.Transparent;
            bindingContext.FreeTextHighlightColor = Colors.Transparent;
            bindingContext.StampHighlightColor = Colors.Transparent;
            //bindingContext.ColorPaletteHighlightColor = Colors.Transparent;
#endif
            bindingContext.IsFileOperationListVisible = false;
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
                    StampAnnotation stamp = new StampAnnotation(stream, pageNumber, new Rect(e.PagePosition.X - imageWidth / 2, e.PagePosition.Y - imageHeight / 2, imageWidth, imageHeight));
                    stamp.IsSignature = true;
                    PdfViewer.AddAnnotation(stamp);
                }
            }
            else if (SignatureHelper.CurrentSignatureItem is TypeSignatureItem typeSignature)
            {

            }
            else if (SignatureHelper.CurrentSignatureItem is ImageSignatureItem imageSignature)
            {
                if (imageSignature.ImageStream != null)
                {
                    StampAnnotation stamp = new StampAnnotation(imageSignature.ImageStream, pageNumber, new Rect(e.PagePosition.X - imageSignature.ImageSize.Width / 2, e.PagePosition.Y - imageSignature.ImageSize.Height / 2, imageSignature.ImageSize.Width, imageSignature.ImageSize.Height));
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
                if (previousDocument != currentTappedDocument)
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
    }

    /// <summary>
    /// Handles when a document is unloaded.
    /// </summary>
    private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
    {
        viewModel.IsDocumentLoaded = false;
        viewModel.IsSaveLayoutVisible = false;
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
#endif
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

    private void PdfViewer_AnnotationSelected(object sender, AnnotationEventArgs e)
    {
        viewModel.SelectedAnnotation = e.Annotation;
        //934549 - [WinUI] Color and delete buttons are shown for locked annotations, 934555 - [WinUI] Unlock button is not showing when a locked annotation is selected
        if (viewModel.AnnotationSettingsIsLocked())
        {
            viewModel.IsThicknessButtonVisible = false;
            viewModel.IsColorPaletteButtonVisible = false;
            viewModel.IsDeleteButtonVisible = false;
            viewModel.IsFontSizeButtonViewVisible = false;
            viewModel.IsStickyIconChangeButtonVisible = false;
            viewModel.IsUnlockButtonVisible = false;
            viewModel.IsLockButtonVisible = true;
        }
        else
        {
            if (viewModel.SelectedAnnotation != null && viewModel.SelectedAnnotation.IsLocked)
            {
                viewModel.IsThicknessButtonVisible = false;
                viewModel.IsColorPaletteButtonVisible = false;
                viewModel.IsDeleteButtonVisible = false;
                viewModel.IsFontSizeButtonViewVisible = false;
                viewModel.IsStickyIconChangeButtonVisible = false;
                viewModel.IsUnlockButtonVisible = true;
                viewModel.IsLockButtonVisible = false;
            }
        }
        //940435 - Sticky note list view icons are not rendered when selecting the sticky note annotation [Windows, MAC].
#if (WINDOWS || MACCATALYST)
        if (viewModel.SelectedAnnotation is StickyNoteAnnotation)
        {
            viewModel.IsStickyIconChangeButtonVisible = true;
        }
#endif
        if (viewModel.SelectedAnnotation is FreeTextAnnotation)
        {
            viewModel.IsFreetextToolsVisible = true;
            viewModel.IsAnnotationsToolsVisible = true;
            viewModel.IsEditLayoutVisible = true;
#if ANDROID || IOS
            viewModel.BottomToolbarContent = new FreetextPropertyToolbar(viewModel);
#endif
        }

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

    private void saveButton_Clicked(object? sender, EventArgs e)
    {
        viewModel?.SaveDocument();
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

    private void printButton_Clicked(object sender, EventArgs e)
    {
        viewModel?.PrintDocument();
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
        image.HorizontalOptions = LayoutOptions.Start;
        image.Margin = new Thickness(5);
        StampView?.CustomStampListLayout?.Children.Add(CreateView(image));
        if (sender is View view)
            view.IsVisible = false;
        ShowToast("The stamp is created and added to custom stamps menu");
    }

    private View CreateView(StampImage Image)
    {
        GestureGrid customStamp = new GestureGrid();
        if (Image.Bounds.Width < 200)
            customStamp.WidthRequest = 195;
        else
            customStamp.WidthRequest = Image.Bounds.Width;
        customStamp.PointerPressed += OnCustomStampTapped;
        customStamp.Children.Add(Image);
        return customStamp;
    }

    private async void OnCustomStampTapped(object? sender, EventArgs? e)
    {
        // 946529 - In custom toolbar, the selected custom stamp is not rendered when tapping on the PDF [All Platform]
        PdfViewer.AnnotationMode = PdfViewer.AnnotationMode == AnnotationMode.None ? AnnotationMode.Stamp : AnnotationMode.None;
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

    internal void FreeTextFontSizeListDisappear()
    {
        freeTextMarkupView.FreeTextFontSizeListDisappear();
    }

    private void FontSizeSliderBar_ValueChangeEnd(object sender, EventArgs e)
    {
        float thickness = (float)FontSizeSliderBar.Value;
        if (BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.FontSizeCommand.Execute(thickness);
        }
    }

    private void PdfViewer_AnnotationPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PdfViewer.AnnotationMode) && PdfViewer.AnnotationMode == AnnotationMode.None)
        {
            viewModel.ClearButtonHighlights();
        }
    }
}