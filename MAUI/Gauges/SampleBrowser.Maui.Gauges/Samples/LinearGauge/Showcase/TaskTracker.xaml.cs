using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class TaskTracker : SampleView
{
	public TaskTracker()
	{
		InitializeComponent();
#if MACCATALYST
#else
        if (DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet)
        {
            taskTrackerLayout.WidthRequest = 340;
            taskTrackerLayout.HorizontalOptions = LayoutOptions.Center;
        }
#endif
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        taskTrackerGauge.Handler?.DisconnectHandler();
    }
}