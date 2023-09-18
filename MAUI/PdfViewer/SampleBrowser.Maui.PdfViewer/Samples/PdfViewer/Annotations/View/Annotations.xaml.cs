#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.PdfViewer;
using System.Reflection;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Annotations : SampleView
{
    private string? previousDocument = string.Empty;
    AnnotationToolbarView? toolbar;
#if ANDROID || IOS
    private ViewCell? lastCell;
#endif
    public Stream CustomStampImageStream { get; set; } = new MemoryStream();
    public double CustomStampWidth { get; set; }
    public double CustomStampHeight { get; set; }
    public bool CustomStamp { get; set; }
    public StampView? StampView { get; set; }

    CustomToolbarViewModel viewModel;
    public Annotations()
    {
        InitializeComponent();
        viewModel = new CustomToolbarViewModel(PdfViewer);
        viewModel.LockButtonsVisible = true;
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        BindingContext = viewModel;
        AddItems();
#if ANDROID || IOS
        toolbar = MobileToolbar;
        StampView = MobileStampView;
        fileOperationGrid.IsVisible = false;
#else
        StampView = DesktopStampView;
        toolbar = DesktopToolbar;
#endif
        PdfViewer.UndoCommand!.CanExecuteChanged += UndoCommand_CanExecuteChanged;
        UpdateToolbarProperties();
        DesktopToolbar.SetBindings();
        StampView!.StampHelper = PdfViewer;
        StampView.StampSelected += StampView_StampSelected;
        viewModel.StickyNoteSelected += ViewModel_StickyNoteSelected;

        string basePath = "SampleBrowser.Maui.Resources.Pdf.";
        if (BaseConfig.IsIndividualSB)
            basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
        Stream? documentStream = this.GetType().Assembly.GetManifestResourceStream(basePath + "Annotations1.pdf");
        PdfViewer.LoadDocumentAsync(documentStream, flattenOptions: FlattenOptions.Unsupported);
    }
    public void AddItems()
    {
        saveLayout.Children.Add(CreateView("\uE75f", "Save", 16));
        importLayout.Children.Add(CreateView("\uE782", "Import Annotation",19));
        exportLayout.Children.Add(CreateView("\uE781", "Export Annotation",19));
    }
    private Grid CreateView(string icon, string iconName,int iconSize)
    {
        GestureGrid childRow = new GestureGrid();
        childRow.WidthRequest = 200;
        childRow.HeightRequest = 40;
        childRow.PointerPressed += ItemClicked;
        Label iconLabel = new Label()
        {
            Padding = new Thickness(20, 0, 0, 0),
            TextColor = new Color(0, 0, 0, 0.6f),
            FontSize = iconSize,
            FontFamily = "Maui Material Assets",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Text = icon,
        };
        childRow.Children.Add(iconLabel);
        Label iconNameLabel = new Label()
        {
            Padding = new Thickness(15, 0, 0, 0),
            Margin = new Thickness(30, 0, 0, 0),
            TextColor = new Color(0, 0, 0, 0.6f),
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            Text = iconName,
        };
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
            if (view != null)
            {
                SizeRequest sizeRequest = view.Measure(double.PositiveInfinity, double.PositiveInfinity);
                if (viewModel.ColorPaletteMargin.Left + sizeRequest.Request.Width > Width)
                    viewModel.ColorPaletteMargin = new Thickness(Width - sizeRequest.Request.Width - 20, viewModel.ColorPaletteMargin.Top, viewModel.ColorPaletteMargin.Right, viewModel.ColorPaletteMargin.Bottom);
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
        viewModel.IsSaveLayoutVisible = true;
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
    private void PdfViewer_Tapped(object sender, GestureEventArgs e)
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
                    PdfViewer.AddAnnotation(builtStamp);
                }
                else if (CustomStamp && CustomStampImageStream != null)
                {
                    StampAnnotation customStamp = new StampAnnotation(CustomStampImageStream, e.PageNumber, new RectF(point.X, point.Y, (float)CustomStampWidth, (float)CustomStampHeight));
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
                    StickyNoteAnnotation builtStickyNote = new StickyNoteAnnotation(bindingContextForSticky.StickyIcon,"",e.PageNumber, point);
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
#if WINDOWS || MACCATALYST
            bindingContext.IsShapeListVisible = false;
            bindingContext.IsStickyNoteListVisible = false;
            bindingContext.IsTextMarkupListVisible = false;
            bindingContext.IsStampListVisible = false;
            bindingContext.IsInkColorPalleteVisible = false;
            bindingContext.IsShapeColorPalleteVisible = false;
            bindingContext.IsTextMarkUpColorPalleteVisible = false;
            bindingContext.IsStampOpacitySliderbarVisible = false;
            bindingContext.IsStickyNoteColorPalleteVisible = false;
#endif
            bindingContext.IsFileOperationListVisible = false;
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
            currentTappedDocument= iconName.Text;
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
                bindingContext.BottomToolbarContent= new AnnotationToolbar(viewModel);
#endif
                previousDocument = currentTappedDocument;
            }
            bindingContext.IsFileOperationListVisible= false;
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
        viewModel.IsTextMarkUpColorPalleteVisible = false;
        viewModel.IsStampOpacitySliderbarVisible = false;
#endif
    }

   
   

    private void PdfViewer_AnnotationSelected(object sender, AnnotationEventArgs e)
    {
        viewModel.SelectedAnnotation = e.Annotation;
        
    }

    private void PdfViewer_AnnotationDeselected(object sender, AnnotationEventArgs e)
    {
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

    private void FileListView_FileSelected(object sender, FileSelectedEventArgs e)
    {
        viewModel.DocumentData.FileName = e.FileName;
    }

    private void OnCreateStampClicked(object sender, StampDialogEventArgs e)
    {
        if (e.IsVisible == true)
            stampDialog.IsVisible = false;
        else
            stampDialog.IsVisible = true;
        viewModel.IsStampListVisible = false;
    }

    private async void OnCustomStampCreated(object sender, CustomStampEventArgs e)
    {
        Stream imageStream = await e.StampView!.GetStreamAsync(Syncfusion.Maui.Core.ImageFileFormat.Png);
        StampImage image = new StampImage();
        Stream memoryStream = new MemoryStream();
        await imageStream.CopyToAsync(memoryStream);
        memoryStream.Position = imageStream.Position = 0;
        image.ImageStream = memoryStream;
        image.Source = ImageSource.FromStream(() => imageStream);
        image.HeightRequest = 40;
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


}