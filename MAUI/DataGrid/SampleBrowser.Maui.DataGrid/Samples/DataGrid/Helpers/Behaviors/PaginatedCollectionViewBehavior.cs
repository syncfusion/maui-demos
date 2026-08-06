using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.DataGrid.SfDataGrid;
using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid.DataPager;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.DataGrid
{
    public class PaginatedCollectionViewBehavior : Behavior<SampleView>
    {

        #region Fields
        private CollectionView? collectionView;
        private PlaceInfoViewModel? pagingViewModel;
        private SfDataPager? dataPager;

        #endregion

        protected override void OnAttachedTo(SampleView bindable)
        {
            collectionView = bindable.FindByName<CollectionView>("verticalCollectionView");
            dataPager = bindable.FindByName<SfDataPager>("dataPager1");
            pagingViewModel = new PlaceInfoViewModel();
            collectionView.BindingContext = pagingViewModel;
            dataPager.PageCount = 8;
            dataPager.PageSize = 12;
            dataPager.UseOnDemandPaging = true;
            dataPager.OnDemandLoading += DataPager_OnDemandLoading;
            base.OnAttachedTo(bindable);
        }

        private void DataPager_OnDemandLoading(object? sender, OnDemandLoadingEventArgs e)
        {
            var source = pagingViewModel!.places!.Skip(e.StartIndex).Take(e.PageSize);
            collectionView!.ItemsSource = source.AsEnumerable();
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            dataPager!.OnDemandLoading -= DataPager_OnDemandLoading;
            collectionView = null;
            pagingViewModel = null;
            dataPager = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
