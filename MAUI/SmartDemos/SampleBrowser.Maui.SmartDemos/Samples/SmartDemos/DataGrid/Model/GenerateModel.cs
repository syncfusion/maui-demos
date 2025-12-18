#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    public class GenerateModel : INotifyPropertyChanged
    {
        private int _studentID;
        private string _studentName;
        private double? _firstYearGPA;
        private double? _secondYearGPA;
        private double? _thirdYearGPA;
        private double? _finalYearGPA;
        private double? _totalGPA;
        private string? _totalGrade;

        public int StudentID
        {
            get => _studentID;
            set
            {
                if (_studentID != value)
                {
                    _studentID = value;
                    OnPropertyChanged(nameof(StudentID));
                }
            }
        }

        public string StudentName
        {
            get => _studentName;
            set
            {
                if (_studentName != value)
                {
                    _studentName = value;
                    OnPropertyChanged(nameof(StudentName));
                }
            }
        }

        public double? FirstYearGPA
        {
            get => _firstYearGPA;
            set
            {
                if (_firstYearGPA != value)
                {
                    _firstYearGPA = value;
                    OnPropertyChanged(nameof(FirstYearGPA));
                }
            }
        }

        public double? SecondYearGPA
        {
            get => _secondYearGPA;
            set
            {
                if (_secondYearGPA != value)
                {
                    _secondYearGPA = value;
                    OnPropertyChanged(nameof(SecondYearGPA));
                }
            }
        }

        public double? ThirdYearGPA
        {
            get => _thirdYearGPA;
            set
            {
                if (_thirdYearGPA != value)
                {
                    _thirdYearGPA = value;
                    OnPropertyChanged(nameof(ThirdYearGPA));
                }
            }
        }

        public double? FinalYearGPA
        {
            get => _finalYearGPA;
            set
            {
                if (_finalYearGPA != value)
                {
                    _finalYearGPA = value;
                    OnPropertyChanged(nameof(FinalYearGPA));
                }
            }
        }

        public double? TotalGPA
        {
            get => _totalGPA;
            set
            {
                if (_totalGPA != value)
                {
                    _totalGPA = value;
                    OnPropertyChanged(nameof(TotalGPA));
                }
            }
        }

        public string? TotalGrade
        {
            get => _totalGrade;
            set
            {
                if (_totalGrade != value)
                {
                    _totalGrade = value;
                    OnPropertyChanged(nameof(TotalGrade));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GenerateModel(int studentID, string studentName, double? firstYearGPA, double? secondYearGPA, double? thirdYearGPA)
        {
            _studentID = studentID;
            _studentName = studentName;
            _firstYearGPA = firstYearGPA;
            _secondYearGPA = secondYearGPA;
            _thirdYearGPA = thirdYearGPA;
        }
    }
}
