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

namespace SampleBrowser.Maui.XlsIO
{
    /// <summary>
    /// Interaction logic for AutoShapes.xaml
    /// </summary>
    public partial class AutoShapes : SampleView
    {
        #region Constructor
        /// <summary>
        /// AutoShape constructor
        /// </summary>
        public AutoShapes()
        {
            this.InitializeComponent();
#if ANDROID || IOS
            this.btnCreate.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        # endregion

        # region Events
        /// <summary>
        /// Creates spreadsheet with AutoShapes
        /// </summary>
        /// <param name="sender">contains a reference to the control/object that raised the event</param>
        /// <param name="e">contains the event data</param>
        private void btnCreate_Click(object sender, EventArgs e)
        {            
            //New instance of XlsIO is created.[Equivalent to launching MS Excel with no workbooks open].
            //The instantiation process consists of two steps.

            //Step 1 : Instantiate the spreadsheet creation engine.
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Step 2 : Instantiate the excel application object.
                Syncfusion.XlsIO.IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Xlsx;

                //A new workbook is created.[Equivalent to creating a new workbook in MS Excel]
                //The new workbook will have 1 worksheet.
                IWorkbook workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet worksheet = workbook.Worksheets[0];

                #region AddAutoShapes
                IShape shape;
                string text;

                IFont font = workbook.CreateFont();
                font.Color = ExcelKnownColors.White;
                font.Italic = true;
                font.Size = 12;


                IFont font2 = workbook.CreateFont();
                font2.Color = ExcelKnownColors.Black;
                font2.Size = 15;
                font2.Italic = true;
                font2.Bold = true;

                text = "Requirement";
                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 2, 7, 60, 192);
                shape.TextFrame.TextRange.Text = text;
                shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
                shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
                shape.Fill.ForeColorIndex = ExcelKnownColors.Light_blue;
                shape.Line.Visible = false;
                shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;

                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 5, 8, 40, 64);
                shape.Fill.ForeColorIndex = ExcelKnownColors.White;
                shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
                shape.Line.Weight = 1;

                text = "Design";
                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 7, 7, 60, 192);
                shape.TextFrame.TextRange.Text = text;
                shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
                shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
                shape.Line.Visible = false;
                shape.Fill.ForeColorIndex = ExcelKnownColors.Light_orange;
                shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;


                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 10, 8, 40, 64);
                shape.Fill.ForeColorIndex = ExcelKnownColors.White;
                shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
                shape.Line.Weight = 1;

                text = "Execution";
                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 12, 7, 60, 192);
                shape.TextFrame.TextRange.Text = text;
                shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
                shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
                shape.Line.Visible = false;
                shape.Fill.ForeColorIndex = ExcelKnownColors.Blue;
                shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;

                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 15, 8, 40, 64);
                shape.Fill.ForeColorIndex = ExcelKnownColors.White;
                shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
                shape.Line.Weight = 1;

                text = "Testing";
                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 17, 7, 60, 192);
                shape.TextFrame.TextRange.Text = text;
                shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
                shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
                shape.Line.Visible = false;
                shape.Fill.ForeColorIndex = ExcelKnownColors.Green;
                shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;

                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.DownArrow, 20, 8, 40, 64);
                shape.Fill.ForeColorIndex = ExcelKnownColors.White;
                shape.Line.ForeColorIndex = ExcelKnownColors.Blue;
                shape.Line.Weight = 1;

                text = "Release";
                shape = worksheet.Shapes.AddAutoShapes(AutoShapeType.RoundedRectangle, 22, 7, 60, 192);
                shape.TextFrame.TextRange.Text = text;
                shape.TextFrame.TextRange.RichText.SetFont(0, text.Length - 1, font);
                shape.TextFrame.TextRange.RichText.SetFont(0, 0, font2);
                shape.Line.Visible = false;
                shape.Fill.ForeColorIndex = ExcelKnownColors.Lavender;
                shape.TextFrame.VerticalAlignment = ExcelVerticalAlignment.MiddleCentered;
                #endregion

                //Saving the workbook to disk.
                string filename = "AutoShapes.xlsx";
                MemoryStream stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                DependencyService.Get<ISave>().SaveAndView(filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);

                #region Stream Dispose               
                stream.Dispose();
                #endregion     
            }           
        }
        #endregion
    }
}