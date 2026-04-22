#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using System.Collections.ObjectModel; 

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class CheckBoxSelectorColumn : SampleView
{
    private CheckBoxSelectorBehavior? behavior;


    public CheckBoxSelectorColumn()
    {
        InitializeComponent();

        // Initialize and attach the behavior
        behavior = new CheckBoxSelectorBehavior();
        this.Behaviors.Add(behavior);
    }

    /// <summary>
    /// Handles header checkbox state changes (Select All)
    /// </summary>
    private void checkBox_StateChanged(object? sender, StateChangedEventArgs e)
    {
        if (sender is null)
            return;

        behavior?.OnHeaderCheckBoxStateChanged(sender, e);
    }

    /// <summary>
    /// Handles individual row checkbox state changes
    /// </summary>
    private void SfCheckBox_StateChanged(object? sender, StateChangedEventArgs e)
    {
        if (sender is null)
            return;

        behavior?.OnRowCheckBoxStateChanged(sender, e);
    }

    private void SfCheckBox_StateChanging(object? sender, StateChangingEventArgs e)
    {
#if MACCATALYST || IOS
        if (sender is null)
            return;

        behavior?.OnRowCheckBoxStateChanging(sender, e);
#endif
    }
}
