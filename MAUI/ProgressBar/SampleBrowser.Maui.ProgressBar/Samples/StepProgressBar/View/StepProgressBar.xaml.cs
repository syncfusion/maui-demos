namespace SampleBrowser.Maui.ProgressBar.SfStepProgressBar;

using SampleBrowser.Maui.Base;

public partial class StepProgressBar : SampleView
{
	public StepProgressBar()
	{
		InitializeComponent();
#if WINDOWS || MACCATALYST
        OrderTrackingDestopUI orderTrackingDestopUI = new OrderTrackingDestopUI();
        this.Content = orderTrackingDestopUI;
        this.OptionView = orderTrackingDestopUI.OptionView;
#elif ANDROID || IOS
        OrderTrackingMobileUI orderTrackingMobileUI = new OrderTrackingMobileUI();
        this.Content = orderTrackingMobileUI;
        this.OptionView = orderTrackingMobileUI.OptionView;
#endif
    }
}