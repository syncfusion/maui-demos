
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.RichTextEditor.RichTextEditor;

/// <summary>
/// 
/// </summary>
public partial class GettingStarted : SampleView
{
    /// <summary>
    /// 
    /// </summary>
    public GettingStarted()
    {
        InitializeComponent();
#if ANDROID || (IOS && !MACCATALYST)
        this.Content = new GettingStartedMobile();
#else 
        this.Content = new GettingStartedDesktop();
#endif

    }
}

