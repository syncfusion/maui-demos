#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartDemos.SmartDemos;

using SampleBrowser.Maui.Base;

public partial class ChartGettingStarted : SampleView
{
	public ChartGettingStarted()
	{
#if ANDROID || IOS
        this.Content = new StockAndroid();
#else
            this.Content = new StockSystem();
#endif
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        var sampleView = this.Content as SampleView;
        if (sampleView != null)
        {
            sampleView.OnAppearing();
        }

    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        var sampleView = this.Content as SampleView;
        if (sampleView != null)
        {
            sampleView.OnDisappearing();
        }
    }
}