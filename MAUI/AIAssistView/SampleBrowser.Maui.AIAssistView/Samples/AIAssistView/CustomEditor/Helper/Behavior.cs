#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.AIAssistView.SfAIAssistView;
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class EditorTextChangedBehavior : Behavior<CustomStyleEditor>
    {
        protected override void OnAttachedTo(CustomStyleEditor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEditorTextChanged;
        }

        protected override void OnDetachingFrom(CustomStyleEditor bindable)
        {
            bindable.TextChanged -= OnEditorTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
        {
            var viewModel = (sender as CustomStyleEditor)!.BindingContext as GettingStartedViewModel;

            // Disable send icon when Stop responding is loading.
            viewModel!.EnableSendIcon = viewModel.enableNewChatIcon && !string.IsNullOrEmpty(e.NewTextValue);
        }
    }
}
