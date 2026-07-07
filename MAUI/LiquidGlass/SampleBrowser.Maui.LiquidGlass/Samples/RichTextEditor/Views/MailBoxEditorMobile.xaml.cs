using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfRichTextEditor;

public partial class MailBoxEditorMobile : SampleView
{
	public MailBoxEditorMobile()
	{
		InitializeComponent();
        BindingContext = new MailViewModel(sfPopup);
    }
}