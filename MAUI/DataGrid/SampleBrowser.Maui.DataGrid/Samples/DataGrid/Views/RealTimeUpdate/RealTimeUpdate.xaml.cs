using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class RealTimeUpdate : SampleView
{
	public RealTimeUpdate()
	{
		InitializeComponent();
    }

    public override void OnDisappearing()
    {
        stockViewModel.Dispose();
        base.OnDisappearing();
    }
}