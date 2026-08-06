using System.Globalization;

namespace SampleBrowser.Maui.RichTextEditor.RichTextEditor
{
    public class TextToVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is NoteModel newNote && newNote.IsNewNote)
            {
                return true;
            }
            if (value is NoteModel noteModel && string.IsNullOrEmpty(noteModel.Title) && string.IsNullOrEmpty(noteModel.HtmlText))
            {
                return false;
            }
            if (value is string text && string.IsNullOrEmpty(text))
            {
                return false;
            }
            return true;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return true;
        }
    }

    public class InverseBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            else
            {
                return false;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            else
            {
                return false;
            }
        }
    }
}
