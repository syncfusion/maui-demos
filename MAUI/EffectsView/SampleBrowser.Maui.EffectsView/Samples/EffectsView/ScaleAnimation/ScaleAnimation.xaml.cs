#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;
using System.ComponentModel;
using System.Globalization;

namespace SampleBrowser.Maui.EffectsView.SfEffectsView
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
            var effectsView = (Syncfusion.Maui.Core.SfEffectsView)sender;
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
        public event PropertyChangedEventHandler? PropertyChanged;

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
                    IsImageSelected = scaleFactorValue == 1;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScaleFactorValue)));
                }
            }
        }

        private bool _isImageSelected;

        public bool IsImageSelected
        {
            get
            {
                return _isImageSelected;
            }
            set
            {
                if (_isImageSelected != value)
                {
                    _isImageSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsImageSelected)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScaleDuration)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TouchUpEffectsValue)));
            }
        }
    }
}