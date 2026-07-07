using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.DataGrid.SfDataGrid;
using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid.DataPager;
using Syncfusion.Maui.ListView;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.DataGrid
{
    public class PaginatedListViewBehavior : Behavior<SampleView>
    {

        #region Fields
        private Syncfusion.Maui.ListView.SfListView? listView;
        private ProductReviewInfoViewModel? pagingViewModel;
        private SfDataPager? dataPager;

        #endregion

        protected override void OnAttachedTo(SampleView bindable)
        {
            listView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("verticalListView");
            dataPager = bindable.FindByName<SfDataPager>("dataPager");
            pagingViewModel = new ProductReviewInfoViewModel();
            listView.BindingContext = pagingViewModel;
            dataPager.PageCount = 8;
            dataPager.PageSize = 12;
            dataPager.UseOnDemandPaging = true;
            dataPager.OnDemandLoading += DataPager_OnDemandLoading;
            base.OnAttachedTo(bindable);
        }

        private void DataPager_OnDemandLoading(object? sender, OnDemandLoadingEventArgs e)
        {
            var source = pagingViewModel!.ReviewInfo!.Skip(e.StartIndex).Take(e.PageSize);
            listView!.ItemsSource = source.AsEnumerable();
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            dataPager!.OnDemandLoading -= DataPager_OnDemandLoading;
            listView = null;
            pagingViewModel = null;
            dataPager = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
