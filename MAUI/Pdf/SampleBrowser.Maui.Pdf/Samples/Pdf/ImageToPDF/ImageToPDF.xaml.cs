#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.Pdf;
using System.Reflection;

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class ImageToPDF : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public ImageToPDF()
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
            //Get the image file stream from assembly.
            Assembly assembly = typeof(Certificate).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            List<Stream?> imageStreams = new List<Stream?>();
            for (int i = 1; i <= 6; i++)
            {
                Stream? jpgImageStream = assembly.GetManifestResourceStream(basePath + "pdf_succinctly_page" + i + ".jpg");
                imageStreams.Add(jpgImageStream);
            }            
            //Create image to PDF converter.
            ImageToPdfConverter imageToPdfConverter = new ImageToPdfConverter();

            //Set image position.
            imageToPdfConverter.ImagePosition = PdfImagePosition.FitToPageAndMaintainAspectRatio;

            //Convert images to PDF file.
            PdfDocument document = imageToPdfConverter.Convert(imageStreams);

            //Saves the PDF to the memory stream.
            using MemoryStream stream = new();
            document.Save(stream);
            document.Close();
            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("ImageToPDF.pdf", "application/pdf", stream);
        }
        #endregion
    }
}
