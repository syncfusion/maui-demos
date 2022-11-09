#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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


        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            this.busyIndicator.FontAttributes = FontAttributes.None;
        }

        private void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            this.busyIndicator.FontAttributes = FontAttributes.Bold;
        }

        private void RadioButton_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            this.busyIndicator.FontAttributes = FontAttributes.Italic;
        }

        private void RadioButton_CheckedChanged_3(object sender, CheckedChangedEventArgs e)
        {
            this.busyIndicator.TitlePlacement = Syncfusion.Maui.Core.BusyIndicatorTitlePlacement.Top;
        }

        private void RadioButton_CheckedChanged_4(object sender, CheckedChangedEventArgs e)
        {
            this.busyIndicator.TitlePlacement = Syncfusion.Maui.Core.BusyIndicatorTitlePlacement.Bottom;
        }

        private void Slider_ValueChanged_1(object sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.TitleSpacing = e.NewValue * 10;
        }
    }
}