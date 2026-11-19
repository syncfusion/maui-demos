using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Core;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public partial class CenterElevation : SampleView
    {
        public CenterElevation()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
#if IOS
            if (IsCardView)
            {
                Chart1.WidthRequest = 350;
                Chart1.HeightRequest = 400;
                Chart1.VerticalOptions = LayoutOptions.Start;
            }
#endif
        }

    }
}

