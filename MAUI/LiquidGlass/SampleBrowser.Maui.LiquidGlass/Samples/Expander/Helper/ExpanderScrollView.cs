#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Layouts;

namespace SampleBrowser.Maui.LiquidGlass.SfExpander
{
    /// <summary>
    /// Represents to add expander items to scroll view.
    /// </summary>
#if NET8_0_OR_GREATER && (IOS || MACCATALYST)
    internal class ExpanderScrollView : ScrollView, ICrossPlatformLayout
#else
    internal class ExpanderScrollView : ScrollView
#endif  
    {
        public ExpanderScrollView()
        {

        }

        #region Public Methods

#if NET8_0_OR_GREATER && (IOS || MACCATALYST)
        /// <summary>
        /// Override this method due to .net8 blank issue.
        /// </summary>
        /// <param name="bounds">Bounds for expander.</param>
        /// <returns>Size of scroll view.</returns>
        Size ICrossPlatformLayout.CrossPlatformArrange(Microsoft.Maui.Graphics.Rect bounds)
        {
            // 855697 - Checked, from where expander bounds changes and override this method.
            // from ScrollViewHandler ICrossPlatformLayout.CrossPlatformArrange native bounds used instead of frame.
            if (this is IScrollView scrollView)
            {
                bounds.X = 0;
                bounds.Y = 0;
                return scrollView.ArrangeContentUnbounded(bounds);
            }

            return bounds.Size;
        }
#endif

        #endregion
    }
}
