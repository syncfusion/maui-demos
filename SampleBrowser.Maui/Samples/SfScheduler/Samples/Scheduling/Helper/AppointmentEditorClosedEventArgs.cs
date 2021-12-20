using System;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// The appointment editor closed event args
    /// </summary>
    internal class AppointmentEditorClosedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets appointment
        /// </summary>
        internal Meeting Appointment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets is edit value
        /// </summary>
        internal bool IsEdit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets is delete value
        /// </summary>
        internal bool IsDelete
        {
            get;
            set;
        }
    }
}
