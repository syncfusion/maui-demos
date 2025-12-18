#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
