#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
            this.Mobile = string.Empty;
        }

        #endregion

        #region Property

        [Display(Name = "Name", Prompt = "Enter your name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile", Prompt = "Enter your mobile number")]
        [Required(ErrorMessage = "Please enter your mobile number")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Please enter a valid number")]
        public string Mobile { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ShortName ="What did you enjoy most about the training?")]
        public string Message { get; set; }

        #endregion

    }

}
