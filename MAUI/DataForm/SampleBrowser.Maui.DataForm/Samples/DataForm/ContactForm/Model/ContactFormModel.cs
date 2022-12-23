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
            this.GroupName = "Friends";
        }

        [DataFormDisplayOptions(ColumnSpan = 2, ShowLabel = false)]
        public string ProfileImage { get; set; }

        [Display(Prompt = "First name")]
        [Required(ErrorMessage = "Name should not be empty")]
        public string Name { get; set; }

        [Display(Prompt = "Last name")]
        public string LastName { get; set; }

        [Display(Prompt = "Mobile")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public double? Mobile { get; set; }

        [Display(Prompt = "Landline")]
        public double? Landline { get; set; }

        [Display(Prompt = "Address")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string Address { get; set; }

        [Display(Prompt = "City")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        public string City { get; set; }

        [Display(Prompt = "State")]
        public string State { get; set; }

        [Display(Prompt = "Zip code")]
        [DataFormDisplayOptions(ShowLabel = false)]
        public double? ZipCode { get; set; }

        [Display(Prompt = "Email")]
        public string Email { get; set; }

        [Display(Prompt = "Group name")]
        public string GroupName { get; set; }
    }
}