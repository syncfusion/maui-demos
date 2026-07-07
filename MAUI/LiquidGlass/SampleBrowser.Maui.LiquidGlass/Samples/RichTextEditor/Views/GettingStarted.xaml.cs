using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfRichTextEditor;

public partial class GettingStarted : SampleView
{
	public GettingStarted()
	{
		InitializeComponent();

#if MACCATALYST
        Content = new MailBoxEditor();
#elif IOS
        Content = new MailBoxEditorMobile();
#endif
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        var sampleView = Content as SampleView;

        if (sampleView != null)
        {
            sampleView.OnAppearing();
        }

    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        var sampleView = Content as SampleView;

        if (sampleView != null)
        {
            sampleView.OnDisappearing();
        }
    }
}