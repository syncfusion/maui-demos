#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class TextmarkupView : ContentView
{
    internal void Initialize()
    {
        InitializeComponent();
        listView.ItemsSource = new List<AnnotationButtonItem>()
        {
            new AnnotationButtonItem()
            {
                Icon = "\uE760",
                IconName = "Highlight"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE762",
                IconName = "Underline"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE763",
                IconName = "StrikeOut"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE765",
                IconName = "Squiggly"
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
        if(listView != null) 
            listView.SelectedItem = null;
    }
}