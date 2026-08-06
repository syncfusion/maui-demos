using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfScheduler
{
    /// <summary>
    /// Interaction logic for GettingStarted.xaml
    /// </summary>
    public partial class GettingStarted : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted" /> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();

#if IOS26_0_OR_GREATER || MACCATALYST26_0_OR_GREATER
            this.Scheduler.Background = Brush.Transparent;
            this.Scheduler.HeaderView.Background = Brush.Transparent;
#endif
        }
    }
}