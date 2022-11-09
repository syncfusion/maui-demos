#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeDragPointer : SampleView
    {

        public LinearGaugeDragPointer()
        {
            InitializeComponent();
        }

        #region Events
        private void shapePointer4_AnimationCompleted(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            shapePointer1.EnableAnimation = shapePointer2.EnableAnimation =
            shapePointer3.EnableAnimation = shapePointer4.EnableAnimation =
            shapePointer5.EnableAnimation = shapePointer6.EnableAnimation =
            shapePointer7.EnableAnimation = shapePointer8.EnableAnimation = false;
        }

        private void shapePointer5_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue > shapePointer7.Value)
                e.Cancel = true;
            if (e.NewValue > 98)
                e.Cancel = true;
        }

        private void shapePointer7_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue < shapePointer5.Value)
                e.Cancel = true;
        }

        #endregion
    }
}
