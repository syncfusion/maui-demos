#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class ShapeListView : ContentView
{
    internal void Initialize()
    {
        InitializeComponent();
        listView.ItemsSource = new List<AnnotationButtonItem>()
        {
            new AnnotationButtonItem()
            {
                Icon = "\uE731",
                IconName = "Square"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE73f",
                IconName = "Circle"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE73d",
                IconName = "Line"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE73c",
                IconName = "Arrow"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE786",
                IconName = "Polyline"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE789",
                IconName = "Polygon"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE783",
                IconName = "Cloud"
            },
        };
    }

    private void SfListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (e.DataItem is AnnotationButtonItem textMarkupDetails && BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute(textMarkupDetails.IconName);
        }
    }
    internal void DisappearHighlight()
    {
        listView.SelectedItem = null;
    }
}