#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections;
using Syncfusion.Maui.Core;
using ListControl = Microsoft.Maui.Controls.ListView;

namespace SampleBrowser.Maui.ParallaxView.SfParallaxView
{
    public class CustomListView : ListControl, IParallaxView
    {
        public double ItemMargin { get; set; }
        public Size ScrollableContentSize { get; set; }

        public event EventHandler<ParallaxScrollingEventArgs>? Scrolling;

        public CustomListView()
        {
            this.Scrolled += CustomListView_Scrolled;

#if ANDROID
            var info = DeviceDisplay.Current.MainDisplayInfo;
            ItemMargin = info.Density * 3;
#endif
        }

        private void CustomListView_Scrolled(object? sender, ScrolledEventArgs e)
        {
            var listView = sender as ListControl;

            if (Scrolling != null)
            {
                Scrolling.Invoke(this, new ParallaxScrollingEventArgs(e.ScrollX, e.ScrollY, false));
            }
        }

        protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
        {
            var minimumSize = new Size(40, 40);
            Size request = new Size(0,0);

            var list = ItemsSource as IList;
            if (list != null && HasUnevenRows == false && RowHeight > 0 && !IsGroupingEnabled)
            {
                request = new Size(widthConstraint, list.Count * RowHeight + ItemMargin);
            }

            var width =Math.Max(request.Width, minimumSize.Width);
            var height =Math.Max(request.Height, minimumSize.Height);
            this.ScrollableContentSize = new Size(width, height);
            return base.MeasureOverride(widthConstraint, heightConstraint);
        }
    }
}
