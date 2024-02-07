
#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
public partial class StampPropertyToolbar : PropertyToolbar
{
    Ellipse selectedColorButtonHighlight;
    public StampPropertyToolbar(CustomToolbarViewModel bindingContext)
    {
        BindingContext = bindingContext;
        InitializeComponent();
        DeleteButtonLayout = deleteButtonLayout;
        selectedColorButtonHighlight = new Ellipse();
        selectedColorButtonHighlight.WidthRequest = 30;
        selectedColorButtonHighlight.HeightRequest = 30;
        selectedColorButtonHighlight.VerticalOptions = LayoutOptions.Center;
        selectedColorButtonHighlight.Stroke = new SolidColorBrush(Colors.Black);
        selectedColorButtonHighlight.StrokeThickness = 2;
    }

    private void ColorButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            int column = Grid.GetColumn(button);
            Grid.SetColumn(selectedColorButtonHighlight, column);
            
        }
        if (selectedColorButtonHighlight.Parent == null)
            opacityGrid.Children.Add(selectedColorButtonHighlight);
    }
}