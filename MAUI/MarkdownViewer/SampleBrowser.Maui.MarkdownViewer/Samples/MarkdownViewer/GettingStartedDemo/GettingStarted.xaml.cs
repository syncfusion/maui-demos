using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.MarkdownViewer.SfMarkdownViewer;

public partial class GettingStarted : SampleView
{
    public GettingStarted()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        markdownViewer.Handler?.DisconnectHandler();
    }

    private async void markdownViewer_HyperlinkClicked(object? sender, Syncfusion.Maui.MarkdownViewer.MarkdownHyperlinkClickedEventArgs e)
    {
        e.Cancel = true;
        var url = e.Url;
        if (string.IsNullOrWhiteSpace(url))
            return;

        try
        {
            await Browser.Default.OpenAsync(url, BrowserLaunchMode.External);
        }
        catch
        {
            await Browser.Default.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }
    }
}