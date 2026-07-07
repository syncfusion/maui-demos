using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.Switch
{
    public partial class GettingStarted : SampleView
    {
        public GettingStarted()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.Content = new GettingStartedDesktop();
#elif WINDOWS || MACCATALYST
            this.Content = new GettingStartedDesktop();
#endif
        }
        
    }
}
