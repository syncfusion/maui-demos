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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SignUpFormModel : IDataErrorInfo
    {
        #region Constructor
        public SignUpFormModel()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.RetypePassword = string.Empty;
            this.Address = string.Empty;
            this.City = string.Empty;
            this.State = string.Empty;
            this.Country = string.Empty;
            this.MobileNumber = string.Empty;
            this.ZipCode = string.Empty;
        }

        #endregion

        #region Property

        [Display(Prompt = "Enter your first name", Name = "First name")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(20, ErrorMessage = "First name should not exceed 20 characters")]
        public string FirstName { get; set; }

        [Display(Prompt = "Enter your last name", Name = "Last name")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(20, ErrorMessage = "First name should not exceed 20 characters")]
        public string LastName { get; set; }

        [Display(Prompt = "Enter your email", Name = "Email")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [EmailAddress(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Display(Prompt = "Enter your number", Name = "Mobile number")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Please enter a valid number")]
        public string MobileNumber { get; set; }

        [Display(Prompt = "Enter your password", Name = "Password")]
        [DataType(DataType.Password)]
        [DataFormDisplayOptions(ColumnSpan = 2, ValidMessage = "Password strength is good")]
        [Required(ErrorMessage = "Please enter the password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{8,}$", ErrorMessage = "A minimum 8-character password should contain a combination of uppercase and lowercase letters.")]
        public string Password { get; set; }

        [Display(Prompt = "Confirm password", Name = "Re-enter Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the password")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string RetypePassword { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Prompt = "Enter your address", Name = "Address")]
        [DataFormDisplayOptions(ColumnSpan = 2, RowSpan = 2)]
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [Display(Prompt = "Enter your city", Name = "City")]
        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Display(Prompt = "Enter your state", Name = "State")]
        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }

        [Display(Prompt = "Enter your country", Name = "Country")]
        [Required(ErrorMessage = "Please select your country")]
        public string Country { get; set; }

        [Display(Prompt = "Enter zip code", Name = "Zip code")]
        [Required(ErrorMessage = "Please enter your zip code")]
        public string ZipCode { get; set; }

        [Display(AutoGenerateField = false)]
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        [Display(AutoGenerateField = false)]
        public string this[string name]
        {
            get
            {
                string result = string.Empty;
                if (name == nameof(RetypePassword) && this.Password != this.RetypePassword)
                {
                    result = string.IsNullOrEmpty(this.RetypePassword) ? string.Empty : "The passwords do not match";
                }

                return result;
            }
        }

        #endregion

    }
}
