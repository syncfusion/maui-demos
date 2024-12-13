#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SampleBrowser.Maui.ListView.SfListView.ListViewSwipingViewModel;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewSwipingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.ListView.SfListView ListView;

        protected override void OnAttachedTo(SampleView bindable)
        {
            ListView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
            (bindable.BindingContext as ListViewSwipingViewModel).ResetSwipeView += ListViewSwipingBehavior_ResetSwipeView;
            ListView.ItemTapped += ListView_ItemTapped;
            base.OnAttachedTo(bindable);
        }

        private void ListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            (e.DataItem as ListViewInboxInfo).IsOpened = true;
        }

        private void ListViewSwipingBehavior_ResetSwipeView(object sender, ResetEventArgs e)
        {
            ListView!.ResetSwipeItem();
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            (bindable.BindingContext as ListViewSwipingViewModel).ResetSwipeView -= ListViewSwipingBehavior_ResetSwipeView;
            ListView.ItemTapped -= ListView_ItemTapped;
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
