#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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
        }

        #endregion

        #region Property

        [Display(Name = "Amount", Prompt = "Maximum limit is $10,000")]
        [DataFormDisplayOptions(ColumnSpan = 3)]
        [Required(ErrorMessage = "Please enter the amount")]
        [Range(1.0, 10000.0, ErrorMessage = "Invalid Amount")]
        public double? Amount { get; set; }

        [Display(Name = "Name", Prompt = "Name on your card")]
        [DataFormDisplayOptions(ColumnSpan = 3)]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Display(Name = "Card Number", Prompt = "Enter 16-digit card number")]
        [DataFormDisplayOptions(ColumnSpan = 3)]
        [Required(ErrorMessage = "Please enter your card number")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{16}$", ErrorMessage = "Invalid card number")]
        public double? CardNumber { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "Please enter the CVV")]
        [DataFormDisplayOptions(ColumnSpan = 1)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{3}$", ErrorMessage = "Invalid CVV")]
        public string CVV { get; set; }

        [Display(Name = "Month")]
        [DataFormDisplayOptions(ColumnSpan = 1)]
        [Required(ErrorMessage = "Please select a month")]
        public string Month { get; set; }

        [Display(Name = "Year")]
        [DataFormDisplayOptions(ColumnSpan = 1)]
        [Required(ErrorMessage = "Please select a year")]
        public string Year { get; set; }

        #endregion
    }
}
