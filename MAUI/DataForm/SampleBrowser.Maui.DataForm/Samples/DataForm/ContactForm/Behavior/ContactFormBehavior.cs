#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.DataForm;

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

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("contactForm");

            if (this.dataForm != null)
            {
                dataForm.RegisterEditor(nameof(ContactFormModel.Mobile), new NumericEditor(dataForm));
                dataForm.RegisterEditor(nameof(ContactFormModel.ZipCode), new NumericEditor(dataForm));
                dataForm.RegisterEditor(nameof(ContactFormModel.Landline), new NumericEditor(dataForm));
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
        private async void OnDataFormValidateForm(object? sender, DataFormValidateFormEventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (e.ErrorMessage != null && e.ErrorMessage.Count > 0)
                {
                    if (e.ErrorMessage.Count == 2)
                    {
                        await App.Current.MainPage.DisplayAlert("", "Please enter the contact name and mobile number", "OK");
                    }
                    else
                    {
                        if (e.ErrorMessage.ContainsKey(nameof(ContactFormModel.Name)))
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please enter the contact name", "OK");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please enter the mobile number", "OK");
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Contact saved", "OK");
                }
            }
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