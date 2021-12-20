using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// Interaction logic for RecursiveAppointments.xaml
    /// </summary>
    public partial class RecursiveAppointments : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecursiveAppointments" /> class.
        /// </summary>
        public RecursiveAppointments()
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