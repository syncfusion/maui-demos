﻿#region Copyright Syncfusion Inc. 2001-2022.
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
    public partial class ThumpRadialSlider : SampleView, INotifyPropertyChanged
    {
        public ThumpRadialSlider()
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

        private void thumpPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 20)
                e.Cancel = true;
            else if (sender is MarkerPointer markerPointer)
            {
                string text = AutomationProperties.GetHelpText(markerPointer);

                double value = e.NewValue;
                value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);

                if (text == "thumpAnnotationLabel1")
                    thumpAnnotationLabel1.Text = value.ToString() + "%";
                else if (text == "thumpAnnotationLabel2")
                    thumpAnnotationLabel2.Text = value.ToString() + "%";
                else if (text == "thumpAnnotationLabel3")
                    thumpAnnotationLabel3.Text = value.ToString() + "%";
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