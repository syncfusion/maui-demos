#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
/// <summary>
/// Provides the functionality to open and save PDF files. 
/// </summary>
public class SignatureFileService 
{
    
        /// <summary>
        /// Shows a file picker and lets the user choose the required PDF file. 
        /// </summary>
        /// <returns>The data of the PDF file that is chosen by the user.</returns>
        public async static Task<PdfFileData?> OpenFile(string fileExtension)
        {
            FilePickerFileType pdfFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>{
                        { DevicePlatform.iOS, new[] { $"com.adobe.{fileExtension}" } },
                        { DevicePlatform.Android, new[] { $"application/{fileExtension}" } },
                        { DevicePlatform.WinUI, new[] { fileExtension } },
                        { DevicePlatform.MacCatalyst, new[] { fileExtension } },
                    });
            PickOptions options = new()
            {
                PickerTitle = "Please select a PDF file",
            };
#if !ANDROID
            options.FileTypes = pdfFileType;
#else
            if (fileExtension == "pdf")
                options.FileTypes = pdfFileType;
#endif
            return await PickFile(options, fileExtension);
        }
        static async Task<PdfFileData?> PickFile(PickOptions options, string fileExtension)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName != null)
                    {
                        if (result.FileName.EndsWith($".{fileExtension}", StringComparison.OrdinalIgnoreCase))
                        {
                            return new PdfFileData(result.FileName, await result.OpenReadAsync());
                        }
                        else
                            Application.Current?.MainPage?.DisplayAlert("Error", $"Pick a file of type {fileExtension}", "OK");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string message;
                if (ex != null && string.IsNullOrEmpty(ex.Message) == false)
                    message = ex.Message;
                else
                    message = "File open failed.";
                Application.Current?.MainPage?.DisplayAlert("Error", message, "OK");
            }
            return null;
        }
        /// <summary>
        /// Saves the PDF stream with given file name using platform specific file saving APIs. 
        /// </summary>
        /// <param name="fileName">The file name with which the PDF needs to be saved.</param>
        /// <param name="fileStream">The stream of the PDF to be saved.</param>
        /// <returns></returns>
        public async static Task<string?> SaveAsAsync(string fileName, Stream fileStream)
        {
            return await FileService.PlatformSaveAsAsync(fileName, fileStream);
        }

        /// <summary>
        /// Writes the given stream to the specified file path.
        /// </summary>
        /// <param name="stream">The stream of the PDF file to be written.</param>
        /// <param name="filePath">The file path to which the PDF file needs to be written.</param>
        /// <returns></returns>
        static async Task WriteStream(Stream stream, string? filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                await using var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                fileStream.SetLength(0);
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                await stream.CopyToAsync(fileStream).ConfigureAwait(false);
            }
        }
}