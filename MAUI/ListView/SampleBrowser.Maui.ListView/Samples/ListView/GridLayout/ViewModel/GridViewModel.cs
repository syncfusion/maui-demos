using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class GridLayoutViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<ListViewGalleryInfo> galleryinfo;     
        private double imageHeightRequest;
        #endregion

        #region Constructor

        public GridLayoutViewModel()
        {
            FavoriteImageCommand = new Command(SetFavorite);           
            GenerateSource();           
        }

        #endregion

        #region Properties

        public Command FavoriteImageCommand
        {
            get;
        }

        public ObservableCollection<ListViewGalleryInfo> Gallerynfo
        {
            get { return galleryinfo; }
            set { this.galleryinfo = value; }
        }
     
        public double ImageHeightRequest
        {
            get { return imageHeightRequest; }
            set
            {
                this.imageHeightRequest = value;
                this.OnPropertyChanged(nameof(ImageHeightRequest));
            }
        }

        #endregion

        public void GenerateSource()
        {
            ListViewGalleryInfoRepository bookRepository = new ListViewGalleryInfoRepository();
            galleryinfo = bookRepository.GetGalleryInfo();
        }        

        private void SetFavorite(object obj)
        {
            if (obj != null)
            {
                var item = obj as ListViewGalleryInfo;
                item!.IsFavorite = !item.IsFavorite;
            }
        }       

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
