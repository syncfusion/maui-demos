#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view.
        /// </summary>
        private SfPicker? picker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private SfSwitch? showHeaderSwitch, showFooterSwitch;

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

#if IOS || MACCATALYST
            Border border = sampleView.Content.FindByName<Border>("border");
            border.IsVisible = true;
            border.Stroke = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker1");
#else
            Frame frame = sampleView.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
            frame.BorderColor = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.picker = sampleView.Content.FindByName<SfPicker>("Picker");
#endif
            this.picker.HeaderView.Height = 50;
            this.picker.HeaderView.Text = "Select a Country";

            this.picker.SelectionView.Padding = 0;
            this.picker.SelectionView.CornerRadius = 0;

            this.showHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<SfSwitch>("showFooterSwitch");

            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.StateChanged += ShowHeaderSwitch_Toggled;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.StateChanged += ShowFooterSwitch_Toggled;
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
        }

        public GettingStartedBehavior()
        {
        }
    }
}