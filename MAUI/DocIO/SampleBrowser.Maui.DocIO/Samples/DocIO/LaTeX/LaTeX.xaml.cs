#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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
using HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class LaTeX : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public LaTeX()
        {
            InitializeComponent();
            assembly = typeof(LaTeX).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Create a Word document with mathematical equation using LaTeX. 
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            string latex = @"f\left(x\right)={a}_{0}+\sum_{n=1}^{\infty}{\left({a}_{n}\cos{\frac{n\pi{x}}{L}}+{b}_{n}\sin{\frac{n\pi{x}}{L}}\right)}";
            //Creates a new Word document instance.
            using WordDocument document = new();
            //Add one section and one paragraph.
            document.EnsureMinimal();
            //Get the last section in the document
            WSection section = document.LastSection;
            //Set page margins
            section.PageSetup.Margins.All = 72;
            //Add new paragraph to the section
            IWParagraph paragraph = section.AddParagraph();

            //Append text to paragraph
            IWTextRange textRange = paragraph.AppendText("Mathematical equation");
            textRange.CharacterFormat.FontSize = 28;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            paragraph.ParagraphFormat.AfterSpacing = 12;

            //Add new paragraph to the section.
            paragraph = section.AddParagraph();
            paragraph.AppendMath(latex);


            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("LaTeXToMath.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        #endregion
    }
}
