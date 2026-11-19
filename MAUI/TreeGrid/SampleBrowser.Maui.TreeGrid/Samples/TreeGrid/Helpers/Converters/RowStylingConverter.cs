using System.Globalization;
using System.Collections;
using Syncfusion.Maui.TreeGrid;

namespace SampleBrowser.Maui.TreeGrid;

/// <summary>
/// Converter to apply different colors for different hierarchy levels (0-2) in TreeGrid.
/// Each level has a distinct color with reduced opacity to create a visual hierarchy.
/// </summary>
public class RowStylingConverter : IValueConverter
{

    /// <summary>
    /// Converts tree grid row data to a color based on the hierarchy level.
    /// </summary>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TreeGridRow row)
        {
            var rowData = row.DataRow;

            if (rowData != null)
            {
                if (rowData.Level == 0)
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#661849A9");
                    }
                    else
                    {
                        return Color.FromArgb("#66C4D4FD");
                    }
                }

                if (rowData.Level == 1)
                    return Colors.Transparent;
            }

            return Colors.Transparent;
        }

        return Colors.Transparent;
    }

    /// <summary>
    /// ConvertBack is not supported for this converter.
    /// </summary>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

