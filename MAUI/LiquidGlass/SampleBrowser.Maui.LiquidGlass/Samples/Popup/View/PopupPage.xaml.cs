using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfPopup;

public partial class PopupPage : SampleView
{
	public PopupPage()
	{
		InitializeComponent();
	}

    private void ClickToShowPopup(object? sender, EventArgs e)
    {
        alertWithTitle.Show();
    }

}