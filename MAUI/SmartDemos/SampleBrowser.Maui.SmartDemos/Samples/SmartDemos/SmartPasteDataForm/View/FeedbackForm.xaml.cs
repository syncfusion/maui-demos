namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    using SampleBrowser.Maui.Base;

    public partial class FeedbackForm : SampleView
    {
        public FeedbackForm()
        {
#if ANDROID || IOS
            this.Content = new FeedbackFormMobileView();
#else
            this.Content = new FeedbackFormDesktopView();
#endif
        }
    }
}
