#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxDropdownFeatures.ViewModel;

namespace SampleBrowser.Maui.Inputs.SfComboBox;

public partial class ComboBoxDropdownMobile : SampleView
{
	public ComboBoxDropdownMobile()
	{
		InitializeComponent();
        var viewModel = new DropDownViewModel();
        this.Content.BindingContext = viewModel;
    }

    private void DropDownPlacement_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (dropDownPlacement.SelectedIndex)
        {
            case 0:
                comboboxDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                comboboxDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                comboboxDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                comboBoxLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                break;

            case 1:
                comboboxDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                comboboxDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                comboboxDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                comboBoxLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                break;

            case 2:
                comboboxDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                comboboxDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                comboboxDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                comboBoxLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                break;

            case 3:
                comboboxDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                comboboxDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                comboboxDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                comboBoxLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                break;
        }
    }

    private void DropdownHighlight_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (dropDownHighlight.SelectedIndex)
        {
            case 0:
                comboboxDropdownHighlight.TextHighlightMode = Syncfusion.Maui.Inputs.OccurrenceMode.FirstOccurrence;
                break;

            case 1:
                comboboxDropdownHighlight.TextHighlightMode = Syncfusion.Maui.Inputs.OccurrenceMode.MultipleOccurrence;
                break;

            case 2:
                comboboxDropdownHighlight.TextHighlightMode = Syncfusion.Maui.Inputs.OccurrenceMode.None;
                break;
        }
    }

    private void headerView_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            comboboxDropdownHeaderFooter.ShowDropdownHeaderView = true;
        }
        else
        {
            comboboxDropdownHeaderFooter.ShowDropdownHeaderView = false;
        }
    }

    private void footerView_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            comboboxDropdownHeaderFooter.ShowDropdownFooterView = true;
        }
        else
        {
            comboboxDropdownHeaderFooter.ShowDropdownFooterView = false;
        }
    }

    private void hasShadow_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            comboboxDropdownHighlight.IsDropDownShadowVisible = true;
            comboboxDropdownCustomization.IsDropDownShadowVisible = true;
            comboboxDropdownHeaderFooter.IsDropDownShadowVisible = true;
            comboBoxLoadMore.IsDropDownShadowVisible = true;
        }
        else
        {
            comboboxDropdownHighlight.IsDropDownShadowVisible = false;
            comboboxDropdownCustomization.IsDropDownShadowVisible = false;
            comboboxDropdownHeaderFooter.IsDropDownShadowVisible = false;
            comboBoxLoadMore.IsDropDownShadowVisible = false;
        }
    }

    private void DropdownBackground_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (dropDownBackground.SelectedIndex)
        {
            case 0:
                comboboxDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#00513C") : Color.FromArgb("#A6F2D2");
                break;

            case 1:
                comboboxDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#264B5C") : Color.FromArgb("#C2E8FD");
                break;

            case 2:
                comboboxDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#404944") : Color.FromArgb("#DBE5DE");
                break;

            case 3:
                comboboxDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#6E334D") : Color.FromArgb("#FFD9E5");
                break;

            case 4:
                comboboxDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#C7CC79") : Color.FromArgb("#C7CC79");
                break;
        }
    }

    private void maximumSuggestion_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (maximumSuggestion.SelectedIndex)
        {
            case 0:
                comboBoxLoadMore.MaximumSuggestion = 2;
                break;

            case 1:
                comboBoxLoadMore.MaximumSuggestion = 3;
                break;

            case 2:
                comboBoxLoadMore.MaximumSuggestion = 4;
                break;

            case 3:
                comboBoxLoadMore.MaximumSuggestion = 5;
                break;
        }
    }
}