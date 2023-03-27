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
using SkiaSharp;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using SampleBrowser.Maui.Base;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class PDFToPDFAConformance : SampleView
    {
        public PDFToPDFAConformance()
        {
            InitializeComponent();
        }
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(PDFToPDFAConformance).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "Invoice.pdf");
            using MemoryStream ms = new();
            documentStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("ConformanceTemplate.pdf", "application/pdf", ms);
        }
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(PDFToPDFAConformance).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "Invoice.pdf");
            //Load the PDF document from stream.
            PdfLoadedDocument document = new PdfLoadedDocument(documentStream);
            //Sample level font event handling
            document.SubstituteFont += LoadedDocument_SubstituteFont;
            //convert a document to PDF/A standard document.
            document.ConvertToPDFA(PdfConformanceLevel.Pdf_A1B);
            using MemoryStream ms = new();
            //Saves the PDF to the memory stream.
            document.Save(ms);
            //Close the PDF document
            document.Close(true);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new SaveService();
            saveService.SaveAndView("Conformance.pdf", "application/pdf", ms);
        }
        static void LoadedDocument_SubstituteFont(object sender, PdfFontEventArgs args)
        {
            //get the font name
            string fontName = args.FontName.Split(',')[0];

            //get the font style
            PdfFontStyle fontStyle = args.FontStyle;

            SKFontStyle sKFontStyle = SKFontStyle.Normal;

            if (fontStyle != PdfFontStyle.Regular)
            {
                if (fontStyle == PdfFontStyle.Bold)
                {
                    sKFontStyle = SKFontStyle.Bold;
                }
                else if (fontStyle == PdfFontStyle.Italic)
                {
                    sKFontStyle = SKFontStyle.Italic;
                }
                else if (fontStyle == (PdfFontStyle.Italic | PdfFontStyle.Bold))
                {
                    sKFontStyle = SKFontStyle.BoldItalic;
                }
            }

            SKTypeface typeface = SKTypeface.FromFamilyName(fontName, sKFontStyle);

            SKStreamAsset typeFaceStream = typeface.OpenStream();

            MemoryStream? memoryStream = null;

            if (typeFaceStream != null && typeFaceStream.Length > 0)
            {
                //Create the fontData from the type face stream.
                byte[] fontData = new byte[typeFaceStream.Length - 1];

                typeFaceStream.Read(fontData, typeFaceStream.Length);
                typeFaceStream.Dispose();

                //Create the new memory stream from the font data.
                memoryStream = new MemoryStream(fontData);
            }

            //set the font stream to the event args.
            args.FontStream = memoryStream;
        }
    }
}