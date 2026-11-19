using SampleBrowser.Maui.Base;
using Syncfusion.Maui.GridSplitter;
namespace SampleBrowser.Maui.GridSplitter.SfGridSplitter;

public partial class GettingStartedMobile : SampleView
{
	public GettingStartedMobile()
	{
		InitializeComponent();
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }
    private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        if (e.DisplayInfo.Orientation == DisplayOrientation.Landscape)
        {
            GridSplitter.Orientation = GridSplitterOrientation.Horizontal;
        }
        else
        {
            GridSplitter.Orientation = GridSplitterOrientation.Vertical;
        }
    }
}