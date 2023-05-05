#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using System.Reflection;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Exporting;
using SampleBrowser.Maui.Base;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class RemoveImage : SampleView
    {
        public RemoveImage()
        {
            InitializeComponent();
        }
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(RemoveImage).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "RemoveImageTemplate.pdf");
            using MemoryStream ms = new();
            documentStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("RemoveImageTemplate.pdf", "application/pdf", ms);
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(RemoveImage).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "RemoveImageTemplate.pdf");
            //Load the PDF document from stream.
            PdfLoadedDocument document = new PdfLoadedDocument(documentStream);
            //Load the first page.
            PdfPageBase page = document.Pages[0];
            //Extract images from the first page.
            PdfImageInfo[] imageInfo = page.ExtractImages();
            //Remove the Image.
            page.RemoveImage(imageInfo[0]);
            using MemoryStream ms = new();
            //Saves the PDF to the memory stream.
            document.Save(ms);
            //Close the PDF document
            document.Close(true);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new SaveService();
            saveService.SaveAndView("RemoveImage.pdf", "application/pdf", ms);
        }
    }
}