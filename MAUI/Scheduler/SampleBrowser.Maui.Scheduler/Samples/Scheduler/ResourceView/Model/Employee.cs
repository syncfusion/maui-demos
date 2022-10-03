#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class Employee
    {
        public Employee()
        {
            this.Name = string.Empty;
            this.Id = this.GetHashCode();
            this.Background = Brush.Transparent;
            this.Foreground = Brush.Black;
            this.ImageName = string.Empty;
        }
            
        #region Properties

        public string Name { get; set; }

        public object? Id { get; set; }

        public Brush Background { get; set; }

        public Brush Foreground { get; set; }

        public string ImageName { get; set; }

        #endregion
    }
}