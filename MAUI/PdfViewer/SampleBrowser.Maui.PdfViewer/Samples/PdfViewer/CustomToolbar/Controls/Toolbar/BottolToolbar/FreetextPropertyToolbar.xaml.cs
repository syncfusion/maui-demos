#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class FreetextPropertyToolbar : PropertyToolbar
{
    public FreetextPropertyToolbar(CustomToolbarViewModel bindingContext)
    {
        BindingContext = bindingContext;
        InitializeComponent();
    }
}