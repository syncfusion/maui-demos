#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class ZoomViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> ZoomData { get; set; }

        public ZoomViewModel()
        {
            DateTime date = new DateTime(1950, 3, 01);
            Random r = new Random();
            ZoomData = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 20; i++)
            {
                ZoomData.Add(new ChartDataModel(date, r.Next(45, 75)));
                date = date.AddDays(5);
            }
        }
    }
}
