#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Popup.SfPopup
{
    public class SimpleModel : INotifyPropertyChanged
    {
        #region Fields
        
        private string? userName;       
        private string? mailId;
        private string? image;
        private bool? isSelected;
        
        #endregion

        #region Properties
        
        public string? UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string? MailId
        {
            get { return mailId; }
            set
            {
                mailId = value;
                OnPropertyChanged("MailId");
            }
        }

        public string? Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public bool? IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        #endregion
        
        #region Constructor

        public SimpleModel()
        {

        }
        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    public class ConfirmationModel : INotifyPropertyChanged 
    {
        #region Fields

        private string? ringtone;
        private bool? selectedRingtone;

        #endregion

        #region

        public string? Ringtone
        {
            get { return ringtone; }
            set
            {
                ringtone = value;
                OnPropertyChanged("Ringtone");
            }
        }

        public bool? SelectedRingtone
        {
            get { return selectedRingtone; }
            set
            {
                selectedRingtone = value;
                OnPropertyChanged("SelectedRingtone");
            }
        }

        #endregion

        #region Constructor

        public ConfirmationModel()
        {

        }
        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    public class FullScreenModel : INotifyPropertyChanged
    {
        #region Fields
        private string? userName;
        private string? email;
        private string? password;
        private string? rePassword;
        #endregion

        #region Properties
        [Display(Prompt = "Pick your username")]
        public string? UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        [Display(Prompt = "Enter your email address")]
        public string? Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        
        [Display(Prompt = "Enter your password", Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        
        [Display(Prompt = "Re-enter your password", Name = "RePassword")]
        [DataType(DataType.Password)]
        public string? RePassword
        {
            get { return rePassword; }
            set
            {
                rePassword = value;
                OnPropertyChanged("RePassword");
            }
        }
        #endregion

        #region Constructor
        public FullScreenModel() 
        {
            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
        }
        #endregion

        #region Interface Member
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string name) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
