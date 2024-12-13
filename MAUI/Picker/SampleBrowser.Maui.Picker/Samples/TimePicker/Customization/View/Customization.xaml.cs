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
    using Syncfusion.Maui.Buttons;

    public partial class Customization : SampleView
    {
        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        private AlarmDetails? alarmDetails;
        public Customization()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.alarmEditPicker1.HeaderView.Height = 40;
            this.alarmEditPicker1.HeaderView.Text = "Edit Alarm";

            this.alarmEditPicker1.FooterView.Height = 40;
            this.alarmEditPicker1.FooterView.OkButtonText = "Save";
#else
            this.alarmEditPicker.HeaderView.Height = 40;
            this.alarmEditPicker.HeaderView.Text = "Edit Alarm";

            this.alarmEditPicker.FooterView.Height = 40;
            this.alarmEditPicker.FooterView.OkButtonText = "Save";
#endif
        }

        private void OnAlarmTapped(object sender, EventArgs e)
        {
#if ANDROID || IOS
            if (sender is Border border && border.BindingContext != null && border.BindingContext is AlarmDetails alarmDetails)
            {
                if (alarmDetails.IsAlarmEnabled)
                {
                    this.alarmEditPicker1.SelectedTime = alarmDetails.AlarmTime;
                    this.alarmDetails = alarmDetails;
                    this.alarmEditPicker1.IsOpen = true;
                }
            }
#else
            if (sender is Border border && border.BindingContext != null && border.BindingContext is AlarmDetails alarmDetails)
            {
                if (alarmDetails.IsAlarmEnabled)
                {
                    this.alarmEditPicker.SelectedTime = alarmDetails.AlarmTime;
                    this.alarmDetails = alarmDetails;
                    this.alarmEditPicker.IsOpen = true;
                }
            }
#endif
        }

        private void AlarmEditPicker_OkButtonClicked(object? sender, EventArgs e)
        {
            if (sender is Syncfusion.Maui.Picker.SfTimePicker picker && this.alarmDetails != null)
            {
                if (picker.SelectedTime != null && this.alarmDetails.AlarmTime != picker.SelectedTime)
                {
                    this.alarmDetails.AlarmTime = picker.SelectedTime.Value;
                }

                this.alarmDetails = null;
            }

#if ANDROID || IOS
            this.alarmEditPicker1.IsOpen = false;
#else
            this.alarmEditPicker.IsOpen = false;
#endif
        }

        private void alarmEditPicker_CancelButtonClicked(object sender, EventArgs e)
        {
#if ANDROID || IOS
            this.alarmEditPicker1.IsOpen = false;
#else
            this.alarmEditPicker.IsOpen = false;
#endif
        }

        private void alarmSwitch_Toggled(object sender, SwitchStateChangedEventArgs e)
        {
            if (sender is SfSwitch toggleSwitch && toggleSwitch.BindingContext != null && toggleSwitch.BindingContext is AlarmDetails alarmDetails && e.NewValue.HasValue)
            {
                if (e.NewValue.Value)
                {
                    alarmDetails.AlarmTextColor = isLightTheme ? Colors.Black : Colors.White;
                    alarmDetails.AlarmSecondaryTextColor = isLightTheme ? Color.FromArgb("#49454F") : Color.FromArgb("#CAC4D0");
                }
                else
                {
                    alarmDetails.AlarmTextColor = isLightTheme ? Colors.Black.WithAlpha(0.5f) : Colors.White.WithAlpha(0.5f);
                    alarmDetails.AlarmSecondaryTextColor = isLightTheme ? Color.FromArgb("#49454F").WithAlpha(0.5f) : Color.FromArgb("#CAC4D0").WithAlpha(0.5f);
                }
            }

        }

        private void OnAddAlarmTapped(object sender, EventArgs e)
        {
#if ANDROID || IOS
            this.alarmPopup1.IsOpen = true;
#else
            this.alarmPopup.IsOpen = true;
#endif
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