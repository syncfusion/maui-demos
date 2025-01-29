#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialSlider
{
    public partial class CustomTextRadialSlider : SampleView
    {
        public CustomTextRadialSlider()
        {
            InitializeComponent();
        }

        #region Events
        private void customTextMarker_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            if (customTextAxis.Pointers != null && customTextAxis.Pointers.Count > 1)
            {
                if (e.Value > 99)
                {
                    Brush brush = new SolidColorBrush(Color.FromRgb(0, 168, 181));

                    if (customTextAxis.Pointers[0] is RangePointer pointer)
                        pointer.Fill = brush;
                    if (customTextAxis.Pointers[1] is ShapePointer pointer1)
                        pointer1.Stroke = Color.FromRgb(0, 168, 181);
                    percentageLabel.Text = "";
                    customTextAnnotation.Text = "Done";

                }
                else
                {
                    Brush brush = new SolidColorBrush(Colors.Orange);
                    if (customTextAxis.Pointers[0] is RangePointer pointer)
                        pointer.Fill = brush;
                    if (customTextAxis.Pointers[1] is ShapePointer pointer1)
                        pointer1.Stroke = Colors.Orange;
                    percentageLabel.Text = (int)e.Value+"%";
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