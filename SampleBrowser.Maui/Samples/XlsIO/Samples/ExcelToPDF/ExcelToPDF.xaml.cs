#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using Syncfusion.XlsIO;
using Syncfusion.Pdf;
using Syncfusion.XlsIO.Implementation;
using System.Reflection;
using Syncfusion.XlsIORenderer;
using System.Collections.Generic;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.XlsIO
{
    /// <summary>
    /// Interaction logic for ExcelToPDF1.xaml
    /// </summary>
    public partial class ExcelToPDF : SampleView
    {
        #region Constructor
        /// <summary>
        /// ExcelToPDF constructor
        /// </summary>
        public ExcelToPDF()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.btnInput.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnConvertToPdf.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Loads the input template
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnInput_Click(object sender, EventArgs e)
        {
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.ExcelTopdfwithChart.xlsx";

            Assembly assembly = typeof(ExcelToPDF).GetTypeInfo().Assembly;
            Stream input = assembly.GetManifestResourceStream(inputPath);

            MemoryStream stream = new MemoryStream();
            input.CopyTo(stream);

            stream.Position = 0;
            DependencyService.Get<ISave>().SaveAndView("ExcelTopdfwithChart.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            input.Dispose();
        }

        /// <summary>
        /// Convert Excel To PDF
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvertToPDF_Click(object sender, EventArgs e)
        {
            using (ExcelEngine engine = new ExcelEngine())
            {
                Syncfusion.XlsIO.IApplication application = engine.Excel;
                string inputPath = "SampleBrowser.Maui.Resources.XlsIO.ExcelTopdfwithChart.xlsx";
                Assembly assembly = typeof(ExcelToPDF).GetTypeInfo().Assembly;
                Stream input = assembly.GetManifestResourceStream(inputPath);
                IWorkbook book = application.Workbooks.Open(input);

                //Initialize XlsIORenderer
                XlsIORenderer renderer = new XlsIORenderer();

                //Intialize the PDFDocument
                PdfDocument pdfDoc = new PdfDocument();

                //Intialize the XlsIORendererSettings
                XlsIORendererSettings settings = new XlsIORendererSettings();

                settings.LayoutOptions = Syncfusion.XlsIORenderer.LayoutOptions.FitSheetOnOnePage;

                //Assign the PdfDocument to the templateDocument property of XlsIORendererSettings
                settings.TemplateDocument = pdfDoc;
                settings.DisplayGridLines = GridLinesDisplayStyle.Invisible;

                //Convert Excel Document into PDF document
                pdfDoc = renderer.ConvertToPDF(book, settings);

                //Save the PDF file
                MemoryStream stream = new MemoryStream();
                pdfDoc.Save(stream);
                stream.Position = 0;
                DependencyService.Get<ISave>().SaveAndView("ExcelToPDF.pdf", "application/pdf", stream);

                #region Close and Dispose                
                input.Dispose();
                stream.Dispose();
                #endregion
            }
        }

       #endregion

      
    }
}
