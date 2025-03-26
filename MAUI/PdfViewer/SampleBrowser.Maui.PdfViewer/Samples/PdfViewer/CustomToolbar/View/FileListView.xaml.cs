#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using System.Reflection;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class FileListView : ContentView
{
    List<AnnotationButtonItem> Items = new List<AnnotationButtonItem>();
    public event EventHandler<FileSelectedEventArgs>? FileSelected;
    public FileListView()
    {
        InitializeComponent();

                Items.Add(new AnnotationButtonItem() { IconName = "PDF_succinctly.pdf", Icon = "\uE780" });
                Items.Add(new AnnotationButtonItem() { IconName = "Rotated_document.pdf", Icon = "\uE780" });
                Items.Add(new AnnotationButtonItem() { IconName = "Password_protected_document.pdf", Icon = "\uE780" });
                Items.Add(new AnnotationButtonItem() { IconName = "Corrupted_document.pdf", Icon = "\uE780" });
                Items.Add(new AnnotationButtonItem() { IconName = "Single_page_document.pdf", Icon = "\uE780" });
                Items.Add(new AnnotationButtonItem() { IconName = "Annotations_document.pdf", Icon = "\uE780" });
                Items.Add(new AnnotationButtonItem() { IconName = "Browse files on this device", Icon = "\uE712" });
            listView.ItemsSource = Items;
        }

    private async void listView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
    {
        await Task.Delay(50);
        AnnotationButtonItem? tappedItem = e.DataItem as AnnotationButtonItem;
        if (tappedItem != null)
        {
            string fileName = string.Empty;
            int tappedIndex = Items.IndexOf(tappedItem);
            if (tappedIndex == 0)
                fileName = "PDF_Succinctly1.pdf";
            else if (tappedIndex == 1)
                fileName = "rotated_document.pdf";
            else if (tappedIndex == 2)
                fileName = "password_protected_document.pdf";
            else if (tappedIndex == 3)
                fileName = "corrupted_document.pdf";
            else if (tappedIndex == 4)
                fileName = "Invoice.pdf";
            else if (tappedIndex == 5)
                fileName = "Annotations.pdf";

            if (BindingContext is CustomToolbarViewModel bindingContext)
            {
                if (tappedIndex == 6)
                {
                    bindingContext.UpdateFileName(tappedItem.IconName);
                }
                else
                {
                    FileSelected?.Invoke(this, new FileSelectedEventArgs(fileName));
                    bindingContext.IsFileListViewVisible = false;
                }
            }
        }
        listView.SelectedItem = null;
    }
}