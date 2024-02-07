#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
#if PresentationSB
using SampleBrowser.Maui.Presentation.Services;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.Presentation;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.Presentation.Presentation
{
    public partial class HeaderAndFooter : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public HeaderAndFooter()
        {
            InitializeComponent();
#if ANDROID || IOS
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates a PowerPoint Presentation file with headers and footers.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(HeaderAndFooter).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.HeaderFooter.pptx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.Presentation.Resources.Presentation.HeaderFooter.pptx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            //Adds footers into all the PowerPoint slides.
            foreach (ISlide slide in presentation.Slides)
            {
                //Enables a date and time footer in slide.
                slide.HeadersFooters.DateAndTime.Visible = true;
                //Enables a footer in slide.
                slide.HeadersFooters.Footer.Visible = true;
                //Sets the footer text.
                slide.HeadersFooters.Footer.Text = "Footer";
                //Enables a slide number footer in slide.
                slide.HeadersFooters.SlideNumber.Visible = true;
            }

            //Adds header into first slide notes page.
            //Adds a notes slide to the slide.
            INotesSlide notesSlide = presentation.Slides[0].AddNotesSlide();
            //Enables a header in notes slide.
            notesSlide.HeadersFooters.Header.Visible = true;
            //Sets the header text.
            notesSlide.HeadersFooters.Header.Text = "Syncfusion PowerPoint Library";

            //Saves the presentation to the memory stream.
            using MemoryStream stream = new();
            presentation.Save(stream);
            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("HeaderFooter.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", stream);
        }
        #endregion
    }
}

