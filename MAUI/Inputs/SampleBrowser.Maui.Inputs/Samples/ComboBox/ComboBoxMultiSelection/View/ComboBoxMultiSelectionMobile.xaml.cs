#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.Model;
using SampleBrowser.Maui.Inputs.Samples.ComboBox.ComboBoxMultiSelection.ViewModel;
using Syncfusion.Maui.Inputs;
using System.Collections;
using System.Collections.ObjectModel;
namespace SampleBrowser.Maui.Inputs.SfComboBox;

public partial class ComboBoxMultiSelectionMobile : SampleView
{
    ApplicationViewModel applicationViewModel = new ApplicationViewModel();


    Syncfusion.Maui.Inputs.SfComboBox? comboBox;

    DataTemplate? itemTemplate;

    ObservableCollection<Applicationlist> Items;
    public ComboBoxMultiSelectionMobile()
	{
		InitializeComponent();
        Items = new ObservableCollection<Applicationlist>();
        this.BindingContext = applicationViewModel;
        this.SetItemTemplate();
        this.comboBox = WrapComboBox();

        if (comboBox.SelectedItems != null)
        {
            comboBox.SelectedItems.Add(applicationViewModel.AppCollection[0]);
        }
        comboBox.SelectionChanged += OnSelectionChanged;
        comboBoxLayout.Children.Add(comboBox);
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

    private Syncfusion.Maui.Inputs.SfComboBox WrapComboBox()
    {
        if (this.comboBox != null)
        {
            this.comboBox.SelectionChanged -= OnSelectionChanged;
            this.comboBox = null;
        }
        var wrapComboBox = new Syncfusion.Maui.Inputs.SfComboBox()
        {
            Placeholder = "Enter App Name",
            PlaceholderColor = Color.FromRgba("#adb2bb"),
            FontSize = 16,
            MaxDropDownHeight = 200,
            TextMemberPath = "AppName",
            DisplayMemberPath = "AppName",
            TextSearchMode = Syncfusion.Maui.Inputs.ComboBoxTextSearchMode.Contains,
            SelectionMode = Syncfusion.Maui.Inputs.ComboBoxSelectionMode.Multiple,
            MultiSelectionDisplayMode = ComboBoxMultiSelectionDisplayMode.Token,
            TokensWrapMode = Syncfusion.Maui.Inputs.ComboBoxTokensWrapMode.Wrap,
            EnableAutoSize = true,
            IsClearButtonVisible = true,
        };
        wrapComboBox.ItemTemplate = itemTemplate;
        wrapComboBox.ItemsSource = applicationViewModel.AppCollection;
        wrapComboBox.SelectionChanged += OnSelectionChanged;
        wrapComboBox.SelectedItems?.Add(applicationViewModel.AppCollection[0]);
        wrapComboBox.SelectedItems?.Add(applicationViewModel.AppCollection[1]);
        this.comboBox = wrapComboBox;
        return wrapComboBox;
    }
    private Syncfusion.Maui.Inputs.SfComboBox NoneComboBox()
    {
        if (this.comboBox != null)
        {
            this.comboBox.SelectionChanged -= OnSelectionChanged;
            this.comboBox = null;
        }

        var noneComboBox = new Syncfusion.Maui.Inputs.SfComboBox()
        {
            Placeholder = "Enter App Name",
            PlaceholderColor = Color.FromRgba("#adb2bb"),
            FontSize = 16,
            MaxDropDownHeight = 200,
            TextMemberPath = "AppName",
            DisplayMemberPath = "AppName",
            TextSearchMode = Syncfusion.Maui.Inputs.ComboBoxTextSearchMode.Contains,
            SelectionMode = Syncfusion.Maui.Inputs.ComboBoxSelectionMode.Multiple,
            MultiSelectionDisplayMode = ComboBoxMultiSelectionDisplayMode.Token,
            IsClearButtonVisible = true,
            Padding = new(4, 0, 0, 0),
        };
#if ANDROID
        noneComboBox.HeightRequest = 50;
#endif
        noneComboBox.ItemTemplate = itemTemplate;
        noneComboBox.ItemsSource = applicationViewModel.AppCollection;
        noneComboBox.SelectionChanged += OnSelectionChanged;
        noneComboBox.SelectedItems?.Add(applicationViewModel.AppCollection[0]);
        noneComboBox.SelectedItems?.Add(applicationViewModel.AppCollection[1]);
        this.comboBox = noneComboBox;
        return noneComboBox;
    }


    private Syncfusion.Maui.Inputs.SfComboBox DelimiterComboBox()
    {
        if (this.comboBox != null)
        {
            this.comboBox.SelectionChanged -= OnSelectionChanged;
            this.comboBox = null;
        }

        var delimiterComboBox = new Syncfusion.Maui.Inputs.SfComboBox()
        {
            Placeholder = "Enter App Name",
            PlaceholderColor = Color.FromRgba("#adb2bb"),
            FontSize = 16,
            MaxDropDownHeight = 200,
            TextMemberPath = "AppName",
            DisplayMemberPath = "AppName",
            TextSearchMode = Syncfusion.Maui.Inputs.ComboBoxTextSearchMode.Contains,
            SelectionMode = Syncfusion.Maui.Inputs.ComboBoxSelectionMode.Multiple,
            MultiSelectionDisplayMode = ComboBoxMultiSelectionDisplayMode.Delimiter,
            IsClearButtonVisible = true,
            Padding = new(4, 0, 0, 0),
        };
#if ANDROID
        delimiterComboBox.HeightRequest = 50;
#endif
        delimiterComboBox.ItemTemplate = itemTemplate;
        delimiterComboBox.ItemsSource = applicationViewModel.AppCollection;
        delimiterComboBox.SelectionChanged += OnSelectionChanged;
        delimiterComboBox.SelectedItems?.Add(applicationViewModel.AppCollection[0]);
        delimiterComboBox.SelectedItems?.Add(applicationViewModel.AppCollection[1]);
        this.comboBox = delimiterComboBox;
        return delimiterComboBox;
    }


    private void OnSelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (comboBox != null)
        {
            if (e.AddedItems != null && (e.AddedItems is ICollection && ((ICollection)e.AddedItems).Count > 0))
            {
                foreach (object item in e.AddedItems)
                {
                    if (!Items.Contains(item))
                        Items.Add((Applicationlist)item);
                }
            }
            else if (e.RemovedItems != null && (e.RemovedItems is ICollection && ((ICollection)e.RemovedItems).Count > 0))
            {
                foreach (object item in e.RemovedItems)
                {
                    if (Items.Contains(item))
                        Items.Remove((Applicationlist)item);
                }
            }
            if (comboBox.SelectedItems != null && comboBox.SelectedItems.Count == 0 && Items.Count > 0)
            {
                Items.Clear();
            }
            BindableLayout.SetItemsSource(listview, Items);
            InstallButtonsVisible();
        }
    }

    private void ClearButton_Tapped(object sender, TappedEventArgs e)
    {
        if (comboBox != null && comboBox.SelectedItems != null)
        {
            comboBox.SelectedItems.Clear();
            Items.Clear();
            listview.Clear();
            installButtons.IsVisible = false;
        }
    }

    private void InstallButtonsVisible()
    {
        if (comboBox != null && comboBox.SelectedItems?.Count > 0)
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
                comboBoxLayout.Clear();
                Items.Clear();
                comboBoxLayout.Add(NoneComboBox());
                foreach (object item in comboBox?.SelectedItems!)
                {
                    Items.Add((Applicationlist)item);
                }
                selectedApplications.IsVisible = true;
                selectedApplicationsLabel.IsVisible = true;
                InstallButtonsVisible();
                break;
            case 1:
                comboBoxLayout.Clear();
                Items.Clear();
                comboBoxLayout.Add(WrapComboBox());
                selectedApplications.IsVisible = false;
                selectedApplicationsLabel.IsVisible = false;
                InstallButtonsVisible();
                break;
        }
    }

    private void delimiterText_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (comboBox != null)
        {
            switch (delimiterText.SelectedIndex)
            {
                case 0:
                    comboBox.DelimiterText = ":";
                    break;
                case 1:
                    comboBox.DelimiterText = ",";
                    break;
                case 2:
                    comboBox.DelimiterText = "/";
                    break;
                case 3:
                    comboBox.DelimiterText = "|";
                    break;
            }
        }
    }

    private void displayModeCombobox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (comboBox != null)
        {
            switch (displayModeCombobox.SelectedIndex)
            {
                case 0:
                    comboBoxLayout.Clear();
                    Items.Clear();
                    comboBoxLayout.Add(WrapComboBox());
                    selectedApplications.IsVisible = false;
                    selectedApplicationsLabel.IsVisible = false;
                    InstallButtonsVisible();
                    delimiterText.IsEnabled = false;
                    delimiterTextLabel.IsEnabled = false;
                    tokensWrapModeCombobox.IsEnabled = true;
                    tokensWrapModeComboboxLabel.IsEnabled = true;
                    break;
                case 1:
                    comboBoxLayout.Clear();
                    Items.Clear();
                    comboBoxLayout.Add(DelimiterComboBox());
                    foreach (object item in comboBox?.SelectedItems!)
                    {
                        Items.Add((Applicationlist)item);
                    }
                    selectedApplications.IsVisible = true;
                    selectedApplicationsLabel.IsVisible = true;
                    InstallButtonsVisible();
                    tokensWrapModeCombobox.IsEnabled = false;
                    tokensWrapModeComboboxLabel.IsEnabled = false;
                    delimiterText.IsEnabled = true;
                    delimiterTextLabel.IsEnabled = true;
                    break;
            }
        }
    }

}