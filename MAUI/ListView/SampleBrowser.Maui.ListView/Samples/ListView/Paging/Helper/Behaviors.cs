using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid.DataPager;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class SfListViewPagingBehavior : Behavior<SampleView>
    {
        #region Fields
        private Syncfusion.Maui.ListView.SfListView? listView;
        private PagingViewModel? pagingViewModel;
        private SfDataPager? dataPager;

        #endregion

        protected override void OnAttachedTo(SampleView bindable)
        {
            listView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("verticalListView");
            dataPager = bindable.FindByName<SfDataPager>("dataPager");
            pagingViewModel = new PagingViewModel();
            listView.BindingContext = pagingViewModel;
            dataPager.PageCount = 8;
            dataPager.PageSize = 12;
            dataPager.UseOnDemandPaging = true;
            dataPager.OnDemandLoading += DataPager_OnDemandLoading;
            base.OnAttachedTo(bindable);
        }

        private void DataPager_OnDemandLoading(object? sender, OnDemandLoadingEventArgs e)
        {
            var source = pagingViewModel!.places!.Skip(e.StartIndex).Take(e.PageSize);
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
