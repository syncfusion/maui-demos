#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using SampleBrowser.Maui.Services;
using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.IO;
using System.Reflection;

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
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
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

            Assembly? assembly = typeof(ExcelToPDF).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);

            if(input != null)
            { 
            MemoryStream stream = new();
            input.CopyTo(stream);

            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("ExcelTopdfwithChart.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            input.Dispose();
            }
        }

        /// <summary>
        /// Convert Excel To PDF
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvertToPDF_Click(object sender, EventArgs e)
        {
            using ExcelEngine engine = new();
            Syncfusion.XlsIO.IApplication application = engine.Excel;
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.ExcelTopdfwithChart.xlsx";
            Assembly? assembly = typeof(ExcelToPDF).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            if(input != null)
            { 
            IWorkbook book = application.Workbooks.Open(input);

            //Initialize XlsIORenderer
            XlsIORenderer renderer = new();

            //Intialize the PDFDocument
            PdfDocument pdfDoc = new();

            //Intialize the XlsIORendererSettings
            XlsIORendererSettings settings = new();

            settings.LayoutOptions = Syncfusion.XlsIORenderer.LayoutOptions.FitSheetOnOnePage;

            //Assign the PdfDocument to the templateDocument property of XlsIORendererSettings
            settings.TemplateDocument = pdfDoc;
            settings.DisplayGridLines = GridLinesDisplayStyle.Invisible;

            //Convert Excel Document into PDF document
            pdfDoc = renderer.ConvertToPDF(book, settings);

            //Save the PDF file
            MemoryStream stream = new();
            pdfDoc.Save(stream);
            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("ExcelToPDF.pdf", "application/pdf", stream);

            #region Close and Dispose                
            input.Dispose();
            stream.Dispose();
            #endregion
            }
        }

        #endregion


    }
}
