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
            this.Mobile = string.Empty;
        }

        [Display(Name = "Name", Prompt = "First name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string FirstName { get; set; }

        [Display(Prompt = "Last name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Mobile", Prompt = "Mobile number")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Please enter a valid number")]
        public string Mobile { get; set; }

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