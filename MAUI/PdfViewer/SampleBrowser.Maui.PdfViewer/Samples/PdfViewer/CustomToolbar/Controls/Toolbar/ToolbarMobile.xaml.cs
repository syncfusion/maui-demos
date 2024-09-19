#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class ToolbarMobile : ToolbarView 
{
	public ToolbarMobile()
	{
#if !WINDOWS && !MACCATALYST
        InitializeComponent();
        AssignControls();
#endif
    }

    void AssignControls()
    {
        SearchButton = searchButton;
        UndoButton = mobileUndoButton;
        RedoButton = mobileRedoButton;
        ZoomMobile = zoomMobile;
    }
    private void mobileUndo_Redo_Clicked(object sender, EventArgs e)
    {
        if (ParentView != null)
        {
            ParentView.CloseAllDialogs();
            ParentView.HideOverlayToolbars();
        }

    }
}