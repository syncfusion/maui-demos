#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.Sliders.SfSlider
{
    public class SliderLabelCustomizationSampleViewModel : INotifyPropertyChanged
    {
        private readonly SolidColorBrush[] brushes;

        private int value = 3;

        private SolidColorBrush activeFill = null!;

        private SolidColorBrush overlayFill = null!;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SliderLabelCustomizationSampleViewModel()
        {
            brushes = new SolidColorBrush[4]
            {
                new SolidColorBrush(Color.FromRgba(237, 86, 59, 255)),
                new SolidColorBrush(Color.FromRgba(246, 213, 92, 255)),
                new SolidColorBrush(Color.FromRgba(60, 174, 163, 255)),
                new SolidColorBrush(Color.FromRgba(32, 100, 155, 255))
            };

            UpdateActiveBrush();
        }

        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                UpdateActiveBrush();
            }
        }

        public SolidColorBrush ActiveFill
        {
            get { return activeFill; }
            set
            {
                activeFill = value;
                OnPropertyChanged(nameof(ActiveFill));
            }
        }

        public SolidColorBrush OverlayFill
        {
            get { return overlayFill; }
            set
            {
                overlayFill = value;
                OnPropertyChanged(nameof(OverlayFill));
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateActiveBrush()
        {
            ActiveFill = brushes[Value - 1];

            OverlayFill = new SolidColorBrush(activeFill.Color.WithAlpha(0.24f));
        }
    }
}
