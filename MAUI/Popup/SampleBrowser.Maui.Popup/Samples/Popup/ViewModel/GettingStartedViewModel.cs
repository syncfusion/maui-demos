#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
        public ICommand RintoneSelectionCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

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

            ShowPopup = new Command<Syncfusion.Maui.Popup.SfPopup>(OnPopupOpened);
            FullScreenCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnFullScreen);
            NotificationCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnNotificationAction);           
            AcceptCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnAcceptAction);
            DeclineCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnDeclineAction);
            ShareCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnShare);
            UploadCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnUpload);
            CopyCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnCopy);
            PrintCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnPrint);
            DeleteCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnDelete);
            SignUpCommand = new Command<Syncfusion.Maui.Popup.SfPopup>(OnSignUp);
            SelectionCommand = new Command(OnSelectionChanged);
            RintoneSelectionCommand = new Command(OnRintoneSelectionCommand);
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

        private void OnPopupOpened(Syncfusion.Maui.Popup.SfPopup popup)
        {
            popup.Show();
        }

        private void OnFullScreen(Syncfusion.Maui.Popup.SfPopup popup)
        {
            popup.Show(true);
        }

        private void OnNotificationAction(Syncfusion.Maui.Popup.SfPopup popup)
        {
#if ANDROID || IOS
            popup.StartY = 0;
#elif WINDOWS
            popup.RelativePosition = Syncfusion.Maui.Popup.PopupRelativePosition.AlignBottomRight;
#else
            popup.RelativePosition = Syncfusion.Maui.Popup.PopupRelativePosition.AlignTopRight;
#endif
            popup.Show();
        }

        private void OnAcceptAction(Syncfusion.Maui.Popup.SfPopup popup)
        {
            popup.IsOpen = false;
        }
      
        private void OnDeclineAction(Syncfusion.Maui.Popup.SfPopup popup)
        {
            popup.IsOpen = false;
        }

        private void OnRintoneSelectionCommand(object obj)
        {
            foreach(var item in RingtoneList)
                item.SelectedRingtone = false;
            (obj as ConfirmationModel)!.SelectedRingtone = true;
        }

        private void OnSelectionChanged(object obj)
        {
            foreach (var item in UserDetails)
                item.IsSelected = false;
            (obj as SimpleModel)!.IsSelected = true;
        }        

        private void OnShare(Syncfusion.Maui.Popup.SfPopup popup)
        {           
            this.ToastText = "File shared";
            OnFileAction(popup);
        }

        private void OnUpload(Syncfusion.Maui.Popup.SfPopup popup)
        {                     
            this.ToastText = "File uploaded";
            OnFileAction(popup);
        }

        private void OnCopy(Syncfusion.Maui.Popup.SfPopup popup)
        {                      
            this.ToastText = "File copied";
            OnFileAction(popup);
        }

        private void OnPrint(Syncfusion.Maui.Popup.SfPopup popup)
        {                       
            this.ToastText = "File printed";
            OnFileAction(popup);
        }

        private void OnDelete(Syncfusion.Maui.Popup.SfPopup popup)
        {          
            this.ToastText = "File deleted";
            OnFileAction(popup);
        }

        private void OnSignUp(Syncfusion.Maui.Popup.SfPopup popup)
        {
            FullScreenModel = new FullScreenModel();
            popup.IsOpen = false;
        }

        private async void OnFileAction(Syncfusion.Maui.Popup.SfPopup popup)
        {
            popup.IsOpen = false;
            this.IsToastVisible = true;
            await Task.Delay(1000);         
            this.IsToastVisible = false;
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
