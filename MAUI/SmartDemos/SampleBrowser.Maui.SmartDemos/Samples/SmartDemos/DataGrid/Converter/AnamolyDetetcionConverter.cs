#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataGrid;
namespace SampleBrowser.Maui.SmartDemos.SmartDemos;

internal class AnamolyDetetcionConverter : IValueConverter
{
    static string[]? data;
    public void GetString(string[] Data)
    {
        data = Data;
    }
    object IValueConverter.Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo info)
    {
        var gridCell = (value as DataGridCell);
        if (gridCell != null && data != null)
        {
            var cellValue = gridCell.CellValue;
            var rowData = (gridCell.DataColumn!.RowData as MachineData)!.AnomalyDescription;

            var mappingName = gridCell.DataColumn!.DataGridColumn!.MappingName;

            if (mappingName == "AnomalyDescription")
            {
                if (cellValue != null && data.Contains(cellValue))
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#A15C07");
                    }
                    else
                    {
                        return Color.FromArgb("#FFD6AE");
                    }
                }

                if (Application.Current!.UserAppTheme == AppTheme.Dark)
                {
                    return Color.FromArgb("#4F7A21");
                }
                else
                {
                    return Color.FromArgb("#D0F8AB");
                }
            }

            var conditions = new Dictionary<string, string>
            {
                { "Temperature", "temperature" },
                { "Voltage", "voltage" },
                { "Pressure", "pressure" },
                { "MotorSpeed", "motor speed" }
            };

            if (conditions.TryGetValue(mappingName, out var keyword) && rowData.Contains(keyword))
            {
                if (Application.Current!.UserAppTheme == AppTheme.Dark)
                {
                    return Color.FromArgb("#A15C07");
                }
                else
                {
                    return Color.FromArgb("#FFD6AE");
                }
            }
        }
        return Colors.Transparent;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        return null;
    }
}
