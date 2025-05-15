#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
using Syncfusion.DocIORenderer;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class TableOfFigures : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public TableOfFigures()
        {
            InitializeComponent();
            assembly = typeof(TableOfFigures).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates a Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {

            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.TableOfFiguresInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.TableOfFiguresInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            #region Add Table of Figures
            //Create a new paragraph.
            WParagraph paragraph = new WParagraph(document);
            paragraph.AppendText("List of Figures");
            //Apply Heading1 style for paragraph.
            paragraph.ApplyStyle(BuiltinStyle.Heading1);
            //Insert the paragraph. 
            document.LastSection.Body.ChildEntities.Insert(0, paragraph);

            //Create new paragraph and append TOC.
            paragraph = new WParagraph(document);
            TableOfContent tableOfContent = paragraph.AppendTOC(1, 3);
            //Disable a flag to exclude heading style paragraphs in TOC entries.
            tableOfContent.UseHeadingStyles = false;
            //Set the name of SEQ field identifier for table of figures.
            tableOfContent.TableOfFiguresLabel = "Figure";
            //Disable the flag, to exclude caption's label and number in TOC entries.
            tableOfContent.IncludeCaptionLabelsAndNumbers = false;

            //Insert the paragraph to the text body.
            document.LastSection.Body.ChildEntities.Insert(1, paragraph);
            #endregion

            #region Add caption for pictures
            //Find all pictures from the document.
            List<Entity> pictures = document.FindAllItemsByProperty(EntityType.Picture, null, null);
            // Iterate each picture and add caption.
            foreach (WPicture picture in pictures)
            {
                //Set alternate text as caption for picture.
                WParagraph captionPara = (WParagraph)picture.AddCaption("Figure", CaptionNumberingFormat.Number, CaptionPosition.AfterImage);
                captionPara.AppendText(" " + picture.AlternativeText);
                //Apply formatting to the caption.
                captionPara.ApplyStyle(BuiltinStyle.Caption);
                captionPara.ParagraphFormat.BeforeSpacing = 8;
                captionPara.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            }
            #endregion


            #region Add Table of Tables
            // Create a new paragraph.
            paragraph = new WParagraph(document);
            paragraph.AppendText("List of Tables");
            // Apply Heading1 style for paragraph.
            paragraph.ApplyStyle(BuiltinStyle.Heading1);
            // Insert the paragraph.
            document.LastSection.Body.ChildEntities.Insert(2, paragraph);

            //Create a new paragraph and append TOC.
            paragraph = new WParagraph(document);
            tableOfContent = paragraph.AppendTOC(1, 3);
            //Disable a flag to exclude heading style paragraphs in TOC entries.
            tableOfContent.UseHeadingStyles = false;
            //Set the name of SEQ field identifier for table of tables.
            tableOfContent.TableOfFiguresLabel = "Table";
            //Disable the flag, to exclude caption's label and number in TOC entries.
            tableOfContent.IncludeCaptionLabelsAndNumbers = false;
            // Insert the paragraph to the text body.
            document.LastSection.Body.ChildEntities.Insert(3, paragraph);
            #endregion

            #region Add caption for tables
            // Find all tables from the document.
            List<Entity> tables = document.FindAllItemsByProperty(EntityType.Table, null, null);
            //Iterate each table and add caption.
            foreach (WTable table in tables)
            {
                //Gets the table index.
                int tableIndex = table.OwnerTextBody.ChildEntities.IndexOf(table);
                //Create a new paragraph and appends the sequence field to use as a caption.
                WParagraph captionPara = new WParagraph(document);
                captionPara.AppendText("Table ");
                captionPara.AppendField("Table", FieldType.FieldSequence);
                //Set alternate text as caption for table.
                captionPara.AppendText(" " + table.Description);
                // Apply formatting to the paragraph.
                captionPara.ApplyStyle(BuiltinStyle.Caption);
                captionPara.ParagraphFormat.BeforeSpacing = 8;
                captionPara.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                //Insert the paragraph next to the table.
                table.OwnerTextBody.ChildEntities.Insert(tableIndex + 1, captionPara);
            }
            #endregion

            #region Update
            //Update all document fields to update SEQ fields.
            document.UpdateDocumentFields();
            //Update the table of contents.
            document.UpdateTableOfContents();
            #endregion


            using MemoryStream ms = new();
            //Saves the word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("TableOfFigures.docx", "application/msword", ms);
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.TableOfFiguresInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.TableOfFiguresInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("TableOfFiguresInput.docx", "application/msword", ms);
        }
        #endregion
    }
}