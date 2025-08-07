#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer.Samples.PdfViewer.ShowAndHideFormField.View
{
    public class ListItem
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string DropdownIcon { get; set; }
        public bool IsExpanded { get; set; }
        public Color EllipseColor { get; set; }
        public bool IsFirstItem { get; set; }

        // Constructor to ensure Email is set
        public ListItem(string name, string email, string dropdownIcon, Color color, bool visible)
        {

            Name = name;
            Email = email;  // Ensure Email is not null
            DropdownIcon = dropdownIcon;
            EllipseColor = color;
            IsFirstItem = visible;
        }
        public void ToggleExpandCollapse(ListItem item)
        {
            item.IsExpanded = !item.IsExpanded;
        }

    }

}
