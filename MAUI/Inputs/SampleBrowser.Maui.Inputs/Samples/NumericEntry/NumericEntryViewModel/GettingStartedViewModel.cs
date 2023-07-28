#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.Inputs.Samples.NumericEntry.NumericEntryViewModel
{
    public class GettingStartedViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;  
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }

        private string placeholderText = "Enter Fahrenheit value";       
        private double minimum = -100;
        private double maximum = 1000;       
        private double? celsiusValue = 0.0d;
        private double? kelvinValue = 273.15d;
        private double? rankineValue = 491.67d;
        private double? fahrenheitValue = 32.0d;
        private bool allowNull = true;


        /// <summary>
        /// Get or Sets the PlaceholderText
        /// </summary>
        /// <value>The PlaceholderText.</value>
        public string PlaceholderText
        {
            get
            {
                return placeholderText;
            }
            set
            {
                placeholderText = value;
                OnPropertyChanged("PlaceholderText");
            }
        }

        /// <summary>
        /// Get or Sets the FahrenheitValue
        /// </summary>
        /// <value>The FahrenheitValue.</value>
        public double? FahrenheitValue
        {
            get
            {
                return fahrenheitValue;
            }
            set
            {
                fahrenheitValue = value;
                OnPropertyChanged("FahrenheitValue");
                OnFahrenheitValueChanged();

            }
        }

        /// <summary>
        /// Get or Sets the CelsiusValue
        /// </summary>
        /// <value>The CelsiusValue.</value>
        public double? CelsiusValue
        {
            get
            {
                return celsiusValue;
            }
            set
            {
                celsiusValue = value;
                OnPropertyChanged("CelsiusValue");
            }
        }

        /// <summary>
        /// Get or Sets the KelvinValue
        /// </summary>
        /// <value>The KelvinValue.</value>
        public double? KelvinValue
        {
            get
            {
                return kelvinValue;
            }
            set
            {
                kelvinValue = value;
                OnPropertyChanged("KelvinValue");
            }
        }

        /// <summary>
        /// Get or Sets the RankineValue
        /// </summary>
        /// <value>The RankineValue.</value>
        public double? RankineValue
        {
            get
            {
                return rankineValue;
            }
            set
            {
                rankineValue = value;
                OnPropertyChanged("RankineValue");
            }
        }       

        /// <summary>
        /// Get or Sets the Minimum
        /// </summary>
        /// <value>The Minimum.</value>
        public double Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                minimum = value;
                OnPropertyChanged("Minimum");
            }
        }

        /// <summary>
        /// Get or Sets the Maximum
        /// </summary>
        /// <value>The Maximum.</value>
        public double Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                maximum = value;
                OnPropertyChanged("Maximum");
            }
        }


        /// <summary>
        /// Get or Sets the AllowNull
        /// </summary>
        /// <value>The AllowNull.</value>
        public bool AllowNull
        {
            get
            {
                return allowNull;
            }
            set
            {
                allowNull = value;
                OnPropertyChanged("AllowNull");
            }
        }

        private void OnFahrenheitValueChanged()
        {
            CelsiusValue = (this.fahrenheitValue - 32) * 5 / 9;
            KelvinValue = (this.fahrenheitValue - 32) * 5 / 9 + 273.15;
            RankineValue = this.fahrenheitValue + 459.67;
        }

        public GettingStartedViewModel()
        {
            
        }

    }

}
