#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class PropertyToolbar : ContentView
{
    internal HorizontalStackLayout? DeleteButtonLayout { get; set; }

    bool isDeleteButtonVisible = false;
    internal bool IsDeleteButtonVisible
    {
        get { return isDeleteButtonVisible; }
        set
        {
            isDeleteButtonVisible = value;
            if (DeleteButtonLayout != null)
                DeleteButtonLayout.IsVisible = value;
        }
    }
}