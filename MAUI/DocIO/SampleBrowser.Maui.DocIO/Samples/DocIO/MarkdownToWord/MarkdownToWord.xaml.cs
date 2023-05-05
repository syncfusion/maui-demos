#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    public partial class MarkdownToWord : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public MarkdownToWord()
        {
            InitializeComponent();
            assembly = typeof(MarkdownToWord).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Converts the Markdown file to Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the input Markdown document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.MarkdownToWord.md";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.MarkdownToWord.md";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Creates a new Word document instance.
            using WordDocument document = new();
            //Opens an existing Markdown document.
            document.Open(fileStream, FormatType.Markdown);

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("MarkdownToWord.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        #endregion
    }
}
