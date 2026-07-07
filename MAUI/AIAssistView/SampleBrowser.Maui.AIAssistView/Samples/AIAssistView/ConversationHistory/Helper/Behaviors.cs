using SampleBrowser.Maui.Base;
using Syncfusion.Maui.AIAssistView;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class SfAIAssistViewConversationBehavior : Behavior<SampleView>
    {
        #region Fields
        private Syncfusion.Maui.AIAssistView.SfAIAssistView? assistView;
        #endregion

        #region Overrides

        protected override void OnAttachedTo(SampleView bindable)
        {
            this.assistView = bindable.FindByName<Syncfusion.Maui.AIAssistView.SfAIAssistView>("sfAIAssistView");
            this.assistView.ChatModeChanged += this.OnChatModeChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            if (this.assistView != null)
            {
                this.assistView.ChatModeChanged -= this.OnChatModeChanged;
            }
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region CallBacks
        private void OnChatModeChanged(object? sender, ChatModeChangedEventArgs e)
        {
            var viewmodel = this.assistView?.BindingContext as GettingStartedViewModel;
            viewmodel?.SetRandomHeaderText();
        }

        #endregion

    }
}
