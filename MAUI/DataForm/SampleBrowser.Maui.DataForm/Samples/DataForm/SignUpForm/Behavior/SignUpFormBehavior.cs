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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SignUpFormBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        ///  Holds the sign up button instance.
        /// </summary>
        private Button? signUpButton;

        /// <summary>
        ///  Holds the cancel button instance.
        /// </summary>
        private Button? cancelButton;

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("signUpForm");
            if (dataForm != null)
            {
                dataForm.ColumnCount = 2;
                dataForm.ItemsSourceProvider = new ItemsSourceProvider();
                dataForm.RegisterEditor("Country", DataFormEditorType.AutoComplete);
                dataForm.RegisterEditor("MobileNumber", new NumericEditor(dataForm));
                dataForm.RegisterEditor("ZipCode", new NumericEditor(dataForm));
                this.dataForm.GenerateDataFormItem += this.OnGenerateDataFormItem;
            }

            this.cancelButton = bindable.Content.FindByName<Button>("cancelButton");
            this.signUpButton = bindable.Content.FindByName<Button>("signUpButton");

            if (this.cancelButton != null)
            {
                this.cancelButton.Clicked += OnCancelButtonClicked;
            }

            if (this.signUpButton != null)
            {
                this.signUpButton.Clicked += OnSignUpButtonClicked;
            }
        }

        /// <summary>
        /// Invokes on data form item generation.
        /// </summary>
        /// <param name="sender">Te data form</param>
        /// <param name="e">The event arguments.</param>
        private void OnGenerateDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
#if WINDOWS || MACCATALYST

                if (e.DataFormItem.FieldName == nameof(SignUpFormModel.FirstName) || e.DataFormItem.FieldName == nameof(SignUpFormModel.LastName)
                    || e.DataFormItem.FieldName == nameof(SignUpFormModel.Email) || e.DataFormItem.FieldName == nameof(SignUpFormModel.MobileNumber)
                    || e.DataFormItem.FieldName == nameof(SignUpFormModel.Password) || e.DataFormItem.FieldName == nameof(SignUpFormModel.RetypePassword))
                {
                    e.DataFormItem.ColumnSpan = 1;
                }
                else if (e.DataFormItem.FieldName == nameof(SignUpFormModel.City) || e.DataFormItem.FieldName == nameof(SignUpFormModel.State))
                {
                    e.Cancel = true;
                }
#endif

                if (e.DataFormItem.FieldName == nameof(SignUpFormModel.Country) && e.DataFormItem is DataFormAutoCompleteItem autoComplete)
                {
                    autoComplete.MaxDropDownHeight = 300;
                }
                else if (e.DataFormItem.FieldName == nameof(SignUpFormModel.Email) && e.DataFormItem is DataFormTextEditorItem textItem)
                {
                    textItem.Keyboard = Keyboard.Email;
                }
            }
        }

        /// <summary>
        /// Invokes on sign up button click.
        /// </summary>
        /// <param name="sender">The sign up button.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnSignUpButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Signed up successfully", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        /// <summary>
        /// Invokes on cancel button click.
        /// </summary>
        /// <param name="sender">The cancel button.</param>
        /// <param name="e">The event arguments.</param>
        private void OnCancelButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null)

            {
                this.dataForm.DataObject = new SignUpFormModel();
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.cancelButton != null)
            {
                this.cancelButton.Clicked -= OnCancelButtonClicked;
            }

            if (this.signUpButton != null)
            {
                this.signUpButton.Clicked -= OnSignUpButtonClicked;
            }

            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= this.OnGenerateDataFormItem;
            }
        }
    }
}
