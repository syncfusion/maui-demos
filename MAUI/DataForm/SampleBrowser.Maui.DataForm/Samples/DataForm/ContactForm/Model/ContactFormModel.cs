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
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents the model class for contact form.
    /// </summary>
    public class ContactFormModel
    {
        public ContactFormModel()
        {
            this.ProfileImage = string.Empty;
            this.Name = string.Empty;
            this.LastName = string.Empty;
            this.Address = string.Empty;
            this.City = string.Empty;
            this.State = string.Empty;
            this.Email = string.Empty;
            this.Mobile = string.Empty;
            this.Landline = string.Empty;
            this.ZipCode = string.Empty;
        }

        [DataFormDisplayOptions(ColumnSpan = 2, ShowLabel = false)]
        public string ProfileImage { get; set; }

        [Display(ShortName = "First name")]
        [Required(ErrorMessage = "Name should not be empty")]
        public string Name { get; set; }

        [Display(ShortName = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Invalid phone number")]
        public string Mobile { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Landline { get; set; }

        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string Address { get; set; }

        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string City { get; set; }

        public string State { get; set; }

        [Display(ShortName = "Zip code")]
        public string ZipCode { get; set; }

        public string Email { get; set; }

    }
}