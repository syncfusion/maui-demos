using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfNumericEntry;

public partial class NumericEntryGettingStarted : SampleView
{

#if ANDROID || IOS
    NumericEntryGettingStartedMobile numericEntryGettingStartedMobile;
#else
    NumericEntryGettingStartedDesktop numericEntryGettingStartedDesktop;
#endif
    public NumericEntryGettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericEntryGettingStartedMobile = new NumericEntryGettingStartedMobile();
        this.Content = numericEntryGettingStartedMobile.Content;
        this.OptionView = numericEntryGettingStartedMobile.OptionView;
#else
        numericEntryGettingStartedDesktop = new NumericEntryGettingStartedDesktop();
        this.Content = numericEntryGettingStartedDesktop.Content;
        this.WidthRequest = numericEntryGettingStartedDesktop.WidthRequest;
        this.OptionView = numericEntryGettingStartedDesktop.OptionView;
        this.Margin = numericEntryGettingStartedDesktop.Margin;
#endif
    }
}