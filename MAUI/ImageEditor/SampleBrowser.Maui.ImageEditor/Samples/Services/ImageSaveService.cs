#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
#if ANDROID
    using Android.Content;
    using Android.OS;
    using Java.IO;
#elif WINDOWS
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    using Windows.UI.Popups;
#elif IOS || MACCATALYST
    using QuickLook;
    using UIKit;
    using Foundation;
#endif

    /// <summary>
    /// Represents the image save service.
    /// </summary>
    internal static class ImageSaveService
    {
        /// <summary>
        /// Saves and views the image.
        /// </summary>
        /// <param name="filename">The file name</param>
        /// <param name="contentType">The file type.</param>
        /// <param name="stream">The image stream.</param>
        internal static async void SaveAndView(string filename, string contentType, MemoryStream stream)
        {
#if ANDROID
            string exception = string.Empty;
            string? root;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.App.Application.Context?.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads)?.AbsolutePath;
            }
            else
            {
                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            }

            if (root == null)
            {
                return;
            }

            Java.IO.File myDir = new(root);
            myDir.Mkdir();

            Java.IO.File file = new(myDir, filename);

            if (file.Exists())
            {
                file.Delete();
            }

            try
            {
                FileOutputStream outs = new(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            if (file.Exists())
            {
                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
                {
                    var fileUri = AndroidX.Core.Content.FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context?.PackageName + ".provider", file);
                    var intent = new Intent(Intent.ActionView);
                    intent.SetData(fileUri);
                    intent.AddFlags(ActivityFlags.NewTask);
                    intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                    Android.App.Application.Context?.StartActivity(intent);
                }
                else
                {
                    var fileUri = Android.Net.Uri.Parse(file.AbsolutePath);
                    var intent = new Intent(Intent.ActionView);
                    intent.SetDataAndType(fileUri, contentType);
                    intent = Intent.CreateChooser(intent, "Open File");
                    intent?.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context?.StartActivity(intent);
                }
            }
#elif WINDOWS
            StorageFile stFile;
            string extension = Path.GetExtension(filename);
            //Gets process windows handle to open the dialog in application process. 
            IntPtr windowHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //Creates file save picker to save a file. 
                FileSavePicker savePicker = new();
                if (extension == ".png")
                {
                    savePicker.DefaultFileExtension = ".png";
                    savePicker.SuggestedFileName = filename;
                    //Saves the file as png file.
                    savePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });
                }
                else if (extension == ".jpg" || extension == ".jpeg")
                {
                    savePicker.DefaultFileExtension = ".jpg";
                    savePicker.SuggestedFileName = filename;
                    savePicker.FileTypeChoices.Add("JPG", new List<string>() { ".jpg", "jpeg" });
                }

                WinRT.Interop.InitializeWithWindow.Initialize(savePicker, windowHandle);
                stFile = await savePicker.PickSaveFileAsync();
            }
            else
            {
                StorageFolder local = ApplicationData.Current.LocalFolder;
                stFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            }

            if (stFile != null)
            {
                using (IRandomAccessStream zipStream = await stFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    //Writes compressed data from memory to file.
                    using Stream outstream = zipStream.AsStreamForWrite();
                    outstream.SetLength(0);
                    //Saves the stream as file.
                    byte[] buffer = stream.ToArray();
                    outstream.Write(buffer, 0, buffer.Length);
                    outstream.Flush();
                }
                //Create message dialog box. 
                MessageDialog msgDialog = new("Do you want to view the image?", "Image has been saved successfully");
                UICommand yesCmd = new("Yes");
                msgDialog.Commands.Add(yesCmd);
                UICommand noCmd = new("No");
                msgDialog.Commands.Add(noCmd);

                WinRT.Interop.InitializeWithWindow.Initialize(msgDialog, windowHandle);

                //Showing a dialog box. 
                IUICommand cmd = await msgDialog.ShowAsync();
                if (cmd.Label == yesCmd.Label)
                {
                    //Launch the saved file. 
                    await Windows.System.Launcher.LaunchFileAsync(stFile);
                }
            }
#elif IOS || MACCATALYST
            string exception = string.Empty;
#if MACCATALYST
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
#else
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
#endif
            string filePath = Path.Combine(path, filename);
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite);
                stream.Position = 0;
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Dispose();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            if (string.IsNullOrEmpty(exception))
            {
#pragma warning disable CA1416 //This call site is reachable on: 'iOS' 14.2 and later, 'maccatalyst' 14.2 and later. 'UIApplication.KeyWindow.get' is unsupported on: 'ios' 13.0 and later, 'maccatalyst' 13.0 and later.
                UIViewController? currentController = UIApplication.SharedApplication?.KeyWindow?.RootViewController;
#pragma warning restore CA1416 //This call site is reachable on: 'iOS' 14.2 and later, 'maccatalyst' 14.2 and later. 'UIApplication.KeyWindow.get' is unsupported on: 'ios' 13.0 and later, 'maccatalyst' 13.0 and later.
                while (currentController?.PresentedViewController != null)
                {
                    currentController = currentController.PresentedViewController;
                }

                QLPreviewController qlPreview = new();
                QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
                qlPreview.DataSource = new PreviewControllerDS(item);
                currentController?.PresentViewController(qlPreview, true, null);
            }

#endif
            await Task.Delay(1);
        }
    }

#if IOS || MACCATALYST

    internal class QLPreviewItemBundle : QLPreviewItem
    {
        readonly string _fileName, _filePath;
        internal QLPreviewItemBundle(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public override string PreviewItemTitle
        {
            get
            {
                return _fileName;
            }
        }
        public override NSUrl PreviewItemUrl
        {
            get
            {
                var documents = NSBundle.MainBundle.BundlePath;
                var lib = Path.Combine(documents, _filePath);
                var url = NSUrl.FromFilename(lib);
                return url;
            }
        }
    }

    internal class PreviewControllerDS : QLPreviewControllerDataSource
    {
        private readonly QLPreviewItem _item;

        internal PreviewControllerDS(QLPreviewItem item)
        {
            _item = item;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return (nint)1;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return _item;
        }
    }
#endif
}