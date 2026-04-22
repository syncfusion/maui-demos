#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Platform;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Core.Internals;
using PointerEventArgs = Syncfusion.Maui.Core.Internals.PointerEventArgs;
#if ANDROID
using Android.Content.Res;
#endif

namespace SampleBrowser.Maui.TreeView
{
    internal class SfEffectsViewAdv : SfEffectsView, ITouchListener
    {
        public SfEffectsViewAdv()
        {
        }

        public new void OnTouch(PointerEventArgs e)
        {
            if (e.PointerDeviceType == PointerDeviceType.Mouse)
            {
                if (e.Action == PointerActions.Entered)
                {
                    this.ApplyEffects(SfEffects.Highlight, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
                }
                else if (e.Action == PointerActions.Pressed)
                {
                    this.ApplyEffects(SfEffects.Ripple, RippleStartPosition.Default, new System.Drawing.Point((int)e.TouchPoint.X, (int)e.TouchPoint.Y), false);
                }
                else if (e.Action == PointerActions.Released || e.Action == PointerActions.Cancelled || e.Action == PointerActions.Exited)
                {
                    this.Reset();
                }
            }
        }
    }

    internal class SfEntry : Entry
    {
        public SfEntry()
        {
#if ANDROID
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is Entry)
                {
                    h.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
                }
            });
#endif
#if WINDOWS
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is Entry)
                {
                    h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                    h.PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(0, 10, 0, 0);
                    h.PlatformView.Resources["TextControlBorderThemeThicknessFocused"] = new Microsoft.UI.Xaml.Thickness(0);
                }
            });
#endif
#if MACCATALYST || IOS
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is Entry)
                {
                    h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                }
            });
#endif
        }
    }
}
