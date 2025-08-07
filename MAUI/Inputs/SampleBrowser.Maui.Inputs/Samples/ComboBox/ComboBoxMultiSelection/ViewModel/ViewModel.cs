#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using SampleBrowser.Maui.Base.Converters;
using SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.Model;

namespace SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Applicationlist> appCollection;
        public ObservableCollection<Applicationlist> AppCollection
        {
            get { return appCollection; }
            set
            {
                appCollection = value;
                RaisePropertyChanged("AppCollection");
            }
        }

        private ObservableCollection<Applicationlist> selectedItem = new ObservableCollection<Applicationlist>();
        public ObservableCollection<Applicationlist> SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ApplicationViewModel()
        {
            appCollection = new ObservableCollection<Applicationlist>();

            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.calculator.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Calculator", Date = "Updated 2 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.multicamera.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Camera", Date = "Updated 4 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.cdmusic.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "CD Music", Date = "Updated 9 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.microsoftexcel.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Excel", Date = "Updated 2 hours ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.gmailfill.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Gmail", Date = "Updated 9 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.chromefill.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Google Chrome", Date = "Updated 2 hours ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.instagram.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Instagram", Date = "Updated 8 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.maps_red.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Maps", Date = "Updated 11 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.microsoftpowerpoint.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Microsoft Power Point", Date = "Updated 9 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.microsoftword.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Microsoft Word", Date = "Updated 7 days ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.skypefill.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Skype", Date = "Updated 1 day ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.twitter.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Twitter", Date = "Updated 6 weeks ago" });
            appCollection.Add(new Applicationlist() { AppImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.yahoofill.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly), AppName = "Yahoo", Date = "Updated 2 days ago" });
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

