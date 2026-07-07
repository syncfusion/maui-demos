namespace SampleBrowser.Maui.Gauges.SfDigitalGauge
{
    using SampleBrowser.Maui.Base;

    public partial class GettingStarted : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted"/> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();
#if ANDROID || IOS
            GettingStartedMobileUI gettingStartedMobileUI = new GettingStartedMobileUI();
            this.Content = gettingStartedMobileUI;
            this.OptionView = gettingStartedMobileUI.OptionView;
            this.OptionView!.BindingContext = gettingStartedMobileUI.BindingContext;
#else
            GettingStartedDesktopUI gettingStartedDesktopUI = new GettingStartedDesktopUI();
            this.Content = gettingStartedDesktopUI;
            this.OptionView = gettingStartedDesktopUI.OptionView;
            this.OptionView!.BindingContext = gettingStartedDesktopUI.BindingContext;
#endif
        }
    }
}