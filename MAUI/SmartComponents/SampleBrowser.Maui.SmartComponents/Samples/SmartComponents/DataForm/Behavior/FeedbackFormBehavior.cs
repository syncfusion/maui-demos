#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using System;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.DataForm;
    using Syncfusion.Maui.Popup;
    using Microsoft.Maui.Controls;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the behavior for the feedback form.
    /// </summary>
    internal class FeedbackFormBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the submit button instance.
        /// </summary>
        private Button? submitButton;

        /// <summary>
        /// Holds the popup object.
        /// </summary>
        private SfPopup? popup;

        /// <summary>
        /// Holds the online smart fill button instance.
        /// </summary>
        private Button? onlineSmartPasteButton;

        /// <summary>
        /// Holds the clipboard text.
        /// </summary>
        private string? clipboardText;

        /// <summary>
        /// It holds the copied button1.
        /// </summary>
        private Button? copiedButton1;

        /// <summary>
        /// It holds the copied button2.
        /// </summary>
        private Button? copiedButton2;

        /// <summary>
        /// It holds the copied button3.
        /// </summary>
        private Button? copiedButton3;

        /// <summary>
        /// Holds the input data 1.
        /// </summary>
        private Label? inputData1;

        /// <summary>
        /// Holds the input data 2.
        /// </summary>
        private Label? inputData2;

        /// <summary>
        /// Holds the input data 3.
        /// </summary>
        private Label? inputData3;

        /// <summary>
        /// Holds the feedback form model instance.
        /// </summary>
        private FeedbackFormModel feedbackFormModel = new FeedbackFormModel();

        /// <summary>
        /// Holds the Azure AI service instance.
        /// </summary>
        private AzureOpenAIServiceConnector azureAIService = new AzureOpenAIServiceConnector();

        /// <summary>
        /// It holds the index. There are 3 product feedback details in the feedback form model. The index is used to get the feedback details.
        /// </summary>
        int index = -1;

        #endregion

        #region Override Methods

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.copiedButton1 = bindable.Content.FindByName<Button>("CopiedButton1");
            this.copiedButton2 = bindable.Content.FindByName<Button>("CopiedButton2");
            this.copiedButton3 = bindable.Content.FindByName<Button>("CopiedButton3");
            this.inputData1 = bindable.Content.FindByName<Label>("InputData1");
            this.inputData2 = bindable.Content.FindByName<Label>("InputData2");
            this.inputData3 = bindable.Content.FindByName<Label>("InputData3");
            this.dataForm = bindable.Content.FindByName<SfDataForm>("feedbackForm");
            this.popup = bindable.Content.FindByName<SfPopup>("popup");
            if (this.popup != null)
            {
                this.popup.FooterTemplate = DataFormHelper.GetFooterTemplate(this.popup);
                this.popup.ContentTemplate = DataFormHelper.GetContentTemplate(this.popup);
            }

            this.submitButton = bindable.Content.FindByName<Button>("submitButton");
            if (this.submitButton != null)
            {
                this.submitButton.Clicked += this.OnSubmitButtonClicked;
            }

            this.onlineSmartPasteButton = bindable.Content.FindByName<Button>("onlineSmartPaste");
            if (this.onlineSmartPasteButton != null)
            {
                this.onlineSmartPasteButton.Clicked += this.OnlineSmartPasteButtonClicked;
            }

            if (this.copiedButton1 != null)
            {
                this.copiedButton1.Clicked += this.AddTextToClipBoard;
            }

            if (this.copiedButton2 != null)
            {
                this.copiedButton2.Clicked += this.AddTextToClipBoard;
            }

            if (this.copiedButton3 != null)
            {
                this.copiedButton3.Clicked += this.AddTextToClipBoard;
            }

            Clipboard.ClipboardContentChanged += OnClipboardContentChanged;
        }

        /// <summary>
        /// Method triggers when the clipboard content changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private async void OnClipboardContentChanged(object? sender, EventArgs e)
        {
            string? copiedText = await Clipboard.GetTextAsync();
            if (this.onlineSmartPasteButton != null)
            {
                this.onlineSmartPasteButton.IsVisible = !string.IsNullOrEmpty(copiedText);
            }
        }

        /// <summary>
        /// Event handler for the Copy button click event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private async void AddTextToClipBoard(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.Text = "\ue726";
                switch (button.AutomationId) // You can also use button.Id, button.AutomationId, etc.
                {
                    case "CopiedButton1":
                        this.index = 0;
                        await Clipboard.SetTextAsync(this.inputData1!.Text);
                        break;
                    case "CopiedButton2":
                        this.index = 1;
                        await Clipboard.SetTextAsync(this.inputData2!.Text);
                        break;
                    case "CopiedButton3":
                        this.index = 2;
                        await Clipboard.SetTextAsync(this.inputData3!.Text);
                        break;
                }
                await Task.Delay(4000);
                button.Text = "\ue737";
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.submitButton != null)
            {
                this.submitButton.Clicked -= this.OnSubmitButtonClicked;
            }

            if (this.copiedButton1 != null)
            {
                this.copiedButton1.Clicked -= this.AddTextToClipBoard;
            }

            if (this.copiedButton2 != null)
            {
                this.copiedButton2.Clicked -= this.AddTextToClipBoard;
            }

            if (this.copiedButton3 != null)
            {
                this.copiedButton3.Clicked -= this.AddTextToClipBoard;
            }

            Clipboard.ClipboardContentChanged -= OnClipboardContentChanged;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method triggers when the smart paste button clicked.
        /// </summary>
        /// <param name="sender">the sender.</param>
        /// <param name="e">The events args.</param>
        private async void OnlineSmartPasteButtonClicked(object? sender, EventArgs e)
        {
            if (Clipboard.Default.HasText)
            {
                this.clipboardText = await Clipboard.Default.GetTextAsync();
            }
            else
            {
                this.clipboardText = string.Empty;
            }

            if (string.IsNullOrEmpty(this.clipboardText))
            {
                if (this.popup != null)
                {
                    this.popup.IsOpen = true;
                    this.popup.Message = "No text copied to clipboard. Please copy the text and try again.";
                }

                return;
            }

            string dataFormJsonData = JsonConvert.SerializeObject(this.dataForm!.DataObject);
            string prompt = $"Merge the copied data into the DataForm field content, ensuring that the copied text matches suitable field names. Here are the details:" +
                  $"\n\nCopied data: {this.clipboardText}," +
                  $"\nDataForm Field Name: {dataFormJsonData}," +
                  $"\nProvide the resultant DataForm directly." +
                  $"\n\nConditions to follow:" +
                  $"\n1. Do not use the copied text directly as the field name; merge appropriately." +
                  $"\n2. Ignore case sensitivity when comparing copied text and field names." +
                  $"\n3. Final output must be Json format" +
                  $"\n4. No need any explanation or comments in the output" +
                  $"\n Please provide the valid JSON object without any additional formatting characters like backticks or newlines";
            string finalResponse = await this.azureAIService.GetAnswerFromGPT(prompt);
            if (finalResponse == "")
            {
                if (this.index == -1)
                {
                    if (this.popup != null)
                    {
                        this.popup.IsOpen = true;
                        this.popup.Message = "Please copy the data from demo and paste.";
                    }

                    return;
                }

                FeedbackFormModel feedbackFormModel = new FeedbackFormModel();
                string response = feedbackFormModel.FeedbackForms[this.index];
                this.UpdateOfflineSmartFillDataForm(response);

            }
            else
            {
                this.UpdateOfflineSmartFillDataForm(finalResponse);
            }
        }

        /// <summary>
        /// Invokes on send button click.
        /// </summary>
        /// <param name="sender">The send button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSubmitButtonClicked(object? sender, EventArgs e)
        {
            if (this.popup == null || this.dataForm == null)
            {
                return;
            }

            if (this.dataForm.Validate())
            {
                this.popup.Message = "Feedback sent successfully";
            }
            else
            {
                this.popup.Message = "Please enter the required details";
            }

            this.popup.Show();
        }

        /// <summary>
        /// Method to fill the data form with the selected product details.
        /// </summary>
        private void UpdateOfflineSmartFillDataForm(string response)
        {
            //// Deserialize the JSON string to a Dictionary
            var openAIJsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            //// Create lists to hold field names and values
            var filedNames = new List<string>();
            var fieldValues = new List<string>();
            foreach (var data in openAIJsonData!)
            {
                filedNames.Add(data.Key);
                fieldValues.Add(data.Value?.ToString() ?? string.Empty);
            }

            if (this.dataForm!.DataObject is FeedBackForm feedbackForm)
            {
                feedbackForm.Name = fieldValues[0];
                feedbackForm.Email = fieldValues[1];
                feedbackForm.ProductName = fieldValues[2];
                feedbackForm.ProductVersion = fieldValues[3];
                feedbackForm.Rating = (int)Math.Round(double.Parse(fieldValues[4]));
                feedbackForm.Comments = fieldValues[5];
            };

            for (int i = 0; i < filedNames.Count; i++)
            {
                this.dataForm!.UpdateEditor(filedNames[i]);
            }
        }

        #endregion
    }
}