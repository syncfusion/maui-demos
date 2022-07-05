#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

namespace SampleBrowser.Maui.Core
{
    public class WrapLayout : VerticalStackLayout
    {
        private readonly List<View> processChildren = new();

        bool isLoaded = false;

        bool canRefresh = true;

        double widthPreview = 0;

        public WrapLayout()
        {
            if (!RunTimeDevice.IsMobileDevice())
            {
                this.Margin = new Microsoft.Maui.Thickness(5);
                this.Opacity = 0;
            }
        }

        internal void PerformAnimation()
        {
            if (this.Parent is ScrollViewExt ext)
            {
                if (this.Parent is ScrollViewExt scrollViewExt && scrollViewExt.Parent is SampleView)
                {
                    Element parent = ext.Parent;
                    ((SampleView)parent).AnimateChildrenDeskTop(0);
                }
            }
        }

        public async void CallRefresh()
        {
            //  this.Opacity = 0;
            await Task.Delay(100);
            this.RearrangeChildren();
            await Task.Delay(500);
            canRefresh = true;

            this.Opacity = 1;
            PerformAnimation();
        }




        protected override Size ArrangeOverride(Rect bounds)
        {
            this.widthPreview = bounds.Width;
            if (canRefresh)
            {
                if (!RunTimeDevice.IsMobileDevice())
                {
                    if (this.widthPreview != this.Width)
                    {
                        this.CallRefresh();
                        this.canRefresh = false;
                    }
                }
            }

            return base.ArrangeOverride(bounds);
        }

        public async void RearrangeChildren()
        {
            this.CalculatedWidth();

            this.PrepareChildren();

            await Task.Delay(100);

            foreach (var item in this.processChildren)
            {
                if (currentWidth > 0)
                {
                    item.WidthRequest = currentWidth;
                    CardViewExt cardViewExt = (CardViewExt)((Grid)item).Children[0];
                    cardViewExt.WidthRequest = currentWidth;
                }
                this.AddChild(item);
            }

        }

        public void PrepareChildren()
        {
            if (!isLoaded)
            {
                foreach (var item in this.Children)
                {
                    isLoaded = true;

                    /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-maccatalyst)'
                    Before:
                                        processChildren.Add(this.WrapChildWithPadding((View)item));
                    After:
                                        processChildren.Add(WrapLayout.WrapChildWithPadding((View)item));
                    */

                    /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-ios)'
                    Before:
                                        processChildren.Add(this.WrapChildWithPadding((View)item));
                    After:
                                        processChildren.Add(WrapLayout.WrapChildWithPadding((View)item));
                    */

                    /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-android)'
                    Before:
                                        processChildren.Add(this.WrapChildWithPadding((View)item));
                    After:
                                        processChildren.Add(WrapLayout.WrapChildWithPadding((View)item));
                    */

                    /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-windows10.0.19041)'
                    Before:
                                        processChildren.Add(this.WrapChildWithPadding((View)item));
                    After:
                                        processChildren.Add(WrapLayout.WrapChildWithPadding((View)item));
                    */
                    processChildren.Add(WrapChildWithPadding((View)item));
                }
            }
            foreach (var item in this.Children)
            {
                if (item is HorizontalStackLayout layout)
                {
                    layout.Children.Clear();
                }

            }
            this.Children.Clear();
        }

        public void AddChild(View view)
        {
            ((Grid)view).IsVisible = false;
            if (this.Children.Count <= 0)
            {

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-maccatalyst)'
                Before:
                                this.Children.Add(this.CreateHorizontalLayout(view));
                After:
                                this.Children.Add(WrapLayout.CreateHorizontalLayout(view));
                */

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-ios)'
                Before:
                                this.Children.Add(this.CreateHorizontalLayout(view));
                After:
                                this.Children.Add(WrapLayout.CreateHorizontalLayout(view));
                */

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-android)'
                Before:
                                this.Children.Add(this.CreateHorizontalLayout(view));
                After:
                                this.Children.Add(WrapLayout.CreateHorizontalLayout(view));
                */

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-windows10.0.19041)'
                Before:
                                this.Children.Add(this.CreateHorizontalLayout(view));
                After:
                                this.Children.Add(WrapLayout.CreateHorizontalLayout(view));
                */
                this.Children.Add(CreateHorizontalLayout(view));
            }
            else
            {
                var lastHorizontalStackLayout = this.Children[this.Children.Count - 1];
                if (lastHorizontalStackLayout is HorizontalStackLayout layout)
                {
                    this.AllocateSpaceBasedOnSize(view, layout);
                }
            }
            ((Grid)view).IsVisible = true;
        }

        public static HorizontalStackLayout CreateHorizontalLayout(View view)
        {
            var newStackLayout = new HorizontalStackLayout();
            newStackLayout.Children.Add(view);
            return newStackLayout;
        }

        int currentWidth = 0;
        private void CalculatedWidth()
        {
            possibleColumn = (int)(this.widthPreview / 400);
            currentWidth = (int)(this.Width / possibleColumn);
        }

        int possibleColumn;
        public void AllocateSpaceBasedOnSize(View view, HorizontalStackLayout layout)
        {
            double totalWidth = 0;
            foreach (var item in layout.Children)
            {
                if (item is Grid)
                {
                    totalWidth += currentWidth;
                }
            }

            if ((totalWidth + currentWidth) <= this.widthPreview)
            {
                layout.Children.Add(view);
            }
            else
            {

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-maccatalyst)'
                Before:
                                var createdView = this.CreateHorizontalLayout(view);
                After:
                                var createdView = WrapLayout.CreateHorizontalLayout(view);
                */

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-ios)'
                Before:
                                var createdView = this.CreateHorizontalLayout(view);
                After:
                                var createdView = WrapLayout.CreateHorizontalLayout(view);
                */

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-android)'
                Before:
                                var createdView = this.CreateHorizontalLayout(view);
                After:
                                var createdView = WrapLayout.CreateHorizontalLayout(view);
                */

                /* Unmerged change from project 'SampleBrowser.Maui.Core (net6.0-windows10.0.19041)'
                Before:
                                var createdView = this.CreateHorizontalLayout(view);
                After:
                                var createdView = WrapLayout.CreateHorizontalLayout(view);
                */
                var createdView = CreateHorizontalLayout(view);
                this.Children.Add(createdView);
            }
        }

        public static Grid WrapChildWithPadding(View view)
        {
            Grid grid = new();
            grid.Children.Add(view);
            grid.WidthRequest = view.WidthRequest;
            grid.HeightRequest = view.HeightRequest;
            return grid;
        }
    }
}
