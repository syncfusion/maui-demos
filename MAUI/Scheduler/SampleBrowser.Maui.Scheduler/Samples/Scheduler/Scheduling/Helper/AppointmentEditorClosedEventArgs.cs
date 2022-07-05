#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// The appointment editor closed event args
    /// </summary>
    internal class AppointmentEditorClosedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets appointment
        /// </summary>
        internal Meeting? Appointment
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
