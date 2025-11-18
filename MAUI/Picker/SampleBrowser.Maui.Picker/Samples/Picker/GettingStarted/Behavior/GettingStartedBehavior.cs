#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfPicker
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Picker;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Inputs;
    using System.Collections.ObjectModel;
    using System;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view.
        /// </summary>
        private SfPicker? picker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private SfSwitch? showHeaderSwitch, showFooterSwitch, showEnableLoopingSwitch;

        /// <summary>
        /// The text display mode combo box.
        /// </summary>
        private SfComboBox? textDisplayComboBox;

        /// <summary>
        /// The text display to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? textDisplay;

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

#if MACCATALYST
            Border border = sampleView.Content.FindByName<Border>("border");
            border.IsVisible = true;
            border.Stroke = Colors.Transparent;
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker1");
#elif IOS
            Border border = sampleView.Content.FindByName<Border>("border1");
            border.IsVisible = true;
            border.Stroke = Colors.Transparent;
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker3");
#elif ANDROID
            Border frame = sampleView.Content.FindByName<Border>("frame1");
            frame.IsVisible = true;
            frame.Stroke = Colors.Transparent;
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker2");
#else
            Border frame = sampleView.Content.FindByName<Border>("frame");
            frame.IsVisible = true;
            frame.Stroke = Colors.Transparent;
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker");
#endif
            this.picker.HeaderView.Height = 50;
            this.picker.HeaderView.Text = "Select a Country";

            this.picker.SelectionView.Padding = 0;
            this.picker.SelectionView.CornerRadius = 0;
            this.picker.SelectedTextStyle.TextColor = isLightTheme ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#381E72");

            this.showHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<SfSwitch>("showFooterSwitch");
            this.showEnableLoopingSwitch = sampleView.Content.FindByName<SfSwitch>("enableLoopingSwitch");

            textDisplay = new ObservableCollection<object>()
            {
                "Default", "Fade", "Shrink", "FadeAndShrink"
            };

            this.textDisplayComboBox = sampleView.Content.FindByName<SfComboBox>("textDisplayComboBox");
            this.textDisplayComboBox.ItemsSource = textDisplay;
            this.textDisplayComboBox.SelectedIndex = 0;
            this.textDisplayComboBox.SelectionChanged += TextdisplayComboBox_SelectionChanged;

            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.StateChanged += ShowHeaderSwitch_Toggled;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.StateChanged += ShowFooterSwitch_Toggled;
            }

            if (this.showEnableLoopingSwitch != null)
            {
                this.showEnableLoopingSwitch.StateChanged += ShowEnableLoopingSwitch_Toggled;
            }
        }

        /// <summary>
        /// The text display combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void TextdisplayComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.picker == null || e.AddedItems == null || textDisplayComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            switch (format)
            {
                case "Default":
                    this.picker.TextDisplayMode = PickerTextDisplayMode.Default;
                    break;

                case "Fade":
                    this.picker.TextDisplayMode = PickerTextDisplayMode.Fade;
                    break;

                case "Shrink":
                    this.picker.TextDisplayMode = PickerTextDisplayMode.Shrink;
                    break;

                case "FadeAndShrink":
                    this.picker.TextDisplayMode = PickerTextDisplayMode.FadeAndShrink;
                    break;

            }
        }

        /// <summary>
        /// Method for show header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowHeaderSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.picker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.picker.HeaderView.Height = 50;
                    this.picker.HeaderView.Text = "Select a Country";
                }
                else if (e.NewValue.Value == false)
                {
                    this.picker.HeaderView.Height = 0;
                }
            }
        }

        /// <summary>
        /// Method for show column header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowColumnHeaderSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.picker != null && e.NewValue.HasValue)
            {
                this.picker.ColumnHeaderView.Height = e.NewValue.Value == true ? 40 : 0;
            }
        }

        /// <summary>
        /// Method for show footer switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowFooterSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.picker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.picker.FooterView.Height = 40;
                }
                else if (e.NewValue.Value == false)
                {
                    this.picker.FooterView.Height = 0;
                }
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.StateChanged -= ShowHeaderSwitch_Toggled;
                this.showHeaderSwitch = null;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.StateChanged -= ShowFooterSwitch_Toggled;
                this.showFooterSwitch = null;
            }

            if (this.showEnableLoopingSwitch != null)
            {
                this.showEnableLoopingSwitch.StateChanged -= ShowEnableLoopingSwitch_Toggled;
                this.showEnableLoopingSwitch = null;
            }
        }

        /// <summary>
        /// Method for enable looping switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowEnableLoopingSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.picker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.picker.EnableLooping = true;
                }
                else if (e.NewValue.Value == false)
                {
                    this.picker.EnableLooping = false;
                }
            }
        }

        public GettingStartedBehavior()
        {
        }
    }
}