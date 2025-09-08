#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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

    private async void markdownViewer_HyperlinkClicked(object sender, Syncfusion.Maui.MarkdownViewer.MarkdownHyperlinkClickedEventArgs e)
    {
        e.Cancel = true;
        var url = e.Url;

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