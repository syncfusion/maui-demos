using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.Switch
{
    public partial class Features : SampleView
    {
        public Features()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.Content = new FeaturesDesktop();
#elif WINDOWS || MACCATALYST
            this.Content = new FeaturesDesktop();
#endif
        }
        
    }
}
