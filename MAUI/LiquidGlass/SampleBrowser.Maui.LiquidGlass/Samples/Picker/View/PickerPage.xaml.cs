using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfPicker;

public partial class PickerPage : SampleView
{
	public PickerPage()
	{
		InitializeComponent();
#if IOS || MACCATALYST
		this.Picker.Background = new SolidColorBrush(Colors.Transparent);
		this.Picker.HeaderView.Background = new SolidColorBrush(Colors.Transparent);
#endif
	}
}