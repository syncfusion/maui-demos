#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.ComponentModel;
using System.Globalization;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class RadialRangeSlider : SampleView
{
#nullable enable
    private readonly RadialRangeSliderViewModel? viewModel;
#nullable disable
    public RadialRangeSlider()
	{
		InitializeComponent();
        if (radialRangeSlider != null && radialRangeSlider.BindingContext is RadialRangeSliderViewModel)
        {
            viewModel = radialRangeSlider.BindingContext as RadialRangeSliderViewModel;
        }
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        radialRangeSlider.Handler?.DisconnectHandler();
    }

    private void firstMarker_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
    {
        if (this.viewModel != null)
        {
            this.range.StartValue = this.viewModel.FirstMarkerValue = e.Value;
            double value = Math.Abs(this.viewModel.FirstMarkerValue - this.viewModel.SecondMarkerValue);
            this.CalculateMinutes(value);
        }
    }

    private void firstMarker_ValueChanging(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
    {
        if (sender is MarkerPointer pointer && pointer.BindingContext is RadialRangeSliderViewModel context)
        {
            if (e.NewValue >= context.SecondMarkerValue || Math.Abs(e.NewValue - context.FirstMarkerValue) > 1)
            {
                e.Cancel = true;
            }
        }
    }

    private void secondMarker_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
    {
        if (this.viewModel != null)
        {
            this.range.EndValue = this.viewModel.SecondMarkerValue = e.Value;
            double value = Math.Abs(this.viewModel.SecondMarkerValue - this.viewModel.FirstMarkerValue);
            this.CalculateMinutes(value);
        }
    }

    private void secondMarker_ValueChanging(object sender, Syncfusion.Maui.Gauges.ValueChangingEventArgs e)
    {
        if (sender is MarkerPointer pointer && pointer.BindingContext is RadialRangeSliderViewModel context)
        {
            if (e.NewValue <= context.FirstMarkerValue || Math.Abs(e.NewValue - context.SecondMarkerValue) > 1)
            {
                e.Cancel = true;
            }
        }
    }

    private void CalculateMinutes(double value)
    {
        int hour = Convert.ToInt32(Math.Floor(value));
        float digit = hour / 10f;
        bool isHourSingleDigit = digit >= 1 ? false : true;

        var min = Math.Floor((value - hour) * 60);
        digit = (float)min / 10f;
        bool isMinuteSingleDigit = digit >= 1 ? false : true;

        string hourValue = isHourSingleDigit ? "0" + hour : hour.ToString(CultureInfo.CurrentCulture);
        string minutesValue = isMinuteSingleDigit ? "0" + min : min.ToString(CultureInfo.CurrentCulture);
        if (this.viewModel != null)
            this.viewModel.AnnotationString = hourValue + "hr " + minutesValue + "m";
    }
}

public class RadialRangeSliderViewModel : INotifyPropertyChanged
{
    private double firstMarkerValue = 2;

    private double secondMarkerValue = 8;

    private string annotationString = "06" + "hr" + " 00" + "m";


    public double FirstMarkerValue
    {
        get { return this.firstMarkerValue; }
        set
        {
            this.firstMarkerValue = value;
        }
    }

    public double SecondMarkerValue
    {
        get { return this.secondMarkerValue; }
        set
        {
            this.secondMarkerValue = value;
        }
    }

    public string AnnotationString
    {
        get
        {
            return this.annotationString;
        }

        set
        {
            this.annotationString = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnnotationString)));
        }
    }

#nullable disable
    public event PropertyChangedEventHandler PropertyChanged;
#nullable enable
}