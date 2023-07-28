#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ProgressBar;

namespace SampleBrowser.Maui.ProgressBar.SfLinearProgressBar;

public partial class LinearProgressBar : SampleView
{
	public LinearProgressBar()
	{
		InitializeComponent();
	}

    #region Corner radius

    private void CornerRadiusProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
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

    #region Padding progress bar

    private void PaddingProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.PaddingProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.PaddingProgressBar.SetProgress(0, 0);
            }
        });
    }

    #endregion

    #region Range progress bar

    private void RangeProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.RangeProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.RangeProgressBar.SetProgress(0, 0);
            }
        });
    }

    #endregion

    #region Gradient progress bar

    private void GradientProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.GradientProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.GradientProgressBar.SetProgress(0, 0);
            }
        });
    }

    #endregion

    #region Linear Buffer

    private void SecondaryProgressProgressBar_ValueChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.SecondaryProgressProgressBar.SecondaryAnimationDuration = 1000;
                this.SecondaryProgressProgressBar.SecondaryProgress = 100;
                this.SecondaryProgressProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.SecondaryProgressProgressBar.SecondaryAnimationDuration = 0;
                this.SecondaryProgressProgressBar.SecondaryProgress = 0;
                this.SecondaryProgressProgressBar.SetProgress(0, 0);
            }
        });
    }

    #endregion

    #region Segmented corner radius

    private void SegmentedCornerRadiusProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
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