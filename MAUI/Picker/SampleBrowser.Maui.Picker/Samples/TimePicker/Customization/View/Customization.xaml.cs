#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfTimePicker
{
    using SampleBrowser.Maui.Base;

    public partial class Customization : SampleView
    {
        private AlarmDetails? alarmDetails;
        public Customization()
        {
            InitializeComponent();
        }

        private void OnAlarmTapped(object sender, EventArgs e)
        {
            if (sender is Border border && border.BindingContext != null && border.BindingContext is AlarmDetails alarmDetails)
            {
                if (alarmDetails.IsAlarmEnabled)
                {
                    this.alarmEditPicker.SelectedTime = alarmDetails.AlarmTime;
                    this.alarmDetails = alarmDetails;
                    this.alarmEditPicker.IsOpen = true;
                }
            }
        }

        private void AlarmEditPicker_OkButtonClicked(object? sender, EventArgs e)
        {
            if (sender is Syncfusion.Maui.Picker.SfTimePicker picker && this.alarmDetails != null)
            {
                if (this.alarmDetails.AlarmTime != picker.SelectedTime)
                {
                    this.alarmDetails.AlarmTime = picker.SelectedTime;
                }

                this.alarmDetails = null;
            }

            this.alarmEditPicker.IsOpen = false;
        }

        private void alarmEditPicker_CancelButtonClicked(object sender, EventArgs e)
        {
            this.alarmEditPicker.IsOpen = false;
        }

        private void alarmSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (sender is Switch toggleSwitch && toggleSwitch.BindingContext != null && toggleSwitch.BindingContext is AlarmDetails alarmDetails)
            {
                if (e.Value)
                {
                    alarmDetails.AlarmTextColor = Colors.Black;
                    alarmDetails.AlarmSecondaryTextColor = Colors.Gray;
                }
                else
                {
                    alarmDetails.AlarmTextColor = Colors.Gray;
                    alarmDetails.AlarmSecondaryTextColor = Colors.LightGray;
                }
            }

        }

        private void OnAddAlarmTapped(object sender, EventArgs e)
        {
            this.alarmPopup.IsOpen = true;
        }

        private void alarmPopup_OnCreated(object sender, EventArgs e)
        {
            if (this.BindingContext != null && this.BindingContext is ViewModel bindingContext && sender is AlarmDetails details)
            {
                bindingContext.AlarmCollection.Add(details);
            }
        }
    }
}