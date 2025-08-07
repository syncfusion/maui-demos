#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class AnnotationToolbarDesktop : AnnotationToolbarView
{
    internal StampViewDesktop? StampViewDesktop { get; set; }
    private Button? lastClickedButton;
	public AnnotationToolbarDesktop()
	{
		InitializeComponent();
        AssignControls();
    }

    public void SetBindings()
    {
        textMarkupButton.SetBinding(Button.TextProperty, new Binding() { Path = nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.AnnotationMode), Source = PdfViewer, Converter = new AnnotationModeToIconConverter("TextMarkup") });
        shapeButton.SetBinding(Button.TextProperty, new Binding() { Path = nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.AnnotationMode), Source = PdfViewer, Converter = new AnnotationModeToIconConverter("Shape") });
    }

    void AssignControls()
    {
        StampButton = stampButton;
        UndoButton = undoButton;
        RedoButton = redoButton;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender != signButton)
            ParentView?.CloseAllDialogs();
        if (sender is Button button)
        {
            double x = 0;
            if (button.Parent is Grid buttonGrid)
            {
                if (buttonGrid.Parent is HorizontalStackLayout stackLayout)
                {
                    if (stackLayout.Parent is ScrollView scroll)
                    {
                        x = buttonGrid.X + scrollView.X - scrollView.ScrollX;
                    }
                    if (button == colorPalette || button == stickyIconChangeButton || button == fontSizeDropDownButton || button == thicknessButton)
                        x = buttonGrid.X + scrollView.X + editLayout.X - scrollView.ScrollX;
                }
                else
                    x = buttonGrid.X;
            }

            if (BindingContext is CustomToolbarViewModel viewModel)
            {
                if (button == textMarkupButton)
                {
                    lastClickedButton = textMarkupButton;
                    ParentView?.TextMarkUpHighlightDisappear();
                    viewModel.TextMarkupListMargin = new Thickness(x - 176 + textMarkupGrid.Width, 40, 0, 0);
                }
                else if (button == fontSizeDropDownButton)
                {
                    lastClickedButton = fontSizeDropDownButton;
                    ParentView?.FreeTextFontSizeListDisappear();
                    viewModel.FreeTextFontSizeListMargin = new Thickness(x - 10, 40, 0, 0);
                }
                else if (button == thicknessButton)
                {
                    lastClickedButton = thicknessButton;
                    viewModel.ThicknessButtonMargin = new Thickness(x - 280 + thicknessGrid.Width, 40, 0, 0);
                }
                else if (button == shapeButton)
                {
                    lastClickedButton = shapeButton;
                    ParentView?.ShapeHighlightDisapper();
                    viewModel.ShapeListMargin = new Thickness(x - 176 + shapesGrid.Width, 40, 0, 0);
                }
                else if (button == stampButton)
                {
                    lastClickedButton = stampButton;
                    double stampContextMenuWidth = 176;
                    viewModel.StampListMargin = new Thickness(x - stampContextMenuWidth + stampGrid.Width, 40, 0, 0);
                    StampViewDesktop?.ShowCustomStamp();
                    if (ParentView != null && ParentView.StampView != null)
                        ParentView.StampView.StampMode = false;
                }
                else if (button == signButton)
                {
                    lastClickedButton = signButton;
                    double signatureListWidth = 230;
                    viewModel.SignatureListMargin = new Thickness(x - signatureListWidth + signatureGrid.Width, 48, 0, 0);
                }
                else if (button == stickyIconChangeButton)
                {
                    lastClickedButton = stickyIconChangeButton;
                    viewModel.StickyNoteListMargin = new Thickness(x - 200 + stickyNoteChangeGrid.Width, 40, 0, 0);
                }
                else if (button == colorPalette && (viewModel.IsFreeTextFillColorVisble || viewModel.IsShapeColorPalleteVisible || viewModel.IsLineAndArrowColorPalleteVisible || viewModel.IsTextMarkUpColorPalleteVisible || viewModel.IsStickyNoteColorPalleteVisible || viewModel.IsInkColorPalleteVisible || viewModel.IsStampOpacitySliderbarVisible || viewModel.IsEraserThicknessToolbarVisible))
                {
                    lastClickedButton = colorPalette;
#if WINDOWS
                    viewModel.ColorPaletteMargin = new Thickness(x - 280 + colorGrid.Width, 40, 0, 0);
#elif MACCATALYST
                    viewModel.ColorPaletteMargin = new Thickness(x - 290 + colorGrid.Width, 40, 0, 0);
#endif
                }
                else if (button == fileOperation)
                {
                    viewModel.FileOperationListMargin = new Thickness(0, 40, 0, 0);
                    viewModel.IsFileOperationListVisible = !viewModel.IsFileOperationListVisible;
                    viewModel.FileOperationHighlightColor = viewModel.IsFileOperationListVisible ? Color.FromRgba(0, 0, 0, 20) : Colors.Transparent;
                    PdfViewer!.AnnotationMode = AnnotationMode.None;
                    viewModel.ClearButtonHighlights();
                    viewModel.IsShapeListVisible = false;
                    viewModel.IsTextMarkupListVisible = false;
                    viewModel.IsStampListVisible = false;
                    viewModel.IsStickyNoteMode = false;
                }
            }
        }
    }
    private void undoRedoBtton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.HideDeskTopOverLays();
            viewModel.CloseAllDialogs();
        }
    }
    private void scrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        if (lastClickedButton != null)
        {
            double x = 0;
            if (lastClickedButton.Parent is Grid buttonGrid)
            {
                if (buttonGrid.Parent is HorizontalStackLayout stackLayout)
                {
                    if (stackLayout.Parent is ScrollView scroll)
                    {
                        x = buttonGrid.X + scrollView.X - scrollView.ScrollX;
                    }
                    if (lastClickedButton == colorPalette || lastClickedButton == stickyIconChangeButton || lastClickedButton == fontSizeDropDownButton || lastClickedButton == thicknessButton)
                        x = buttonGrid.X + scrollView.X + editLayout.X - scrollView.ScrollX;
                }
                else
                    x = buttonGrid.X;
            }
            if (BindingContext is CustomToolbarViewModel viewModel && PdfViewer != null)
            {
                if (lastClickedButton == textMarkupButton)
                {
                    viewModel.TextMarkupListMargin = new Thickness(x - 176 + textMarkupGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == fontSizeDropDownButton)
                {
                    viewModel.FreeTextFontSizeListMargin = new Thickness(x - 10, 40, 0, 0);
                }
                else if (lastClickedButton == thicknessButton)
                {
                    viewModel.ThicknessButtonMargin = new Thickness(x - 280 + thicknessGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == shapeButton)
                {
                    viewModel.ShapeListMargin = new Thickness(x - 176 + shapesGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == stampButton)
                {
                    double stampContextMenuWidth = 176;
                    viewModel.StampListMargin = new Thickness(x - stampContextMenuWidth + stampGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == signButton)
                {
                    double signatureListWidth = 230;
                    viewModel.SignatureListMargin = new Thickness(x - signatureListWidth + signatureGrid.Width, 48, 0, 0);
                }
                else if (lastClickedButton == stickyIconChangeButton)
                    viewModel.StickyNoteListMargin = new Thickness(x - 200 + stickyNoteChangeGrid.Width, 40, 0, 0);
                else if (lastClickedButton == colorPalette && (viewModel.IsFreeTextFillColorVisble || viewModel.IsShapeColorPalleteVisible || viewModel.IsLineAndArrowColorPalleteVisible || viewModel.IsTextMarkUpColorPalleteVisible || viewModel.IsStickyNoteColorPalleteVisible || viewModel.IsInkColorPalleteVisible || viewModel.IsStampOpacitySliderbarVisible || viewModel.IsEraserThicknessToolbarVisible))
                {
#if WINDOWS
                    viewModel.ColorPaletteMargin = new Thickness(x - 280 + colorGrid.Width, 40, 0, 0);
#elif MACCATALYST
                    viewModel.ColorPaletteMargin = new Thickness(x - 290 + colorGrid.Width, 40, 0, 0);
#endif
                }
                else if (lastClickedButton == fileOperation)
                {
                    viewModel.FileOperationListMargin = new Thickness(0, 40, 0, 0);
                }
            }
        }
    }
    private void ScrollView_SizeChanged(object? sender, EventArgs e)
    {
        if (lastClickedButton != null)
        {
            double x = 0;
            if (lastClickedButton.Parent is Grid buttonGrid)
            {
                if (buttonGrid.Parent is HorizontalStackLayout stackLayout)
                {
                    if (stackLayout.Parent is ScrollView scroll)
                    {
                        x = buttonGrid.X + scrollView.X - scrollView.ScrollX;
                    }
                    if (lastClickedButton == colorPalette || lastClickedButton == stickyIconChangeButton || lastClickedButton == fontSizeDropDownButton || lastClickedButton == thicknessButton)
                        x = buttonGrid.X + scrollView.X + editLayout.X - scrollView.ScrollX;
                }
                else
                    x = buttonGrid.X;
            }
            if (BindingContext is CustomToolbarViewModel viewModel && PdfViewer != null)
            {
                if (lastClickedButton == textMarkupButton)
                {
                    viewModel.TextMarkupListMargin = new Thickness(x - 176 + textMarkupGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == fontSizeDropDownButton)
                {
                    viewModel.FreeTextFontSizeListMargin = new Thickness(x - 10, 40, 0, 0);
                }
                else if (lastClickedButton == thicknessButton)
                {
                    viewModel.ThicknessButtonMargin = new Thickness(x - 280 + thicknessGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == shapeButton)
                {
                    viewModel.ShapeListMargin = new Thickness(x - 176 + shapesGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == stampButton)
                {
                    double stampContextMenuWidth = 176;
                    viewModel.StampListMargin = new Thickness(x - stampContextMenuWidth + stampGrid.Width, 40, 0, 0);
                }
                else if (lastClickedButton == signButton)
                {
                    double signatureListWidth = 230;
                    viewModel.SignatureListMargin = new Thickness(x - signatureListWidth + signatureGrid.Width, 48, 0, 0);
                }
                else if (lastClickedButton == stickyIconChangeButton)
                    viewModel.StickyNoteListMargin = new Thickness(x - 200 + stickyNoteChangeGrid.Width, 40, 0, 0);
                else if (lastClickedButton == colorPalette && (viewModel.IsFreeTextFillColorVisble || viewModel.IsShapeColorPalleteVisible || viewModel.IsLineAndArrowColorPalleteVisible || viewModel.IsTextMarkUpColorPalleteVisible || viewModel.IsStickyNoteColorPalleteVisible || viewModel.IsInkColorPalleteVisible || viewModel.IsStampOpacitySliderbarVisible || viewModel.IsEraserThicknessToolbarVisible))
                {
#if WINDOWS
                    viewModel.ColorPaletteMargin = new Thickness(x - 280 + colorGrid.Width, 40, 0, 0);
#elif MACCATALYST
                    viewModel.ColorPaletteMargin = new Thickness(x - 290 + colorGrid.Width, 40, 0, 0);
#endif
                }
                else if (lastClickedButton == fileOperation)
                {
                    viewModel.FileOperationListMargin = new Thickness(0, 40, 0, 0);
                }
            }
        }
    }
}