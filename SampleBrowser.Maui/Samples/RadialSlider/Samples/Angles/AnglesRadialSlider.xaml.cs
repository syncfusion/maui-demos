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
    public partial class AnglesRadialSlider : SampleView, INotifyPropertyChanged
    {
        public AnglesRadialSlider()
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
                angleSlidersLayout.Orientation = StackOrientation.Horizontal;
                GaugeHeight = GaugeWidth = widthConstraint / 4.5;
                gauge2Axis.CanScaleToFit = gauge3Axis.CanScaleToFit = true;
            }
            else
            {
                angleSlidersLayout.Orientation = StackOrientation.Vertical;
                GaugeWidth = GaugeHeight = heightConstraint / 4;
                gauge2Axis.CanScaleToFit = gauge3Axis.CanScaleToFit = false;
            }
            return base.MeasureOverride(widthConstraint, heightConstraint);
        }

        private void angleMarkerPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 20)
                e.Cancel = true;
            else if (sender is MarkerPointer markerPointer)
            {
                string text = AutomationProperties.GetHelpText(markerPointer);

                double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                if (text == "angleAnnotationLabel1")
                    angleAnnotationLabel1.Text = value.ToString() + "%";
                else if (text == "angleAnnotationLabel2")
                    angleAnnotationLabel2.Text = value.ToString() + "%";
                else if (text == "angleAnnotationLabel3")
                    angleAnnotationLabel3.Text = value.ToString() + "%";
                else if (text == "angleAnnotationLabel4")
                    angleAnnotationLabel4.Text = value.ToString() + "%";
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            foreach (var item in angleSlidersLayout.Children)
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