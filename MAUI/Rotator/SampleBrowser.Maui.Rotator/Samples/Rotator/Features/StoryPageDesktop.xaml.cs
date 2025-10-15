#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Rotator.Rotator;

public partial class StoryPageDesktop : SampleView
{
	public StoryPageDesktop()
	{
		InitializeComponent();
		picker.SelectedIndex = 0;

    }

    private void sfRotator_SelectionChanged(object sender, Syncfusion.Maui.Core.Rotator.SelectedIndexChangedEventArgs e)
    {
        int count = (int)(e.Index + 1) * 25;
        if (sfRotator != null)
        {
            progressBar.Progress = count;
        }
    }

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		if(picker != null)
		{
			if(picker.SelectedIndex == 0)
			{
				sfRotator.DotPlacement = Syncfusion.Maui.Core.Rotator.DotsPlacement.None;
                progressBar.IsVisible = true;
            }
			else if(picker.SelectedIndex == 1)
			{
                sfRotator.DotPlacement = Syncfusion.Maui.Core.Rotator.DotsPlacement.Default;
				progressBar.IsVisible = false;
            }
			else
			{
                sfRotator.DotPlacement = Syncfusion.Maui.Core.Rotator.DotsPlacement.OutSide;
                progressBar.IsVisible = false;
            }
		}
    }
}