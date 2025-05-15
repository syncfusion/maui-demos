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
using Syncfusion.Drawing;
using System;
using System.IO;
using System.Reflection;
using SizeF = Syncfusion.Drawing.SizeF;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class CompareDocuments : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public CompareDocuments()
        {
            InitializeComponent();
            assembly = typeof(CompareDocuments).GetTypeInfo().Assembly;
#if ANDROID || IOS
            btnCompareDocuments.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Compare two Word documents.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the original input word document.
            string documentPath = "SampleBrowser.Maui.Resources.DocIO.OriginalDocument.docx";
            if (BaseConfig.IsIndividualSB)
                documentPath = "SampleBrowser.Maui.DocIO.Resources.DocIO.OriginalDocument.docx";
            using Stream? originalfileStream = assembly.GetManifestResourceStream(documentPath);
            //Creates a new Word document instance.
            using WordDocument originalDocument = new();
            //Opens an existing original input word document.
            originalDocument.Open(originalfileStream, FormatType.Markdown);

            //Gets the revised input word document.
            documentPath = "SampleBrowser.Maui.Resources.DocIO.RevisedDocument.docx";
            if (BaseConfig.IsIndividualSB)
                documentPath = "SampleBrowser.Maui.DocIO.Resources.DocIO.RevisedDocument.docx";
            using Stream? revisedfileStream = assembly.GetManifestResourceStream(documentPath);
            //Creates a new Word document instance.
            using WordDocument revisedDocument = new();
            //Opens an existing revised input word document.
            revisedDocument.Open(revisedfileStream, FormatType.Markdown);

            if (detectFormatChanges.IsChecked)
            {
                //Compares the original document with revised document
                originalDocument.Compare(revisedDocument, "Nancy Davolio", DateTime.Now.AddDays(-1));
            }
            else
            {
                //Disable the flag to ignore the formatting changes while comparing the documents
                ComparisonOptions comparisonOptions = new ComparisonOptions();
                comparisonOptions.DetectFormatChanges = false;
                originalDocument.Compare(revisedDocument, "Nancy Davolio", DateTime.Now.AddDays(-1), comparisonOptions);
            }

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            originalDocument.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Compare Documents.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        #endregion
    }
}