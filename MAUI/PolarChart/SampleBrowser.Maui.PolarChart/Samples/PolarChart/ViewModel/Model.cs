#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PolarChart.SfPolarChart
{
    public class ChartDataModel
    {
        public string? Category { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }

        #region Constructor

        public ChartDataModel() { }

        public ChartDataModel(string category, double value1, double value2, double value3)
        {
            Category = category;
            Value1 = value1;
            Value2 = value2;
            Value3 = value2;
        }

        public ChartDataModel(string category, double value1, double value2)
        {
            Category = category;
            Value1 = value1;
            Value2 = value2;
        }

        #endregion
    }
}