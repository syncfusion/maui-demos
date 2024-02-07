#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.dataForm = bindable.Content.FindByName<SfDataForm>("eventForm");

            if (this.dataForm != null)
            {
                this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Signature), new SignaturePadEditor());
                this.dataForm.RegisterEditor(nameof(EventRegistrationModel.Event), DataFormEditorType.ComboBox);
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
        private async void OnSubmitButtonClicked(object? sender, EventArgs e)
        {
            if (this.dataForm != null && App.Current?.MainPage != null)
            {
                if (this.dataForm.Validate())
                {
                    EventRegistrationModel eventRegistration = (EventRegistrationModel)this.dataForm.DataObject;
                    await App.Current.MainPage.DisplayAlert("Hi " + eventRegistration.FirstName, "You have registered for the " + eventRegistration.Event + " event"  , "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Please enter the required details", "OK");
                }
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
                if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Event) && e.DataFormItem is DataFormComboBoxItem comboBoxItem)
                {
                    List<string> events = new List<string>
                    {
                        "Hackathon",
                        "Innovation program",
                        "Tech Spotlight"
                    };

                    comboBoxItem.ItemsSource = events;
                }
                else if (e.DataFormItem.FieldName == nameof(EventRegistrationModel.Mobile) && e.DataFormItem is DataFormMaskedTextItem mobile)
                {
                    mobile.Mask = "(###) ###-####";
                    mobile.Keyboard = Keyboard.Numeric;
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