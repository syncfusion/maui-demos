#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class AnnotationToolbar : ContentView
{
	public AnnotationToolbar(CustomToolbarViewModel bindingContext)
	{
        BindingContext = bindingContext;
        InitializeComponent();

		if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
        {
            endscrollabilityIndicator.IsVisible = false;
            startscrollabilityIndicator.IsVisible = false;
        }
#if IOS
        startscrollabilityIndicator.Opacity = 0.5;
        endscrollabilityIndicator.Opacity = 0.5;
#endif

    }
    private void ScrollEvent(object sender, ScrolledEventArgs e)
    {

        if (e.ScrollX == 0)
        {
            startscrollabilityIndicator.IsVisible = false;
            endscrollabilityIndicator.IsVisible = true;
        }
        else if (e.ScrollX + scrollView.Width >= scrollView.ContentSize.Width)
        {
            startscrollabilityIndicator.IsVisible = true;
            endscrollabilityIndicator.IsVisible = false;
        }
        else
        {
            startscrollabilityIndicator.IsVisible = true;
            endscrollabilityIndicator.IsVisible = true;
        }

    }
}