#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using System.Reflection;
using SkiaSharp;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Redaction;
using SampleBrowser.Maui.Base;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class Redaction : SampleView
    {
        public Redaction()
        {
            InitializeComponent();
        }
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(Redaction).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "RedactionTemplate.pdf");
            using MemoryStream ms = new();
            documentStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("RedactionTemplate.pdf", "application/pdf", ms);
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(Redaction).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "RedactionTemplate.pdf");
			
            //Load the PDF document from stream.
            PdfLoadedDocument loadedDocument = new(documentStream);

            PdfLoadedPage? lpage = loadedDocument.Pages[0] as PdfLoadedPage;
			
			//Create PDF redaction for the page to redact text 
			PdfRedaction textRedaction = new PdfRedaction(new Syncfusion.Drawing.RectangleF(477f, 154f, 62.709f, 16.802f), Syncfusion.Drawing.Color.Black);

            PdfRedaction textRedaction2 = new PdfRedaction(new Syncfusion.Drawing.RectangleF(70, 240, 65.709f, 16.802f), Syncfusion.Drawing.Color.Black);

            PdfRedaction imageRedaction = new PdfRedaction(new Syncfusion.Drawing.RectangleF(52.14447f, 712.1465f, 126.10835f, 81.45297f), Syncfusion.Drawing.Color.Black);

            lpage.AddRedaction(textRedaction);
            lpage.AddRedaction(textRedaction2);
            lpage.AddRedaction(imageRedaction);
            loadedDocument.Redact();
			
            using MemoryStream stream = new();
            //Saves the PDF to the memory stream.
            loadedDocument.Save(stream);
            loadedDocument.Close();
            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new SaveService();
            saveService.SaveAndView("Redaction.pdf", "application/pdf", stream);
        }        
    }
}