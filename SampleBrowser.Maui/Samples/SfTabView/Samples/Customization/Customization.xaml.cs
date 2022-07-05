#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.TabView;
using System;
using System.Globalization;

namespace SampleBrowser.Maui.SfTabView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Customization : SampleView
    {
        public Color SelectedItemColor
        {
            get { return (Color)GetValue(SelectedItemColorProperty); }
            set { SetValue(SelectedItemColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemColor.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty SelectedItemColorProperty =
            BindableProperty.Create(nameof(SelectedItemColor), typeof(Color), typeof(Customization), Colors.RoyalBlue);


        public Customization()
        {
            InitializeComponent();
            this.BindingContext = this;

            this.TabView.IndicatorBackground = new SolidColorBrush(Colors.RoyalBlue);
            this.SelectedItemColor = ((SolidColorBrush)this.TabView.IndicatorBackground).Color;
            int r = Convert.ToInt32(this.SelectedItemColor.Red * 255);
            int g = Convert.ToInt32(this.SelectedItemColor.Green * 255);
            int b = Convert.ToInt32(this.SelectedItemColor.Blue * 255);

            this.TabView.TabBarBackground = new SolidColorBrush(Color.FromRgba(r, g, b, 25));
        }

        private void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
        {
            var view = (Syncfusion.Maui.TabView.SfTabView)sender;

            SfTabItem item = view.Items[(int)e.NewIndex];

            var selectedFile = item.Header;

            if (item.Header == "Document")
            {
                view.IndicatorBackground = new SolidColorBrush(Colors.RoyalBlue);
            }
            else if (item.Header == "Excel")
            {
                view.IndicatorBackground = new SolidColorBrush(Colors.Green);
            }
            else if (item.Header == "PDF")
            {
                view.IndicatorBackground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (item.Header == "PowerPoint")
            {
                view.IndicatorBackground = new SolidColorBrush(Colors.Red);
            }

            this.SelectedItemColor = ((SolidColorBrush)view.IndicatorBackground).Color;
            int r = Convert.ToInt32(this.SelectedItemColor.Red * 255);
            int g = Convert.ToInt32(this.SelectedItemColor.Green * 255);
            int b = Convert.ToInt32(this.SelectedItemColor.Blue * 255);

            view.TabBarBackground = new SolidColorBrush(Color.FromRgba(r, g, b, 25));
        }
    }

    public class TextToFormatTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = (string)value;

            string format = (string)parameter;

            if (format == ".docx")
            {
                fileName += " Document File";
            }
            else if (format == ".xlsx")
            {
                fileName += " Excel File";
            }
            else if (format == ".pdf")
            {
                fileName += " PDF File";
            }
            else if (format == ".pptx")
            {
                fileName += " PowerPoint File";
            }

            return fileName += format;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
