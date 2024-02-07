#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class StickyNoteListView : ContentView
{
    
    public StickyNoteListView()
	{
		InitializeComponent();
        listView.ItemsSource = new List<AnnotationButtonItem>()
        {
            new AnnotationButtonItem()
            {
                Icon = "\uE775",
                IconName = "Note"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE779",
                IconName = "Insert"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE711",
                IconName = "Comment"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE777",
                IconName = "Key"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE778",
                IconName = "Help"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE776",
                IconName = "Paragraph"
            },
            new AnnotationButtonItem()
            {
                Icon = "\uE77a",
                IconName = "New Paragraph"
            },
        };
    }

    private void SfListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        
        if (e.DataItem is AnnotationButtonItem stickyNoteDetails && BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute(stickyNoteDetails.IconName);
        }
    }
}