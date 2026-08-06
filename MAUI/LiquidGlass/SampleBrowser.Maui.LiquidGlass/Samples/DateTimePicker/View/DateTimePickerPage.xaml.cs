using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfDateTimePicker;

public partial class DateTimePickerPage : SampleView
{
	public DateTimePickerPage()
	{
		InitializeComponent();
#if IOS || MACCATALYST
		this.DateTimePicker.Background = new SolidColorBrush(Colors.Transparent);
		this.DateTimePicker.ColumnHeaderView.Background = new SolidColorBrush(Colors.Transparent);
		this.DateTimePicker.HeaderView.Background = new SolidColorBrush(Colors.Transparent);
#endif
	}
}