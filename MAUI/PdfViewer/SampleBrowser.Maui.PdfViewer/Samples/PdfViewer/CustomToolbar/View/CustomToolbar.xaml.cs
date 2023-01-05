#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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
    private int goToPageNumber = 0;
    private bool isTextChanged = false;
#if WINDOWS
    private bool isZoomOutEnabled = true;
    private bool isZoomInEnabled = true;
#endif
    private string? previousDocument = string.Empty;
#if ANDROID || IOS
    private ViewCell? lastCell;
#endif
    public CustomToolbar()
    {
        InitializeComponent();
#if WINDOWS
        zoomIn.PropertyChanged += ZoomIn_PropertyChanged;
        zoomOut.PropertyChanged += ZoomOut_PropertyChanged;
        goToNext.PropertyChanged += GoToNext_PropertyChanged;
#endif
    }
    private void MessageBox_CloseClicked(object? sender, CloseClickedEventArgs? e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext && e?.Title == "Incorrect Password")
            bindingContext.UpdateFileName("Encrypted PDF");
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        PdfViewer.Handler?.DisconnectHandler();
    }

    private void pageNumberEntry_HandlerChanged(object sender, EventArgs e)
    {
        if (pageNumberEntry.Handler != null)
        {
            var handler = pageNumberEntry.Handler as Microsoft.Maui.Handlers.EntryHandler;
            if (handler != null)
            {
#if ANDROID
            handler.PlatformView.SetSelectAllOnFocus(true);
#elif IOS || MACCATALYST
                handler.PlatformView.EditingDidBegin += (s, e) =>
                {
                    handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
                };
#elif WINDOWS
            handler.PlatformView.Padding= new Microsoft.UI.Xaml.Thickness(0);
            handler.PlatformView.GotFocus += (s, e) =>
            {
                handler.PlatformView.SelectAll();
            };
#endif
            }
        }
    }

    void pageNumberEntry_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        if (entry != null && entry.IsFocused && !string.IsNullOrEmpty(entry.Text))
        {
            bool isNumber = int.TryParse(entry.Text, out goToPageNumber);
            if (isNumber)
            {
                isTextChanged = true;
                entry.Text = goToPageNumber.ToString();
            }
            else
            {
                entry.Text = e.OldTextValue;
            }
        }
    }

    void pageNumberEntry_Unfocused(System.Object sender, Microsoft.Maui.Controls.FocusEventArgs e)
    {
        var entry = (Entry)sender;
        if (PdfViewer != null && PdfViewer.PageCount != 0)
            entry.Text = PdfViewer.PageNumber.ToString();
    }

    private void GoToEntry_Completed(object sender, EventArgs e)
    {
#if ANDROID
        if (Platform.CurrentActivity?.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
        if (sender is Entry entry && isTextChanged && goToPageNumber != PdfViewer.PageNumber)
        {
            if (goToPageNumber > 0 && goToPageNumber <= PdfViewer.PageCount)
            {
                PdfViewer.GoToPage(goToPageNumber);
                isTextChanged = false;
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Error", "Invalid Page Number"));
                entry.Text = PdfViewer.PageNumber.ToString();
            }
        }
    }

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

    private void pageNumberEntry_Focused(object sender, FocusEventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext && bindingContext.IsFilePickerVisible)
            bindingContext.IsFilePickerVisible = false;
    }

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

    private void PdfViewer_PasswordRequested(object sender, PasswordRequestedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (messageBox.IsVisible == true) 
            {
                messageBox.IsVisible = false;
            }
        });
        WaitForPasswordCompletion();
        e.Password = passwordDialog.Password;
        e.Handled = true;
    }

    void WaitForPasswordCompletion()
    {
        while (true)
        {
            if (passwordDialog.IsVisible)
                Task.Delay(1000);
            else
                break;
        }
    }

    private void PdfViewer_DocumentLoadFailed(object sender, DocumentLoadFailedEventArgs e)
    {
        if (e.Message == "Invalid cross reference table.")
            MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Error", "Failed to load the PDF document."));
        else if (e.Message == "Can't open an encrypted document. The password is invalid." && !passwordDialog.IsVisible)
            MainThread.BeginInvokeOnMainThread(() => messageBox.Show("Incorrect Password", "The password you entered is incorrect. Please try again.", "OK"));
    }

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

    private void PdfViewer_DocumentLoaded(object sender, EventArgs e)
    {
        if (messageBox.IsVisible == true)
            messageBox.IsVisible = false;
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.CanZoomIn = bindingContext.CanZoomOut = true;
            bindingContext.ValidateZoomChange();
        }
    }

#if WINDOWS
    private void GoToNext_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {

        if (e.PropertyName == "IsEnabled")
        {
            if (!goToNext.IsEnabled)
                goToNext.Unfocus();
        }

    }
#endif

    private void ZoomOut_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
#if WINDOWS
        if (e.PropertyName == "IsEnabled")
        {
            if (sender is Button button)
            {
                if (isZoomOutEnabled != button.IsEnabled)
                {
                    if (button.IsEnabled)
                        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(1), () =>
                        {
                            VisualStateManager.GoToState(button, "Normal");
                        });
                    if (zoomIn.IsPressed && !zoomOut.IsEnabled)
                        zoomOut.Unfocus();
                }
                isZoomOutEnabled = button.IsEnabled;
            }
        }
#endif
    }

    private void ZoomIn_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
#if WINDOWS
        if (e.PropertyName == "IsEnabled")
        {
            if (sender is Button button)
            {
                if (isZoomInEnabled != button.IsEnabled)
                {
                    if (button.IsEnabled)
                        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(1), () =>
                        {
                            VisualStateManager.GoToState(button, "Normal");
                        });
                }
                isZoomInEnabled = button.IsEnabled;
            }
        }
#endif
    }

    private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
    {
        if (this.BindingContext is CustomToolbarViewModel bindingContext)
        {
            bindingContext.CanZoomIn = false;
            bindingContext.CanZoomOut = false;
        }
    }
}