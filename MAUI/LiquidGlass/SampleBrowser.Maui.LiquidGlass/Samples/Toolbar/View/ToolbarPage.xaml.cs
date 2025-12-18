#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfToolbar;

public partial class ToolbarPage : SampleView
{
	public ToolbarPage()
	{
		InitializeComponent();
	}

    private async void HorizontalToolbar_Tapped(object sender, Syncfusion.Maui.Toolbar.ToolbarTappedEventArgs e)
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
                (sender as Syncfusion.Maui.Toolbar.SfToolbar)?.ClearSelection();
            }
        }
    }

#if IOS || MACCATALYST
 
        /// <summary>
        /// Overrides <see cref="OnAppearing"/> to apply platform-specific resource overrides for Kanban control.
        /// </summary>
        public override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current?.Resources is not ResourceDictionary resources)
            {
                return;
            }
 
            if (resources == null || resources.MergedDictionaries.Contains(resources))
            {
                return;
            }
 
            var toolbarResource = new ResourceDictionary
            {
                { "SfToolbarNormalBackground", Colors.Transparent },
            };
 
            resources.MergedDictionaries.Add(toolbarResource);
        }
#endif
}