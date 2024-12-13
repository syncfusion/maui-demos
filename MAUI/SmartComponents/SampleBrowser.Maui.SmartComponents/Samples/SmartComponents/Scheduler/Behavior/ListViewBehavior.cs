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

    /// <summary>
    /// Represents Class to access the list view control.
    /// </summary>
    public class ListViewBehavior : Behavior<VerticalStackLayout>
    {
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
        /// On Detached method.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnDetachingFrom(VerticalStackLayout bindable)
        {
            base.OnDetachingFrom(bindable);

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