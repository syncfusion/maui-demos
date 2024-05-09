#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class AnnotationSecondaryToolbar : ContentView
{
    public static StampViewDesktop? stampViewDesktop { get; set; }
    public AnnotationSecondaryToolbar()
    {
        InitializeComponent();
    }
  
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            double x = 0;
            if (button.Parent is Grid buttonGrid)
            {
                if (buttonGrid.Parent is HorizontalStackLayout stackLayout)
                {
                    x = annotationStackLayout.X + buttonGrid.X;
                    if (button == colorPalette || button == stickyIconChangeButton)
                        x += editLayout.X;
                }
                else
                    x = buttonGrid.X;
            }

            if (BindingContext is CustomToolbarViewModel viewModel)
            {
                if (button == textMarkupButton)
                {
                    viewModel.TextMarkupListMargin = new Thickness(x, 100, 0, 0);
                }
                else if (button == shapeButton)
                {
                    viewModel.ShapeListMargin = new Thickness(x, 100, 0, 0);
                }
                else if (button == stampButton)
                {
                    double stampContextMenuWidth = 176;
                    if (x + stampContextMenuWidth <= this.Bounds.Width)
                    {
                        viewModel.StampListMargin = new Thickness(x, 100, 0, 0);
                    }
                    else
                        viewModel.StampListMargin = new Thickness(this.Bounds.Width - stampContextMenuWidth, 100, 0, 0);
                    stampViewDesktop?.ShowCustomStamp();
                }
                else if (button == colorPalette)
                {
                    viewModel.ColorPaletteMargin = new Thickness(x, 100, 0, 0);
                }
                else if(button == stickyIconChangeButton)
                    viewModel.StickyNoteListMargin = new Thickness(x, 100, 0, 0);
            }
        }
    }
}