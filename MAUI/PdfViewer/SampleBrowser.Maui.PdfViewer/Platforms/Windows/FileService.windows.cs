#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Diagnostics;
using Windows.Storage.Pickers;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public partial class FileService
    {
        static List<string> fileExtensions = new() { "." };
        internal static async Task<string> PlatformSaveAsAsync(string fileName, Stream stream)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, Process.GetCurrentProcess().MainWindowHandle);

            var extension = Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(extension))
            {
                savePicker.FileTypeChoices.Add(extension, new List<string> { extension });
            }

            savePicker.FileTypeChoices.Add("All files", fileExtensions);
            savePicker.SuggestedFileName = Path.GetFileNameWithoutExtension(fileName);

            var filePickerOperation = savePicker.PickSaveFileAsync();

            var file = await filePickerOperation;
            if (string.IsNullOrEmpty(file?.Path))
            {
                throw new Exception("User canceled or error in saving.");
            }

            await WriteStream(stream, file.Path).ConfigureAwait(false);
            return file.Path;
        }
    }
}
