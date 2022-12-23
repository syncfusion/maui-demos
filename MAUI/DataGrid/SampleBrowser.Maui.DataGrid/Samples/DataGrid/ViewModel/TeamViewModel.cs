#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class TeamViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<Team> data;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the GettingStartedViewModel class. 
        /// </summary>
        public TeamViewModel()
        {
            this.data = new ObservableCollection<Team>();
            this.AddRows();
        }

        #endregion
        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the Data.
        /// </summary>
        /// <value>The Data.</value>
        public ObservableCollection<Team> Data
        {
            get { return this.data; }
        }

        #region updating code

        /// <summary>
        /// Adds the rows.
        /// </summary>
        private void AddRows()
        {
            this.data.Add(new Team("Cavaliers", .616, 0, 93, 58, "cavaliers.png", "East"));
            this.data.Add(new Team("Clippers", .550, 10, 82, 67, "clippers.png", "West"));
            this.data.Add(new Team("Denver", .514, 15, 76, 72, "denvernuggets.png", "Central"));
            this.data.Add(new Team("Detroit", .513, 15, 77, 73, "detroitpistons.png", "East"));
            this.data.Add(new Team("Golden State", .347, 40, 52, 98, "goldenstate.png", "West"));
            this.data.Add(new Team("Los Angeles", .560, 0, 84, 66, "losangeles.png", "Central"));
            this.data.Add(new Team("Mavericks", .547, 2, 82, 68, "mavericks.png", "East"));
            this.data.Add(new Team("Memphis", .540, 3, 81, 69, "memphis.png", "West"));
            this.data.Add(new Team("Miami", .464, 14, 70, 81, "miami.png", "Central"));
            this.data.Add(new Team("Milwakke", .433, 19, 65, 85, "milwakke.png", "East"));
            this.data.Add(new Team("New York", .642, 0, 97, 54, "newyork.png", "West"));
            this.data.Add(new Team("Orlando", .510, 20, 77, 74, "orlando.png", "Central"));
            this.data.Add(new Team("Thunder", .480, 24, 72, 78, "thunder_logo.png", "East"));
        }

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
    }
}
