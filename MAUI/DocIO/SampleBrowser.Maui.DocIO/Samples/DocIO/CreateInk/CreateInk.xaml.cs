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
using HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;
using Color = Syncfusion.Drawing.Color;

namespace SampleBrowser.Maui.DocIO.DocIO
{
    /// <summary>
    /// Integration logic for xaml.
    /// </summary>
    public partial class CreateInk : SampleView
    {
        #region Fields
        private readonly Assembly assembly;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public CreateInk()
        {
            InitializeComponent();
            assembly = typeof(CreateInk).GetTypeInfo().Assembly;
#if ANDROID || IOS
            stkLayout.HorizontalOptions = LayoutOptions.Center;
            btnViewTemplate.HorizontalOptions = LayoutOptions.Center;
            btnGenerateDocument.HorizontalOptions = LayoutOptions.Center;
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Create a Word document with Ink Element.
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.CreateInkInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.CreateInkInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            //Loads an existing Word document.
            using WordDocument document = new(fileStream, FormatType.Automatic);
            WParagraph paragraph = document.LastParagraph;
            // Append an ink object to the paragraph
            WInk inkObj = paragraph.AppendInk(80, 20);
            // Get the traces collection from the ink object (traces represent the drawing strokes)
            IOfficeInkTraces traces = inkObj.Traces;
            // Retrieve an array of points that define the path of the ink stroke
            PointF[] tracePoints = GetPoints();
            // Add a new trace (stroke) to the traces collection using the retrieved points
            IOfficeInkTrace trace = traces.Add(tracePoints);

            // Configure the appearance of the ink
            // Get the brush object associated with the trace to configure its appearance
            IOfficeInkBrush brush = trace.Brush;
            // Set the ink effect type to None (Pen effect applied)
            brush.InkEffect = OfficeInkEffectType.None;
            // Set the color of the ink stroke
            brush.Color = Color.Black;
            // Set the size (thickness) of the ink stroke to 1.5 units
            brush.Size = new SizeF(1.5f, 1.5f);


            #region Document SaveOption
            using MemoryStream ms = new();
            //Saves the Word document to the memory stream.
            document.Save(ms, FormatType.Docx);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("CreateInk.docx", "application/msword", ms);
            #endregion Document SaveOption
        }
        /// <summary>
        /// Opens the input template Word document.
        /// </summary>
        private void ButtonView_Click(object sender, EventArgs e)
        {
            //Gets the input Word document.
            string resourcePath = "SampleBrowser.Maui.Resources.DocIO.CreateInkInput.docx";
            if (BaseConfig.IsIndividualSB)
                resourcePath = "SampleBrowser.Maui.DocIO.Resources.DocIO.CreateInkInput.docx";
            using Stream? fileStream = assembly.GetManifestResourceStream(resourcePath);
            using MemoryStream ms = new();
            fileStream!.CopyTo(ms);
            ms.Position = 0;
            //Saves the memory stream as file.
            SaveService saveService = new();
            saveService.SaveAndView("CreateInkInput.docx", "application/msword", ms);
        }
        /// <summary>
        /// Gets the array of trace points defining the signature.
        /// </summary>
        /// <returns>Array of <see cref="PointF"/> representing the path coordinates.</returns>
        public PointF[] GetPoints()
        {
            PointF[] tracePoints = new PointF[95];

            tracePoints[0] = new PointF(15f, 35f);
            tracePoints[1] = new PointF(18f, 28f);
            tracePoints[2] = new PointF(22f, 22f);
            tracePoints[3] = new PointF(27f, 17f);
            tracePoints[4] = new PointF(32f, 14f);
            tracePoints[5] = new PointF(37f, 12f);
            tracePoints[6] = new PointF(42f, 12f);
            tracePoints[7] = new PointF(46f, 14f);
            tracePoints[8] = new PointF(49f, 18f);
            tracePoints[9] = new PointF(51f, 23f);
            tracePoints[10] = new PointF(52f, 28f);
            tracePoints[11] = new PointF(52f, 33f);
            tracePoints[12] = new PointF(51f, 38f);
            tracePoints[13] = new PointF(49f, 42f);
            tracePoints[14] = new PointF(46f, 45f);
            tracePoints[15] = new PointF(42f, 47f);
            tracePoints[16] = new PointF(38f, 48f);
            tracePoints[17] = new PointF(34f, 47f);
            tracePoints[18] = new PointF(31f, 45f);
            tracePoints[19] = new PointF(35f, 42f);
            tracePoints[20] = new PointF(40f, 39f);
            tracePoints[21] = new PointF(46f, 37f);
            tracePoints[22] = new PointF(52f, 36f);
            tracePoints[23] = new PointF(58f, 36f);
            tracePoints[24] = new PointF(63f, 37f);
            tracePoints[25] = new PointF(67f, 40f);
            tracePoints[26] = new PointF(69f, 44f);
            tracePoints[27] = new PointF(69f, 48f);
            tracePoints[28] = new PointF(67f, 51f);
            tracePoints[29] = new PointF(64f, 53f);
            tracePoints[30] = new PointF(60f, 54f);
            tracePoints[31] = new PointF(56f, 53f);
            tracePoints[32] = new PointF(54f, 51f);
            tracePoints[33] = new PointF(53f, 48f);
            tracePoints[34] = new PointF(54f, 45f);
            tracePoints[35] = new PointF(57f, 43f);
            tracePoints[36] = new PointF(61f, 42f);
            tracePoints[37] = new PointF(65f, 42f);
            tracePoints[38] = new PointF(69f, 43f);
            tracePoints[39] = new PointF(73f, 44f);
            tracePoints[40] = new PointF(77f, 43f);
            tracePoints[41] = new PointF(81f, 40f);
            tracePoints[42] = new PointF(84f, 44f);
            tracePoints[43] = new PointF(86f, 48f);
            tracePoints[44] = new PointF(88f, 52f);
            tracePoints[45] = new PointF(90f, 55f);
            tracePoints[46] = new PointF(92f, 52f);
            tracePoints[47] = new PointF(94f, 48f);
            tracePoints[48] = new PointF(96f, 44f);
            tracePoints[49] = new PointF(98f, 40f);
            tracePoints[50] = new PointF(100f, 37f);
            tracePoints[51] = new PointF(104f, 36f);
            tracePoints[52] = new PointF(107f, 38f);
            tracePoints[53] = new PointF(109f, 42f);
            tracePoints[54] = new PointF(110f, 46f);
            tracePoints[55] = new PointF(110f, 50f);
            tracePoints[56] = new PointF(109f, 53f);
            tracePoints[57] = new PointF(112f, 51f);
            tracePoints[58] = new PointF(116f, 48f);
            tracePoints[59] = new PointF(120f, 46f);
            tracePoints[60] = new PointF(125f, 45f);
            tracePoints[61] = new PointF(130f, 46f);
            tracePoints[62] = new PointF(134f, 48f);
            tracePoints[63] = new PointF(137f, 51f);
            tracePoints[64] = new PointF(138f, 54f);
            tracePoints[65] = new PointF(137f, 57f);
            tracePoints[66] = new PointF(134f, 58f);
            tracePoints[67] = new PointF(130f, 58f);
            tracePoints[68] = new PointF(126f, 56f);
            tracePoints[69] = new PointF(124f, 53f);
            tracePoints[70] = new PointF(123f, 49f);
            tracePoints[71] = new PointF(125f, 45f);
            tracePoints[72] = new PointF(127f, 40f);
            tracePoints[73] = new PointF(129f, 35f);
            tracePoints[74] = new PointF(131f, 30f);
            tracePoints[75] = new PointF(133f, 25f);
            tracePoints[76] = new PointF(135f, 20f);
            tracePoints[77] = new PointF(140f, 25f);
            tracePoints[78] = new PointF(148f, 32f);
            tracePoints[79] = new PointF(158f, 38f);
            tracePoints[80] = new PointF(170f, 43f);
            tracePoints[81] = new PointF(182f, 46f);
            tracePoints[82] = new PointF(190f, 47f);
            tracePoints[83] = new PointF(185f, 48f);
            tracePoints[84] = new PointF(175f, 50f);
            tracePoints[85] = new PointF(160f, 52f);
            tracePoints[86] = new PointF(140f, 54f);
            tracePoints[87] = new PointF(115f, 55f);
            tracePoints[88] = new PointF(85f, 56f);
            tracePoints[89] = new PointF(60f, 56f);
            tracePoints[90] = new PointF(40f, 55f);
            tracePoints[91] = new PointF(25f, 53f);
            tracePoints[92] = new PointF(15f, 50f);
            tracePoints[93] = new PointF(10f, 47f);
            tracePoints[94] = new PointF(8f, 44f);

            return tracePoints;
        }
        #endregion
    }
}
