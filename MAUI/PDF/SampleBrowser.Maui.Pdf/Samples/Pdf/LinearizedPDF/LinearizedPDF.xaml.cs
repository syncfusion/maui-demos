using SampleBrowser.Maui.Base;
using Syncfusion.Pdf;
using System.Reflection;
using Syncfusion.Pdf.Graphics;
using SizeF = Syncfusion.Drawing.SizeF;
using PointF = Syncfusion.Drawing.PointF;
using Syncfusion.Pdf.Parsing;

#if PDFSB
using SampleBrowser.Maui.Pdf.Services;
#else
using SampleBrowser.Maui.Services;
#endif

namespace SampleBrowser.Maui.Pdf.Pdf
{
    public partial class LinearizedPDF : SampleView
    {
        private Stream? customPdfStream;
        private string? customFileName;

        #region Constructor
        /// <summary>
        /// Initializes component.
        /// </summary>
        public LinearizedPDF()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Browse and select a PDF file from the device.
        /// </summary>
        private async void OnBrowseButtonClicked(object? sender, EventArgs e)
        {
            try
            {
                var customPdfFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                        { DevicePlatform.Android, new[] { "application/pdf" } },
                        { DevicePlatform.WinUI, new[] { ".pdf" } },
                        { DevicePlatform.MacCatalyst, new[] { "com.adobe.pdf" } },
                        { DevicePlatform.Tizen, new[] { "*/*" } },
                    });

                PickOptions options = new()
                {
                    PickerTitle = "Please select a PDF file",
                    FileTypes = customPdfFileType,
                };

                FileResult? result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    fileNameEntry.Text = result.FileName;
                    customFileName = result.FileName;
                    customPdfStream?.Dispose();
                    customPdfStream = await result.OpenReadAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Convert to Linearized PDF document.
        /// </summary>
        private async void OnButtonClicked(object? sender, EventArgs e)
        {
            Stream? documentStream = null;

            if (customPdfStream != null)
            {
                // FILE SIZE LIMIT (10 MB)
                const long MAX_FILE_SIZE = 10 * 1024 * 1024;
                if (customPdfStream.Length > MAX_FILE_SIZE)
                {
                    await App.Current!.Windows[0].Page!.DisplayAlertAsync(
                        "File too large",
                        string.Format("The selected PDF file exceeds the maximum allowed size of 10 MB. File size: {0:F2} MB.", customPdfStream.Length / (1024.0 * 1024.0)),
                        "OK");
                    return;
                }

                MemoryStream ms = new();
                customPdfStream.Position = 0;
                customPdfStream.CopyTo(ms);
                ms.Position = 0;
                documentStream = ms;
            }
            else
            {
                Assembly assembly = typeof(LinearizedPDF).GetTypeInfo().Assembly;

                string basePath = "SampleBrowser.Maui.Resources.Pdf.";

                if (BaseConfig.IsIndividualSB)
                    basePath = "SampleBrowser.Maui.Pdf.Resources.Pdf.";

                //Load PDF file to stream
                documentStream = assembly.GetManifestResourceStream(basePath + "jQuery_Succinctly.pdf");
            }

            if (documentStream == null)
                return;

            // Load the PDF document from stream.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(documentStream);

            // Apply linearization to the loaded document
            loadedDocument.FileStructure.IsLinearized = true;

            //Creating the stream object.
            using MemoryStream stream = new();

            //Saves the PDF to the memory stream.
            loadedDocument.Save(stream);

            //Close the PDF document
            loadedDocument.Close(true);

            stream.Position = 0;

            //Save the output stream as a file using file picker.
            SaveService saveService = new();

            saveService.SaveAndView("LinearizedPDF.pdf", "application/pdf", stream);
        }
        #endregion
    }
}