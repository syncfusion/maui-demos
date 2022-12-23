#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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
using Syncfusion.Drawing;
using System;
using System.IO;
using System.Reflection;
using SizeF = Syncfusion.Drawing.SizeF;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class GettingStarted : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();
            assembly = typeof(GettingStarted).GetTypeInfo().Assembly;
#if ANDROID || IOS
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates a simple Word document with text, image and table.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Creates a new Word document.
            using WordDocument document = new();
            //Adds a new section to the document.
            WSection? section = document.AddSection() as WSection;
            //Sets margin of the section.
            section!.PageSetup.Margins.All = 72;
            //Sets page size of the section.
            section.PageSetup.PageSize = new SizeF(612, 792);

            //Creates normal paragraph styles.
            WParagraphStyle? style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style!.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;
            //Creates heading paragraph style.
            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style!.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 12;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

            //Creates paragraph in the Word document.
            CreateParagraph(section);
            //Creates product overview table in the Word document.
            CreateProductOverviewTable(section);

            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Getting Started.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Creates paragraph in the Word document.
        /// </summary>
        /// <param name="section">The section to add paragraphs.</param>
        private void CreateParagraph(WSection section)
        {
            #region Inserting Header
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
            //Gets the image.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.HeaderImage.png";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.HeaderImage.png";
            Stream? imageStream = assembly.GetManifestResourceStream(resourcePath);
            //Appends image to the paragraph.
            WPicture? picture = paragraph.AppendPicture(imageStream) as WPicture;
            picture!.WidthScale = 173f;
            picture.HeightScale = 149f;
            #endregion

            #region Inserting Footer
            paragraph = section.HeadersFooters.Footer.AddParagraph();
            paragraph.ParagraphFormat.Tabs.AddTab(523f, TabJustification.Right, TabLeader.NoLeader);
            //Adds page and Number of pages field to the document.
            paragraph.AppendText("\tPage ");
            paragraph.AppendField("Page", FieldType.FieldPage);
            paragraph.AppendText(" of ");
            paragraph.AppendField("NumPages", FieldType.FieldNumPages);
            #endregion

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            WTextRange? textRange = paragraph.AppendText("Adventure Works Cycles") as WTextRange;
            textRange!.CharacterFormat.FontSize = 18f;
            textRange.CharacterFormat.FontName = "Calibri";

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Adventure Works Cycles, the fictitious company on which the Adventure Works sample databases are based, is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, European and Asian commercial markets. While its base operation is located in Bothell, Washington with 290 employees, several regional sales teams are located throughout their market base.") as WTextRange;
            textRange!.CharacterFormat.FontSize = 12f;

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("In 2000, Adventure Works Cycles bought a small manufacturing plant, Importadores Neptuno, located in Mexico. Importadores Neptuno manufactures several critical subcomponents for the Adventure Works Cycles product line. These subcomponents are shipped to the Bothell location for final product assembly. In 2001, Importadores Neptuno, became the sole manufacturer and distributor of the touring bicycle product group.") as WTextRange;
            textRange!.CharacterFormat.FontSize = 12f;

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Product Overview") as WTextRange;
            textRange!.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
        }

        /// <summary>
        /// Creates product overview table in the Word document.
        /// </summary>
        /// <param name="section">The section to add table.</param>
        private void CreateProductOverviewTable(WSection section)
        {
            //Appends table.
            IWTable table = section.AddTable();
            table.ResetCells(4, 2);
            table.TableFormat.Borders.BorderType = Syncfusion.DocIO.DLS.BorderStyle.None;
            table.TableFormat.IsAutoResized = true;

            //Appends paragraph.
            IWParagraph paragraph = table[0, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.Mountain200.jpg";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.Mountain200.jpg";
            //Appends picture to the paragraph.
            Stream? imageStream = assembly.GetManifestResourceStream(resourcePath);
            WPicture? picture = paragraph.AppendPicture(imageStream) as WPicture;
            picture!.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            picture.VerticalOrigin = VerticalOrigin.Paragraph;
            picture.VerticalPosition = 4.5f;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = -2.15f;
            picture.WidthScale = 79;
            picture.HeightScale = 79;

            //Appends paragraph.
            paragraph = table[0, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Mountain-200");

            //Appends paragraph.
            paragraph = table[0, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            //Adds textrange.
            AddTextRange(paragraph, "Product No: BK-M68B-38\r");
            AddTextRange(paragraph, "Size: 38\r");
            AddTextRange(paragraph, "Weight: 25\r");
            AddTextRange(paragraph, "Price: $2,294.99\r");

            //Appends paragraph.
            paragraph = table[0, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = table[1, 0].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Mountain-300 ");

            //Appends paragraph.
            paragraph = table[1, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            //Adds textrange.
            AddTextRange(paragraph, "Product No: BK-M47B-38\r");
            AddTextRange(paragraph, "Size: 35\r");
            AddTextRange(paragraph, "Weight: 22\r");
            AddTextRange(paragraph, "Price: $1,079.99\r");

            //Appends paragraph.
            paragraph = table[1, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = table[1, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.LineSpacing = 12f;
            resourcePath = "SampleBrowser.Maui.Resources.DocIO.Mountain300.jpg";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.Mountain300.jpg";
            //Appends picture to the paragraph.
            imageStream = assembly.GetManifestResourceStream(resourcePath);
            picture = paragraph.AppendPicture(imageStream) as WPicture;
            picture!.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            picture.VerticalOrigin = VerticalOrigin.Paragraph;
            picture.VerticalPosition = 8.2f;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = -14.95f;
            picture.WidthScale = 75;
            picture.HeightScale = 75;

            //Appends paragraph.
            paragraph = table[2, 0].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.LineSpacing = 12f;
            resourcePath = "SampleBrowser.Maui.Resources.DocIO.Road550W.jpg";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.Road550W.jpg";
            //Appends picture to the paragraph.
            imageStream = assembly.GetManifestResourceStream(resourcePath);
            picture = paragraph.AppendPicture(imageStream) as WPicture;
            picture!.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            picture.VerticalOrigin = VerticalOrigin.Paragraph;
            picture.VerticalPosition = 3.75f;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = -5f;
            picture.WidthScale = 92;
            picture.HeightScale = 92;

            //Appends paragraph.
            paragraph = table[2, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Road-150 ");
            //Appends paragraph.
            paragraph = table[2, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            //Adds textrange.
            AddTextRange(paragraph, "Product No: BK-R93R-44\r");
            AddTextRange(paragraph, "Size: 44\r");
            AddTextRange(paragraph, "Weight: 14\r");
            AddTextRange(paragraph, "Price: $3,578.27\r");

            //Appends paragraph.
            paragraph = table[2, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = table[3, 0].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Mountain-100");

            //Appends paragraph.
            paragraph = table[3, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            //Adds textrange.
            AddTextRange(paragraph, "Product No: BK-M47B-38\r");
            AddTextRange(paragraph, "Size: 42\r");
            AddTextRange(paragraph, "Weight: 20\r");
            AddTextRange(paragraph, "Price: $1,079.99\r");

            //Appends paragraph.
            paragraph = table[3, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = table[3, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.LineSpacing = 12f;
            resourcePath = "SampleBrowser.Maui.Resources.DocIO.Mountain300.jpg";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.Mountain300.jpg";
            //Appends picture to the paragraph.
            imageStream = assembly.GetManifestResourceStream(resourcePath);
            picture = paragraph.AppendPicture(imageStream) as WPicture;
            picture!.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            picture.VerticalOrigin = VerticalOrigin.Paragraph;
            picture.VerticalPosition = 8.2f;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = -14.95f;
            picture.WidthScale = 75;
            picture.HeightScale = 75;

            //Appends paragraph.
            section.AddParagraph();
        }

        /// <summary>
        /// Adds textrange in the paragraph.
        /// </summary>
        private static void AddTextRange(IWParagraph paragraph, string text)
        {
            WTextRange? textRange = paragraph.AppendText(text) as WTextRange;
            textRange!.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
        }
        #endregion
    }
}