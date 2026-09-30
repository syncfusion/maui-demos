
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.RichTextEditor.RichTextEditor;

/// <summary>
/// 
/// </summary>
public partial class GettingStartedDesktop : SampleView
{
    /// <summary>
    /// 
    /// </summary>
    public GettingStartedDesktop()
    {
        InitializeComponent();
        BindingContext = new MailViewModel(popup);
    }
}

