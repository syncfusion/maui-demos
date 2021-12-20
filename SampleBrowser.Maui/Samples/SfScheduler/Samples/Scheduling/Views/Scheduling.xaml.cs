using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// Interaction logic for Scheduling.xaml
    /// </summary>
    public partial class Scheduling : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scheduling" /> class.
        /// </summary>
        public Scheduling()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Schedule.Handler?.DisconnectHandler();
        }
    }
}