#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.ProgressBar;

namespace SampleBrowser.Maui.ProgressBar.SfCircularProgressBar;

public partial class CircularProgressBar : SampleView
{
    bool isRunning = true;

    bool isDisposed;

    public CircularProgressBar()
    {
        InitializeComponent();

        #region Custom content

        this.SetCustomContentProgress();
        this.SetVideoPlayerContent();

        #endregion

        #region Circular radius

        this.SetTrackInsideProgress();

        #endregion

        #region Circular segment

        this.SetSegmentedFillingStyleProgress();

        #endregion
    }

    #region custom content

    private void SetVideoPlayerContent()
    {
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(50), () =>
        {
            Dispatcher.DispatchAsync(() =>
            {
                this.SetProgress();
            });

            return isRunning;
        });
    }

    private void SetProgress()
    {
        this.VideoPlayerProgressBar.Progress += 0.5;
        if (this.VideoPlayerProgressBar.Progress == 100)
        {
            this.VideoPlayerProgressBar.Progress = 0;
        }
    }

    private void SetCustomContentProgress()
    {
        double progress = 0;

        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(50), () =>
        {
            Dispatcher.DispatchAsync(() =>
            {
                this.CustomContentCircularProgressBar.Progress = progress += 1;
                this.CustomContentProgressBarLabel.Text = progress + "%";
            });

            return progress < 74;
        });
    }

    private void PlayButton_Clicked(object sender, EventArgs e)
    {
        isRunning = !isRunning;
        this.PlayButton.Text = isRunning ? "\ue769" : "\ue737";
        if (isRunning)
        {
            this.SetVideoPlayerContent();
        }
    }

    #endregion

    #region Circular radius

    private void TrackOutsideProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.TrackOutsideProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.TrackOutsideProgressBar.SetProgress(0, 0);
            }
        });
    }

    private void FilledIndicatorProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.FilledIndicatorProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.FilledIndicatorProgressBar.SetProgress(0, 0);
            }
        });
    }

    private void ThinTrackStyle_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.ThinTrackStyle.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.ThinTrackStyle.SetProgress(0, 0);
            }
        });
    }

    private void SetTrackInsideProgress()
    {
        double progress = 0;
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(50), () =>
        {
            Dispatcher.DispatchAsync(() =>
            {
                this.TrackInsideProgressBar.Progress = progress += 0.25;
                this.TrackInsideProgressBarProgressLabel.Text = (int)progress + "%";
                if (progress == 100)
                {
                    this.TrackInsideProgressBar.Progress = progress = 0;
                }
            });

            return !isDisposed;
        });
    }

    #endregion

    #region Circular segment

    private void SegmentedCircularProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(75))
            {
                this.SegmentedCircularProgressBar.SetProgress(0, 0);
            }

            if (e.Progress.Equals(0))
            {
                this.SegmentedCircularProgressBar.Progress = 75;
            }
        });
    }

    private void SegmentedPaddingCircularProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(75))
            {
                this.SegmentedPaddingCircularProgressBar.SetProgress(0, 0);
            }

            if (e.Progress.Equals(0))
            {
                this.SegmentedPaddingCircularProgressBar.Progress = 75;
            }
        });
    }

    private void SetSegmentedFillingStyleProgress()
    {
        double progress = 0;

        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(300), () =>
        {
            if (progress == 100)
            {
                this.SegmentedFillingStyle.Progress = progress = 0;
            }

            Dispatcher.DispatchAsync(() =>
            {
                this.SegmentedFillingStyle.Progress = progress += 5;
            });

            return !isDisposed;
        });
    }

    #endregion

    #region Custom angle

    private void AngleCustomizationProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.AngleCustomizationProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.AngleCustomizationProgressBar.SetProgress(0, 0);
            }
        });
    }

    #endregion

    #region Range colors

    private void RangeColorProgressBar_ProgressChanged(object sender, ProgressValueEventArgs e)
    {
        Dispatcher.DispatchAsync(() =>
        {
            if (e.Progress.Equals(0))
            {
                this.RangeColorProgressBar.Progress = 100;
            }

            if (e.Progress.Equals(100))
            {
                this.RangeColorProgressBar.SetProgress(0, 0);
            }
        });
    }

    #endregion

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        isRunning = false;
        isDisposed = true;
        this.DeterminateCircularProgressBar.Handler?.DisconnectHandler();
        this.IndeterminateCircularProgressBar.Handler?.DisconnectHandler();
        this.CustomContentCircularProgressBar.Handler?.DisconnectHandler();
        this.VideoPlayerProgressBar.Handler?.DisconnectHandler();
        this.TrackOutsideProgressBar.Handler?.DisconnectHandler();
        this.FilledIndicatorProgressBar.Handler?.DisconnectHandler();
        this.TrackInsideProgressBar.Handler?.DisconnectHandler();
        this.ThinTrackStyle.Handler?.DisconnectHandler();
        this.SegmentedCircularProgressBar.Handler?.DisconnectHandler();
        this.SegmentedPaddingCircularProgressBar.Handler?.DisconnectHandler();
        this.SegmentedFillingStyle.Handler?.DisconnectHandler();
        this.AngleCustomizationProgressBar.Handler?.DisconnectHandler();
        this.RangeColorProgressBar.Handler?.DisconnectHandler();
    }
}