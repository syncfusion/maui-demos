#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using QuickLook;
using System;
using System.IO;
using System.Threading.Tasks;
using UIKit;

namespace SampleBrowser.Maui.Services
{
    public partial class SaveService
    {
        public partial void SaveAndView(string filename, string contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, filename);
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Create);
                stream.Position = 0;
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (contentType != "application/html" || exception == string.Empty)
            {
#pragma warning disable CA1416 //This call site is reachable on: 'iOS' 14.2 and later, 'maccatalyst' 14.2 and later. 'UIApplication.KeyWindow.get' is unsupported on: 'ios' 13.0 and later, 'maccatalyst' 13.0 and later.
                UIViewController? currentController = UIApplication.SharedApplication!.KeyWindow!.RootViewController;
#pragma warning restore CA1416 //This call site is reachable on: 'iOS' 14.2 and later, 'maccatalyst' 14.2 and later. 'UIApplication.KeyWindow.get' is unsupported on: 'ios' 13.0 and later, 'maccatalyst' 13.0 and later.
                while (currentController!.PresentedViewController != null)
                    currentController = currentController.PresentedViewController;

                QLPreviewController qlPreview = new();
                QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
                qlPreview.DataSource = new PreviewControllerDS(item);
                currentController.PresentViewController((UIViewController)qlPreview, true, null);
            }
        }
    }
}
