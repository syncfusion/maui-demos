using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCircularChart
{
	public partial class DataLabel : SampleView
	{
		public DataLabel()
		{
			InitializeComponent();
			if (RunTimeDevice.IsMobileDevice())
			{
				Chart.HeightRequest = 510;
			}
            else
            {
				Chart.VerticalOptions = LayoutOptions.FillAndExpand;
            }
		}

        public override void OnDisappearing()
        {
            base.OnDisappearing();

			Chart.Handler?.DisconnectHandler();
        }

		public override void OnExpandedViewDisappearing(View view)
		{
			base.OnExpandedViewDisappearing(view);

			view.Handler?.DisconnectHandler();
		}
	}
}
