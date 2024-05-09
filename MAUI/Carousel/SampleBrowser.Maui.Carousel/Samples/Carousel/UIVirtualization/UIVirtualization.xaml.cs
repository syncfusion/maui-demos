#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
