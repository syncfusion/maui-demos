using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Base;
using System;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeRange : SampleView
    {
        public LinearGaugeRange()
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
                contentView.Content = new LinearGaugeRangeHorizontal();
            }
            else if (e.NewIndex == 1)
            {
                contentView.Content = new LinearGaugeRangeVertical();
            }
        }

        #endregion
    }
}
