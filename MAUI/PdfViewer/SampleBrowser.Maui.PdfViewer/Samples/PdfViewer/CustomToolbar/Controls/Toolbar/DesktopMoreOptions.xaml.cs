#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class DesktopMoreOptions : ContentView
{
    internal void Initialize()
    {
        InitializeComponent();
        List<AnnotationButtonItem> items = new List<AnnotationButtonItem>()
        {
            new AnnotationButtonItem()
            {
                Icon = "\uE796",
                IconName = "Continuous page"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE798",
                IconName = "Page by page"
            },
        };
        listView.ItemsSource = items;
        listView.SelectedItem = items[0];
    }

    private void SfListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if (e.DataItem is AnnotationButtonItem buttonItem && BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute(buttonItem.IconName);
        }
    }
    internal void DisappearHighlight()
    {
        listView.SelectedItem = null;
    }
}