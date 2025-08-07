#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;
using SampleBrowser.Maui.PdfViewer.SfPdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class FreeTextFontSizeList : ContentView
{
	internal void Initialize()
	{
		InitializeComponent();
		freeFontListView.ItemsSource = new List<AnnotationButtonItem>()
		{ 
		 new AnnotationButtonItem
		 {
			 Icon="10",
		 },
         new AnnotationButtonItem
         {
             Icon="11",
         },
          new AnnotationButtonItem
         {
             Icon="12",
         },
           new AnnotationButtonItem
         {
             Icon="13",
         },
            new AnnotationButtonItem
         {
             Icon="14",
         },
             new AnnotationButtonItem
         {
             Icon="15",
         },
              new AnnotationButtonItem
         {
             Icon="16",
         },
        };

	}

    private void freeFontListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        if(e.DataItem is AnnotationButtonItem freeTextFontSizeList && BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute(freeTextFontSizeList.Icon);
        }
    }

    internal void FreeTextFontSizeListDisappear()
    {
        if(freeFontListView !=null)
        {
            freeFontListView.SelectedItem=null;
        }
    }
}