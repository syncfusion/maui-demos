using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfNumericEntry;

public partial class NumericEntryPage : SampleView
{

#if ANDROID || IOS
    NumericEntryPageMobile numericEntryPageMobile;
#else
    NumericEntryPageDesktop numericEntryPageDesktop;
#endif
    public NumericEntryPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericEntryPageMobile = new NumericEntryPageMobile();
        this.Content = numericEntryPageMobile.Content;
        this.OptionView = numericEntryPageMobile.OptionView;
#else
        numericEntryPageDesktop = new NumericEntryPageDesktop();
        this.Content = numericEntryPageDesktop.Content;
        this.WidthRequest = numericEntryPageDesktop.WidthRequest;
        this.OptionView = numericEntryPageDesktop.OptionView;
        this.Margin = numericEntryPageDesktop.Margin;
#endif
    }
}