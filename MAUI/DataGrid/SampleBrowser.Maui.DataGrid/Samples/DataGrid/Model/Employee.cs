#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class Employee : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables

        private int employeeID;

        private string? firstName;

        private string? designation;

        private DateTime birthDate;

        private string? image;

        private string? city;

        private string? country;
        private string? telePhone;

        private string? about;
        private string? email;
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        #endregion

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Public Properties

        /// <summary>
        /// Gets or sets the value of EmployeeID and notifies user when value gets changed
        /// </summary>
        public int EmployeeID
        {
            get
            {
                return this.employeeID;
            }

            set
            {
                this.employeeID = value;
                this.RaisePropertyChanged("EmployeeID");
            }
        }

        /// <summary>
        /// Gets or sets the value of Designation and notifies user when value gets changed
        /// </summary>
        public string? Designation
        {
            get
            {
                return this.designation;
            }

            set
            {
                this.designation = value;
                this.RaisePropertyChanged("Designation");
            }
        }

        /// <summary>
        /// Gets or sets the value of Name and notifies user when value gets changed
        /// </summary>
        public string? Name
        {
            get
            {
                return this.firstName;
            }

            set
            {
                this.firstName = value;
                this.RaisePropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Gets or sets the value of DateOfBirth and notifies user when value gets changed
        /// </summary>
        public DateTime DateOfBirth
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                this.birthDate = value;
                this.RaisePropertyChanged("DateOfBirth");
            }
        }

        /// <summary>
        /// Gets or sets the value of Email and notifies user when value gets changed
        /// </summary>
        public string? EMail
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged("EMail");
            }
        }

        /// <summary>
        /// Gets or sets the value of Image and notifies user when value gets changed
        /// </summary>
        public string? Image
        {
            get
            {
                return this.image;
            }

            set
            {
                this.image = value;
                this.RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the value of City and notifies user when value gets changed
        /// </summary>
        public string? City
        {
            get
            {
                return this.city;
            }

            set
            {
                this.city = value;
                this.RaisePropertyChanged("City");
            }
        }

        /// <summary>
        /// Gets or sets the value of Country and notifies user when value gets changed
        /// </summary>
        public string? Country
        {
            get
            {
                return this.country;
            }

            set
            {
                this.country = value;
                this.RaisePropertyChanged("Country");
            }
        }

        /// <summary>
        /// Gets or sets the value of Telephone and notifies user when value gets changed
        /// </summary>
        public string? Telephone
        {
            get
            {
                return this.telePhone;
            }

            set
            {
                this.telePhone = value;
                this.RaisePropertyChanged("Telephone");
            }
        }

        /// <summary>
        /// Gets or sets the value of About and notifies user when value gets changed
        /// </summary>
        public string? About
        {
            get
            {
                return this.about;
            }

            set
            {
                this.about = value;
                this.RaisePropertyChanged("About");
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="name">string type parameter name</param>
        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region IDataErrorInfo
        [Bindable(false)]
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName == "EMail" && this.EMail != null)
                {
                    return !emailRegex.IsMatch(this.EMail) ? "Email ID is invalid!" : string.Empty;
                }
                return string.Empty;
            }
        }
        #endregion   
    }
}
