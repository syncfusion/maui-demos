#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.Maui.Controls;
using System.Collections;

namespace SampleBrowser.Maui.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class OrdersReport : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public OrdersReport()
        {
            InitializeComponent();
            assembly = typeof(OrdersReport).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Performs Mail merge for nested groups in Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string basePath = "SampleBrowser.Maui.Resources.DocIO.";
            string dataPath = basePath + @"TemplateLetter.docx";
            //Creates a new Word document instance.
            using WordDocument document = new();
            Stream fileStream = assembly.GetManifestResourceStream(dataPath);
            //Opens an existing Word document.
            document.Open(fileStream, FormatType.Doc);
            fileStream.Dispose();

            //Inserts page break at last paragraph, to populate each employee details in new page.
            Break pageBreak = new(document, BreakType.PageBreak);
            document.LastParagraph.ChildEntities.Insert(0, pageBreak);

            #region Execute Mail merge
            MailMergeDataSet dataSet = GetMailMergeDataSet(basePath);
            List<DictionaryEntry> commands = new();
            //DictionaryEntry contain "Source table" (KEY) and "Command" (VALUE)
            DictionaryEntry entry = new("Employees", string.Empty);
            commands.Add(entry);

            //Retrives customer details.
            entry = new DictionaryEntry("Customers", "EmployeeID = %Employees.EmployeeID%");
            commands.Add(entry);

            //Retrives order details.
            entry = new DictionaryEntry("Orders", "CustomerID = %Customers.CustomerID%");
            commands.Add(entry);

            //Executes nested Mail merge using explicit relational data.
            document.MailMerge.ExecuteNestedGroup(dataSet, commands);
            #endregion

            //Clears the items (Page break) in last paragraph to remove empty page after execution.
            document.LastParagraph.ChildEntities.Clear();

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Orders Report.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string dataPath = "SampleBrowser.Maui.Resources.DocIO.TemplateLetter.docx";
            //Loads template document stream.
            using Stream fileStream = assembly.GetManifestResourceStream(dataPath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("TemplateLetter.docx", "application/msword", ms);
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Gets the mail merge data set.
        /// </summary>
        private MailMergeDataSet GetMailMergeDataSet(string basePath)
        {
            List<EmployeeDetails> employees = new();
            List<CustomerDetails> customers = new();
            List<OrderDetails> orders = new();
            //Reads data from xml.
            Stream stream = assembly.GetManifestResourceStream(basePath + @"Employees.xml");

            XmlSerializer serializer = new(typeof(EmployeesList));
            //Deserializes XML into DOM.
            EmployeesList employeesList = (EmployeesList)serializer.Deserialize(stream);
            //Gets list of employees.
            foreach (EmployeeDetails employee in employeesList.Employees)
            {
                employees.Add(employee);
                //Gets list of customers.
                foreach (CustomerDetails customer in employee.Customers)
                {
                    customers.Add(customer);
                    //Gets list of orders.
                    foreach (OrderDetails order in customer.Orders)
                    {
                        orders.Add(order);
                    }
                }
            }
            //Creates MailMergeDataSet.
            MailMergeDataSet dataSet = new();
            dataSet.Add(new MailMergeDataTable("Employees", employees));
            dataSet.Add(new MailMergeDataTable("Customers", customers));
            dataSet.Add(new MailMergeDataTable("Orders", orders));
            return dataSet;
        }
        #endregion
    }

    #region Helper classes
    /// <summary>
    /// Helper class to maintain collection of employees.
    /// </summary>
    public class EmployeesList
    {
        #region Fields
        private EmployeeDetails[] employees;
        #endregion

        #region Properties
        [System.Xml.Serialization.XmlElementAttribute("Employees")]
        public EmployeeDetails[] Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// Helper class to maintain employee details.
    /// </summary>
    public class EmployeeDetails
    {
        #region Fields
        private string firstName;
        private string lastName;
        private string employeeID;
        private string extension;
        private string address;
        private string city;
        private string country;
        private CustomerDetails[] customers;
        #endregion

        #region Properties
        public string FirstName
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
        public string LastName
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
        public string EmployeeID
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
        public string Extension
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
        public string Address
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
        public string City
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
        public string Country
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
        [System.Xml.Serialization.XmlElementAttribute("Customers")]
        public CustomerDetails[] Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// Helper class to maintain customer details.
    /// </summary>
    public class CustomerDetails
    {
        #region Fields
        private string customerID;
        private string employeeID;
        private string companyName;
        private string contactName;
        private string city;
        private string country;
        private OrderDetails[] orders;
        #endregion

        #region Properties
        public string CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }
        public string EmployeeID
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
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }
        public string ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                contactName = value;
            }
        }
        public string City
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
        public string Country
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
        [System.Xml.Serialization.XmlElementAttribute("Orders")]
        public OrderDetails[] Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
            }
        }
        #endregion
    }
    /// <summary>
    /// Helper class to maintain order details.
    /// </summary>
    public class OrderDetails
    {
        #region Fields
        private string orderID;
        private string customerID;
        private string orderDate;
        private string shippedDate;
        private string requiredDate;
        #endregion

        #region Properties
        public string OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                orderID = value;
            }
        }
        public string CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }
        public string OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
            }
        }
        public string ShippedDate
        {
            get
            {
                return shippedDate;
            }
            set
            {
                shippedDate = value;
            }
        }
        public string RequiredDate
        {
            get
            {
                return requiredDate;
            }
            set
            {
                requiredDate = value;
            }
        }
        #endregion
    }
    #endregion
}