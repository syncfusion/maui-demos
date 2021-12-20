using SampleBrowser.Maui.Core;
using System;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Microsoft.Maui.Controls;
using System.IO;

namespace SampleBrowser.Maui.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class Bookmarks : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public Bookmarks()
        {
            InitializeComponent();
#if ANDROID || IOS
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Appends bookmarks into the Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Creates a new Word document.
            using WordDocument document = new();
            //Adds a section to the document.
            IWSection section = document.AddSection();
            //Adds a new paragraph to the section.
            IWParagraph paragraph = section.AddParagraph();
            //Appends text.
            paragraph.AppendText("This document demonstrates Essential DocIO's Bookmark functionality.").CharacterFormat.FontSize = 14f;
            //Adds paragraph to the section.
            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.AppendText("1. Inserting Bookmark Text").CharacterFormat.FontSize = 12f;

            //Adds paragraph to the section.
            section.AddParagraph();
            paragraph = section.AddParagraph();

            #region Bookmark Creation
            //Appends BookmarkStart.
            paragraph.AppendBookmarkStart("Bookmark");
            //Appends text.
            paragraph.AppendText("Bookmark Text");
            //Appends BookmarkEnd.
            paragraph.AppendBookmarkEnd("Bookmark");

            //Adds paragraph to the section.
            section.AddParagraph();
            paragraph = section.AddParagraph();
            //Indicates hidden bookmark text start.
            paragraph.AppendBookmarkStart("_HiddenText");
            //Appends bookmark text.
            paragraph.AppendText("2. Hidden Bookmark Text").CharacterFormat.Font = new Syncfusion.Drawing.Font("Comic Sans MS", 10);
            //Indicates hidden bookmark text end.
            paragraph.AppendBookmarkEnd("_HiddenText");

            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.AppendText("3. Nested Bookmarks").CharacterFormat.FontSize = 12f;

            //Appends nested bookmarks.
            section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.AppendBookmarkStart("Main");
            paragraph.AppendText(" Main data ");
            paragraph.AppendBookmarkStart("Nested");
            paragraph.AppendText(" Nested data ");
            paragraph.AppendBookmarkStart("NestedNested");
            paragraph.AppendText(" Nested Nested ");
            paragraph.AppendBookmarkEnd("NestedNested");
            paragraph.AppendText(" data Nested ");
            paragraph.AppendBookmarkEnd("Nested");
            paragraph.AppendText(" Data Main ");
            paragraph.AppendBookmarkEnd("Main");
            #endregion Bookmark Creation

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            DependencyService.Get<ISave>().SaveAndView("Bookmarks.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        #endregion
    }
}