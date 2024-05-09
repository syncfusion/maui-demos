#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System.Globalization;

namespace SampleBrowser.Maui.PolarChart.SfPolarChart;

public partial class PolarDataLabel : SampleView
{
    public PolarDataLabel()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        Chart.Handler?.DisconnectHandler();
    }
}

public class IconColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
       if (value is PolarSeries series)
        {
           if(series.Label == "Optimum Health")
            {
                return new SolidColorBrush(Color.FromRgb(0xDF, 0xD4, 0xFF));

            }
            else if (series.Label == "Actual Health")
            {
                return new SolidColorBrush(Color.FromRgb(0x97, 0x30, 0xE7));
            }
        }
      
        return Colors.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class PolarSeriesExt : PolarAreaSeries
{
    protected override void DrawDataLabel(ICanvas canvas, Brush? fillcolor, string label, PointF point, int index)
    {
        if(label != "53")
        {
            base.DrawDataLabel(canvas, fillcolor, label, point, index);
        }
    }
}
