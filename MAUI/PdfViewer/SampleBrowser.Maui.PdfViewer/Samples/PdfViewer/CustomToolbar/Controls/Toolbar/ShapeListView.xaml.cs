#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class ShapeListView : ContentView
{
    public ShapeListView()
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