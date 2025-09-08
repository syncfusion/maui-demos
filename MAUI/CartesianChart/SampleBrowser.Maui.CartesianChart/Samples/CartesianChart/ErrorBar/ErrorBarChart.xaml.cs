#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;
using System.Collections.ObjectModel;
namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class ErrorBarChart : SampleView
    {
        #region Constructor

        #region  Public Constructor

        public ErrorBarChart()
        {
            InitializeComponent();
            errorBar.HorizontalErrorValue = 1;
            errorBar.VerticalErrorValue = 5;
        }

        #endregion

        #endregion

        #region Methods


        #region  Private Methods

        public override void OnAppearing()
        {
            base.OnAppearing();
            if(!IsCardView)
            {
                errorBarChart.Title = (Label)layout.Resources["title"];
                xAxis.Title = new ChartAxisTitle() { Text = "Country Code" }; 
                yAxis.Title = new ChartAxisTitle() { Text = "Sales Percentage" };
            }
        }

        private void TypeSelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                errorBar.Type = ErrorBarType.Fixed;
            }
            else if (selectedIndex == 1)
            {
                errorBar.Type = ErrorBarType.Percentage;
            }
            else if (selectedIndex == 2)
            {
                errorBar.Type = ErrorBarType.StandardError;
            }
            else if (selectedIndex == 3)
            {
                errorBar.Type = ErrorBarType.StandardDeviation;
            }
        }

        private void ModeSelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                errorBar.Mode = ErrorBarMode.Vertical;
                horStepper.IsEnabled = false;
                verStepper.IsEnabled = true;

            }
            else if (selectedIndex == 1)
            {
                errorBar.Mode = ErrorBarMode.Horizontal;
                horStepper.IsEnabled = true;
                verStepper.IsEnabled = false;

            }
            else
            {
                errorBar.Mode = ErrorBarMode.Both;
                horStepper.IsEnabled = true;
                verStepper.IsEnabled = true;
            }
        }

        private void DirectionSelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                if (errorBar.Mode == ErrorBarMode.Horizontal)
                {
                    errorBar.HorizontalDirection = ErrorBarDirection.Both;
                }
                else if (errorBar.Mode == ErrorBarMode.Vertical)
                {
                    errorBar.VerticalDirection = ErrorBarDirection.Both;
                }
                else
                {
                    errorBar.HorizontalDirection = ErrorBarDirection.Both;
                    errorBar.VerticalDirection = ErrorBarDirection.Both;
                }
            }
            else if (selectedIndex == 1)
            {
                if (errorBar.Mode == ErrorBarMode.Horizontal)
                {
                    errorBar.HorizontalDirection = ErrorBarDirection.Plus;
                }
                else if (errorBar.Mode == ErrorBarMode.Vertical)
                {
                    errorBar.VerticalDirection = ErrorBarDirection.Plus;
                }
                else if (errorBar.Mode == ErrorBarMode.Both)
                {
                    errorBar.HorizontalDirection = errorBar.VerticalDirection = ErrorBarDirection.Plus;
                }
            }
            else
            {
                if (errorBar.Mode == ErrorBarMode.Horizontal)
                {
                    errorBar.HorizontalDirection = ErrorBarDirection.Minus;
                }
                if (errorBar.Mode == ErrorBarMode.Vertical)
                {
                    errorBar.VerticalDirection = ErrorBarDirection.Minus;
                }
                if (errorBar.Mode == ErrorBarMode.Both)
                {
                    errorBar.HorizontalDirection = errorBar.VerticalDirection = ErrorBarDirection.Minus;
                }
            }
        }

        private void Stepper_ValueChanged(object sender, Microsoft.Maui.Controls.ValueChangedEventArgs e)
        {
            var stepper = (Stepper)sender;
            var selectedValue = stepper.StyleId;
            switch (selectedValue)
            {
                case "horStepper":
                    errorBar.HorizontalErrorValue = stepper.Value;
                    break;
                case "verStepper":
                    errorBar.VerticalErrorValue = stepper.Value;
                    break;
            }
        }

        #endregion

        #endregion
    }
}
