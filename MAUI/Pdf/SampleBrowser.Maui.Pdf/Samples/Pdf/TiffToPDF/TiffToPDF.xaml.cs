#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using System.Reflection;
using Syncfusion.Pdf;
using SampleBrowser.Maui.Base;
using Syncfusion.Pdf.Graphics;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class TiffToPDF : SampleView
    {
        public TiffToPDF()
        {
            InitializeComponent();
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(TiffToPDF).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? imageFileStream = assembly.GetManifestResourceStream(basePath + "image.tiff");
            //Create a new PDF document
            PdfDocument document = new PdfDocument();
            //Set margin to the page
            document.PageSettings.Margins.All = 0;
            //Load Multiframe Tiff image
            PdfTiffImage tiffImage = new PdfTiffImage(imageFileStream);
            //Get the frame count
            int frameCount = tiffImage.FrameCount;
            //Access each frame and draw into the page
            for (int i = 0; i < frameCount; i++)
            {
                //Add new page for each frames
                PdfPage page = document.Pages.Add();
                //Get page graphics
                PdfGraphics graphics = page.Graphics;
                //Set active frame.
                tiffImage.ActiveFrame = i;
                //Draw Tiff image into page
                graphics.DrawImage(tiffImage, 0, 0, page.GetClientSize().Width, page.GetClientSize().Height);
            }
            //Save PDF document
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            //Close the PDF document
            document.Close(true);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new SaveService();
            saveService.SaveAndView("TiffToPDF.pdf", "application/pdf", ms);
        }
    }
}