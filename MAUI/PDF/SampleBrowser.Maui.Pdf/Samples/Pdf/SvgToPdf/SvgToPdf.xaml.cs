using SampleBrowser.Maui.Base;
using Syncfusion.Pdf;
using System.Reflection;
using Syncfusion.Pdf.Graphics;
using SizeF = Syncfusion.Drawing.SizeF;
using PointF = Syncfusion.Drawing.PointF;
#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class SVGToPDF : SampleView
    {
        private Stream? customSvgStream;
        private string? customFileName;

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public SVGToPDF()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Browse and select an SVG file from the device.
        /// </summary>
        private async void OnBrowseButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var customSvgFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "public.svg-image" } },
                        { DevicePlatform.Android, new[] { "image/svg+xml" } },
                        { DevicePlatform.WinUI, new[] { ".svg" } },
                        { DevicePlatform.MacCatalyst, new[] { "public.svg-image" } },
                        { DevicePlatform.Tizen, new[] { "*/*" } },
                    });

                PickOptions options = new()
                {
                    PickerTitle = "Please select an SVG file",
                    FileTypes = customSvgFileType,
                };

                FileResult? result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    fileNameEntry.Text = result.FileName;
                    customFileName = result.FileName;
                    customSvgStream?.Dispose();
                    customSvgStream = await result.OpenReadAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Convert SVG to PDF document.
        /// </summary>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            Stream? documentStream = null;

            if (customSvgStream != null)
            {
                MemoryStream ms = new();
                customSvgStream.Position = 0;
                customSvgStream.CopyTo(ms);
                ms.Position = 0;
                documentStream = ms;
            }
            else
            {
                Assembly assembly = typeof(SVGToPDF).GetTypeInfo().Assembly;

                string basePath = "SampleBrowser.Maui.Resources.Pdf.";

                if (BaseConfig.IsIndividualSB)
                    basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";

                //Load SVG file to stream
                documentStream = assembly.GetManifestResourceStream(basePath + "SVGToPDF.svg");
            }

            if (documentStream == null)
                return;

            //Create SVG to PDF converter
            SvgConverter converter = new SvgConverter();

            //Convert SVG document into PDF document
            PdfTemplate temp = converter.Convert(documentStream);
            PdfDocument document = new PdfDocument();
            document.PageSettings.Margins.All = 0;
            document.PageSettings.Size = new SizeF(temp.Width, temp.Height);

            PdfPage page = document.Pages.Add();
            page.Graphics.DrawPdfTemplate(temp, new PointF(0, 0), new SizeF(temp.Width, temp.Height));

            //Creating the stream object.
            using MemoryStream stream = new();

            //Saves the PDF to the memory stream.
            document.Save(stream);

            //Close the PDF document
            document.Close(true);

            stream.Position = 0;

            //Save the output stream as a file using file picker.
            SaveService saveService = new();

            saveService.SaveAndView("SVGToPDF.pdf", "application/pdf", stream);
        }
        #endregion
    }
}