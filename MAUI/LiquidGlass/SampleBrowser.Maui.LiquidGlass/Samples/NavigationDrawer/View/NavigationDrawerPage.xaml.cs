using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfNavigationDrawer;

public partial class NavigationDrawerPage : SampleView
{
#if ANDROID || IOS
    NavigationDrawerPageMobile navigationDrawerPageMobile;
#elif WINDOWS || MACCATALYST
    NavigationDrawerPageDesktop navigationDrawerPageDesktop;
#endif
    public NavigationDrawerPage()
	{
		InitializeComponent();
#if ANDROID || IOS     
        navigationDrawerPageMobile = new NavigationDrawerPageMobile();
        this.Content = navigationDrawerPageMobile.Content;
        this.OptionView = navigationDrawerPageMobile.OptionView;
#elif WINDOWS || MACCATALYST
        navigationDrawerPageDesktop = new NavigationDrawerPageDesktop();
        this.Content = navigationDrawerPageDesktop.Content;
        this.OptionView = navigationDrawerPageDesktop.OptionView;
#endif
    }
}