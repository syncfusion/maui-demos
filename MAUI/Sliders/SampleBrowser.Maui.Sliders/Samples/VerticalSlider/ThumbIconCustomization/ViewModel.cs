using System.ComponentModel;

namespace SampleBrowser.Maui.Sliders.VerticalSlider
{
    public class ThumbIconViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private double sliderValue = 5.0;
        private string iconText = "";

        public ThumbIconViewModel()
        {
            UpdateThumbIconText();
        }

        public double Value
        {
            get { return sliderValue; }
            set
            {
                if (sliderValue != value)
                {
                    sliderValue = value;
                    UpdateThumbIconText();
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public string IconText
        {
            get { return iconText; }
            set
            {
                if (iconText != value)
                {
                    iconText = value;
                    OnPropertyChanged(nameof(IconText));
                }
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateThumbIconText()
        {
            if (sliderValue == 0)
            {
                IconText = "\ue702";
            }
            else if (sliderValue == 10)
            {
                IconText = "\ue703";
            }
            else
            {
                IconText = "\ue705";
            }
        }
    }
}