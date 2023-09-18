#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Globalization;

namespace SampleBrowser.Maui.Picker.SfPicker;

public class StringValueToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string colorName = (string)value;
        Color color = Colors.Transparent;
        switch (colorName)
        {
            case "Pink":
                color = Colors.Pink;
                break;
            case "Green":
                color = Colors.Green;
                break;
            case "Blue":
                color = Colors.Blue;
                break;
            case "Yellow":
                color = Colors.Yellow;
                break;
            case "Orange":
                color = Colors.Orange;
                break;
            case "Purple":
                color = Colors.Purple;
                break;
            case "SkyBlue":
                color = Colors.SkyBlue;
                break;
            case "PaleGreen":
                color = Colors.PaleGreen;
                break;
            case "Gray":
                color = Colors.Gray;
                break;
            case "LiteGreen":
                color = Colors.LightGreen;
                break;
            case "Brown":
                color = Colors.Brown;
                break;
        }

        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return string.Empty;
    }
}
