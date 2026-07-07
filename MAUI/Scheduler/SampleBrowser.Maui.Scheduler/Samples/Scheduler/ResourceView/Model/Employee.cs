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