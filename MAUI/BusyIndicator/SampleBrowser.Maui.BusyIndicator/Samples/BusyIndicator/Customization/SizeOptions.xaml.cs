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
    public partial class SizeOptions : SampleView
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public SizeOptions()
        {
            InitializeComponent();
        }
        #endregion

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.busyIndicator.SizeFactor = e.NewValue;
        }
    }
}