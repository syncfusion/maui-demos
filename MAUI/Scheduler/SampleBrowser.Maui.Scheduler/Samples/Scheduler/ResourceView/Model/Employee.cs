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
    public class Employee : INotifyPropertyChanged
    {
        #region Fields

        private string name;
        private object? id;
        private Brush background;
        private Brush foreground;
        private string imageName;

        #endregion

        #region Constructor

        public Employee()
        {
            this.name = string.Empty;
            this.id = this.GetHashCode();
            this.background = Brush.Transparent;
            this.foreground = Brush.Black;
            this.imageName = string.Empty;  
        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.OnPropertyChanged(nameof(this.Name));
            }
        }

        public object? Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (this.id == value)
                {
                    return;
                }

                this.id = value;
                this.OnPropertyChanged(nameof(this.Id));
            }
        }


        public Brush Background
        {
            get
            {
                return this.background;
            }

            set
            {
                if (this.background == value)
                {
                    return;
                }

                this.background = value;
                this.OnPropertyChanged(nameof(this.Background));
            }
        }


        public Brush Foreground
        {
            get
            {
                return this.foreground;
            }

            set
            {
                if (this.foreground == value)
                {
                    return;
                }

                this.foreground = value;
                this.OnPropertyChanged(nameof(this.Foreground));
            }
        }

        public string ImageName
        {
            get
            {
                return this.imageName;
            }

            set
            {
                if (this.imageName == value)
                {
                    return;
                }

                this.imageName = value;
                this.OnPropertyChanged(nameof(this.ImageName));
            }
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}