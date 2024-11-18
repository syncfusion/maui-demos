#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Base.Converters;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.TabView.SfTabView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabViewGettingStarted : SampleView
    {
       
        public TabViewGettingStarted()
        {
            InitializeComponent();
            this.BindingContext = new TabViewModel();
        }

        private void tabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
        {
            switch(e.NewIndex)
            {
                case 0:
                    FavoritesIcon.Color = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
                    RecentsIcon.Color = ContactsIcon.Color = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["TextColour"] : (Color)App.Current.Resources["TextColourLight"];
                    break;

                case 1:
                    RecentsIcon.Color = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
                    FavoritesIcon.Color = ContactsIcon.Color = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["TextColour"] : (Color)App.Current.Resources["TextColourLight"];
                    break;

                case 2:
                    ContactsIcon.Color = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
                    FavoritesIcon.Color = RecentsIcon.Color = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["TextColour"] : (Color)App.Current.Resources["TextColourLight"];
                    break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TabModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color? ImageBackground { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImageSource? ImageSource { get; set; }
    }

    public class TabViewModel
    {
        
        /// <summary>
        /// 
        /// </summary>
        public List<TabModel> TabModelSource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TabViewModel()
        {
            TabModelSource = new List<TabModel>();
            List<TabModel> TabModels = new List<TabModel>();
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#F0F361"), Name = "Alex", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.alexandar.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#FFC252"), Name = "Clara", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.clara.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#8AF8FF"), Name = "Steve", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.sebastian.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#A1B2FF"), Name = "Richard", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.jackson.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#7A7A7A"), Name = "Nora", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.nora.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#FFB381"), Name = "David", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.tye.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#7FE8EE"), Name = "Gabriella", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.gabriella.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#FFF27C"), Name = "Lita", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.lita.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
#if WINDOWS || MACCATALYST
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#EB70FF"), Name = "Liam", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.liam.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#F0F361"), Name = "Dave", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.alexandar.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
            TabModels.Add(new TabModel() { ImageBackground = Color.FromArgb("#8AF8FF"), Name = "Ben", ImageSource = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images.sebastian.png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly) });
#endif
            TabModelSource = TabModels;
        }

    }
}
