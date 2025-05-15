#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataGrid;
using System.Globalization;
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{

    public class DataPredictionConverter : IValueConverter
    {

        object IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo info)
        {
            var gridCell = (value as DataGridCell);
            if (gridCell != null && gridCell.CellValue != null)
            {
                var rowData = (gridCell.DataColumn!.RowData as GenerateModel)!.TotalGrade;
                var mappingName = gridCell.DataColumn!.DataGridColumn!.MappingName;
               
                if ((mappingName == "FinalYearGPA" || mappingName == "TotalGPA") )
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#2B4212");
                    }
                    else
                    {
                        return Color.FromArgb("#A6EF67");
                    }
                }
                else
                {
                    if (mappingName == "TotalGrade")
                    {
                        if (rowData != null)
                        {
                            if (rowData == "A+" || rowData == "A")
                            {
                                if (Application.Current!.UserAppTheme == AppTheme.Dark)
                                {
                                    return Color.FromArgb("#283618");
                                }
                                else
                                {
                                    return Color.FromArgb("#D0EF84");
                                }
                            }
                            else if (rowData == "B" || rowData == "B+")
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
                            else
                            {
                                if (Application.Current!.UserAppTheme == AppTheme.Dark)
                                {
                                    return Color.FromArgb("#df2935");
                                }
                                else
                                {
                                    return Color.FromArgb("#f08080");
                                }
                            }
                        }
                    }
                }


            }
            return Colors.Transparent;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
