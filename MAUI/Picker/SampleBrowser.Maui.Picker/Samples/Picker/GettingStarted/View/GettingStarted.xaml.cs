#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Picker;

namespace SampleBrowser.Maui.Picker.SfPicker;

public partial class GettingStarted : SampleView
{
    /// <summary>
    /// Check the application theme is light or dark.
    /// </summary>
    private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

    /// <summary>
    /// The view model instance.
    /// </summary>
    ViewModel? viewModel;

    public GettingStarted()
    {
        InitializeComponent();
        this.viewModel = new ViewModel();
        List<string> list = new List<string>()
        {
            "Australia",
            "China",
            "Japan",
            "Germany",
            "India",
            "USA",
            "France",
            "UK",
            "UAE",
            "Egypt",
            "Argentina",
        };

#if MACCATALYST
        this.Picker1.Columns.Add(new PickerColumn()
        {
            ItemsSource = list,
            SelectedItem = this.viewModel.SelectedItem,         
        });
#elif IOS
        this.Picker3.Columns.Add(new PickerColumn()
        {
            ItemsSource = list,
            SelectedItem = this.viewModel.SelectedItem,         
        });
#elif ANDROID
        this.Picker2.Columns.Add(new PickerColumn()
        {
            ItemsSource = list,
            SelectedItem = this.viewModel.SelectedItem,         
        });
#else

        this.Picker.Columns.Add(new PickerColumn()
        {
            ItemsSource = list,
            SelectedItem = this.viewModel.SelectedItem,         
        });
#endif
    }

    /// <summary>
    /// Method to update the change log based on the selected index.
    /// </summary>
    /// <param name="sender">The Sender.</param>
    /// <param name="e">The event args.</param>
    private void OnPickerSelectionChanged(object sender, PickerSelectionChangedEventArgs e)
    {
        Syncfusion.Maui.Picker.SfPicker? picker = sender as Syncfusion.Maui.Picker.SfPicker;
        if (picker == null || picker.Columns == null || picker.Columns.Count == 0)
        {
            return;
        }

#if IOS || MACCATALYST
        if (picker == this.Picker || picker == this.Picker2)
        {
            return;
        }
#else
        if (picker == this.Picker1 || picker == this.Picker3)
        {
            return;
        }
#endif

        PickerColumn selectedColumn = picker.Columns[0];
        string selectedIndex = selectedColumn.SelectedIndex.ToString();
        string countryName = "Australia";

        switch (selectedIndex)
        {
            case "0":
                countryName = "Australia";
                break;
            case "1":
                countryName = "China";
                break;
            case "2":
                countryName = "Japan";
                break;
            case "3":
                countryName = "Germany";
                break;
            case "4":
                countryName = "India";
                break;
            case "5":
                countryName = "USA";
                break;
            case "6":
                countryName = "France";
                break;
            case "7":
                countryName = "UK";
                break;
            case "8":
                countryName = "UAE";
                break;
            case "9":
                countryName = "Egypt";
                break;
            case "10":
                countryName = "Argentina";
                break;
        }

        if (labelStack == null || labelStack1 == null)
        {
            return;
        }

        string labelText = countryName + " has been selected";
        bool isNeedToAdd = true;
#if WINDOWS || MACCATALYST
        if (labelStack.Children.Count > 0)
        {
            var view = labelStack.Children[labelStack.Children.Count - 1];
            if (view is Label label && label.Text == labelText)
            {
                isNeedToAdd = false;
            }
        }

        if (!isNeedToAdd)
        {
            return;
        }

        labelStack.Add(new Label()
        {
            Text = labelText,
            VerticalOptions = LayoutOptions.Center,
        });

        if (labelStack.Parent is ScrollView scrollView)
        {
            scrollView.PropertyChanged += OnScrollViewPropertyChanged;
        }
#else
        if (labelStack1.Children.Count > 0)
        {
            var view = labelStack1.Children[labelStack.Children.Count - 1];
            if (view is Label label && label.Text == labelText)
            {
                isNeedToAdd = false;
            }
        }

        if (!isNeedToAdd)
        {
            return;
        }

        labelStack1.Add(new Label()
        {
            Text = labelText,
            VerticalOptions = LayoutOptions.Center,
        });

        if (labelStack1.Parent is ScrollView scrollView)
        {
            scrollView.PropertyChanged += OnScrollViewPropertyChanged;
        }
#endif
    }

    private void OnScrollViewPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ScrollView.ContentSize) && sender is ScrollView scrollView)
        {
            scrollView.PropertyChanged -= OnScrollViewPropertyChanged;
            scrollView.ScrollToAsync(0, scrollView.ContentSize.Height - scrollView.Height, false);
        }
    }

    private void OnClearButtonClicked(object sender, EventArgs e)
    {
#if WINDOWS || MACCATALYST
        labelStack.Children.Clear();
#else
        labelStack1.Children.Clear();
#endif
    }
}