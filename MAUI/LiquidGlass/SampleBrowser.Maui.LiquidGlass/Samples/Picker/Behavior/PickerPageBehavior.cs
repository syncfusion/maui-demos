#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Picker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfPicker
{
    internal class PickerPageBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view.
        /// </summary>
        private Syncfusion.Maui.Picker.SfPicker? picker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? showHeaderSwitch, showFooterSwitch, showEnableLoopingSwitch;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.picker = sampleView.Content.FindByName<Syncfusion.Maui.Picker.SfPicker>("Picker");
            this.picker.HeaderView.Height = 50;
            this.picker.HeaderView.Text = "Select a Country";
            this.picker.SelectionView.CornerRadius = 20;
            this.showHeaderSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showFooterSwitch");
            this.showEnableLoopingSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("enableLoopingSwitch");

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
    }
}
