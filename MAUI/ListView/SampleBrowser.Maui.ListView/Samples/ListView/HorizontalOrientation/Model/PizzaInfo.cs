#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class PizzaInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? pizzaName;
        private string? pizzaImage;

        #endregion

        #region Constructor

        public PizzaInfo()
        {

        }

        #endregion

        #region Properties

        public string? PizzaName
        {
            get { return pizzaName; }
            set
            {
                pizzaName = value;
                OnPropertyChanged("PizzaName");
            }
        }

        public string? PizzaImage
        {
            get
            {
                return pizzaImage;
            }

            set
            {
                pizzaImage = value;
                OnPropertyChanged("PizzaImage");
            }
        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
