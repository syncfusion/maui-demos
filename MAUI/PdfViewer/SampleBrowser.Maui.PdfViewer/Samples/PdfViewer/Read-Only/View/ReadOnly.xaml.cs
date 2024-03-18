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
public partial class ReadOnly : SampleView
{
    private string? previousDocument = string.Empty;
    //It is used to delay the current thread's execution, until the user enters the password.
    ManualResetEvent manualResetEvent = new ManualResetEvent(false);
    ToolbarView? toolbar;
    public Stream CustomStampImageStream { get; set; } = new MemoryStream();
    public double CustomStampWidth { get; set; }
    public double CustomStampHeight { get; set; }
    public bool CustomStamp { get; set; }
    public SearchView SearchView { get; set; }
    public StampView? StampView { get; set; }

    CustomToolbarViewModel viewModel;
    public ReadOnly()
    {
        InitializeComponent();
        viewModel = new CustomToolbarViewModel(PdfViewer, "readOnly");
        viewModel.IsReadOnly = true;
        viewModel.IsAnnotationsToolsVisible = false;
        PdfViewer.TextSelectionChanged += PdfViewer_TextSelectionChanged;
        BindingContext = viewModel;
        viewModel.IsFileOpenVisible = false;
        viewModel.IsPageLayoutVisible = false;
#if ANDROID || IOS
        SearchView = MobileSearchView;
        toolbar = MobileToolbar;
#else
        SearchView = DesktopSearchView;
        toolbar = DesktopToolbar;
#endif
        UpdateToolbarProperties();
        DesktopToolbar.SetBindings();
        SearchView.SearchHelper = PdfViewer;
        SearchView.NoMatchesFound += NoMatchesFound;
#if WINDOWS
        //Note: Due to known MAUI issue (#13714) in Entry control when the property "IsVisible=false" by default, the mentioned property is set after the control is loaded.
        if (SearchView.SearchInputEntry != null)
            SearchView.SearchInputEntry.Loaded += SearchInputEntryLoaded;
#endif            
    }

    private void PdfViewer_TextSelectionChanged(object? sender, TextSelectionChangedEventArgs? e)
    {
        if (viewModel.IsAnnotationsToolsVisible == false && (e as TextSelectionChangedEventArgs) != null)
        {
            e.Handled = true;
        }
        else if ((e as TextSelectionChangedEventArgs) != null)
        {
            e.Handled = false;
        }
    }

    void UpdateToolbarProperties()
    {
        if (toolbar != null)
        {
            toolbar.ReadOnlyParentView = this;
            toolbar.PdfViewer = PdfViewer;
            viewModel.IsAnnotationsToolsVisible = false;
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
    /// Handles when a document is loaded.
    /// </summary>
    private void PdfViewer_DocumentLoaded(object sender, EventArgs e)
    {
        viewModel.IsReadOnly = true;
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDocumentLoaded = true;
            bindingContext.IsEditLayoutVisible = false;

        }
        MainThread.BeginInvokeOnMainThread(() =>
        {
            toolbar!.SearchButton!.IsEnabled = true;

        });
        if (viewModel.IsReadOnly == true)
        {
            viewModel.LockAllAnnotationsAndFields(true);           
            PdfViewer.EnableTextSelection = false;
        }
    }

    /// <summary>
    /// Handles when a document is unloaded.
    /// </summary>
    private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
    {
        viewModel.IsDocumentLoaded = false;
        toolbar!.SearchButton!.IsEnabled = false;
        SearchView?.Close();
        viewModel.SelectedAnnotation = null;
        viewModel.IsReadOnly = false;
        viewModel.IsEditMode = false;

#if !(WINDOWS || MACCATALYST)
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
        viewModel.IsAnnotationsToolsVisible = false;
        SearchView!.Close();
    }

    private void PdfViewer_AnnotationDeselected(object sender, AnnotationEventArgs e)
    {
        viewModel.IsAnnotationsToolsVisible = false;
        viewModel.SelectedAnnotation = null;
    }

    private void TextSelection_Changed(object sender, TextSelectionChangedEventArgs e)
    {

    }
}