#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Inputs;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class CartesianLegend : SampleView
    {
        public CartesianLegend()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            tooltipChart.Handler?.DisconnectHandler();
        }

        private void FloatingLegend_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            CartesianChartLegend.IsFloating = checkBox.IsChecked;
            xOffsetStepperLabel.IsEnabled = checkBox.IsChecked;
            xOffsetStepperLabel.Opacity = checkBox.IsChecked ? 1 : 0.5;
            xOffsetStepper.IsEnabled = checkBox.IsChecked;
            xOffsetStepper.Opacity = checkBox.IsChecked ? 1 : 0.5;

            yOffsetStepperLabel.IsEnabled = checkBox.IsChecked;
            yOffsetStepperLabel.Opacity = checkBox.IsChecked ? 1 : 0.5;
            yOffsetStepper.IsEnabled = checkBox.IsChecked;
            yOffsetStepper.Opacity = checkBox.IsChecked ? 1 : 0.5;
        }

        private void LegendPlacement_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                CartesianChartLegend.Placement = LegendPlacement.Top;
            }
            else if (selectedIndex == 1)
            {
                CartesianChartLegend.Placement = LegendPlacement.Left;
            }
            else if (selectedIndex == 2)
            {
                CartesianChartLegend.Placement = LegendPlacement.Bottom;
            }
            else if (selectedIndex == 3)
            {
                CartesianChartLegend.Placement = LegendPlacement.Right;
            }
        }

        int xOffset = 0;
        int yOffset = 0;

        private void OnXIncrementClicked(object sender, EventArgs e)
        {
            if (xOffset < 100)
            {
                xOffset += 10;
                xOffsetValue.Text = xOffset.ToString();
                CartesianChartLegend.OffsetX = xOffset;
            }
        }

        private void OnXDecrementClicked(object sender, EventArgs e)
        {
            if (xOffset > -100)
            {
                xOffset -= 10;
                xOffsetValue.Text = xOffset.ToString();
                CartesianChartLegend.OffsetX = xOffset;
            }
        }

        private void OnYIncrementClicked(object sender, EventArgs e)
        {
            if (yOffset < 100)
            {
                yOffset += 10;
                yOffsetValue.Text = yOffset.ToString();
                CartesianChartLegend.OffsetY = yOffset;
            }
        }

        private void OnYDecrementClicked(object sender, EventArgs e)
        {
            if (yOffset > -100)
            {
                yOffset -= 10;
                yOffsetValue.Text = yOffset.ToString();
                CartesianChartLegend.OffsetY = yOffset;
            }
        }
    }

    public class LineSeriesExt : LineSeries
    {
        public Geometry? PathData
        {
            get { return (Geometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public static readonly BindableProperty PathDataProperty =
    BindableProperty.Create(
        nameof(PathData),
        typeof(Geometry),
        typeof(LineSeriesExt),
        null,
        BindingMode.Default,
        null);

        public LineSeriesExt()
        {
        }
    }
}
