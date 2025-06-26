#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    private Button? lastClickedButton;
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
                    if (button == colorPalette || button == stickyIconChangeButton || button==fontSizeDropDownButton || button==thicknessButton)
                        x += editLayout.X;
                }
                else
                    x = buttonGrid.X;
            }

            if (BindingContext is CustomToolbarViewModel viewModel)
            {
                if (button == textMarkupButton)
                {
                    lastClickedButton = textMarkupButton;
                    viewModel.TextMarkupListMargin = new Thickness(x - 176 + button.Width, 100, 0, 0);
                }
                else if (button == fontSizeDropDownButton)
                {
                    lastClickedButton = fontSizeDropDownButton;
                    viewModel.FreeTextFontSizeListMargin = new Thickness(x, 100, 0, 0);
                }
                else if (button == shapeButton)
                {
                    lastClickedButton = shapeButton;
                    viewModel.ShapeListMargin = new Thickness(x - 176 + button.Width, 100, 0, 0);
                }
                else if (button == thicknessButton)
                {
                    lastClickedButton = thicknessButton;
                    viewModel.ThicknessButtonMargin = new Thickness(x - 280 + button.Width, 100, 0, 0);
                }
                else if (button == stampButton)
                {
                    lastClickedButton = stampButton;
                    viewModel.StampListMargin = new Thickness(x - 176 + button.Width, 100, 0, 0);
                    stampViewDesktop?.ShowCustomStamp();
                }
                else if (button == signButton)
                {
                    lastClickedButton = signButton;
                    viewModel.SignatureListMargin = new Thickness(x - 230 + button.Width, 100, 0, 0);
                }
                else if (button == colorPalette)
                {
                    lastClickedButton = colorPalette;
#if WINDOWS
                    viewModel.ColorPaletteMargin = new Thickness(x - 280 + button.Width, 100, 0, 0);
#elif MACCATALYST
                    viewModel.ColorPaletteMargin = new Thickness(x - 290 + button.Width, 100, 0, 0);
#endif
                }
                else if (button == stickyIconChangeButton)
                {
                    lastClickedButton = stickyIconChangeButton;
                    viewModel.StickyNoteListMargin = new Thickness(x - 200 + button.Width, 100, 0, 0);
                }
            }
        }
    }
    private void AnnotationSecondaryToolbar_SizeChanged(object? sender, EventArgs e)
    {
        if (lastClickedButton != null)
        {
            double x = 0;
            if (lastClickedButton.Parent is Grid buttonGrid)
            {
                if (buttonGrid.Parent is HorizontalStackLayout stackLayout)
                {
                    x = annotationStackLayout.X + buttonGrid.X;
                    if (lastClickedButton == colorPalette || lastClickedButton == stickyIconChangeButton || lastClickedButton == fontSizeDropDownButton || lastClickedButton == thicknessButton)
                        x += editLayout.X;
                }
                else
                    x = buttonGrid.X;
            }

            if (BindingContext is CustomToolbarViewModel viewModel)
            {
                if (lastClickedButton == textMarkupButton)
                {
                    viewModel.TextMarkupListMargin = new Thickness(x - 176 + lastClickedButton.Width, 100, 0, 0);
                }
                else if (lastClickedButton == fontSizeDropDownButton)
                {
                    viewModel.FreeTextFontSizeListMargin = new Thickness(x, 100, 0, 0);
                }
                else if (lastClickedButton == shapeButton)
                {
                    viewModel.ShapeListMargin = new Thickness(x - 176 + lastClickedButton.Width, 100, 0, 0);
                }
                else if (lastClickedButton == thicknessButton)
                {
                    viewModel.ThicknessButtonMargin = new Thickness(x - 280 + lastClickedButton.Width, 100, 0, 0);
                }
                else if (lastClickedButton == stampButton)
                {
                    viewModel.StampListMargin = new Thickness(x - 176 + lastClickedButton.Width, 100, 0, 0);
                    stampViewDesktop?.ShowCustomStamp();
                }
                else if (lastClickedButton == signButton)
                {
                    viewModel.SignatureListMargin = new Thickness(x - 230 + lastClickedButton.Width, 100, 0, 0);
                }
                else if (lastClickedButton == colorPalette)
                {
#if WINDOWS
                    viewModel.ColorPaletteMargin = new Thickness(x - 280 + lastClickedButton.Width, 100, 0, 0);
#elif MACCATALYST
                    viewModel.ColorPaletteMargin = new Thickness(x - 290 + lastClickedButton.Width, 100, 0, 0);
#endif
                }
                else if (lastClickedButton == stickyIconChangeButton)
                    viewModel.StickyNoteListMargin = new Thickness(x - 200 + lastClickedButton.Width, 100, 0, 0);
            }
        }
    }
}