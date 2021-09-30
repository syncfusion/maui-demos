#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BindableProperty.Create("SelectedItemColor", typeof(Color), typeof(Customization), Colors.RoyalBlue);


        public Customization()
        {
            InitializeComponent();
            this.BindingContext = this;

            this.TabView.IndicatorBackground = new SolidColorBrush(Colors.RoyalBlue);
            this.SelectedItemColor = (this.TabView.IndicatorBackground as SolidColorBrush).Color;
            byte r = Convert.ToByte(this.SelectedItemColor.Red * 255);
            byte g = Convert.ToByte(this.SelectedItemColor.Green * 255);
            byte b = Convert.ToByte(this.SelectedItemColor.Blue * 255);

            this.TabView.TabBarBackground = new SolidColorBrush(Color.FromRgba(r, g, b, 25));
        }

        private void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
        {
            var view = (sender as Syncfusion.Maui.TabView.SfTabView);

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

            this.SelectedItemColor = (view.IndicatorBackground as SolidColorBrush).Color;
            byte r = Convert.ToByte(this.SelectedItemColor.Red * 255);
            byte g = Convert.ToByte(this.SelectedItemColor.Green * 255);
            byte b = Convert.ToByte(this.SelectedItemColor.Blue * 255);

            view.TabBarBackground = new SolidColorBrush(Color.FromRgba(r, g, b, 25));
        }
    }

    public class TextToFormatTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = (value as string);

            string format = (parameter as string);

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
