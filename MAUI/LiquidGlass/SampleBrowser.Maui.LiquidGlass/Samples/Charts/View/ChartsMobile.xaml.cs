#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.LiquidGlass.SfCartesianChart;

public partial class ChartsMobile : SampleView
{
	public ChartsMobile()
	{
		InitializeComponent();
	}

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        cartesianChart.Handler?.DisconnectHandler();
        funnelChart.Handler?.DisconnectHandler();
        sunburstChart.Handler?.DisconnectHandler();
    }
}

public class CustomDateTimeAxis : DateTimeAxis
{
    private readonly (int Month, int Day)[] _fixedDates =
    {
        (1, 5),  
        (3, 1),  
        (5, 5), 
        (7, 12),  
        (9, 10),
        (11, 6) 
    };
 
    protected override void OnCreateLabels()
    {
        if (VisibleLabels == null)
            return;
 
        VisibleLabels.Clear();
 
        double minVal = VisibleMinimum;
        double maxVal = VisibleMaximum;
 
        DateTime min = FromOADate(minVal);
        DateTime max = FromOADate(maxVal);
 
        if (min >= max)
            return;
 
        for (int year = min.Year; year <= max.Year; year++)
        {
            foreach (var (month, day) in _fixedDates)
            {
                DateTime date;
                try
                {
                    date = new DateTime(year, month, day);
                }
                catch
                {
                    continue;
                }
 
                if (date < min || date > max)
                    continue;

                string text = date.ToString("MMM-dd");
 
                AddLabel(date, text);
            }
        }
    }
 
    private void AddLabel(DateTime date, string text)
    {
        double value = ToOADate(date);
 
        var style = new ChartAxisLabelStyle();
 
        VisibleLabels.Add(new ChartAxisLabel(value, text));
    }
 
    private static readonly DateTime OaBase = new DateTime(1899, 12, 30);
    private static double ToOADate(DateTime date) => (date - OaBase).TotalDays;
    private static DateTime FromOADate(double oa) => OaBase.AddDays(oa);
}