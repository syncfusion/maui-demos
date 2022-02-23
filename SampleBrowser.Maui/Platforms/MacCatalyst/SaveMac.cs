#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Controls;
using System;
using QuickLook;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(SaveMac))]

class SaveMac : ISave
{
    public async Task SaveAndView(string filename, string contentType, MemoryStream stream)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(path, filename);
        //Saves the document
        using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        stream.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Dispose();

        //Launch the file
        UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        while (currentController.PresentedViewController != null)
            currentController = currentController.PresentedViewController;
        UIView currentView = currentController.View;

        QLPreviewController qlPreview = new QLPreviewController();
        QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
        qlPreview.DataSource = new PreviewControllerDS(item);
        currentController.PresentViewController((UIViewController)qlPreview, true, (Action)null);

    }


}

public class QLPreviewItemFileSystem : QLPreviewItem
{
    string _fileName, _filePath;

    public QLPreviewItemFileSystem(string fileName, string filePath)
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
            return NSUrl.FromFilename(_filePath);
        }
    }
}

public class QLPreviewItemBundle : QLPreviewItem
{
    string _fileName, _filePath;
    public QLPreviewItemBundle(string fileName, string filePath)
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

public class PreviewControllerDS : QLPreviewControllerDataSource
{
    private QLPreviewItem _item;

    public PreviewControllerDS(QLPreviewItem item)
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




