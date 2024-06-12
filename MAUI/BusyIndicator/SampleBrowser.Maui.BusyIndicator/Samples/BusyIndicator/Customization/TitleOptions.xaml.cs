#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.BusyIndicator.SfBusyIndicator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleOptions : SampleView
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public TitleOptions()
        {
            InitializeComponent();
        }
        #endregion

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.FontSize = e.NewValue * 10;
        }

        private void Slider_ValueChanged_1(object sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.TitleSpacing = e.NewValue * 10;
        }

        private void RadioButton_CheckedChanged(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
        {
            if (e.CurrentItem != null && this.busyIndicator != null)
            {
                if (e.CurrentItem.Text == "None")
                {
                    this.busyIndicator.FontAttributes = FontAttributes.None;
                }
                else if (e.CurrentItem.Text == "Bold")
                {
                    this.busyIndicator.FontAttributes = FontAttributes.Bold;
                }
                else if (e.CurrentItem.Text == "Italic")
                {
                    this.busyIndicator.FontAttributes = FontAttributes.Italic;
                }
            }
           
           
        }

        private void RadioButton_CheckedChanged_1(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
        {
            if (e.CurrentItem != null && this.busyIndicator != null)
            {
                if (e.CurrentItem.Text == "Top")
                {
                    this.busyIndicator.TitlePlacement = Syncfusion.Maui.Core.BusyIndicatorTitlePlacement.Top;
                }
                else if (e.CurrentItem.Text == "Bottom")
                {
                    this.busyIndicator.TitlePlacement = Syncfusion.Maui.Core.BusyIndicatorTitlePlacement.Bottom;

                }
            }
        }
    }
}