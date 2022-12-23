#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using System.Text.RegularExpressions;
    using Syncfusion.Maui.DataForm;

    /// <summary>
    /// Represents the custom numeric editor class.
    /// </summary>
    public class NumericEditor : IDataFormEditor
    {
        /// <summary>
        /// Holds the data form object.
        /// </summary>
        private Syncfusion.Maui.DataForm.SfDataForm dataForm;

        /// <summary>
        /// Holds the DataForm item object.
        /// </summary>
        private DataFormCustomItem? dataFormCustomItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericEditor"/> class.
        /// </summary>
        /// <param name="dataForm">The data form instance.</param>
        public NumericEditor(Syncfusion.Maui.DataForm.SfDataForm dataForm)
        {
            this.dataForm = dataForm;
        }

        /// <inheritdoc/>
        public View CreateEditorView(DataFormItem dataFormItem)
        {
#if ANDROID
            //// In android entry don't have bottom border, hence used editor.
            Editor inputView = new Editor();
            inputView.MaxLength = 16;
#else
            Entry inputView = new Entry();
            if (dataFormItem.FieldName == "CVV")
            {
                inputView.IsPassword = true;
                inputView.MaxLength = 3;
            }

#if IOS || MACCATALYST
            inputView.PlaceholderColor = dataFormItem.PlaceholderColor;
#endif
#endif
            inputView.Keyboard = Keyboard.Numeric;
            inputView.Placeholder = dataFormItem.PlaceholderText;
            DataFormTextStyle textStyle = dataForm.EditorTextStyle;
            inputView.TextColor = textStyle.TextColor;
            inputView.FontSize = textStyle.FontSize;
            inputView.FontFamily = textStyle.FontFamily;
            inputView.FontAttributes = textStyle.FontAttributes;
            inputView.TextChanged += this.OnViewTextChanged;
            this.dataFormCustomItem = (DataFormCustomItem)dataFormItem;
            this.dataFormCustomItem.EditorValue = string.Empty;
            return inputView;
        }

        /// <inheritdoc/>
        public void CommitValue(DataFormItem dataFormItem, View view)
        {
            if (view is InputView numericText)
            {
                if (dataFormItem.FieldName == nameof(PaymentFormModel.CVV))
                {
                    dataFormItem.SetValue(numericText.Text);
                }
                else
                {
                    double numericValue;
                    double.TryParse(numericText.Text, out numericValue);
                    dataFormItem.SetValue(numericValue);
                }
            }
        }

        /// <summary>
        /// Method validates the view
        /// </summary>
        /// <param name="dataFormItem">The data form item.</param>
        private void ValidateValue(DataFormItem dataFormItem)
        {
            dataForm.Validate(new List<string>() { dataFormItem.FieldName });
        }

        /// <summary>
        /// Invokes on view text changes.
        /// </summary>
        /// <param name="sender">The entry field.</param>
        /// <param name="e">The event arguments.</param>
        private void OnViewTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is not InputView numericEntry || dataFormCustomItem == null)
            {
                return;
            }

            string? numericText = Regex.Replace(numericEntry.Text, "[^0-9]+", string.Empty);
            if (numericText != numericEntry.Text)
            {
                numericEntry.Text = numericText;
                return;
            }

            dataFormCustomItem.EditorValue = numericText;
            this.ValidateValue(dataFormCustomItem);
            this.CommitValue(dataFormCustomItem, numericEntry);
        }

        public void UpdateReadyOnly(DataFormItem dataFormItem)
        {
        }
    }
}