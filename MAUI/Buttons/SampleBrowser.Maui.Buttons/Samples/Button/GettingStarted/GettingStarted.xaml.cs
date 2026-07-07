using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.Button
{
    public partial class GettingStarted : SampleView
    {
        public GettingStarted()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.Content = new GettingStartedMobile();
#elif WINDOWS || MACCATALYST
            this.Content = new GettingStartedDesktop();
#endif
        }
        
    }
}
