using Syncfusion.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    public class SignInFormModel
    {
        #region Constructor
        public SignInFormModel()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
        }

        #endregion

        #region Property

        [Display(Prompt = "example@mail.com", Name = "Email")]
        [DataFormDisplayOptions(ColumnSpan = 3)]
        [EmailAddress(ErrorMessage = "Enter your email")]
        public string? UserName { get; set; }

        [Display(Prompt = "Enter your password", Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter the password")]
        [DataFormDisplayOptions(ColumnSpan = 3)]
        public string? Password { get; set; }

        #endregion       
    }
}
