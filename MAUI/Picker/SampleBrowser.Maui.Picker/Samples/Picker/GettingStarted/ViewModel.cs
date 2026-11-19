using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Picker.SfPicker
{
    public class ViewModel : INotifyPropertyChanged
    {
        private object? selectedItem = "Australia";

        private ObservableCollection<object> dataSource = new ObservableCollection<object>()
        {
            "Pink", "Green", "Blue", "Yellow", "Orange", "Purple", "SkyBlue", "PaleGreen", "Gray", "LiteGreen", "Brown"
        };

        private Brush selectionColor = Colors.Orange;

        public object? SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                RaisePropertyChanged(nameof(this.SelectedItem));
            }
        }

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