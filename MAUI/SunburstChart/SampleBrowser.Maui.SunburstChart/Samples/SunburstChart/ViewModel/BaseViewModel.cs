using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleBrowser.Maui.SunburstChart.SfSunburstChart
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;  

        public ObservableCollection<Brush> CustomBrush1 { get; set; }
        public ObservableCollection<Brush> CustomBrush2 { get; set; }
        public ObservableCollection<Brush> CustomBrush3 { get; set; }

        private bool enableAnimation = true;
        public bool EnableAnimation
        {
            get { return enableAnimation; }
            set
            {
                enableAnimation = value;
                OnPropertyChanged("EnableAnimation");
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public BaseViewModel()
        {
            CustomBrush1 = new ObservableCollection<Brush>()
            {
               new SolidColorBrush(Color.FromArgb("#08CDAA")),
               new SolidColorBrush(Color.FromArgb("#06B1E2")),
               new SolidColorBrush(Color.FromArgb("#CDB509")),
               new SolidColorBrush(Color.FromArgb("#F954A3")),
            };

            CustomBrush2 = new ObservableCollection<Brush>()
            {
               new SolidColorBrush(Color.FromArgb("#1089E9")),
               new SolidColorBrush(Color.FromArgb("#08CDAA")),
               new SolidColorBrush(Color.FromArgb("#F954A3")),
               new SolidColorBrush(Color.FromArgb("#F9C200")),
               new SolidColorBrush(Color.FromArgb("#9656FF")),
            };

            CustomBrush3 = new ObservableCollection<Brush>()
            {
               new SolidColorBrush(Color.FromArgb("#D41089E9")),
               new SolidColorBrush(Color.FromArgb("#D408CDAA")),
               new SolidColorBrush(Color.FromArgb("#D49656FF")),
               new SolidColorBrush(Color.FromArgb("#D4CDB509")),
               new SolidColorBrush(Color.FromArgb("#D4F954A3")),
            };
        }
    }
}