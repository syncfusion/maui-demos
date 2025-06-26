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
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class DocumentProtection : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public DocumentProtection()
        {
            InitializeComponent();
            assembly = typeof(DocumentProtection).GetTypeInfo().Assembly;
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
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.TemplateReading.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.TemplateReading.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            
			// Access the paragraph
            WParagraph paragraph = (WParagraph)document.Sections[0].Body.ChildEntities[5];
            // Create a new editable range start
            EditableRangeStart editableRangeStart = new EditableRangeStart(document);
            // Insert the editable range start at the beginning of the selected paragraph
            paragraph.ChildEntities.Insert(0, editableRangeStart);
            // Set the editor group for the editable range to allow everyone to edit
            editableRangeStart.EditorGroup = EditorType.Everyone;
            // Append an editable range end to close the editable region
            paragraph.AppendEditableRangeEnd();
            
            // Access the first table in the first section of the document
            WTable table = (WTable)document.Sections[0].Tables[0];
            // Access the paragraph in the third row and third column of the table
            paragraph = (WParagraph) table[2, 2].ChildEntities[0];
            // Create a new editable range start for the table cell paragraph
            editableRangeStart = new EditableRangeStart(document);
            // Insert the editable range start at the beginning of the paragraph
            paragraph.ChildEntities.Insert(0, editableRangeStart);
            // Set the editor group for the editable range to allow everyone to edit
            editableRangeStart.EditorGroup = EditorType.Everyone;
            // Apply editable range to second column only
            editableRangeStart.FirstColumn = 1;
            editableRangeStart.LastColumn = 1;
            // Access the paragraph
            paragraph = (WParagraph) table[5, 2].ChildEntities[0];
            // Append an editable range end to close the editable region
            paragraph.AppendEditableRangeEnd();
			
            //Sets the protection with password and it allows only to modify the form fields type
            document.Protect(ProtectionType.AllowOnlyReading, "syncfusion");
			
            using MemoryStream ms = new();
            //Saves the word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("DocumentProtection.docx", "application/msword", ms);
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.TemplateReading.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.TemplateReading.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("TemplateReading.docx", "application/msword", ms);
        }
        #endregion
    }
}