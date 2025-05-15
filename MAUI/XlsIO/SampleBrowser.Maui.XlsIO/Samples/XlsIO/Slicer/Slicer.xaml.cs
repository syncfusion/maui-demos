#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Services;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for Slicer.xaml
    /// </summary>
    public partial class Slicer : SampleView
    {
        # region Constructor
        /// <summary>
        /// Slicer constructor
        /// </summary>
        public Slicer()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnInput.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnCreateDocument.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
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
            string inputpath = "SampleBrowser.Maui.Resources.XlsIO.TableSlicer.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputpath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.TableSlicer.xlsx";

            Assembly? assembly = typeof(Slicer).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputpath);
            if (input != null)
            {
                MemoryStream stream = new();
                input.CopyTo(stream);
                stream.Position = 0;
                SaveService saveService = new();
                saveService.SaveAndView("TableSlicer.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                input.Dispose();
            }
        }

        /// <summary>
        /// Converts spreadsheet to Image
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnCreateDocument_Click(object sender, EventArgs e)
        {
            // New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            // The instantiation process consists of two steps.

            // Step 1 : Instantiate the spreadsheet creation engine.
            using ExcelEngine excelEngine = new();

            // Step 2 : Instantiate the excel application object.
            Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Excel2007;
            application.XlsIORenderer = new XlsIORenderer();

            // An existing workbook is opened.
            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.TableSlicer.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.TableSlicer.xlsx";
            Assembly? assembly = typeof(Slicer).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            if (input != null)
            {
                IWorkbook workbook = application.Workbooks.Open(input);

                // The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                IListObject table = sheet.ListObjects[0];

                int colId1 = 0;
                int colId2 = 0;
                //Get the column id from the given column name
                if (Column1.Text != null)
                    colId1 = GetColumnId(Column1.Text, table);
                if (Column2.Text != null)
                    colId2 = GetColumnId(Column2.Text, table);

                // Add slicer for the table
                sheet.Slicers.Add(table, colId1, 11, 2);
                sheet.Slicers.Add(table, colId2, 11, 4);

                MemoryStream stream = new();
                workbook.SaveAs(stream);
                stream.Position = 0;
                SaveService saveService = new();
                saveService.SaveAndView("TableSlicer.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);


                input.Dispose();
                stream.Dispose();
            }
        }
        private int GetColumnId(String columnName, IListObject table)
        {
            int colId = 0;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].Name == columnName)
                {
                    colId = i + 1;
                    break;
                }
            }
            return colId;
        }
        #endregion

    }
}
