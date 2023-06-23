#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.BulletChart.BulletChart;

public partial class BulletChartHorizontalDemo : ContentView
{
	public BulletChartHorizontalDemo()
	{
		InitializeComponent();
	}

	private void SfLinearGauge_LabelCreated(object sender, LabelCreatedEventArgs e)
	{
        e.Text += "%";
    }

	private void SfLinearGauge_LabelCreated_1(object sender, LabelCreatedEventArgs e)
	{
		if(e.Text == "0")
		{
			e.Text = "$" + e.Text;
        }
		else
		{
            e.Text = "$" + e.Text + "K";
        }
		
    }

	private void SfLinearGauge_LabelCreated_2(object sender, LabelCreatedEventArgs e)
	{
		e.Text = "$" + e.Text;
    }
}