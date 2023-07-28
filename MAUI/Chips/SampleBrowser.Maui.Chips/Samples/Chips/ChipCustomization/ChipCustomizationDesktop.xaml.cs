#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Windows.Input;

namespace SampleBrowser.Maui.Chips.SfChip;

public partial class ChipCustomizationDesktop : SampleView
{
    public ChipCustomizationDesktop()
	{
		InitializeComponent();
    }
        private void TextColorSegment_Clicked(object sender, EventArgs e)
    {
        chipgroup.ChipTextColor = Color.FromArgb("#E5E4E2");
        chipgroup.SelectionIndicatorColor = Color.FromArgb("#E5E4E2");
        chipgroup.CloseButtonColor = Color.FromArgb("#E5E4E2");
    }


    private void TextColorSegment_Clicked_1(object sender, EventArgs e)
    {
        chipgroup.ChipTextColor = Color.FromArgb("#c6c6c6");
        chipgroup.SelectionIndicatorColor = Color.FromArgb("#c6c6c6");
        chipgroup.CloseButtonColor = Color.FromArgb("#c6c6c6");
    }

    private void TextColorSegment_Clicked_2(object sender, EventArgs e)
    {
        chipgroup.ChipTextColor = Color.FromArgb("#538eed");
        chipgroup.SelectionIndicatorColor = Color.FromArgb("#538eed");
        chipgroup.CloseButtonColor = Color.FromArgb("#538eed");
    }

    private void TextColorSegment_Clicked_3(object sender, EventArgs e)
    {
        chipgroup.ChipTextColor = Color.FromArgb("#af2463");
        chipgroup.SelectionIndicatorColor = Color.FromArgb("#af2463");
        chipgroup.CloseButtonColor = Color.FromArgb("#af2463");
    }

    private void TextColorSegment_Clicked_4(object sender, EventArgs e)
    {
        chipgroup.ChipTextColor = Color.FromArgb("#000000");
        chipgroup.SelectionIndicatorColor = Color.FromArgb("#000000");
        chipgroup.CloseButtonColor = Color.FromArgb("#000000");
    }
    private void BackgroundColorSegment_Clicked(object sender, EventArgs e)
    {
        chipgroup.ChipBackground = Color.FromArgb("#E5E4E2");
    }

    private void BackgroundColorSegment_Clicked_1(object sender, EventArgs e)
    {
        chipgroup.ChipBackground = Color.FromArgb("#c6c6c6");
    }

    private void BackgroundColorSegment_Clicked_2(object sender, EventArgs e)
    {
        chipgroup.ChipBackground = Color.FromArgb("#538eed");
    }

    private void BackgroundColorSegment_Clicked_3(object sender, EventArgs e)
    {
        chipgroup.ChipBackground = Color.FromArgb("#af2463");
    }

    private void BackgroundColorSegment_Clicked_4(object sender, EventArgs e)
    {
        chipgroup.ChipBackground = Color.FromArgb("#000000");
    }

    private void BorderColorSegment_Clicked(object sender, EventArgs e)
    {
        chipgroup.ChipStroke = Color.FromArgb("#E5E4E2");
    }
    private void BorderColorSegment_Clicked_1(object sender, EventArgs e)
    {
        chipgroup.ChipStroke = Color.FromArgb("#c6c6c6");
    }

    private void BorderColorSegment_Clicked_2(object sender, EventArgs e)
    {
        chipgroup.ChipStroke = Color.FromArgb("#538eed");
    }

    private void BorderColorSegment_Clicked_3(object sender, EventArgs e)
    {
        chipgroup.ChipStroke = Color.FromArgb("#af2463");
    }

    private void BorderColorSegment_Clicked_4(object sender, EventArgs e)
    {
        chipgroup.ChipStroke = Color.FromArgb("#000000");
    }
}