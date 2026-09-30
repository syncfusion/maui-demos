using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.NumericUpDown;

public partial class NumericUpDownGettingStarted : SampleView
{

#if ANDROID || IOS
    NumericUpDownGettingStartedMobile numericUpDownGettingStartedMobile;
#else
    NumericUpDownGettingStartedDesktop numericUpDownGettingStartedDesktop;
#endif
    public NumericUpDownGettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericUpDownGettingStartedMobile = new NumericUpDownGettingStartedMobile();
        this.Content = numericUpDownGettingStartedMobile.Content;
        this.OptionView = numericUpDownGettingStartedMobile.OptionView;
#else
        numericUpDownGettingStartedDesktop = new NumericUpDownGettingStartedDesktop();
        this.Content = numericUpDownGettingStartedDesktop.Content;
        this.WidthRequest = numericUpDownGettingStartedDesktop.WidthRequest;
        this.OptionView = numericUpDownGettingStartedDesktop.OptionView;
        this.Margin = numericUpDownGettingStartedDesktop.Margin;
#endif
    }
}