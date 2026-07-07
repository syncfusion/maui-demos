using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfRichTextEditor;

public partial class MailBoxEditor : SampleView
{
	public MailBoxEditor()
	{
		InitializeComponent();
        BindingContext = new MailViewModel(sfPopup);
    }
}