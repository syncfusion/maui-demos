using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
#if DOCIOSB
using SampleBrowser.Maui.DocIO.Services;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Office;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class DigitalSignature : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public DigitalSignature()
        {
            InitializeComponent();
            assembly = typeof(DigitalSignature).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnVisibleSignDocument.HorizontalOptions = LayoutOptions.Center;
            btnInvisibleSignDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Create a new Word document and digitally sign it using visible signature.
        /// </summary>
        private void VisibleButton_Click(object? sender, EventArgs e)
        {
            OfficeDigitalSignatureCertificate sigCertificate = CreateCertificate();
            //Create a new Word document.
            using WordDocument document = new WordDocument();
            AddContentToDocument(document);
            AddSignatureLine(document);
            SignVisibleDocument(document, sigCertificate);
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("VisibleSignature.docx", "application/msword", ms);
        }

        /// <summary>
        /// Create a new Word document and digitally sign it using invisible signature.
        /// </summary>
        private void InvisibleButton_Click(object? sender, EventArgs e)
        {
            OfficeDigitalSignatureCertificate sigCertificate = CreateCertificate();
            //Create a new Word document.
            using WordDocument document = new WordDocument();
            AddContentToDocument(document);
            SignInvisibleDocument(document, sigCertificate);
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            document.Close();
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("InvisibleSignature.docx", "application/msword", ms);
        }
        #endregion

        #region Helper methods
        private OfficeDigitalSignatureCertificate CreateCertificate()
        {
            //Gets the input Word document.
            string certResourcePath = @"SampleBrowser.Maui.Resources.DocIO.Certificate.pfx";
            if (BaseConfig.IsIndividualSB)
                certResourcePath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.Certificate.pfx";
            using Stream? certFileStream = assembly.GetManifestResourceStream(certResourcePath);
            //OfficeDigitalSignatureCertificate only accepts a file path, so extract
            //the embedded certificate resource to a temporary file on disk first.
            string certTempPath = Path.Combine(Path.GetTempPath(), "Certificate.pfx");
            using (FileStream fs = new FileStream(certTempPath, FileMode.Create, FileAccess.Write))
            {
                certFileStream?.CopyTo(fs);
            }
            return new OfficeDigitalSignatureCertificate(certTempPath, "password123");
        }

        private WordDocument AddContentToDocument(WordDocument document)
        {
            IWSection section = document.AddSection();
            WTextBody body = section.Body;
            IWTextRange textRange = body.AddParagraph().AppendText("INVOICE");
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontSize = 24;
            body.AddParagraph();
            textRange = body.AddParagraph().AppendText("This invoice has been generated for the goods and services provided as per our agreement.\r\nWe value your business and look forward to serving you again in the future.");
            body.AddParagraph();
            return document;
        }

        private void SignVisibleDocument(WordDocument document, OfficeDigitalSignatureCertificate certificate)
        {
            string imageDataPath = @"SampleBrowser.Maui.Resources.DocIO.Signature.png";
            if (BaseConfig.IsIndividualSB)
                imageDataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.Signature.png";
            using Stream? imageFileStream = assembly.GetManifestResourceStream(imageDataPath);
            byte[] signatureImage = GetImageBytes(imageFileStream ?? Stream.Null);
            WPicture? picture = GetSignatureLinePicture(document);
            if (picture == null)
                throw new Exception("Signature line not found.");
            SignatureSettings settings = new SignatureSettings
            {
                SignatureLineId = picture.SignatureLine.Id,
                SignTime = DateTime.Now,
                SignatureLineImage = signatureImage,
                Comments = "Approved",
                XmlDsigLevel = XmlDsigLevel.XmlDSig
            };
            document.AddDigitalSignature(certificate, settings);
        }

        private void AddSignatureLine(WordDocument document)
        {
            SignatureLineSettings settings = new SignatureLineSettings
            {
                Signer = "John Doe",
                SignerTitle = "Manager",
                Email = "john.doe@company.com",
                ShowDate = true
            };
            document.LastSection
                    .AddParagraph()
                    .AppendSignatureLine(settings, 192, 96);
        }

        private WPicture? GetSignatureLinePicture(WordDocument document)
        {
            WPicture? picture = null;
            foreach (WSection section in document.Sections)
            {
                foreach (WParagraph paragraph in section.Paragraphs)
                {
                    foreach (Entity entity in paragraph.ChildEntities)
                    {
                        if (entity is WPicture currentPicture &&
                            currentPicture.IsSignatureLine)
                        {
                            picture = currentPicture;
                            break;
                        }
                    }
                    if (picture != null)
                        break;
                }
                if (picture != null)
                    break;
            }
            return picture;
        }

        private byte[] GetImageBytes(Stream? image)
        {
            if (image == null)
                return Array.Empty<byte>();
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private void SignInvisibleDocument(WordDocument document, OfficeDigitalSignatureCertificate certificate)
        {
            SignatureSettings settings = new SignatureSettings
            {
                Comments = "Approved",
                XmlDsigLevel = XmlDsigLevel.XmlDSig
            };
            document.AddDigitalSignature(certificate, settings);
        }
        #endregion
    }
}