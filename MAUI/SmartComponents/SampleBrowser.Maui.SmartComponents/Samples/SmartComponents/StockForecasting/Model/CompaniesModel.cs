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
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class CompaniesModel : INotifyPropertyChanged
    {
        public double High { get; set; }

        public double Low { get; set; }

        public DateTime Date { get; set; }

        public double Open { get; set; }

        public double Close { get; set; }

        public CompaniesModel()
        {

        }
        public CompaniesModel(DateTime date, double high, double low, double open, double close)
        {
            Date = date;
            High = high;
            Low = low;
            Open = open;
            Close = close;
        }

        private string name = string.Empty;
        private string desc = string.Empty;
        private double stockPrice;
        private double diffPercent;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return desc; }
            set
            {
                desc = value;
                OnPropertyChanged("Description");
            }
        }

        public double StockPrice
        {
            get { return stockPrice; }
            set
            {
                stockPrice = value;
                OnPropertyChanged("StockPrice");
            }
        }

        public double DifferenceInPercent
        {
            get { return diffPercent; }
            set
            {
                diffPercent = value;
                OnPropertyChanged("DifferenceInPercent");
            }
        }

        private string? companyIcon;

        public string? CompanyIcon
        {
            get { return companyIcon; }
            set
            {
                companyIcon = value;
                OnPropertyChanged("CompanyIcon");
            }
        }

        public CompaniesModel(string name, string desc, double stkPrice, double percent, string img)
        {
            Name = name;
            Description = desc;
            StockPrice = stkPrice;
            DifferenceInPercent = percent;
            CompanyIcon = img;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
