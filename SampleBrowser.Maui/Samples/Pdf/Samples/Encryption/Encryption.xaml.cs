using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using System.Reflection;
using System.IO;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Parsing;

namespace SampleBrowser.Maui.Pdf
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
            Stream documentStream = assembly.GetManifestResourceStream(basePath + "credit_card_statement.pdf");

            //Load the PDF document from stream.
            PdfLoadedDocument document = new PdfLoadedDocument(documentStream);

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
            //Saves the presentation to the memory stream.
            document.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Encryption.pdf", "application/pdf", ms);
        }
        #endregion

    }
}
