#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System;
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using Syncfusion.Presentation;
using System.Reflection;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.PresentationToPdfConverter;

namespace SampleBrowser.Maui.Presentation
{
    public partial class PPTXToPDF : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public PPTXToPDF()
        {
            InitializeComponent();
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Opens the input template PowerPoint Presentation file.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(PPTXToPDF).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.Template.pptx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            //Creates the MemoryStream to save the converted PDF.
            using MemoryStream pdfStream = new();
            //Converts the PowerPoint Presentation to PDF document.
            using (PdfDocument pdfDocument = PresentationToPdfConverter.Convert(presentation))
            {
                //Saves the converted PDF document to MemoryStream.
                pdfDocument.Save(pdfStream);
                pdfStream.Position = 0;
            }
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("PPTXToPDF.pdf", "application/pdf", pdfStream);
        }

        /// <summary>
        /// Converts the PowerPoint Presentation to PDF.
        /// </summary>
        private void OnButtonClickedView(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(PPTXToImage).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.Template.pptx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Template.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }
        #endregion
    }
}
