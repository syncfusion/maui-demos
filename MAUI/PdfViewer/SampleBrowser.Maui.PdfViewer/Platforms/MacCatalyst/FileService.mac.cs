#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Foundation;
using System.Threading;
using System.Runtime.Versioning;
using UIKit;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    [SupportedOSPlatform("iOS14.0")]
    [SupportedOSPlatform("MacCatalyst14.0")]
    public partial class FileService
    {
        static UIDocumentPickerViewController? documentPickerViewController;
        static TaskCompletionSource<string>? taskCompetedSource;
        internal static async Task<string> PlatformSaveAsAsync(string fileName, Stream stream)
        {
            string initialPath = "DCIM";
            CancellationToken cancellationToken = CancellationToken.None;
            var fileManager = NSFileManager.DefaultManager;
            var tempDirectoryPath = fileManager.GetTemporaryDirectory().Append(Guid.NewGuid().ToString(), true);
            var isDirectoryCreated = fileManager.CreateDirectory(tempDirectoryPath, true, null, out var error);
            if (!isDirectoryCreated)
            {
                throw new Exception(error?.LocalizedDescription ?? "Unable to create temp directory.");
            }

            var fileUrl = tempDirectoryPath.Append(fileName, false);
            await WriteStream(stream, fileUrl.Path);

            cancellationToken.ThrowIfCancellationRequested();
            taskCompetedSource?.TrySetCanceled(CancellationToken.None);
            var tcs = taskCompetedSource = new(cancellationToken);

            documentPickerViewController = new(new[] { fileUrl })
            {
                DirectoryUrl = NSUrl.FromString(initialPath)
            };
            documentPickerViewController.DidPickDocumentAtUrls += DocumentPickerViewControllerOnDidPickDocumentAtUrls;
            documentPickerViewController.WasCancelled += DocumentPickerViewControllerOnWasCancelled;

            var currentViewController = Platform.GetCurrentUIViewController();
            if (currentViewController is not null)
            {
                currentViewController.PresentViewController(documentPickerViewController, true, null);
            }
            else
            {
                throw new Exception("Unable to get a window where to present the file saver UI.");
            }

            return await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
        }

        static void DocumentPickerViewControllerOnWasCancelled(object? sender, EventArgs e)
        {
            taskCompetedSource?.TrySetException(new Exception("Operation cancelled."));
        }

        static void DocumentPickerViewControllerOnDidPickDocumentAtUrls(object? sender, UIDocumentPickedAtUrlsEventArgs e)
        {
            taskCompetedSource?.TrySetResult(e.Urls[0].Path ?? throw new Exception("Unable to retrieve the path of the saved file."));
        }
    }
}
