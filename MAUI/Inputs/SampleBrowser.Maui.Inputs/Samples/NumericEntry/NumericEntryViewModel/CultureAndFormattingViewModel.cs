#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Inputs.Samples.NumericEntry.NumericEntryViewModel
{
    public class CultureAndFormattingViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }

       
        private bool showGroupSeparator = true;
        private int minimumIntegerDigits = 5;
        private int minimumFractionDigits = 2;
        private int maximumFractionDigits = 3;
        private string centimeterCustomFormat = string.Empty;
        private string poundCustomFormat = string.Empty;
        private string kilobyteCustomFormat = string.Empty;
        private string litreCustomFormat = string.Empty;


        /// <summary>
        /// Get or Sets the MinimumIntegerDigits
        /// </summary>
        /// <value>The MinimumIntegerDigits.</value>
        public int MinimumIntegerDigits
        {
            get
            {
                return minimumIntegerDigits;
            }
            set
            {
                if (value < 0)
                {
                    minimumIntegerDigits = 0;
                }
                else
                {
                    minimumIntegerDigits = value;
                }
                OnPropertyChanged("MinimumIntegerDigits");
                OnCustomFormatStringChanged();
            }
        }

        /// <summary>
        /// Get or Sets the MinimumFractionDigits
        /// </summary>
        /// <value>The MinimumFractionDigits.</value>
        public int MinimumFractionDigits
        {
            get
            {
                return minimumFractionDigits;
            }
            set
            {
                if (value < 0)
                {
                    minimumFractionDigits = 0;
                }
                else
                {
                    minimumFractionDigits = value;
                }
                OnPropertyChanged("MinimumFractionDigits");
                OnCustomFormatStringChanged();
            }
        }

        /// <summary>
        /// Get or Sets the MaximumFractionDigits
        /// </summary>
        /// <value>The MaximumFractionDigits.</value>
        public int MaximumFractionDigits
        {
            get
            {
                return maximumFractionDigits;
            }
            set
            {
                if (value < 0)
                {
                    maximumFractionDigits = 0;
                }
                else
                {
                    maximumFractionDigits = value;
                }
                OnPropertyChanged("MaximumFractionDigits");
                OnCustomFormatStringChanged();
            }
        }

        /// <summary>
        /// Get or Sets the ShowGroupSeparator
        /// </summary>
        /// <value>The ShowGroupSeparator.</value>
        public bool ShowGroupSeparator
        {
            get
            {
                return showGroupSeparator;
            }
            set
            {
                showGroupSeparator = value;
                OnPropertyChanged("ShowGroupSeparator");
                OnCustomFormatStringChanged();
            }
        }

        /// <summary>
        /// Get or Sets the CentimeterCustomFormat
        /// </summary>
        /// <value>The CentimeterCustomFormat.</value>
        public string CentimeterCustomFormat
        {
            get
            {
                return centimeterCustomFormat;
            }
            set
            {
                centimeterCustomFormat = value;
                this.OnPropertyChanged("CentimeterCustomFormat");
            }
        }

        /// <summary>
        /// Get or Sets the PoundCustomFormat
        /// </summary>
        /// <value>The PoundCustomFormat.</value>
        public string PoundCustomFormat
        {
            get
            {
                return poundCustomFormat;
            }
            set
            {
                poundCustomFormat = value;
                this.OnPropertyChanged("PoundCustomFormat");
            }
        }

        /// <summary>
        /// Get or Sets the KilobyteCustomFormat
        /// </summary>
        /// <value>The KilobyteCustomFormat.</value>
        public string KilobyteCustomFormat
        {
            get
            {
                return kilobyteCustomFormat;
            }
            set
            {
                kilobyteCustomFormat = value;
                this.OnPropertyChanged("KilobyteCustomFormat");
            }
        }

        /// <summary>
        /// Get or Sets the LitreCustomFormat
        /// </summary>
        /// <value>The LitreCustomFormat.</value>
        public string LitreCustomFormat
        {
            get
            {
                return litreCustomFormat;
            }
            set
            {
                litreCustomFormat = value;
                this.OnPropertyChanged("LitreCustomFormat");
            }
        }


        private void OnCustomFormatStringChanged()
        {
            string minIntegerFormat = "";
            string minFractionFormat = "";
            string maxFractionFormat = "";

            //Adding group seperator in custom format string.
            if (this.ShowGroupSeparator)
            {
                minIntegerFormat = minIntegerFormat.PadRight(minIntegerFormat.Length + 1, '#');
                minIntegerFormat = minIntegerFormat.PadRight(minIntegerFormat.Length + 1, ',');
            }

            //Adding minimum integer digits in custom format string.
            if (this.MinimumIntegerDigits > 0)
            {
                minIntegerFormat = minIntegerFormat.PadRight(minIntegerFormat.Length + this.MinimumIntegerDigits, '0');
            }
            else
            {
                minIntegerFormat = minIntegerFormat.PadRight(minIntegerFormat.Length + 1, '#');
            }

            //Adding minimum and maximum fraction digits in custom format string.
            minFractionFormat = minFractionFormat.PadRight(this.MinimumFractionDigits, '0');
            maxFractionFormat = maxFractionFormat.PadRight(Math.Abs(this.MaximumFractionDigits - this.MinimumFractionDigits), '#');

            //Creating the custom format string
            string customFormat = string.Format("{0}.{1}{2} ",
                minIntegerFormat,
                minFractionFormat,
                maxFractionFormat

                );

            //Assigning created custom format string to CustomFormat property.
            this.CentimeterCustomFormat = customFormat + "cm";
            this.PoundCustomFormat = customFormat + "lb";
            this.KilobyteCustomFormat = customFormat + "kb";
            this.LitreCustomFormat = customFormat + "ℓ";
        }

        public CultureAndFormattingViewModel()
        {

        }

    }
}
