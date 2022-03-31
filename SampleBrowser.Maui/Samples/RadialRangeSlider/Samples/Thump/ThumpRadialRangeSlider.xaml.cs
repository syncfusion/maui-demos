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

namespace SampleBrowser.Maui.RadialRangeSlider
{
    public partial class ThumpRadialRangeSlider : SampleView, INotifyPropertyChanged
    {
        public ThumpRadialRangeSlider()
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
                rootLayout.Orientation = StackOrientation.Horizontal;
                GaugeHeight = GaugeWidth = widthConstraint / 3.5;
            }
            else
            {
                rootLayout.Orientation = StackOrientation.Vertical;
                GaugeWidth = widthConstraint;
                GaugeHeight = heightConstraint / 4;
            }
            return base.MeasureOverride(widthConstraint, heightConstraint);
        }

        private void thumpPointer1_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (sender is MarkerPointer markerPointer)
            {
                string text = AutomationProperties.GetHelpText(markerPointer);

                if (Math.Abs(e.NewValue - e.OldValue) > 20 ||
                    (text == "thumpAnnotationLabel1" && (e.NewValue >= thumpMarkerPointer2.Value || Math.Abs(e.NewValue - thumpMarkerPointer1.Value) > 10)) ||
                    (text == "thumpAnnotationLabel2" && (e.NewValue >= thumpMarkerPointer4.Value || Math.Abs(e.NewValue - thumpMarkerPointer3.Value) > 10)) ||
                    (text == "thumpAnnotationLabel3" && (e.NewValue >= thumpMarkerPointer6.Value || Math.Abs(e.NewValue - thumpMarkerPointer5.Value) > 10)))
                    e.Cancel = true;
                else
                {
                    double value = e.NewValue;
                    value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);

                    if (text == "thumpAnnotationLabel1")
                        thumpAnnotationLabel1.Text = value.ToString() + " - " + (int)thumpMarkerPointer2.Value;
                    else if (text == "thumpAnnotationLabel2")
                        thumpAnnotationLabel2.Text = value.ToString() + " - " + (int)thumpMarkerPointer4.Value;
                    else if (text == "thumpAnnotationLabel3")
                        thumpAnnotationLabel3.Text = value.ToString() + " - " + (int)thumpMarkerPointer6.Value;
                }
            }
        }

        private void thumpPointer2_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (sender is MarkerPointer markerPointer)
            {
                string text = AutomationProperties.GetHelpText(markerPointer);

                if (Math.Abs(e.NewValue - e.OldValue) > 20 ||
                    (text == "thumpAnnotationLabel1" && (e.NewValue <= thumpMarkerPointer1.Value || Math.Abs(e.NewValue - thumpMarkerPointer2.Value) > 10)) ||
                    (text == "thumpAnnotationLabel2" && (e.NewValue <= thumpMarkerPointer3.Value || Math.Abs(e.NewValue - thumpMarkerPointer4.Value) > 10)) ||
                    (text == "thumpAnnotationLabel3" && (e.NewValue <= thumpMarkerPointer5.Value || Math.Abs(e.NewValue - thumpMarkerPointer6.Value) > 10)))
                    e.Cancel = true;
                else
                {
                    double value = e.NewValue;
                    value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);

                    if (text == "thumpAnnotationLabel1")
                        thumpAnnotationLabel1.Text = (int)thumpMarkerPointer1.Value + " - " + value.ToString();
                    else if (text == "thumpAnnotationLabel2")
                        thumpAnnotationLabel2.Text = (int)thumpMarkerPointer3.Value + " - " + value.ToString();
                    else if (text == "thumpAnnotationLabel3")
                        thumpAnnotationLabel3.Text = (int)thumpMarkerPointer5.Value + " - " + value.ToString();
                }
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            gauge1.Handler?.DisconnectHandler();
            gauge2.Handler?.DisconnectHandler();
            gauge3.Handler?.DisconnectHandler();
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