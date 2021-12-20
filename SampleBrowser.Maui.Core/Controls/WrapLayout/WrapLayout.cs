using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Core
{
    public class WrapLayout : VerticalStackLayout
    {
        List<View> processChildren = new List<View>();

        bool isLoaded = false;

        bool canRefresh = true;

        double widthPreview = 0;

        public WrapLayout()
        {
            if(!RunTimeDevice.IsMobileDevice())
            {
                this.Margin = new Microsoft.Maui.Thickness(5);
                this.Opacity = 0;
            }
        }

        internal  void PerformAnimation()
        {
            if (this.Parent is ScrollViewExt)
            {
                if((this.Parent as ScrollViewExt).Parent is SampleView)
                {
                    ((this.Parent as ScrollViewExt).Parent as SampleView).AnimateChildrenDeskTop(0);
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


        

        protected override Size ArrangeOverride(Rectangle bounds)
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
                    ((item as Grid).Children[0] as CardViewExt).WidthRequest = currentWidth;
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
                    processChildren.Add(this.WrapChildWithPadding(item as View));
                }
            }
            foreach (var item in this.Children)
            {
                if (item is HorizontalStackLayout)
                {
                    (item as HorizontalStackLayout).Children.Clear();
                }

            }
            this.Children.Clear();
        }

        public void AddChild(View view)
        {
            (view as Grid).IsVisible = false;
            if (this.Children.Count <= 0)
            {
                this.Children.Add(this.CreateHorizontalLayout(view));
            }
            else
            {
                var lastHorizontalStackLayout = this.Children[this.Children.Count - 1];
                if (lastHorizontalStackLayout is HorizontalStackLayout)
                {
                    this.AllocateSpaceBasedOnSize(view, lastHorizontalStackLayout as HorizontalStackLayout);
                }
            }
            (view as Grid).IsVisible = true;
        }

        public HorizontalStackLayout CreateHorizontalLayout(View view)
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
                    totalWidth = totalWidth + currentWidth;
                }
            }

            if ((totalWidth + currentWidth) <= this.widthPreview)
            {
                layout.Children.Add(view);
            }
            else
            {
                var createdView = this.CreateHorizontalLayout(view);
                this.Children.Add(createdView);
            }
        }

        public Grid WrapChildWithPadding(View view)
        {
            Grid grid = new Grid();
            grid.Children.Add(view);
            grid.WidthRequest = view.WidthRequest;
            grid.HeightRequest = view.HeightRequest;
            return grid;
        }
    }
}
