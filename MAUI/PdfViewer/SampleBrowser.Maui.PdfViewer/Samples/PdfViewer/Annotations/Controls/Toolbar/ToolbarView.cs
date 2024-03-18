#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public class AnnotationToolbarView : Microsoft.Maui.Controls.ContentView
{
    private Syncfusion.Maui.PdfViewer.SfPdfViewer? pdfViewer;
    public Button? UndoButton { get; set; }
    public Button? SaveButton { get; set; }
    public Button? RedoButton { get; set; }
    public Button? StampButton { get; set; }

    public Annotations? ParentView { get; set; }
    
    public Syncfusion.Maui.PdfViewer.SfPdfViewer? PdfViewer 
    { 
        get
        {
            return pdfViewer;
        }
        set
        {
            pdfViewer = value;
            BindPdfViewerValues();
        }
    }

    void BindPdfViewerValues()
    {
        if (PdfViewer != null)
        {
            if (UndoButton != null)
                UndoButton.Command = PdfViewer.UndoCommand;
            if (RedoButton != null)
                RedoButton.Command = PdfViewer.RedoCommand;
        }
    }
}
