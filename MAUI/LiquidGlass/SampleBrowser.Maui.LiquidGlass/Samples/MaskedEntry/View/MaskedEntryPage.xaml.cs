using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfMaskedEntry;

public partial class MaskedEntryPage : SampleView
{
    
#if ANDROID || IOS
    MaskedEntryPageMobile maskedEntryGettingStartedMobile;
#else
    MaskedEntryPageDesktop maskedEntryGettingStartedDesktop;
#endif
    public MaskedEntryPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        maskedEntryGettingStartedMobile = new MaskedEntryPageMobile();
        this.Content = maskedEntryGettingStartedMobile.Content;
        this.OptionView = maskedEntryGettingStartedMobile.OptionView;
#else
        maskedEntryGettingStartedDesktop = new MaskedEntryPageDesktop();
        this.Content = maskedEntryGettingStartedDesktop.Content;
        this.WidthRequest = maskedEntryGettingStartedDesktop.WidthRequest;
        this.OptionView = maskedEntryGettingStartedDesktop.OptionView;
        this.Margin = maskedEntryGettingStartedDesktop.Margin;
#endif
    }
}