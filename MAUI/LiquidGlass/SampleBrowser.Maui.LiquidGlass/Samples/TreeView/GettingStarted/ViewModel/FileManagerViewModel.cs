#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
//using SampleBrowser.Maui.LiquidGlass.SfTreeView;
using Syncfusion.Maui.TreeView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.LiquidGlass
{
    internal class FileManagerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<object>? checkedItems;

        private TreeViewSortDirection sortingOptions;
        public ObservableCollection<Folder> Folders { get;set; }
        public ObservableCollection<object>? CheckedItems
        {
            get { return checkedItems; }
            set { this.checkedItems = value; }
        }

        public TreeViewSortDirection SortingOptions
        {
            get
            {
                return sortingOptions;
            }
            set
            {
                sortingOptions = value;
                OnPropertyChanged(nameof(SortingOptions));
            }
        }

        public FileManagerViewModel() 
        {
            this.Folders = GenerateItem();
        }

        private ObservableCollection<Folder> GenerateItem()
        {

            var doc = new Folder() { FolderName = "Documents", ImageIcon = "folder.png" };
            var download = new Folder() { FolderName = "Downloads", ImageIcon = "folder.png" };
            var music = new Folder() { FolderName = "Music", ImageIcon = "folder.png" };
            var picture = new Folder() { FolderName = "Pictures", ImageIcon = "folder.png" };
            var videos = new Folder() { FolderName = "Videos", ImageIcon = "folder.png" };

            var pollution = new File() { FolderName = "Environmental Pollution.docx", ImageIcon = "Word.png" };
            var global = new File { FolderName = "Global Warming.ppt", ImageIcon = "ppt.png" };
            var sanitation = new File() { FolderName = "Sanitation.docx", ImageIcon = "Word.png" };
            var socialNetwork = new File { FolderName = "Social Networks.ppt", ImageIcon = "ppt.png" };
            var youthEmpowerment = new File { FolderName = "Youth Empowerment.pdf", ImageIcon = "pdfimage.png" };

            var tutorials = new File() { FolderName = "Tutorials.zip", ImageIcon = "zip.png" };
            var typeScript = new File() { FolderName = "TypeScript.7z", ImageIcon = "zip.png" };
            var uiGuide = new File() { FolderName = "UI-Guide.pdf", ImageIcon = "pdfimage.png" };

            var song = new File() { FolderName = "Gouttes", ImageIcon = "Audio.png" };

            var camera = new File() { FolderName = "Camera Roll", ImageIcon = "folder.png"};
            var stone = new File() { FolderName = "Stone.jpg", ImageIcon = "image.png" };
            var wind = new File() { FolderName = "Wind.jpg", ImageIcon = "image.png" };

            var pic1 = new SubFiles() { FolderName = "WIN_20160726_09.JPG", ImageIcon = "people_circle23.png" };
            var pic2 = new SubFiles() { FolderName = "WIN_20160726.Png", ImageIcon = "people_circle2.png" };

            var video1 = new File() { FolderName = "Naturals.mp4", ImageIcon = "video.png" };
            var video2 = new File() { FolderName = "Wild.mpeg", ImageIcon = "video.png"};

            doc.FilesInfo = new ObservableCollection<File>();
            doc.FilesInfo.Add(pollution);
            doc.FilesInfo.Add(global);
            doc.FilesInfo.Add(sanitation);
            doc.FilesInfo.Add(socialNetwork);
            doc.FilesInfo.Add(youthEmpowerment);

            download.FilesInfo = new ObservableCollection<File>();
            download.FilesInfo.Add(tutorials);
            download.FilesInfo.Add(typeScript);
            download.FilesInfo.Add(uiGuide);

            music.FilesInfo = new ObservableCollection<File>();
            music.FilesInfo.Add(song);

            picture.FilesInfo = new ObservableCollection<File>();
            picture.FilesInfo.Add(camera);
            picture.FilesInfo.Add(stone);
            picture.FilesInfo.Add(wind);

            videos.FilesInfo= new ObservableCollection<File>();
            videos.FilesInfo.Add(video1);
            videos.FilesInfo.Add(video2);

            camera.SubFiles = new ObservableCollection<SubFiles>();
            camera.SubFiles.Add(pic1);
            camera.SubFiles.Add(pic2);

            var deviceList=new ObservableCollection<Folder>();
            deviceList.Add(doc);
            deviceList.Add(download);
            deviceList.Add(music);
            deviceList.Add(picture);
            deviceList.Add(videos);

            checkedItems = new ObservableCollection<object>();
            checkedItems.Add(video1);
            checkedItems.Add(global);
            checkedItems.Add(camera);
            checkedItems.Add(socialNetwork);

            return deviceList;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
