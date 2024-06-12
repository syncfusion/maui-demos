#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ProgressBar.SfStepProgressBar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.ProgressBar;

    /// <summary>
    /// Represents the behavior for order tracking sample.
    /// </summary>
    internal class OrderTrackingBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The step progress bar instance.
        /// </summary>
        private SfStepProgressBar? stepProgress;

        /// <summary>
        /// The switch to show or hide the tool tip.
        /// </summary>
        private SfSwitch? showToolTipSwitch;

        #endregion

        #region Override methods

        /// <summary>
        /// Invoked when behavior is attached to a view.
        /// </summary>
        /// <param name="sampleView">The sample view to which the behavior is attached.</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

            this.stepProgress = sampleView.Content.FindByName<SfStepProgressBar>("stepProgress");
            this.showToolTipSwitch = sampleView.FindByName<SfSwitch>("showToolTipSwitch");

            if (this.showToolTipSwitch != null)
            {
                this.showToolTipSwitch.StateChanged += this.OnShowToolTipSwitchStateChanged;
            }
        }

        /// <summary>
        /// Invoked when behavior is detached from a view.
        /// </summary>
        /// <param name="sampleView">The sample view from which the behavior is detached.</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.stepProgress != null)
            {
                this.stepProgress = null;
            }

            if (this.showToolTipSwitch != null)
            {
                this.showToolTipSwitch.StateChanged -= this.OnShowToolTipSwitchStateChanged;
            }
        }

        #endregion

        #region Property Changed
        private void OnShowToolTipSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.stepProgress == null)
            {
                return;
            }

            if (e.NewValue.HasValue)
            {
                if (e.NewValue.Value)
                {
                    this.stepProgress.Margin = new Thickness(24, 0, 0, 0);
                }
                else
                {
                    this.stepProgress.Margin = new Thickness(24, 10, 0, 0);
                }

                this.stepProgress.ShowToolTip = e.NewValue.Value;
            }
        }

        #endregion
    }
}
