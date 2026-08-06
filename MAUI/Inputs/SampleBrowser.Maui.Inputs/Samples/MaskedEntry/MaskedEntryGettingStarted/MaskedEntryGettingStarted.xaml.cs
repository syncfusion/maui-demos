using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfMaskedEntry;

public partial class MaskedEntryGettingStarted : SampleView
{
    
#if ANDROID || IOS
    MaskedEntryGettingStartedMobile maskedEntryGettingStartedMobile;
#else
    MaskedEntryGettingStartedDesktop maskedEntryGettingStartedDesktop;
#endif
    public MaskedEntryGettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        maskedEntryGettingStartedMobile = new MaskedEntryGettingStartedMobile();
        this.Content = maskedEntryGettingStartedMobile.Content;
        this.OptionView = maskedEntryGettingStartedMobile.OptionView;
#else
        maskedEntryGettingStartedDesktop = new MaskedEntryGettingStartedDesktop();
        this.Content = maskedEntryGettingStartedDesktop.Content;
        this.WidthRequest = maskedEntryGettingStartedDesktop.WidthRequest;
        this.OptionView = maskedEntryGettingStartedDesktop.OptionView;
        this.Margin = maskedEntryGettingStartedDesktop.Margin;
#endif
    }
}