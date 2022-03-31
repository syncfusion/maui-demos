#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using SampleBrowser.Maui.Services;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.Pdf
{
    public partial class Invoice : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public Invoice()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        RectangleF TotalPriceCellBounds = RectangleF.Empty;
        RectangleF QuantityCellBounds = RectangleF.Empty;
        /// <summary>
        /// Creates charts in a presentation.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument document = new();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Get the page width and height
            float pageWidth = page.GetClientSize().Width;
            float pageHeight = page.GetClientSize().Height;

            //Set the header height
            float headerHeight = 90;
            //Create brush with light blue color. 
            PdfColor lightBlue = Color.FromArgb(255, 91, 126, 215);
            PdfBrush lightBlueBrush = new PdfSolidBrush(lightBlue);
            //Create brush with dark blue color. 
            PdfColor darkBlue = Color.FromArgb(255, 65, 104, 209);
            PdfBrush darkBlueBrush = new PdfSolidBrush(darkBlue);
            //Create brush with white color. 
            PdfBrush whiteBrush = new PdfSolidBrush(Color.FromArgb(255, 255, 255, 255));

            //Get the font file stream from assembly. 
            Assembly assembly = typeof(Invoice).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            Stream? fontStream = assembly.GetManifestResourceStream(basePath + "arial.ttf");

            //Create PdfTrueTypeFont from stream with different size. 
            PdfTrueTypeFont headerFont = new(fontStream, 30, PdfFontStyle.Regular);
            PdfTrueTypeFont arialRegularFont = new(fontStream, 18, PdfFontStyle.Regular);
            PdfTrueTypeFont arialBoldFont = new(fontStream, 9, PdfFontStyle.Bold);

            //Create string format.
            PdfStringFormat format = new();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;

            float y = 0;
            float x = 0;

            //Set the margins of address.
            float margin = 30;

            //Set the line space.
            float lineSpace = 10;

            //Create border pen and draw the border to PDF page. 
            PdfColor borderColor = Color.FromArgb(255, 142, 170, 219);
            PdfPen borderPen = new(borderColor, 1f);
            graphics.DrawRectangle(borderPen, new RectangleF(0, 0, pageWidth, pageHeight));

            //Create a new PdfGrid 
            PdfGrid grid = new();

            //Add five columns to the grid.
            grid.Columns.Add(5);

            //Create the header row of the grid.
            PdfGridRow[] headerRow = grid.Headers.Add(1);

            //Set style to the header row and set value to the header cells. 
            headerRow[0].Style.BackgroundBrush = new PdfSolidBrush(new PdfColor(68, 114, 196));
            headerRow[0].Style.TextBrush = PdfBrushes.White;
            headerRow[0].Cells[0].Value = "Product ID";
            headerRow[0].Cells[0].StringFormat.Alignment = PdfTextAlignment.Center;
            headerRow[0].Cells[1].Value = "Product Name";
            headerRow[0].Cells[2].Value = "Price ($)";
            headerRow[0].Cells[3].Value = "Quantity";
            headerRow[0].Cells[4].Value = "Total ($)";

            //Add products to the grid table.
            AddProducts("CA-1098", "AWC Logo Cap", 8.99, 2, 17.98, grid);
            AddProducts("LJ-0192", "Long-Sleeve Logo Jersey,M", 49.99, 3, 149.97, grid);
            AddProducts("So-B909-M", "Mountain Bike Socks,M", 9.50, 2, 19, grid);
            AddProducts("LJ-0192", "Long-Sleeve Logo Jersey,M", 49.99, 4, 199.96, grid);
            AddProducts("FK-5136", "ML Fork", 175.49, 6, 1052.94, grid);
            AddProducts("HL-U509", "Sports-100 Helmet,Black", 34.99, 1, 34.99, grid);

            #region Header         

            //Fill the header with light blue brush 
            graphics.DrawRectangle(lightBlueBrush, new RectangleF(0, 0, pageWidth, headerHeight));

            string title = "INVOICE";

            //Specificy the bounds for total value. 
            RectangleF headerTotalBounds = new(400, 0, pageWidth - 400, headerHeight);

            //Measure the string size using font. 
            SizeF textSize = headerFont.MeasureString(title);
            graphics.DrawString(title, headerFont, whiteBrush, new RectangleF(0, 0, textSize.Width + 50, headerHeight), format);
            //Draw rectangle in PDF page. 
            graphics.DrawRectangle(darkBlueBrush, headerTotalBounds);
            //Draw the toal value to PDF page. 
            graphics.DrawString("$" + GetTotalAmount(grid).ToString(), arialRegularFont, whiteBrush, new RectangleF(400, 0, pageWidth - 400, headerHeight + 10), format);
            //Create font from font stream. 
            arialRegularFont = new PdfTrueTypeFont(fontStream, 9, PdfFontStyle.Regular);
            //Set bottom line alignment and draw the text to PDF page. 
            format.LineAlignment = PdfVerticalAlignment.Bottom;
            graphics.DrawString("Amount", arialRegularFont, whiteBrush, new RectangleF(400, 0, pageWidth - 400, headerHeight / 2 - arialRegularFont.Height), format);
            #endregion
            //Measure the string size using font. 
            SizeF size = arialRegularFont.MeasureString("Invoice Number: 2058557939");
            y = headerHeight + margin;
            x = (pageWidth - margin) - size.Width;
            //Draw text to PDF page with provided font and location. 
            graphics.DrawString("Invoice Number: 2058557939", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
            //Measure the string size using font.
            size = arialRegularFont.MeasureString("Date :" + DateTime.Now.ToString("dddd dd, MMMM yyyy"));
            x = (pageWidth - margin) - size.Width;
            y += arialRegularFont.Height + lineSpace;
            //Draw text to PDF page with provided font and location. 
            graphics.DrawString("Date: " + DateTime.Now.ToString("dddd dd, MMMM yyyy"), arialRegularFont, PdfBrushes.Black, new PointF(x, y));

            y = headerHeight + margin;
            x = margin;
            //Draw text to PDF page with provided font and location. 
            graphics.DrawString("Bill To:", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
            y += arialRegularFont.Height + lineSpace;
            graphics.DrawString("Abraham Swearegin,", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
            y += arialRegularFont.Height + lineSpace;
            graphics.DrawString("United States, California, San Mateo,", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
            y += arialRegularFont.Height + lineSpace;
            graphics.DrawString("9920 BridgePointe Parkway,", arialRegularFont, PdfBrushes.Black, new PointF(x, y));
            y += arialRegularFont.Height + lineSpace;
            graphics.DrawString("9365550136", arialRegularFont, PdfBrushes.Black, new PointF(x, y));

            #region Grid
            //Set width to grid columns. 
            grid.Columns[0].Width = 110;
            grid.Columns[1].Width = 150;
            grid.Columns[2].Width = 110;
            grid.Columns[3].Width = 70;
            grid.Columns[4].Width = 100;

            for (int i = 0; i < grid.Headers.Count; i++)
            {
                //Set height to the grid header row. 
                grid.Headers[i].Height = 20;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    //Create string format for header cell. 
                    PdfStringFormat pdfStringFormat = new();
                    pdfStringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                    pdfStringFormat.Alignment = PdfTextAlignment.Left;

                    //Set cell padding for header cell. 
                    if (j == 0 || j == 2)
                        grid.Headers[i].Cells[j].Style.CellPadding = new PdfPaddings(30, 1, 1, 1);
                    //Set string format to grid header cell. 
                    grid.Headers[i].Cells[j].StringFormat = pdfStringFormat;
                    //Set font to the grid header cell. 
                    grid.Headers[i].Cells[j].Style.Font = arialBoldFont;

                }
                //Set value to the grid header cell. 
                grid.Headers[0].Cells[0].Value = "Product ID";
            }
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                //Set height to the grid row. 
                grid.Rows[i].Height = 23;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    //Create string format for grid row. 
                    PdfStringFormat pdfStringFormat = new();
                    pdfStringFormat.LineAlignment = PdfVerticalAlignment.Middle;
                    pdfStringFormat.Alignment = PdfTextAlignment.Left;

                    //Set cell padding for grid row cell. 
                    if (j == 0 || j == 2)
                        grid.Rows[i].Cells[j].Style.CellPadding = new PdfPaddings(30, 1, 1, 1);

                    //Set string format to grid row cell. 
                    grid.Rows[i].Cells[j].StringFormat = pdfStringFormat;
                    //Set font to the grid row cell. 
                    grid.Rows[i].Cells[j].Style.Font = arialRegularFont;
                }
            }
            //Apply built-in table style to the grid. 
            grid.ApplyBuiltinStyle(PdfGridBuiltinStyle.ListTable4Accent5);
            //Subscribing to begin cell layout event.
            grid.BeginCellLayout += Grid_BeginCellLayout;
            //Draw the PDF grid to PDF page and get the layout result. 
            PdfGridLayoutResult result = grid.Draw(page, new PointF(0, y + 40));

            //Using the layout result, continue to draw the text. 
            y = result.Bounds.Bottom + lineSpace;
            format = new PdfStringFormat
            {
                Alignment = PdfTextAlignment.Center
            };
            RectangleF bounds = new(QuantityCellBounds.X, y, QuantityCellBounds.Width, QuantityCellBounds.Height);
            //Draw text to PDF page based on the layout result. 
            page.Graphics.DrawString("Grand Total:", arialBoldFont, PdfBrushes.Black, bounds, format);
            //Draw the total amount value to PDF page based on the layout result. 
            bounds = new RectangleF(TotalPriceCellBounds.X, y, TotalPriceCellBounds.Width, TotalPriceCellBounds.Height);
            page.Graphics.DrawString("$" + GetTotalAmount(grid).ToString(), arialBoldFont, PdfBrushes.Black, bounds);
            #endregion
            //Create border pen with custom dash style and draw the border to page. 
            borderPen.DashStyle = PdfDashStyle.Custom;
            borderPen.DashPattern = new float[] { 3, 3 };
            graphics.DrawLine(borderPen, new PointF(0, pageHeight - 100), new PointF(pageWidth, pageHeight - 100));

            //Get the image file stream from assembly.
            Stream? imageStream = assembly.GetManifestResourceStream(basePath + "AdventureWork.png");

            //Create PDF bitmap image from stream.
            PdfBitmap bitmap = new(imageStream);
            //Draw the image to PDF page. 
            graphics.DrawImage(bitmap, new RectangleF(10, pageHeight - 90, 80, 80));

            //Calculate the text position and draw the text to PDF page. 
            y = pageHeight - 100 + margin;
            size = arialRegularFont.MeasureString("800 Interchange Blvd.");
            x = pageWidth - size.Width - margin;
            graphics.DrawString("800 Interchange Blvd.", arialRegularFont, PdfBrushes.Black, new PointF(x, y));

            //Calculate the text position and draw the text to PDF page. 
            y += arialRegularFont.Height + lineSpace;
            size = arialRegularFont.MeasureString("Suite 2501,  Austin, TX 78721");
            x = pageWidth - size.Width - margin;
            graphics.DrawString("Suite 2501,  Austin, TX 78721", arialRegularFont, PdfBrushes.Black, new PointF(x, y));

            //Calculate the text position and draw the text to PDF page. 
            y += arialRegularFont.Height + lineSpace;
            size = arialRegularFont.MeasureString("Any Questions? support@adventure-works.com");
            x = pageWidth - size.Width - margin;
            graphics.DrawString("Any Questions? support@adventure-works.com", arialRegularFont, PdfBrushes.Black, new PointF(x, y));

            using MemoryStream ms = new();
            //Saves the PDF to the memory stream.
            document.Save(ms);
            //Close the PDF document
            document.Close(true);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Invoice.pdf", "application/pdf", ms);
        }

        private void Grid_BeginCellLayout(object sender, PdfGridBeginCellLayoutEventArgs args)
        {
            PdfGrid? grid = sender as PdfGrid;
            if (args.CellIndex == grid!.Columns.Count - 1)
            {
                //Get the bounds of price cell in grid row. 
                TotalPriceCellBounds = args.Bounds;
            }
            else if (args.CellIndex == grid.Columns.Count - 2)
            {
                //Get the bounds of quantity cell in grid row. 
                QuantityCellBounds = args.Bounds;
            }

        }
        #endregion

        #region Helper Methods
        //Create and row for the grid.
        void AddProducts(string productId, string productName, double price, int quantity, double total, PdfGrid grid)
        {
            //Add a new row and set the product value to grid row cells. 
            PdfGridRow row = grid.Rows.Add();
            row.Cells[0].Value = productId;
            row.Cells[1].Value = productName;
            row.Cells[2].Value = price.ToString();
            row.Cells[3].Value = quantity.ToString();
            row.Cells[4].Value = total.ToString();
        }
        /// <summary>
        /// Get the Total amount of purcharsed items.
        /// </summary>
        private float GetTotalAmount(PdfGrid grid)
        {
            float Total = 0f;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                string cellValue = (grid.Rows[i].Cells[grid.Columns.Count - 1].Value as string)!.ToString();
                float result = float.Parse(cellValue, System.Globalization.CultureInfo.InvariantCulture);
                Total += result;
            }
            return Total;

        }
        #endregion
    }
}
