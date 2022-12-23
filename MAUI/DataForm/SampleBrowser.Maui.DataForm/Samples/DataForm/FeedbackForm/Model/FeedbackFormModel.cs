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
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class FeedbackFormModel
    {
        #region Constructor

        public FeedbackFormModel()
        {
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.Message = string.Empty;
        }

        #endregion

        #region Property

        [Display(Name = "Name", Prompt = "Enter your name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Display(Name = "Mobile", Prompt = "Enter your mobile number")]
        [Required(ErrorMessage = "Please enter your mobile number")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10}$", ErrorMessage = "Invalid number")]
        public double? Mobile { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ShortName ="What did you enjoy most about the training?", Prompt = "Enter your feedback here")]
        public string Message { get; set; }

        #endregion

    }

}
