using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SmartDemos.SmartDemos;

public partial class DataPrediction : SampleView
{
	public DataPrediction()
	{
		InitializeComponent();
	}

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Behaviors.Clear();
    }
}