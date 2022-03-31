#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SfCircularChart
{
    public class CenterViewDoughnut : PieSeries
    {

        protected override void DrawDataLabel(ICanvas canvas, Brush? fillcolor, string label, PointF point, int index)
        {
            if (this.ItemsSource is ObservableCollection<IncomeAndExpenses> items)
                label = ((items[index].Value / 41893) * 100).ToString("#") + "%";

            base.DrawDataLabel(canvas, fillcolor, label, point, index);
        }
    }

    public class DoughnutChartExt : PieSeries
    {
        protected override void DrawDataLabel(ICanvas canvas, Brush? fillcolor, string label, PointF point, int index)
        {
            if (this.ItemsSource is ObservableCollection<IncomeAndExpenses> items)
            {
                var data = (items[index]);
                label = data.Name + ":" + data.Percentage.ToString("#") + "%";
            }

            base.DrawDataLabel(canvas, fillcolor, label, point, index);
        }
    }

    public class SmartLabelsViewModel
    {
        public ObservableCollection<IncomeAndExpenses> DataSource { get; set; }

        public ObservableCollection<IncomeAndExpenses> SubDataSource { get; set; }
        public ObservableCollection<Brush> PaletteBrushes { get; set; }
        public ObservableCollection<Brush> SubPaletteBrushes { get; set; }

        public SmartLabelsViewModel()
        {
            PaletteBrushes = new ObservableCollection<Brush>()
            {
               new SolidColorBrush(Color.FromArgb("#314A6E")),
                 new SolidColorBrush(Color.FromArgb("#48988B")),
                 new SolidColorBrush(Color.FromArgb("#5E498C")),
                 new SolidColorBrush(Color.FromArgb("#74BD6F")),
                 new SolidColorBrush(Color.FromArgb("#597FCA")),
            };

            DataSource = new ObservableCollection<IncomeAndExpenses>()
            {
                new IncomeAndExpenses("Food", 7923),
                new IncomeAndExpenses("Housing", 10927),
                new IncomeAndExpenses("Entertainment",4793),
                new IncomeAndExpenses("Healthcare", 3577),
                new IncomeAndExpenses("Others", 14673)
            };

            if (RunTimeDevice.IsMobileDevice())
            {
                SubDataSource = new ObservableCollection<IncomeAndExpenses>()
            {
                new IncomeAndExpenses("Food","At home", 4464, 7923),
                new IncomeAndExpenses("Food","Outside",3459, 7923),
                new IncomeAndExpenses("Housing","Owned",6678, 10927),
                new IncomeAndExpenses("Housing","Rented",4249 , 10927),

                new IncomeAndExpenses("Entertainment","Movie",3975, 4793),
                new IncomeAndExpenses("Entertainment","Toys",818,4793),
                new IncomeAndExpenses("Healthcare","Services",3405,3577),
                new IncomeAndExpenses("Healthcare","Supplies",172,3577),

                new IncomeAndExpenses("Others","Care products",768, 14673),
                new IncomeAndExpenses("Others","Reading",108, 14673),
                new IncomeAndExpenses("Others","Education",1407 ,14673),
                new IncomeAndExpenses("Others","Tobacco supplies",347, 14673),
                new IncomeAndExpenses("Others","Miscellaneous",993, 14673),
                new IncomeAndExpenses("Others","Cash contributions",1888 , 14673),
                new IncomeAndExpenses("Others","Insurance",7296, 14673),
                new IncomeAndExpenses("Others","Clothing",1866, 14673),
            };
            }
            else
            {
                SubDataSource = new ObservableCollection<IncomeAndExpenses>()
            {
                new IncomeAndExpenses("Food","At home", 4464, 7923),
                new IncomeAndExpenses("Food","Outside",3459, 7923),
                new IncomeAndExpenses("Housing","Owned",6678, 10927),
                new IncomeAndExpenses("Housing","Rented",4249 , 10927),

                new IncomeAndExpenses("Entertainment","Movie",3975, 4793),
                new IncomeAndExpenses("Entertainment","Toys",818,4793),
                new IncomeAndExpenses("Healthcare","Services",1609,3577),
                new IncomeAndExpenses("Healthcare","Supplies",1967,3577),

                new IncomeAndExpenses("Others","Miscellaneous",993, 14673),
                new IncomeAndExpenses("Others","Cash contributions",1888 , 14673),
                new IncomeAndExpenses("Others","Insurance",7296, 14673),
                new IncomeAndExpenses("Others","Clothing",1866, 14673),
                new IncomeAndExpenses("Others","Care products",768, 14673),
                new IncomeAndExpenses("Others","Reading",108, 14673),
                new IncomeAndExpenses("Others","Education",1407 ,14673),
                new IncomeAndExpenses("Others","Tobacco supplies",347, 14673),
            };
            }

            SubPaletteBrushes = new ObservableCollection<Brush>();

            foreach (var item in SubDataSource)
            {
                switch (item.Category)
                {
                    case "Food":
                        SubPaletteBrushes.Add(PaletteBrushes[0]);
                        break;
                    case "Housing":
                        SubPaletteBrushes.Add(PaletteBrushes[1]);
                        break;
                    case "Entertainment":
                        SubPaletteBrushes.Add(PaletteBrushes[2]);
                        break;
                    case "Healthcare":
                        SubPaletteBrushes.Add(PaletteBrushes[3]);
                        break;
                    case "Others":
                        SubPaletteBrushes.Add(PaletteBrushes[4]);
                        break;
                }
            }
        }
    }

    public class IncomeAndExpenses : ChartDataModel
    {
        public string Category { get; set; }

        public double Percentage { get { return (Value / Size) * 100; } }

        public IncomeAndExpenses(string category, string name, double value, double total) : base(name, value, total)
        {
            Category = category;
        }

        public IncomeAndExpenses(string category, double value)
        {
            Category = category;
            Value = value;
        }
    }
}
