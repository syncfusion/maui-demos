#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using Microsoft.Maui;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.DataForm;
    using Syncfusion.Maui.Popup;

    /// <summary>
    /// Represents a behavior class for event registration sample.
    /// </summary>
    public class EventRegistrationBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the clear button instance.
        /// </summary>
        private Button? clearButton;

        /// <summary>
        /// Holds the submit button instance.
        /// </summary>
        private Button? submitButton;

        /// <summary>
        /// Holds the popup object.
        /// </summary>
        private SfPopup? popup;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("eventForm");
            this.popup = bindable.Content.FindByName<SfPopup>("popup");

            if (this.popup != null)
            {
                popup.FooterTemplate = DataFormSampleHelper.GetFooterTemplate(popup);
            }

            if (this.dataForm != null)
            {
                this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Signature), new SignaturePadEditor());
                this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Event), DataFormEditorType.Segment);
                this.dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
            }

            this.clearButton = bindable.Content.FindByName<Button>("clearButton");
            this.submitButton = bindable.Content.FindByName<Button>("submitButton");

            if (this.clearButton != null)
            {
                this.clearButton.Clicked += OnClearButtonClicked;
            }

            if (this.submitButton != null)
            {
                this.submitButton.Clicked += OnSubmitButtonClicked;
            }
        }

        /// <summary>
        /// Invokes on clear button click.
        /// </summary>
        /// <param name="sender">The clear button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnClearButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null)
            {
                this.dataForm.DataObject = new EventRegistrationModel();   
            }
        }

        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        private void OnSubmitButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm == null || this.popup == null)
            {
                return;
            }

            EventRegistrationModel eventRegistration = (EventRegistrationModel)this.dataForm.DataObject;
            popup.HeaderTitle = "Hi " + eventRegistration.FirstName;
            if (this.dataForm.Validate())
            {
                popup.ShowHeader = true;
                popup.Message = "You have registered for the " + eventRegistration.Event + " event";
                popup.HeaderHeight = 65;
                this.UpdateContentTemplate(true);
            }
            else
            {
                popup.ShowHeader = false;
                popup.Message = "Please enter the required details";
                this.UpdateContentTemplate(false);
            }

            popup.Show();
        }

        /// <summary>
        /// Invokes on each data form item generation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private void OnGenerateDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Event) && e.DataFormItem is DataFormSegmentItem segmentItem)
                {
                    List<string> events = new List<string>
                    {
                        "Hackathon",
                        "Innovation program",
                        "Tech Spotlight"
                    };

                    segmentItem.ItemsSource = events;
#if ANDROID || IOS
                    segmentItem.VisibleCount = 2;
#endif
                }
                else if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Mobile) && e.DataFormItem is DataFormMaskedTextItem mobile)
                {
                    mobile.Mask = "(###) ###-####";
#if !ANDROID
                    mobile.Keyboard = Keyboard.Numeric;
#endif
                }
                else if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.EventDate) && e.DataFormItem is DataFormDateItem dateItem)
                {
                    dateItem.MinimumDisplayDate = DateTime.Now.Date;
                }
                else if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Signature))
                {
#if WINDOWS || MACCATALYST
                    e.DataFormItem.LabelTextStyle = new DataFormTextStyle { FontSize = 14 };
#else
                    e.DataFormItem.LabelTextStyle = new DataFormTextStyle { FontSize = 12 };
#endif
                }
            }
        }

        /// <summary>
        /// Method to update the content template.
        /// </summary>
        /// <param name="isValidate">Check whether is validate.</param>
        private void UpdateContentTemplate(bool isValidate)
        {
            if (this.popup != null && this.dataForm != null)
            {
                popup.ContentTemplate = DataFormSampleHelper.GetContentTemplate(popup, isValidate);
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView grid)
        {
            base.OnDetachingFrom(grid);
            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= OnGenerateDataFormItem;
            }

            if (this.clearButton != null)
            {
                this.clearButton.Clicked -= OnClearButtonClicked;
            }

            if (this.submitButton != null)
            {
                this.submitButton.Clicked -= OnSubmitButtonClicked;
            }
        }
    }
}