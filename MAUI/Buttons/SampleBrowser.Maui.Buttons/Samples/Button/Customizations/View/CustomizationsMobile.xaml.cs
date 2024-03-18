#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Buttons.Button;

namespace SampleBrowser.Maui.Buttons.Button;

public partial class CustomizationsMobile : SampleView
{
    ViewModel viewModel;

    public CustomizationsMobile()
	{
		InitializeComponent();
        viewModel = new ViewModel();
        this.BindingContext = viewModel;    
    }
    private void TextColorSegment_Clicked(object sender, EventArgs e)
    {
        viewModel.TextColor = Color.FromArgb("#E5E4E2");
    }

    private void TextColorSegment_Clicked_1(object sender, EventArgs e)
    {
        viewModel.TextColor = Color.FromArgb("#c6c6c6");
    }

    private void TextColorSegment_Clicked_2(object sender, EventArgs e)
    {
        viewModel.TextColor = Color.FromArgb("#538eed");
    }

    private void TextColorSegment_Clicked_3(object sender, EventArgs e)
    {
        viewModel.TextColor = Color.FromArgb("#af2463");
    }

    private void TextColorSegment_Clicked_4(object sender, EventArgs e)
    {
        viewModel.TextColor = Color.FromArgb("#000000");
        
    }
    private void BackgroundColorSegment_Clicked(object sender, EventArgs e)
    {
        cartButton.Background = Color.FromArgb("#E5E4E2");
    }

    private void BackgroundColorSegment_Clicked_1(object sender, EventArgs e)
    {
        cartButton.Background = Color.FromArgb("#c6c6c6");
    }

    private void BackgroundColorSegment_Clicked_2(object sender, EventArgs e)
    {
        cartButton.Background = Color.FromArgb("#538eed");
    }

    private void BackgroundColorSegment_Clicked_3(object sender, EventArgs e)
    {
        cartButton.Background = Color.FromArgb("#af2463");
    }

    private void BackgroundColorSegment_Clicked_4(object sender, EventArgs e)
    {
        cartButton.Background = Color.FromArgb("#000000");
    }

    private void BorderColorSegment_Clicked(object sender, EventArgs e)
    {

        cartButton.Stroke = Color.FromArgb("#E5E4E2");
    }
    private void BorderColorSegment_Clicked_1(object sender, EventArgs e)
    {
        cartButton.Stroke = Color.FromArgb("#c6c6c6");
    }

    private void BorderColorSegment_Clicked_2(object sender, EventArgs e)
    {
        cartButton.Stroke = Color.FromArgb("#538eed");
    }

    private void BorderColorSegment_Clicked_3(object sender, EventArgs e)
    {
        cartButton.Stroke = Color.FromArgb("#af2463");
    }

    private void BorderColorSegment_Clicked_4(object sender, EventArgs e)
    {
        cartButton.Stroke = Color.FromArgb("#000000");
    }
}