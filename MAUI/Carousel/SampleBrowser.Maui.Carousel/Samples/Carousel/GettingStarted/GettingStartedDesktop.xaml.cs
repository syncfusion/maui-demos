using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Carousel.Carousel;
public partial class GettingStartedDesktop : SampleView
{
	public GettingStartedDesktop()
	{
		InitializeComponent();
	}

    private void carousel_SelectionChanged(object? sender, Syncfusion.Maui.Core.Carousel.SelectionChangedEventArgs e)
    {
		if(sender is Syncfusion.Maui.Carousel.SfCarousel sfCarousel)
		{
            var selectedItem = sfCarousel.ItemsSource.ElementAt(sfCarousel.SelectedIndex) as CarouselModel;
            if(selectedItem != null)
            {
                countryDescriptionLabel.Text =  selectedItem.Description;
                countryNameLabel.Text = selectedItem.Name;
            }
        }
    }
}