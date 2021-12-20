#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfEffectsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScaleAnimation : SampleView
    {
        public ScaleAnimation()
        {
            InitializeComponent();
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {
            var effectsView = sender as Syncfusion.Maui.Core.SfEffectsView;
            if (effectsView.ScaleFactor == 0.85)
            {
                effectsView.ScaleFactor = 1;
            }
            else
            {

                effectsView.ScaleFactor = 0.85;
            }
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double scaleFactorValue = 0.85;

        public double ScaleFactorValue
        {
            get
            {
                return scaleFactorValue;
            }
            set
            {
                if (scaleFactorValue != value)
                {
                    scaleFactorValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ScaleFactorValue"));
                }
            }
        }

        private double scaleDuration = 150;

        public double ScaleDuration
        {
            get
            {
                return scaleDuration;
            }
            set
            {
                if (scaleDuration != value)
                {
                    scaleDuration = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ScaleDuration"));
                }
            }
        }

        private SfEffects touchUpEffectsValue = SfEffects.Scale;
        public SfEffects TouchUpEffectsValue
        {
            get
            {
                return touchUpEffectsValue;
            }
            set
            {
                touchUpEffectsValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TouchUpEffectsValue"));
            }
        }
    }

    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Syncfusion.Maui.Core.SfEffectsView effectsView = parameter as Syncfusion.Maui.Core.SfEffectsView;
            return effectsView.ScaleFactor == 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}