#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Picker.SfPicker
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object> dataSource = new ObservableCollection<object>()
        {
            "Pink", "Green", "Blue", "Yellow", "Orange", "Purple", "SkyBlue", "PaleGreen", "Gray", "LiteGreen", "Brown"
        };

        private Brush selectionColor = Colors.Orange;

        public ObservableCollection<object> DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
                RaisePropertyChanged("DataSource");
            }
        }

        public Brush SelectionColor
        {
            get
            {
                return selectionColor;
            }
            set
            {
                selectionColor = value;
                RaisePropertyChanged("SelectionColor");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModel()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}