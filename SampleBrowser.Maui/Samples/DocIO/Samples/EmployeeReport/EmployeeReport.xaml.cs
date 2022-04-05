#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using SampleBrowser.Maui.Services;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace SampleBrowser.Maui.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class EmployeeReport : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EmployeeReport()
        {
            InitializeComponent();
            assembly = typeof(EmployeeReport).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        #region  EmployeeReport
        /// <summary>
        /// Generates an employee report using Mail merge.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string dataPathEmployee = @"SampleBrowser.Maui.Resources.DocIO.EmployeesReportDemo.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(dataPathEmployee);
            //Creates a new Word document instance.
            using WordDocument document = new();
            //Opens an existing Word document.
            document.Open(fileStream, FormatType.Doc);
            document.MailMerge.MergeImageField += new MergeImageFieldEventHandler(MergeField_EmployeeImage);
            //Creates MailMergeDataTable.
            MailMergeDataTable mailMergeDataTable = GetMailMergeDataTableEmployee();
            //Executes Mail Merge with groups. 
            document.MailMerge.ExecuteGroup(mailMergeDataTable);

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Employee Report.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        /// <summary>
        /// Binds image from file system during mail merge.
        /// </summary>
        private void MergeField_EmployeeImage(object sender, MergeImageFieldEventArgs args)
        {
            //Gets the image from disk during Merge.
            if (args.FieldName == "Photo")
            {
                string? ProductFileName = args.FieldValue.ToString();
                if(ProductFileName != null)
                {
                    byte[] bytes = Convert.FromBase64String(ProductFileName);
                    MemoryStream ms = new(bytes);
                    args.ImageStream = ms;
                }
            }
        }
        #endregion  EmployeeReport
        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string dataPathEmployee = @"SampleBrowser.Maui.Resources.DocIO.EmployeesReportDemo.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(dataPathEmployee);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EmployeesReportDemo.docx", "application/msword", ms);
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Gets the MailMergeDataTable.
        /// </summary>        
        private MailMergeDataTable GetMailMergeDataTableEmployee()
        {
            List<Employee> employees = new();
            //Reads data from xml.
            Stream? fileStream = assembly.GetManifestResourceStream(@"SampleBrowser.Maui.Resources.DocIO.EmployeesList.xml");

            XmlSerializer serializer = new(typeof(Employees));

            if (fileStream != null)
            {
                XmlReader reader = XmlReader.Create(fileStream);
                //Deserializes XML into DOM.
                Employees? employeesList = serializer.Deserialize(reader) as Employees;

                if (employeesList != null && employeesList.Employee != null)
                {
                    foreach (Employee employee in employeesList.Employee)
                        employees.Add(employee);
                }
            }
            //Creates MailMergeDataTable from xml.
            MailMergeDataTable dataTable = new("Employees", employees);
            fileStream!.Dispose();
            return dataTable;
        }
        #endregion
    }

    #region Helper class
    /// <summary>
    /// Helper class to maintain collection of employee details.
    /// </summary>
    public class Employees
    {
        #region Fields
        private Employee[]? employee;
        #endregion

        #region Properties
        [System.Xml.Serialization.XmlElementAttribute("Employee")]
        public Employee[]? Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }
        #endregion
    }
    /// <summary>
    ///  Helper class to maintain employee details.
    /// </summary>
    public class Employee
    {
        #region Fields
        private string? employeeID;
        private string? lastName;
        private string? firstName;
        private string? title;
        private string? titleOfCourtesy;
        private string? birthDate;
        private string? hireDate;
        private string? address;
        private string? city;
        private string? region;
        private string? postalCode;
        private string? country;
        private string? homePhone;
        private string? extension;
        private string? photo;
        private string? notes;
        private string? reportsTo;
        #endregion

        #region Properties
        public string? EmployeeID
        {
            get
            {
                return employeeID;
            }
            set
            {
                employeeID = value;
            }
        }
        public string? LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        public string? FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }
        public string? Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string? TitleOfCourtesy
        {
            get
            {
                return titleOfCourtesy;
            }
            set
            {
                titleOfCourtesy = value;
            }
        }
        public string? BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
            }
        }
        public string? HireDate
        {
            get
            {
                return hireDate;
            }
            set
            {
                hireDate = value;
            }
        }
        public string? Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public string? City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string? Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value;
            }
        }
        public string? PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                postalCode = value;
            }
        }
        public string? Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public string? HomePhone
        {
            get
            {
                return homePhone;
            }
            set
            {
                homePhone = value;
            }
        }
        public string? Extension
        {
            get
            {
                return extension;
            }
            set
            {
                extension = value;
            }
        }
        public string? Photo
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value;
            }
        }
        public string? Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }
        public string? ReportsTo
        {
            get
            {
                return reportsTo;
            }
            set
            {
                reportsTo = value;
            }
        }
        #endregion
    }
    #endregion
}