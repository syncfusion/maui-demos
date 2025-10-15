#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.ParallaxView.SfParallaxView
{
    public partial class GettingStarted : SampleView
    {
        public GettingStarted()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Label label && label.BindingContext is ListViewInboxInfo item)
            {
                item.IsFavorite = !item.IsFavorite;
            }
        }
    }
}