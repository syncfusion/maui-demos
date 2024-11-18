#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Feedback form model class.
    /// </summary>
    public class FeedbackFormModel
    {
        #region Fields

        /// <summary>
        /// Gets or sets the feedback form.
        /// </summary>
        public FeedBackForm FeedbackForm { get; set; }

        /// <summary>
        /// Gets or sets the feedback forms.
        /// </summary>
        public List<string> FeedbackForms = new List<string>();

        /// <summary>
        /// Gets or sets ObservableCollection to hold product details for dropdown list.
        /// </summary>
        public ObservableCollection<string> ProductDetails { get; set; }

        /// <summary>
        /// Gets or sets the feedback form 1.
        /// </summary>
        public string FeedbackForm1 { get; set; }

        /// <summary>
        /// Gets or sets the feedback form 2.
        /// </summary>
        public string FeedbackForm2 { get; set; }

        /// <summary>
        /// Gets or sets the feedback form 3.
        /// </summary>
        public string FeedbackForm3 { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackFormModel" /> class.
        /// </summary>
        public FeedbackFormModel()
        {
            this.FeedbackForm1 = "My name is John Miller, and I have been using the Calendar Control (version 25.3.1) for the past six months. It’s a reliable tool for managing dates, though I believe it could benefit from additional customization options. Overall, I would rate this product 4 out of 5. For any further details, feel free to reach out to me at johnmiller@gmail.com";
            this.FeedbackForm2 = "My name is Sarah Thompson, and I've been using the Scheduler Control (version 20.1.4) for the past five months. It's a highly effective tool for managing appointments and tasks, though I would appreciate more flexibility in event color-coding. I would rate this product 4.5 out of 5. If you need further information, please contact me at sarahthompson@gmail.com.";
            this.FeedbackForm3 = "My name is Michael Johnson, and I've been using the DataForm Control (version 18.6.7) for the past few months. It's a solid tool for managing form data, though I believe there could be improvements in its validation features. Overall, I would rate this product 4 out of 5. For any further details, feel free to reach out to me at michaeljohnson@gmail.com.";
            this.FeedbackForm = new FeedBackForm();
            //// Initialize the product details dropdown with sample data.
            this.ProductDetails = new ObservableCollection<string>();
            this.ProductDetails.Add("Calendar");
            this.ProductDetails.Add("Scheduler");
            this.ProductDetails.Add("DataForm");
            this.FeedbackForms.Add("{\r\n  \"Name\": \"John Miller\",\r\n  \"Email\": \"johnmiller@gmail.com\",\r\n  \"ProductName\": \"Calendar\",\r\n  \"ProductVersion\": \"25.3.1\",\r\n  \"Rating\": 5,\r\n  \"Comments\": \"My name is John Miller, and I have been using the Calendar Control (version 25.3.1) for the past six months. It’s a reliable tool for managing dates, though I believe it could benefit from additional customization options. Overall, I would rate this product 4 out of 5. For any further details, feel free to reach out to me at johnmiller@gmail.com.\"\r\n}\r\n");
            this.FeedbackForms.Add("{\r\n  \"Name\": \"Sarah Thompson\",\r\n  \"Email\": \"sarahthompson@gmail.com\",\r\n  \"ProductName\": \"Scheduler\",\r\n  \"ProductVersion\": \"20.1.4\",\r\n  \"Rating\": 4.5,\r\n  \"Comments\": \"My name is Sarah Thompson, and I've been using the Scheduler Control (version 20.1.4) for the past five months. It's a highly effective tool for managing appointments and tasks, though I would appreciate more flexibility in event color-coding. I would rate this product 4.5 out of 5. If you need further information, please contact me at sarahthompson@gmail.com.\"\r\n}\r\n");
            this.FeedbackForms.Add("{\r\n  \"Name\": \"Michael Johnson\",\r\n  \"Email\": \"michaeljohnson@gmail.com\",\r\n  \"ProductName\": \"DataForm\",\r\n  \"ProductVersion\": \"18.6.7\",\r\n  \"Rating\": 4,\r\n  \"Comments\": \"My name is Michael Johnson, and I've been using the DataForm Control (version 18.6.7) for the past few months. It's a solid tool for managing form data, though I believe there could be improvements in its validation features. Overall, I would rate this product 4 out of 5. For any further details, feel free to reach out to me at michaeljohnson@gmail.com.\"\r\n}\r\n");
        }

        #endregion
    }

    /// <summary>
    /// Feedback form model class
    /// </summary>
    public class FeedBackForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackForm" /> class.
        /// </summary>
        public FeedBackForm()
        {
            this.Name = string.Empty;
            this.Email = string.Empty;
            this.ProductName = string.Empty;
            this.ProductVersion = string.Empty;
            this.Rating = 0;
            this.Comments = string.Empty;
        }

        [Display(Name = "Name", Prompt = "Enter your name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Display(Name = "Product Name", Prompt = "Example: Scheduler")]
        public string ProductName { get; set; }

        [Display(Name = "Product Version", Prompt = "Example: 26.2.8")]
        public string ProductVersion { get; set; }

        [Display(Name = "Rating", Prompt = "Rating between 1-5")]
        [Range(1, 5, ErrorMessage = "Rating should be between 1 and 5")]
        public int Rating { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(ShortName = "Describe your feedback in detail", Name = "Comments")]
        public string Comments { get; set; }
    }
}
