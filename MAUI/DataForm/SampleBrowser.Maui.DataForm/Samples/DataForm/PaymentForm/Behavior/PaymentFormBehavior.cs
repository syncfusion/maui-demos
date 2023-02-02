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

    internal class PaymentFormBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private SfDataForm? dataForm;

        /// <summary>
        /// Holds the submit button instance.
        /// </summary>
        private Button? submitButton;

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("paymentForm");
            if (this.dataForm != null)
            {
                this.dataForm.DefaultLayoutSettings.LabelPosition = DataFormLabelPosition.Top;
                this.dataForm.RegisterEditor(nameof(PaymentFormModel.Month), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor(nameof(PaymentFormModel.Year), DataFormEditorType.ComboBox);
                this.dataForm.RegisterEditor(nameof(PaymentFormModel.Amount), new NumericEditor(dataForm));
                this.dataForm.RegisterEditor(nameof(PaymentFormModel.CardNumber), new NumericEditor(dataForm));
#if ANDROID
                this.dataForm.RegisterEditor(nameof(PaymentFormModel.CVV), DataFormEditorType.Password);
#else
                this.dataForm.RegisterEditor(nameof(PaymentFormModel.CVV), new NumericEditor(dataForm));
#endif
                this.dataForm.ItemsSourceProvider = new ItemsSourceProvider();
                this.dataForm.GenerateDataFormItem += this.OnGeneratingDataFormItem;
            }

            this.submitButton = bindable.Content.FindByName<Button>("Submit");
            if (this.submitButton != null)
            {
                this.submitButton.Clicked += OnSubmitButtonClicked;
            }
        }

        /// <summary>
        /// Invokes on submit button click.
        /// </summary>
        /// <param name="sender">The submit button.</param>
        /// <param name="e">The event arguments.</param>
        private async void OnSubmitButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    await App.Current.MainPage.DisplayAlert("", "Payment Successful", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
            }
        }

        /// <summary>
        /// Invokes on data form item generation.
        /// </summary>
        /// <param name="sender">The DataForm</param>
        /// <param name="e">The event arguments.</param>
        private void OnGeneratingDataFormItem(object? sender, GenerateDataFormItemEventArgs e)
        {
            if (e.DataFormItem != null)
            {
                if (e.DataFormItem.FieldName == nameof(PaymentFormModel.CVV) && e.DataFormItem is DataFormPasswordItem textItem)
                {
                    textItem.Keyboard = Keyboard.Numeric;
                    textItem.MaxLength = 3;
                }
                else if ((e.DataFormItem.FieldName == nameof(PaymentFormModel.Month) || e.DataFormItem.FieldName == nameof(PaymentFormModel.Year)) && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
                {
                    comboBoxItem.MaxDropDownHeight = 200;
                }
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.dataForm != null)
            {
                this.dataForm.GenerateDataFormItem -= this.OnGeneratingDataFormItem;
            }

            if (this.submitButton != null)
            {
                this.submitButton.Clicked -= OnSubmitButtonClicked;
            }
        }
    }
}