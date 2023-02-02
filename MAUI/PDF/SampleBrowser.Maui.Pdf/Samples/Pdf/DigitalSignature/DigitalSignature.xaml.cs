#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class DigitalSignature : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public DigitalSignature()
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
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(Encryption).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "digital_signature_template.pdf");

            //Load the PDF document from stream.
            PdfLoadedDocument loadedDocument = new(documentStream);

            //Get the .pfx certificate file stream.
            Stream? certificateStream = assembly.GetManifestResourceStream(basePath + "certificate.pfx");

            //Get the signature field to add digital signature. 
            PdfLoadedSignatureField? signatureField = loadedDocument.Form.Fields[6] as PdfLoadedSignatureField;
            //Get the signature bounds. 
            RectangleF bounds = signatureField!.Bounds;

            //Create PDF certificate using certificate stream and password.
            PdfCertificate pdfCertificate = new(certificateStream, "password123");

            //Add certificate to first page of the document.
            PdfSignature signature = new(loadedDocument, loadedDocument.Pages[0], pdfCertificate, "", signatureField);
            signature.ContactInfo = "johndoe@owned.us";
            signature.LocationInfo = "Honolulu, Hawaii";
            signature.Reason = "I am author of this document.";

            //Set the cryptographic standard to signature settings. 
            signature.Settings.CryptographicStandard = CryptographicStandard.CADES;

            signature.Settings.DigestAlgorithm = DigestAlgorithm.SHA512;

            //Get the signature field appearance graphics.
            PdfGraphics graphics = signature.Appearance.Normal.Graphics;
            if (graphics != null)
            {
                //Draw the rectangle in appearance graphics.
                graphics.DrawRectangle(PdfPens.Black, bounds);

                //Get the image file stream from assembly. 
                Stream? imageStream = assembly.GetManifestResourceStream(basePath + "signature.png");

                //Create the PDF bitmap image from stream. 
                PdfBitmap bitmap = new(imageStream, true);
                //Draw image to appearance graphics. 
                graphics.DrawImage(bitmap, new RectangleF(2, 1, 30, 30));

                //Get certificate subject name.
                string subject = pdfCertificate.SubjectName;

                //Create a new font instance and draw a text to appearance graphics. 
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7);
                RectangleF textRect = new(45, 0, bounds.Width - 45, bounds.Height);
                PdfStringFormat format = new(PdfTextAlignment.Justify);
                graphics.DrawString("Digitally signed by " + subject + " \r\nReason: Testing signature \r\nLocation: USA", font, PdfBrushes.Black, textRect, format);
            }

            //Set the digital signature to signing the field. 
            signatureField.Signature = signature;

            using MemoryStream stream = new();
            //Saves the PDF to the memory stream.
            loadedDocument.Save(stream);
            loadedDocument.Close();

            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("DigitalSignature.pdf", "application/pdf", stream);
        }
        #endregion


    }
}
