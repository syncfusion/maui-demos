#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Pdf;
using System.Reflection;
using Syncfusion.XPS;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class XPSToPDF : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public XPSToPDF()
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
            Assembly assembly = typeof(XPSToPDF).GetTypeInfo().Assembly;

            string basePath = "SampleBrowser.Maui.Resources.Pdf.";

            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";

            //Load XPS file to stream
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "XPSToPDF.xps");

            XPSToPdfConverter converter = new XPSToPdfConverter();

            //Convert XPS document into PDF document
            PdfDocument document = converter.Convert(documentStream);

            //Creating the stream object.
            using MemoryStream stream = new();

            //Saves the PDF to the memory stream.
            document.Save(stream);

            //Close the PDF document
            document.Close(true);

            stream.Position = 0;

            //Save the output stream as a file using file picker.
            SaveService saveService = new();

            saveService.SaveAndView("XPSToPDF.pdf", "application/pdf", stream);
        }
        #endregion
    }
}