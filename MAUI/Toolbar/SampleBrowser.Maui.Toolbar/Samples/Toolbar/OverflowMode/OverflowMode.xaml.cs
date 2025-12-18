#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Toolbar.SfToolbar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Toolbar;

    public partial class OverflowMode : SampleView
    {
        public OverflowMode()
        {
            InitializeComponent();
        }

        private async void HorizontalToolbar_Tapped(object sender, ToolbarTappedEventArgs e)
        {
            if (e.NewToolbarItem != null)
            {
                if (e.NewToolbarItem.Name == "Alignment")
                {
                    var item = e.NewToolbarItem?.OverlayToolbar;

                    if (!this.layout.Children.Contains(item))
                    {
                        this.layout.Children.Add(item);
                    }

                    await Task.Delay(1000);
                    (sender as SfToolbar)?.ClearSelection();
                }
            }
        }
    }
}