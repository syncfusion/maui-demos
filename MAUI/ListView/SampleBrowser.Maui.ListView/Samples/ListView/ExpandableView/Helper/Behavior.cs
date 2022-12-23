#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.ListView.Helpers;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    class GridBehavior : Behavior<Grid>
    {
        public Syncfusion.Maui.ListView.SfListView ListView { get; set; }
        private TapGestureRecognizer tapGestureRecognizer;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnItemTapped;
            (bindable as Grid).GestureRecognizers.Add(tapGestureRecognizer);
#if ANDROID
            ListView.RefreshView();
#endif
            base.OnAttachedTo(bindable);
        }

        private void OnItemTapped(object sender, EventArgs e)
        {
            var dataItem = (sender as Grid).BindingContext as ListViewFoodCategory;
            var currentIndex = ListView.DataSource.DisplayItems.IndexOf(dataItem);

            if (dataItem.IsExpanded)
            {
                dataItem.IsExpanded = false;
            }
            else
            {
                dataItem.IsExpanded = true;
            }
            ListView.RefreshItem(currentIndex, currentIndex, false);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {            
            base.OnDetachingFrom(bindable);            
            ListView = null;
            tapGestureRecognizer.Tapped -= OnItemTapped;
        }
    }
}
