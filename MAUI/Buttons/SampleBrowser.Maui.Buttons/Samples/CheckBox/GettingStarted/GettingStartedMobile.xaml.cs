using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class GettingStartedMobile : SampleView
{
    public GettingStartedMobile()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object? sender, EventArgs e)
    {
        if (brown.IsChecked == true)
            brown.IsChecked = false;
        else
            brown.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_1(object? sender, EventArgs e)
    {
        if(green.IsChecked == true)
            green.IsChecked = false;
        else
            green.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_2(object? sender, EventArgs e)
    {
        if(red.IsChecked == true)
            red.IsChecked = false;
        else
            red.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_3(object? sender, EventArgs e)
    {
        if(sandal.IsChecked == true)
            sandal.IsChecked = false;
        else
            sandal.IsChecked = true;
    }
}