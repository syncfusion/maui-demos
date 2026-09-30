using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfMaskedEntry;

public partial class Culture : SampleView
{
    public Culture()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content = new CultureMobile();
#else
        CultureDesktop cultureDesktop = new();
        this.Content = cultureDesktop.Content;
        this.Margin = cultureDesktop.Margin;
#endif

    }

   
}