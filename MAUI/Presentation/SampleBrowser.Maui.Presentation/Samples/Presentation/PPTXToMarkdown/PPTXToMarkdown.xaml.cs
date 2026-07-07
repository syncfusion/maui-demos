
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
    public partial class PPTXToMarkdown : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public PPTXToMarkdown()
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
            Assembly assembly = typeof(PPTXToMarkdown).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.PPTXToMarkdown.pptx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.Presentation.Resources.Presentation.PPTXToMarkdown.pptx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //FileStream outputStream1 = new FileStream(resourcePath, FileMode.Create);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            string outputPath = "SampleBrowser.Maui.Resources.Presentation.Output.md";
            if (BaseConfig.IsIndividualSB)
                outputPath = "SampleBrowser.Maui.Presentation.Resources.Presentation.Output.md";
            using MemoryStream ms = new();

            using Stream? outputStream = assembly.GetManifestResourceStream(outputPath);
            outputStream!.CopyTo(ms);
            ms.Position = 0;
            presentation.Save(ms, FormatType.Markdown);
            ms.Position = 0;
            SaveService saveService = new();
            //Saves the memory stream as file.
            saveService.SaveAndView("PPTXToMarkdown.md", "text/markdown", ms);
        }

        /// <summary>
        /// Converts the PowerPoint Presentation to Markdown.
        /// </summary>
        private void OnButtonClickedView(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(PPTXToMarkdown).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.PPTXToMarkdown.pptx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.Presentation.Resources.Presentation.PPTXToMarkdown.pptx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("PPTXToMarkdown.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", ms);
        }
        #endregion
    }
}
