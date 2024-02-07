#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Buttons.CheckBox;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class CustomizationsDesktop : SampleView
{
    ViewModel viewModel;
    public CustomizationsDesktop()
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
    private void CheckedColorSegment_Clicked(object sender, EventArgs e)
    {
        checkBox.CheckedColor = Color.FromArgb("#E5E4E2");
    }

    private void CheckedColorSegment_Clicked_1(object sender, EventArgs e)
    {
        checkBox.CheckedColor = Color.FromArgb("#c6c6c6");
    }

    private void CheckedColorSegment_Clicked_2(object sender, EventArgs e)
    {
        checkBox.CheckedColor = Color.FromArgb("#538eed");
    }

    private void CheckedColorSegment_Clicked_3(object sender, EventArgs e)
    {
        checkBox.CheckedColor = Color.FromArgb("#af2463");
    }

    private void CheckedColorSegment_Clicked_4(object sender, EventArgs e)
    {
        checkBox.CheckedColor = Color.FromArgb("#000000");
    }

    private void UncheckedColorSegment_Clicked(object sender, EventArgs e)
    {

        checkBox.UncheckedColor = Color.FromArgb("#E5E4E2");
    }
    private void UncheckedColorSegment_Clicked_1(object sender, EventArgs e)
    {
        checkBox.UncheckedColor = Color.FromArgb("#c6c6c6");
    }

    private void UncheckedColorSegment_Clicked_2(object sender, EventArgs e)
    {
        checkBox.UncheckedColor = Color.FromArgb("#538eed");
    }

    private void UncheckedColorSegment_Clicked_3(object sender, EventArgs e)
    {
        checkBox.UncheckedColor = Color.FromArgb("#af2463");
    }

    private void UncheckedColorSegment_Clicked_4(object sender, EventArgs e)
    {
        checkBox.UncheckedColor = Color.FromArgb("#000000");
    }
}