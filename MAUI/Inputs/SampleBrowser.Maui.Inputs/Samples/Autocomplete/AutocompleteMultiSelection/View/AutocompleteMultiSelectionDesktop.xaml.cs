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

    Syncfusion.Maui.Inputs.SfAutocomplete? autocomplete;

    DataTemplate? itemTemplate;

    ObservableCollection<Applicationlist> Items;
    public AutocompleteMultiSelectionDesktop()
    {
        InitializeComponent();
        Items = new ObservableCollection<Applicationlist>();
        this.BindingContext = applicationViewModel;

        this.SetItemTemplate();
        this.autocomplete = WrapAutocomplete();

        if (autocomplete.SelectedItems != null)
        {
            autocomplete.SelectedItems.Add(applicationViewModel.AppCollection[0]);
        }
        autocomplete.SelectionChanged += OnSelectionChanged;
        autocompletelayout.Children.Add(autocomplete);

        BindableLayout.SetItemsSource(listview, Items);
    }

    private void SetItemTemplate()
    {
        itemTemplate = new DataTemplate(() =>
        {
            var horizontalStackLayout = new HorizontalStackLayout
            {
                Padding = new Thickness(10, 10),
                Spacing = 12
            };

            var image = new Image();
            image.SetBinding(Image.SourceProperty, "AppImage");
            image.WidthRequest = 28;
            image.HeightRequest = 28;

            var label = new Label();
            label.SetBinding(Label.TextProperty, "AppName");
            label.VerticalOptions = LayoutOptions.Center;

            horizontalStackLayout.Children.Add(image);
            horizontalStackLayout.Children.Add(label);

            return horizontalStackLayout;
        });
    }

    private Syncfusion.Maui.Inputs.SfAutocomplete WrapAutocomplete()
    {
        if (this.autocomplete != null)
        {
            this.autocomplete.SelectionChanged -= OnSelectionChanged;
            this.autocomplete = null;
        }
        var wrapAutocomplete = new Syncfusion.Maui.Inputs.SfAutocomplete()
        {
            Placeholder = "Enter App Name",
            PlaceholderColor = Color.FromRgba("#adb2bb"),
            FontSize = 16,
            MaxDropDownHeight = 200,
            DropDownItemHeight = 48,
            TextMemberPath = "AppName",
            DisplayMemberPath = "AppName",
            TextSearchMode = Syncfusion.Maui.Inputs.AutocompleteTextSearchMode.Contains,
            SelectionMode = Syncfusion.Maui.Inputs.AutocompleteSelectionMode.Multiple,
            TokensWrapMode = Syncfusion.Maui.Inputs.AutocompleteTokensWrapMode.Wrap,
            EnableAutoSize = true,
            IsClearButtonVisible = true,
        };
        wrapAutocomplete.ItemTemplate = itemTemplate;
        wrapAutocomplete.ItemsSource = applicationViewModel.AppCollection;
        wrapAutocomplete.SelectionChanged += OnSelectionChanged;
        wrapAutocomplete.SelectedItems?.Add(applicationViewModel.AppCollection[0]);
        this.autocomplete = wrapAutocomplete;
        return wrapAutocomplete;
    }
    private Syncfusion.Maui.Inputs.SfAutocomplete NoneAutocomplete()
    {
        if (this.autocomplete != null)
        {
            this.autocomplete.SelectionChanged -= OnSelectionChanged;
            this.autocomplete = null;
        }

        var noneAutocomplete = new Syncfusion.Maui.Inputs.SfAutocomplete()
        {
            HeightRequest = 35,
            Placeholder = "Enter App Name",
            PlaceholderColor = Color.FromRgba("#adb2bb"),
            FontSize = 16,
            MaxDropDownHeight = 200,
            DropDownItemHeight = 48,
            TextMemberPath = "AppName",
            DisplayMemberPath = "AppName",
            TextSearchMode = Syncfusion.Maui.Inputs.AutocompleteTextSearchMode.Contains,
            SelectionMode = Syncfusion.Maui.Inputs.AutocompleteSelectionMode.Multiple,
            IsClearButtonVisible = true,
            Padding = new(3,0,0,0),
        };
        noneAutocomplete.ItemTemplate = itemTemplate;
        noneAutocomplete.ItemsSource = applicationViewModel.AppCollection;
        noneAutocomplete.SelectionChanged += OnSelectionChanged;
        noneAutocomplete.SelectedItems?.Add(applicationViewModel.AppCollection[0]);
        this.autocomplete = noneAutocomplete;
        return noneAutocomplete;
    }

    private void OnSelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if(this.autocomplete!=null)
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
            InstallButtonsVisible();
        }
    }

    private void ClearButton_Tapped(object sender, TappedEventArgs e)
    {
        if (autocomplete != null && autocomplete.SelectedItems != null)
        {
            autocomplete.SelectedItems.Clear();
            Items.Clear();
            listview.Clear();
            installButtons.IsVisible = false;
        }
    }

    private void InstallButtonsVisible()
    {
        if (autocomplete != null && autocomplete.SelectedItems?.Count > 0)
        {
            installButtons.IsVisible = true;
        }
        else
        {
            installButtons.IsVisible = false;
        }
    }

    private void tokensWrapModeCombobox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (tokensWrapModeCombobox.SelectedIndex)
        {
            case 0:
                autocompletelayout.Clear();
                Items.Clear();
                autocompletelayout.Add(NoneAutocomplete());
                foreach (object item in autocomplete?.SelectedItems!)
                {
                    Items.Add((Applicationlist)item);
                }
                selectedApplications.IsVisible = true;
                selectedApplicationsLabel.IsVisible = true;
                InstallButtonsVisible();
                break;
            case 1:
                autocompletelayout.Clear();
                Items.Clear();
                autocompletelayout.Add(WrapAutocomplete());
                selectedApplications.IsVisible = false;
                selectedApplicationsLabel.IsVisible = false;
                InstallButtonsVisible();
                break;
        }
    }
}