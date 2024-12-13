#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GettingStarted : SampleView
{
    Button? fileOpenButton, fileSaveButton;
    string? currentFileName = "PDF_Succinctly.pdf";
    public GettingStarted()
    {
        InitializeComponent();
        AddFileOperationsToolbarItems();  
    }

    void AddFileOperationsToolbarItems()
    {
        fileOpenButton = new Button
        {
            Text = "\ue712",
            FontSize = 24,
            IsEnabled = false,
            FontFamily = "MauiMaterialAssets",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Transparent,
            BorderColor = Colors.Transparent,
            Padding = 10,
            Margin = new Thickness(5, 0, 0, 0),
            Opacity = 0.5,
            Style = base.Style,
        };
        if (Application.Current != null && Application.Current.Resources != null)
        {            
            fileOpenButton.SetAppThemeColor(Button.TextColorProperty,
                (Color)Application.Current.Resources["IconColourLight"], 
                (Color)Application.Current.Resources["IconColour"]);     
            
            if (Application.Current.RequestedTheme == AppTheme.Unspecified)
            {
                fileOpenButton.TextColor = (Color)Application.Current.Resources["DefaultColor"];
            }
        }
        else
        {
            // Fallback to a default color if Application.Current or Resources are null
            fileOpenButton.TextColor = Color.FromArgb("#49454F");
        }    
        fileOpenButton.Clicked += FileOpenButton_Clicked;

        fileSaveButton = new Button
        {
            Text = "\ue75f",           
            FontSize = 24,
            FontFamily = "MauiMaterialAssets",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Transparent,
            BorderColor = Colors.Transparent,
            IsEnabled = false,
            Opacity = 0.5,
            Padding = 10,
            Style = base.Style,
        };
        if (Application.Current != null && Application.Current.Resources != null)
        {
            fileSaveButton.SetAppThemeColor(Button.TextColorProperty,
                (Color)Application.Current.Resources["IconColourLight"],  
                (Color)Application.Current.Resources["IconColour"]);   
           
            if (Application.Current.RequestedTheme == AppTheme.Unspecified)
            {
                fileSaveButton.TextColor = (Color)Application.Current.Resources["DefaultColor"];
            }
        }
        else
        {          
            fileSaveButton.TextColor = Color.FromArgb("#49454F");
        }
        fileSaveButton.Clicked += FileSaveButton_Clicked;
#if !WINDOWS && !MACCATALYST
        fileOpenButton.CornerRadius = 5;
        fileOpenButton.FontSize = 24;
        fileSaveButton.CornerRadius = 5;
        fileSaveButton.FontSize = 24;
        fileSaveButton.Margin = new Thickness(10, 0, 0, 0);
        PdfViewer?.Toolbars?.GetByName("TopToolbar")?.Items?.Insert(0, new Syncfusion.Maui.PdfViewer.ToolbarItem(fileOpenButton, "FileOpenButton"));
        PdfViewer?.Toolbars?.GetByName("TopToolbar")?.Items?.Insert(1, new Syncfusion.Maui.PdfViewer.ToolbarItem(fileSaveButton, "FileSaveButton")); 
#else
        PdfViewer?.Toolbars?.GetByName("PrimaryToolbar")?.Items?.Insert(0, new Syncfusion.Maui.PdfViewer.ToolbarItem(fileOpenButton, "FileOpenButton"));
        PdfViewer?.Toolbars?.GetByName("PrimaryToolbar")?.Items?.Insert(1, new Syncfusion.Maui.PdfViewer.ToolbarItem(fileSaveButton, "FileSaveButton"));
#endif       
    }
    private void PdfViewer_DocumentLoaded(object sender, EventArgs e)
    {
        if (fileOpenButton != null)
        {
            fileOpenButton.IsEnabled = true;
            fileOpenButton.Opacity = 1;
        }
        if (fileSaveButton != null)
        {
            fileSaveButton.IsEnabled = true;
            fileSaveButton.Opacity = 1;
        }
    }

    private async void FileSaveButton_Clicked(object? sender, EventArgs e)
    {
        Stream savedStream = new MemoryStream();
        await PdfViewer.SaveDocumentAsync(savedStream);
        if (!string.IsNullOrEmpty(currentFileName))
        {
            try
            {
                string? filePath = await FileService.SaveAsAsync(currentFileName, savedStream);
                await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
            }
            catch (Exception exception)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
            }
        }
    }

    private async void FileOpenButton_Clicked(object? sender, EventArgs e)
    {
        PdfFileData? fileData = await FileService.OpenFile("pdf");
        if (fileData != null)
        {
            currentFileName = fileData.FileName;
            PdfViewer.LoadDocument(fileData.Stream);
        }
    }

    private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
    {
        if (fileOpenButton != null)
        {
            fileOpenButton.IsEnabled = false;
            fileOpenButton.Opacity = 0.5;
        }
        if (fileSaveButton != null)
        {
            fileSaveButton.IsEnabled = false;
            fileSaveButton.Opacity = 0.5;
        }
    }

    private void PdfViewer_DocumentLoadFailed(object sender, Syncfusion.Maui.PdfViewer.DocumentLoadFailedEventArgs e)
    {
        if (fileOpenButton != null)
        {
            fileOpenButton.IsEnabled = false;
            fileOpenButton.Opacity = 0.5;
        }
        if (fileSaveButton != null)
        {
            fileSaveButton.IsEnabled = false;
            fileSaveButton.Opacity = 0.5;
        }
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
}