#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeTicksVertical : ContentView
    {
        public LinearGaugeTicksVertical()
        {
            InitializeComponent();
        }

        double previousWidth, previousHeight = 0;
        protected override Size ArrangeOverride(Rect bounds)
        {
            if (previousWidth != bounds.Width || previousHeight != bounds.Height)
            {
                previousWidth = bounds.Width;
                previousHeight = bounds.Height;

                if (bounds.Width > 440)
                {
                    grid.ColumnDefinitions.Clear();
                    grid.RowDefinitions.Clear();

                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                    for (int i = 0; i < grid.Children.Count; i++)
                    {
                        Grid.SetColumn(grid.Children[i] as View, i);
                    }
                }
                else
                {
                    grid.ColumnDefinitions.Clear();
                    grid.RowDefinitions.Clear();

                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());

                    Grid.SetColumn(grid.Children[0] as View, 0);
                    Grid.SetRow(grid.Children[0] as View, 0);

                    Grid.SetColumn(grid.Children[1] as View, 1);
                    Grid.SetRow(grid.Children[1] as View, 0);

                    Grid.SetColumn(grid.Children[2] as View, 0);
                    Grid.SetRow(grid.Children[2] as View, 1);

                    Grid.SetColumn(grid.Children[3] as View, 1);
                    Grid.SetRow(grid.Children[3] as View, 1);

                }
            }
            return base.ArrangeOverride(bounds);
        }
    }
}