using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleBrowser.Maui.DataGrid
{
    public class EmployeeInfo : INotifyPropertyChanged
    {
        private int _employeeID;
        private string _name = string.Empty;
        private int _rating;
        private int _contactID;
        private string _title = string.Empty;
        private DateOnly _birthDate;
        private string _gender = string.Empty;
        private double _sickLeaveHours;
        private decimal _salary;

        public int EmployeeID
        {
            get => _employeeID;
            set { if (_employeeID != value) { _employeeID = value; OnPropertyChanged(); } }
        }

        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        public int Rating
        {
            get => _rating;
            set { if (_rating != value) { _rating = value; OnPropertyChanged(); } }
        }

        public int ContactID
        {
            get => _contactID;
            set { if (_contactID != value) { _contactID = value; OnPropertyChanged(); } }
        }

        public string Title
        {
            get => _title;
            set { if (_title != value) { _title = value; OnPropertyChanged(); } }
        }

        public DateOnly BirthDate
        {
            get => _birthDate;
            set { if (_birthDate != value) { _birthDate = value; OnPropertyChanged(); } }
        }

        public string Gender
        {
            get => _gender;
            set { if (_gender != value) { _gender = value; OnPropertyChanged(); } }
        }

        public double SickLeaveHours
        {
            get => _sickLeaveHours;
            set { if (_sickLeaveHours != value) { _sickLeaveHours = value; OnPropertyChanged(); } }
        }

        public decimal Salary
        {
            get => _salary;
            set { if (_salary != value) { _salary = value; OnPropertyChanged(); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));
        }
    }
}
