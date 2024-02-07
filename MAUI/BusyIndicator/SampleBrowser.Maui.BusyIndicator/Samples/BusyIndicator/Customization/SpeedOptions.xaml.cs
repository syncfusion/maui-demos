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
    public partial class SpeedOptions : SampleView
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public SpeedOptions()
        {
            InitializeComponent();
        }
        #endregion

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.DurationFactor = e.NewValue;
        }
    }
}