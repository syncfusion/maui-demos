#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
#if DOCIOSB
using SampleBrowser.Maui.DocIO.Services;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class EncryptAndDecrypt : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EncryptAndDecrypt()
        {
            InitializeComponent();
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnEncryptDocument.HorizontalOptions = LayoutOptions.Center;
            btnDecryptDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Encrypts the Word document.
        /// </summary>
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            Assembly assembly = typeof(EncryptAndDecrypt).GetTypeInfo().Assembly;
            //Gets the input Word document.
            string dataPath = @"SampleBrowser.Maui.Resources.DocIO.Adventure.docx";
            if (BaseConfig.IsIndividualSB)
                dataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.Adventure.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(dataPath);
            //Loads an existing encrypted Word document.
            using WordDocument document = new(fileStream, FormatType.Docx);

            //Encrypts the document by giving password.
            document.EncryptDocument("syncfusion");

            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Encrypt.docx", "application/msword", ms);
        }

        /// <summary>
        /// Decrypts the Word document.
        /// </summary>
        private void DecryptButton_Click(object sender, EventArgs e)
        {
            Assembly assembly = typeof(EncryptAndDecrypt).GetTypeInfo().Assembly;
            //Gets the input Word document.
            string dataPath = @"SampleBrowser.Maui.Resources.DocIO.Decrypt.docx";
            if (BaseConfig.IsIndividualSB)
                dataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.Decrypt.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(dataPath);
            //Loads an existing encrypted Word document.
            using WordDocument document = new(fileStream, FormatType.Docx, "syncfusion");
            using MemoryStream ms = new();

            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            document.Close();
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Decrypt.docx", "application/msword", ms);
        }
        #endregion
    }
}