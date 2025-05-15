#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if ANDROID
using Android.Content.Res; 
#endif
using Microsoft.Maui.Platform;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class SfEntry : Entry
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
