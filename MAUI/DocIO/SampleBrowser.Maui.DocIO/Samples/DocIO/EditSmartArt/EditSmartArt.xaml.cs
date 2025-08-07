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
using Syncfusion.Office;
using System.Reflection;
using Color = Syncfusion.Drawing.Color;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class EditSmartArt : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EditSmartArt()
        {
            InitializeComponent();
            assembly = typeof(EditSmartArt).GetTypeInfo().Assembly;
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
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.EditSmartArtInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.EditSmartArtInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Docx);
            //Gets the last paragraph in the document.
            WParagraph paragraph = document.LastParagraph;
            //Retrieves the SmartArt object from the paragraph.
            WSmartArt smartArt = (WSmartArt)paragraph.ChildEntities[0];
            //Sets the background fill type of the SmartArt to solid.
            smartArt.Background.FillType = OfficeShapeFillType.Solid;
            //Sets the background color of the SmartArt.
            smartArt.Background.SolidFill.Color = Color.FromArgb(255, 242, 169, 132);
            //Gets the first node of the SmartArt.
            IOfficeSmartArtNode node = smartArt.Nodes[0];
            //Modifies the text content of the first node.
            node.TextBody.Text = "Goals";
            //Retrieves the first shape of the node.
            IOfficeSmartArtShape shape = node.Shapes[0];
            //Sets the fill color of the shape.
            shape.Fill.SolidFill.Color = Color.FromArgb(255, 160, 43, 147);
            //Sets the line format color of the shape.
            shape.LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 160, 43, 147);
            //Gets the first child node of the current node.
            IOfficeSmartArtNode childNode = node.ChildNodes[0];
            //Modifies the text content of the child node.
            childNode.TextBody.Text = "Set clear goals to the team.";
            //Sets the line format color of the first shape in the child node.
            childNode.Shapes[0].LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 160, 43, 147);

            //Retrieves the second node in the SmartArt and updates its text content.
            node = smartArt.Nodes[1];
            node.TextBody.Text = "Progress";

            //Retrieves the third node in the SmartArt and updates its text content.
            node = smartArt.Nodes[2];
            node.TextBody.Text = "Result";
            //Retrieves the first shape of the third node.
            shape = node.Shapes[0];
            //Sets the fill color of the shape.
            shape.Fill.SolidFill.Color = Color.FromArgb(255, 78, 167, 46);
            //Sets the line format color of the shape.
            shape.LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 78, 167, 46);
            //Sets the line format color of the first shape in the child node.
            node.ChildNodes[0].Shapes[0].LineFormat.Fill.SolidFill.Color = Color.FromArgb(255, 78, 167, 46);

            using MemoryStream ms = new();
            //Saves the word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EditSmartArt.docx", "application/msword", ms);
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.EditSmartArtInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.EditSmartArtInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EditSmartArtInput.docx", "application/msword", ms);
        }
        #endregion
    }
}