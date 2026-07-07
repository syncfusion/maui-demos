#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for MarkdownToExcel.xaml
    /// </summary>
    public partial class MarkdownToExcel : SampleView
    {
        #region Constructor
        /// <summary>
        /// MarkdownToExcel constructor
        /// </summary>
        public MarkdownToExcel()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnInput.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnConvertToExcel.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
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
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.MarkdownToExcel.md";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.MarkdownToExcel.md";
            Assembly? assembly = typeof(MarkdownToExcel).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);

            if (input != null)
            {
                MemoryStream stream = new();
                input.CopyTo(stream);

                stream.Position = 0;
                SaveService saveService = new();
                saveService.SaveAndView("MarkdownToExcel.md", "text/markdown", stream);

                input.Dispose();
            }
        }

        /// <summary>
        /// Convert Excel To PDF
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvertToExcel_Click(object sender, EventArgs e)
        {
            using ExcelEngine engine = new();
            Syncfusion.XlsIO.IApplication application = engine.Excel;
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.MarkdownToExcel.md";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.MarkdownToExcel.md";
            Assembly? assembly = typeof(MarkdownToExcel).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            if (input != null)
            {
                application.PreserveCSVDataTypes = true;
                IWorkbook book = application.Workbooks.Open(input, ExcelOpenType.Markdown);
                IWorksheet sheet = book.Worksheets[0];
                sheet.UsedRange.AutofitColumns();
                sheet.Calculate();

                //Save the Markdown file
                MemoryStream stream = new MemoryStream();
                book.Version = ExcelVersion.Xlsx;
                book.SaveAs(stream);
                stream.Position = 0;
                SaveService saveService = new();
                saveService.SaveAndView("MarkdownToExcel.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                #region Close and Dispose                
                input.Dispose();
                stream.Dispose();
                #endregion
            }
        }

        #endregion


    }
}