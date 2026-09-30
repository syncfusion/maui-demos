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
using System.Globalization;
using System.IO;
using System.Reflection;
using IFont = Syncfusion.XlsIO.IFont;
using Color = Syncfusion.Drawing.Color;

namespace SampleBrowser.Maui.XlsIO.XlsIO
{
    /// <summary>
    /// Interaction logic for FromatCells.xaml
    /// </summary>
    public partial class FormatCells : SampleView
    {
        #region Constructor
        /// <summary>
        /// FormatCells constructor
        /// </summary>
        public FormatCells()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.stkLayout.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
            this.btnConvertToPdf.HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events

        /// <summary>
        /// Convert Excel To PDF
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnConvertToPDF_Click(object sender, EventArgs e)
        {
            using ExcelEngine engine = new();
            Syncfusion.XlsIO.IApplication application = engine.Excel;
            IWorkbook book = application.Workbooks.Create(1);
            IWorksheet sheet = book.Worksheets[0];
            sheet.IsGridLinesVisible = false;

            # region RTF
            //Insert Rich Text
            Syncfusion.XlsIO.IRange range = sheet.Range["D3"];
            range.Text = "Employee Report";
            IRichTextString rtf = range.RichText;

            //Formatting the text
            IFont redFont = book.CreateFont();
            redFont.Size = 16;
            redFont.Bold = true;
            redFont.Italic = true;
            redFont.RGBColor = Color.FromArgb(0x82, 0x2e, 0x1b);
            rtf.SetFont(0, 14, redFont);


            #endregion

            #region Number Formatting

            sheet.Range["H24"].Number = 5000;
            sheet.Range["H24"].NumberFormat = "$#,##0.00";
            sheet.Range["H14"].NumberFormat = "dd/mm/yyyy";
            sheet.Range["H14"].DateTime = DateTime.Parse(" 8/3/1963", CultureInfo.InvariantCulture);
            sheet.Range["H16"].NumberFormat = "mm/dd/yyyy";
            sheet.Range["H16"].DateTime = DateTime.Parse(" 4/1/1992", CultureInfo.InvariantCulture);

            #endregion

            # region Alignment settings

            # region Text alignment
            sheet.Range["F10:F24"].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range["D3"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            sheet.Range["H10:H24"].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range["F10:F24"].VerticalAlignment = ExcelVAlign.VAlignCenter;

            #endregion

            #region Text Control
            sheet.Range["F27"].WrapText = true;
            sheet.Range["F27"].Text = "Antony Jose graduated from St. Andrews University, Scotland, with a BSC degree in 1976.  Upon joining the company as a sales representative in 1992, he spent 6 months in an orientation program at the Seattle office and then returned to his permanent post in London.  He was promoted to sales manager in March 1993.";

            #endregion

            #region Cell merging
            sheet.Range["F27:H28"].Merge();
            sheet.Range["D3:F3"].Merge();
            sheet.Range["B7:J8"].Merge();
            sheet.Range["F27"].RowHeight = 66;
            sheet.Range["H10:H24"].ColumnWidth = 25;
            sheet.Range["C10:D17"].Merge();
            sheet.Range["B10:C28"].ColumnWidth = 1;
            sheet.Range["E10:E28"].ColumnWidth = 5;
            sheet.Range["D10:D28"].ColumnWidth = 15;
            #endregion

            #region Text Direction
            sheet.Range["B7"].Text = "Antony Jose";
            sheet.Range["B7"].CellStyle.ReadingOrder = Syncfusion.XlsIO.ExcelReadingOrderType.LeftToRight;
            sheet.Range["B7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range["B7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
            #endregion

            #region Text Indent

            sheet.Range["D7"].CellStyle.IndentLevel = 6;

            #endregion


            #endregion

            #region Font settings

            // Setting Font Type
            sheet.Range["F10:F24"].CellStyle.Font.FontName = "Arial";
            sheet.Range["D3"].CellStyle.Font.FontName = "Arial";
            sheet.Range["B7"].CellStyle.Font.FontName = "Arial";
            sheet.Range["B7"].CellStyle.Font.Size = 12f;
            sheet.Range["B7"].CellStyle.Font.Bold = true;

            // Setting Font Styles
            sheet.Range["F10:F24"].CellStyle.Font.Bold = true;
            sheet.Range["D3"].CellStyle.Font.Bold = true;

            // Setting Font Size
            sheet.Range["F10:F24"].CellStyle.Font.Size = 10;
            sheet.Range["D3"].CellStyle.Font.Size = 14;
            sheet.Range["F27:H28"].CellStyle.Font.Size = 9f;

            // Setting Font Color
            sheet.Range["B7"].CellStyle.Font.RGBColor = Color.White;
            sheet.Range["D28"].CellStyle.Font.RGBColor = Color.White;


            // Setting UnderLine 
            sheet.Range["D3"].CellStyle.Font.Underline = ExcelUnderline.Double;

            sheet.Range["F10"].Text = "Name";
            sheet.Range["F12"].Text = "Title";
            sheet.Range["F14"].Text = "Birth Date";
            sheet.Range["F16"].Text = "Hire date";
            sheet.Range["F18"].Text = "Home phone";
            sheet.Range["F20"].Text = "Extension";
            sheet.Range["F22"].Text = "Home address";
            sheet.Range["F24"].Text = "Salary";

            sheet.Range["H10"].Text = "Antony Jose";
            sheet.Range["H12"].Text = "Sales Manager";
            sheet.Range["H18"].Text = "(206) 555-3412";
            sheet.Range["H20"].Number = 3355;
            sheet.Range["H22"].Text = "722 Moss Bay Blvd";


            #endregion

            #region Insert Image
            string inputImgPath = "SampleBrowser.Maui.Resources.XlsIO.EMPID1.png";
            if (BaseConfig.IsIndividualSB)
                inputImgPath = "SampleBrowser.Maui.XlsIO.Resources.XlsIO.EMPID1.png";
            Assembly? assembly = typeof(ExcelToPDF).GetTypeInfo().Assembly;
            Stream? input = assembly.GetManifestResourceStream(inputImgPath);
            if (input != null)
            {
                IPictureShape picture = sheet.Pictures.AddPicture(10, 3, input);
                picture.PlaceInCell = true;
            }
            #endregion

            #region Border setting

            sheet.Range["H10"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H10"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H10"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H12"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H12"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H12"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H14"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H14"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H14"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H16"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H16"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H16"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H18"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H18"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H18"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H20"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H20"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H20"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H22"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H22"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H22"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["H24"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["H24"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["H24"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            sheet.Range["F27:H28"].CellStyle.Borders.LineStyle = ExcelLineStyle.Medium;
            sheet.Range["F27:H28"].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            sheet.Range["F27:H28"].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

            #endregion

            #region Pattern settings
            sheet.Range["B9:I29"].CellStyle.Color = Color.FromArgb(198, 215, 239);
            sheet.Range["A7:J8"].CellStyle.Color = Color.FromArgb(57, 93, 148);

            sheet.Range["A7:A30"].CellStyle.Color = Color.FromArgb(57, 93, 148);
            sheet.Range["A7:A30"].ColumnWidth = 2.29;

            sheet.Range["J7:J30"].CellStyle.Color = Color.FromArgb(57, 93, 148);
            sheet.Range["J7:J30"].ColumnWidth = 2.29;

            sheet.Range["A30:J30"].CellStyle.Color = Color.FromArgb(57, 93, 148);

            sheet.Range["F27:H28"].CellStyle.Color = Color.FromArgb(231, 235, 247);
            #endregion

            //Save the Excel file
            MemoryStream stream = new();
            book.Version = ExcelVersion.Xlsx;
            book.SaveAs(stream);
            stream.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("FormatCells.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

            #region Close and Dispose
            stream.Dispose();
            #endregion
        }

        #endregion


    }
}
