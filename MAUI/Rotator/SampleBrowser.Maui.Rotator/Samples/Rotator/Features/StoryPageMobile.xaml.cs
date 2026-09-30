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

    private void sfRotator_SelectionChanged(object? sender, Syncfusion.Maui.Core.Rotator.SelectedIndexChangedEventArgs e)
    {
        count = (int)(e.Index + 1) * 25;
        if (sfRotator != null)
		{
            ProgressAnimation(count);
        }      
    }
}