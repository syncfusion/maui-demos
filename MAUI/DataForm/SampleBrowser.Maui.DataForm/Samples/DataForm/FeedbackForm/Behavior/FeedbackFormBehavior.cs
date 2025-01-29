#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.DataForm;
    using Syncfusion.Maui.Popup;
    using System;

    internal class FeedbackFormBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the submit button instance.
        /// </summary>
        private Button? sendButton;

        /// <summary>
        /// Holds the popup object.
        /// </summary>
        private SfPopup? popup;

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("feedbackForm");
            this.popup = bindable.Content.FindByName<SfPopup>("popup");

            if (this.popup != null)
            {
                popup.FooterTemplate = DataFormSampleHelper.GetFooterTemplate(popup);
                popup.ContentTemplate = DataFormSampleHelper.GetContentTemplate(popup);
            }

            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem += OnGenerateDataFormItem;
            }

            this.sendButton = bindable.Content.FindByName<Button>("Send");
            if (this.sendButton != null)
            {
                this.sendButton.Clicked += OnSubmitButtonClicked;
            }
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
                if (e.DataFormItem.FieldName == nameof(FeedbackFormModel.Message))
                {
#if WINDOWS || MACCATALYST
                    e.DataFormItem.RowSpan = 2;
#else
                    e.DataFormItem.RowSpan = 3;
#endif
                }
                else if (e.DataFormItem.FieldName == nameof(FeedbackFormModel.Email) && e.DataFormItem is DataFormTextEditorItem textItem)
                {
                    textItem.Keyboard = Keyboard.Email;
                }
                else if (e.DataFormItem.FieldName == nameof(FeedbackFormModel.Mobile) && e.DataFormItem is DataFormMaskedTextItem mobile)
                {
                    mobile.Mask = "(###) ###-####";
                    mobile.Keyboard = Keyboard.Numeric;
                }
            }
        }

        /// <summary>
        /// Invokes on send button click.
        /// </summary>
        /// <param name="sender">The send button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSubmitButtonClicked(object? sender, EventArgs e)
        {
            if (this.popup == null)
            {
                return;
            }

            if (this.dataForm != null)
            {
                if (this.dataForm.Validate())
                {
                    popup.Message = "Feedback sent successfully";
                }
                else
                {
                    popup.Message = "Please enter the required details";
                }

            }

            popup.Show();
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.sendButton != null)
            {
                this.sendButton.Clicked -= OnSubmitButtonClicked;
            }

            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= OnGenerateDataFormItem;
            }
        }
    }
}
