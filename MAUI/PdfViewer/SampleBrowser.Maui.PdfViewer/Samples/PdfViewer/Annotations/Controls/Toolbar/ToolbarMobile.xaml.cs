#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class AnnotationToolbarMobile : AnnotationToolbarView
{
	public AnnotationToolbarMobile()
	{
		InitializeComponent();
        AssignControls();
	}

    void AssignControls()
    {
        UndoButton = mobileUndoButton;
        RedoButton = mobileRedoButton;
    }

    private void undoRedoClicked(object sender, EventArgs e)
    {
        if (ParentView != null)
        {
            ParentView.CloseAllDialogs();
            ParentView.HideOverlayToolbars();
        }
    }
}