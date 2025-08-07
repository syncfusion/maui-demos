#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;
#if ANDROID
using PlatformView = Android.Widget.EditText;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.TextBox;
#endif
using PointerEventArgs = Syncfusion.Maui.Core.Internals.PointerEventArgs;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class CustomStyleEditor : Editor
    {
#if WINDOWS || ANDROID
        protected override void OnHandlerChanged()
        {
#if WINDOWS
            // Hide editor border and underline.
            var platformView = this.Handler?.PlatformView as PlatformView;
            if (platformView != null)
            {
                this.ApplyTextBoxStyle(platformView);
            }
#else
            var platformView = this.Handler?.PlatformView as PlatformView;
            if (platformView != null)
            {
                this.ApplyTextBoxStyle(platformView);
            }
#endif
            base.OnHandlerChanged();
        }
#endif
#if WINDOWS || ANDROID
        private void ApplyTextBoxStyle(PlatformView? platformView)
        {
            if (platformView != null)
            {
#if WINDOWS
                var textBoxStyle = new Microsoft.UI.Xaml.Style(typeof(Microsoft.UI.Xaml.Controls.TextBox));
                textBoxStyle.Setters.Add(new Microsoft.UI.Xaml.Setter() { Property = Microsoft.UI.Xaml.Controls.Control.BorderBrushProperty, Value = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0)) });
                textBoxStyle.Setters.Add(new Microsoft.UI.Xaml.Setter() { Property = Microsoft.UI.Xaml.Controls.Control.BorderThicknessProperty, Value = new Thickness(0) });

                platformView.Resources.Add(typeof(Microsoft.UI.Xaml.Controls.TextBox), textBoxStyle);
#else
                platformView.Background = null;
                platformView.SetPadding(0, 0, 0, 0);
#endif
            }
        }
#endif
    }

    internal class SfEffectsViewAdv : SfEffectsView, ITouchListener, IGestureListener
    {
        public SfEffectsViewAdv()
        {

        }
        
        public new void OnTouch(PointerEventArgs e)
        {
            if (e.Action == PointerActions.Entered)
            {
                this.ApplyEffects(SfEffects.Highlight, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
            else if (e.Action == PointerActions.Released)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Cancelled)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Exited)
            {
                this.Reset();
            }
            else if (e.Action == PointerActions.Pressed)
            {
                this.ApplyEffects(SfEffects.Ripple, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
            }
        }

        internal void ForceRemoveEffects()
        {
            this.Reset();
        }
    }
}
