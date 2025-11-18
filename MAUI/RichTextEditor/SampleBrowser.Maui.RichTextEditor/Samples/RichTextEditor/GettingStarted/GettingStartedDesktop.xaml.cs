#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

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

