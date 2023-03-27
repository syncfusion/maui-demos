#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomToolbar : SampleView
{
    private int targetPageNumber = 0;
    private bool pageNumberChanged = false;
    private string? previousDocument = string.Empty;
    //It is used to delay the current thread's execution, until the user enters the password.
    ManualResetEvent manualResetEvent = new ManualResetEvent(false);
    SearchView searchView;
#if ANDROID || IOS
    private ViewCell? lastCell;
#endif

    public CustomToolbar()
    {
        InitializeComponent();
#if ANDROID || IOS
        searchView = MobileSearchView;
#else
        searchView = DesktopSearchView;
#endif
        searchView.SearchHelper = PdfViewer;
        searchView.NoMatchesFound += NoMatchesFound;
#if WINDOWS
        //Note: Due to known MAUI issue (#13714) in Entry control when the property "IsVisible=false" by default, the mentioned property is set after the control is loaded.
        if (searchView.SearchInputEntry != null)
            searchView.SearchInputEntry.Loaded += SearchInputEntryLoaded;
#endif            
    }

    private void SearchInputEntryLoaded(object? sender, EventArgs e)
    {
        if (searchView != null)
            searchView.IsVisible = false;
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
        if (this.BindingContext is CustomToolbarViewModel bindingContext && e?.Title == "Incorrect Password")
            bindingContext.UpdateFileName("Password protected PDF");
    }
    
    private void CloseAllDialogs()
    {
        if (BindingContext is CustomToolbarViewModel bindingContext)
            bindingContext?.CloseAllDialogsCommand?.Execute(true);
    }

    /// <summary>
    /// Handles when leaving the current page
    /// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        PdfViewer?.UnloadDocument();
        PdfViewer?.Handler?.DisconnectHandler();
    }

    /// <summary>
    /// Handles the page number validation when the page number is typed in the entry.
    /// </summary>
    void pageNumberEntry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
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
    void pageNumberEntry_Unfocused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        var entry = (Entry)sender;
        if (PdfViewer != null && PdfViewer.PageCount != 0)
            entry.Text = PdfViewer.PageNumber.ToString();
    }

    /// <summary>
    /// Handles when the page number is entered in the entry.
    /// </summary>
    private void GoToEntry_Completed(object sender, EventArgs e)
    {
        // When the enter key is pressed, the soft keyboard hides.
#if ANDROID
        if (Platform.CurrentActivity?.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
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
                MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Error", "Invalid Page Number"));
                entry.Text = PdfViewer.PageNumber.ToString();
            }
            MainThread.BeginInvokeOnMainThread(() => entry.Unfocus());
        }
    }

    /// <summary>
    /// Handles when a Pdf is tapped.
    /// </summary>
    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
#if ANDROID
        if (Platform.CurrentActivity?.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        if (this.BindingContext is CustomToolbarViewModel bindingContext && bindingContext.IsFilePickerVisible)
        {
            bindingContext.IsFilePickerVisible = false;
        }
    }

    /// <summary>
    /// Handles when the page number entry is focused.
    /// </summary>
    private void pageNumberEntry_Focused(object sender, FocusEventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext && bindingContext.IsFilePickerVisible)
            bindingContext.IsFilePickerVisible = false;
    }

    /// <summary>
    /// Handles when a file picker content is tapped.
    /// </summary>
    private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            if (bindingContext.IsFilePickerVisible)
            {
                if (!passwordDialog.IsVisible || previousDocument != e.Item.ToString())
                    bindingContext.UpdateFileName(e.Item.ToString());
                bindingContext.IsFilePickerVisible = false;
                previousDocument = e.Item.ToString();
            }
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
        searchView?.Close();
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
    }

    /// <summary>
    /// Handles when a document fails to load.
    /// </summary>
    private void PdfViewer_DocumentLoadFailed(object sender, DocumentLoadFailedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            searchButton.IsEnabled = false;
            outlineButton.IsEnabled = false;
            searchView?.Close();
            CloseAllDialogs();
        });
        e.Handled = true;
        if (e.Message == "Invalid cross reference table.")
            MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Error", "Failed to load the PDF document."));
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
        }
        MainThread.BeginInvokeOnMainThread(() =>
        {
        searchButton.IsEnabled = true;
        outlineButton.IsEnabled = true;
        });
    }

    /// <summary>
    /// Handles when a document is unloaded.
    /// </summary>
    private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.IsDocumentLoaded = false;
        }
        searchButton.IsEnabled = false;
        outlineButton.IsEnabled = false;
        searchView?.Close();
    }

    private void Search_Clicked(object sender, EventArgs e)
    {
        CloseAllDialogs();
        MainThread.BeginInvokeOnMainThread(()=> searchView?.Open());
    }

    private void outlineButton_Clicked(object sender, EventArgs e)
    {
        searchView?.Close();
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            if (!viewModel.ShowOutlineView)
                CloseAllDialogs();
            viewModel.ShowOutlineView = !viewModel.ShowOutlineView;
        }
    }

    private void PdfViewer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PdfViewer.IsOutlineViewVisible) && !PdfViewer.IsOutlineViewVisible)
        {
            pageNumberEntry.HideKeyboard();
        }
    }
}