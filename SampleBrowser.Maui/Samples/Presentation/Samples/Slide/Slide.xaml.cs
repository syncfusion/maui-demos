#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using SampleBrowser.Maui.Services;
using Syncfusion.Presentation;
using System;
using System.IO;
using System.Reflection;

namespace SampleBrowser.Maui.Presentation
{
    public partial class Slide : SampleView
    {
        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public Slide()
        {
            InitializeComponent();
#if ANDROID || IOS
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Creates slides with simple text, table and image in a PowerPoint Presentation.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            //Gets the input PowerPoint Presentation file.
            Assembly assembly = typeof(Slide).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.Slides.pptx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Opens an existing PowerPoint Presentation file.
            using IPresentation presentation = Syncfusion.Presentation.Presentation.Open(fileStream);
            //Creates slides in PowerPoint Presentation file.
            CreateFirstSlide(presentation);
            CreateSecondSlide(presentation);
            CreateThirdSlide(presentation);
            CreateFourthSlide(presentation);

            //Saves the presentation to the memory stream.
            using MemoryStream stream = new();
            presentation.Save(stream);
            stream.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("Slide.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", stream);
        }
        #endregion

        #region Helper methods
        #region Slide1
        /// <summary>
        /// Creates first slide in PowerPoint Presentation file.
        /// </summary>
        private static void CreateFirstSlide(IPresentation presentation)
        {
            ISlide slide1 = presentation.Slides[0];
            IShape? shape1 = slide1.Shapes[0] as IShape;
            shape1!.Left = 1.5 * 72;
            shape1.Top = 1.94 * 72;
            shape1.Width = 10.32 * 72;
            shape1.Height = 2 * 72;

            //Adds text in a shape.
            ITextBody textFrame1 = shape1.TextBody;
            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            IParagraph paragraph1 = paragraphs1.Add();
            ITextPart textPart1 = paragraph1.AddTextPart();
            paragraphs1[0].IndentLevelNumber = 0;
            textPart1.Text = "Essential Presentation";
            textPart1.Font.FontName = "HelveticaNeue LT 65 Medium";
            textPart1.Font.CapsType = TextCapsType.All;
            textPart1.Font.FontSize = 48;
            textPart1.Font.Bold = true;
            slide1.Shapes.RemoveAt(1);
        }
        #endregion

        #region Slide2
        /// <summary>
        /// Creates second slide in PowerPoint Presentation file.
        /// </summary>
        private static void CreateSecondSlide(IPresentation presentation)
        {
            ISlide slide2 = presentation.Slides.Add(SlideLayoutType.SectionHeader);
            IShape? shape1 = slide2!.Shapes[0] as IShape;
            shape1!.Left = 0.77 * 72;
            shape1.Top = 0.32 * 72;
            shape1.Width = 7.96 * 72;
            shape1.Height = 0.99 * 72;

            //Adds content for first shape.
            ITextBody textFrame1 = shape1.TextBody;

            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            IParagraph paragraph1 = paragraphs1.Add();
            ITextPart textpart1 = paragraph1.AddTextPart();
            paragraphs1[0].HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1.Text = "Slide with simple text";
            textpart1.Font.FontName = "Helvetica CE 35 Thin";
            textpart1.Font.FontSize = 40;

            IShape? shape2 = slide2.Shapes[1] as IShape;
            shape2!.Left = 1.21 * 72;
            shape2.Top = 1.66 * 72;
            shape2.Width = 10.08 * 72;
            shape2.Height = 4.93 * 72;

            //Adds content for second shape.
            ITextBody textFrame2 = shape2.TextBody;
            string text = "Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Vestibulum duis integer diam mi libero felis, sollicitudin id dictum etiam blandit lacus, ac condimentum magna dictumst interdum et,";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "nam commodo mi habitasse enim fringilla nunc, amet aliquam sapien per tortor luctus. Conubia voluptates at nunc, congue lectus, malesuada nulla.";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Rutrum quo morbi, feugiat sed mi turpis, ac cursus integer ornare dolor. Purus dui in et tincidunt, sed eros pede adipiscing tellus, est suscipit nulla,";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.";
            AddTextInSecondSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "arcu nec fringilla vel aliquam, mollis lorem rerum hac vestibulum ante nullam. Volutpat a lectus, lorem pulvinar quis. Lobortis vehicula in imperdiet orci urna.";
            AddTextInSecondSlide(textFrame2, text);
        }

        /// <summary>
        /// Adds a new paragraph and text.
        /// </summary>
        private static void AddTextInSecondSlide(ITextBody textFrame, string text)
        {
            //Adds a new paragraph and text.
            IParagraphs paragraphs = textFrame.Paragraphs;
            IParagraph paragraph = paragraphs.Add();
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Left;
            ITextPart textpart = paragraph.AddTextPart();
            textpart.Text = text;
            textpart.Font.FontName = "Calibri (Body)";
            textpart.Font.FontSize = 15;
            textpart.Font.Color = ColorObject.Black;
        }
        #endregion

        #region Slide3
        /// <summary>
        /// Creates third slide in PowerPoint Presentation file.
        /// </summary>
        private static void CreateThirdSlide(IPresentation presentation)
        {
            ISlide slide2 = presentation.Slides.Add(SlideLayoutType.TwoContent);
            IShape? shape1 = slide2.Shapes[0] as IShape;
            shape1!.Left = 0.36 * 72;
            shape1.Top = 0.51 * 72;
            shape1.Width = 11.32 * 72;
            shape1.Height = 1.06 * 72;

            //Adds textframe in shape.
            ITextBody textFrame1 = shape1.TextBody;
            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            paragraphs1.Add();
            IParagraph paragraph1 = paragraphs1[0];
            ITextPart textpart1 = paragraph1.AddTextPart();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Left;

            //Assigns value to textpart.
            textpart1.Text = "Slide with Image";
            textpart1.Font.FontName = "Helvetica CE 35 Thin";

            //Adds a shape in slide.
            IShape ?shape2 = slide2.Shapes[1] as IShape;
            shape2!.Left = 8.03 * 72;
            shape2.Top = 1.96 * 72;
            shape2.Width = 4.39 * 72;
            shape2.Height = 4.53 * 72;
            ITextBody textFrame2 = shape2.TextBody;

            string text = "Lorem ipsum dolor sit amet, lacus amet amet ultricies. Quisque mi venenatis morbi libero, orci dis, mi ut et class porta, massa ligula magna enim, aliquam orci vestibulum tempus.";
            AddTextInThirdSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Turpis facilisis vitae consequat, cum a a, turpis dui consequat massa in dolor per, felis non amet.";
            AddTextInThirdSlide(textFrame2, text);

            //Adds a new paragraph and text.
            text = "Auctor eleifend in omnis elit vestibulum, donec non elementum tellus est mauris, id aliquam, at lacus, arcu pretium proin lacus dolor et. Eu tortor, vel ultrices amet dignissim mauris vehicula.";
            AddTextInThirdSlide(textFrame2, text);

            //Removes the shape.
            slide2.Shapes.RemoveAt(2);

            //Adds a picture to the slide.
            Assembly assembly = typeof(Slide).GetTypeInfo().Assembly;
            string resourcePath = "SampleBrowser.Maui.Resources.Presentation.tablet.jpg";
            Stream? imageStream = assembly.GetManifestResourceStream(resourcePath);
            slide2.Shapes.AddPicture(imageStream, 0.81 * 72, 1.96 * 72, 6.63 * 72, 4.43 * 72);
            imageStream!.Dispose();
        }

        /// <summary>
        /// Adds a new paragraph and text.
        /// </summary>
        private static void AddTextInThirdSlide(ITextBody textFrame, string text)
        {
            //Adds a new paragraph and text.
            IParagraphs paragraphs = textFrame.Paragraphs;
            IParagraph paragraph = paragraphs.Add();
            ITextPart textpart = paragraph.AddTextPart();
            textpart.Text = text;
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart.Font.FontName = "Helvetica CE 35 Thin";
            textpart.Font.FontSize = 16;
        }
        #endregion

        #region Slide4
        /// <summary>
        /// Creates fourth slide in PowerPoint Presentation file.
        /// </summary>
        private static void CreateFourthSlide(IPresentation presentation)
        {
            ISlide slide4 = presentation.Slides.Add(SlideLayoutType.TwoContent);
            IShape? shape1 = slide4.Shapes[0] as IShape;
            shape1!.Left = 0.51 * 72;
            shape1.Top = 0.34 * 72;
            shape1.Width = 11.32 * 72;
            shape1.Height = 1.06 * 72;

            ITextBody textFrame1 = shape1.TextBody;

            //Adds a new paragraph and text.
            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            paragraphs1.Add();
            IParagraph paragraph1 = paragraphs1[0];
            ITextPart textpart1 = paragraph1.AddTextPart();
            paragraph1.HorizontalAlignment = HorizontalAlignmentType.Left;

            //Assigns value to textpart.
            textpart1.Text = "Slide with Table";
            textpart1.Font.FontName = "Helvetica CE 35 Thin";

            //Removes the shape.
            IShape? shape2 = slide4.Shapes[1] as IShape;
            slide4.Shapes.Remove(shape2);

            //Adds a new table to the shape collection with the specified number of rows and columns.
            ITable table = (ITable)slide4.Shapes.AddTable(6, 6, 0.81 * 72, 2.14 * 72, 11.43 * 72, 3.8 * 72);
            table.Rows[0].Height = 0.85 * 72;
            table.Rows[1].Height = 0.42 * 72;
            table.Rows[2].Height = 0.85 * 72;
            table.Rows[3].Height = 0.85 * 72;
            table.Rows[4].Height = 0.85 * 72;
            table.Rows[5].Height = 0.85 * 72;
            table.HasBandedRows = true;
            table.HasHeaderRow = true;
            table.HasBandedColumns = false;
            table.BuiltInStyle = BuiltInTableStyle.MediumStyle2Accent1;

            #region First Row
            //Adds text to the first cell in first row.
            ICell cell1 = table.Rows[0].Cells[0];
            string text = "ID";
            AddParaInCell(cell1, text);

            //Adds text to the second cell in first row.
            ICell cell2 = table.Rows[0].Cells[1];
            text = "Company Name";
            AddParaInCell(cell2, text);

            //Adds text to the third cell in first row.
            ICell cell3 = table.Rows[0].Cells[2];
            text = "Contact Name";
            AddParaInCell(cell3, text);

            //Adds text to the fourth cell in first row.
            ICell cell4 = table.Rows[0].Cells[3];
            text = "Address";
            AddParaInCell(cell4, text);

            //Adds text to the fifth cell in first row.
            ICell cell5 = table.Rows[0].Cells[4];
            text = "City";
            AddParaInCell(cell5, text);

            //Adds text to the sixth cell in first row.
            ICell cell6 = table.Rows[0].Cells[5];
            text = "Country";
            AddParaInCell(cell6, text);
            #endregion

            #region Second Row
            //Adds text to the first cell in second row.
            cell1 = table.Rows[1].Cells[0];
            text = "1";
            AddParaInCell(cell1, text);

            //Adds text to the second cell in second row.
            cell1 = table.Rows[1].Cells[1];
            text = "New Orleans Cajun Delights";
            AddParaInCell(cell1, text);

            //Adds text to the third cell in second row.
            cell1 = table.Rows[1].Cells[2];
            text = "Shelley Burke";
            AddParaInCell(cell1, text);

            //Adds text to the fourth cell in second row.
            cell1 = table.Rows[1].Cells[3];
            text = "P.O. Box 78934";
            AddParaInCell(cell1, text);

            //Adds text to the fifth cell in second row.
            cell1 = table.Rows[1].Cells[4];
            text = "New Orleans";
            AddParaInCell(cell1, text);

            //Adds text to the sixth cell in second row.
            cell1 = table.Rows[1].Cells[5];
            text = "USA";
            AddParaInCell(cell1, text);
            #endregion

            #region Third Row
            //Adds text to the first cell in third row.
            cell1 = table.Rows[2].Cells[0];
            text = "2";
            AddParaInCell(cell1, text);

            //Adds text to the second cell in third row.
            cell1 = table.Rows[2].Cells[1];
            text = "Cooperativa de Quesos 'Las Cabras";
            AddParaInCell(cell1, text);

            //Adds text to the third cell in third row.
            cell1 = table.Rows[2].Cells[2];
            text = "Antonio del Valle Saavedra";
            AddParaInCell(cell1, text);

            //Adds text to the fourth cell in third row.
            cell1 = table.Rows[2].Cells[3];
            text = "Calle del Rosal 4";
            AddParaInCell(cell1, text);

            //Adds text to the fifth cell in third row.
            cell1 = table.Rows[2].Cells[4];
            text = "Oviedo";
            AddParaInCell(cell1, text);

            //Adds text to the sixth cell in third row.
            cell1 = table.Rows[2].Cells[5];
            text = "Spain";
            AddParaInCell(cell1, text);
            #endregion

            #region Fourth Row
            //Adds text to the first cell in fourth row.
            cell1 = table.Rows[3].Cells[0];
            text = "3";
            AddParaInCell(cell1, text);

            //Adds text to the second cell in fourth row.
            cell1 = table.Rows[3].Cells[1];
            text = "Mayumi";
            AddParaInCell(cell1, text);

            //Adds text to the third cell in fourth row.
            cell1 = table.Rows[3].Cells[2];
            text = "Mayumi Ohno";
            AddParaInCell(cell1, text);

            //Adds text to the fourth cell in fourth row.
            cell1 = table.Rows[3].Cells[3];
            text = "92 Setsuko Chuo-ku";
            AddParaInCell(cell1, text);

            //Adds text to the fifth cell in fourth row.
            cell1 = table.Rows[3].Cells[4];
            text = "Osaka";
            AddParaInCell(cell1, text);

            //Adds text to the sixth cell in fourth row.
            cell1 = table.Rows[3].Cells[5];
            text = "Japan";
            AddParaInCell(cell1, text);
            #endregion

            #region Fifth Row
            //Adds text to the first cell in fifth row.
            cell1 = table.Rows[4].Cells[0];
            text = "4";
            AddParaInCell(cell1, text);

            //Adds text to the second cell in fifth row.
            cell1 = table.Rows[4].Cells[1];
            text = "Pavlova, Ltd.";
            AddParaInCell(cell1, text);

            //Adds text to the third cell in fifth row.
            cell1 = table.Rows[4].Cells[2];
            text = "Ian Devling";
            AddParaInCell(cell1, text);

            //Adds text to the fourth cell in fifth row.
            cell1 = table.Rows[4].Cells[3];
            text = "74 Rose St. Moonie Ponds";
            AddParaInCell(cell1, text);

            //Adds text to the fifth cell in fifth row.
            cell1 = table.Rows[4].Cells[4];
            text = "Melbourne";
            AddParaInCell(cell1, text);

            //Adds text to the sixth cell in fifth row.
            cell1 = table.Rows[4].Cells[5];
            text = "Australia";
            AddParaInCell(cell1, text);
            #endregion

            #region Sixth Region
            //Adds text to the first cell in sixth row.
            cell1 = table.Rows[5].Cells[0];
            text = "5";
            AddParaInCell(cell1, text);

            //Adds text to the second cell in sixth row.
            cell1 = table.Rows[5].Cells[1];
            text = "Specialty Biscuits, Ltd.";
            AddParaInCell(cell1, text);

            //Adds text to the third cell in sixth row.
            cell1 = table.Rows[5].Cells[2];
            text = "Peter Wilson";
            AddParaInCell(cell1, text);

            //Adds text to the fourth cell in sixth row.
            cell1 = table.Rows[5].Cells[3];
            text = "29 King's Way";
            AddParaInCell(cell1, text);

            //Adds text to the fifth cell in sixth row.
            cell1 = table.Rows[5].Cells[4];
            text = "Manchester";
            AddParaInCell(cell1, text);

            //Adds text to the sixth cell in sixth row.
            cell1 = table.Rows[5].Cells[5];
            text = "UK";
            AddParaInCell(cell1, text);
            #endregion

            //Removes the shape.
            slide4.Shapes.RemoveAt(1);
        }
        /// <summary>
        /// Adds text to the cell.
        /// </summary>
        private static void AddParaInCell(ICell cell, string text)
        {
            ITextBody textFrame = cell.TextBody;
            IParagraph paragraph = textFrame.Paragraphs.Add();
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;
            ITextPart textPart = paragraph.AddTextPart();
            textPart.Text = text;
        }
        #endregion
        #endregion
    }
}
