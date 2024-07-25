#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Dispatching;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class Clock : SampleView
{
    private double seconds;
    private bool canStopTimer;
    public Clock()
	{
		InitializeComponent();
        this.seconds = 0;
        StartTimer();
    }

    private void StopTimer()
    {
        canStopTimer = true;
    }

    private async void StartTimer()
    {
        await Task.Delay(500);
        if (Application.Current != null)
        {
            Application.Current.Dispatcher.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateTick);
        }

        canStopTimer = false;
    }

    private bool UpdateTick()
    {
        if (canStopTimer) return false;
        this.seconds += 0.2;
        if (this.seconds > 12)
        {
            this.secondsPointer.EnableAnimation = false;
            this.seconds = 0;
            this.secondsPointer.Value = seconds;
            this.secondsPointer.EnableAnimation = true;
            this.seconds += 0.2;
            this.secondsPointer.Value = seconds;
        }
        else
        {
            this.secondsPointer.Value = seconds;
        }
        return true;
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTimer();
        clockGauge.Handler?.DisconnectHandler();
    }
}

public class ClockViewModel
{
    private readonly double hour;
    private readonly double minute;

    public ClockViewModel()
    {
        var date = DateTime.Now;
        hour = date.Hour > 12 ? date.Hour - 12 : date.Hour;
        hour = hour + date.Minute / 60d;
        minute = date.Minute / 60d * 12d;
    }

    public double Hour
    {
        get
        {
            return hour;
        }
    }

    public double Minute
    {
        get
        {
            return minute;
        }
    }
}