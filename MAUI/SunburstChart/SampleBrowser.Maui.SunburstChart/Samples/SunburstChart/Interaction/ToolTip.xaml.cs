#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Globalization;
namespace SampleBrowser.Maui.SunburstChart.SfSunburstChart;

public partial class ToolTip : SampleView
{
    public ToolTip()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        sunburstChart.Handler?.DisconnectHandler();
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