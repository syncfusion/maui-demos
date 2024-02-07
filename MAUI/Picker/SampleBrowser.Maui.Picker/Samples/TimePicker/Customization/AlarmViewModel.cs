#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfTimePicker
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class AlarmDetails : INotifyPropertyChanged
    {
        private TimeSpan alarmTime;
        private string alarmMessage = string.Empty;
        private bool isAlarmEnabled = true;
        private Color alarmTextColor = Colors.Black;
        private Color alarmSecondaryTextColor = Colors.Grey;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TimeSpan AlarmTime
        {
            get
            {
                return alarmTime;
            }
            set
            {
                alarmTime = value;
                RaisePropertyChanged("AlarmTime");
            }
        }

        public string AlarmMessage
        {
            get
            {
                return alarmMessage;
            }
            set
            {
                alarmMessage = value;
                RaisePropertyChanged("AlarmMessage");
            }
        }

        public bool IsAlarmEnabled
        {
            get
            {
                return isAlarmEnabled;
            }
            set
            {
                isAlarmEnabled = value;
                RaisePropertyChanged("IsAlarmEnabled");
            }
        }

        public Color AlarmTextColor
        {
            get
            {
                return alarmTextColor;
            }
            set
            {
                alarmTextColor = value;
                RaisePropertyChanged("AlarmTextColor");
            }
        }

        public Color AlarmSecondaryTextColor
        {
            get
            {
                return alarmSecondaryTextColor;
            }
            set
            {
                alarmSecondaryTextColor = value;
                RaisePropertyChanged("AlarmSecondaryTextColor");
            }
        }


        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AlarmDetails> alarmCollection;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AlarmDetails> AlarmCollection
        {
            get
            {
                return alarmCollection;
            }
            set
            {
                alarmCollection = value;
                RaisePropertyChanged("AlarmCollection");
            }
        }

        public ViewModel()
        {
            this.alarmCollection = new ObservableCollection<AlarmDetails>()
            {
                new AlarmDetails() { AlarmTime = new TimeSpan(4, 0, 0), AlarmMessage = "Wake Up", IsAlarmEnabled = true, AlarmTextColor = Colors.Black, AlarmSecondaryTextColor=Colors.Gray },
                new AlarmDetails() { AlarmTime = new TimeSpan(5, 0, 0), AlarmMessage = "Morning Workout", IsAlarmEnabled = true, AlarmTextColor = Colors.Black, AlarmSecondaryTextColor=Colors.Gray },
                new AlarmDetails() { AlarmTime = new TimeSpan(13, 0, 0), AlarmMessage = "No Alarm Message", IsAlarmEnabled = false, AlarmTextColor = Colors.Gray, AlarmSecondaryTextColor=Colors.LightGray },
            };
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}