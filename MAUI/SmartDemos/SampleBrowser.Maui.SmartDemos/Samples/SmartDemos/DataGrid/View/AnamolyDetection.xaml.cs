using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SmartDemos.SmartDemos;

public partial class AnamolyDetection : SampleView
{
	public AnamolyDetection()
	{
		InitializeComponent();
	}

    /// <inheritdoc/>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        this.Behaviors.Clear();
    }
}