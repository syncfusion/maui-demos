#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfAccordion
{
    public class EmployeeInfo
    {
        #region Constructor

        public EmployeeInfo(string name, string image, string position, string organizationunit, string dateofbirth, string location, string phone, bool isexpanded, string description)
        {
            Name = name;
            Image = image;
            Position = position;
            OrganizationUnit = organizationunit;
            DateOfBirth = dateofbirth;
            Location = location;
            Phone = phone;
            IsExpanded = isexpanded;
            Description = description;
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Image { get; set; }

        public string Position { get; set; }

        public string OrganizationUnit { get; set; }

        public string DateOfBirth { get; set; }

        public string Location { get; set; }

        public string Phone { get; set; }

        public bool IsExpanded { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
