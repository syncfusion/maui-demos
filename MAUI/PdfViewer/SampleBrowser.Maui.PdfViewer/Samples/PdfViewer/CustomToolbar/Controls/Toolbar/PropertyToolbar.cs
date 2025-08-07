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
   
    bool isDeleteButtonVisible = false;

    bool isOpacityButtonVisible = false;

    internal HorizontalStackLayout? DeleteButtonLayout { get; set; }

    internal Grid? OpacityButtonLayout { get; set; }

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
    internal bool IsOpacityButtonVisible
    {
        get { return isOpacityButtonVisible; }
        set
        {
            isOpacityButtonVisible = value;
            if (OpacityButtonLayout != null)
                OpacityButtonLayout.IsVisible = value;
        }
    }
}