#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Chips.SfChip;

public partial class ChipCustomizationMobile : SampleView
{
    public ChipCustomizationMobile()
    {
        InitializeComponent();
    }

    private void TextColorSegment_Clicked(object sender, EventArgs e)
    {
        chip.TextColor = Color.FromArgb("#f2f3f4");
        chip.SelectionIndicatorColor = Color.FromArgb("#f2f3f4");
        chip.CloseButtonColor = Color.FromArgb("#f2f3f4");
    }


    private void TextColorSegment_Clicked_1(object sender, EventArgs e)
    {
        chip.TextColor = Color.FromArgb("#c6c6c6");
        chip.SelectionIndicatorColor = Color.FromArgb("#c6c6c6");
        chip.CloseButtonColor = Color.FromArgb("#c6c6c6");
    }

    private void TextColorSegment_Clicked_2(object sender, EventArgs e)
    {
        chip.TextColor = Color.FromArgb("#538eed");
        chip.SelectionIndicatorColor = Color.FromArgb("#538eed");
        chip.CloseButtonColor = Color.FromArgb("#538eed");
    }

    private void TextColorSegment_Clicked_3(object sender, EventArgs e)
    {
        chip.TextColor = Color.FromArgb("#af2463");
        chip.SelectionIndicatorColor = Color.FromArgb("#af2463");
        chip.CloseButtonColor = Color.FromArgb("#af2463");
    }

    private void TextColorSegment_Clicked_4(object sender, EventArgs e)
    {
        chip.TextColor = Color.FromArgb("#000000");
        chip.SelectionIndicatorColor = Color.FromArgb("#000000");
        chip.CloseButtonColor = Color.FromArgb("#000000");
    }
    private void BackgroundColorSegment_Clicked(object sender, EventArgs e)
    {
        chip.Background = new SolidColorBrush(Color.FromArgb("#f2f3f4"));
    }

    private void BackgroundColorSegment_Clicked_1(object sender, EventArgs e)
    {
        chip.Background = new SolidColorBrush(Color.FromArgb("#c6c6c6"));

    }

    private void BackgroundColorSegment_Clicked_2(object sender, EventArgs e)
    {
        chip.Background = new SolidColorBrush(Color.FromArgb("#538eed"));
    }

    private void BackgroundColorSegment_Clicked_3(object sender, EventArgs e)
    {
        chip.Background = new SolidColorBrush(Color.FromArgb("#af2463"));
    }

    private void BackgroundColorSegment_Clicked_4(object sender, EventArgs e)
    {
        chip.Background = new SolidColorBrush(Color.FromArgb("#000000"));
    }

    private void BorderColorSegment_Clicked(object sender, EventArgs e)
    {
        chip.Stroke = Color.FromArgb("#f2f3f4");
    }
    private void BorderColorSegment_Clicked_1(object sender, EventArgs e)
    {
        chip.Stroke = Color.FromArgb("#c6c6c6");
    }

    private void BorderColorSegment_Clicked_2(object sender, EventArgs e)
    {
        chip.Stroke = Color.FromArgb("#538eed");
    }

    private void BorderColorSegment_Clicked_3(object sender, EventArgs e)
    {
        chip.Stroke = Color.FromArgb("#af2463");
    }

    private void BorderColorSegment_Clicked_4(object sender, EventArgs e)
    {
        chip.Stroke = Color.FromArgb("#000000");
    }
}