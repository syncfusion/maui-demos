using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Gauges;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Interaction : SampleView
    {
        private RadialRangeSliderViewModel viewModel;
        public Interaction()
        {
            InitializeComponent();
            viewModel = radialRangeSlider.BindingContext as RadialRangeSliderViewModel;
        }

        private void rangePointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue < 6)
            {
                e.Cancel = true;
            }
        }

        private void rangePointer_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.markerPointer.Value = e.Value - 2;
        }


        private void firstMarker_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.range.StartValue = this.viewModel.FirstMarkerValue = e.Value;
            double value = Math.Abs(this.viewModel.FirstMarkerValue - this.viewModel.SecondMarkerValue);
            this.CalculateMinutes(value);
        }

        private void firstMarker_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            var context = (sender as MarkerPointer).BindingContext as RadialRangeSliderViewModel;
            if (e.NewValue >= context.SecondMarkerValue || Math.Abs(e.NewValue - context.FirstMarkerValue) > 1)
            {
                e.Cancel = true;
            }
        }

        private void secondMarker_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.range.EndValue = this.viewModel.SecondMarkerValue = e.Value;
            double value = Math.Abs(this.viewModel.SecondMarkerValue - this.viewModel.FirstMarkerValue);
            this.CalculateMinutes(value);
        }

        private void secondMarker_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            var context = (sender as MarkerPointer).BindingContext as RadialRangeSliderViewModel;
            if (e.NewValue <= context.FirstMarkerValue || Math.Abs(e.NewValue - context.SecondMarkerValue) > 1)
            {
                e.Cancel = true;
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AnnotationString"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}