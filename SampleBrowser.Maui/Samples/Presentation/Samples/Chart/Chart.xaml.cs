using System;
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using Syncfusion.Presentation;
using System.IO;
using Syncfusion.OfficeChart;
using Syncfusion.Drawing;

namespace SampleBrowser.Maui.Presentation
{
    public partial class Chart : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public Chart()
        {
            InitializeComponent();
#if ANDROID || IOS
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates charts in a presentation.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Creates a new instance of the PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Create();
            //Creates slides with chart.
            CreateChart(presentation);
            using MemoryStream ms = new();
            //Saves the presentation to the memory stream.
            presentation.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Chart.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Creates slides with chart.
        /// </summary>
        private static void CreateChart(IPresentation presentation)
        {
            //Adds a blank slide to the presentation.
            ISlide slide1 = presentation.Slides.Add(SlideLayoutType.Blank);
            //Adds a chart to the slide with position and size.
            IPresentationChart chart = slide1.Shapes.AddChart(1.93 * 72, 0.31 * 72, 9.48 * 72, 6.89 * 72);

            //Sets the data for chart– RowIndex, columnIndex and data.
            chart.ChartData.SetValue(1, 1, "Crescent City, CA");
            chart.ChartData.SetValue(3, 2, "Precipitation,in.");
            chart.ChartData.SetValue(3, 3, "Temperature,deg.F");

            chart.ChartData.SetValue(4, 1, "Jan");
            chart.ChartData.SetValue(5, 1, "Feb");
            chart.ChartData.SetValue(6, 1, "March");
            chart.ChartData.SetValue(7, 1, "Apr");
            chart.ChartData.SetValue(8, 1, "May");
            chart.ChartData.SetValue(9, 1, "June");
            chart.ChartData.SetValue(10, 1, "July");
            chart.ChartData.SetValue(11, 1, "Aug");
            chart.ChartData.SetValue(12, 1, "Sept");
            chart.ChartData.SetValue(13, 1, "Oct");
            chart.ChartData.SetValue(14, 1, "Nov");
            chart.ChartData.SetValue(15, 1, "Dec");

            chart.ChartData.SetValue(4, 2, 10.9);
            chart.ChartData.SetValue(5, 2, 8.9);
            chart.ChartData.SetValue(6, 2, 8.6);
            chart.ChartData.SetValue(7, 2, 4.8);
            chart.ChartData.SetValue(8, 2, 3.2);
            chart.ChartData.SetValue(9, 2, 1.4);
            chart.ChartData.SetValue(10, 2, 0.6);
            chart.ChartData.SetValue(11, 2, 0.7);
            chart.ChartData.SetValue(12, 2, 1.7);
            chart.ChartData.SetValue(13, 2, 5.4);
            chart.ChartData.SetValue(14, 2, 9.0);
            chart.ChartData.SetValue(15, 2, 10.4);

            chart.ChartData.SetValue(4, 3, 47.5);
            chart.ChartData.SetValue(5, 3, 48.7);
            chart.ChartData.SetValue(6, 3, 48.9);
            chart.ChartData.SetValue(7, 3, 50.2);
            chart.ChartData.SetValue(8, 3, 53.1);
            chart.ChartData.SetValue(9, 3, 56.3);
            chart.ChartData.SetValue(10, 3, 58.1);
            chart.ChartData.SetValue(11, 3, 59.0);
            chart.ChartData.SetValue(12, 3, 58.5);
            chart.ChartData.SetValue(13, 3, 55.4);
            chart.ChartData.SetValue(14, 3, 51.1);
            chart.ChartData.SetValue(15, 3, 47.8);
            //Sets the data range of chart series – start row, start column, end row, end column.
            chart.DataRange = chart.ChartData[3, 1, 15, 3];
            //Sets the chart title and axis title.
            chart.ChartTitle = "Crescent City, CA";
            chart.PrimaryValueAxis.Title = "Precipitation,in.";
            chart.PrimaryValueAxis.TitleArea.TextRotationAngle = 90;
            chart.PrimaryValueAxis.MaximumValue = 14.0;
            chart.PrimaryValueAxis.NumberFormat = "0.0";

            #region Format Series
            //Formats the first serie.
            IOfficeChartSerie serieOne = chart.Series[0];
            serieOne.Name = "Precipitation,in.";
            serieOne.SerieFormat.Fill.FillType = OfficeFillType.Gradient;
            serieOne.SerieFormat.Fill.TwoColorGradient(OfficeGradientStyle.Vertical, OfficeGradientVariants.ShadingVariants_2);
            serieOne.SerieFormat.Fill.GradientColorType = OfficeGradientColor.TwoColor;
            serieOne.SerieFormat.Fill.ForeColor = Color.Plum;

            //Shows value as data labels.
            serieOne.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
            serieOne.DataPoints.DefaultDataPoint.DataLabels.Position = OfficeDataLabelPosition.Outside;

            //Formats the second serie.
            IOfficeChartSerie serieTwo = chart.Series[1];
            serieTwo.SerieType = OfficeChartType.Line_Markers;
            serieTwo.Name = "Temperature,deg.F";

            //Formats marker.
            serieTwo.SerieFormat.MarkerStyle = OfficeChartMarkerType.Diamond;
            serieTwo.SerieFormat.MarkerSize = 8;
            serieTwo.SerieFormat.MarkerBackgroundColor = Color.DarkGreen;
            serieTwo.SerieFormat.MarkerForegroundColor = Color.DarkGreen;
            serieTwo.SerieFormat.LineProperties.LineColor = Color.DarkGreen;

            //Uses Secondary Axis.
            serieTwo.UsePrimaryAxis = false;
            #endregion

            #region Set the Secondary Axis for Second Serie.
            //Displays secondary axis for the series.
            chart.SecondaryCategoryAxis.IsMaxCross = true;
            chart.SecondaryValueAxis.IsMaxCross = true;

            //Sets the title.
            chart.SecondaryValueAxis.Title = "Temperature,deg.F";
            chart.SecondaryValueAxis.TitleArea.TextRotationAngle = 90;

            //Hides the secondary category axis.
            chart.SecondaryCategoryAxis.Border.LineColor = Color.Transparent;
            chart.SecondaryCategoryAxis.MajorTickMark = OfficeTickMark.TickMark_None;
            chart.SecondaryCategoryAxis.TickLabelPosition = OfficeTickLabelPosition.TickLabelPosition_None;
            #endregion

            #region Legend setting
            chart.Legend.Position = OfficeLegendPosition.Bottom;
            chart.Legend.IsVerticalLegend = false;
            #endregion
        }
        #endregion
    }
}
