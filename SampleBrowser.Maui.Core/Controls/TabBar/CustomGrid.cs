#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.Core
{
    public class CustomGrid : Grid, ITouchListener
    {

        public CustomGrid()
        {
            this.AddTouchListener(this);
        }

        public void OnTouch(PointerEventArgs e)
        {
            var parentView = this.Parent;
            var badgeView = (SfBadgeView)((Grid)parentView).Parent;
            
            if (e.Action == PointerActions.Entered)
            {
                if (badgeView != null)
                {
                    //TODO: Need to replace this once EffectView highlight issue fixed
                    badgeView.BackgroundColor = Color.FromArgb("#77f1e8ff");
                }
            }
            else if (e.Action == PointerActions.Exited)
            {
                if (badgeView != null)
                {
                    //TODO: Need to replace this once EffectView highlight issue fixed
                    badgeView.BackgroundColor = Color.FromArgb("#00ffffff");
                }
            }
        }
    }

    public class CustomLabel : Label, ITouchListener
    {
        public CustomLabel()
        {
#if WINDOWS || MACCATALYST
            this.AddTouchListener(this);
#endif
        }

        public void OnTouch(PointerEventArgs e)
        {
            var badge = this.Parent.Parent;
            var grid = badge.Parent;
            var frame = (Frame)((Grid)grid).Children[1];
            if (e.Action == PointerActions.Entered)
            {
                frame.BackgroundColor = Color.FromArgb("#F5F5F5");
                frame.IsVisible = true;
            }
            else if (e.Action == PointerActions.Exited)
            {
                frame.BackgroundColor = Color.FromArgb("#00FFFFFF");
            }
        }
    }
}
