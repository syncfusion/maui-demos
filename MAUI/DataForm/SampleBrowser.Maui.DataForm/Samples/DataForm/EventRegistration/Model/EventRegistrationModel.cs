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
    /// Represents the model class for event registration.
    /// </summary>
    public class EventRegistrationModel
    {
        public EventRegistrationModel()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Event = string.Empty;
        }

        [Display(Name = "Name", Prompt = "First name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string FirstName { get; set; }

        [DataFormDisplayOptions(ShowLabel = false)]
        [Display(Prompt = "Last name")]
        public string LastName { get; set; }
     
        [Display(Name ="Mobile", Prompt = "Mobile number")]
        [StringLength(10, MinimumLength =6, ErrorMessage = "Please enter a valid number")]
        public double? Mobile { get; set; }

        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event or Activity", Prompt = "Select an event")]
        [DataFormDisplayOptions(ColumnSpan = 2)]
        [Required(ErrorMessage = "Please select an event")]
        public string Event { get; set; }

        [DataFormDisplayOptions(ColumnSpan = 2, ShowLabel = false)]
        public ImageSource? Signature { get; set; }
    }
}