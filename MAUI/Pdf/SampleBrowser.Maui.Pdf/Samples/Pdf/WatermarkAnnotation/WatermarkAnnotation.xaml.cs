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
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Interactive;
using PointF = Syncfusion.Drawing.PointF;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class WatermarkAnnotation : SampleView
    {
#region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public WatermarkAnnotation()
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
            //Get the image file stream from assembly.
            Assembly assembly = typeof(Certificate).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "Invoice.pdf");

            //Load the existing PDF document.
            PdfLoadedDocument document = new PdfLoadedDocument(documentStream);

            //Get the existing PDF page.
            PdfLoadedPage? page = document.Pages[0] as PdfLoadedPage;

            SizeF pageSize = page!.Size;

            //Create new watermark annotation.
            PdfWatermarkAnnotation watermarkAnnotation = new PdfWatermarkAnnotation(new RectangleF(0, 0, pageSize.Width, pageSize.Height));
            //Set opacity and annotation flags.
            watermarkAnnotation.Opacity = 0.25F;
            watermarkAnnotation.AnnotationFlags = PdfAnnotationFlags.Print;
            //Get the appearance graphics.
            PdfGraphics graphics = watermarkAnnotation.Appearance.Normal.Graphics;
            string watermarkText = "Confidential";
            PdfFont watermarkFont = new PdfStandardFont(PdfFontFamily.Helvetica, 40);
            SizeF textSize = watermarkFont.MeasureString(watermarkText);
            //Find the center position.
            float x = pageSize.Width / 2 - textSize.Width / 2;
            float y = pageSize.Height / 2;
            graphics.Save();
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(-45);
            //Draw the watermark content.
            graphics.DrawString(watermarkText, watermarkFont, PdfBrushes.Red, PointF.Empty);
            graphics.Restore();
            if (flatten.IsChecked)
            {
                watermarkAnnotation.Flatten = true;
            }
            //Add the watermark annotation to the PDF page.
            page.Annotations.Add(watermarkAnnotation);

            using MemoryStream stream = new();
            //Saves the PDF to the memory stream.
            document.Save(stream);
            //Close the PDF document
            document.Close(true);

            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("WatermarkAnnotation.pdf", "application/pdf", stream);
        }
#endregion 
    }
}
