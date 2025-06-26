#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Inputs;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleBrowser.Maui.Inputs.SfRating
{
    /// <summary>
    /// Rating Getting Started Sample
    /// </summary>
    public partial class RatingGettingStarted : SampleView
    {
        /// <summary>
        /// Initialize the GettingStarted sample
        /// </summary>
        public RatingGettingStarted()
        {
            InitializeComponent();
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = sender as Syncfusion.Maui.Inputs.SfComboBox;
            itemSizeSlider.IsEnabled = true;
            itemSpacingSlider.IsEnabled = true;
            if (comboBox != null)
            {
                if (comboBox.SelectedIndex == 0)
                {
                    rating.RatingShape = RatingShape.Circle;
                }

                else if (comboBox.SelectedIndex == 1)
                {
                    rating.RatingShape = RatingShape.Diamond;
                }
                else if (comboBox.SelectedIndex == 2)
                {
                    rating.RatingShape = RatingShape.Heart;

                }
                else if (comboBox.SelectedIndex == 3)
                {
                    rating.RatingShape = RatingShape.Star;
                }

                else if (comboBox.SelectedIndex == 4)
                {
                    itemSizeSlider.Value = 35;
                    itemSizeSlider.IsEnabled = false;
                    itemSpacingSlider.IsEnabled = false;
                    rating.RatingShape = RatingShape.Custom;
                    rating.Path = "M17.5 35.5C19.9063 35.5 21.875 33.8846 21.875 31.9103H13.125C13.125 33.8846 15.0719 35.5 17.5 35.5ZM30.625 24.7308V15.7564C30.625 10.2462 27.0375 5.63334 20.7812 4.41282V3.19231C20.7812 1.70256 19.3156 0.5 17.5 0.5C15.6844 0.5 14.2188 1.70256 14.2188 3.19231V4.41282C7.94063 5.63334 4.375 10.2282 4.375 15.7564V24.7308L0 28.3205V30.1154H35V28.3205L30.625 24.7308Z";
                }
            }
        }

        private void SfComboBox_ItemCount_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = sender as Syncfusion.Maui.Inputs.SfComboBox;

            if (comboBox != null)
            {
                if (comboBox.SelectedIndex == 0)
                    rating.ItemCount = 1;
                else if (comboBox.SelectedIndex == 1)
                    rating.ItemCount = 2;
                else if (comboBox.SelectedIndex == 2)
                    rating.ItemCount = 3;
                else if (comboBox.SelectedIndex == 3)
                    rating.ItemCount = 4;
                else if (comboBox.SelectedIndex == 4)
                    rating.ItemCount = 5;
            }
            if (rating.Value > rating.ItemCount)
                rating.Value = rating.ItemCount;
        }

        private void SfComboBox_Precision_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = sender as Syncfusion.Maui.Inputs.SfComboBox;

            if (comboBox != null)
            {
                if (comboBox.SelectedIndex == 0)
                {
                    rating.Precision = Precision.Standard;
                }
                else if (comboBox.SelectedIndex == 1)
                {
                    rating.Precision = Precision.Half;
                }
                else if (comboBox.SelectedIndex == 2)
                {
                    rating.Precision = Precision.Exact;
                }
                rating.Value = Convert.ToDouble(valueLabel.Text);
            }
        }

        private void Rating_ValueChanged(object sender, Syncfusion.Maui.Inputs.ValueChangedEventArgs e)
        {
            if (rating.Precision == Precision.Standard)
            {
                valueLabel.Text = Math.Ceiling(e.Value).ToString();
            }
            else if (rating.Precision == Precision.Half)
            {
                double floorValue = Math.Floor(e.Value);
                double decimalValue = e.Value % 1;
                valueLabel.Text = (decimalValue != 0 ? (decimalValue > 0.5 ? floorValue + 1 : floorValue + 0.5) : floorValue).ToString();
            }
            else
            {
                valueLabel.Text = Math.Round(rating.Value,1).ToString();
            }
        }
    }
    public class RatingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string[] PrecisionList { get; set; } = new string[] { "Standard", "Half", "Exact" };

        public string[] ItemCountList { get; set; } = new string[] { "1", "2", "3", "4", "5" };

        public string[] RatingShapeList { get; set; } = new string[] { "Circle", "Diamond", "Heart", "Star", "Custom" };

        private int itemSpacingValue;

        public int ItemSpacingValue
        {
            get { return itemSpacingValue; }
            set
            {
                itemSpacingValue = value;
                OnPropertyChanged();
            }
        }

        private double itemSpacingSliderValue = 10;

        public double ItemSpacingSliderValue
        {
            get { return itemSpacingSliderValue; }
            set
            {
                itemSpacingSliderValue = value;
                ItemSpacingValue = Convert.ToInt32(itemSpacingSliderValue);
            }
        }

        private double itemSizeValue = 50;

        public double ItemSizeValue
        {
            get { return itemSizeValue; }
            set { itemSizeValue = value; OnPropertyChanged(); }
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}