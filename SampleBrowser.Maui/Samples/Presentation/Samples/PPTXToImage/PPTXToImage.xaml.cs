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
using Syncfusion.PresentationRenderer;

namespace SampleBrowser.Maui.Presentation
{
    public partial class PPTXToImage : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public PPTXToImage()
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
            Assembly assembly = typeof(PPTXToImage).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.Template.pptx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            //Initializes PresentationRenderer to perform image conversion.
            presentation.PresentationRenderer = new PresentationRenderer();
            //Converts PowerPoint slide to image stream.
            using MemoryStream stream = (MemoryStream)presentation.Slides[0].ConvertToImage(Syncfusion.Presentation.ExportImageFormat.Jpeg);
            stream.Position = 0;
            //Saves the memory stream as image.
            DependencyService.Get<ISave>().SaveAndView("PPTXToImage.png", "image/png", stream);
        }

        /// <summary>
        /// Converts the PowerPoint slide to an image.
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
            DependencyService.Get<ISave>().SaveAndView("Template.pptx", "/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }
        #endregion
    }
}

