#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfEffectsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RotateAnimation : SampleView
    {
        public RotateAnimation()
        {
            InitializeComponent();
        }

        private void RotationEffectsViewAnimationCompleted(object sender, EventArgs e)
        {
            if (RotationEffectsView.Angle == 180)
            {
                RotationEffectsView.Angle = 0;
            }
            else
            {
                RotationEffectsView.Angle = 180;
            }
        }
    }
}
