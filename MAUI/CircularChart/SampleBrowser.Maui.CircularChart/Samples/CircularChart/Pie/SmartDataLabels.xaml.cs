#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public partial class SmartDataLabels: SampleView
    {
        public SmartDataLabels()
        {
            InitializeComponent();
            dataLabel.SelectedIndex = 0;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
#if IOS || ANDROID
            if (IsCardView)
            {
                Chart.WidthRequest = 350;
                Chart.HeightRequest = 400;
                Chart.VerticalOptions = LayoutOptions.Start;
            }
#endif
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }

        private void smartDataLabel_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var value = dataLabel.SelectedIndex;
            switch (value)
            {
                case 0:
                    dataLabelSetting.SmartLabelAlignment = SmartLabelAlignment.Shift;
                    break;
                case 1:
                    dataLabelSetting.SmartLabelAlignment = SmartLabelAlignment.None;
                    break;
                case 2:
                    dataLabelSetting.SmartLabelAlignment = SmartLabelAlignment.Hide;
                    break;
            }
        }
    }
}
