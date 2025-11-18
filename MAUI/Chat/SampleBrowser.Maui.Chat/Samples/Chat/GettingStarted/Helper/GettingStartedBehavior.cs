#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Chat;
using System;
using System.Collections.Specialized;

namespace SampleBrowser.Maui.Chat.SfChat
{
    public class GettingStartedBehavior : Behavior<SampleView>
    {
        #region Fields

        private GettingStartedViewModel? viewModel;
        private Syncfusion.Maui.Chat.SfChat? sfChat;
        private int unReadMessageCount;

        #endregion

        /// <summary>
        /// Method will be called when the view is attached to window
        /// </summary>
        /// <param name="bindable">SampleView type of bindable parameter</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.sfChat = bindable.FindByName<Syncfusion.Maui.Chat.SfChat>("sfChat");
            this.viewModel = bindable.FindByName<GettingStartedViewModel>("viewModel");

            this.viewModel.Messages!.CollectionChanged += Messages_CollectionChanged;
            this.sfChat.Scrolled += SfChat_Scrolled;
        }

        /// <summary>
        /// Raised when the chat scrolled.
        /// </summary>
        /// <param name="sender">The object as sender.</param>
        /// <param name="e">ChatScrolledEventArgs as e.</param>
        private void SfChat_Scrolled(object? sender, Syncfusion.Maui.Chat.ChatScrolledEventArgs e)
        {
            this.sfChat!.CanAutoScrollToBottom = e.IsBottomReached;
            this.viewModel!.IsBadgeViewVisible = !e.IsBottomReached;
            if (e.IsBottomReached)
            {
                this.viewModel!.MessageCount = string.Empty;
                this.unReadMessageCount = 0;
            }
        }

        /// <summary>
        /// Raised when the viewmodel message collection changed.
        /// </summary>
        /// <param name="sender">The object as sender.</param>
        /// <param name="e">NotifyCollectionChangedEventArgs as e.</param>
        private void Messages_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var chatItem in e.NewItems!)
                {
                    if ((chatItem as TextMessage) != null && this.viewModel!.IsBadgeViewVisible)
                    {
                        this.unReadMessageCount++;
                        this.viewModel.MessageCount = this.unReadMessageCount.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Method will be called when the view is detached from window
        /// </summary>
        /// <param name="bindable">SampleView type of bindable parameter</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.viewModel = null;
            this.sfChat = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
