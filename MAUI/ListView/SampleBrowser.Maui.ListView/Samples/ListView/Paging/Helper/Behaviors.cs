#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
