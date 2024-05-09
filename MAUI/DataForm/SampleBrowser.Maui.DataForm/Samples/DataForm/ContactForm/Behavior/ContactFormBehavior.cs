#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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

    /// <summary>
    /// Represents a behavior class for contact form.
    /// </summary>
    public class ContactFormBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the save button instance.
        /// </summary>
        private Button? saveButton;

        /// <summary>
        /// Holds the popup object.
        /// </summary>
        private SfPopup? popup;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("contactForm");
            this.popup = bindable.Content.FindByName<SfPopup>("popup");

            if (this.popup != null)
            {
                popup.FooterTemplate = DataFormSampleHelper.GetFooterTemplate(popup);
                popup.ContentTemplate = DataFormSampleHelper.GetContentTemplate(popup);
            }

            if (this.dataForm != null)
            {
                dataForm.ItemsSourceProvider = new ItemsSourceProvider();
                dataForm.ValidateForm += this.OnDataFormValidateForm;
                dataForm.ValidateProperty += this.OnDataFormValidateProperty;
            }

            this.saveButton = bindable.Content.FindByName<Button>("saveButton");
            if (this.saveButton != null)
            {
                this.saveButton.Clicked += OnSaveButtonClicked;
            }
        }

        /// <summary>
        /// Invokes on data form item validation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDataFormValidateProperty(object? sender, DataFormValidatePropertyEventArgs e)
        {
            if (e.PropertyName == nameof(ContactFormModel.Mobile) && !e.IsValid)
            {
                e.ErrorMessage = e.NewValue == null || string.IsNullOrEmpty(e.NewValue.ToString()) ? "Please enter the mobile number" : "Invalid mobile number";
            }
        }

        /// <summary>
        /// Invokes on manual data form validation.
        /// </summary>
        /// <param name="sender">The data form.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDataFormValidateForm(object? sender, DataFormValidateFormEventArgs e)
        {
            if (this.popup == null)
            {
                return;
            }

            if (this.dataForm != null)
            {
                if (e.ErrorMessage != null && e.ErrorMessage.Count > 0)
                {
                    if (e.ErrorMessage.Count == 2)
                    {
                        popup.Message = "Please enter the contact name and mobile number";
                    }
                    else
                    {
                        if (e.ErrorMessage.ContainsKey(nameof(ContactFormModel.Name)))
                        {
                            popup.Message = "Please enter the contact name";
                        }
                        else
                        {
                            popup.Message = "Please enter the mobile number";
                        }
                    }
                }
                else
                {
                    popup.Message = "Contact saved";
                }
            }

            popup.Show();
        }

        /// <summary>
        /// Invokes on save button click.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSaveButtonClicked(object? sender, EventArgs e)
        {
            this.dataForm?.Validate();
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.dataForm != null)
            {
                this.dataForm.ValidateForm -= this.OnDataFormValidateForm;
                this.dataForm.ValidateProperty -= this.OnDataFormValidateProperty;
                this.dataForm = null;
            }

            if (this.saveButton != null)
            {
                this.saveButton.Clicked -= OnSaveButtonClicked;
                this.saveButton = null;
            }
        }
    }
}