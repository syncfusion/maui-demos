#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.LiquidGlass.SfLinearProgressBar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.ProgressBar;

    /// <summary>
    /// Represents a page that displays and manages a Linear Progress Bar within the application.
    /// </summary>
    public partial class LinearProgressBarPage : SampleView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearProgressBarPage"/> class.
        /// </summary>
        public LinearProgressBarPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Corner radius

        /// <summary>
        /// Invoked when the corner radius progress bar progress value is changed.
        /// </summary>
        /// <param name="sender">The linear progress bar object.</param>
        /// <param name="e">The progress value event args.</param>
        private void OnCornerRadiusProgressBarProgressChanged(object sender, ProgressValueEventArgs e)
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                if (e.Progress.Equals(0))
                {
                    this.CornerRadiusProgressBar.Progress = 100;
                }

                if (e.Progress.Equals(100))
                {
                    this.CornerRadiusProgressBar.SetProgress(0, 0);
                }
            });
        }

        #endregion

        #region Segmented corner radius

        /// <summary>
        /// Invoked when the segemented corner radius progress bar progress value is changed.
        /// </summary>
        /// <param name="sender">The linear progress bar object.</param>
        /// <param name="e">The progress value event args.</param>
        private void OnSegmentedCornerRadiusProgressBarProgressChanged(object sender, ProgressValueEventArgs e)
        {
            this.Dispatcher.DispatchAsync(() =>
            {
                if (e.Progress.Equals(0))
                {
                    this.SegmentedCornerRadiusProgressBar.Progress = 100;
                }

                if (e.Progress.Equals(100))
                {
                    this.SegmentedCornerRadiusProgressBar.SetProgress(0, 0);
                }
            });
        }

        #endregion
    }
}