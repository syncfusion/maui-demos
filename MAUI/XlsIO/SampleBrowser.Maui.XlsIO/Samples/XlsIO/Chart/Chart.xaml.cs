#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.XlsIO;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public sealed partial class Chart : SampleView
    {
        # region Constructor
        /// <summary>
        /// Chart constructor
        /// </summary>
        public Chart()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = LayoutOptions.Center;
            this.btnInput.HorizontalOptions = LayoutOptions.Center;
            this.btnCreate.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        # endregion

        #region Creating Charts
        private void btnCreate_Click(object sender, EventArgs e)
        {
            using ExcelEngine excelEngine = new();
            #region Workbook Initialize

            Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Xlsx;

            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.ChartData.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.ChartData.xlsx";

            Assembly? assembly = typeof(Chart).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            IWorkbook workbook = application.Workbooks.Open(input);

            #endregion

            IWorksheet sheet = workbook.Worksheets[0];

            // Generate Chart
            IChartShape chart = sheet.Charts.Add();

            chart.DataRange = sheet["A16:E26"];
            chart.ChartTitle = sheet["A15"].Text;
            chart.HasLegend = false;
            chart.TopRow = 3;
            chart.LeftColumn = 1;
            chart.RightColumn = 6;
            chart.BottomRow = 15;

            #region Workbook Save and Close
            string outFileName = "Chart.xlsx";
            MemoryStream output = new();
            workbook.SaveAs(output);
            output.Position = 0;

            SaveService saveService = new();
            saveService.SaveAndView(outFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", output);

            output.Dispose();
            #endregion
        }
        #endregion

        #region HelperMethods

        #region View the Input file
        private void btnInput_Click(object sender, EventArgs e)
        {
            //Launching the Input Template using the default Application.[MS Excel Or Free ExcelViewer]
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.ChartData.xlsx";
            if(BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.ChartData.xlsx";
            Assembly? assembly = typeof(Chart).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            if(input != null)
            { 
            MemoryStream stream = new();
            input.CopyTo(stream);
            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("ChartData.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            input.Dispose();
            }


        }
        #endregion

        #endregion
    }
}