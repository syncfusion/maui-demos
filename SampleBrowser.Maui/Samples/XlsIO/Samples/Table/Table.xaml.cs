#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Syncfusion.XlsIO;
using System;
using System.Globalization;
using System.IO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using System.Reflection;

namespace SampleBrowser.Maui.XlsIO
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : SampleView
    {
        # region Constructor
        /// <summary>
        /// Table constructor
        /// </summary>
        public Table()
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
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                #region Table data
                // Create data
                worksheet[1, 1].Text = "Products";
                worksheet[1, 2].Text = "Qtr1";
                worksheet[1, 3].Text = "Qtr2";
                worksheet[1, 4].Text = "Qtr3";
                worksheet[1, 5].Text = "Qtr4";

                worksheet[2, 1].Text = "Alfreds Futterkiste";
                worksheet[2, 2].Number = 744.6;
                worksheet[2, 3].Number = 162.56;
                worksheet[2, 4].Number = 5079.6;
                worksheet[2, 5].Number = 1249.2;

                worksheet[3, 1].Text = "Antonio Moreno Taqueria";
                worksheet[3, 2].Number = 5079.6;
                worksheet[3, 3].Number = 1249.2;
                worksheet[3, 4].Number = 943.89;
                worksheet[3, 5].Number = 349.6;

                worksheet[4, 1].Text = "Around the Horn";
                worksheet[4, 2].Number = 1267.5;
                worksheet[4, 3].Number = 1062.5;
                worksheet[4, 4].Number = 744.6;
                worksheet[4, 5].Number = 162.56;

                worksheet[5, 1].Text = "Bon app";
                worksheet[5, 2].Number = 1418;
                worksheet[5, 3].Number = 756;
                worksheet[5, 4].Number = 1267.5;
                worksheet[5, 5].Number = 1062.5;

                worksheet[6, 1].Text = "Eastern Connection";
                worksheet[6, 2].Number = 4728;
                worksheet[6, 3].Number = 4547.92;
                worksheet[6, 4].Number = 1418;
                worksheet[6, 5].Number = 756;

                worksheet[7, 1].Text = "Ernst Handel";
                worksheet[7, 2].Number = 943.89;
                worksheet[7, 3].Number = 349.6;
                worksheet[7, 4].Number = 4728;
                worksheet[7, 5].Number = 4547.92;
                #endregion

                // Create style for table number format
                IStyle style1 = workbook.Styles.Add("CurrencyFormat");
                style1.NumberFormat = "$#,###"; 

                // Apply number format
                worksheet["B2:E8"].CellStyleName = "CurrencyFormat";

                // Create table
                IListObject table1 = worksheet.ListObjects.Create("Table1", worksheet["A1:E7"]);

                // Apply builtin style
                table1.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium9;

                worksheet.UsedRange.AutofitColumns();
                worksheet.SetColumnWidth(2, 12.43);
                worksheet.SetColumnWidth(4, 12.43);

                string outputFile = "Table.xlsx";
                // Save and close the document
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;
                DependencyService.Get<ISave>().SaveAndView(outputFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
                stream.Dispose();
            }
        }
        #endregion

    }
}