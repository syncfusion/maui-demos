#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfTimePicker
{
    using Syncfusion.Maui.Popup;
    using Syncfusion.Maui.Core;

    public class AlarmPopup : SfPopup
    {
        private Syncfusion.Maui.Picker.SfTimePicker alarmTimePicker;
        private Entry alarmMessageEntry;
        private SfTextInputLayout alarmTextInput;

        public event EventHandler? OnCreated;
        public AlarmPopup()
        {
            this.alarmTimePicker = new Syncfusion.Maui.Picker.SfTimePicker();
            StackLayout stack = new StackLayout();
            Label label = new Label();
            label.Text = "Alarm Message";
            label.Margin = new Thickness(10, 4);
            label.FontSize = 12;
            stack.Add(label);
            this.alarmTextInput = new SfTextInputLayout();
            this.alarmTextInput.Padding = new Thickness(0);
            this.alarmTextInput.Hint = "Alarm Message";
            this.alarmTextInput.HelperText = "Enter Alarm Message";
            this.alarmMessageEntry = new Entry();
            this.alarmMessageEntry.HeightRequest = 40;
            this.alarmMessageEntry.Text = string.Empty;
            this.alarmMessageEntry.Margin = new Thickness(5, 0);
            this.alarmTextInput.Content = this.alarmMessageEntry;
            stack.Add(this.alarmTextInput);
            Label label1 = new Label();
            label1.Text = "Alarm Time";
            label1.FontSize = 12;
            label1.Margin = new Thickness(10, 5);
            stack.Add(label1);
            this.alarmTimePicker.FooterView.Height = 40;
            this.alarmTimePicker.HeightRequest = 280;
            this.alarmTimePicker.Format = Syncfusion.Maui.Picker.PickerTimeFormat.h_mm_tt;
            this.alarmTimePicker.OkButtonClicked += AlarmTimePicker_OkButtonClicked;
            this.alarmTimePicker.CancelButtonClicked += AlarmTimePicker_CancelButtonClicked;
            stack.Add(this.alarmTimePicker);
            stack.VerticalOptions = LayoutOptions.Center;
            this.ContentTemplate = new DataTemplate(() =>
            {
                return stack;
            });

            this.HeaderTemplate = new DataTemplate(() =>
            {
                return new Label() { Text = "Set Alarm", FontSize = 20, HeightRequest = 40, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            });

#if ANDROID || IOS || MACCATALYST
            this.HeightRequest = 470;
#else
            this.HeightRequest = 450;
#endif
            this.WidthRequest = 300;
            this.ShowFooter = false;
            this.ShowHeader = true;
            this.HeaderHeight = 40;
            this.PopupStyle.CornerRadius = new CornerRadius(5);
        }

        private void AlarmTimePicker_CancelButtonClicked(object? sender, EventArgs e)
        {
            this.Reset();
            this.IsOpen = false;
        }

        private void AlarmTimePicker_OkButtonClicked(object? sender, EventArgs e)
        {
            if (this.alarmTimePicker.SelectedTime != null)
            {
                this.OnCreated?.Invoke(new AlarmDetails() { AlarmTime = this.alarmTimePicker.SelectedTime.Value, AlarmMessage = this.alarmMessageEntry.Text == string.Empty ? "No Alarm Message" : this.alarmMessageEntry.Text, IsAlarmEnabled = true }, new EventArgs());

            }
            this.IsOpen = false;
            this.Reset();
        }

        public void Reset()
        {
            this.alarmTimePicker.SelectedTime = DateTime.Now.TimeOfDay;
            this.alarmMessageEntry.Text = string.Empty;
            this.alarmMessageEntry.Placeholder = "Enter Alarm Message";
        }
    }
}
