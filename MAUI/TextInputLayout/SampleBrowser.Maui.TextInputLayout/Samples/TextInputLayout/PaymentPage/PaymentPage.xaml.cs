using SampleBrowser.Maui.Base;
using core = Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.TextInputLayout.SfTextInputLayout;

public partial class PaymentPage : SampleView
{
	public PaymentPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content = new PaymentPageMobile();
#else
        this.Content = new PaymentPageDesktop();
#endif

    }



}