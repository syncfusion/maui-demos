using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Carousel.Carousel
{
    public partial class UIVirtualization : SampleView
    {
        CarouselViewModel viewModel = new CarouselViewModel();

        public UIVirtualization()
        {
            InitializeComponent();
            button.Clicked += Button_Clicked;
#if ANDROID
            button.Text = "Tap to View 1000+ Icons";
#endif
        }

        private void Button_Clicked(object? sender, EventArgs e)
        {
            this.Content = new UIVirtualizationDesktop();
            this.Content.BindingContext = viewModel;
        }
    }
}
