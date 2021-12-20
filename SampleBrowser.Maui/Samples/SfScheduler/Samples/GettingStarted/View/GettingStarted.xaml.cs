using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfScheduler
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
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Scheduler.Handler?.DisconnectHandler();
        }
    }
}