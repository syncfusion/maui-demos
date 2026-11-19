using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfChip;

public partial class ChipPage : SampleView
{
	public ChipPage()
	{
		InitializeComponent();
#if WINDOWS || MACCATALYST
		this.Content=new ChipPageDesktop();
#elif ANDROID || IOS
		this.Content=new ChipPageMobile();
#endif
    }
}