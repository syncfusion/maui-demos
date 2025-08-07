#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.PullToRefresh;

namespace SampleBrowser.Maui.PullToRefresh;

/// <summary>
/// Base generic class for user-defined behaviors that can respond to conditions and events.
/// </summary>
public class PullToRefreshViewBehavior : Behavior<SampleView>
{
    private SfListView? listView;
    private SfListView? windListView;
    private Syncfusion.Maui.PullToRefresh.SfPullToRefresh? pullToRefresh;
    private SfComboBox? transitionType;
    private PullToRefreshViewModel? viewModel;

    /// <summary>
    /// Used to Update weather data value
    /// </summary>
    internal void UpdateData()
    {
        var weatherType = this.viewModel!.Data!.WeatherType;
        this.viewModel.Data.Temperature = this.viewModel.UpdateTemperature(weatherType!);
    }

    /// <summary>
    /// You can override this method while View was detached from window
    /// </summary>
    /// <param name="bindable">Sample View typed parameter named as bindable</param>
    protected override void OnAttachedTo(SampleView bindable)
    {
        this.viewModel = new PullToRefreshViewModel();
        bindable.BindingContext = this.viewModel;
        this.pullToRefresh = bindable.FindByName<Syncfusion.Maui.PullToRefresh.SfPullToRefresh>("pullToRefresh");
        this.listView = bindable.FindByName<SfListView>("listView");
        this.windListView = bindable.FindByName<SfListView>("windList");
        this.transitionType = bindable.FindByName<SfComboBox>("comboBox");
        this.pullToRefresh.PullingThreshold = 100;
        this.listView.BindingContext = this.viewModel;
        this.listView.ItemsSource = this.viewModel.SelectedData;
        this.listView.SelectedItem = (this.listView.BindingContext as PullToRefreshViewModel)?.SelectedData![0];
        this.pullToRefresh.Refreshing += this.PullToRefresh_Refreshing;
        this.listView.SelectionChanging += this.ListView_SelectionChanging;
        this.transitionType.SelectionChanged += this.OnSelectionChanged;
        base.OnAttachedTo(bindable);
    }

    /// <summary>
    /// You can override this method while View was detached from window
    /// </summary>
    /// <param name="bindAble">SampleView typed parameter named as bindAble</param>
    protected override void OnDetachingFrom(SampleView bindAble)
    {
        this.pullToRefresh!.Refreshing -= this.PullToRefresh_Refreshing;
        this.listView!.SelectionChanging -= this.ListView_SelectionChanging;
        this.transitionType!.SelectionChanged -= this.OnSelectionChanged;
        this.pullToRefresh = null;
        this.viewModel = null;
        this.listView = null;
        this.transitionType = null;
        base.OnDetachingFrom(bindAble);
    }

    /// <summary>
    /// Fired when selected index gets changed
    /// </summary>
    /// <param name="sender">OnSelectionChanged event sender</param>
    /// <param name="e">OnSelectionChanged event args e</param>
    private void OnSelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        this.pullToRefresh!.TransitionMode = this.transitionType!.SelectedIndex == 0 ? PullToRefreshTransitionType.Push : PullToRefreshTransitionType.SlideOnTop;
    }

    /// <summary>
    /// Triggers while pulltorefresh view was refreshing
    /// </summary>
    /// <param name="sender">PullToRefresh_Refreshing sender</param>
    /// <param name="args">PullToRefresh_Refreshing event args e</param>
    private void PullToRefresh_Refreshing(object? sender, EventArgs args)
    {
        this.pullToRefresh!.IsRefreshing = true;
        Dispatcher.StartTimer(
            new TimeSpan( 0, 0, 0, 1, 3000), () =>
            {
                this.UpdateData();
                this.pullToRefresh.IsRefreshing = false;
                return false;
            });
    }

    /// <summary>
    /// Triggers while list view selection was changing
    /// </summary>
    /// <param name="sender">ListView_SelectionChanging event sender</param>
    /// <param name="e">ListView_SelectionChanging event args e</param>
    private void ListView_SelectionChanging(object? sender, ItemSelectionChangingEventArgs e)
    {
        if (e.AddedItems!.Count == 0 || this.pullToRefresh!.IsRefreshing)
        {
            e.Cancel = true;
        }
        else if (e.AddedItems!.Count > 0)
        {
            this.viewModel!.Data = (WeatherData)e.AddedItems[0];
        }
    }
}