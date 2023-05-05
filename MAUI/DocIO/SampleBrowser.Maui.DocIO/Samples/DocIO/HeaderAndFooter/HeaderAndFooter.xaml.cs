#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    public partial class HeaderAndFooter : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public HeaderAndFooter()
        {
            InitializeComponent();
            assembly = typeof(HeaderAndFooter).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Inserts headers and footers in the Word document.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            #region HeaderandFooter
            //Gets the input Word document.
            string dataPath = @"SampleBrowser.Maui.Resources.DocIO.HeaderFooterTemplate.docx";
            if (BaseConfig.IsIndividualSB)
                dataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.HeaderFooterTemplate.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(dataPath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            //Gets a last section of the document.
            IWSection section = document.LastSection;
            //Sets the header/footer setup.
            section.PageSetup.DifferentFirstPage = true;
            //Inserts Header Footer to first page.
            InsertFirstPageHeaderFooter(document, section);
            //Inserts Header Footer to all pages.
            InsertPageHeaderFooter(document, section);

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Header and Footer.docx", "application/msword", ms);
            #endregion Document SaveOption
            #endregion HeaderandFooter
        }
        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string dataPath = @"SampleBrowser.Maui.Resources.DocIO.HeaderFooterTemplate.docx";
            if (BaseConfig.IsIndividualSB)
                dataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.HeaderFooterTemplate.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(dataPath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("HeaderFooterTemplate.docx", "application/msword", ms);
        }
        #endregion

        #region Helper methods
        #region InsertFirstPageHeaderFooter
        /// <summary>
        /// Inserts Header and Footer to first page.
        /// </summary>
        private void InsertFirstPageHeaderFooter(WordDocument doc, IWSection section)
        {
            //Adds a new table to the header.
            IWTable table = section.HeadersFooters.FirstPageHeader.AddTable();

            RowFormat format = new();

            //Sets cleared table border style.
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Cleared;

            //Inserts table with a row and two columns.
            table.ResetCells(1, 2, format, 265);

            //Inserts logo image to the table first cell.
            IWParagraph? headerPara = table[0, 0].AddParagraph() as WParagraph;
            string dataPath = @"SampleBrowser.Maui.Resources.DocIO.Northwindlogo.png";
            if (BaseConfig.IsIndividualSB)
                dataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.Northwindlogo.png";
            Stream? imageStream = assembly.GetManifestResourceStream(dataPath);
            headerPara!.AppendPicture(imageStream);
            //Sets Image size
            (headerPara.Items[0] as WPicture)!.Width = 232.5f;
            (headerPara.Items[0] as WPicture)!.Height = 54.75f;

            //Inserts text to the table second cell.
            headerPara = table[0, 1].AddParagraph() as WParagraph;
            IWTextRange? txt = headerPara!.AppendText("Company Headquarters,\n2501 Aerial Center Parkway,\nSuite 110, Morrisville, NC 27560,\nTEL 1-888-936-8638.");
            txt.CharacterFormat.FontSize = 12;
            txt.CharacterFormat.CharacterSpacing = 1.7f;
            headerPara.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
            //Adds a new paragraph to the header with address text.
            headerPara = new WParagraph(doc);
            headerPara.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            txt = headerPara.AppendText("\nFirst Page Header");
            txt.CharacterFormat.CharacterSpacing = 1.7f;
            section.HeadersFooters.FirstPageHeader.Paragraphs.Add(headerPara);

            //Adds a footer paragraph text to the document.
            WParagraph footerPara = new(doc);
            footerPara.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
            //Adds text.
            footerPara.AppendText("Copyright Northwind Inc. 2001 - 2021");
            //Adds page and Number of pages field to the document.
            footerPara.AppendText("\tPage ");
            footerPara.AppendField("Page", FieldType.FieldPage);
            footerPara.AppendText(" of ");
            footerPara.AppendField("NumPages", FieldType.FieldNumPages);
            section.HeadersFooters.FirstPageFooter.Paragraphs.Add(footerPara);
            #region Page Number Settings
            section.PageSetup.RestartPageNumbering = true;
            section.PageSetup.PageStartingNumber = 1;
            section.PageSetup.PageNumberStyle = PageNumberStyle.Arabic;
            #endregion Page Number Settings
        }
        #endregion InsertFirstPageHeaderFooter

        #region InsertPageHeaderFooter
        /// <summary>
        /// Inserts Header and Footer to all pages.
        /// </summary>
        private void InsertPageHeaderFooter(WordDocument doc, IWSection section1)
        {
            //Adds a new table to the header
            IWTable table = section1.HeadersFooters.Header.AddTable();

            RowFormat format = new();
            //Sets Single table border style.
            format.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.Single;

            //Inserts table with a row and two columns.
            table.ResetCells(1, 2, format, 265);

            //Inserts logo image to the table first cell.
            IWParagraph? headerPara = table[0, 0].AddParagraph() as WParagraph;
            string dataPath = @"SampleBrowser.Maui.Resources.DocIO.Northwindlogo.png";
            if (BaseConfig.IsIndividualSB)
                dataPath = @"SampleBrowser.Maui.DocIO.Resources.DocIO.Northwindlogo.png";
            Stream? imageStream = assembly.GetManifestResourceStream(dataPath);
            headerPara!.AppendPicture(imageStream);
            //Sets Image size.
            (headerPara.Items[0] as WPicture)!.Width = 232.5f;
            (headerPara.Items[0] as WPicture)!.Height = 54.75f;
            //Inserts text to the table second cell.
            headerPara = table[0, 1].AddParagraph() as WParagraph;
            IWTextRange txt = headerPara!.AppendText("Company Headquarters,\n2501 Aerial Center Parkway,\nSuite 110, Morrisville, NC 27560,\nTEL 1-888-936-8638.");
            txt.CharacterFormat.FontSize = 12;
            txt.CharacterFormat.CharacterSpacing = 1.7f;
            headerPara.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;

            //Adds a footer paragraph text to the document.
            WParagraph footerPara = new(doc);
            footerPara.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
            //Adds text.
            footerPara.AppendText("Copyright Northwind Inc. 2001 - 2021");
            //Adds page and Number of pages field to the document.
            footerPara.AppendText("\tPage ");
            footerPara.AppendField("Page", FieldType.FieldPage);
            footerPara.AppendText(" of ");
            footerPara.AppendField("NumPages", FieldType.FieldNumPages);
            section1.HeadersFooters.Footer.Paragraphs.Add(footerPara);

            #region Page Number Settings
            section1.PageSetup.RestartPageNumbering = true;
            section1.PageSetup.PageStartingNumber = 1;
            section1.PageSetup.PageNumberStyle = PageNumberStyle.Arabic;
            #endregion Page Number Settings
        }
        #endregion InsertPageHeaderFooter
        #endregion
    }
}
