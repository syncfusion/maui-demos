#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class ActiveHours : SampleView
{
	public ActiveHours()
	{
		InitializeComponent();
        this.GenerateActiveHoursPointers();
    }


    #region Active hours

    private void GenerateActiveHoursPointers()
    {
        double[] inActiveHours = { 2, 3, 4, 6, 7, 9 };
        for (int i = 0; i <= 18; i++)
        {
            LinearContentPointer contentPointer = new();
            contentPointer.Value = i;
            RoundRectangle roundRectangle = new()
            {
                WidthRequest = 10,
                CornerRadius = new CornerRadius(5),
                HeightRequest = 70,
            };
            if (inActiveHours.Contains(i))
            {
                roundRectangle.Fill = new SolidColorBrush(Color.FromArgb("#6405C3DD"));
            }
            else
            {
                roundRectangle.Fill = new SolidColorBrush(Color.FromArgb("#FF05C3DD"));
            }

            contentPointer.Content = roundRectangle;
            activeHoursGauge.MarkerPointers.Add(contentPointer);
        }
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        activeHoursGauge.Handler?.DisconnectHandler();
    }
    #endregion
}