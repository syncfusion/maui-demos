#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.DockLayout.SfDockLayout
{
    public partial class DefaultDockLayoutSample : SampleView
    {
        public DefaultDockLayoutSample()
        {
            InitializeComponent();
        }

        private void OnLastChildCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            docklayout.ShouldExpandLastChild = e.Value;
        }

        private void OnHorizontalSpacingChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender is Slider slider)
            {
                docklayout.HorizontalSpacing = slider.Value;
            }
        }

        private void OnVerticalSpacingChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender is Slider slider)
            {
                docklayout.VerticalSpacing = slider.Value;
            }
        }

        private void OnDirectionSelectionChanged(object sender, Syncfusion.Maui.Inputs.ComboBoxValueChangedEventArgs e)
        {
            if (sender is Syncfusion.Maui.Inputs.SfComboBox comboBox && comboBox.SelectedItem is FlowDirection selectedDirection)
            {
                docklayout.FlowDirection = selectedDirection;
            }
        }
    }

    public class DockLayoutViewModel
    {
        public List<FlowDirection> Directions { get; }

        public DockLayoutViewModel()
        {
            Directions = new List<FlowDirection>
            {
                FlowDirection.LeftToRight,
                FlowDirection.RightToLeft,
                FlowDirection.MatchParent
            };
        }
    }
}