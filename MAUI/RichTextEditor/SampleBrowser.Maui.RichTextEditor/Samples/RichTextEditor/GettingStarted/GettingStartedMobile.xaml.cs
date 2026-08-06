using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.RichTextEditor.RichTextEditor;

/// <summary>
/// 
/// </summary>
public partial class GettingStartedMobile : SampleView
{
    /// <summary>
    /// 
    /// </summary>
    public GettingStartedMobile()
    {
        InitializeComponent();
        BindingContext = new MailViewModel();
    }
}

