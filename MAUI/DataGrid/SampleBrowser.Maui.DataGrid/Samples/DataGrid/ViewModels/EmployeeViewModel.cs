using SampleBrowser.Maui.Base.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region Field

        private List<Employee>? employeeInformation;
        private List<Employee>? multiRowEmployeeInformation;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CellTemplateViewModel class.
        /// </summary>
        public EmployeeViewModel()
        {
            
            this.employeeInformation = this.GetEmployeeDetails();
            this.multiRowEmployeeInformation = this.GetMultiRowEmployeeDetails();
        }

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region ItemsSource

        /// <summary>
        /// Gets or sets the value of EmployeeInformation
        /// </summary>
        public List<Employee>? EmployeeInformation
        {
            get { return this.employeeInformation; }
            set { this.employeeInformation = value; }
        }

        /// <summary>
        /// Gets or sets the value of MultiRowEmployeeInformation
        /// </summary>
        public List<Employee>? MultiRowEmployeeInformation
        {
            get { return this.multiRowEmployeeInformation; }
            set { this.multiRowEmployeeInformation = value; }
        }

        #endregion

        /// <summary>
        /// Used to generates the Items source.
        /// </summary>
        /// <returns>returns generates items</returns>
        public List<Employee> GetEmployeeDetails()
        {
            List<Employee> employeeDetails = new List<Employee>();
            employeeDetails.Add(new Employee()
            {
                Name = "Nancy",
                Designation = "Sales Representative",
                DateOfBirth = new DateTime(1948, 8, 12),
                About = "Education includes a BA in psychology from Colorado State University in 1970. Nancy is a member of Toastmasters International.",
                Country = "USA",
                EmployeeID = 4563,
                Telephone = "(206) 555 -9857",
                Image = "ellipse635.png",
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Andrea",
                Designation = "Vice President",
                DateOfBirth = new DateTime(1952, 2, 19),
                About = "Andrea received her Ph.D. in international marketing in 1981. She joined the company as a sales representative in March 1993.",
                Country = "USA",
                EmployeeID = 4362,
                Telephone = "(206) 555 -9482",
                Image = "ellipse637.png",
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Garry",
                Designation = "Sales Representative",
                DateOfBirth = new DateTime(1963, 8, 30),
                Country = "USA",
                EmployeeID = 4134,
                About = "Garry has a BS degree in chemistry from Boston College (1984). Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
                Telephone = "(206) 555 -9356",
                Image = "ellipse631.png",
            });
            employeeDetails.Add(new Employee()
            {
                Name = "Margaret",
                Designation = "Sales Representative",
                DateOfBirth = new DateTime(1937, 9, 19),
                Country = "USA",
                EmployeeID = 4834,
                About = "Margaret holds a BA in English literature from Concordia College (1958).  She was assigned to the London office temporarily 1992.",
                Telephone = "(206) 555 -4766",
                Image = "ellipse639.png"
            });
            employeeDetails.Add(new Employee()
            {
                Name = "Steven",
                Designation = "Sales Manager",
                DateOfBirth = new DateTime(1955, 4, 3),
                Country = "USA",
                EmployeeID = 4267,
                About = "Steven Buchanan graduated with a BSC degree in 1976. He spent 6 months in an orientation program at the Seattle office and then returned to London.",
                Telephone = "(206) 555 -4567",
                Image = "ellipse633.png"
            });
            employeeDetails.Add(new Employee()
            {
                Name = "Michale",
                Designation = "Sales Representative",
                DateOfBirth = new DateTime(1963, 7, 2),
                Country = "USA",
                EmployeeID = 4553,
                About = "Michale is a graduate of Sussex University (MA, economics, 1983).  She has also taken the course Multi-Cultural Selling for the Sales Professional.",
                Telephone = "(206) 555 -7777",
                Image = "ellipse634.png"
            });
            employeeDetails.Add(new Employee()
            {
                Name = "Robert",
                Designation = "Sales Representative",
                DateOfBirth = new DateTime(1960, 5, 27),
                Country = "USA",
                EmployeeID = 4423,
                About = "Robert King completing his degree in English at the University of Michigan in 1992.  After completing a course, he was transferred to the London office in March 1993.",
                Telephone = "(206) 555 -7856",
                Image = "ellipse636.png"
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Laura",
                Designation = "Inside Sales Coordinator",
                DateOfBirth = new DateTime(1958, 9, 1),
                Country = "Seattle",
                EmployeeID = 4265,
                About = "Laura received a BA in psychology from the University of Washington.  She has also completed a course in business French.",
                Telephone = "(206) 555 -1189",
                Image = "ellipse632.png"
            });

            employeeDetails.Add(new Employee()
            {
                Name = "John",
                Designation = "Sales Representative",
                DateOfBirth = new DateTime(1966, 1, 27),
                Country = "USA",
                EmployeeID = 3563,
                About = "John has a BA degree in English from St. Lawrence College. She has also completed a course in business French.",
                Telephone = "(206) 555 -7856",
                Image = "ellipse638.png"
            });

            return employeeDetails;
        }

        /// <summary>
        /// Used to generates the Items source for MultiRowView sample.
        /// </summary>
        /// <returns>returns generates items</returns>
        public List<Employee> GetMultiRowEmployeeDetails()
        {
            List<Employee> employeeDetails = new List<Employee>();
            var ResourceAssembly = typeof(SfImageResourceExtension).GetTypeInfo().Assembly;

            employeeDetails.Add(new Employee()
            {
                Name = "Nancy",
                Designation = "Design Lead",
                DateOfBirth = new DateTime(1993, 4, 18),
                About = "Alyssa creates polished visual concepts for mobile-first data views.",
                Country = "USA",
                City = "Seattle",
                EmployeeID = 7001,
                Telephone = "(206) 555 -2001",
                Image = "people_circle0.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle0.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Bianca",
                Designation = "Software Engineer",
                DateOfBirth = new DateTime(1992, 9, 12),
                About = "Bianca builds responsive screen logic and efficient data bindings.",
                Country = "Canada",
                City = "Toronto",
                EmployeeID = 7002,
                Telephone = "(416) 555 -2002",
                Image = "people_circle1.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle1.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Clara",
                Designation = "UX Architect",
                DateOfBirth = new DateTime(1990, 6, 24),
                About = "Clara shapes layout structure and spacing rules for rich grid samples.",
                Country = "UK",
                City = "London",
                EmployeeID = 7003,
                Telephone = "(020) 555 -2003",
                Image = "people_circle2.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle2.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Diana",
                Designation = "Content Strategist",
                DateOfBirth = new DateTime(1995, 1, 8),
                About = "Diana prepares sample narratives and clear product descriptions.",
                Country = "Australia",
                City = "Sydney",
                EmployeeID = 7004,
                Telephone = "(02) 555 -2004",
                Image = "people_circle3.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle3.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Elena",
                Designation = "Support Engineer",
                DateOfBirth = new DateTime(1993, 11, 15),
                About = "Elena validates interactions and checks the experience across platforms.",
                Country = "USA",
                City = "Denver",
                EmployeeID = 7005,
                Telephone = "(303) 555 -2005",
                Image = "people_circle4.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle4.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Mark",
                Designation = "Product Manager",
                DateOfBirth = new DateTime(1989, 3, 9),
                About = "Mark coordinates feature planning and sample presentation quality.",
                Country = "USA",
                City = "Los Angeles",
                EmployeeID = 7006,
                Telephone = "(213) 555 -2006",
                Image = "people_circle5.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle5.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Fiona",
                Designation = "QA Analyst",
                DateOfBirth = new DateTime(1996, 7, 21),
                About = "Fiona tests the data layout and ensures consistent rendering behavior.",
                Country = "Ireland",
                City = "Dublin",
                EmployeeID = 7007,
                Telephone = "(01) 555 -2007",
                Image = "people_circle6.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle6.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Georgia",
                Designation = "Data Analyst",
                DateOfBirth = new DateTime(1991, 5, 30),
                About = "Georgia reviews presentation data and supports sample insights.",
                Country = "New Zealand",
                City = "Auckland",
                EmployeeID = 7008,
                Telephone = "(09) 555 -2008",
                Image = "people_circle7.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle7.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Henry",
                Designation = "Design Engineer",
                DateOfBirth = new DateTime(1990, 12, 2),
                About = "Henry supports the visual composition of the sample browser.",
                Country = "USA",
                City = "San Francisco",
                EmployeeID = 7009,
                Telephone = "(415) 555 -2009",
                Image = "people_circle8.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle8.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Iris",
                Designation = "Frontend Engineer",
                DateOfBirth = new DateTime(1994, 8, 14),
                About = "Iris develops responsive layouts and polish for the sample interface.",
                Country = "Germany",
                City = "Berlin",
                EmployeeID = 7010,
                Telephone = "(030) 555 -2010",
                Image = "people_circle9.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle9.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Julia",
                Designation = "Visual Designer",
                DateOfBirth = new DateTime(1997, 2, 6),
                About = "Julia crafts attractive visuals for friendly sample browsing.",
                Country = "France",
                City = "Paris",
                EmployeeID = 7011,
                Telephone = "(01) 555 -2011",
                Image = "people_circle10.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle10.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Kira",
                Designation = "Documentation Writer",
                DateOfBirth = new DateTime(1992, 10, 19),
                About = "Kira writes concise notes and sample descriptions for users.",
                Country = "USA",
                City = "Miami",
                EmployeeID = 7012,
                Telephone = "(305) 555 -2012",
                Image = "people_circle11.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle11.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Owen",
                Designation = "DevOps Engineer",
                DateOfBirth = new DateTime(1988, 6, 28),
                About = "Owen manages build reliability and sample deployment automation.",
                Country = "USA",
                City = "Dallas",
                EmployeeID = 7013,
                Telephone = "(214) 555 -2013",
                Image = "people_circle12.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle12.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Paula",
                Designation = "Mobile Developer",
                DateOfBirth = new DateTime(1993, 9, 7),
                About = "Paula implements platform-specific interactions for mobile devices.",
                Country = "USA",
                City = "Orlando",
                EmployeeID = 7014,
                Telephone = "(407) 555 -2014",
                Image = "people_circle13.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle13.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Rafael",
                Designation = "Program Manager",
                DateOfBirth = new DateTime(1987, 4, 11),
                About = "Rafael coordinates milestones and keeps the sample goals on track.",
                Country = "Spain",
                City = "Madrid",
                EmployeeID = 7015,
                Telephone = "(91) 555 -2015",
                Image = "people_circle14.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle14.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Sienna",
                Designation = "Interaction Designer",
                DateOfBirth = new DateTime(1995, 12, 5),
                About = "Sienna focuses on touch-friendly interaction design and spacing.",
                Country = "Canada",
                City = "Vancouver",
                EmployeeID = 7016,
                Telephone = "(604) 555 -2016",
                Image = "people_circle15.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle15.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Tara",
                Designation = "Technical Writer",
                DateOfBirth = new DateTime(1991, 3, 16),
                About = "Tara creates clear instructions and documentation for sample users.",
                Country = "UK",
                City = "Manchester",
                EmployeeID = 7017,
                Telephone = "(0161) 555 -2017",
                Image = "people_circle16.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle16.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Uma",
                Designation = "Customer Success",
                DateOfBirth = new DateTime(1996, 11, 23),
                About = "Uma gathers feedback and improves the demo experience for users.",
                Country = "India",
                City = "Bengaluru",
                EmployeeID = 7018,
                Telephone = "(080) 555 -2018",
                Image = "people_circle17.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle17.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Victor",
                Designation = "Security Engineer",
                DateOfBirth = new DateTime(1986, 7, 29),
                About = "Victor reviews sample code and supports secure behavior.",
                Country = "USA",
                City = "Las Vegas",
                EmployeeID = 7019,
                Telephone = "(702) 555 -2019",
                Image = "people_circle18.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle18.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Wendy",
                Designation = "Research Analyst",
                DateOfBirth = new DateTime(1994, 5, 20),
                About = "Wendy studies user behavior and suggests layout improvements.",
                Country = "USA",
                City = "Columbus",
                EmployeeID = 7020,
                Telephone = "(614) 555 -2020",
                Image = "people_circle19.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle19.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Xena",
                Designation = "Graphic Designer",
                DateOfBirth = new DateTime(1998, 9, 25),
                About = "Xena prepares visual assets that keep the sample attractive.",
                Country = "USA",
                City = "Tampa",
                EmployeeID = 7021,
                Telephone = "(813) 555 -2021",
                Image = "people_circle20.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle20.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Yara",
                Designation = "Operations Lead",
                DateOfBirth = new DateTime(1989, 2, 13),
                About = "Yara ensures the team delivers consistent sample quality.",
                Country = "UAE",
                City = "Dubai",
                EmployeeID = 7022,
                Telephone = "(04) 555 -2022",
                Image = "people_circle21.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle21.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Zara",
                Designation = "Marketing Specialist",
                DateOfBirth = new DateTime(1993, 8, 3),
                About = "Zara helps present product features in a clear and engaging way.",
                Country = "USA",
                City = "Nashville",
                EmployeeID = 7023,
                Telephone = "(615) 555 -2023",
                Image = "people_circle22.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle22.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Brian",
                Designation = "Business Analyst",
                DateOfBirth = new DateTime(1988, 10, 17),
                About = "Brian studies workflows and shapes the presentation strategy.",
                Country = "USA",
                City = "Salt Lake City",
                EmployeeID = 7024,
                Telephone = "(801) 555 -2024",
                Image = "people_circle23.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle23.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Cody",
                Designation = "Application Consultant",
                DateOfBirth = new DateTime(1992, 12, 30),
                About = "Cody supports configuration decisions and sample guidance.",
                Country = "USA",
                City = "Kansas City",
                EmployeeID = 7025,
                Telephone = "(816) 555 -2025",
                Image = "people_circle24.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle24.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Derek",
                Designation = "UX Researcher",
                DateOfBirth = new DateTime(1997, 6, 9),
                About = "Derek studies usability and recommends improvements for clarity.",
                Country = "USA",
                City = "Indianapolis",
                EmployeeID = 7026,
                Telephone = "(317) 555 -2026",
                Image = "people_circle25.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle25.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Evan",
                Designation = "Lead Developer",
                DateOfBirth = new DateTime(1985, 4, 22),
                About = "Evan oversees implementation of advanced grid features.",
                Country = "USA",
                City = "Cleveland",
                EmployeeID = 7027,
                Telephone = "(216) 555 -2027",
                Image = "people_circle26.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle26.png", ResourceAssembly),
            });

            employeeDetails.Add(new Employee()
            {
                Name = "Farid",
                Designation = "Release Engineer",
                DateOfBirth = new DateTime(1987, 11, 11),
                About = "Farid manages release readiness and package delivery.",
                Country = "UAE",
                City = "Abu Dhabi",
                EmployeeID = 7028,
                Telephone = "(02) 555 -2028",
                Image = "people_circle27.png",
                EmployeeImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.people_circle27.png", ResourceAssembly),
            });

            return employeeDetails;
        }

        #region Property Changed

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="propertyName">string type parameter propertyName</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion
    }
}
