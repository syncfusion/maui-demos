#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
public partial class StickyNotePropertyToolbar : PropertyToolbar
{
	public StickyNotePropertyToolbar(CustomToolbarViewModel bindingContext)
	{
        BindingContext = bindingContext;
        InitializeComponent();
        DeleteButtonLayout = deleteButtonLayout;
    }

    private void deleteButtonLayout_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsVisible")
        {
            if (deleteButtonLayout.IsVisible)
            {
                deleteButtonLayout.Margin = new Thickness(10, 0, 0, 0);
                lockUnlokButtonLayout.Margin = new Thickness(7, 0, 0, 0);
            }
            else
            {
                deleteButtonLayout.Margin = new Thickness(0, 0, 0, 0);
                lockUnlokButtonLayout.Margin = new Thickness(0, 0, 0, 0);
            }
        }
    }
}