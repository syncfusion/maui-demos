using SampleBrowser.Maui.Base;
using core = Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.TextInputLayout.SfTextInputLayout;

public partial class SignUpPage : SampleView
{
	public SignUpPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content = new SignUpPageMobile();
#else
        this.Content = new SignUpPageDesktop();
#endif
    }

}