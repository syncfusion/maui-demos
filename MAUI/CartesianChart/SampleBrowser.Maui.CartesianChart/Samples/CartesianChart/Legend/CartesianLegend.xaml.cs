#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
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
