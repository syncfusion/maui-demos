#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    internal void Initialize()
    {
        InitializeComponent();
        AddItems();
    }
    public void AddItems()
    {
        openLayout.Children.Add(CreateView("\uE712", "Open", true));
        saveLayout.Children.Add(CreateView("\uE75f", "Save", false));
        printLayout.Children.Add(CreateView("\uE77f", "Print", false));
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
            else if (grid!=null && grid.Children[1] is Label printIconName && printIconName.Text.Equals("Print"))
            {
                viewModel.PrintDocument();
            }
        }
        ItemTapped?.Invoke(this, new ItemTappedEventArgs(grid!.Children[0]));
    }
    private Grid CreateView(string icon, string iconName, bool isExpand)
    {
        GestureGrid childRow = new GestureGrid();
        childRow.WidthRequest = 205;
        childRow.HeightRequest = 40;
        childRow.PointerPressed += ItemClicked;
        Color lightThemeColor = new Color();
        Color darkThemeColor = new Color();
        if(Application.Current != null)
        {
            lightThemeColor = (Color)Application.Current.Resources["IconColourLight"];
            darkThemeColor = (Color)Application.Current.Resources["IconColour"];
        }
        Label iconLabel = new Label()
        {
            Padding = new Thickness(16, 0, 12, 0),
            TextColor = Color.FromArgb("#49454F"),
            FontSize = 16,
            FontFamily = "MauiMaterialAssets",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            Text = icon,
        };
        iconLabel.SetAppThemeColor(Label.TextColorProperty,lightThemeColor, darkThemeColor);
        childRow.Children.Add(iconLabel);
        Label iconNameLabel = new Label()
        {
            Padding = new Thickness(15, 0, 0, 0),
            Margin = new Thickness(30, 0, 0, 0),
            TextColor = Color.FromArgb("#49454F"),
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            Text = iconName,
        };
        iconNameLabel.SetAppThemeColor(Label.TextColorProperty, lightThemeColor, darkThemeColor);
        childRow.Children.Add(iconNameLabel);
        if(isExpand)
        {
            Label expandLabel = new Label()
            {
                Margin = new Thickness(0, 0, 15, 0),
                TextColor = Color.FromArgb("#49454F"),
                FontFamily = "MauiMaterialAssets",
                FontSize = 16,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "\uE704"
            };
            expandLabel.SetAppThemeColor(Label.TextColorProperty, lightThemeColor, darkThemeColor);
            childRow.Children.Add(expandLabel);
        }
        return childRow;
    }
}