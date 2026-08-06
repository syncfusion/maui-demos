using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Base;
using System;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeShapePointer : SampleView
    {
        public LinearGaugeShapePointer()
        {
            InitializeComponent();
#if IOS
            contentView.HeightRequest = 550;
#endif
        }

        #region Events
        private void SegmentedControl_SelectionChanged(object? sender, Syncfusion.Maui.Buttons.SelectionChangedEventArgs e)
        {
            if (e.NewIndex == 0)
            {
                contentView.Content = new LinearGaugeShapePointerHorizontal();
            }
            else if (e.NewIndex == 1)
            {
                contentView.Content = new LinearGaugeShapePointerVertical();
            }
        }

        #endregion
    }
}
