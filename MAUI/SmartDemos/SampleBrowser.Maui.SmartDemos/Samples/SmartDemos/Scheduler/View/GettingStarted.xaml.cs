#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.AIAssistView;

    public partial class GettingStarted : SampleView
    {
        public GettingStarted()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            this.Behaviors.Clear();
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var appointmentData = (sender as Border)?.BindingContext as AppointmentModel;
            AssistItem botMessage = new AssistItem() { Text = appointmentData?.Name!, IsRequested = true, ShowAssistItemFooter = false };
            this.viewModel!.Messages.Add(botMessage);
            this.viewModel!.ShowHeader = false;
            await this.viewModel.GetResponse(appointmentData?.Name!).ConfigureAwait(true);
        }
    }
}