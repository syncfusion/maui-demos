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
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class Encryption : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public Encryption()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        /// <summary>
        /// Creates charts in a presentation.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Get the document file stream from assembly. 
            Assembly assembly = typeof(Encryption).GetTypeInfo().Assembly;
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";
            Stream? documentStream = assembly.GetManifestResourceStream(basePath + "credit_card_statement.pdf");

            //Load the PDF document from stream.
            PdfLoadedDocument document = new(documentStream);

            //Get the document security.
            PdfSecurity security = document.Security;

            //use 256 bits key in AES mode.
            security.KeySize = PdfEncryptionKeySize.Key256Bit;
            security.Algorithm = PdfEncryptionAlgorithm.AES;

            //Set the PdfEncryption option to encrypt the document.  
            security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContents;

            //Set the user and owner password.
            security.OwnerPassword = "syncfusion";
            security.UserPassword = "password@123";

            //Set the permission flags for the file. 
            security.Permissions = PdfPermissionsFlags.Print | PdfPermissionsFlags.FullQualityPrint;

            using MemoryStream ms = new();
            //Saves the PDF to the memory stream.
            document.Save(ms);
            //Close the PDF document
            document.Close(true);

            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Encryption.pdf", "application/pdf", ms);
        }
        #endregion

    }
}
