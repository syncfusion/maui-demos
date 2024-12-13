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
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using SampleBrowser.Maui.Base;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class CompressExistingPDF : SampleView
    {
        public CompressExistingPDF()
        {
            InitializeComponent();
        }
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(CompressExistingPDF).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "jQuery_Succinctly.pdf");
            using MemoryStream ms = new();
            documentStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("CompressionTemplate.pdf", "application/pdf", ms);
        }
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(CompressExistingPDF).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "jQuery_Succinctly.pdf");
            //Load the PDF document from stream.
            PdfLoadedDocument document = new PdfLoadedDocument(documentStream);
            //Initialize new instance of PdfCompressionOptions class.
            PdfCompressionOptions options = new PdfCompressionOptions();
            //set the compress images based on the image quality. 
            options.CompressImages = true;
            options.ImageQuality = 50;
            //Enable the optimize font option
            options.OptimizeFont = true;
            //Enable the optimize page contents.
            options.OptimizePageContents = true;
            //Set to remove the metadata information.
            options.RemoveMetadata = true;
            //Assign the compression option to the document
            document.Compress(options);
            using MemoryStream ms = new();
            //Saves the PDF to the memory stream.
            document.Save(ms);
            ms.Position = 0;

            //Calculate input and output document size in KB.
            double inputSize = ((double)documentStream!.Length / 1024);
            double outputSize = ((double)ms.Length / 1024);

            //Close the PDF document
            document.Close(true);

            //Open alert window to display the compression percentage.
			bool result = await App.Current!.Windows[0].Page!.DisplayAlert("PDF is now " + (100 - (int)((outputSize / inputSize) * 100)).ToString() + "% smaller!", ((int)inputSize) + " KB -> " + ((int)outputSize) + " KB", "View PDF", "Close");
            if (result)
            {
                //Saves the memory stream as file.
                SaveService saveService = new SaveService();
                saveService.SaveAndView("Compression.pdf", "application/pdf", ms);
            }
        }
    }
}