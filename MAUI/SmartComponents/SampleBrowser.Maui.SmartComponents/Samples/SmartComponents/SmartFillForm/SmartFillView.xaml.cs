#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf.Parsing;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public partial class SmartFillView : SampleView
{
    private AIHelper _smartAI = new AIHelper();

    private bool tapped;
    Animation animation;
    public SmartFillView()
    {
        InitializeComponent();
        animation = new Animation();
        Clipboard.ClipboardContentChanged += Clipboard_ClipboardContentChanged;
        PdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
    }

    private void PdfViewer_DocumentLoaded(object? sender, EventArgs? e)
    {
#if ANDROID || IOS
        MobileCopiedData.IsVisible = true;
#endif

#if !WINDOWS
        if (AzureBaseService.IsCredentialValid)
        {
            SubmitForm.IsEnabled = true;
        }
#endif
    }

    private async void Clipboard_ClipboardContentChanged(object? sender, EventArgs e)
    {
        string? copiedText = await Clipboard.GetTextAsync();
        StartBubbleAnimation();
        if (_smartAI.Client != null && AzureBaseService.IsCredentialValid)
        {
            SubmitForm.IsEnabled = true;
        }
        else if (copiedText == viewModel.UserDetail1 || copiedText == viewModel.UserDetail2 || copiedText == viewModel.UserDetail3)
        {
            SubmitForm.IsEnabled = true;
        }
        else
        {
            SubmitForm.IsEnabled = false;
            StopBubbleAnimation();
        }
    }

    // Event handler for the SmartFill button click event
    private async void OnSmartFillClicked(object sender, EventArgs e)
    {
        loadingIndicator.IsRunning = true; // Show loading indicator
        loadingIndicator.IsVisible = true; // Make loading indicator visible
        PdfViewer.Opacity = 0.5; // Dim the PDF viewer to indicate loading
#if ANDROID || IOS
        SmartTools.IsVisible = false;
#endif
        string? inputData = await Clipboard.GetTextAsync(); // Variable to hold inputData text
        if (!string.IsNullOrEmpty(inputData))
        {
            string? inputFileContentAsString = GetXFDFAsString(); // Read XFDF content from PDF Viewer as String to provide input to OpenAI
            string? CustomValues = HintValuesforFieldsAsString();
            string mergePrompt = $"Merge the input data into the XFDF file content. Hint text: {CustomValues}. " +
                                $"Ensure the input data matches suitable field names. " +
                                $"Here are the details: " +
                                $"input data: {inputData}, " +
                                $"XFDF information: {inputFileContentAsString}. " +
                                $"Provide the resultant XFDF directly. " +
                                $"Some conditions need to follow: " +
                                $"1. input data is not directly provided as the field name; you need to think and merge appropriately. " +
                                $"2. When comparing input data and field names, ignore case sensitivity. " +
                                $"3. First, determine the best match for the field name. If there isn?t an exact match, use the input data to find a close match. " +
                                $"remove ```xml and remove ``` if it is there in the code.";
            string resultantXfdfFileStream = await _smartAI.GetAnswerFromGPT(mergePrompt);
            DataHelper dataHelper = new DataHelper();
            var inputFileStream = new MemoryStream();
            // Write the merged XFDF content to the MemoryStream
            var writer = new StreamWriter(inputFileStream, leaveOpen: true);
            if (resultantXfdfFileStream == " ")
            {
                if (inputData.Contains("John"))
                {
                    await writer.WriteAsync(dataHelper.UserDetail1);
                }
                else if (inputData.Contains("David"))
                {
                    await writer.WriteAsync(dataHelper.UserDetail2);
                }
                else
                {
                    await writer.WriteAsync(dataHelper.UserDetail3);
                }
            }
            else
            {
                await writer.WriteAsync(resultantXfdfFileStream);
            }
            await writer.FlushAsync();
            inputFileStream.Position = 0;
            PdfViewer.ImportFormData(inputFileStream, DataFormat.XFdf, true);
        }
#if !WINDOWS
        else if(AzureBaseService.IsCredentialValid)
        {
            if (Application.Current != null)
            {
                var mainWindow = Application.Current.Windows.FirstOrDefault();
                if (mainWindow != null && mainWindow.Page != null)
                {
                    await mainWindow.Page.DisplayAlert("", "No data found in clipboard. Please copy the necessary information and try again.", "OK");
                }
            }
        }
#endif
        loadingIndicator.IsRunning = false; // Stop the loading indicator
        loadingIndicator.IsVisible = false; // Hide the loading indicator
        PdfViewer.Opacity = 1;
        StopBubbleAnimation();
    }

    // Method to read XFDF content from the PDF viewer and return it as a string
    private string GetXFDFAsString()
    {

        MemoryStream stream = new MemoryStream(); // Create a new memory stream
        PdfViewer.ExportFormData(stream, DataFormat.XFdf); // Export form data in XFDF format to the stream
        using (stream) // Use the stream
        {
            if (stream != null) // Check if the stream is not null
            {
                using (StreamReader reader = new StreamReader(stream)) // Create a StreamReader to read from the stream
                {
                    return reader.ReadToEnd(); // Read the stream content to the end and return as string
                }
            }
            else
            {
                return ""; // If stream is null, return an empty string
            }
        }
    }
    // This method generates a string with custom data for each form field in a PDF viewer
    private string HintValuesforFieldsAsString()
    {
        string? hintData = null;
        // Loop through each form field in the PDF viewer
        foreach (FormField form in PdfViewer.FormFields)
        {
            // Check if the form field is a ComboBox
            if (form.GetType() == typeof(ComboBoxFormField))
            {
                ComboBoxFormField? combo = form as ComboBoxFormField;
                if (combo != null)
                {
                    // Append ComboBox name and items to the hintData string
                    hintData += "\n" + combo.Name + " : Collection of Items are :";
                    foreach (string item in combo.Items)
                    {
                        hintData += item + ", ";
                    }
                }
            }
            // Check if the form field is a RadioButton
            else if (form.GetType() == typeof(RadioButtonFormField))
            {
                RadioButtonFormField? radio = form as RadioButtonFormField;
                if (radio != null)
                {
                    // Append RadioButton name and items to the hintData string
                    hintData += "\n" + radio.Name + " : Collection of Items are :";
                    foreach (string item in radio.Items)
                    {
                        hintData += item + ", ";
                    }
                }
            }
            // Check if the form field is a ListBox
            else if (form.GetType() == typeof(ListBoxFormField))
            {
                ListBoxFormField? list = form as ListBoxFormField;
                if (list != null)
                {
                    // Append ListBox name and items to the hintData string
                    hintData += "\n" + list.Name + " : Collection of Items are :";
                    foreach (string item in list.Items)
                    {
                        hintData += item + ", ";
                    }
                }
            }
            // Check if the form field name contains 'Date', 'dob', or 'date'
            else if (form.Name.Contains("Date") || form.Name.Contains("dob") || form.Name.Contains("date"))
            {
                // Append instructions for date format to the hintData string
                hintData += "\n" + form.Name + " : Write Date in MMM/dd/YYYY format";
            }
            // Append other form field names to the hintData string
        }

        // Return the hintData string if not null, otherwise return an empty string
        if (hintData != null)
        {
            return hintData;
        }
        return "";
    }

    private void SavePDF(object sender, EventArgs e)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, "SavedSample.pdf");

        var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        PdfViewer.SaveDocument(stream);
#pragma warning disable CS0618 // Type or member is obsolete
        Application.Current?.MainPage?.DisplayAlert("Success", $"Document saved successfully at:\n{filePath}", "OK");
#pragma warning restore CS0618 // Type or member is obsolete

    }
    private void PrintPDF(object sender, EventArgs e)
    {
        PdfViewer.PrintDocument();
    }


    private async void AddTextToClipBoard(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.Text = "\ue726";
            switch (button.AutomationId) // You can also use button.Id, button.AutomationId, etc.
            {
                case "CopiedButton1":
                    await Clipboard.SetTextAsync(InputData1.Text);
                    break;
                case "CopiedButton2":
                    // Logic for Button2
                    await Clipboard.SetTextAsync(InputData2.Text);
                    break;
                case "CopiedButton3":
                    // Logic for Button3
                    await Clipboard.SetTextAsync(InputData3.Text);
                    break;
            }
            await Task.Delay(3000);
            button.Text = "\ue737";
        }
    }

    private void FullViewForCopiedData(object sender, EventArgs e)
    {
        if (CopiedDataViewButton.Text == "\ue702")
        {
            MobileCopiedData.HeightRequest = 2 * MobileCopiedData.HeightRequest;
            CopiedDataViewButton.Text = "\ue703";
        }
        else
        {
            MobileCopiedData.HeightRequest = MobileCopiedData.HeightRequest / 2;
            CopiedDataViewButton.Text = "\ue702";
        }
    }
    private void StartBubbleAnimation()
    {
        if (!tapped)
        {
            var bubbleEffect = new Animation(v => SubmitForm.Scale = v, 1, 1.05, Easing.CubicInOut);
            var fadeEffect = new Animation(v => SubmitForm.Opacity = v, 1, 0.5, Easing.CubicInOut);

            animation.Add(0, 0.5, bubbleEffect);
            animation.Add(0, 0.5, fadeEffect);
            animation.Add(0.5, 1, new Animation(v => SubmitForm.Scale = v, 1.05, 1, Easing.CubicInOut));
            animation.Add(0.5, 1, new Animation(v => SubmitForm.Opacity = v, 1, 1, Easing.CubicInOut));

            animation.Commit(this, "BubbleEffect", length: 1500, easing: Easing.CubicInOut, repeat: () => true);

        }
    }

    private void StopBubbleAnimation()
    {
        this.AbortAnimation("BubbleEffect");
        tapped = false;
    }
}