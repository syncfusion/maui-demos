using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Carousel.Carousel
{
    public partial class LoadMore : SampleView
    {
        public LoadMore()
        {
            InitializeComponent();
#if ANDROID || IOS
            this.Content = new LoadMoreMobile();
#elif WINDOWS || MACCATALYST
            this.Content = new LoadMoreDesktop();
#endif
        }
    }
}
