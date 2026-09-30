using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.DataGrid
{
    public class EmployeeShiftViewModel
    {
        private readonly Random random = new();

        public ObservableCollection<EmployeeShiftInfo> Employees { get; set; } = new ObservableCollection<EmployeeShiftInfo>();

        public ObservableCollection<string> Departments { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> Locations { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<ShiftProjectInfo> Projects { get; set; } = new ObservableCollection<ShiftProjectInfo>();

        public EmployeeShiftViewModel()
        {
            PopulateProjects();

            Employees = GenerateEmployees(50);
        }

        private void PopulateProjects()
        {
            Departments = new ObservableCollection<string>()
            {
                "Development",
                "Quality Assurance",
                "Human Resources",
                "Finance",
                "Sales",
                "UI/UX Design",
                "DevOps"
            };

            Locations = new ObservableCollection<string>()
{
                "New York",
"Los Angeles",
    "Seattle",
    "London",
    "Berlin",
    "Paris",
    "Toronto",
    "Sydney",
    "Zurich",
    "Dublin"
};

            Projects = new ObservableCollection<ShiftProjectInfo>()
            {
                new ShiftProjectInfo()
                {
                    ProjectId = "PRJ001",
                    ProjectName = "MAUI DataGrid",
                    Customer = "Syncfusion"
                },

                new ShiftProjectInfo()
                {
                    ProjectId = "PRJ002",
                    ProjectName = "Scheduler",
                    Customer = "Contoso"
                },

                new ShiftProjectInfo()
                {
                    ProjectId = "PRJ003",
                    ProjectName = "Kanban",
                    Customer = "Fabrikam"
                },

                new ShiftProjectInfo()
                {
                    ProjectId = "PRJ004",
                    ProjectName = "PDF Viewer",
                    Customer = "Adventure Works"
                },

                new ShiftProjectInfo()
{

ProjectId = "PRJ005",

ProjectName = "Charts",
Customer = "Northwind"

},
                new ShiftProjectInfo()
{

ProjectId = "PRJ006",

ProjectName = "Calendar",

Customer = "Tailspin Toys"

},
            };
        }

        private ObservableCollection<EmployeeShiftInfo> GenerateEmployees(int count)
        {
            var employees = new ObservableCollection<EmployeeShiftInfo>();

            string[] employeeNames =
            {
    "Michael",
    "Daniel",
    "John",
    "Mark",
    "William",
    "Steve",
    "Gina",
    "Katie",
    "Brenda",
    "Jennifer",
    "Linda",
    "Julie",
    "Kyle",
    "Oscar",
    "Ralph",
    "Bill",
    "Frank",
    "Carol",
    "Shannon",
    "Terry"
};

            for (int i = 1; i <= count; i++)
            {
                employees.Add(new EmployeeShiftInfo()
                {
                    EmployeeId = 1000 + i,

                    EmployeeName =
                        employeeNames[random.Next(employeeNames.Length)],

                    Department = Departments[(i - 1) % Departments.Count],

                    WorkLocation =
                        Locations[random.Next(Locations.Count)],

                    JoinDate =
                        DateTime.Today.AddDays(-random.Next(300, 2500)),

                    ShiftStartTime =
                        GetShiftTime(),

                    AssignedProject =
                        Projects[random.Next(Projects.Count)]
                });
            }

            return employees;
        }

        private TimeSpan GetShiftTime()
        {
            TimeSpan[] shifts =
            {
                new TimeSpan(8, 0, 0),
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0)
            };

            return shifts[random.Next(shifts.Length)];
        }
    }
}