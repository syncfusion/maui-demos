using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class AppointmentActionsViewModel
    {
        public ObservableCollection<SchedulerAppointment> Appointments { get; set; }

        private readonly string[] subjects =
        {
            "Story Time", "Camping World", "Wildlife Walk", "Parrot Talk",
            "Walk with John", "Birds of Prey", "Black Cockatoo", "Croco World"
        };

        private readonly string[] colors =
        {
            "#22B14C", "#3F7DD1", "#84B500", "#E77A52", "#15B8B3", "#F58613"
        };

        public AppointmentActionsViewModel()
        {
            Appointments = new ObservableCollection<SchedulerAppointment>();
            GenerateAppointments();
        }

        private void GenerateAppointments()
        {
            Random ran = new();
            DateTime today = DateTime.Today;
            DateTime startDate = today.AddDays(-7);

            for (int dayOffset = 0; dayOffset < 15; dayOffset++)
            {
                DateTime date = startDate.AddDays(dayOffset);

                DateTime morningStart = date.AddHours(9 + ran.Next(0, 2));
                Appointments.Add(new SchedulerAppointment()
                {
                    Subject = subjects[ran.Next(subjects.Length)],
                    StartTime = morningStart,
                    EndTime = morningStart.AddHours(1),
                    Background = Color.FromArgb(colors[ran.Next(colors.Length)])
                });

                DateTime afternoonStart = date.AddHours(14 + ran.Next(0, 2));
                Appointments.Add(new SchedulerAppointment()
                {
                    Subject = subjects[ran.Next(subjects.Length)],
                    StartTime = afternoonStart,
                    EndTime = afternoonStart.AddHours(1),
                    Background = Color.FromArgb(colors[ran.Next(colors.Length)])
                });
            }
        }
    }
}