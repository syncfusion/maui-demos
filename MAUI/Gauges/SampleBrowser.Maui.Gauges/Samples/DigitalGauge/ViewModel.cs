#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Gauges.SfDigitalGauge
{
    using Microsoft.Maui.Controls;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        private Color disabledSegmentStroke;
        private Color characterStroke;

        DateTime dateTime = new DateTime();
        private string timeText = DateTime.Now.ToString("HH mm ss");
        private float disabledSegmentAlpha = 0.5f;
        private double characterSpacing = 8;
        private double characterWidth = 30;
        private double characterHeight = 50;

#if ANDROID || IOS
        private double strokeWidth = 3;
#else
        private double strokeWidth = 4;
#endif
        #endregion

        #region Properties

        public double CharacterSpacing
        {
            get
            {
                return this.characterSpacing;
            }
            set
            {
                if (this.characterSpacing != value)
                {
                    this.characterSpacing = value;
                    this.RaisePropertyChanged(nameof(this.CharacterSpacing));
                }
            }
        }

        public double CharacterWidth
        {
            get
            {
                return this.characterWidth;
            }
            set
            {
                if (this.characterWidth != value)
                {
                    this.characterWidth = value;
                    this.RaisePropertyChanged(nameof(this.CharacterWidth));
                }
            }
        }

        public double CharacterHeight
        {
            get
            {
                return this.characterHeight;
            }
            set
            {
                if (this.characterHeight != value)
                {
                    this.characterHeight = value;
                    this.RaisePropertyChanged(nameof(this.CharacterHeight));
                }
            }
        }

        public double StrokeWidth
        {
            get
            {
                return this.strokeWidth;
            }
            set
            {
                if (this.strokeWidth != value)
                {
                    this.strokeWidth = value;
                    this.RaisePropertyChanged(nameof(this.StrokeWidth));
                }
            }
        }

        public string TimeText
        {
            get { return timeText; }
            set
            {
                timeText = value;
                RaisePropertyChanged("TimeText");
            }
        }

        public float DisabledSegmentAlpha
        {
            get { return this.disabledSegmentAlpha; }
            set
            {
                if (this.disabledSegmentAlpha != value)
                {
                    this.disabledSegmentAlpha = value;
                    this.RaisePropertyChanged(nameof(this.DisabledSegmentAlpha));
                }
            }
        }

        public Color DisabledSegmentStroke
        {
            get { return this.disabledSegmentStroke; }
            set
            {
                this.disabledSegmentStroke = value;
                RaisePropertyChanged(nameof(this.DisabledSegmentStroke));
            }
        }

        public Color CharacterStroke
        {
            get { return this.characterStroke; }
            set
            {
                this.characterStroke = value;
                RaisePropertyChanged(nameof(this.CharacterStroke));
            }
        }

        public DateTime GaugeDateTime
        {
            set
            {
                if (dateTime != value)
                {
                    string timeText = DateTime.Now.ToString("HH mm ss");
                    this.TimeText = timeText;
                }
            }
            get
            {
                return dateTime;
            }
        }

        public ObservableCollection<Color> StrokeColors { get; set; }
        public ObservableCollection<Color> DisabledStrokeColors { get; set; }

        #endregion

        #region Constructor

        public ViewModel()
        {
            if (this.isLightTheme)
            {
                this.disabledSegmentStroke = Color.FromArgb("#E7E0EC");
                this.characterStroke = Color.FromArgb("#49454F");
                this.DisabledStrokeColors = new ObservableCollection<Color>
                {
                    Color.FromArgb("#0D71C1"),
                    Color.FromArgb("#33B677"),
                    Color.FromArgb("#C9588E"),
                    Color.FromArgb("#A6B6FF"),
                    Color.FromArgb("#8B40C6"),
                    Color.FromArgb("#FF4E4E"),

                };

                this.StrokeColors = new ObservableCollection<Color>
                {
                    Color.FromArgb("#0D71C1"),
                    Color.FromArgb("#33B677"),
                    Color.FromArgb("#C9588E"),
                    Color.FromArgb("#A6B6FF"),
                    Color.FromArgb("#8B40C6"),
                    Color.FromArgb("#FF4E4E"),
                };
            }
            else
            {
                this.disabledSegmentStroke = Color.FromArgb("#49454F");
                this.characterStroke = Color.FromArgb("#CAC4D0");
                this.StrokeColors = new ObservableCollection<Color>
                {
                    Color.FromArgb("#2196F3"),
                    Color.FromArgb("#25E739"),
                    Color.FromArgb("#E2227E"),
                    Color.FromArgb("#9215F3"),
                    Color.FromArgb("#49454F"),
                    Color.FromArgb("#9B4848"),
                };

                this.DisabledStrokeColors = new ObservableCollection<Color>
                {
                    Color.FromArgb("#2196F3"),
                    Color.FromArgb("#25E739"),
                    Color.FromArgb("#E2227E"),
                    Color.FromArgb("#9215F3"),
                    Color.FromArgb("#49454F"),
                    Color.FromArgb("#9B4848"),
                };
            }

#pragma warning disable CS0612 // Type or member is obsolete
            DigitalGaugeTimer();
#pragma warning restore CS0612 // Type or member is obsolete
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Private Methods

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        [Obsolete]
        private void DigitalGaugeTimer()
        {
            this.GaugeDateTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                this.GaugeDateTime = DateTime.Now;
                return true;
            });
        }

        #endregion
    }
}
