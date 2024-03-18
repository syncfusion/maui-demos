#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using Syncfusion.Maui.DataForm;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PaymentFormModel
    {
        #region Constructor

        public PaymentFormModel()
        {
            this.Name = string.Empty;
            this.CVV = string.Empty;
            this.Month = string.Empty;
            this.Year = string.Empty;
            this.Amount = string.Empty;
            this.CardNumber = string.Empty;
        }

        #endregion

        #region Property

        [DataType(DataType.Currency)]
        [Display(Name = "Amount", Prompt = "Maximum limit is $10,000")]
        [DataFormDisplayOptions(ColumnSpan = 4)]
        [Range(1.0, 10000.0, ErrorMessage = "Invalid Amount")]
        public string Amount { get; set; }

        [Display(Name = "Name", Prompt = "Name on your card")]
        [DataFormDisplayOptions(ColumnSpan = 4)]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [DataType(DataType.CreditCard)]
        [Display(Name = "Card Number", Prompt = "Enter 16-digit card number")]
        [DataFormDisplayOptions(ColumnSpan = 3)]
        [RegularExpression(@"^(?:\d{4}[-\s]?){3}\d{4}$", ErrorMessage = "Invalid Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "CVV")]
        [DataFormDisplayOptions(ColumnSpan = 1)]
        public string CVV { get; set; }

        [Display(Name = "Month")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Required(ErrorMessage = "Please select a month")]
        public string Month { get; set; }

        [Display(Name = "Year")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Required(ErrorMessage = "Please select a year")]
        public string Year { get; set; }

        #endregion
    }
}
