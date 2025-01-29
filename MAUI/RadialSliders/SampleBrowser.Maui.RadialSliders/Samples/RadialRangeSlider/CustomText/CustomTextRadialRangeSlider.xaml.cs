#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialRangeSlider
{
    public partial class CustomTextRadialRangeSlider : SampleView
    {
        public CustomTextRadialRangeSlider()
        {
            InitializeComponent();
        }

        #region Events
        private void customTextMarker_ValueChanging1(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue >= customTextMarker2.Value || Math.Abs(e.NewValue - customTextMarker1.Value) > 10)
                e.Cancel = true;
            else
                UpdateCustomTextAnnotationLabel(Math.Abs(customTextMarker2.Value - customTextMarker1.Value));
        }

        private void customTextMarker_ValueChanging2(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue <= customTextMarker1.Value || Math.Abs(e.NewValue - customTextMarker2.Value) > 10)
                e.Cancel = true;
            else
                UpdateCustomTextAnnotationLabel(Math.Abs(customTextMarker2.Value - customTextMarker1.Value));
        }

        private void UpdateCustomTextAnnotationLabel(double value)
        {
            if (customTextAxis.Pointers != null && customTextAxis.Pointers.Count > 1)
            {
                if (value > 99)
                {
                    Brush brush = new SolidColorBrush(Color.FromRgb(0, 168, 181));
                    customTextAxis.Ranges[0].Fill = brush;

                    if (customTextAxis.Pointers[0] is ShapePointer pointer)
                        pointer.Stroke = Color.FromRgb(0, 168, 181);

                    if (customTextAxis.Pointers[1] is ShapePointer pointer1)
                        pointer1.Stroke = Color.FromRgb(0, 168, 181);
                    percentageLabel.Text = "";
                    customTextAnnotation.Text = "Done";

                }
                else
                {
                    Brush brush = new SolidColorBrush(Colors.Orange);
                    customTextAxis.Ranges[0].Fill = brush;

                    if (customTextAxis.Pointers[0] is ShapePointer pointer)
                        pointer.Stroke = Colors.Orange;
                    if (customTextAxis.Pointers[1] is ShapePointer pointer1)
                        pointer1.Stroke = Colors.Orange;

                    percentageLabel.Text = (int)value + "%";
                    customTextAnnotation.Text = "Progress";
                }
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            customTextGauge.Handler?.DisconnectHandler();
        }

        #endregion
    }
}