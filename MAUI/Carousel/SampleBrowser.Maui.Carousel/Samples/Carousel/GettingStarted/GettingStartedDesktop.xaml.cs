#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Carousel.Carousel;
public partial class GettingStartedDesktop : SampleView
{
	public GettingStartedDesktop()
	{
		InitializeComponent();
	}

    private void carousel_SelectionChanged(object sender, Syncfusion.Maui.Core.Carousel.SelectionChangedEventArgs e)
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