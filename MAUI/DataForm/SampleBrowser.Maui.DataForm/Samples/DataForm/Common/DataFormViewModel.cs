namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    /// <summary>
    /// Represents the view model class for data form.
    /// </summary>
    public class DataFormViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataFormViewModel" /> class.
        /// </summary>
        public DataFormViewModel()
        {
            this.ContactFormModel = new ContactFormModel();
            this.EventRegistrationModel = new EventRegistrationModel();
            this.SignUpFormModel = new SignUpFormModel();
            this.SignInFormModel = new SignInFormModel();
            this.PaymentFormModel = new PaymentFormModel();
            this.FeedbackFormModel = new FeedbackFormModel();
        }

        /// <summary>
        /// Gets or sets the contact form model.
        /// </summary>
        public ContactFormModel ContactFormModel { get; set; }

        /// <summary>
        /// Gets or sets the event registration model.
        /// </summary>
        public EventRegistrationModel EventRegistrationModel { get; set; }

        /// <summary>
        /// Gets or sets the Sign up model.
        /// </summary>
        public SignUpFormModel SignUpFormModel { get; set; }

        /// <summary>
        /// Gets or sets the sign in model.
        /// </summary>
        public SignInFormModel SignInFormModel { get; set; }

        /// <summary>
        /// Gets or sets the payment form model.
        /// </summary>
        public PaymentFormModel PaymentFormModel { get; set; }

        /// <summary>
        /// Gets or sets the feedback form model.
        /// </summary>
        public FeedbackFormModel FeedbackFormModel { get; set; }
    }
}
