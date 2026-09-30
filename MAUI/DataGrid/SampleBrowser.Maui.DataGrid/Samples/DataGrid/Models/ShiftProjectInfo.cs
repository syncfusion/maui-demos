using System.ComponentModel;

namespace SampleBrowser.Maui.DataGrid
{
    public class ShiftProjectInfo : INotifyPropertyChanged
    {
        private string? projectId;
        private string? projectName;
        private string? customer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? ProjectId
        {
            get => projectId;
            set
            {
                projectId = value;
                OnPropertyChanged(nameof(ProjectId));
            }
        }

        public string? ProjectName
        {
            get => projectName;
            set
            {
                projectName = value;
                OnPropertyChanged(nameof(ProjectName));
            }
        }

        public string? Customer
        {
            get => customer;
            set
            {
                customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
