#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
#if ANDROID
using Android.OS;
#endif
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

#nullable disable
namespace SampleBrowser.Maui.Popup.SfPopup
{
    public class GettingStartedViewModel : INotifyPropertyChanged
    {
        #region Fields
        
        private ObservableCollection<SimpleModel> userDetails;
        private ObservableCollection<ConfirmationModel> ringtoneList;
        private string toastText;
        private bool isToastVisible;
        private FullScreenModel fullScreenModel;

        private bool isAlertOpen;
        private bool isAlertWithTitleOpen;
        private bool isAlertWithIconOpen;
        private bool isSimpleOpen;
        private bool isConfirmationOpen;
        private bool isModalOpen;
        private bool isFullScreenOpen;
        private bool isNotificationOpen;
        private bool isActionsheetOpen;
        private bool isBlurredOpen;

        #endregion

        #region Properties
        public bool IsBlurButtonVisible { get; set; }
        public FullScreenModel FullScreenModel
        {
            get { return fullScreenModel; }
            set
            {
                fullScreenModel = value;
                OnPropertyChanged("FullScreenModel");
            }
        }

        public bool IsAlertOpen { get => isAlertOpen; set { isAlertOpen = value; OnPropertyChanged(nameof(IsAlertOpen)); } }
        public bool IsAlertWithTitleOpen { get => isAlertWithTitleOpen; set { isAlertWithTitleOpen = value; OnPropertyChanged(nameof(IsAlertWithTitleOpen)); } }
        public bool IsAlertWithIconOpen { get => isAlertWithIconOpen; set { isAlertWithIconOpen = value; OnPropertyChanged(nameof(IsAlertWithIconOpen)); } }
        public bool IsSimpleOpen { get => isSimpleOpen; set { isSimpleOpen = value; OnPropertyChanged(nameof(IsSimpleOpen)); } }
        public bool IsConfirmationOpen { get => isConfirmationOpen; set { isConfirmationOpen = value; OnPropertyChanged(nameof(IsConfirmationOpen)); } }
        public bool IsModalOpen { get => isModalOpen; set { isModalOpen = value; OnPropertyChanged(nameof(IsModalOpen)); } }
        public bool IsFullScreenOpen { get => isFullScreenOpen; set { isFullScreenOpen = value; OnPropertyChanged(nameof(IsFullScreenOpen)); } }
        public bool IsNotificationOpen { get => isNotificationOpen; set { isNotificationOpen = value; OnPropertyChanged(nameof(IsNotificationOpen)); } }
        public bool IsActionsheetOpen { get => isActionsheetOpen; set { isActionsheetOpen = value; OnPropertyChanged(nameof(IsActionsheetOpen)); } }
        public bool IsBlurredOpen { get => isBlurredOpen; set { isBlurredOpen = value; OnPropertyChanged(nameof(IsBlurredOpen)); } }
        public ObservableCollection<SimpleModel> UserDetails
        {
            get { return userDetails; }
            set
            {
                userDetails = value;
                OnPropertyChanged("UserDetails");
            }
        }
        public ObservableCollection<ConfirmationModel> RingtoneList
        {
            get { return ringtoneList; }
            set
            {
                ringtoneList = value;
                OnPropertyChanged("RingtoneList");
            }
        }
        public string ToastText
        {
            get { return toastText; }
            set
            {
                toastText = value;
                OnPropertyChanged("ToastText");
            }
        }
        public bool IsToastVisible
        {
            get { return isToastVisible; }
            set
            {
                isToastVisible = value;
                OnPropertyChanged("IsToastVisible");
            }
        }
        public ICommand ShowPopup { get; set; }
        public ICommand FullScreenCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand UploadCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        public ICommand SelectionCommand { get; set; }
        public ICommand RingtoneSelectionCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public ICommand OpenPopupCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }

        #endregion

        #region Constructor

        public GettingStartedViewModel()
        {
            UserDetails = new ObservableCollection<SimpleModel>()
            {
                new SimpleModel() { UserName = "Michael", MailId = "michael40@sample.com", Image="sebastian.png", IsSelected = true},
                new SimpleModel() { UserName = "Laura", MailId = "laura13@jourrapide.com", Image ="clara.png", IsSelected = false},
                new SimpleModel() { UserName = "Tamara", MailId = "tamara17@rpy.com", Image = "lita.png", IsSelected = false},
            };
            
            RingtoneList = new ObservableCollection<ConfirmationModel>();
            for (int i=0; i< Ringtones.Length; i++)
            {
                var ringtone = new ConfirmationModel()
                {
                    Ringtone = Ringtones[i],
                };

                if(i==0)
                    ringtone.SelectedRingtone = true;
                else
                    ringtone.SelectedRingtone = false;

                RingtoneList.Add(ringtone);
            }

            OpenPopupCommand = new Command<string>(OpenByKey);
            ClosePopupCommand = new Command<string>(CloseByKey);

            ShareCommand = new Command(OnShare);
            UploadCommand = new Command(OnUpload);
            CopyCommand = new Command(OnCopy);
            PrintCommand = new Command(OnPrint);
            DeleteCommand = new Command(OnDelete);
            SignUpCommand = new Command(OnSignUp);

            FullScreenModel = new FullScreenModel();
            this.ToastText = string.Empty;
            this.IsToastVisible = false;
            this.IsBlurButtonVisible = true;
#if ANDROID
            int androidVersion = (int)Build.VERSION.SdkInt;
            if(androidVersion < 31)
            {
                this.IsBlurButtonVisible = false;
            }
#endif
        }

        #endregion

        #region Private Methods

        private void OnShare()
        {           
            this.ToastText = "File shared";
            OnFileAction();
        }

        private void OnUpload()
        {                     
            this.ToastText = "File uploaded";
            OnFileAction();
        }

        private void OnCopy()
        {                      
            this.ToastText = "File copied";
            OnFileAction();
        }

        private void OnPrint()
        {                       
            this.ToastText = "File printed";
            OnFileAction();
        }

        private void OnDelete()
        {          
            this.ToastText = "File deleted";
            OnFileAction();
        }

        private void OnSignUp()
        {
            FullScreenModel.UserName = string.Empty;
            FullScreenModel.Email = string.Empty;
            FullScreenModel.Password = string.Empty;
            FullScreenModel.RePassword = string.Empty;
            IsFullScreenOpen = false;
        }

        private async void OnFileAction()
        {
            // Close actionsheet and show toast.
            IsActionsheetOpen = false;
            this.IsToastVisible = true;
            await Task.Delay(1000);
            this.IsToastVisible = false;
        }

        #endregion

        #region Helpers for MVVM popup control

        private void OpenByKey(string key)
        {
            switch (key)
            {
                case "Alert": IsAlertOpen = true; break;
                case "AlertWithTitle": IsAlertWithTitleOpen = true; break;
                case "AlertWithIcon": IsAlertWithIconOpen = true; break;
                case "Simple": IsSimpleOpen = true; break;
                case "Confirmation": IsConfirmationOpen = true; break;
                case "Modal": IsModalOpen = true; break;
                case "FullScreen": IsFullScreenOpen = true; break;
                case "Notification": IsNotificationOpen = true; break;
                case "Actionsheet": IsActionsheetOpen = true; break;
                case "Blurredbackground": IsBlurredOpen = true; break;
            }
        }

        private void CloseByKey(string key)
        {
            switch (key)
            {
                case "Alert": IsAlertOpen = false; break;
                case "AlertWithTitle": IsAlertWithTitleOpen = false; break;
                case "AlertWithIcon": IsAlertWithIconOpen = false; break;
                case "Simple": IsSimpleOpen = false; break;
                case "Confirmation": IsConfirmationOpen = false; break;
                case "Modal": IsModalOpen = false; break;
                case "FullScreen": IsFullScreenOpen = false; break;
                case "Notification": IsNotificationOpen = false; break;
                case "Actionsheet": IsActionsheetOpen = false; break;
                case "Blurredbackground": IsBlurredOpen = false; break;
            }
        }

        #endregion

        #region Collection

        string[] Ringtones = new string[]
        {
            "Asteroid",
            "Atomic Bell",
            "Beep Once",
            "Beep-Beep",
            "Chime Time",
            "Comet",
            "Cosmos",
            "Finding Galaxy",
            "Galaxy Bells",
            "Homecoming",
            "Moon Discovery",
            "Neptune",
        };

#endregion

#region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

#endregion
    }
}
