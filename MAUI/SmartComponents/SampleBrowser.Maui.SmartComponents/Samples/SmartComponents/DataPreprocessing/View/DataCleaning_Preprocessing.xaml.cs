#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public partial class DataCleaning_Preprocessing : SampleView
{
    public DataCleaning_Preprocessing()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        Chart.Handler?.DisconnectHandler();
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        viewModel.IsBusy = true;
        await Task.Delay(2000);
        await viewModel.LoadCleanedDataAsync();
    }
}