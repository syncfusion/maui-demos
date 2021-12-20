#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using System;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Microsoft.Maui.Controls;
using System.Reflection;
using System.IO;

namespace SampleBrowser.Maui.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class RTFToWord : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public RTFToWord()
        {
            InitializeComponent();
            assembly = typeof(RTFToWord).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Converts the RTF file to Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the input RTF document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.RTFtoWord.rtf";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Creates a new Word document instance.
            using WordDocument document = new();
            //Opens an existing RTF document.
            document.Open(fileStream, FormatType.Rtf);

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("RTFtoWord.docx", "application/msword", ms);
            #endregion Document SaveOption
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input RTF document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.RTFtoWord.rtf";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("RTFToWord.rtf", "application/msword", ms);
        }
        #endregion
    }
}
