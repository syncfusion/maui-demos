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
        }

        [DataFormDisplayOptions(ColumnSpan = 2, ShowLabel = false)]
        public string ProfileImage { get; set; }

        [Display(ShortName = "First name")]
        [Required(ErrorMessage = "Name should not be empty")]
        public string Name { get; set; }

        [Display(ShortName = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public double? Mobile { get; set; }

        public double? Landline { get; set; }

        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string Address { get; set; }

        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string City { get; set; }

        public string State { get; set; }

        [Display(ShortName = "Zip code")]
        public double? ZipCode { get; set; }

        public string Email { get; set; }

    }
}