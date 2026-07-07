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
using System;
using System.IO;
using System.Reflection;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using Color = Syncfusion.Drawing.Color;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class EditInk : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public EditInk()
        {
            InitializeComponent();
            assembly = typeof(EditInk).GetTypeInfo().Assembly;
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
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.EditInkInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.EditInkInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            // Access the first section of the document
            WSection section = document.Sections[0];
            // Access the first ink and customize its trace points.
            WInk firstInk = (WInk)section.Paragraphs[0].ChildEntities[0];
			// Move the ink vertically.
			firstInk.VerticalPosition = 25f;
			// Copy existing points into the new array.
			int oldTracePointsLength = firstInk.Traces[0].Points.Length;
			int newTracePointsLength = oldTracePointsLength + 3;
			PointF[] newTracePoints = new PointF[newTracePointsLength];
			PointF[] oldTracePoints = firstInk.Traces[0].Points;
			Array.Copy(oldTracePoints, newTracePoints, oldTracePointsLength);
			newTracePoints[newTracePoints.Length - 3] = new PointF(oldTracePoints[3].X, 0);
			newTracePoints[newTracePoints.Length - 2] = new PointF(oldTracePoints[0].X, 0);
			newTracePoints[newTracePoints.Length - 1] = new PointF(oldTracePoints[0].X, oldTracePoints[0].Y);
			// Update the trace points of the first ink with the new array.
			firstInk.Traces[0].Points = newTracePoints;

            // Access the second ink and customize its container width.
            WInk secondInk = (WInk)section.Paragraphs[1].ChildEntities[0];
            IOfficeInkTrace secondInkTrace = secondInk.Traces[0];
            // Set the ink size (thickness) to 1 point
            secondInkTrace.Brush.Size = new SizeF(1f, 1f);

            // Access the third ink and customize its container width.
            WInk thirdInk = (WInk)section.Paragraphs[2].ChildEntities[0];
            // Set the width of the ink container to 130 points.
            thirdInk.Width = 130f;

            // Access the fourth ink and customize its brush color.
            WParagraph paragraph = (WParagraph)section.Tables[0].Rows[0].Cells[0].ChildEntities[0] ;
            WInk fourthInk = (WInk)paragraph.ChildEntities[0];
            IOfficeInkTrace fourthInkTrace = fourthInk.Traces[0];
            // Set the color of the ink stroke to Yellow
            fourthInkTrace.Brush.Color = Color.Yellow;

            using MemoryStream ms = new();
            //Saves the word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EditInk.docx", "application/msword", ms);
        }

        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.EditInkInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.EditInkInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("EditInkInput.docx", "application/msword", ms);
        }
        #endregion
    }
}