#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Reflection;
using SizeF = Syncfusion.Drawing.SizeF;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class Certificate : SampleView
    {
#region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public Certificate()
        {
            InitializeComponent();
        }
#endregion

#region Events
        /// <summary>
        /// Creates slides with simple text, table and image in a PowerPoint Presentation.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument document = new();
            //Set PDF landscape page orientiation. 
            document.PageSettings.Orientation = PdfPageOrientation.Landscape;
            //Set page margins. 
            document.PageSettings.Margins.All = 0;
            //Add page to the PDF document. 
            PdfPage page = document.Pages.Add();
            //Get the page size to draw the contents to PDF page. 
            SizeF pageSize = page.GetClientSize();

            //Get the image file stream from assembly.
            Assembly assembly = typeof(Certificate).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? imageStream = assembly.GetManifestResourceStream(basePath + "certificate.jpg");

            //Load the PDF document from stream.
            PdfBitmap bitmapImage = new(imageStream);
            //Draw the PDF bitmap image to PDF page with provided size. 
            page.Graphics.DrawImage(bitmapImage, new RectangleF(0, 0, pageSize.Width, pageSize.Height));

            //Create font with different size. 
            PdfFont nameFont = new PdfStandardFont(PdfFontFamily.Helvetica, 22);
            PdfFont controlFont = new PdfStandardFont(PdfFontFamily.Helvetica, 19);
            PdfFont dateFont = new PdfStandardFont(PdfFontFamily.Helvetica, 16);
            PdfBrush textBrush = new PdfSolidBrush(new PdfColor(20, 58, 86));

            string name = "John Milton";
            string courseName = "Microsoft Azure Fundamentals";

            //Find the X position based on text and font size. 
            float x = calculateXPosition(name, nameFont, pageSize.Width);
            //Draw the string to specified location. 
            page.Graphics.DrawString(name, nameFont, textBrush, new RectangleF(x, 253, 0, 0));

            //Find the X position based on text and font size. 
            x = calculateXPosition(courseName, controlFont, pageSize.Width);
            //Draw the string to specified location. 
            page.Graphics.DrawString(courseName, controlFont, textBrush, new RectangleF(x, 340, 0, 0));

            ////Get date value from date picker field. 
            //var dateTimeOffset = date.Date;
            //DateTime time = dateTimeOffset.Value.DateTime;
            //Get the formatted Date to add in PDF page. 
            string formatedDate = "on October 10, 2021";// + time.ToString("MMMM d, yyyy");

            //Find the X position based on text and font size. 
            x = calculateXPosition(formatedDate, dateFont, pageSize.Width);
            //Draw the string to specified location. 
            page.Graphics.DrawString(formatedDate, dateFont, textBrush, new RectangleF(x, 385, 0, 0));

            using MemoryStream stream = new();
            //Saves the PDF to the memory stream.
            document.Save(stream);
            //Close the PDF document
            document.Close(true);

            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Certificate.pdf", "application/pdf", stream);
        }
#endregion
            
        float calculateXPosition(string text, PdfFont font, float pageWidth)
        {
            //Measure the text size based on the font size. 
            SizeF textSize = font.MeasureString(text, new SizeF(pageWidth, 0));
            return (pageWidth - textSize.Width) / 2;
        }

    }
}
