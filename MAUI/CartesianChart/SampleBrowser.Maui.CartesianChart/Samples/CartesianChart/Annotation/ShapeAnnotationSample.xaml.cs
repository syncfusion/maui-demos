#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.CartesianChart.Samples.CartesianChart.Annotation;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class ShapeAnnotationSample : SampleView
    {
        private ChartAnnotation? _selectedAnnotation;

        public ShapeAnnotationSample()
        {
            InitializeComponent();
            verticalLine.Attach(Chart);
            horizontalLine.Attach(Chart);
            lineAnnotation.Attach(Chart);
            rectangleAnnotation.Attach(Chart);
            ellipseAnnotation.Attach(Chart);
            textAnnotation.Attach(Chart);
        }

        private void OnDragDropCheckChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            bool isEnabled = e.IsChecked == true ? true : false;
            EnableDragDrop(isEnabled);
            if (isEnabled && ChangeColorRadio.IsChecked)
            {
                ChangeColorRadio.IsChecked = false;
            }
        }

        private void OnChangeColorRadioChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            bool isEnabled = e.IsChecked == true ? true : false;
            ChangeColorRadio.IsChecked = isEnabled;
            if (isEnabled && DragDropCheckBox.IsChecked)
            {
                DragDropCheckBox.IsChecked = false;
            }
        }

        private void EnableDragDrop(bool isEnabled)
        {
            verticalLine.SetDragEnabled(isEnabled);
            horizontalLine.SetDragEnabled(isEnabled);
            lineAnnotation.SetDragEnabled(isEnabled);
            rectangleAnnotation.SetDragEnabled(isEnabled);
            ellipseAnnotation.SetDragEnabled(isEnabled);
            textAnnotation.SetDragEnabled(isEnabled);
        }

        private void Chart_AnnotationTapped(object sender, Syncfusion.Maui.Charts.Chart.Events.AnnotationTappedEventArgs e)
        {
            if (!ChangeColorRadio.IsChecked)
            { return; }

            _selectedAnnotation = e.Annotation;
            ColorPickerOverlay.IsVisible = true;
        }

        private void OnApplyColorClicked(object? sender, EventArgs e)
        {
            ApplyColorToAnnotation();
            ColorPickerOverlay.IsVisible = false;
            _selectedAnnotation = null;
        }
        private void OnCloseTapped(object? sender, TappedEventArgs e)
        {
            ColorPickerOverlay.IsVisible = false;
            _selectedAnnotation = null;
        }
        private void ApplyColorToAnnotation()
        {
            if (_selectedAnnotation == null)
            {
                return;
            }

            Color color = ColorPicker.SelectedColor;
            SolidColorBrush brush = new(color);
            if (_selectedAnnotation is DraggableLineAnnotation lineAnn)
            {
                lineAnn.Stroke = brush;
            }
            else if (_selectedAnnotation is DraggableHorizontalLineAnnotation hLineAnn)
            {
                hLineAnn.Stroke = brush;
            }
            else if (_selectedAnnotation is DraggableVerticalLineAnnotation vLineAnn)
            {
                vLineAnn.Stroke = brush;
            }
            else if (_selectedAnnotation is DraggableRectangleAnnotation rectAnn)
            {
                rectAnn.Fill = brush;
            }
            else if (_selectedAnnotation is DraggableEllipseAnnotation ellipseAnn)
            {
                ellipseAnn.Fill = brush;
            }
            else if (_selectedAnnotation is DraggableTextAnnotation textAnn)
            {
                textAnn.LabelStyle = new ChartAnnotationLabelStyle
                {
                    TextColor = color
                };
            }
        }

        private void OnColorPickerClosed(object? sender, EventArgs e)
        {
            ColorPickerOverlay.IsVisible = false;
            _selectedAnnotation = null;
        }
    }
}
