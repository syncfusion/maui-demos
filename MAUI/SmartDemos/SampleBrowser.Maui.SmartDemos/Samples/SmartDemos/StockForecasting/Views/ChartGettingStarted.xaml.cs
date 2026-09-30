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