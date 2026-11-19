namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
	using System.Collections.ObjectModel;
	using System.Windows.Input;
	using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Provides clipboard related commands for managing scheduler appointments, including copy, cut, and paste operations.
    /// </summary>
    public static class ClipboardCommands
	{
		#region Fields

		/// <summary>
		/// Stores the appointment copied during copy or cut operations.
		/// </summary>
		private static SchedulerAppointment? copiedAppointment { get; set; }

		/// <summary>
		/// Holds a reference to the original appointment selected during a cut operation.
		/// </summary>
		private static SchedulerAppointment? cutAppointment { get; set; }

		/// <summary>
		/// Indicates whether the current clipboard operation is a cut operation.
		/// </summary>
		private static bool isCutOperation = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command used to cut the appointment.
        /// </summary>
        public static ICommand Cut { get; } = new CutAppointmentCommand();

        /// <summary>
        /// Gets the command used to copy the appointment.
        /// </summary>
        public static ICommand Copy { get; } = new CopyAppointmentCommand();

        /// <summary>
        /// Gets the command used to paste the appointment.
        /// </summary>
        public static ICommand Paste { get; } = new PasteAppointmentCommand();

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a clone of the specified appointment.
        /// </summary>
        /// <param name="appointment">The source appointment to clone.</param>
        /// <returns>The cloned appointment.</returns>
        private static SchedulerAppointment GetClonedAppointment(SchedulerAppointment appointment)
		{
			return new SchedulerAppointment
			{
				StartTime = appointment.StartTime,
				EndTime = appointment.EndTime,
				Subject = appointment.Subject,
				Notes = appointment.Notes,
				IsAllDay = appointment.IsAllDay,
				Location = appointment.Location,
				RecurrenceRule = appointment.RecurrenceRule,
				Background = appointment.Background,
				ResourceIds = appointment.ResourceIds,
			};
		}

        #endregion

        #region Commands

        /// <summary>
        /// Represents a command that copies the appointment from the scheduler context menu.
        /// </summary>
        internal class CopyAppointmentCommand : ICommand
		{
            #region Events

            /// <summary>
            /// Occurs when the command execution state changes.
            /// </summary>
            event EventHandler? ICommand.CanExecuteChanged
			{
				add { }
				remove { }
			}

			#endregion

			#region Methods

			/// <summary>
			/// Determines whether the copy command can be executed.
			/// </summary>
			/// <param name="parameter">The command parameter, expected to be of type <see cref="SchedulerContextMenuInfo"/>.</param>
			/// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
			bool ICommand.CanExecute(object? parameter)
			{
				return parameter is SchedulerContextMenuInfo info && info.Scheduler != null && info.Appointment != null;
			}

			/// <summary>
			/// Executes the copy operation on the selected appointment.
			/// </summary>
			/// <param name="parameter">The command parameter containing scheduler context information.</param>
			void ICommand.Execute(object? parameter)
			{
				if (parameter is SchedulerContextMenuInfo info && info.Appointment != null)
				{
					isCutOperation = false;
					cutAppointment = null;
					copiedAppointment = GetClonedAppointment(info.Appointment);
				}
			}
			
			#endregion
		}

		/// <summary>
		/// Represents a command that cuts the appointment from the scheduler context menu.
		/// </summary>
		internal class CutAppointmentCommand : ICommand
		{
			#region Events

			/// <summary>
			/// Occurs when the command execution state changes.
			/// </summary>
			event EventHandler? ICommand.CanExecuteChanged
			{
				add { }
				remove { }
			}

			#endregion

			#region Methods

			/// <summary>
			/// Determines whether the cut command can be executed.
			/// </summary>
			/// <param name="parameter">The command parameter containing scheduler context information.</param>
			/// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>	
			bool ICommand.CanExecute(object? parameter)
			{
				return parameter is SchedulerContextMenuInfo info && info.Scheduler != null && info.Appointment != null;
			}

			/// <summary>
			/// Executes the cut operation on the selected appointment.
			/// </summary>
			/// <param name="parameter">The command parameter containing scheduler context information.</param>
			void ICommand.Execute(object? parameter)
			{
				if (parameter is SchedulerContextMenuInfo info && info.Scheduler != null && info.Appointment != null)
				{
					isCutOperation = true;
					cutAppointment = info.Appointment;
					copiedAppointment = GetClonedAppointment(cutAppointment);
				}
			}

			#endregion
		}

		/// <summary>
		/// Represents a command that paste the appointment from the scheduler context menu.
		/// </summary>
		internal class PasteAppointmentCommand : ICommand
		{
			#region Events

			/// <summary>
			/// Occurs when the command execution state changes.
			/// </summary>
			event EventHandler? ICommand.CanExecuteChanged
			{
				add { }
				remove { }
			}

			#endregion

			#region Methods

			/// <summary>
			/// Determines whether the paste command can be executed.
			/// </summary>
			/// <param name="parameter">The command parameter containing scheduler context information.</param>
			/// <returns><c>true</c> if the command can be executed; otherwise, <c>false</c>.</returns>
			bool ICommand.CanExecute(object? parameter)
			{
				return parameter is SchedulerContextMenuInfo info && info.Scheduler != null && copiedAppointment != null;
			}

			/// <summary>
			/// Executes the paste operation by adding a new appointment based on the copied data.
			/// </summary>
			/// <param name="parameter">The command parameter containing scheduler context information.</param>
			void ICommand.Execute(object? parameter)
			{
				if (parameter is not SchedulerContextMenuInfo info || info.Scheduler == null || copiedAppointment == null || info.Scheduler.AppointmentsSource is not ObservableCollection<SchedulerAppointment> appointments)
				{
					return;
				}

				if (isCutOperation && cutAppointment != null)
				{
					appointments.Remove(cutAppointment);
					cutAppointment = null;
					isCutOperation = false;
				}

				var newAppointment = GetClonedAppointment(copiedAppointment);
				var duration = copiedAppointment.EndTime - copiedAppointment.StartTime;
				newAppointment.StartTime = info.DateTime;
				newAppointment.EndTime = info.DateTime.Add(duration);
				appointments.Add(newAppointment);
			}

			#endregion
		}

        #endregion
    }
}