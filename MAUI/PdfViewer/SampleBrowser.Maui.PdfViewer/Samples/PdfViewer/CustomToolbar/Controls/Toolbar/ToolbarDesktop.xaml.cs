#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class ToolbarDesktop : ToolbarView
{
	public ToolbarDesktop()
	{
        InitializeComponent();
        AssignControls();
        fileOperation.PropertyChanged += FileOperation_PropertyChanged;
    }
    
	private void FileOperation_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == "IsEnabled")
        {
            fileOperation.IsEnabled = true;
        }
    }
    void AssignControls()
    {
        PageNumberEntry = pageNumberEntry;
        SearchButton = searchButton;
        OutlineButton = outlineButton;
        PageCountLabel = pageCountLabel;
        GoToPreviousPageButton = goToPreviousPageButton;
        GoToNextPageButton = goToNextPageButton;
        //StampButton = stampButton;
        UndoButton = undoButton;
        RedoButton = redoButton;
        ZoomPercentage = zoomPercentage;
    }

    public void SetBindings()
    {
        //textMarkupButton.SetBinding(Button.TextProperty, new Binding() {Path = nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.AnnotationMode), Source = PdfViewer, Converter = new AnnotationModeToIconConverter("TextMarkup")});
        //shapeButton.SetBinding(Button.TextProperty, new Binding() { Path = nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.AnnotationMode), Source = PdfViewer, Converter = new AnnotationModeToIconConverter("Shape") });
    }
   
    private void Button_Clicked(object sender, EventArgs e)
    {
        ParentView?.SearchView?.Close();
        ParentView?.CloseAllDialogs();
        ReadOnlyParentView?.SearchView?.Close();
        ReadOnlyParentView?.CloseAllDialogs();
        if (sender is Button button)
        {
            if (BindingContext is CustomToolbarViewModel viewModel)
            {
                if (button == fileOperation)
                {
                    viewModel.FileOperationListMargin = new Thickness(0, 48, 0, 0);
                }
            }
        }
    }

    private void undo_RedoClicked(object sender, EventArgs e)
    {
        if (BindingContext is CustomToolbarViewModel viewModel)
        {
            viewModel.HideDeskTopOverLays();
            viewModel.CloseAllDialogs();
        }

    }
    private void pageLayoutButton_Clicked(object sender, EventArgs e)
    {
        ParentView?.SearchView?.Close();
    }
}