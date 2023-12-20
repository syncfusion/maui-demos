#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Uri = Android.Net.Uri;
using Application = Android.App.Application;
using Environment = Android.OS.Environment;
using Android.Content;
using Android.Webkit;
using Android.Provider;
using System.Web;
using Java.IO;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public partial class FileService
    {
        internal static async Task<string?> PlatformSaveAsAsync(string fileName, Stream stream)
        {
            Uri? filePath = null;
            CancellationToken cancellationToken = CancellationToken.None;

            if (!OperatingSystem.IsAndroidVersionAtLeast(33))
            {
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>().WaitAsync(cancellationToken).ConfigureAwait(false);
                if (status is not PermissionStatus.Granted)
                {
                    throw new PermissionException("Storage permission is not granted.");
                }
            }

            var intent = new Intent(Intent.ActionCreateDocument);

            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType(MimeTypeMap.Singleton?.GetMimeTypeFromExtension(MimeTypeMap.GetFileExtensionFromUrl(fileName)) ?? "*/*");
            intent.PutExtra(Intent.ExtraTitle, fileName);
            await IntermediateActivity.StartAsync(intent, 2001, onResult: OnResult).WaitAsync(cancellationToken).ConfigureAwait(false);

            if (filePath is null)
            {
                throw new Exception("User canceled or error in saving.");
            }

            return await SaveDocument(filePath, stream, cancellationToken).ConfigureAwait(false);

            void OnResult(Intent resultIntent)
            {
                filePath = resultIntent.Data;
            }
        }

        static async Task<string?> SaveDocument(Uri uri, Stream stream, CancellationToken cancellationToken)
        {
            using var parcelFileDescriptor = Application.Context.ContentResolver?.OpenFileDescriptor(uri, "wt");
            using var fileOutputStream = new FileOutputStream(parcelFileDescriptor?.FileDescriptor);
            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            await fileOutputStream.WriteAsync(memoryStream.ToArray()).WaitAsync(cancellationToken).ConfigureAwait(false);

            return ConvertToPhysicalPath(uri);
        }

        static string? ConvertToPhysicalPath(Uri uri)
        {
            const string uriSchemeFolder = "content";
            if (uri.Scheme is null || !uri.Scheme.Equals(uriSchemeFolder, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (uri.PathSegments?.Count < 2)
            {
                return null;
            }

            // Example path would be /tree/primary:DCIM, or /tree/SDCare:DCIM
            var path = uri.PathSegments?[1];

            if (path is null)
            {
                return null;
            }

            var pathSplit = path.Split(':');
            if (pathSplit.Length < 2)
            {
                return null;
            }

            // Primary is the device's internal storage, and anything else is an SD card or other external storage
            if (pathSplit[0].Equals(AndroidDirectoryConstants.PrimaryStorage, StringComparison.OrdinalIgnoreCase))
            {
                // Example for internal path /storage/emulated/0/DCIM
                return $"{Environment.ExternalStorageDirectory?.Path}/{pathSplit[1]}";
            }

            // Example for external path /storage/1B0B-0B1C/DCIM
            return $"/{AndroidDirectoryConstants.Storage}/{pathSplit[0]}/{pathSplit[1]}";
        }
    }
}
