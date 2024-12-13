#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Rotator.Rotator;

public partial class GettingStartedDesktop : SampleView
{
    [Obsolete]
    public GettingStartedDesktop()
    {
        InitializeComponent();
    }


    private void stripPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (stripPosition != null)
        {
            if (stripPosition.SelectedIndex == 0)
            {
                sfRotator.NavigationStripPosition = Syncfusion.Maui.Core.Rotator.NavigationStripPosition.Bottom;
            }
            else if (stripPosition.SelectedIndex == 1)
            {
                sfRotator.NavigationStripPosition = Syncfusion.Maui.Core.Rotator.NavigationStripPosition.Top;
            }
            else if (stripPosition.SelectedIndex == 2)
            {
                sfRotator.NavigationStripPosition = Syncfusion.Maui.Core.Rotator.NavigationStripPosition.Left;
            }
            else
            {
                sfRotator.NavigationStripPosition = Syncfusion.Maui.Core.Rotator.NavigationStripPosition.Right;
            }

        }
    }
    private void stripMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (stripMode != null)
        {
            if (stripMode.SelectedIndex == 0)
            {
                sfRotator.NavigationStripMode = Syncfusion.Maui.Core.Rotator.NavigationStripMode.Thumbnail;
            }
            else
            {
                sfRotator.NavigationStripMode = Syncfusion.Maui.Core.Rotator.NavigationStripMode.Dots;
            }
        }
    }

    private void dotPlacement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dotsPlacement != null)
        {
            if (dotsPlacement.SelectedIndex == 0)
            {
                sfRotator.DotPlacement = Syncfusion.Maui.Core.Rotator.DotsPlacement.None;
            }
            else if (dotsPlacement.SelectedIndex == 1)
            {
                sfRotator.DotPlacement = Syncfusion.Maui.Core.Rotator.DotsPlacement.Default;
            }
            else
            {
                sfRotator.DotPlacement = Syncfusion.Maui.Core.Rotator.DotsPlacement.OutSide;
            }
        }
    }
}