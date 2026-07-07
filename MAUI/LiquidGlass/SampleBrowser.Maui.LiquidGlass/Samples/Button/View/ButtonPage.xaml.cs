using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfButton
{
    public partial class ButtonPage : SampleView
    {
        public ButtonPage()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.Content = new ButtonMobilePage();
#elif WINDOWS || MACCATALYST
            this.Content = new ButtonDesktopPage();
#endif
        }
        
    }
}
