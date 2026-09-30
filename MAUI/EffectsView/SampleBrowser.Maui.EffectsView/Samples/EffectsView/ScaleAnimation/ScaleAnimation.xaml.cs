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

        private void AnimationCompleted(object? sender, EventArgs e)
        {
            Syncfusion.Maui.Core.SfEffectsView? effectsView = null;

            if (sender is Syncfusion.Maui.Core.SfEffectsView view)
            {
                effectsView = view;
            }

            if (effectsView?.ScaleFactor == 0.85)
            {
                effectsView.ScaleFactor = 1;
            }
            else
            {
                effectsView?.ScaleFactor = 0.85;
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