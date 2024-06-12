#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PullToRefresh;
using Syncfusion.Maui.DataSource.Extensions;
using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.Inputs;
using Microsoft.Maui.Platform;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class PullToRefreshBehavior : Behavior<SampleView>
    {
        private SfPullToRefresh pullToRefresh;
        private Syncfusion.Maui.ListView.SfListView ListView;
        private InboxInfoViewModel ViewModel;
        private SfComboBox transitionType;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindable">SampleView type parameter named as bindable.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            this.ViewModel = new InboxInfoViewModel();
            bindable.BindingContext = ViewModel;
            this.pullToRefresh = bindable.FindByName<Syncfusion.Maui.PullToRefresh.SfPullToRefresh>("pullToRefresh");
            this.ListView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
            this.transitionType = bindable.FindByName<SfComboBox>("comboBox");
            this.transitionType.SelectionChanged += this.OnSelectionChanged;
            this.pullToRefresh.Refreshing += this.PullToRefresh_Refreshing;
            this.pullToRefresh.Refreshed += PullToRefresh_Refreshed;
            this.ListView.ItemTapped += ListView_ItemTapped;
            this.ListView.QueryItemSize += ListView_QueryItemSize;
            this.ListView!.DataSource!.SortDescriptors.Add(new SortDescriptor()
            {
                PropertyName = "Date",
                Direction = Syncfusion.Maui.DataSource.ListSortDirection.Descending,
            });
            this.ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "Date",
                KeySelector = (obj) =>
                {
                    var groupName = ((InboxInfo)obj).Date;
                    return GetKey(groupName);
                },
                Comparer = new CustomGroupComparer(),
            });
            this.ListView.DataSource.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;

            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Fired when pulltorefresh view is refreshed.
        /// </summary>
        /// <param name="sender">PullToRefresh_Refreshed event sender</param>
        /// <param name="e">PullToRefresh_Refreshed event args</param>
        private void PullToRefresh_Refreshed(object sender, EventArgs e)
        {
#if ANDROID
        (this.ListView!.Handler!.PlatformView as Android.Views.View)!.InvalidateMeasure(this.ListView as IView);
#endif
        }

        /// <summary>
        /// Fired when pullToRefresh View is refreshing
        /// </summary>
        /// <param name="sender">PullToRefresh_Refreshing event sender</param>
        /// <param name="e">PullToRefresh_Refreshing event args</param>
        private async void PullToRefresh_Refreshing(object sender, EventArgs e)
        {
            this.pullToRefresh!.IsRefreshing = true;
            await Task.Delay(2500);
            this.ViewModel!.AddItemsRefresh(3);
            this.pullToRefresh.IsRefreshing = false;
        }

        /// <summary>
        /// Fired when selected index is changed
        /// </summary>
        /// <param name="sender">OnSelectionChanged sender</param>
        /// <param name="e">OnSelectionChanged event args e</param>
        private void OnSelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            this.pullToRefresh!.TransitionMode = this.transitionType!.SelectedIndex == 0 ? PullToRefreshTransitionType.SlideOnTop : PullToRefreshTransitionType.Push;
        }

        /// <summary>
        /// Fired when taps the listview item.
        /// </summary>
        /// <param name="sender">ItemTapped sender.</param>
        /// <param name="e">Represents the event args.</param>
        private void ListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            if (e.ItemType == ItemType.Record)
            {
                (e.DataItem as InboxInfo)!.IsOpened = true;
            }
        }

        /// <summary>
        /// Fires whenever an item comes to view.
        /// </summary>
        /// <param name="sender">ListView_QueryItemSize event sender.</param>
        /// <param name="e">Represent ListView_QueryItemSize event args.</param>
        private void ListView_QueryItemSize(object sender, QueryItemSizeEventArgs e)
        {
            if (e.ItemType == ItemType.GroupHeader && e.ItemIndex == 0)
            {
                var groupName = e.DataItem as GroupResult;

                if (groupName != null && (GroupName)groupName.Key == GroupName.Today)
                {
                    e.ItemSize = 0;
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindable">SampleView type parameter named as bindable</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.ListView!.ItemTapped -= ListView_ItemTapped;
            this.ListView.QueryItemSize -= ListView_QueryItemSize;
            this.pullToRefresh!.Refreshing -= PullToRefresh_Refreshing;
            this.pullToRefresh.Refreshed -= PullToRefresh_Refreshed;
            this.transitionType!.SelectionChanged -= this.OnSelectionChanged;
            this.pullToRefresh = null;
            this.ListView = null;
            this.ViewModel = null;
            this.transitionType = null;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Helper method to get the key value for the GroupHeader name based on Data.
        /// </summary>
        /// <param name="groupName">Date of an item.</param>
        /// <returns>Returns specific group name.</returns>
        private GroupName GetKey(DateTime groupName)
        {
            int compare = groupName.Date.CompareTo(DateTime.Now.Date);

            if (compare == 0)
            {
                return GroupName.Today;
            }
            else if (groupName.Date.CompareTo(DateTime.Now.AddDays(-1).Date) == 0)
            {
                return GroupName.Yesterday;
            }
            else if (IsLastWeek(groupName))
            {
                return GroupName.LastWeek;
            }
            else if (IsThisWeek(groupName))
            {
                return GroupName.ThisWeek;
            }
            else if (IsThisMonth(groupName))
            {
                return GroupName.ThisMonth;
            }
            else if (IsLastMonth(groupName))
            {
                return GroupName.LastMonth;
            }
            else
            {
                return GroupName.Older;
            }
        }

        /// <summary>
        /// Helper method to check whether particular date is in this week or not.
        /// </summary>
        /// <param name="groupName">Date of an item.</param>
        /// <returns>Returns true if the mentioned date is in this week.</returns>
        private bool IsThisWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var currentSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day;

            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return currentSunday == groupWeekSunDay && (groupMonth == currentMonth || groupMonth == currentMonth - 1) && isCurrentYear;
        }

        /// <summary>
        /// Helper method to check whether particular date is in last week or not.
        /// </summary>
        /// <param name="groupName">Date of an item.</param>
        /// <returns>Returns true if the mentioned date is in last week.</returns>
        private bool IsLastWeek(DateTime groupName)
        {
            var groupWeekSunDay = groupName.AddDays(-(int)groupName.DayOfWeek).Day;
            var lastSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).Day - 7;

            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return lastSunday == groupWeekSunDay && (groupMonth == currentMonth || groupMonth == currentMonth - 1) && isCurrentYear;
        }

        /// <summary>
        /// Helper method to check whether particular date is in this month or not.
        /// </summary>
        /// <param name="groupName">Date of an item.</param>
        /// <returns>Returns true if the mentioned date is in this month.</returns>
        private bool IsThisMonth(DateTime groupName)
        {
            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return groupMonth == currentMonth && isCurrentYear;
        }

        /// <summary>
        /// Helper method to check whether particular date is in last month or not.
        /// </summary>
        /// <param name="groupName">Date of an item.</param>
        /// <returns>Returns true if the mentioned date is in last month.</returns>
        private bool IsLastMonth(DateTime groupName)
        {
            var groupMonth = groupName.Month;
            var currentMonth = DateTime.Today.AddMonths(-1).Month;

            var isCurrentYear = groupName.Year == DateTime.Today.Year;

            return groupMonth == currentMonth && isCurrentYear;
        }
    }

    public enum GroupName
    {
        Today = 0,
        Yesterday,
        ThisWeek,
        LastWeek,
        ThisMonth,
        LastMonth,
        Older
    }
}

