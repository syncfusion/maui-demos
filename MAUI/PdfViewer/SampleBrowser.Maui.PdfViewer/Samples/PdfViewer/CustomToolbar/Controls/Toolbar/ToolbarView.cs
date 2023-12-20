#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public class ToolbarView : Microsoft.Maui.Controls.ContentView
{
    private int targetPageNumber = 0;
    private bool pageNumberChanged = false;
    private Syncfusion.Maui.PdfViewer.SfPdfViewer? pdfViewer;

    public CustomEntry? PageNumberEntry { get; set; }
    public Label? PageCountLabel { get; set; }
    public Button? SearchButton { get; set; }
    public Button? OutlineButton { get; set; }
    public Button? GoToPreviousPageButton { get; set; }
    public Button? GoToNextPageButton { get; set; }
    public Button? UndoButton { get; set; }
    public Button? SaveButton { get; set; }
    public Button? RedoButton { get; set; }
    public Button? StampButton { get; set; }

    public CustomToolbar? ParentView { get; set; }

    public ReadOnly? ReadOnlyParentView { get; set; }
    
    public Syncfusion.Maui.PdfViewer.SfPdfViewer? PdfViewer 
    { 
        get
        {
            return pdfViewer;
        }
        set
        {
            pdfViewer = value;
            BindPdfViewerValues();
        }
    }

    void BindPdfViewerValues()
    {
        if (PdfViewer != null)
        {
            BindingBase pageNumberBindingBase = new Binding(nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.PageNumber), source: PdfViewer);
            PageNumberEntry?.SetBinding(CustomEntry.TextProperty, pageNumberBindingBase);
            BindingBase pageCountBindingBase = new Binding(nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.PageCount), source: PdfViewer);
            PageCountLabel?.SetBinding(Label.TextProperty, pageCountBindingBase);
            if (GoToPreviousPageButton != null)
                GoToPreviousPageButton.Command = PdfViewer.GoToPreviousPageCommand;
            if (GoToNextPageButton != null)
                GoToNextPageButton.Command = PdfViewer.GoToNextPageCommand;
            if (UndoButton != null)
                UndoButton.Command = PdfViewer.UndoCommand;
            if (RedoButton != null)
                RedoButton.Command = PdfViewer.RedoCommand;
        }
    }

    /// <summary>
    /// Handles the page number validation when the page number is typed in the entry.
    /// </summary>
    public void pageNumberEntry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (entry != null && entry.IsFocused && !string.IsNullOrEmpty(entry.Text))
        {
            bool isNumber = int.TryParse(entry.Text, out targetPageNumber);
            if (isNumber)
            {
                pageNumberChanged = true;
                entry.Text = targetPageNumber.ToString();
            }
            else
            {
                entry.Text = e.OldTextValue;
            }
        }
    }

    /// <summary>
    /// Handles when the page number entry is unfocused.
    /// </summary>
    public void pageNumberEntry_Unfocused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        var entry = (Entry)sender;
        if (PdfViewer != null && PdfViewer.PageCount != 0)
            entry.Text = PdfViewer.PageNumber.ToString();
    }

    public void outlineButton_Clicked(object sender, EventArgs e)
    {
        ParentView?.SearchView?.Close();
        ReadOnlyParentView?.SearchView?.Close();
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            if (!viewModel.ShowOutlineView)
            {
                ParentView?.CloseAllDialogs();
                ReadOnlyParentView?.CloseAllDialogs();
            }
            viewModel.IsShapeListVisible = false;
            viewModel.IsStampListVisible = false;
            viewModel.IsStickyNoteListVisible = false;
            viewModel.IsTextMarkupListVisible = false;
            viewModel.IsInkColorPalleteVisible = false;
            viewModel.IsShapeColorPalleteVisible = false;
            viewModel.IsLineAndArrowColorPalleteVisible = false;
            viewModel.IsStickyNoteColorPalleteVisible = false;
            viewModel.IsStampOpacitySliderbarVisible = false;
            viewModel.IsTextMarkUpColorPalleteVisible = false;
            viewModel.ShowOutlineView = !viewModel.ShowOutlineView;
            viewModel.DeselectSelectedAnnotation();
            if (pdfViewer != null)
            {
                pdfViewer.AnnotationMode = Syncfusion.Maui.PdfViewer.AnnotationMode.None;
                viewModel.ClearButtonHighlights();
            }
        }
    }

    public void Search_Clicked(object sender, EventArgs e)
    {
#if ANDROID || IOS
        if (ParentView != null && ParentView.BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute("Back");
            viewModel.BottomToolbarContent = new AnnotationToolbar(viewModel);
        }
#else
        ParentView?.CloseAllDialogs();
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.IsShapeListVisible = false;
            viewModel.IsStampListVisible = false;
            viewModel.IsStickyNoteListVisible = false;
            viewModel.IsTextMarkupListVisible = false;
            viewModel.IsInkColorPalleteVisible = false;
            viewModel.IsShapeColorPalleteVisible = false;
            viewModel.IsLineAndArrowColorPalleteVisible = false;
            viewModel.IsStickyNoteColorPalleteVisible = false;
            viewModel.IsStampOpacitySliderbarVisible = false;
            viewModel.IsTextMarkUpColorPalleteVisible = false;
            viewModel.IsEraserThicknessToolbarVisible = false;
            viewModel.DeselectSelectedAnnotation();
            if (pdfViewer != null)
            {
                pdfViewer.AnnotationMode = Syncfusion.Maui.PdfViewer.AnnotationMode.None;
                viewModel.ClearButtonHighlights();
            }
        }
#endif
        MainThread.BeginInvokeOnMainThread(() => ParentView?.SearchView?.Open());
        MainThread.BeginInvokeOnMainThread(() => ReadOnlyParentView?.SearchView?.Open());
    }
    
    /// <summary>
    /// Handles when the page number entry is focused.
    /// </summary>
    public void pageNumberEntry_Focused(object sender, FocusEventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext && bindingContext.IsFilePickerVisible)
            bindingContext.IsFilePickerVisible = false;
    }

    /// <summary>
    /// Handles when the page number is entered in the entry.
    /// </summary>
    public void GoToEntry_Completed(object sender, EventArgs e)
    {
        // When the enter key is pressed, the soft keyboard hides.
#if ANDROID
        if (Platform.CurrentActivity?.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        if (PdfViewer != null)
        {
            if (sender is Entry entry && pageNumberChanged && targetPageNumber != PdfViewer.PageNumber)
            {
                if (targetPageNumber > 0 && targetPageNumber <= PdfViewer.PageCount)
                {
                    PdfViewer.GoToPage(targetPageNumber);
                    pageNumberChanged = false;
                }
                else
                {
                    // Shows the invalid page number to the user when trying to go to the entered page number when the loaded PDF document does not have a page number.
                    MainThread.BeginInvokeOnMainThread(() => ParentView?.messageBox?.Show("Error", "Invalid Page Number"));
                    MainThread.BeginInvokeOnMainThread(() => ReadOnlyParentView?.messageBox?.Show("Error", "Invalid Page Number"));
                    entry.Text = PdfViewer.PageNumber.ToString();
                }
                MainThread.BeginInvokeOnMainThread(() => entry.Unfocus());
            }
        }
    }
}
