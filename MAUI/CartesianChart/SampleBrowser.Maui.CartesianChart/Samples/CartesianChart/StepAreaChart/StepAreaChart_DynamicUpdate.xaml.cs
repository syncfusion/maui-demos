#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart;

public partial class StepAreaChart_DynamicUpdate : SampleView
{
	public StepAreaChart_DynamicUpdate()
	{
        InitializeComponent();
        if (!(BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile))
            viewModel1.StartTimer();
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        if (BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile)
        {
            viewModel1.StopTimer();
            viewModel1.StartTimer();
        }
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        if (viewModel1 != null)
            viewModel1.StopTimer();

        Chart1.Handler?.DisconnectHandler();
    }
}