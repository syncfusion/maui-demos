#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    using Syncfusion.Maui.AIAssistView;
    using Syncfusion.Maui.Scheduler;
    using System;
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    ///  Represents the behavior for the assist view.
    /// </summary>
    public class AssistViewBehavior : Behavior<SfAIAssistView>
    {
        /// <summary>
        /// Holds the assist view instance.
        /// </summary>
        private SfAIAssistView? assistView;

        /// <summary>
        /// Holds the request time.
        /// </summary>
        private string requestTime = string.Empty;

        /// <summary>
        /// Holds the azure AI services.
        /// </summary>
        private AzureAIServices azureAIServices = new AzureAIServices();

        /// <summary>
        /// Gets or sets the scheduler.
        /// </summary>
        public SfScheduler? Scheduler { get; set; }

        /// <summary>
        /// Gets or sets the assist view model.
        /// </summary>
        public SchedulerViewModel? SchedulerViewModel { get; set; }

        /// <summary>
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnAttachedTo(SfAIAssistView bindable)
        {
            base.OnAttachedTo(bindable);
            this.assistView = bindable;
            if (this.assistView != null)
            {
                this.assistView.Request += this.OnAssistViewRequest;
            }
        }

        /// <summary>
        /// Method to provide AI request.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private async void OnAssistViewRequest(object? sender, RequestEventArgs e)
        {
            this.SchedulerViewModel!.ShowHeader = false;
            string requeststring = e.RequestItem!.Text;
            DateTime sophiaStartTime;
            DateTime sophiaEndTime;
            string sophiaSubject = string.Empty;
            string sophiaLocation = string.Empty;
            string sophiaResourceID = string.Empty;
            DateTime johnStartTime;
            DateTime johnEndTime;
            string johnSubject = string.Empty;
            string johnLocation = string.Empty;
            string johnResourceID = string.Empty;


            if (AzureBaseService.IsCredentialValid)
            {
                if (string.IsNullOrEmpty(e.RequestItem!.Text))
                {
                    return;
                }

                string pattern = @"\b\d{2}:\d{2} (AM|PM)\b";
                bool isValidPattern = Regex.IsMatch(requeststring, pattern);

                if (Regex.IsMatch(requeststring, @"9 AM|11 AM|2 PM|4 PM|10 AM|12 PM|3 PM|5 PM") || Regex.IsMatch(requeststring, @"General Check-Up|Vaccinations|Diagnostic report|Diabetes"))
                {
                    if (requeststring == "9 AM" || requeststring == "11 AM" || requeststring == "2 PM"
                    || requeststring == "4 PM" || requeststring == "10 AM" || requeststring == "12 PM" || requeststring == "3 PM" || requeststring == "5 PM")
                    {
                        requestTime = requeststring;
                        await Task.Delay(2000);
                        var subjectLsit = GetSubjectSuggestion();
                        AssistItem subjectMessage = new AssistItem() { Text = "What is the purpose of your appointment?", Suggestion = subjectLsit, ShowAssistItemFooter = false };
                        this.SchedulerViewModel!.Messages.Add(subjectMessage);
                    }
                    else if (requeststring == "General Check-Up" || requeststring == "Vaccinations" || requeststring == "Diagnostic report" || requeststring == "Diabetes")
                    {
                        await Task.Delay(1000);
                        UpdateOfflineAppointmentBooking(requestTime, requeststring);
                        await Task.Delay(1000);
                        AssistItem botMessage = new AssistItem() { Text = $"Your appointment with Doctor {SchedulerViewModel!.CurrentResource} has been booked.", ShowAssistItemFooter = false };
                        AssistItem refreshMessage = new AssistItem() { Text = $"Please click the refresh button to schedule a new appointment.", ShowAssistItemFooter = false };
                        this.SchedulerViewModel.Messages.Add(botMessage);
                        this.SchedulerViewModel.Messages.Add(refreshMessage);
                    }
                }
                else if (!isValidPattern)
                {
                    await this.SchedulerViewModel!.GetAIResults(e.RequestItem.Text).ConfigureAwait(true);
                }
                else
                {
                    if (requeststring.StartsWith("John: ", StringComparison.OrdinalIgnoreCase))
                    {
                        this.SchedulerViewModel!.CurrentResource = "John";
                        requeststring = requeststring.Substring("John: ".Length);
                    }
                    else if (requeststring.StartsWith("Sophia: ", StringComparison.OrdinalIgnoreCase))
                    {
                        this.SchedulerViewModel!.CurrentResource = "Sophia";
                        requeststring = requeststring.Substring("Sophia: ".Length);
                    }

                    for (int i = 0; i < this.SchedulerViewModel!.SophiaAvailableTimeSlots?.Count; i++)
                    {
                        if (requeststring == this.SchedulerViewModel.SophiaAvailableTimeSlots[i].ToString())
                        {
                            sophiaStartTime = this.SchedulerViewModel.SophiaStartTimeCollection![i];
                            sophiaEndTime = this.SchedulerViewModel.SophiaEndTimeCollection![i];
                            sophiaSubject = this.SchedulerViewModel.SophiaSubjectCollection![i];
                            sophiaLocation = this.SchedulerViewModel.SophiaLocationCollection![i];
                            sophiaResourceID = this.SchedulerViewModel.SophiaResourceIDCollection![i];
                            this.AppointmentBooking(sophiaStartTime, sophiaEndTime, sophiaSubject, sophiaLocation, sophiaResourceID);
                            await Task.Delay(1000);
                            AssistItem botMessage = new AssistItem() { Text = "Doctor Sophia appointment successfully booked.\nThank you! ", ShowAssistItemFooter = false };
                            this.SchedulerViewModel.Messages.Add(botMessage);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    for (int j = 0; j < this.SchedulerViewModel.JohnAvailableTimeSlots?.Count; j++)
                    {
                        if (requeststring == this.SchedulerViewModel.JohnAvailableTimeSlots[j].ToString())
                        {
                            johnStartTime = this.SchedulerViewModel.JohnStartTimeCollection![j];
                            johnEndTime = this.SchedulerViewModel.JohnEndTimeCollection![j];
                            johnSubject = this.SchedulerViewModel.JohnSubjectCollection![j];
                            johnLocation = this.SchedulerViewModel.JohnLocationCollection![j];
                            johnResourceID = this.SchedulerViewModel.JohnResourceIDCollection![j];
                            this.AppointmentBooking(johnStartTime, johnEndTime, johnSubject, johnLocation, johnResourceID);
                            await Task.Delay(1000);
                            AssistItem botMessage = new AssistItem() { Text = "Doctor John appointment successfully booked.\nThank you! ", ShowAssistItemFooter = false };
                            this.SchedulerViewModel.Messages.Add(botMessage);
                        }
                    }
                }
            }
            else
            {
                if (requeststring == "9 AM" || requeststring == "11 AM" || requeststring == "2 PM"
                   || requeststring == "4 PM" || requeststring == "10 AM" || requeststring == "12 PM" || requeststring == "3 PM" || requeststring == "5 PM")
                {
                    requestTime = requeststring;
                    await Task.Delay(2000);
                    var subjectLsit = GetSubjectSuggestion();
                    AssistItem subjectMessage = new AssistItem() { Text = "What is the purpose of your appointment?", Suggestion = subjectLsit, ShowAssistItemFooter = false };
                    this.SchedulerViewModel!.Messages.Add(subjectMessage);
                }
                else if (requeststring == "General Check-Up" || requeststring == "Vaccinations" || requeststring == "Diagnostic report" || requeststring == "Diabetes")
                {
                    await Task.Delay(1000);
                    UpdateOfflineAppointmentBooking(requestTime, requeststring);
                    await Task.Delay(1000);
                    AssistItem botMessage = new AssistItem() { Text = $"Your appointment with Doctor {SchedulerViewModel!.CurrentResource} has been booked.", ShowAssistItemFooter = false };
                    AssistItem refreshMessage = new AssistItem() { Text = $"Please click the refresh button to schedule a new appointment.", ShowAssistItemFooter = false };
                    this.SchedulerViewModel.Messages.Add(botMessage);
                    this.SchedulerViewModel.Messages.Add(refreshMessage);
                }
                else
                {
                    await Task.Delay(1000);
                    AssistItem botMessage = new AssistItem() { Text = "You are offline. Please connect to the internet and OpenAI.", ShowAssistItemFooter = false };
                    this.SchedulerViewModel!.Messages.Add(botMessage);
                }
            }
        }

        /// <summary>
        /// Method to get the subject suggestions.
        /// </summary>
        /// <returns></returns>
        private AssistItemSuggestion GetSubjectSuggestion()
        {
            var chatSubjectSuggestions = new AssistItemSuggestion();
            var subjectsuggestions = new ObservableCollection<ISuggestion>();
            subjectsuggestions.Add(new AssistSuggestion() { Text = SubjectPromptRequest[0] });
            subjectsuggestions.Add(new AssistSuggestion() { Text = SubjectPromptRequest[1] });
            subjectsuggestions.Add(new AssistSuggestion() { Text = SubjectPromptRequest[2] });
            subjectsuggestions.Add(new AssistSuggestion() { Text = SubjectPromptRequest[3] });
            chatSubjectSuggestions.Items = subjectsuggestions;
            chatSubjectSuggestions.Orientation = SuggestionsOrientation.Vertical;
            return chatSubjectSuggestions;
        }

        /// <summary>
        /// Prompt request collection.
        /// </summary>
        private string[] SubjectPromptRequest = new string[]
        {
          "General Check-Up",
          "Vaccinations",
          "Diagnostic report",
          "Diabetes",
        };

        /// <summary>
        /// Method to book the online appointments.
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="subject">The subject</param>
        /// <param name="location">The location</param>
        /// <param name="resourceID">The resource id</param>
        private void AppointmentBooking(DateTime startTime, DateTime endTime, string subject, string location, string resourceID)
        {
            this.Scheduler!.DisplayDate = startTime;
            this.SchedulerViewModel?.Appointments?.Add(new SchedulerAppointment()
            {
                StartTime = startTime,
                EndTime = endTime,
                Subject = subject,
                Location = location,
                ResourceIds = new ObservableCollection<object>() { resourceID },
                Background = resourceID == "1000" ? new SolidColorBrush(Color.FromArgb("#36B37B")) : new SolidColorBrush(Color.FromArgb("#8B1FA9")),
            });

            (this.Scheduler!.AppointmentsSource as ObservableCollection<SchedulerAppointment>)?.AddRange(this.SchedulerViewModel?.Appointments!);
        }

        /// <summary>
        /// Method to book the offline appointments.
        /// </summary>
        /// <param name="timeValue">The time value.</param>
        /// <param name="subject">The subject.</param>
        private void UpdateOfflineAppointmentBooking(string timeValue, string subject)
        {
            var appointmentData = new Dictionary<string, (int hour, string resourceId)>
            {
                { "9 AM",  (9, "1000") },
                { "10 AM", (10, "1001") },
                { "11 AM", (11, "1000") },
                { "12 PM", (12, "1001") },
                { "2 PM",  (14, "1000") },
                { "3 PM",  (15, "1001") },
                { "4 PM",  (16, "1000") },
                { "5 PM",  (17, "1001") }
            };

            if (appointmentData.TryGetValue(timeValue, out var data))
            {
                var startTime = DateTime.Today.AddHours(data.hour);
                var appointment = new SchedulerAppointment
                {
                    StartTime = startTime,
                    EndTime = startTime.AddMinutes(30),
                    Subject = subject,
                    Location = "ABC hospital",
                    Background = data.resourceId == "1000" ? new SolidColorBrush(Color.FromArgb("#36B37B")) : new SolidColorBrush(Color.FromArgb("#8B1FA9")),
                    ResourceIds = new ObservableCollection<object> { data.resourceId }
                };

                this.Scheduler!.DisplayDate = startTime;
                (this.Scheduler!.AppointmentsSource as ObservableCollection<SchedulerAppointment>)?.Add(appointment);
            }
        }

        /// <summary>
        /// On Detached method.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnDetachingFrom(SfAIAssistView bindable)
        {
            base.OnDetachingFrom(bindable);
            this.assistView!.Request -= OnAssistViewRequest;
        }
    }
}