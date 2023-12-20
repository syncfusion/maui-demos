#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    /// Interaction logic for PivotTable.xaml
    /// </summary>
    public partial class PivotTable : SampleView
    {
        # region Constructor
        /// <summary>
        /// PivotTable constructor
        /// </summary>
        public PivotTable()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.btnCreate.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        # endregion               

        # region Events
        /// <summary>
        /// Creates spreadsheet
        /// </summary>
        /// <param name="sender">Contains a reference to the control/object that raised the event</param>
        /// <param name="e">Contains the event data</param>    
        private void btnCreate_Click(object sender, EventArgs e)
        {
            using ExcelEngine excelEngine = new();
            Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Xlsx;

            string inputPath = "SampleBrowser.Maui.Resources.XlsIO.PivotCodeDate.xlsx";
            if (BaseConfig.IsIndividualSB)
                inputPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.PivotCodeDate.xlsx";

            Assembly? assembly = typeof(Chart).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputPath);
            IWorkbook workbook = application.Workbooks.Open(input);

           
            // The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];

            //Access the sheet to draw pivot table.
            IWorksheet pivotSheet = workbook.Worksheets[1];
            pivotSheet.Activate();
            //Select the data to add in cache
            IPivotCache cache = workbook.PivotCaches.Add(sheet["A1:H50"]);

            //Insert the pivot table. 
            IPivotTable pivotTable = pivotSheet.PivotTables.Add("PivotTable1", pivotSheet["A1"], cache);

            if (chkboxGroupingFilter.IsChecked)
                pivotTable.Fields[0].Axis = PivotAxisTypes.Row;

            pivotTable.Fields[2].Axis = PivotAxisTypes.Row;
            pivotTable.Fields[4].Axis = PivotAxisTypes.Page;
            pivotTable.Fields[6].Axis = PivotAxisTypes.Row;
            pivotTable.Fields[3].Axis = PivotAxisTypes.Column;

            IPivotField field = pivotSheet.PivotTables[0].Fields[5];
            pivotTable.DataFields.Add(field, "Sum of Units", PivotSubtotalTypes.Sum);
          
            if (chkboxGroupingFilter.IsChecked)
            {
                IPivotFieldGroup group = pivotTable.Fields[0].FieldGroup;
                group.GroupBy = PivotFieldGroupType.Years | PivotFieldGroupType.Quarters | PivotFieldGroupType.Months;
            }

            //Apply built in style.
            pivotTable.BuiltInStyle = PivotBuiltInStyles.PivotStyleMedium2;
            pivotSheet.Range[1, 1, 1, 14].ColumnWidth = 11;
            pivotSheet.SetColumnWidth(1, 15.29);
            pivotSheet.SetColumnWidth(2, 15.29);
            //Activate the pivot sheet.
            pivotSheet.Activate();

            string outputFile = "PivotTable.xlsx";
            // Save and close the document
            MemoryStream stream = new();
            workbook.SaveAs(stream);
            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView(outputFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
            stream.Dispose();
        }
        #endregion

    }
}