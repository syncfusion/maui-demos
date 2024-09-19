#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using System.Threading.Tasks;
    using System.Collections.ObjectModel;
    using Syncfusion.Maui.ListView;
    using Syncfusion.Maui.AIAssistView;

    /// <summary>
    /// Represents Class to access the list view control.
    /// </summary>
    public class ListViewBehavior : Behavior<VerticalStackLayout>
    {
        /// <summary>
        /// Holds the list view instance.
        /// </summary>
        private SfListView? listView;

        /// <summary>
        /// Gets or sets the assist view model.
        /// </summary>
        public SchedulerViewModel? ListAssistViewModel { get; set; }

        /// <summary>
        /// Gets or sets the close button.
        /// </summary>
        public Button? CloseButton { get; set; }

        /// <summary>
        /// Gets or sets the refresh button.
        /// </summary>
        public Button? RefreshButton { get; set; }

        /// <summary>
        /// Gets or sets the header view.
        /// </summary>
        public Border? HeaderView { get; set; }

        /// <summary>
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnAttachedTo(VerticalStackLayout bindable)
        {
            base.OnAttachedTo(bindable);
            this.listView = bindable.FindByName<SfListView>("listView");
            if (this.listView != null)
            {
                this.listView.ItemTapped += OnSfListViewItemTapped;
            }

            if (this.CloseButton != null)
            {
                this.CloseButton.Clicked += CloseButton_Clicked;
            }

            if (this.RefreshButton != null)
            {
                this.RefreshButton.Clicked += RefreshButton_Clicked;
            }
        }

        /// <summary>
        /// Used to handle refreshing the assist view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RefreshButton_Clicked(object? sender, EventArgs e)
        {
            if (this.ListAssistViewModel != null)
            {
                this.ListAssistViewModel.ShowHeader = false;
                this.ListAssistViewModel.Messages.Clear();
                this.ListAssistViewModel.ShowIndicator = true;

                await Task.Delay(1000).ConfigureAwait(true);
                this.ListAssistViewModel.ShowHeader = true;
                this.ListAssistViewModel.ShowIndicator = false;
            }
        }

        /// <summary>
        /// Used to handle the visibility of assist view.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event args.</param>
        private void CloseButton_Clicked(object? sender, EventArgs e)
        {
            if (this.ListAssistViewModel != null)
            {
                this.ListAssistViewModel.ShowAssistView = false;
                this.HeaderView!.IsVisible = false;
            }
        }

        /// <summary>
        /// Method to access the list view items.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private async void OnSfListViewItemTapped(object? sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            var appointmentData = e.DataItem as AppointmentModel;
            AssistItem botMessage = new AssistItem() { Text = appointmentData?.Name!, IsRequested = true, ShowAssistItemFooter = false };
            this.ListAssistViewModel!.Messages.Add(botMessage);
            this.ListAssistViewModel!.ShowHeader = false;
            await this.GetResponse(appointmentData?.Name!).ConfigureAwait(true);
        }

        /// <summary>
        /// Method to get the offline response.
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns></returns>
        private async Task GetResponse(string query)
        {
            var suggestion = new AssistItemSuggestion();
            await Task.Delay(1000).ConfigureAwait(true);
            suggestion = GetSuggestion(query);
            AssistItem botMessage = new AssistItem() { Text = "Please select an appointment time:", Suggestion = suggestion, ShowAssistItemFooter = false };
            this.ListAssistViewModel!.Messages.Add(botMessage);
        }

        /// <summary>
        /// Method to get the offline suggestion.
        /// </summary>
        /// <param name="prompt">The prompt</param>
        /// <returns></returns>
        private AssistItemSuggestion GetSuggestion(string prompt)
        {
            var chatSuggestions = new AssistItemSuggestion();
            var suggestions = new ObservableCollection<ISuggestion>();
            if (prompt == "Book an appointment with Dr. Sophia")
            {
                this.ListAssistViewModel!.CurrentResource = "Sophia";
                suggestions.Add(new AssistSuggestion() { Text = SophiaPromptRequest[0] });
                suggestions.Add(new AssistSuggestion() { Text = SophiaPromptRequest[1] });
                suggestions.Add(new AssistSuggestion() { Text = SophiaPromptRequest[2] });
                suggestions.Add(new AssistSuggestion() { Text = SophiaPromptRequest[3] });
            }
            else
            {
                this.ListAssistViewModel!.CurrentResource = "John";
                suggestions.Add(new AssistSuggestion() { Text = JohnPromptRequest[0] });
                suggestions.Add(new AssistSuggestion() { Text = JohnPromptRequest[1] });
                suggestions.Add(new AssistSuggestion() { Text = JohnPromptRequest[2] });
                suggestions.Add(new AssistSuggestion() { Text = JohnPromptRequest[3] });
            }

            chatSuggestions.Items = suggestions;
            chatSuggestions.Orientation = SuggestionsOrientation.Horizontal;
            return chatSuggestions;
        }

        /// <summary>
        /// John prompt request collection.
        /// </summary>
        private string[] SophiaPromptRequest = new string[]
        {
          "9 AM",
          "11 AM",
          "2 PM",
          "4 PM",
        };

        /// <summary>
        /// John prompt request collection.
        /// </summary>
        private string[] JohnPromptRequest = new string[]
        {
          "10 AM",
          "12 PM",
          "3 PM",
          "5 PM",
        };

        /// <summary>
        /// On Detached method.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnDetachingFrom(VerticalStackLayout bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.listView != null)
            {
                this.listView.ItemTapped -= OnSfListViewItemTapped;
            }

            if (this.CloseButton != null)
            {
                this.CloseButton.Clicked -= CloseButton_Clicked;
            }

            if (this.RefreshButton != null)
            {
                this.RefreshButton.Clicked -= RefreshButton_Clicked;
            }
        }
    }
}