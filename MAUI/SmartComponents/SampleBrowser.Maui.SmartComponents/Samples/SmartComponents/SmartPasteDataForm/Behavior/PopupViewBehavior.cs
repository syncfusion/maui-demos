#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using System;
    using Syncfusion.Maui.DataForm;
    using Syncfusion.Maui.Buttons;
    using Microsoft.Maui.Controls;
    using Syncfusion.Maui.Inputs;
    using Newtonsoft.Json;
    using Syncfusion.Maui.Popup;

    /// <summary>
    /// Represents the behavior for the feedback form.
    /// </summary>
    internal class PopupViewBehavior : Behavior<Grid>
    {
        #region Fields

        /// <summary>
        /// Holds the smart fill button instance.
        /// </summary>
        private SfButton? smartFillButton;

        /// <summary>
        /// Holds the feedback editor instance.
        /// </summary>
        private Editor? inputData;

        /// <summary>
        /// Holds the product info.
        /// </summary>
        private SfComboBox? productInfo;

        /// <summary>
        /// Holds the feedback form model instance.
        /// </summary>
        private FeedbackFormModel feedbackFormModel = new FeedbackFormModel();

        /// <summary>
        /// Gets or sets the data form.
        /// </summary>
        public SfDataForm? DataForm { get; set; }

        /// <summary>
        /// Gets or sets the AI popup window.
        /// </summary>
        public SfPopup? AIPopupWindow { get; set; }

        #endregion

        #region Override Methods

        protected override void OnAttachedTo(Grid bindable)
        {
            base.OnAttachedTo(bindable);
            this.inputData = bindable.FindByName<Editor>("inputData");
            this.productInfo = bindable.FindByName<SfComboBox>("productInfo");
            if (this.productInfo != null)
            {
                this.productInfo.SelectionChanged += this.OnProductInfoSelectionChanged;
            }

            this.smartFillButton = bindable.FindByName<SfButton>("smartFillButton");
            if (this.smartFillButton != null)
            {
                this.smartFillButton.Clicked += this.OnSmartFillButtonClicked;
            }
        }

        protected override void OnDetachingFrom(Grid bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.productInfo != null)
            {
                this.productInfo.SelectionChanged -= this.OnProductInfoSelectionChanged;
            }

            if (this.smartFillButton != null)
            {
                this.smartFillButton.Clicked -= this.OnSmartFillButtonClicked;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to handle the smart paste button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnSmartFillButtonClicked(object? sender, EventArgs e)
        {
            this.UpdateOfflineSmartFillDataForm();
            if (this.AIPopupWindow == null)
            {
                return;
            }

            this.AIPopupWindow.IsOpen = false;
        }

        /// <summary>
        /// Method to handle the product details menu selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The selection changed event args.</param>
        private void OnProductInfoSelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.inputData == null || this.productInfo == null)
            {
                return;
            }

            int index = this.productInfo!.SelectedIndex;
            string finalResponse = this.feedbackFormModel.FeedbackForms[index];
            //// Deserialize the JSON string to a Dictionary
            var openAIJsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(finalResponse);
            //// Create lists to hold field values.
            var fieldValues = new List<string>();
            foreach (var data in openAIJsonData!)
            {
                fieldValues.Add(data.Value?.ToString() ?? string.Empty);
            }

            this.inputData.Text = fieldValues[5];
        }


        /// <summary>
        /// Method to fill the data form with the selected product details.
        /// </summary>
        private void UpdateOfflineSmartFillDataForm()
        {
            int index = this.productInfo!.SelectedIndex;
            FeedbackFormModel feedbackFormModel = new FeedbackFormModel();
            string finalResponse = feedbackFormModel.FeedbackForms[index];
            //// Deserialize the JSON string to a Dictionary
            var openAIJsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(finalResponse);
            //// Create lists to hold field names and values
            var filedNames = new List<string>();
            var fieldValues = new List<string>();
            foreach (var data in openAIJsonData!)
            {
                filedNames.Add(data.Key);
                fieldValues.Add(data.Value?.ToString() ?? string.Empty);
            }

            if (this.DataForm!.DataObject is FeedBackForm feedbackForm)
            {
                feedbackForm.Name = fieldValues[0];
                feedbackForm.Email = fieldValues[1];
                feedbackForm.ProductName = fieldValues[2];
                feedbackForm.ProductVersion = fieldValues[3];
                feedbackForm.Rating =(int)Math.Round(double.Parse(fieldValues[4]));
                feedbackForm.Comments = fieldValues[5];
            };

            for (int i = 0; i < filedNames.Count; i++)
            {
                this.DataForm!.UpdateEditor(filedNames[i]);
            }
        }

        #endregion
    }
}
