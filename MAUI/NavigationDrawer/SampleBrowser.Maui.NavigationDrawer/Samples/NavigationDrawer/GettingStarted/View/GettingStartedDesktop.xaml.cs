#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.NavigationDrawer.NavigationDrawer;

public partial class GettingStartedDesktop : SampleView
{
    public GettingStartedDesktop()
	{
		InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        navigationDrawer.ToggleSecondaryDrawer();
    }
    private void CloseIcon_Tapped(object sender, TappedEventArgs e)
    {
        navigationDrawer.SecondaryDrawerSettings.IsOpen = false;
    }

    private void CloseBorder_PointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is VisualElement ve)
            VisualStateManager.GoToState(ve, "Hover");
    }

    private void CloseBorder_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is VisualElement ve)
            VisualStateManager.GoToState(ve, "Normal");
    }

}

