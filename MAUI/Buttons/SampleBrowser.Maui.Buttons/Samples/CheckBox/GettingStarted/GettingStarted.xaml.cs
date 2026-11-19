using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class GettingStarted : SampleView
{
#if ANDROID || IOS
    GettingStartedMobile gettingStartedMobile;
#else
    GettingStartedDesktop gettingStartedDesktop;
#endif
    public GettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        gettingStartedMobile = new GettingStartedMobile();
        this.Content = gettingStartedMobile.Content;
        this.OptionView = gettingStartedMobile.OptionView;
#else
        gettingStartedDesktop = new GettingStartedDesktop();
        this.Content = gettingStartedDesktop.Content;       
        this.OptionView = gettingStartedDesktop.OptionView;  
        this.WidthRequest = gettingStartedDesktop.WidthRequest;
#endif
    }
}