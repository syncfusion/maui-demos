#region Copyright Syncfusion Inc. 2001 - 2024
// Copyright Syncfusion Inc. 2001 - 2024. All rights reserved.
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
				UIWindow? window = GetKeyWindow();
                if (window != null && window.RootViewController != null)
                {
                    UIViewController? uiViewController = window.RootViewController;
                    if (uiViewController != null)
                    {
                        QLPreviewController qlPreview = new();
                        QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
                        qlPreview.DataSource = new PreviewControllerDS(item);
                        uiViewController.PresentViewController((UIViewController)qlPreview, true, null);
                    }
                }
            }
        }
        public UIWindow? GetKeyWindow()
        {
            foreach (var scene in UIApplication.SharedApplication.ConnectedScenes)
            {
                if (scene is UIWindowScene windowScene)
                {
                    foreach (var window in windowScene.Windows)
                    {
                        if (window.IsKeyWindow)
                        {
                            return window;
                        }
                    }
                }
            }
			return null;
        }
    }
}
