#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Shimmer;

namespace SampleBrowser.Maui.Shimmer.SfShimmer;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GettingStarted : SampleView
{
	public GettingStarted()
	{
		InitializeComponent();
        tabView.SelectedIndex = 0;
    }

    private void ShimmerColor_Clicked(object sender, EventArgs e)
    {
        viewModel.ShimmerColor = ((Button)sender).BackgroundColor;
        int index = viewModel.ShimmerColors.IndexOf(viewModel.ShimmerColor);
        viewModel.WaveColor = viewModel.WaveColors[index];
    }

    private void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        string type = viewModel.ShimmerTypes[(int)e.NewIndex];

        switch (type)
        {
            case "Persona":
                shimmer.Type = ShimmerType.CirclePersona;
                break;
            case "Profile":
                shimmer.Type = ShimmerType.Profile;
                break;
            case "Article":
                shimmer.Type = ShimmerType.Article;
                break;
            case "Video":
                shimmer.Type = ShimmerType.Video;
                break;
            case "Feed":
                shimmer.Type = ShimmerType.Feed;
                break;
            case "Shopping":
                shimmer.Type = ShimmerType.Shopping;
                break;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Border border = (Border)sender;
        foreach (var item in ColorGrid.Children)
        {
            if (item == border)
            {
                viewModel.ShimmerColor = border.BackgroundColor;
                int index = viewModel.ShimmerColors.IndexOf(viewModel.ShimmerColor);
                viewModel.WaveColor = viewModel.WaveColors[index];
                ((Border)item).StrokeThickness = 1;
                continue;
            }

            ((Border)item).StrokeThickness = 0;
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        Border border = (Border)sender;
        int waveIndex = directionGrid.Children.IndexOf(border);
        shimmer.WaveDirection = (ShimmerWaveDirection)waveIndex;
        foreach (var item in directionGrid.Children)
        {
            if (item == border)
            {
                ((Border)item).StrokeThickness = 1;
                continue;
            }

           ((Border)item).StrokeThickness = 0;
        }
    }
}