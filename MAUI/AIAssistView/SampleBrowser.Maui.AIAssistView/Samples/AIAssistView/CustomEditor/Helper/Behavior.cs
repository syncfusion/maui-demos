using SampleBrowser.Maui.AIAssistView.SfAIAssistView;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using Syncfusion.Maui.AIAssistView;

#if ANDROID
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
#endif
#if IOS || MACCATALYST
using UIKit;
using Foundation;
using CoreGraphics;
#endif

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    class EditorTextChangedBehavior : Behavior<RequestEditorView>
    {
        private View? _associatedObject;
        private Page? parentPage;
        private SafeAreaEdges pageSafeAreaEdges;

#if MACCATALYST
        private RequestEditorView? _bindableEditor;
        private UIKit.UITextView? _nativeEditor;
        private bool ShiftDown = false;
        private UITextViewChange? _prevShouldChangeText;
#endif
#if ANDROID
        private Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust _oldAdjust;
        private bool _isResized;
#endif
        protected override void OnAttachedTo(RequestEditorView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEditorTextChanged;
            _associatedObject = bindable;
            parentPage = GetParentPage();

#if MACCATALYST
            _bindableEditor = bindable;
            bindable.HandlerChanged += OnHandlerChanged;
            TryWireNative(bindable);
            bindable.Loaded += OnEditorLoaded;
#endif
#if ANDROID
            var application = IPlatformApplication.Current?.Application as Microsoft.Maui.Controls.Application;
            if (application != null)
            {
                _oldAdjust = application.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().GetWindowSoftInputModeAdjust();
                application.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Resize);
                _isResized = true;
            }

            bindable.Focused += OnEditorFoused;
            bindable.Unfocused += Bindable_Unfocused;
#endif

        }

        private void Bindable_Unfocused(object? sender, FocusEventArgs e)
        {
        }

        private void OnEditorFoused(object? sender, FocusEventArgs e)
        {
            if (parentPage is ContentPage contentPage)
            {
                pageSafeAreaEdges = contentPage.SafeAreaEdges;
                contentPage.SafeAreaEdges = SafeAreaEdges.All;
            }
        }

        private void OnEditorUnfocused(object? sender, FocusEventArgs e)
        {
            if (parentPage is ContentPage contentPage)
            {
                contentPage.SafeAreaEdges = pageSafeAreaEdges;
            }
        }

        private Page? GetParentPage()
        {
            var application = IPlatformApplication.Current?.Application as Microsoft.Maui.Controls.Application;
            if (application != null && application.Windows.Count > 0 && application.Windows[0] != null)
            {
                var page = application.Windows[0].Page;
                if (page is NavigationPage navigationPage)
                {
                    return navigationPage.CurrentPage;
                }
            }
            
            return null;
        }

        protected override void OnDetachingFrom(RequestEditorView bindable)
        {
            bindable.TextChanged -= OnEditorTextChanged;
#if MACCATALYST
            bindable.HandlerChanged -= OnHandlerChanged;
            UnwireNative();
            _bindableEditor = null;
            bindable.Loaded -= OnEditorLoaded;
#endif
#if ANDROID
            if (_isResized)
            {
                var application = IPlatformApplication.Current?.Application as Microsoft.Maui.Controls.Application;
                if (application != null)
                {
                    application.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(_oldAdjust);
                }
                _isResized = false;
            }
            bindable.Focused -= OnEditorFoused;
            bindable.Unfocused -= Bindable_Unfocused;
#endif
            _associatedObject = null;

            base.OnDetachingFrom(bindable);
        }

        private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
        {
            var viewModel = (sender as RequestEditorView)!.BindingContext as GettingStartedViewModel;
            viewModel!.EnableSendIcon = viewModel.enableNewChatIcon && !string.IsNullOrEmpty(e.NewTextValue);
        }

#if MACCATALYST
        private void OnHandlerChanged(object? sender, EventArgs e)
        {
            if (sender is RequestEditorView editor)
            {
                UnwireNative();
                TryWireNative(editor);
            }
        }

        private void OnEditorLoaded(object? sender, EventArgs e)
        {
            if (sender is RequestEditorView editor)
            {
                TryWireNative(editor);
                if (_nativeEditor == null && editor?.Handler?.PlatformView is not null)
                {
                    TryWireNative(editor);
                }
            }
        }

        private void TryWireNative(RequestEditorView editor)
        {
            if (editor?.Handler?.PlatformView is UIKit.UIView root)
            {
                var textView = FindTextView(root);
                if (textView != null && textView.ShouldChangeText != ShouldChangeText)
                {
                    _nativeEditor = textView;
                    _prevShouldChangeText = textView.ShouldChangeText;
                    textView.ShouldChangeText = ShouldChangeText;
                }
            }
        }

        private void UnwireNative()
        {
            if (_nativeEditor != null)
            {
                _nativeEditor.ShouldChangeText = _prevShouldChangeText;
                _nativeEditor = null;
                _prevShouldChangeText = null;
            }
        }

        private UIKit.UITextView? FindTextView(UIKit.UIView root)
        {
            if (root is UIKit.UITextView tv)
                return tv;

            return null;
        }

        private bool ShouldChangeText(UIKit.UITextView textView, Foundation.NSRange range, string text)
        {
            if (text == "\n")
            {
                if (!this.ShiftDown)
                {
                    var context = _bindableEditor?.BindingContext;
                    if (context != null)
                    {
                        var enableSendIconProp = context.GetType().GetProperty("EnableSendIcon");
                        var sendCmdProp = context.GetType().GetProperty("SendButtonCommand");
                        var canSend = enableSendIconProp != null && enableSendIconProp.GetValue(context) is bool isEnabled && isEnabled;
                        if (canSend && sendCmdProp?.GetValue(context) is System.Windows.Input.ICommand cmd && cmd.CanExecute(null))
                        {
                            cmd.Execute(null);
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }

            if (_prevShouldChangeText != null)
            {
                return _prevShouldChangeText(textView, range, text);
            }

            return true;
        }
#endif
    }

    public class EnterKeyToSendBehavior : Behavior<View>, IKeyboardListener
    {
        private View? _associatedObject;

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;
            _associatedObject.AddKeyboardListener(this);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            bindable.RemoveKeyboardListener(this);
            _associatedObject = null;
            base.OnDetachingFrom(bindable);
        }

        public void OnKeyDown(KeyEventArgs args)
        {
        }

        public void OnKeyUp(KeyEventArgs args)
        {
        }

        public void OnPreviewKeyDown(KeyEventArgs args)
        {
#if WINDOWS
            if (args.Key == KeyboardKey.Enter && !args.IsShiftKeyPressed)
            {
                args.Handled = true;
                var context = (_associatedObject as BindableObject)?.BindingContext;
                if (context != null)
                {
                    var prop = context.GetType().GetProperty("SendButtonCommand");
                    var enableSendIcon = context.GetType().GetProperty("EnableSendIcon");
                    if (enableSendIcon != null && enableSendIcon.GetValue(context) is bool isEnabled && isEnabled)
                    {
                        if (prop?.GetValue(context) is ICommand cmd && cmd.CanExecute(null))
                        {
                            cmd.Execute(null);
                        }
                    }
                }
            }
#endif
        }
    }
}
