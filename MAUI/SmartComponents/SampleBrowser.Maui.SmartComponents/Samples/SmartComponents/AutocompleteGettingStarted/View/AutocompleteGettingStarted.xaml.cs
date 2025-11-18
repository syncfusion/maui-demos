#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents;

public partial class AutocompleteGettingStarted : SampleView
{
	public AutocompleteGettingStarted()
	{
		InitializeComponent();
	}

    public override void OnAppearing()
    {
        base.OnAppearing();
        BubbleAnimation();
    }

    private void BubbleAnimation()
    {
        Animation animation = new Animation();
        // Image animations
        var bubbleEffect = new Animation(v => aiImage.Scale = v, 1, 1.10, Easing.CubicInOut);
        var fadeEffect = new Animation(v => aiImage.Opacity = v, 1, 0.7, Easing.CubicInOut);

        // Border animations
        var borderScaleEffect = new Animation(v => aiBorder.Scale = v, 1, 1.10, Easing.CubicInOut);
        var borderOpacityEffect = new Animation(v => aiBorder.Opacity = v, 1, 0.7, Easing.CubicInOut);

        // Adding image animations
        animation.Add(0, 0.5, bubbleEffect);
        animation.Add(0, 0.5, fadeEffect);
        animation.Add(0.5, 1, new Animation(v => aiImage.Scale = v, 1.10, 1, Easing.CubicInOut));
        animation.Add(0.5, 1, new Animation(v => aiImage.Opacity = v, 0.7, 1, Easing.CubicInOut));

        // Adding border animations
        animation.Add(0, 0.5, borderScaleEffect);
        animation.Add(0, 0.5, borderOpacityEffect);
        animation.Add(0.5, 1, new Animation(v => aiBorder.Scale = v, 1.10, 1, Easing.CubicInOut));
        animation.Add(0.5, 1, new Animation(v => aiBorder.Opacity = v, 0.7, 1, Easing.CubicInOut));

        // Commit animation
        animation.Commit(this, "BubbleEffect", length: 1500, easing: Easing.CubicInOut, repeat: () => true);

    }
}