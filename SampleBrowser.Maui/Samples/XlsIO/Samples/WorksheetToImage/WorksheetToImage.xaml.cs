#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Syncfusion.XlsIO;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Syncfusion.XlsIORenderer;
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.XlsIO
{
    /// <summary>
    /// Interaction logic for WorksheetToImage.xaml
    /// </summary>
    public partial class WorksheetToImage : SampleView
    {
        # region Constructor
        /// <summary>
        /// WorksheetToImage constructor
        /// </summary>
        public WorksheetToImage()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.btnInput.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnConvertToImage.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
#endif
        }
        # endregion

        #region Events
        /// <summary>
        /// Opens input template
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnInput_Click(object sender, EventArgs e)
        {
            string inputpath = "SampleBrowser.Maui.Resources.XlsIO.ExpenseReport.xlsx";

            Assembly assembly = typeof(WorksheetToImage).GetTypeInfo().Assembly;
            Stream input = assembly.GetManifestResourceStream(inputpath);

            MemoryStream stream = new MemoryStream();
            input.CopyTo(stream);
            stream.Position = 0;
            DependencyService.Get<ISave>().SaveAndView("ExpenseReport.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            input.Dispose();
        }

        /// <summary>
        /// Converts spreadsheet to Image
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvertToImage_Click(object sender, EventArgs e)
        {
            // New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            // The instantiation process consists of two steps.

            // Step 1 : Instantiate the spreadsheet creation engine.
            using (ExcelEngine excelEngine = new ExcelEngine())
            {

                // Step 2 : Instantiate the excel application object.
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2007;
                application.XlsIORenderer = new XlsIORenderer();

                // An existing workbook is opened.
                string inputPath = "SampleBrowser.Maui.Resources.XlsIO.ExpenseReport.xlsx";
                Assembly assembly = typeof(WorksheetToImage).GetTypeInfo().Assembly;
                Stream input = assembly.GetManifestResourceStream(inputPath);
                IWorkbook workbook = application.Workbooks.Open(input);

                // The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                ExportImageOptions imageOptions = new ExportImageOptions();

                MemoryStream stream = new MemoryStream();
                // Save the image.
                 imageOptions.ImageFormat = ExportImageFormat.Png;

                // Convert the worksheet to image
                sheet.ConvertToImage(sheet.UsedRange, imageOptions, stream);

                stream.Position = 0;
                string filename = "WorksheetToImage.png";
                DependencyService.Get<ISave>().SaveAndView(filename, "image/png", stream);

               
                                
                input.Dispose();
                stream.Dispose();
            }
        }
        #endregion
       
    }
}
