namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
public partial class StickyNotePropertyToolbar : PropertyToolbar
{
	public StickyNotePropertyToolbar(CustomToolbarViewModel bindingContext)
	{
        BindingContext = bindingContext;
        InitializeComponent();
        DeleteButtonLayout = deleteButtonLayout;
    }

    private void deleteButtonLayout_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs? e)
    {
        if (e?.PropertyName == "IsVisible")
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