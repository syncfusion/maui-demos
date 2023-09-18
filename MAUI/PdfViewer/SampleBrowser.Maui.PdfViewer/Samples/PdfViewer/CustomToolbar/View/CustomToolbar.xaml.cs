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

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomToolbar : SampleView
{
    private string? previousDocument = string.Empty;
    //It is used to delay the current thread's execution, until the user enters the password.
    ManualResetEvent manualResetEvent = new ManualResetEvent(false);
    ToolbarView? toolbar;

#if ANDROID || IOS
    private ViewCell? lastCell;
#endif
    public Stream CustomStampImageStream { get; set; } = new MemoryStream();
    public double CustomStampWidth { get; set; }
    public double CustomStampHeight { get; set; }
    public bool CustomStamp { get; set; }
    public SearchView SearchView { get; set; }
    public StampView? StampView { get; set; }

    CustomToolbarViewModel viewModel;
    public CustomToolbar()
    {
        InitializeComponent();
        viewModel = new CustomToolbarViewModel(PdfViewer);
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        BindingContext = viewModel;
        AddDocumentItems();
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
    }
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
    private Grid CreateView(string iconName)
    {
        GestureGrid childRow = new GestureGrid();
        childRow.WidthRequest = 220;
        childRow.HeightRequest = 40;
        childRow.PointerPressed += ListView_ItemTapped;
        
        Label iconNameLabel = new Label()
        {
            Padding = new Thickness(15, 0, 0, 0),
            Margin = new Thickness(5, 0, 0, 0),
            TextColor = new Color(0, 0, 0, 0.6f),
            FontSize = 15,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Text = iconName,
        };
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

    private void SearchInputEntryLoaded(object? sender, EventArgs e)
    {
        if (SearchView != null)
            SearchView.IsVisible = false;
    }

    private void NoMatchesFound(object? sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Search Result", "No matches were found"));
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
                if (BindingContext is CustomToolbarViewModel viewModel)
                {
                    if (viewModel.DocumentData.FileName == "rotated_document.pdf")
                    {
                        PdfLoadedDocument? document = new PdfLoadedDocument(viewModel.DocumentData.DocumentStream);
                        PdfLoadedPage? page = document.Pages[e.PageNumber - 1] as PdfLoadedPage;
                        if (page != null)
                            point = AdjustForRotation(page.Size.Width, page.Size.Height, page.Rotation, point.X, point.Y);
                        document.Dispose();
                        document = null;
                    }
                }
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
                    if (BindingContext is CustomToolbarViewModel viewModel)
                    {
                        if(viewModel.DocumentData.FileName == "rotated_document.pdf")
                        {
                            PdfLoadedDocument? document = new PdfLoadedDocument(viewModel.DocumentData.DocumentStream);
                            PdfLoadedPage? page = document.Pages[e.PageNumber - 1] as PdfLoadedPage;
                            if (page != null)
                                point = AdjustForRotation(page.Size.Width, page.Size.Height, page.Rotation, point.X, point.Y);
                            document.Dispose();
                            document = null;
                        }
                    }
                    StickyNoteAnnotation builtStickyNote = new StickyNoteAnnotation(bindingContextForSticky.StickyIcon,string.Empty,e.PageNumber, point);
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
			bindingContext.IsFileOperationListVisible = false;
#endif
        }
    }

    internal static Point AdjustForRotation(double pageWidth, double pageHeight, PdfPageRotateAngle pageRotationAngle, double x, double y)
    {
        if (pageRotationAngle == PdfPageRotateAngle.RotateAngle90)
        {
            double rotatedX = pageHeight - y;
            double rotatedY = x;
            return new Point(rotatedX, rotatedY);
        }
        else if (pageRotationAngle == PdfPageRotateAngle.RotateAngle180)
        {
            double rotatedX = pageWidth - x;
            double rotatedY = pageHeight - y;
            return new Point(rotatedX, rotatedY);
        }
        else if (pageRotationAngle == PdfPageRotateAngle.RotateAngle270)
        {
            double rotatedX = y;
            double rotatedY = pageWidth - x;
            return new Point(rotatedX, rotatedY);
        }
        else
        {
            return new Point(x, y);
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
                if (!passwordDialog.IsVisible || previousDocument != currentTappedDocument)
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
    /// Handles password requested event.
    /// </summary>
    private void PdfViewer_PasswordRequested(object sender, PasswordRequestedEventArgs e)
    {
        e.Handled = true;

        //Show the password dialog.
        passwordDialog.Dispatcher.Dispatch(() => {
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
            if(sender is PasswordDialogBox passwordDialog && passwordDialog.Password == null)
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
        if (e.PropertyName == nameof(PdfViewer.IsOutlineViewVisible) && DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
        {
            toolbar!.IsVisible = !PdfViewer.IsOutlineViewVisible;
        }
    }
   
    
    private void PdfViewer_AnnotationSelected(object sender, AnnotationEventArgs e)
    {
        viewModel.SelectedAnnotation = e.Annotation;
        SearchView!.Close();
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
        Label customStampLabel = e.CustomStampLabel ?? new Label(); // Initialize only if it's null
        Stream imageStream = await e.StampView!.GetStreamAsync(Syncfusion.Maui.Core.ImageFileFormat.Png);
        StampImage image = new StampImage();
        Stream memoryStream = new MemoryStream();
        await imageStream.CopyToAsync(memoryStream);
        memoryStream.Position = imageStream.Position = 0;
        image.ImageStream = memoryStream;
        image.Source = ImageSource.FromStream(() => imageStream);
        image.HeightRequest = 40;
#if WINDOWS || MACCATALYST
        image.HorizontalOptions = LayoutOptions.Start;
#endif
        image.Margin = new Thickness(5);
        StampView?.CustomStampListLayout?.Children.Add(CreateView(image, customStampLabel));
        if (sender is View view)
            view.IsVisible = false;
        ShowToast("The stamp is created and added to custom stamps menu");
    }


    private View CreateView(StampImage Image,Label customStampLabel)
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