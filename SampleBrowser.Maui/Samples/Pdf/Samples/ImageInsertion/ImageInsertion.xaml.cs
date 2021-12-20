#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using System.Reflection;
using System.Data;
using Syncfusion.Pdf;
using System.IO;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;


namespace SampleBrowser.Maui.Pdf
{
    public partial class ImageInsertion : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public ImageInsertion()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Add table in a PowerPoint Presentation file.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add page to the PDF document. 
            PdfPage page = document.Pages.Add();
            //Create font with size and style. 
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //Draw text to PDF page with font and location. 
            graphics.DrawString("JPEG Image", font, PdfBrushes.Blue, new PointF(0, 40));

            //Get the image file stream from assembly.
            Assembly assembly = typeof(Certificate).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            Stream jpgImageStream = assembly.GetManifestResourceStream(basePath + "Xamarin_JPEG.jpg");

            //Create new image from stream. 
            PdfImage jpgImage = new PdfBitmap(jpgImageStream);
            //Draw the JPEG image
            graphics.DrawImage(jpgImage, new RectangleF(0, 70, 515, 215));
            //Draw text to PDF page with font and location. 
            graphics.DrawString("PNG Image", font, PdfBrushes.Blue, new PointF(0, 355));

            //Get the image file stream from assembly. 
            Stream pngImageStream = assembly.GetManifestResourceStream(basePath + "Xamarin_PNG.png");

            //Create new image from stream. 
            PdfImage pngImage = new PdfBitmap(pngImageStream);
            //Draw the PNG image
            graphics.DrawImage(pngImage, new RectangleF(0, 375, 199, 300));

            //Saves the presentation to the memory stream.
            using MemoryStream stream = new();
            document.Save(stream);
            document.Close();
            stream.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("ImageInsertion.pdf", "application/pdf", stream);
        }
        #endregion
    }
}
