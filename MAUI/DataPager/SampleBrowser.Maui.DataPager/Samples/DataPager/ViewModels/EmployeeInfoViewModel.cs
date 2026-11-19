using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace SampleBrowser.Maui.DataPager.SfDataPager
{
    public class EmployeeInfoViewModel : INotifyPropertyChanged
    {
        internal static Random random = new Random(123);

        private static readonly Dictionary<string, DateTime> JoiningDateCache = new();

        #region Constructor

        public EmployeeInfoViewModel()
        {
            this.PersonDetails = this.CreateGenericPersonData(17, 4);
            this.EmployeeDetails = this.GetEmployeesInfo();
        }

        #endregion

        #region Properties

        private ObservableCollection<EmployeeInfo> _personDetails = new ObservableCollection<EmployeeInfo>();

        /// <summary>
        /// Gets or sets the person details.
        /// </summary>
        /// <value>The person details.</value>
        public ObservableCollection<EmployeeInfo> PersonDetails
        {
            get { return _personDetails; }
            set { _personDetails = value; }
        }

        private ObservableCollection<EmployeeInfo> _employeeDetails = new ObservableCollection<EmployeeInfo>();

        /// <summary>
        /// Gets or sets the employee details.
        /// </summary>
        /// <value>The employee details.</value>
        public ObservableCollection<EmployeeInfo> EmployeeDetails
        {
            get { return _employeeDetails; }
            set { _employeeDetails = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="name">string type of name</param>
        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Generates a collection of employee information for self-relational view.
        /// </summary>
        /// <returns>A collection of <see cref="EmployeeInfo"/> objects.</returns>
        private ObservableCollection<EmployeeInfo> GetEmployeesInfo()
        {
            ObservableCollection<EmployeeInfo> model = new ObservableCollection<EmployeeInfo>();

            //Root Nodes
            model.Add(new EmployeeInfo() { Title = "General Manager", ReportsTo = -1, ID = 2, FirstName = "Sean", LastName = "Jacobson", Salary = 200000 });
            model.Add(new EmployeeInfo() { Title = "Accounts Manager", ReportsTo = -1, ID = 3, FirstName = "Phyllis", LastName = "Allen", Salary = 175000 });
            model.Add(new EmployeeInfo() { Title = "Sales Manager", ReportsTo = -1, ID = 4, FirstName = "Oscar", LastName = "Alpuerto", Salary = 150000 });
            model.Add(new EmployeeInfo() { Title = "Marketing Manager", ReportsTo = -1, ID = 5, FirstName = "Maxwell", LastName = "Amland", Salary = 140000 });
            model.Add(new EmployeeInfo() { Title = "HR Manager", ReportsTo = -1, ID = 6, FirstName = "Emiliya", LastName = "Alvaro", Salary = 135000 });
            model.Add(new EmployeeInfo() { Title = "Advertising Manager", ReportsTo = -1, ID = 7, FirstName = "Carla", LastName = "Adams", Salary = 125000 });
            model.Add(new EmployeeInfo() { Title = "Production Manager", ReportsTo = -1, ID = 8, FirstName = "John", LastName = "Ault", Salary = 125000 });

            model.Add(new EmployeeInfo() { FirstName = "Fernando", LastName = "Joseph", Title = "Management", Salary = 2000000, ReportsTo = -1, ID = 32 });
            model.Add(new EmployeeInfo() { FirstName = "John", LastName = "Adams", Title = "Accounts", Salary = 2000000, ReportsTo = -1, ID = 33 });
            model.Add(new EmployeeInfo() { FirstName = "Thomas", LastName = "Jefferson", Title = "Sales", Salary = 300000, ReportsTo = -1, ID = 34 });
            model.Add(new EmployeeInfo() { FirstName = "Andrew", LastName = "Madison", Title = "Marketing", Salary = 4000000, ReportsTo = -1, ID = 35 });
            model.Add(new EmployeeInfo() { FirstName = "Ulysses", LastName = "Pierce", Title = "HR", Salary = 1500000, ReportsTo = -1, ID = 36 });
            model.Add(new EmployeeInfo() { FirstName = "Jimmy", LastName = "Harrison", Title = "Purchasing", Salary = 200000, ReportsTo = -1, ID = 37 });
            model.Add(new EmployeeInfo() { FirstName = "Ronald", LastName = "Fillmore", Title = "Production", Salary = 2800000, ReportsTo = -1, ID = 38 });
            model.Add(new EmployeeInfo() { FirstName = "Nancy", LastName = "Peacock", Title = "Manager", Salary = 85000, ReportsTo = -1, ID = 50 });
            model.Add(new EmployeeInfo() { FirstName = "Margaret", LastName = "Davolio", Title = "Accounts", Salary = 70000, ReportsTo = -1, ID = 51 });
            model.Add(new EmployeeInfo() { FirstName = "Steven", LastName = "Buchanan", Title = "Sales", Salary = 90000, ReportsTo = -1, ID = 52 });

            //Child Nodes
            model.Add(new EmployeeInfo() { FirstName = "Robert", LastName = "Fuller", Salary = 1200000, Title = "Design Engineer", ID = 9, ReportsTo = 2 });
            model.Add(new EmployeeInfo() { FirstName = "Janet", LastName = "Leverling", Salary = 1000000, Title = "Engineering Manager", ID = 10, ReportsTo = 9 });
            model.Add(new EmployeeInfo() { FirstName = "Steven", LastName = "Buchanan", Salary = 900000, Title = "Business Manager", ID = 11, ReportsTo = 10 });
            model.Add(new EmployeeInfo() { FirstName = "Albert", LastName = "King", Salary = 730000, Title = "Sales Representative", ID = 95, ReportsTo = 2 });

            model.Add(new EmployeeInfo() { FirstName = "Nancy", LastName = "Davolio", Salary = 850000, Title = "Accounts Supervisor", ID = 12, ReportsTo = 3 });
            model.Add(new EmployeeInfo() { FirstName = "Margaret", LastName = "Peacock", Salary = 700000, Title = "Accounts Representative", ID = 13, ReportsTo = 3 });
            model.Add(new EmployeeInfo() { FirstName = "Michael", LastName = "Suyama", Salary = 700000, Title = "Accounts Coordinator", ID = 14, ReportsTo = 3 });
            model.Add(new EmployeeInfo() { FirstName = "Andrew", LastName = "King", Salary = 650000, Title = "Accountant", ID = 15, ReportsTo = 3 });

            model.Add(new EmployeeInfo() { FirstName = "SIMOB", LastName = "Callahan", Salary = 900000, Title = "Sales Representative", ID = 16, ReportsTo = 4 });
            model.Add(new EmployeeInfo() { FirstName = "Anne", LastName = "Dodsworth", Salary = 800000, Title = "Sales Coordinator", ID = 17, ReportsTo = 4 });
            model.Add(new EmployeeInfo() { FirstName = "Albert", LastName = "Hellstern", Salary = 750000, Title = "Sales Representative", ID = 18, ReportsTo = 17 });
            model.Add(new EmployeeInfo() { FirstName = "Seves", LastName = "Smith", Salary = 700000, Title = "Inside Sales Coordinator", ID = 19, ReportsTo = 16 });
            model.Add(new EmployeeInfo() { FirstName = "Justin", LastName = "Brid", Salary = 700000, Title = "Sales Supervisor", ID = 20, ReportsTo = 4 });

            model.Add(new EmployeeInfo() { FirstName = "Caroline", LastName = "Patterson", Salary = 800000, Title = "Marketing Director", ID = 21, ReportsTo = 5 });
            model.Add(new EmployeeInfo() { FirstName = "Hill", LastName = "Martin", Salary = 700000, Title = "Marketing Associate", ID = 22, ReportsTo = 5 });

            model.Add(new EmployeeInfo() { FirstName = "Albert", LastName = "Pereira", Salary = 900000, Title = "HR Coordinator", ID = 23, ReportsTo = 6 });
            model.Add(new EmployeeInfo() { FirstName = "Hawkin", LastName = "Abbas", Salary = 650000, Title = "HR Assistant", ID = 24, ReportsTo = 6 });
            model.Add(new EmployeeInfo() { FirstName = "Amy", LastName = "Alberts", Salary = 650000, Title = "HR Assistant", ID = 25, ReportsTo = 6 });

            model.Add(new EmployeeInfo() { FirstName = "SIMOB", LastName = "Ansman-Wolfe", Salary = 600000, Title = "Advertising Director", ID = 26, ReportsTo = 7 });
            model.Add(new EmployeeInfo() { FirstName = "Michael", LastName = "Blythe", Salary = 550000, Title = "Advertising Coordinator", ID = 27, ReportsTo = 26 });
            model.Add(new EmployeeInfo() { FirstName = "Seves", LastName = "Campbell", Salary = 450000, Title = "Advertising Specialist", ID = 28, ReportsTo = 7 });

            model.Add(new EmployeeInfo() { FirstName = "Janet", LastName = "Carson", Salary = 600000, Title = "Production Supervisor", ID = 29, ReportsTo = 8 });
            model.Add(new EmployeeInfo() { FirstName = "Caroline", LastName = "Ito", Salary = 550000, Title = "Production Technician", ID = 30, ReportsTo = 8 });
            model.Add(new EmployeeInfo() { FirstName = "Steven", LastName = "Jiang", Salary = 450000, Title = "Production Control Manager", ID = 31, ReportsTo = 8 });

            model.Add(new EmployeeInfo() { FirstName = "Andrew", LastName = "Fuller", ID = 61, Salary = 1200000, ReportsTo = 32, Title = "Vice President" });
            model.Add(new EmployeeInfo() { FirstName = "Janet", LastName = "Leverling", ID = 62, Salary = 1000000, ReportsTo = 32, Title = "GM" });
            model.Add(new EmployeeInfo() { FirstName = "Steven", LastName = "Buchanan", ID = 63, Salary = 900000, ReportsTo = 32, Title = "Manager" });

            model.Add(new EmployeeInfo() { FirstName = "Nancy", LastName = "Davolio", ID = 64, Salary = 850000, ReportsTo = 33, Title = "Accounts Manager" });
            model.Add(new EmployeeInfo() { FirstName = "Margaret", LastName = "Peacock", ID = 65, Salary = 700000, ReportsTo = 33, Title = "Accountant" });
            model.Add(new EmployeeInfo() { FirstName = "Michael", LastName = "Suyama", ID = 66, Salary = 700000, ReportsTo = 33, Title = "Accountant" });
            model.Add(new EmployeeInfo() { FirstName = "Robert", LastName = "King", ID = 67, Salary = 650000, ReportsTo = 33, Title = "Accountant" });

            model.Add(new EmployeeInfo() { FirstName = "Laura", LastName = "Callahan", ID = 68, Salary = 900000, ReportsTo = 34, Title = "Sales Manager" });
            model.Add(new EmployeeInfo() { FirstName = "Anne", LastName = "Dodsworth", ID = 69, Salary = 800000, ReportsTo = 34, Title = "Sales Representative" });
            model.Add(new EmployeeInfo() { FirstName = "Albert", LastName = "Hellstern", ID = 70, Salary = 750000, ReportsTo = 68, Title = "Sales Representative" });
            model.Add(new EmployeeInfo() { FirstName = "Tim", LastName = "Smith", ID = 71, Salary = 700000, ReportsTo = 69, Title = "Sales Representative" });
            model.Add(new EmployeeInfo() { FirstName = "Justin", LastName = "Brid", ID = 72, Salary = 700000, ReportsTo = 70, Title = "Sales Representative" });

            model.Add(new EmployeeInfo() { FirstName = "Caroline", LastName = "Patterson", ID = 73, Salary = 800000, ReportsTo = 35, Title = "Receptionist" });
            model.Add(new EmployeeInfo() { FirstName = "Xavier", LastName = "Martin", ID = 74, Salary = 700000, ReportsTo = 35, Title = "Mail Clerk" });

            model.Add(new EmployeeInfo() { FirstName = "Laurent", LastName = "Pereira", ID = 75, Salary = 900000, ReportsTo = 36, Title = "HR Manager" });
            model.Add(new EmployeeInfo() { FirstName = "Syed", LastName = "Abbas", ID = 76, Salary = 650000, ReportsTo = 36, Title = "HR Assistant" });
            model.Add(new EmployeeInfo() { FirstName = "Amy", LastName = "Alberts", ID = 77, Salary = 650000, ReportsTo = 36, Title = "HR Assistant" });

            model.Add(new EmployeeInfo() { FirstName = "Pamela", LastName = "Ansman-Wolfe", ID = 78, Salary = 600000, ReportsTo = 37, Title = "Purchase Manager" });
            model.Add(new EmployeeInfo() { FirstName = "Michael", LastName = "Blythe", ID = 79, Salary = 550000, ReportsTo = 37, Title = "Store Keeper" });
            model.Add(new EmployeeInfo() { FirstName = "David", LastName = "Campbell", ID = 80, Salary = 450000, ReportsTo = 37, Title = "Store Keeper" });

            model.Add(new EmployeeInfo() { FirstName = "Jillian", LastName = "Carson", ID = 81, Salary = 600000, ReportsTo = 38, Title = "Production Manager" });
            model.Add(new EmployeeInfo() { FirstName = "Shu", LastName = "Ito", ID = 82, Salary = 550000, ReportsTo = 38, Title = "Production Engineer" });
            model.Add(new EmployeeInfo() { FirstName = "Stephen", LastName = "Jiang", ID = 83, Salary = 450000, ReportsTo = 38, Title = "Production Engineer" });

            model.Add(new EmployeeInfo() { FirstName = "Stephen", LastName = "Jiang", ID = 84, Salary = 450000, ReportsTo = 50, Title = "Production Engineer" });
            model.Add(new EmployeeInfo() { FirstName = "Tim", LastName = "Smith", ID = 85, Salary = 700000, ReportsTo = 50, Title = "Sales" });

            model.Add(new EmployeeInfo() { FirstName = "Justin", LastName = "Brid", ID = 86, Salary = 700000, ReportsTo = 51, Title = "Sales" });
            model.Add(new EmployeeInfo() { FirstName = "Albert", LastName = "Hellstern", ID = 87, Salary = 750000, ReportsTo = 51, Title = "Sales Representative" });
            model.Add(new EmployeeInfo() { FirstName = "Laura", LastName = "Callahan", ID = 91, Salary = 900000, ReportsTo = 86, Title = "Sales" });

            model.Add(new EmployeeInfo() { FirstName = "Tim", LastName = "Smith", ID = 88, Salary = 700000, ReportsTo = 52, Title = "Engineer" });
            model.Add(new EmployeeInfo() { FirstName = "Justin", LastName = "Brid", ID = 89, Salary = 700000, ReportsTo = 52, Title = "Engineer" });
            model.Add(new EmployeeInfo() { FirstName = "Stephen", LastName = "Jiang", ID = 90, Salary = 45000, ReportsTo = 89, Title = "Engineer" });
            model.Add(new EmployeeInfo() { FirstName = "Anne", LastName = "Dodsworth", ID = 92, Salary = 800000, ReportsTo = 90, Title = "Sales" });
            model.Add(new EmployeeInfo() { FirstName = "Albert", LastName = "Hellstern", ID = 93, Salary = 750000, ReportsTo = 92, Title = "Sales" });

            return model;
        }

        /// <summary>
        /// Generates a collection of employee information for nested view.
        /// </summary>
        /// <param name="parentCount">The number of root nodes to generate</param>
        /// <param name="childCount">The number of child nodes to generate for each parent</param>
        /// <returns>A collection of <see cref="EmployeeInfo"/>.</returns>
        private ObservableCollection<EmployeeInfo> CreateGenericPersonData(int parentCount, int childCount)
        {
            var personList = new ObservableCollection<EmployeeInfo>();
            if (parentCount > 0)
            {
                for (int i = 0; i < parentCount; i++)
                {
                    var firstname = firstName[random.Next(firstName.GetLength(0))];
                    var lastname = lastName[random.Next(lastName.GetLength(0))];
                    personList.Add(new EmployeeInfo()
                    {
                        FirstName = firstname,
                        LastName = lastname,
                        ID = 1000 + i,
                        JoiningDate = this.GetJoiningDate(firstname, lastname),
                        Title = title[random.Next(0, 13)],
                        Children = this.CreateChildList(childCount, (int)Math.Max(0, childCount - 1), lastname),
                        Salary = 30000d + random.Next(9) * 10000,
                        ReportsTo = random.Next(0, 20),
                    });
                }
            }
            return personList;
        }

        /// <summary>
        /// Generates a list of child nodes for a parent node.
        /// </summary>
        /// <param name="parentCount">The number of child records to generate per parent.</param>
        /// <param name="childCount">The depth of child hierarchy to generate.</param>
        /// <param name="lastName">The last name to assign to children.</param>
        /// <returns>A collection of <see cref="EmployeeInfo"/>.</returns>
        private ObservableCollection<EmployeeInfo> CreateChildList(int parentCount, int childCount, string lastName)
        {
            ObservableCollection<EmployeeInfo> children = new ObservableCollection<EmployeeInfo>();
            if (parentCount > 0 && childCount > 0)
            {
                children = CreateGenericPersonData(parentCount, childCount - 1);
                foreach (EmployeeInfo employee in children)
                    employee.LastName = lastName;
            }

            return children;
        }

        private DateTime GetJoiningDate(string firstName, string lastName)
        {
            string key = $"{firstName}_{lastName}";

            if (!JoiningDateCache.TryGetValue(key, out DateTime joiningDate))
            {
                int randInt = random.Next(4000);
                joiningDate = DateTime.Now.AddDays(-8000 + randInt);
                JoiningDateCache[key] = joiningDate;
            }

            return joiningDate;
        }

        #endregion

        #region Array Collections

        internal static string[] firstName = new string[]{
            "George","John","Thomas","James","Andrew","Martin","William","Zachary", "Millard","Franklin","Abraham","Ulysses",
            "Rutherford", "Chester","Grover","Benjamin","Theodore","Woodrow","Warren","Calvin","Herbert","Franklin","Harry",
            "Dwight","Lyndon","Gerald","Jimmy","Ronald","George","Bill", "Barack", "Warner","Peter", "Larry"
        };

        internal static string[] lastName = new string[]{
            "Washington","Adams","Jefferson","Madison","Monroe","Jackson","Van Buren","Harrison","Tyler", "Polk","Taylor","Fillmore", "Pierce","Buchanan",
            "Lincoln","Johnson","Grant","Hayes","Garfield", "Arthur","Cleveland","Harrison","McKinley","Roosevelt","Taft", "Wilson","Harding",
            "Coolidge", "Hoover","Truman","Eisenhower","Kennedy","Johnson","Nixon","Ford","Carter","Reagan","Bush", "Clinton","Bush","Obama","Smith","Jones","Stogner"
        };

        internal static string[] title = new string[] {
            "Engineering Manager","Production Manager","Design Engineer","Network Administrator", "Stocker","Production Technician", "Master Scheduler",
            "Marketing Specialist", "Recruiter", "Maintenance Supervisor","Marketing Assistant","Tool Designer", "Senior Tool Designer","Quality Supervisor"
        };

        #endregion

    }
}
