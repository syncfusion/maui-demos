#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Globalization;

namespace SampleBrowser.Maui.LiquidGlass.SfCartesianChart;

public partial class LanguageManagement : SampleView
{
	public LanguageManagement()
	{
		InitializeComponent();

#if IOS
        Content = new ChartsMobile();
#elif MACCATALYST
        Content = new ChartsDesktop();
#endif
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        var sampleView = Content as SampleView;

        if (sampleView != null)
        {
            sampleView.OnAppearing();
        }

    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        var sampleView = Content as SampleView;

        if (sampleView != null)
        {
            sampleView.OnDisappearing();
        }
    }
}

public class SunburstItemIndexConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IList<object> list && parameter is string paramStr && int.TryParse(paramStr, out int index))
        {
            return list.Count > index ? list[index]?.ToString() : string.Empty;
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class CountToPercentageConverter : IValueConverter
{
    public double DefaultTotal { get; set; } = 180d;

    public object? Convert(Object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;
        if (!double.TryParse(value.ToString(), out var count))
            return null;

        double total = DefaultTotal;
        if (parameter != null && double.TryParse(parameter.ToString(), out var pTotal))
            total = pTotal;

        if (total <= 0) return "0";
        var pct = (count / total) * 100d;

        // Return string formatted to 2 decimals; change as needed
        return $"{pct:0.#}%";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}