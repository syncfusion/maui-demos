#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Gauges;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Axis : SampleView
    {
        public Axis()
        {
            InitializeComponent();
        }
    }

    public class RadialAxisExt : RadialAxis
    {
        protected override List<GaugeLabelInfo> GenerateVisibleLabels()
        {
            List<GaugeLabelInfo> customLabels = new List<GaugeLabelInfo>();
            for (int i = 0; i < 9; i++)
            {
                double value = CalculateLabelValue(i);
                GaugeLabelInfo label = new GaugeLabelInfo
                {
                    Value = value,
                    Text = value.ToString(CultureInfo.CurrentCulture)
                };
                customLabels.Add(label);
            }

            return customLabels;
        }

        public override double ValueToFactor(double value)
        {
            if (value >= 0 && value <= 2)
            {
                return value * 0.125 / 2;
            }
            else if (value > 2 && value <= 5)
            {
                return ((value - 2) * 0.125 / (5 - 2)) + (1 * 0.125);
            }
            else if (value > 5 && value <= 10)
            {
                return ((value - 5) * 0.125 / (10 - 5)) + (2 * 0.125);
            }
            else if (value > 10 && value <= 20)
            {
                return ((value - 10) * 0.125 / (20 - 10)) + (3 * 0.125);
            }
            else if (value > 20 && value <= 30)
            {
                return ((value - 20) * 0.125 / (30 - 20)) + (4 * 0.125);
            }
            else if (value > 30 && value <= 50)
            {
                return ((value - 30) * 0.125 / (50 - 30)) + (5 * 0.125);
            }
            else if (value > 50 && value <= 100)
            {
                return ((value - 50) * 0.125 / (100 - 50)) + (6 * 0.125);
            }
            else if (value > 100 && value <= 150)
            {
                return ((value - 100) * 0.125 / (150 - 100)) + (7 * 0.125);
            }
            else
            {
                return 1;
            }
        }

        private static double CalculateLabelValue(double value)
        {
            if (value == 0)
            {
                return 0;
            }
            else if (value == 1)
            {
                return 2;
            }
            else if (value == 2)
            {
                return 5;
            }
            else if (value == 3)
            {
                return 10;
            }
            else if (value == 4)
            {
                return 20;
            }
            else if (value == 5)
            {
                return 30;
            }
            else if (value == 6)
            {
                return 50;
            }
            else if (value == 7)
            {
                return 100;
            }
            else
            {
                return 150;
            }
        }
    }
}