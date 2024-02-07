#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Collections.ObjectModel;
using SampleBrowser.Maui.Inputs.Samples.Autocomplete.AutocompleteMultiSelection.Model;
using SampleBrowser.Maui.Inputs.Samples.Autocomplete.AutocompleteMultiSelection.ViewModel;
using System.Collections;

namespace SampleBrowser.Maui.Inputs.SfAutocomplete;

public partial class AutocompleteMultiSelectionDesktop : SampleView
{
    ApplicationViewModel applicationViewModel = new ApplicationViewModel();

    ObservableCollection<Applicationlist> Items;
    public AutocompleteMultiSelectionDesktop()
    {
        InitializeComponent();
        Items = new ObservableCollection<Applicationlist>();
        this.BindingContext = applicationViewModel;
        if (autocomplete.SelectedItems != null)
        {
            autocomplete.SelectedItems?.Add(applicationViewModel.AppCollection[0]);
           
        }
        BindableLayout.SetItemsSource(listview, Items);
    }

    private void OnSelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (e.AddedItems != null && (e.AddedItems is ICollection && ((ICollection)e.AddedItems).Count > 0))
        {
            foreach (object item in e.AddedItems)
            {
                Items.Add((Applicationlist)item);
            }
        }
        else if (e.RemovedItems != null && (e.RemovedItems is ICollection && ((ICollection)e.RemovedItems).Count > 0))
        {
            foreach (object item in e.RemovedItems)
            {
                Items.Remove((Applicationlist)item);
            }
        }
        BindableLayout.SetItemsSource(listview, Items);
        if (autocomplete.SelectedItems?.Count > 0)
        {
            installButtons.IsVisible = true;
        }
        else
        {
            installButtons.IsVisible = false;
        }
    }

    private void ClearButton_Tapped(object sender, TappedEventArgs e)
    {
        if (autocomplete.SelectedItems != null)
        {
            autocomplete.SelectedItems.Clear();
            Items.Clear();
            listview.Clear();
            installButtons.IsVisible = false;
        }
    }
}