using SampleBrowser.Maui.Core;
using System;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using Microsoft.Maui.Controls;
using System.Reflection;
using System.IO;

namespace SampleBrowser.Maui.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class WordToPDF : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public WordToPDF()
        {
            InitializeComponent();
            assembly = typeof(WordToPDF).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Converts Word document to PDF.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.WordtoPDF.docx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            //Creates a new DocIORenderer instance.
            using DocIORenderer render = new();
            //Enables a flag to preserve document structured tags in the converted tagged PDF.
            render.Settings.AutoTag = true;
            //Enables a flag to preserve the Word document form field as editable PDF form field in PDF document.
            render.Settings.PreserveFormFields = true;
            //Sets ExportBookmarks for preserving Word document headings as PDF bookmarks.
            render.Settings.ExportBookmarks = Syncfusion.DocIO.ExportBookmarkType.Headings;

            #region Preserve track changes in PDF
            //Enables to show the revision marks in the generated PDF for tracked changes or revisions in the Word document.
            document.RevisionOptions.ShowMarkup = RevisionType.Deletions | RevisionType.Formatting | RevisionType.Insertions;
            //Sets revision bars color as Black.
            document.RevisionOptions.RevisionBarsColor = RevisionColor.Black;
            //Sets revised properties (Formatting) color as Blue.
            document.RevisionOptions.RevisedPropertiesColor = RevisionColor.Blue;
            //Sets deleted text (Deletions) color as Yellow.
            document.RevisionOptions.DeletedTextColor = RevisionColor.Yellow;
            //Sets inserted text (Insertions) color as Pink.
            document.RevisionOptions.InsertedTextColor = RevisionColor.Pink;
            #endregion

            #region Preserve comments in PDF
            //Sets ShowInBalloons to render a document comments in converted PDF document.
            document.RevisionOptions.CommentDisplayMode = CommentDisplayMode.ShowInBalloons;
            //Sets the color to be used for Comment Balloon.
            document.RevisionOptions.CommentColor = RevisionColor.Blue;
            #endregion

            //Converts Word document into PDF.
            using PdfDocument pdfDocument = render.ConvertToPDF(document);
            using MemoryStream ms = new();
            //Saves the converted PDF document to the memory stream.
            pdfDocument.Save(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("WordtoPDF.pdf", "application/pdf", ms);
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.WordtoPDF.docx";
            using Stream fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("WordtoPDF.docx", "application/msword", ms);
        }
        #endregion
    }
}