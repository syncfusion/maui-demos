#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.RadialMenu;

namespace SampleBrowser.Maui.RadialMenu.SfRadialMenu;
public partial class Customization : SampleView
{
    //MainViewModel mainViewModel;

    [Obsolete]
	public Customization()
	{
		InitializeComponent();
        //mainViewModel = new MainViewModel();
        //sfRotator.BindingContext = mainViewModel;

      //  Device.StartTimer(new TimeSpan(0, 0, 0, 0, 800), TimerElapsed);
    }

    //private void SfRadialMenuItem_ItemTapped(object sender, Syncfusion.Maui.RadialMenu.ItemTappedEventArgs e)
    //{
    //    SfRadialMenuItem item1 = (SfRadialMenuItem)sender;
    //    mainViewModel.RotatorCollection[sfRotator.SelectedIndex].TeamColor = item1.BackgroundColor;
    //    teamUSARadialMenu.IsOpen = false;
    //}

    //[Obsolete]
    //private bool TimerElapsed()
    //{
    //    Device.BeginInvokeOnMainThread(() =>
    //    {
    //        teamUSARadialMenu.IsOpen = true;
    //    });

    //    return false;
    //}
}
