#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class DesktopZoomPercentage : ContentView
{
    internal void Initialize()
    {
        InitializeComponent();
        SeparatorLine.BackgroundColor = Color.FromArgb("#CAC4D0");
        List<AnnotationButtonItem> items = new List<AnnotationButtonItem>()
        {
            new AnnotationButtonItem()
            {
                Icon="\ue79b",
                IconName="Fit to Width"
            },
            new AnnotationButtonItem()
            {
                Icon="\ue79d",
                IconName="Fit to Page"
            },

        };
        listView.ItemsSource = items;
        List<AnnotationButtonItem> zoomList=new List<AnnotationButtonItem>() 
        {
            new AnnotationButtonItem()
            {
                Icon = "25%",
            },
            new AnnotationButtonItem()
            {
                Icon = "50%",
            },
            new AnnotationButtonItem()
            {
                Icon = "100%",
            },
            new AnnotationButtonItem()
            {
                Icon = "125%",
            },
            new AnnotationButtonItem()
            {
                Icon = "150%",
            }
        };
        ZoomlistView.ItemsSource = zoomList;
    }

    private void SfListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (e.DataItem is AnnotationButtonItem buttonItem && BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute(buttonItem.Icon);
        }
    }
    internal void DisappearHighlight()
    {
        listView.SelectedItem = null;
        ZoomlistView = null;
    }
}