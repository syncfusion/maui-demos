using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.Inputs.SfNumericEntry;

public partial class NumericEntryCultureAndFormatting : SampleView
{

#if ANDROID || IOS
    NumericEntryCultureAndFormattingMobile numericEntryCultureAndFormattingMobile;
#else
    NumericEntryCultureAndFormattingDesktop numericEntryCultureAndFormattingDesktop;
#endif
    public NumericEntryCultureAndFormatting()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericEntryCultureAndFormattingMobile = new NumericEntryCultureAndFormattingMobile();
        this.Content = numericEntryCultureAndFormattingMobile.Content;
        this.OptionView = numericEntryCultureAndFormattingMobile.OptionView;
#else
        numericEntryCultureAndFormattingDesktop = new NumericEntryCultureAndFormattingDesktop();
        this.Content = numericEntryCultureAndFormattingDesktop.Content;
        this.WidthRequest = numericEntryCultureAndFormattingDesktop.WidthRequest;
        this.OptionView = numericEntryCultureAndFormattingDesktop.OptionView;
        this.Margin = numericEntryCultureAndFormattingDesktop.Margin;
#endif
    }
}