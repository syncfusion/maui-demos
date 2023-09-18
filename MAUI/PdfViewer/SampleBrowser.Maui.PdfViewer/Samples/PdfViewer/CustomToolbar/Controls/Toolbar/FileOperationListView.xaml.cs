#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class FileOperationListView : ContentView
{
    public event EventHandler<ItemTappedEventArgs>? ItemTapped;
    public FileOperationListView()
	{
        InitializeComponent();
        AddItems();
    }
    public void AddItems()
    {
        openLayout.Children.Add(CreateView("\uE712", "Open", true));
        saveLayout.Children.Add(CreateView("\uE75f", "Save", false));
    }
    private void ItemClicked(object? sender, EventArgs e)
    {
        GestureGrid? grid = sender as GestureGrid;
        if(BindingContext is CustomToolbarViewModel viewModel)
        {
            if (grid!.Children[1] is Label openIconName && openIconName.Text.Equals("Open"))
            {
                viewModel.OpenCloseFile();
            }
            else if(grid!.Children[1] is Label saveIconName && saveIconName.Text.Equals("Save"))
            {
                viewModel.SaveDocument();
            }
            else if(grid!.Children[1] is Label importIconName && importIconName.Text.Equals("Import Annotation"))
            {
                viewModel.ImportAnnotations();
            }
            else if (grid!.Children[1] is Label exportIconName && exportIconName.Text.Equals("Export Annotation"))
            {
                viewModel.ExportAnnotations();
            }
        }
        ItemTapped?.Invoke(this, new ItemTappedEventArgs(grid!.Children[0]));
    }
    private Grid CreateView(string icon, string iconName, bool isExpand)
    {
        GestureGrid childRow = new GestureGrid();
        childRow.WidthRequest = 120;
        childRow.HeightRequest = 40;
        childRow.PointerPressed += ItemClicked;
        Label iconLabel = new Label()
        {
            Padding = new Thickness(20, 0, 0, 0),
            TextColor = new Color(0,0,0,0.6f),
            FontSize = 16,
            FontFamily = "Maui Material Assets",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            Text = icon,
        };
        childRow.Children.Add(iconLabel);
        Label iconNameLabel = new Label()
        {
            Padding = new Thickness(15, 0, 0, 0),
            Margin = new Thickness(30, 0, 0, 0),
            TextColor = new Color(0, 0, 0, 0.6f),
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            Text = iconName,
        };
        childRow.Children.Add(iconNameLabel);
        if(isExpand)
        {
            Label expandLabel = new Label()
            {
                Margin = new Thickness(0, 0, 15, 0),
                TextColor = new Color(0, 0, 0, 0.6f),
                FontFamily = "Maui Material Assets",
                FontSize = 16,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "\uE704"
            };
            childRow.Children.Add(expandLabel);
        }
        return childRow;
    }
}