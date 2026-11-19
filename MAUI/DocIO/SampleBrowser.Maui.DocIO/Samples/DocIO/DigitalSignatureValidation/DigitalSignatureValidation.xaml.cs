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
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using Color = Syncfusion.Drawing.Color;
using System.Text;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class DigitalSignatureValidation : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public DigitalSignatureValidation()
        {
            InitializeComponent();
            assembly = typeof(DigitalSignatureValidation).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Validates the digital signatures in the Word document and
        /// displays the result string in the result editor.
        /// </summary>
        private void Button_Click(object? sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.DigitalSignatureInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.DigitalSignatureInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            StringBuilder builder = new StringBuilder();
            if (document.DigitalSignatures.Count > 0)
            {
                builder.AppendLine("The following digital signatures were found in the Word document");
                int index = 1;
                foreach (OfficeDigitalSignature signature in document.DigitalSignatures)
                {
                    bool isValid = signature.IsValid;
                    builder.AppendLine();
                    builder.AppendLine($"{index:D3}:");
                    if (isValid)
                        builder.AppendLine("The signature is valid");
                    else
                        builder.AppendLine("The signature is invalid");
                    //Signature details
                    builder.AppendLine("Signed at : " + signature.SigningTime);
                    builder.AppendLine("Subject name : " + signature.Certificate.Subject);
                    builder.AppendLine("Issuer name : " + signature.Certificate.Issuer);
                    builder.AppendLine("Valid From : " + signature.Certificate.ValidFrom);
                    builder.AppendLine("Valid To : " + signature.Certificate.ValidTo);
                    builder.AppendLine(); // Empty line between signatures
                    index++;
                }
            }
            else
            {
                builder.AppendLine("The document has no digital signatures.");
            }
            //Display the validation result in the read-only editor.
            resultEditor.Text = builder.ToString();
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object? sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.DigitalSignatureInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.DigitalSignatureInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("DigitalSignatureInput.docx", "application/msword", ms);
        }
        #endregion
    }
}