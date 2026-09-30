using System;
using System.ComponentModel;

namespace SampleBrowser.Maui.DataGrid
{
    public class EmployeeShiftInfo : INotifyPropertyChanged
    {
        private int employeeId;
        private string? employeeName;
        private string? department;
        private string? workLocation;
        private DateTime joinDate;
        private TimeSpan shiftStartTime;
        private ShiftProjectInfo? assignedProject;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int EmployeeId
        {
            get => employeeId;
            set
            {
                employeeId = value;
                RaisePropertyChanged(nameof(EmployeeId));
            }
        }

        public string? EmployeeName
        {
            get => employeeName;
            set
            {
                employeeName = value;
                RaisePropertyChanged(nameof(EmployeeName));
            }
        }

        public string? Department
        {
            get => department;
            set
            {
                department = value;
                RaisePropertyChanged(nameof(Department));
            }
        }

        public string? WorkLocation
        {
            get => workLocation;
            set
            {
                workLocation = value;
                RaisePropertyChanged(nameof(WorkLocation));
            }
        }

        public DateTime JoinDate
        {
            get => joinDate;
            set
            {
                joinDate = value;
                RaisePropertyChanged(nameof(JoinDate));
            }
        }

        public TimeSpan ShiftStartTime
        {
            get => shiftStartTime;
            set
            {
                shiftStartTime = value;
                RaisePropertyChanged(nameof(ShiftStartTime));
            }
        }

        public ShiftProjectInfo? AssignedProject
        {
            get => assignedProject;
            set
            {
                assignedProject = value;
                RaisePropertyChanged(nameof(AssignedProject));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
