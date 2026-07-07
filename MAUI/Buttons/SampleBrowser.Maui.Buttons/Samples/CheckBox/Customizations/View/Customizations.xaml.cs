using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class Customizations : SampleView
{
    public Customizations()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content = new CustomizationsMobile();
#else
        this.Content = new CustomizationsDesktop();
#endif
    }
}