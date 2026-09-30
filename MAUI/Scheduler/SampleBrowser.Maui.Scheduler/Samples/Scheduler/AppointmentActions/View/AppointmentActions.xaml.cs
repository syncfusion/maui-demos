namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System.Collections.ObjectModel;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Interaction logic for AppointmentActions.xaml
    /// </summary>
    public partial class AppointmentActions : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentActions"/> class.
        /// </summary>
        public AppointmentActions()
        {
            InitializeComponent();
        }

        private void EditTapped(object? sender, TappedEventArgs e)
        {
            if ((sender as Label)?.BindingContext is SchedulerAppointment appointment)
            {
                this.Scheduler.OpenEditPopup(appointment);
            }
        }

        private void DeleteTapped(object? sender, TappedEventArgs e)
        {
            if ((sender as Label)?.BindingContext is SchedulerAppointment appointment)
            {
                this.Scheduler.DeleteAppointment(appointment);
            }
        }
    }
}