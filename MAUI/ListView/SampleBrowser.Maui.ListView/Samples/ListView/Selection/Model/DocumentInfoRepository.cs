#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class DocumentInfoRepository
    {
        #region Constructor

        public DocumentInfoRepository()
        {

        }

        #endregion

        #region Properties

        internal ObservableCollection<DocumentInfo> GetDocumentInfo()
        {
            var random = new Random();
            var documetInfo = new ObservableCollection<DocumentInfo>();
            for (int i = 0; i < DocumentNames.Length; i++)
            {
                var info = new DocumentInfo()
                {
                    DocumentName = DocumentNames[i],
                    DocumentSize = DocumentSizes[i],
                    DocumentImage = DocumentImages[i],
                };
                documetInfo.Add(info);
            }
            return documetInfo;
        }

        #endregion

        #region DocumentInfo

        readonly string[] DocumentNames = new string[]
        {
           ".NET MAUI Guidelines",
           "Introduction to .NET MAUI",
           "Learn .NET MAUI",
           ".NET MAUI First look",
           "What is .NET MAUI",
           "Learn .NET Community Toolkit",
           "A Journey to .NET MAUI",
           ".NET MAUI community stand up",
           ".NET MAUI samples demo",
           ".NET MAUI community samples",
           "Sample browser",
           "com.syncfusion.samplebrowser.maui.Signed",
           "com.android.chrome",
           "com.android.vending",
           "com.android.mediacenter",
           "Syncfusion Gallery",
           "Syncfusion Orientation Program",
           "Employee list",
           "Team list",
           "Escalation list",
        };
        readonly string[] DocumentSizes = new string[]
        {
           "12 KB",
           "10 MB",
           "12 MB",
           "10 MB",
           "5 KB",
           "20 MB",
           "10 KB",
           "30 MB",
           "50 MB",
           "100 MB",
           "50 MB",
           "22 MB",
           "2 KB",
           "4 KB",
           "2 KB",
           "50 MB",
           "10 KB",
           "25 KB",
           "10 KB",
           "25 KB",
        };
        readonly string[] DocumentImages = new string[]
       {
           "pdfimage.png",
           "video.png",
           "video.png",
           "video.png",
           "pdfimage.png",
           "video.png",
           "pdfimage.png",
           "video.png",
           "video.png",
           "zip.png",
           "zip.png",
           "apk.png",
           "folder.png",
           "folder.png",
           "folder.png",
           "folder.png",
           "ppt.png",
           "excelimage.png",
           "excelimage.png",
           "excelimage.png",
       };

        #endregion
    }
}
