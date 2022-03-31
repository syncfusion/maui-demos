#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using System;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public partial class ChartAxis : SampleView
    {
        int month = int.MaxValue;

        public ChartAxis()
        {
            InitializeComponent();
            dateTimeChart.XAxes[0].LabelCreated += Primary_LabelCreated;
        }

        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {
            var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;
            content?.AnimateSeries();
        }

        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);

            if (view is Syncfusion.Maui.Charts.SfCartesianChart cartesianChart)
            {
                var behavior = cartesianChart.ZoomPanBehavior;
                if (behavior != null)
                {
                    if (behavior.ZoomMode == ZoomMode.X)
                    {
                        behavior.EnablePanning = true;
                    }
                    else
                    {
                        behavior.EnablePinchZooming = behavior.EnableDoubleTap = behavior.EnablePanning = true;
                    }
                }
            }
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

            zooming.EnableDoubleTap = zooming.EnablePanning = zooming.EnablePinchZooming = false;
            zooming1.EnablePanning = false;

            view.Handler?.DisconnectHandler();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            categoryChart.Handler?.DisconnectHandler();
            dateTimeChart.Handler?.DisconnectHandler();
            numericChart.Handler?.DisconnectHandler();
            axisCrossingChart.Handler?.DisconnectHandler();
        }

        private void Primary_LabelCreated(object? sender, ChartAxisLabelEventArgs e)
        {
            DateTime baseDate = new(1899, 12, 30);
            var date = baseDate.AddDays(e.Position);
            if (date.Month != month)
            {
                ChartAxisLabelStyle labelStyle = new();
                labelStyle.LabelFormat = "MMM-dd";
                labelStyle.FontAttributes = FontAttributes.Bold;
                e.LabelStyle = labelStyle;

                month = date.Month;
            }
            else
            {
                ChartAxisLabelStyle labelStyle = new();
                labelStyle.LabelFormat = "dd";
                e.LabelStyle = labelStyle;
            }
        }
    }
}
