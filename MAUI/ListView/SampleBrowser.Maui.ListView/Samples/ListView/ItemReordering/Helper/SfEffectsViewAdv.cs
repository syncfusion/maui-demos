#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.Core;
using PointerEventArgs = Syncfusion.Maui.Core.Internals.PointerEventArgs;

namespace SampleBrowser.Maui.ListView.SfListView
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
                else if (e.Action == PointerActions.Exited)
                {
                    this.Reset();
                }             
            }
        }
    }
}
