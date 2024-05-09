#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Rotator.Rotator;

public partial class StoryPageMobile : SampleView
{
    int count = 0;
	public StoryPageMobile()
	{
        InitializeComponent();
        sfRotator.SelectedIndex = 0;
        sfRotator.Loaded += StoryPageMobile_Loaded;     
    }

    private void StoryPageMobile_Loaded(object? sender, EventArgs e)
    {
        sfRotator.Loaded -= StoryPageMobile_Loaded;
        ProgressAnimation(25);
    }

    private void ProgressAnimation(int end)
    {
        if(end == 25)
        {
            progressBar.SetProgress(0, 0, Easing.Linear);
        }
        
        if(progressBar.Progress > end)
        {
            progressBar.SetProgress(end - 25, 0, Easing.Linear);
        }
        
        progressBar.Progress = end;
    }

    private void sfRotator_SelectionChanged(object sender, Syncfusion.Maui.Core.Rotator.SelectedIndexChangedEventArgs e)
    {
        count = (int)(e.Index + 1) * 25;
        if (sfRotator != null)
		{
            ProgressAnimation(count);
        }      
    }
}