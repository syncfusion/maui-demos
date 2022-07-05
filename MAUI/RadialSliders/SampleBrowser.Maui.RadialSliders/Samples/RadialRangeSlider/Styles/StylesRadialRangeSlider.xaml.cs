#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialRangeSlider
{
    public partial class StylesRadialRangeSlider : SampleView, INotifyPropertyChanged
    {
        public StylesRadialRangeSlider()
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

#if ANDROID || WINDOWS
        protected override Size MeasureOverride(double width, double height)
#else
        protected override void OnSizeAllocated(double width, double height)
#endif
        {
            if (rootStackLayout == null || stackLayout1 == null || stackLayout2 == null)
            {
#if ANDROID || WINDOWS
                return base.MeasureOverride(width, height);
#else
                base.OnSizeAllocated(width, height);
                return;
#endif
            }

            if (width > height)
            {
                rootStackLayout.Orientation = StackOrientation.Vertical;
                stackLayout1.Orientation = StackOrientation.Horizontal;
                stackLayout2.Orientation = StackOrientation.Horizontal;
                GaugeWidth = width / 3;
                GaugeHeight = height / 2;
            }
            else
            {
                rootStackLayout.Orientation = StackOrientation.Horizontal;
                stackLayout1.Orientation = StackOrientation.Vertical;
                stackLayout2.Orientation = StackOrientation.Vertical;
                GaugeWidth = width / 2;
                GaugeHeight = height / 3;
            }

#if ANDROID || WINDOWS
            return base.MeasureOverride(width, height);
#else
            base.OnSizeAllocated(width, height);
#endif
        }


        private void styleMarker_ValueChanging1(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
        {
            string text = string.Empty;
            if (sender is MarkerPointer markerPointer)
                text = AutomationProperties.GetHelpText(markerPointer);
            if (sender is NeedlePointer needlePointer)
                text = AutomationProperties.GetHelpText(needlePointer);

            if (Math.Abs(e.NewValue - e.OldValue) > 20 ||
                (text == "styleAnnotationLabel1" && (e.NewValue >= styleMarker2.Value || Math.Abs(e.NewValue - styleMarker1.Value) > 10)) ||
                (text == "styleAnnotationLabel2" && (e.NewValue >= styleMarker4.Value || Math.Abs(e.NewValue - styleMarker3.Value) > 10)) ||
                (text == "styleAnnotationLabel3" && (e.NewValue >= styleMarker6.Value || Math.Abs(e.NewValue - styleMarker5.Value) > 10)) ||
                (text == "styleAnnotationLabel4" && (e.NewValue >= styleMarker8.Value || Math.Abs(e.NewValue - styleMarker7.Value) > 10)) ||
                (text == "styleAnnotationLabel5" && (e.NewValue >= styleMarker10.Value || Math.Abs(e.NewValue - styleMarker9.Value) > 10)) ||
                (text == "styleAnnotationLabel6" && (e.NewValue >= styleMarker12.Value || Math.Abs(e.NewValue - styleMarker11.Value) > 10)))
                e.Cancel = true;
            else
            {
                double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                if (text == "styleAnnotationLabel1")
                    styleAnnotationLabel1.Text = value.ToString() + " - " + (int)styleMarker2.Value + "%";
                else if (text == "styleAnnotationLabel2")
                    styleAnnotationLabel2.Text = value.ToString() + " - " + (int)styleMarker4.Value + "%";
                else if (text == "styleAnnotationLabel3")
                    styleAnnotationLabel3.Text = value.ToString() + " - " + (int)styleMarker6.Value + "%";
                else if (text == "styleAnnotationLabel4")
                    styleAnnotationLabel4.Text = value.ToString() + " - " + (int)styleMarker8.Value + "%";
                else if (text == "styleAnnotationLabel5")
                    styleAnnotationLabel5.Text = value.ToString() + " - " + (int)styleMarker10.Value + "%";
                else if (text == "styleAnnotationLabel6")
                    styleAnnotationLabel6.Text = value.ToString() + " - " + (int)styleMarker12.Value;
            }
        }

        private void styleMarker_ValueChanging2(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
        {
            string text = string.Empty;
            if (sender is MarkerPointer markerPointer)
                text = AutomationProperties.GetHelpText(markerPointer);
            if (sender is NeedlePointer needlePointer)
                text = AutomationProperties.GetHelpText(needlePointer);

            if (Math.Abs(e.NewValue - e.OldValue) > 20 ||
                (text == "styleAnnotationLabel1" && (e.NewValue <= styleMarker1.Value || Math.Abs(e.NewValue - styleMarker2.Value) > 10)) ||
                (text == "styleAnnotationLabel2" && (e.NewValue <= styleMarker3.Value || Math.Abs(e.NewValue - styleMarker4.Value) > 10)) ||
                (text == "styleAnnotationLabel3" && (e.NewValue <= styleMarker5.Value || Math.Abs(e.NewValue - styleMarker6.Value) > 10)) ||
                (text == "styleAnnotationLabel4" && (e.NewValue <= styleMarker7.Value || Math.Abs(e.NewValue - styleMarker8.Value) > 10)) ||
                (text == "styleAnnotationLabel5" && (e.NewValue <= styleMarker9.Value || Math.Abs(e.NewValue - styleMarker10.Value) > 10)) ||
                (text == "styleAnnotationLabel6" && (e.NewValue <= styleMarker11.Value || Math.Abs(e.NewValue - styleMarker12.Value) > 10)))
                e.Cancel = true;
            else
            {
                double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                if (text == "styleAnnotationLabel1")
                    styleAnnotationLabel1.Text = (int)styleMarker1.Value + " - " + value.ToString() + "%";
                else if (text == "styleAnnotationLabel2")
                    styleAnnotationLabel2.Text = (int)styleMarker3.Value + " - " + value.ToString() + "%";
                else if (text == "styleAnnotationLabel3")
                    styleAnnotationLabel3.Text = (int)styleMarker5.Value + " - " + value.ToString() + "%";
                else if (text == "styleAnnotationLabel4")
                    styleAnnotationLabel4.Text = (int)styleMarker7.Value + " - " + value.ToString() + "%";
                else if (text == "styleAnnotationLabel5")
                    styleAnnotationLabel5.Text = (int)styleMarker9.Value + " - " + value.ToString() + "%";
                else if (text == "styleAnnotationLabel6")
                    styleAnnotationLabel6.Text = (int)styleMarker11.Value + " - " + value.ToString();
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