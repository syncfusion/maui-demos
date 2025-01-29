#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.AIAssistView;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class CustomAssistViewChat : AssistViewChat
    {
        public CustomAssistViewChat(Syncfusion.Maui.AIAssistView.SfAIAssistView aIAssistView) : base(aIAssistView)
        {

        }
    }

    public class CustomAssistView : Syncfusion.Maui.AIAssistView.SfAIAssistView
    {
        public static readonly BindableProperty AssistChatViewProperty =
       BindableProperty.Create(nameof(AssistChatView), typeof(CustomAssistViewChat), typeof(CustomAssistView));

        public CustomAssistViewChat AssistChatView
        {
            get { return (CustomAssistViewChat)this.GetValue(AssistChatViewProperty); }
            set { this.SetValue(AssistChatViewProperty, value); }
        }
        protected override AssistViewChat CreateAssistChat()
        {
            AssistChatView = new CustomAssistViewChat(this);
            return AssistChatView;
        }
    }
}
