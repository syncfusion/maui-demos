#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Gauges;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleBrowser.Maui.RadialSlider
{
    public partial class StylesRadialSlider : SampleView, INotifyPropertyChanged
    {
        public StylesRadialSlider()
        {
            InitializeComponent();
            BindingContext = this;
        }

        #region Events
        private double gaugeHeight;

        public double GaugeHeight
        {
            get { return gaugeHeight; }
            set { gaugeHeight = value; NotifyPropertyChanged(); }
        }

        private double gaugeWidth;

        public double GaugeWidth
        {
            get { return gaugeWidth; }
            set { gaugeWidth = value; NotifyPropertyChanged(); }
        }

        protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
        {
            if (widthConstraint > heightConstraint)
            {
                rootStackLayout.Orientation = StackOrientation.Vertical;
                stackLayout1.Orientation = StackOrientation.Horizontal;
                stackLayout2.Orientation = StackOrientation.Horizontal;
                GaugeWidth = widthConstraint / 3;
                GaugeHeight = heightConstraint / 2;
            }
            else
            {
                rootStackLayout.Orientation = StackOrientation.Horizontal;
                stackLayout1.Orientation = StackOrientation.Vertical;
                stackLayout2.Orientation = StackOrientation.Vertical;
                GaugeWidth = widthConstraint / 2;
                GaugeHeight = heightConstraint / 3;
            }
            return base.MeasureOverride(widthConstraint, heightConstraint);
        }

        private void styleMarker_ValueChanging(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 20)
                e.Cancel = true;
            else
            {
                string text = string.Empty;
                if (sender is MarkerPointer markerPointer)
                    text = AutomationProperties.GetHelpText(markerPointer);
                if (sender is NeedlePointer needlePointer)
                    text = AutomationProperties.GetHelpText(needlePointer);

                double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                if (text == "styleAnnotationLabel1")
                    styleAnnotationLabel1.Text = value.ToString() + "%";
                else if (text == "styleAnnotationLabel2")
                    styleAnnotationLabel2.Text = value.ToString() + "%";
                else if (text == "styleAnnotationLabel3")
                    styleAnnotationLabel3.Text = value.ToString() + "%";
                else if (text == "styleAnnotationLabel4")
                    styleAnnotationLabel4.Text = value.ToString() + "%";
                else if (text == "styleAnnotationLabel5")
                    styleAnnotationLabel5.Text = value.ToString() + "%";
                else if (text == "styleAnnotationLabel6")
                {
                    styleAnnotationLabel6.Text = value.ToString();
                    styleRangePointer6.Value = value == 100 ? 100 : value == 0 ? 0.1 : e.NewValue;
                }
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            foreach (var item in stackLayout1.Children)
            {
                item.Handler?.DisconnectHandler();
            }

            foreach (var item in stackLayout2.Children)
            {
                item.Handler?.DisconnectHandler();
            }
        }

#nullable disable
        public new event PropertyChangedEventHandler PropertyChanged;
#nullable enable

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}