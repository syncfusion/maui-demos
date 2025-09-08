#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Inputs.Samples.Autocomplete.AutocompleteDropdownFeatures.ViewModel;

namespace SampleBrowser.Maui.Inputs.SfAutocomplete;

public partial class AutocompleteDropdownDesktop : SampleView
{
	public AutocompleteDropdownDesktop()
	{
		InitializeComponent();
        var viewModel = new DropDownViewModel();
        autocompleteDropdownHighlight.SelectedItem = viewModel.Islands.FirstOrDefault();
        autocompleteDropdownCustomization.SelectedItem = viewModel.Continents.FirstOrDefault();
        autocompleteDropdownHeaderFooter.SelectedItem = viewModel.Wonders.FirstOrDefault();
        autocompleteLoadMore.SelectedItem = viewModel.Countries.FirstOrDefault();
    }

    private void DropDownPlacement_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
		switch(dropDownPlacement.SelectedIndex)
		{
			case 0:
                autocompleteDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                autocompleteDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                autocompleteDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                autocompleteLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Auto;
                break;

			case 1:
                autocompleteDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                autocompleteDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                autocompleteDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                autocompleteLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Bottom;
                break;

            case 2:
                autocompleteDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                autocompleteDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                autocompleteDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                autocompleteLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.None;
                break;

            case 3:
                autocompleteDropdownHighlight.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                autocompleteDropdownCustomization.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                autocompleteDropdownHeaderFooter.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                autocompleteLoadMore.DropDownPlacement = Syncfusion.Maui.Core.DropDownPlacement.Top;
                break;
        }
    }

    private void DropdownHighlight_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch(dropDownHighlight.SelectedIndex)
        {
            case 0:
                autocompleteDropdownHighlight.TextHighlightMode = Syncfusion.Maui.Inputs.OccurrenceMode.FirstOccurrence;
                break;

            case 1:
                autocompleteDropdownHighlight.TextHighlightMode = Syncfusion.Maui.Inputs.OccurrenceMode.MultipleOccurrence;
                break;

            case 2:
                autocompleteDropdownHighlight.TextHighlightMode = Syncfusion.Maui.Inputs.OccurrenceMode.None;
                break;
        }
    }

    private void headerView_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(e.Value)
        {
            autocompleteDropdownHeaderFooter.ShowDropdownHeaderView = true;
        }
        else
        {
            autocompleteDropdownHeaderFooter.ShowDropdownHeaderView = false;
        }
    }

    private void footerView_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            autocompleteDropdownHeaderFooter.ShowDropdownFooterView = true;
        }
        else
        {
            autocompleteDropdownHeaderFooter.ShowDropdownFooterView = false;
        }
    }

    private void hasShadow_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            autocompleteDropdownHighlight.IsDropDownShadowVisible = true;
            autocompleteDropdownCustomization.IsDropDownShadowVisible = true;
            autocompleteDropdownHeaderFooter.IsDropDownShadowVisible = true;
            autocompleteLoadMore.IsDropDownShadowVisible = true;
        }
        else
        {
            autocompleteDropdownHighlight.IsDropDownShadowVisible = false;
            autocompleteDropdownCustomization.IsDropDownShadowVisible = false;
            autocompleteDropdownHeaderFooter.IsDropDownShadowVisible = false;
            autocompleteLoadMore.IsDropDownShadowVisible = false;
        }
    }

    private void DropdownBackground_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (dropDownBackground.SelectedIndex)
        {
            case 0:
                autocompleteDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#00513C") : Color.FromArgb("#A6F2D2");
                break;

            case 1:
                autocompleteDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#264B5C") : Color.FromArgb("#C2E8FD");
                break;

            case 2:
                autocompleteDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#404944") : Color.FromArgb("#DBE5DE");
                break;

            case 3:
                autocompleteDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#6E334D") : Color.FromArgb("#FFD9E5");
                break;

            case 4:
                autocompleteDropdownCustomization.DropDownBackground = Application.Current!.UserAppTheme == AppTheme.Dark ? Color.FromArgb("#C7CC79") : Color.FromArgb("#C7CC79");
                break;
        }
    }

    private void maximumSuggestion_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (maximumSuggestion.SelectedIndex)
        {
            case 0:
                autocompleteLoadMore.MaximumSuggestion = 2;
                break;

            case 1:
                autocompleteLoadMore.MaximumSuggestion = 3;
                break;

            case 2:
                autocompleteLoadMore.MaximumSuggestion = 4;
                break;

            case 3:
                autocompleteLoadMore.MaximumSuggestion = 5;
                break;
        }
    }
}