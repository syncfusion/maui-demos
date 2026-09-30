namespace SampleBrowser.Maui.LiquidGlass.SfStepProgressBar
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Represents a page that displays and manages a Step Progress Bar within the application.
    /// </summary>
    public partial class StepProgressBarPage : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepProgressBarPage"/> class.
        /// </summary>
        public StepProgressBarPage()
        {
            InitializeComponent();

#if WINDOWS || MACCATALYST
            OrderTrackingDestopUI orderTrackingDestopUI = new OrderTrackingDestopUI();
            this.Content = orderTrackingDestopUI;
            this.OptionView = orderTrackingDestopUI.OptionView;
#elif ANDROID || IOS
            OrderTrackingMobileUI orderTrackingMobileUI = new OrderTrackingMobileUI();
            this.Content = orderTrackingMobileUI;
            this.OptionView = orderTrackingMobileUI.OptionView;
#endif
        }
    }
}