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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class Team : INotifyPropertyChanged
    {
        #region Private Members

        private string? team;
        private string? location;
        private int wins;
        private int losses;
        private string? logo;
        private double pct;
        private int gb;

        #endregion

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Public properties

        /// <summary>
        /// Gets or sets the Team.
        /// </summary>
        /// <value>The Team.</value>
        public string? TeamName
        {
            get
            {
                return team;
            }

            set
            {
                team = value;
                RaisePropertyChanged("TeamName");
            }
        }

        /// <summary>
        /// Gets or sets the PCT.
        /// </summary>
        /// <value>The PCT.</value>
        public double PCT
        {
            get
            {
                return pct;
            }

            set
            {
                pct = value;
                RaisePropertyChanged("PCT");
            }
        }

        /// <summary>
        /// Gets or sets the GB.
        /// </summary>
        /// <value>The GB.</value>
        public int GB
        {
            get
            {
                return gb;
            }

            set
            {
                gb = value;
                RaisePropertyChanged("GB");
            }
        }

        /// <summary>
        /// Gets or sets the Wins.
        /// </summary>
        /// <value>The Wins.</value>
        public int Wins
        {
            get
            {
                return wins;
            }

            set
            {
                wins = value;
                RaisePropertyChanged("Wins");
            }
        }

        /// <summary>
        /// Gets or sets the Losses.
        /// </summary>
        /// <value>The Losses.</value>
        public int Losses
        {
            get
            {
                return losses;
            }

            set
            {
                losses = value;
                RaisePropertyChanged("Losses");
            }
        }

        /// <summary>
        /// Gets or sets the team image source.
        /// </summary>
        /// <value>The image source for team.</value>
        public string? Logo
        {
            get
            {
                return logo;
            }

            set
            {
                logo = value;
                RaisePropertyChanged("Logo");
            }
        }

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        /// <value>The Location.</value>
        public string? Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                RaisePropertyChanged("Location");
            }
        }

        #endregion

        public Team(string? teamname, double pct, int gb, int wins, int losses, string? logo,string? location)
        {
            this.TeamName = teamname;
            this.PCT = pct;
            this.GB = gb;
            this.Wins = wins;
            this.Losses = losses;
            this.Logo = logo;
            this.Location = location;
        }

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="propertyName">string type of parameter propertyName</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
