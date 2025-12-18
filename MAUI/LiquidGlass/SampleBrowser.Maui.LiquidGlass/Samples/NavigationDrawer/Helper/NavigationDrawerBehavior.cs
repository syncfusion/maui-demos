#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.DataSource.Extensions;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.ListView.Helpers;
using Syncfusion.Maui.NavigationDrawer;
using System.ComponentModel;
using System.Reflection;

namespace SampleBrowser.Maui.LiquidGlass.SfNavigationDrawer;

/// <summary>
/// Base generic class for user-defined behaviors that can respond to conditions and events.
/// </summary>
public class NavigationDrawerBehavior : Behavior<SampleView>
{
    private Syncfusion.Maui.ListView.SfListView? ListView;
    private MailInfoViewModel? ViewModel;

    private Syncfusion.Maui.Inputs.SfComboBox? positionType;
    private SfEffectsView? inboxeffectsView;
    private SfEffectsView? starredeffectsView;
    private SfEffectsView? senteffectsView;
    private SfEffectsView? draftseffectsView;
    private SfEffectsView? allmailseffectsView;
    private SfEffectsView? trasheffectsView;
    private SfEffectsView? spameffectsView;
    private SfEffectsView? primarynavigationEffectsView;
    private Syncfusion.Maui.NavigationDrawer.SfNavigationDrawer? navigationdrawer;
    private Label? title;

    /// <summary>
    /// You can override this method to subscribe to AssociatedObject events and initialize properties.
    /// </summary>
    /// <param name="bindable">SampleView type parameter named as bindable.</param>
    protected override void OnAttachedTo(SampleView bindable)
    {
        this.ViewModel = new MailInfoViewModel();
        bindable.BindingContext = this.ViewModel;
        this.ListView = bindable.FindByName<Syncfusion.Maui.ListView.SfListView>("listView");
        this.ListView.ItemsSource = this.ViewModel.MailInfos;
        this.navigationdrawer = bindable.FindByName<Syncfusion.Maui.NavigationDrawer.SfNavigationDrawer>("navigationDrawer");
        this.navigationdrawer.BindingContext = this.ViewModel;
        this.positionType = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("positioncomboBox");
        this.positionType.SelectionChanged += this.OnPositionSelectionChanged;

        this.inboxeffectsView = bindable.FindByName<SfEffectsView>("inboxEffects");
        TapGestureRecognizer inboxtapGestureRecognizer = new TapGestureRecognizer();
        inboxtapGestureRecognizer.Tapped += InboxTapGestureRecognizer_Tapped;
        this.inboxeffectsView.GestureRecognizers.Add(inboxtapGestureRecognizer);

        this.starredeffectsView = bindable.FindByName<SfEffectsView>("starredEffects");
        TapGestureRecognizer starredtapGestureRecognizer = new TapGestureRecognizer();
        starredtapGestureRecognizer.Tapped += StarredTapGestureRecognizer_Tapped;
        this.starredeffectsView.GestureRecognizers.Add(starredtapGestureRecognizer);

        this.senteffectsView = bindable.FindByName<SfEffectsView>("sentEffects");
        TapGestureRecognizer senttapGestureRecognizer = new TapGestureRecognizer();
        senttapGestureRecognizer.Tapped += SentTapGestureRecognizer_Tapped;
        this.senteffectsView.GestureRecognizers.Add(senttapGestureRecognizer);

        this.draftseffectsView = bindable.FindByName<SfEffectsView>("draftEffects");
        TapGestureRecognizer drafttapGestureRecognizer = new TapGestureRecognizer();
        drafttapGestureRecognizer.Tapped += DraftTapGestureRecognizer_Tapped;
        this.draftseffectsView.GestureRecognizers.Add(drafttapGestureRecognizer);

        this.allmailseffectsView = bindable.FindByName<SfEffectsView>("allMailEffects");
        TapGestureRecognizer allmailtapGestureRecognizer = new TapGestureRecognizer();
        allmailtapGestureRecognizer.Tapped += AllMailsTapGestureRecognizer_Tapped;
        this.allmailseffectsView.GestureRecognizers.Add(allmailtapGestureRecognizer);

        this.trasheffectsView = bindable.FindByName<SfEffectsView>("trashEffects");
        TapGestureRecognizer trashtapGestureRecognizer = new TapGestureRecognizer();
        trashtapGestureRecognizer.Tapped += TrashTapGestureRecognizer_Tapped;
        this.trasheffectsView.GestureRecognizers.Add(trashtapGestureRecognizer);

        this.spameffectsView = bindable.FindByName<SfEffectsView>("spamEffects");
        TapGestureRecognizer spamtapGestureRecognizer = new TapGestureRecognizer();
        spamtapGestureRecognizer.Tapped += SpamTapGestureRecognizer_Tapped;
        this.spameffectsView.GestureRecognizers.Add(spamtapGestureRecognizer);

        this.primarynavigationEffectsView = bindable.FindByName<SfEffectsView>("primaryNavigation");
        TapGestureRecognizer navigationtapGestureRecognizer = new TapGestureRecognizer();
        navigationtapGestureRecognizer.Tapped += NavigationTapGestureRecognizer_Tapped;
        this.primarynavigationEffectsView.GestureRecognizers.Add(navigationtapGestureRecognizer);

        this.title = bindable.FindByName<Label>("titleName");

        this.ListView.PropertyChanged += ListView_PropertyChanged;
        this.ListView.ItemTapped += ListView_ItemTapped;
        this.ListView.QueryItemSize += ListView_QueryItemSize;
        this.ListView.SwipeEnded += ListView_SwipeEnded;
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
                var groupName = ((MailInfo)obj).Date;
                return GetKey(groupName);
            },
            Comparer = new CustomGroupComparer(),
        });
        this.ListView.DataSource.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;

        base.OnAttachedTo(bindable);
    }

    private void NavigationTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.navigationdrawer!.ToggleDrawer();
    }

    private void InboxTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.inboxeffectsView!.IsSelected = true;
        this.title!.Text = "Inbox";
        this.ViewModel!.AddItemsRefresh(1);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void StarredTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.starredeffectsView!.IsSelected = true;
        this.title!.Text = "Starred";
        this.ViewModel!.AddItemsRefresh(2);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void SentTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.senteffectsView!.IsSelected = true;
        this.title!.Text = "Sent";
        this.ViewModel!.AddItemsRefresh(3);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void DraftTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.draftseffectsView!.IsSelected = true;
        this.title!.Text = "Draft";
        this.ViewModel!.AddItemsRefresh(4);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void AllMailsTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.allmailseffectsView!.IsSelected = true;
        this.title!.Text = "All Mails";
        this.ViewModel!.AddItemsRefresh(1);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void TrashTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.trasheffectsView!.IsSelected = true;
        this.title!.Text = "Trash";
        this.ViewModel!.AddItemsRefresh(2);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void SpamTapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        this.ResetSelection();
        this.spameffectsView!.IsSelected = true;
        this.title!.Text = "Spam";
        this.ViewModel!.AddItemsRefresh(1);
        this.ListView!.ItemsSource = ViewModel!.MailInfos;
        this.navigationdrawer!.ToggleDrawer();
    }

    private void ResetSelection()
    {
        this.inboxeffectsView!.IsSelected = false;
        this.starredeffectsView!.IsSelected = false;
        this.senteffectsView!.IsSelected = false;
        this.draftseffectsView!.IsSelected = false;
        this.allmailseffectsView!.IsSelected = false;
        this.trasheffectsView!.IsSelected = false;
        this.spameffectsView!.IsSelected = false;
    }

    /// <summary>
    ///  Fires whenever Position ComboBox's selection changed.
    /// </summary>
    /// <param name="sender">OnPositionSelectionChanged sender.</param>
    /// <param name="e">SelectionChangedEventArgs e.</param>
    private void OnPositionSelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (positionType != null)
        {
            switch (positionType.SelectedItem?.ToString())
            {
                case "Right":
                    this.navigationdrawer!.DrawerSettings.Position = Position.Right;
                    break;

                case "Top":
                    this.navigationdrawer!.DrawerSettings.Position = Position.Top;
                    break;

                case "Bottom":
                    this.navigationdrawer!.DrawerSettings.Position = Position.Bottom;
                    break;

                default:
                    this.navigationdrawer!.DrawerSettings.Position = Position.Left;
                    break;
            }
        }
    }

    /// <summary>
    /// Fires whenever ListView's propertychanged.
    /// </summary>
    /// <param name="sender">ListView_PropertyChanged event sender.</param>
    /// <param name="e">ListView_PropertyChanged event args.</param>
    private void ListView_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Width" && this.ListView!.Orientation == ItemsLayoutOrientation.Vertical && this.ListView.SwipeOffset != this.ListView.Width)
            this.ListView.SwipeOffset = this.ListView.Width;
        else if (e.PropertyName == "Height" && this.ListView!.Orientation == ItemsLayoutOrientation.Horizontal && this.ListView.SwipeOffset != this.ListView.Height)
            this.ListView.SwipeOffset = this.ListView.Height;
    }

    /// <summary>
    /// Fired when taps the listview item.
    /// </summary>
    /// <param name="sender">ItemTapped sender.</param>
    /// <param name="e">Represents the event args.</param>
    private void ListView_ItemTapped(object? sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (e.DataItem as MailInfo != null)
        {
            (e.DataItem as MailInfo)!.IsOpened = true;
        }
    }

    /// <summary>
    /// Fired when Swiped ended.
    /// </summary>
    /// <param name="sender">ListView_SwipeEnded event sender.</param>
    /// <param name="e">Represent SwipeEndedEvent args.</param>
    private async void ListView_SwipeEnded(object? sender, Syncfusion.Maui.ListView.SwipeEndedEventArgs e)
    {
        if (e.Offset <= 100)
        {
            return;
        }

        if (e.Direction == SwipeDirection.Right)
        {
            await Task.Delay(400);
            this.ViewModel!.ArchiveCommand!.Execute(e.DataItem);
        }

        if (e.Direction == SwipeDirection.Left)
        {
            await Task.Delay(400);
            this.ViewModel!.DeleteCommand!.Execute(e.DataItem);
        }
    }

    /// <summary>
    /// Fires whenever an item comes to view.
    /// </summary>
    /// <param name="sender">ListView_QueryItemSize event sender.</param>
    /// <param name="e">Represent ListView_QueryItemSize event args.</param>
    private void ListView_QueryItemSize(object? sender, QueryItemSizeEventArgs e)
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
        this.ListView.PropertyChanged -= ListView_PropertyChanged;
        this.ListView.QueryItemSize -= ListView_QueryItemSize;
        this.ListView.SwipeEnded -= ListView_SwipeEnded;
        this.ListView = null;
        this.ViewModel = null;
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
