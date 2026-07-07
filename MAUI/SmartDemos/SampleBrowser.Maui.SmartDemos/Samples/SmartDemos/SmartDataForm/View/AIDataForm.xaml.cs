namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    using SampleBrowser.Maui.Base;

    public partial class AIDataForm : SampleView
    {
        public AIDataForm()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.Content = new DataFormMobileUI();
#else
            this.Content = new DataFormDesktopUI();
#endif
        }
    }
}
