#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    using SampleBrowser.Maui.Base;

    public partial class FeedbackForm : SampleView
    {
        public FeedbackForm()
        {
#if ANDROID || IOS
            this.Content = new FeedbackFormMobileView();
#else
            this.Content = new FeedbackFormDesktopView();
#endif
        }
    }
}
