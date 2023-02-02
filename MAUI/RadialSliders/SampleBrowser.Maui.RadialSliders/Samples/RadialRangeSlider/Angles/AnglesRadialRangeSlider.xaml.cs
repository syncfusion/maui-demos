#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Gauges;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.RadialSliders.RadialRangeSlider
{
    public partial class AnglesRadialRangeSlider : SampleView, INotifyPropertyChanged
    {
        public AnglesRadialRangeSlider()
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
            if (rootLayout == null || gauge2Axis == null || gauge3Axis == null)
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
                rootLayout.Orientation = StackOrientation.Horizontal;
                GaugeHeight = GaugeWidth = width / 4.5;
                gauge2Axis.CanScaleToFit = gauge3Axis.CanScaleToFit = true;
            }
            else
            {
                rootLayout.Orientation = StackOrientation.Vertical;
                GaugeWidth = GaugeHeight = height / 4;
                gauge2Axis.CanScaleToFit = gauge3Axis.CanScaleToFit = false;
            }

#if ANDROID || WINDOWS
            return base.MeasureOverride(width, height);
#else
            base.OnSizeAllocated(width, height);
#endif
        }

        private void angleMarkerPointer1_ValueChanging(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
        {
            if (sender is MarkerPointer markerPointer)
            {
                string text = AutomationProperties.GetHelpText(markerPointer);

                if (Math.Abs(e.NewValue - e.OldValue) > 20 ||
                    (text == "angleAnnotationLabel1" && (e.NewValue >= angleMarker2.Value || Math.Abs(e.NewValue - angleMarker1.Value) > 10)) ||
                    (text == "angleAnnotationLabel2" && (e.NewValue >= angleMarker4.Value || Math.Abs(e.NewValue - angleMarker3.Value) > 10)) ||
                    (text == "angleAnnotationLabel3" && (e.NewValue >= angleMarker6.Value || Math.Abs(e.NewValue - angleMarker5.Value) > 10)) ||
                    (text == "angleAnnotationLabel4" && (e.NewValue >= angleMarker8.Value || Math.Abs(e.NewValue - angleMarker7.Value) > 10)))
                    e.Cancel = true;
                else
                {
                    double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                    if (text == "angleAnnotationLabel1")
                        angleAnnotationLabel1.Text = value.ToString() + "-" + (int)angleMarker2.Value;
                    else if (text == "angleAnnotationLabel2")
                        angleAnnotationLabel2.Text = value.ToString() + "-" + (int)angleMarker4.Value;
                    else if (text == "angleAnnotationLabel3")
                        angleAnnotationLabel3.Text = value.ToString() + "-" + (int)angleMarker6.Value;
                    else if (text == "angleAnnotationLabel4")
                        angleAnnotationLabel4.Text = value.ToString() + "-" + (int)angleMarker8.Value;
                }
            }
        }

        private void angleMarkerPointer2_ValueChanging(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
        {
            if (sender is MarkerPointer markerPointer)
            {
                string text = AutomationProperties.GetHelpText(markerPointer);

                if (Math.Abs(e.NewValue - e.OldValue) > 20 ||
                    (text == "angleAnnotationLabel1" && (e.NewValue <= angleMarker1.Value || Math.Abs(e.NewValue - angleMarker2.Value) > 10)) ||
                    (text == "angleAnnotationLabel2" && (e.NewValue <= angleMarker3.Value || Math.Abs(e.NewValue - angleMarker4.Value) > 10)) ||
                    (text == "angleAnnotationLabel3" && (e.NewValue <= angleMarker5.Value || Math.Abs(e.NewValue - angleMarker6.Value) > 10)) ||
                    (text == "angleAnnotationLabel4" && (e.NewValue <= angleMarker7.Value || Math.Abs(e.NewValue - angleMarker8.Value) > 10)))
                    e.Cancel = true;
                else
                {
                    double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                    if (text == "angleAnnotationLabel1")
                        angleAnnotationLabel1.Text = (int)angleMarker1.Value + "-" + value.ToString();
                    else if (text == "angleAnnotationLabel2")
                        angleAnnotationLabel2.Text = (int)angleMarker3.Value + "-" + value.ToString();
                    else if (text == "angleAnnotationLabel3")
                        angleAnnotationLabel3.Text = (int)angleMarker5.Value + "-" + value.ToString();
                    else if (text == "angleAnnotationLabel4")
                        angleAnnotationLabel4.Text = (int)angleMarker7.Value + "-" + value.ToString();
                }
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            foreach (var item in rootLayout.Children)
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