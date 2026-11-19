namespace SampleBrowser.Maui.ImageEditor.SfImageEditor;

using System;
using System.Globalization;

/// <summary>
/// Represents a converter that converts the name of an icon to its corresponding Unicode character.
/// </summary>
public class ImageEditIconConverter : IValueConverter
{
    /// <summary>
    /// Converts the name of the icon to its corresponding Unicode character.
    /// </summary>
    /// <param name="value">The toolbar name.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">The paramater.</param>
    /// <param name="culture">The culture info.</param>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? name = value?.ToString();

        return name switch
        {
            "Reset" => "\ue746",
            "Undo" => "\ue744",
            "Redo" => "\ue745",
            "Save" => "\ue75f",
            "SaveEdit" => "\ue70c",
            "Add" => "\ue70d",
            "Replace" => "\ue726",
            "Bring Front" => "\ue764",
            "Send Backward" => "\ue70e",
            "Back" =>"\ue72d",
            _ => string.Empty
        };
    }

    /// <summary>
    /// Converts the value back to its original form.
    /// </summary>
    /// <param name="value">The toolbar name.</param>
    /// <param name="targetType">The target type.</param>
    /// <param name="parameter">The paramater.</param>
    /// <param name="culture">The culture info.</param>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}