#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.ListView.Helpers;

#nullable disable
namespace SampleBrowser.Maui.SfListView
{
    class SfListViewAccordionBehavior : Behavior<SampleView>
    {
        #region Fields

        private Contact tappedItem;
        private Syncfusion.Maui.ListView.SfListView listview;
        private AccordionViewModel viewModel;

        #endregion

        #region Override Methods

        protected override void OnAttachedTo(SampleView bindable)
        {
            listview = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
            viewModel = bindable.FindByName<AccordionViewModel>("viewModel");
            listview.ItemTapped += ListView_ItemTapped;

        }


        protected override void OnDetachingFrom(BindableObject bindable)
        {
            listview.ItemTapped -= ListView_ItemTapped;
        }
        #endregion

        #region Private Methods

        private void ListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            if (tappedItem != null && tappedItem.IsVisible)
            {
                var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);

                tappedItem.IsVisible = false;

                if (DeviceInfo.Platform != DevicePlatform.MacCatalyst)
                    listview.Dispatcher.Dispatch(() => { listview.RefreshItem(previousIndex, previousIndex, false); });
            }

            if (tappedItem == (e.ItemData as Contact))
            {
                if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                {
                    var previousIndex = listview.DataSource.DisplayItems.IndexOf(tappedItem);
                    listview.Dispatcher.Dispatch(() => { listview.RefreshItem(previousIndex, previousIndex, false); });
                }

                tappedItem = null;
                return;
            }

            tappedItem = e.ItemData as Contact;
            tappedItem.IsVisible = true;

            if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                var visibleLines = this.listview.GetVisualContainer().ScrollRows.GetVisibleLines();
                var firstIndex = visibleLines[visibleLines.FirstBodyVisibleIndex].LineIndex;
                var lastIndex = visibleLines[visibleLines.LastBodyVisibleIndex].LineIndex;
                listview.Dispatcher.Dispatch(() => { listview.RefreshItem(firstIndex, lastIndex, false); });
            }
            else
            {
                var currentIndex = listview.DataSource.DisplayItems.IndexOf(e.ItemData);
                listview.Dispatcher.Dispatch(() => { listview.RefreshItem(currentIndex, currentIndex, false); });
            }
        }
        #endregion
    }
}
