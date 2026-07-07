#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
#if PresentationSB
using SampleBrowser.Maui.Presentation.Services;
using SkiaSharp;
#else
using SampleBrowser.Maui.Services;
#endif
using Syncfusion.Presentation;
using Syncfusion.PresentationRenderer;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SampleBrowser.Maui.Presentation.Presentation
{
    public partial class FindAndReplace : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public FindAndReplace()
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
        private void OnButtonClickedReplace(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(FindAndReplace).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.InputTemplate.pptx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.Presentation.Resources.Presentation.InputTemplate.pptx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            
            //Finds the first occurrence of a particular text.
            ITextSelection textSelection = presentation.Find("{product}", false, false);

            if (textSelection != null)
            {
                //Gets the found text as single text part
                ITextPart textPart = textSelection.GetAsOneTextPart();
                //Replace the text
                textPart.Text = "Service";
            }
            
            Regex regex = new Regex("{[A-Za-z]+}");
            //Finds all the occurrence of a particular text pattern
            ITextSelection[] textSelections = presentation.FindAll(regex);

            if (textSelections != null)
            {
                foreach (ITextSelection textSelection2 in textSelections)
                {
                    //Gets the found text as single text part
                    ITextPart textPart = textSelection2.GetAsOneTextPart();
                    //Replace the text
                    textPart.Text = "Service";
                }
            }

            using MemoryStream ms = new();
            presentation.Save(ms);
            ms.Position = 0;
            SaveService saveService = new();
            saveService.SaveAndView("FindAndReplace.pptx", "/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }

        /// <summary>
        /// Converts the PowerPoint slide to an image.
        /// </summary>
        private void OnButtonClickedView(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(FindAndReplace).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.InputTemplate.pptx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.Presentation.Resources.Presentation.InputTemplate.pptx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("View Template.pptx", "/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }
        #endregion
    }
}
