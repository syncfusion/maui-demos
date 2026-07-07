#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for Comments.xaml
    /// </summary>
    public partial class Comments : SampleView
    {
        #region Constructor
        /// <summary>
        /// Comments constructor
        /// </summary>
        public Comments()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnInput.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnCreate.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnConvert.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
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
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.CommentsTemplate.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.CommentsTemplate.xlsx";
            Assembly? assembly = typeof(Comments).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);

            if (input != null)
            {
                MemoryStream stream = new();
                input.CopyTo(stream);

                stream.Position = 0;
                SaveService saveService = new();
                saveService.SaveAndView("CommentsTemplate.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                input.Dispose();
            }
        }

        /// <summary>
        /// Create Excel
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //Initialize ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Initialize IApplication and set the default application version
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Load the Excel template into IWorkbook and get the worksheet into IWorksheet
                string inputPath = "SampleBrowser.Maui.Resources.XlsIO.CommentsTemplate.xlsx";
                if (BaseConfig.IsIndividualSB)
                    inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.CommentsTemplate.xlsx";
                Assembly? assembly = typeof(Comments).GetTypeInfo().Assembly;
                Stream? input = assembly.GetManifestResourceStream(inputPath);
                if (input != null)
                {
                    IWorkbook workbook = application.Workbooks.Open(input);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Add Comments
                    AddComments(worksheet);

                    //Save the Excel file
                    MemoryStream stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    SaveService saveService = new SaveService();
                    saveService.SaveAndView("ExcelComments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                    #region Close and Dispose                
                    input.Dispose();
                    stream.Dispose();
                    #endregion
                }
            }
        }

        /// <summary>
        /// Convert To PDF
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            //Initialize ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Initialize IApplication and set the default application version
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Load the Excel template into IWorkbook and get the worksheet into IWorksheet
                string inputPath = "SampleBrowser.Maui.Resources.XlsIO.CommentsTemplate.xlsx";
                if (BaseConfig.IsIndividualSB)
                    inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.CommentsTemplate.xlsx";
                Assembly? assembly = typeof(Comments).GetTypeInfo().Assembly;
                Stream? input = assembly.GetManifestResourceStream(inputPath);
                if (input != null)
                {
                    IWorkbook workbook = application.Workbooks.Open(input);
                    IWorksheet worksheet = workbook.Worksheets[0];

                    //Add Comments
                    AddComments(worksheet);

                    //Set print location of comments
                    worksheet.PageSetup.PrintComments = ExcelPrintLocation.PrintSheetEnd;

                    //Initialize XlsIORenderer and convert the Excel document to PDF
                    XlsIORenderer renderer = new XlsIORenderer();
                    PdfDocument document = renderer.ConvertToPDF(workbook);

                    //Save the Excel file
                    MemoryStream stream = new MemoryStream();
                    document.Save(stream);
                    stream.Position = 0;
                    SaveService saveService = new SaveService();
                    saveService.SaveAndView("ExcelComments.pdf", "application/pdf", stream);

                    #region Close and Dispose                
                    input.Dispose();
                    stream.Dispose();
                    #endregion
                }
            }
        }
        private void AddComments(IWorksheet worksheet)
        {
            //Add Comments (Notes)
            IComment comment = worksheet.Range["H1"].AddComment();
            comment.Text = "This Total column comment will be printed at the end of sheet.";
            comment.IsVisible = true;

            //Add Threaded Comments
            IThreadedComment threadedComment = worksheet.Range["H16"].AddThreadedComment("What is the reason for the higher total amount of \"desk\"  in the west region?", "User1", DateTime.Now);
            threadedComment.AddReply("The unit cost of desk is higher compared to other items in the west region. As a result, the total amount is elevated.", "User2", DateTime.Now);
            threadedComment.AddReply("Additionally, Wilson sold 31 desks in the west region, which is also a contributing factor to the increased total amount.", "User3", DateTime.Now);
        }
        #endregion
    }
}