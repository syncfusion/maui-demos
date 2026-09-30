using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfCalendar;

public partial class CalendarPage : SampleView
{
	public CalendarPage()
	{
		InitializeComponent();
#if IOS || MACCATALYST
		this.Calendar.Background = new SolidColorBrush(Colors.Transparent);
#endif
	}
}