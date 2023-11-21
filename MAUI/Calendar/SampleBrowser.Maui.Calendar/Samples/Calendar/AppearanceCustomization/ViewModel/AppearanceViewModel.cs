#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;
using System.Globalization;
using System.Xml;

namespace SampleBrowser.Maui.Calendar.SfCalendar;

public class AppearanceViewModel : INotifyPropertyChanged
{
    private DataTemplate circleTemplate;

    private DataTemplate rectTemplate;

    private DataTemplate template;

    public event PropertyChangedEventHandler? PropertyChanged;

    public DataTemplate Template
    {
        get
        {
            return this.template;
        }
        set
        {
            this.template = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Template)));
        }
    }

    public AppearanceViewModel()
    {
        this.circleTemplate = new DataTemplate(() =>
        {
            Grid grid = new Grid();

            Border border = new Border();
            border.BackgroundColor = Color.FromRgba("#F5F5F5");
            border.StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(25)
            };

            border.SetBinding(Border.StrokeThicknessProperty, "Date", converter: new DateToStrokeConverter());
            border.Stroke = Color.FromArgb("#0A3A74");

            Label label = new Label();
            label.SetBinding(Label.TextProperty, "Date.Day");
            label.HorizontalOptions = LayoutOptions.Center;
            label.VerticalOptions = LayoutOptions.Center;
            label.Padding = new Thickness(2);
            border.Content = label;

            grid.Add(border);
            grid.Padding = new Thickness(1);

            return grid;
        });

        this.rectTemplate = new DataTemplate(() =>
        {
            Grid grid = new Grid();

            Border border = new Border();
            border.BackgroundColor = Color.FromRgba("#F5F5F5");
            border.StrokeShape = new RoundRectangle()
            {
                CornerRadius = new CornerRadius(2)
            };

            border.SetBinding(Border.StrokeThicknessProperty, "Date", converter: new DateToStrokeConverter());
            border.Stroke = Color.FromArgb("#0A3A74");

            Label label = new Label();
            label.SetBinding(Label.TextProperty, "Date.Day");
            label.HorizontalOptions = LayoutOptions.Center;
            label.VerticalOptions = LayoutOptions.Center;
            border.Content = label;

            grid.Add(border);
            grid.Padding = new Thickness(1);

            return grid;
        });

        this.template = this.circleTemplate;
    }

    public void UpdateSelectionShape(bool isCircleShape)
    {
        this.Template = isCircleShape ? this.circleTemplate : this.rectTemplate;
    }
}

internal class DateToStrokeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var date = value as DateTime?;
        if (date.HasValue && date.Value.Date == DateTime.Now.Date)
        {
            return 2;
        }

        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}
