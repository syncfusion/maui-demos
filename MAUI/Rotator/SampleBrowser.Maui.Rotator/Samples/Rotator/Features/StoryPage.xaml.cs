using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Rotator.Rotator;

public partial class StoryPage : SampleView
{
    public StoryPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        StoryPageMobile storyPageMobile = new StoryPageMobile();
        this.Content = storyPageMobile;
#endif
    }
}
