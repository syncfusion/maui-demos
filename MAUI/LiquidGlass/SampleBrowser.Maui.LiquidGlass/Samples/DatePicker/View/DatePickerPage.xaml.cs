using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfDatePicker;

public partial class DatePickerPage : SampleView
{
	public DatePickerPage()
	{
		InitializeComponent();
#if IOS || MACCATALYST
		this.DatePicker.Background = new SolidColorBrush(Colors.Transparent);
		this.DatePicker.ColumnHeaderView.Background = new SolidColorBrush(Colors.Transparent);
		this.DatePicker.HeaderView.Background = new SolidColorBrush(Colors.Transparent);
#endif
	}
}