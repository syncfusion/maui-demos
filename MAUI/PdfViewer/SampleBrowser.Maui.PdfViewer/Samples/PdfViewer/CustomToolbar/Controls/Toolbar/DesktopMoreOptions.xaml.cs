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

    private void SfListView_ItemTapped(object? sender, Syncfusion.Maui.ListView.ItemTappedEventArgs? e)
    {
        if (e?.DataItem is AnnotationButtonItem buttonItem && BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.ToolbarCommand.Execute(buttonItem.IconName);
        }
    }
    internal void DisappearHighlight()
    {
        listView.SelectedItem = null;
    }
}