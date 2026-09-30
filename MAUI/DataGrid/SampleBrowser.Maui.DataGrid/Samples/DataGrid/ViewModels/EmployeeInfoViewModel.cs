using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SampleBrowser.Maui.DataGrid
{
    public class EmployeeInfoViewModel
    {
        private ObservableCollection<EmployeeInfo> _employees = new ObservableCollection<EmployeeInfo>();
        private Random _r = new Random();
        private Dictionary<string, string> _gender = new Dictionary<string, string>();

        private readonly ObservableCollection<string> _femaleNames = new ObservableCollection<string>
        {
            "Phyllis", "Sandra", "Selena", "Ramona", "Sabria", "Hannah", "Kyley",
            "Teresa", "Catherine", "Pilar", "Frances", "Carla", "Lili", "Amy", "Anna", "Mae"
        };

        private readonly ObservableCollection<string> _maleNames = new ObservableCollection<string>
        {
            "Jacobson", "Marvin", "Michael", "Cecil", "Oscar", "Emilio", "Maxwell",
            "Tom", "John", "Chris", "Robert", "Stephen", "Humberto", "Ronald",
            "Samuel", "James", "Francois", "Milton", "Paul", "Gregory", "Daniel", "Cory"
        };

        public EmployeeInfoViewModel()
        {
            PopulateData();
            Employees = GetEmployeesDetails(100);
        }

        public ObservableCollection<EmployeeInfo> Employees
        {
            get => _employees;
            set => _employees = value;
        }

        private ObservableCollection<EmployeeInfo> GetEmployeesDetails(int count)
        {
            var employees = new ObservableCollection<EmployeeInfo>();
            for (int i = 1; i <= count; i++)
            {
                employees.Add(GetEmployee(i));
            }
            return employees;
        }

        private EmployeeInfo GetEmployee(int i)
        {
            var name = EmployeeName[_r.Next(EmployeeName.Length)];
            return new EmployeeInfo
            {
                EmployeeID = 1000 + i,
                Name = name,
                Rating = _r.Next(4, 10),
                ContactID = _r.Next(1001, 2000),
                Gender = _gender.ContainsKey(name) ? _gender[name] : (_r.Next(2) == 0 ? "Male" : "Female"),
                Title = Titles[_r.Next(Titles.Length)],
                BirthDate = new DateOnly(_r.Next(1975, 1990), _r.Next(1, 12), _r.Next(1, 27)),
                SickLeaveHours = _r.Next(15, 70),
                Salary = new decimal(Math.Round(_r.NextDouble() * 6000.5, 2))
            };
        }

        private void PopulateData()
        {
            foreach (var name in EmployeeName)
            {
                if (_femaleNames.Any(n => string.Equals(n, name, StringComparison.OrdinalIgnoreCase)))
                {
                    _gender[name] = "Female";
                }
                else if (_maleNames.Any(n => string.Equals(n, name, StringComparison.OrdinalIgnoreCase)))
                {
                    _gender[name] = "Male";
                }
                else
                {
                    _gender[name] = "Male";
                }
            }
        }

        private string[] Titles = new string[]
        {
            "Marketer",
            "Manager",
            "Designer",
            "Toolmaker",
            "Strategist",
            "Supervisor",
            "Technician",
            "Engineer",
            "Producer",
            "Administrator",
            "Scheduler",
            "Specialist",
            "Recruiter",
            "Maintainer",
            "Clerk",
            "Stocker",
            "Operator"
        };

        private string[] EmployeeName = new string[]
        {
            "Jacobson",
            "Phyllis",
            "Marvin",
            "Michael",
            "Cecil",
            "Oscar",
            "Sandra",
            "Selena",
            "Emilio",
            "Maxwell",
            "Anderson",
            "Ramona",
            "Sabria",
            "Hannah",
            "Kyley",
            "Tom",
            "John",
            "Chris",
            "Teresa",
            "Robert",
            "Stephen",
            "Bacalzo",
            "Achong",
            "Catherine",
            "Abercrombie",
            "Humberto",
            "Pilar",
            "Frances",
            "Smith",
            "Carla",
            "Adams",
            "Ronald",
            "Samuel",
            "James",
            "Robert",
            "Francois",
            "Akers",
            "Lili",
            "Amy",
            "Anna",
            "Milton",
            "Paul",
            "Gregory",
            "Alexander",
            "Daniel",
            "Cory",
            "Bailey"
        };
    }
}
