using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.BusyIndicator.SfBusyIndicator;

public partial class AnimationType : SampleView
{
#if ANDROID || IOS
    AnimationTypeMobile animationTypeMobile;
#else
AnimationTypeDesktop animationTypeDesktop;
#endif
    public AnimationType()
    {
        InitializeComponent();
#if ANDROID || IOS
        animationTypeMobile = new AnimationTypeMobile();
        this.Content = animationTypeMobile.Content;
        this.OptionView = animationTypeMobile.OptionView;
        this.WidthRequest = animationTypeMobile.WidthRequest;
#else
    animationTypeDesktop = new AnimationTypeDesktop();
    this.Content = animationTypeDesktop.Content;
    this.OptionView = animationTypeDesktop.OptionView;
    this.WidthRequest = animationTypeDesktop.WidthRequest;
#endif
    }
}