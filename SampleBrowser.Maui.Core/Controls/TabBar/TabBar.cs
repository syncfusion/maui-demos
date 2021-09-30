#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using CoreEventArgs = SampleBrowser.Maui.Core.SelectedIndexChangedEventArgs;

namespace SampleBrowser.Maui.Core
{
    public class TabBar : Grid
    {
        #region members

        private ScrollView? tabHeaderScrollView;

        private Grid? tabHeaderContentContainer;

        private Grid? tabSelectionIndicator;

        private HorizontalStackLayout? tabHeaderItemContent;

        private int defaultIndicatorHeight = 2;

        private CoreEventArgs? selectionIndexChangedEventArgs;

        ///<summary>
        ///
        /// </summary>
        public event EventHandler<CoreEventArgs>? SelectedIndexChanged;

        #endregion

        #region constructor

        ///<summary>
        ///
        /// </summary>
        public TabBar()
        {
            this.InitializeControl();
            this.selectionIndexChangedEventArgs = new CoreEventArgs();
        }


        #endregion

        #region properties

        ///<summary>
        ///
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(TabBar), null,
                propertyChanged: OnItemsSourceChanged);

        ///<summary>
        ///
        /// </summary>
        public IList? ItemsSource
        {
            get => (IList?)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as TabBar)?.UpdateItemsSource();

        public static readonly BindableProperty ItemDataTemplateProperty =
            BindableProperty.Create(nameof(ItemDataTemplate), typeof(DataTemplate), typeof(TabBar), null);

        public DataTemplate? ItemDataTemplate
        {
            get => (DataTemplate?)GetValue(ItemDataTemplateProperty);
            set => SetValue(ItemDataTemplateProperty, value);
        }

        ///<summary>
        ///
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(double), typeof(TabBar), null, BindingMode.TwoWay,
                propertyChanged: OnSelectedIndexChanged);

        ///<summary>
        ///
        /// </summary>
        public double SelectedIndex
        {
            get => (double)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as TabBar)?.UpdateSelectedIndex((double)newValue, (double)oldValue);

        internal void RaiseSelectedIndexChangedEvent(CoreEventArgs args)
        {
            if (this.SelectedIndexChanged != null)
            {
                this.SelectedIndexChanged(this, args);
            }
        }

        #endregion

        #region override methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widthConstraint"></param>
        /// <param name="heightConstraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
        {
            if (this.Height <= 0)
            {
                this.UpdateTabHeaderItemHeight();
            }
            this.UpdateTabIndicatorWidth();

            return base.MeasureOverride(widthConstraint, heightConstraint);
        }

        #endregion

        #region private methods

        void InitializeControl()
        {
            this.BackgroundColor = Colors.White;
            this.InitializeTabHeaderContainer();
        }

        void InitializeTabHeaderContainer()
        {
            this.tabSelectionIndicator = new Grid()
            {
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.FromArgb("#6200EE"),
                HeightRequest = this.defaultIndicatorHeight,
                VerticalOptions = LayoutOptions.End,
        };
            this.tabHeaderItemContent = new HorizontalStackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 48,
            };
            this.tabHeaderContentContainer = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Children = { tabSelectionIndicator, tabHeaderItemContent },
                HeightRequest = 48,
            };
            this.tabHeaderScrollView = new ScrollView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Fill,
                Content = this.tabHeaderContentContainer,
                Orientation = ScrollOrientation.Horizontal,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                HeightRequest = 48,
            };

            this.Children.Add(this.tabHeaderScrollView);
        }

        void UpdateTabIndicatorWidth()
        {
            if (this.tabSelectionIndicator != null && this.tabHeaderItemContent != null && this.tabHeaderItemContent?.Children.Count > 0 && this.tabHeaderItemContent.Children.Count > this.SelectedIndex)
            {
                double selectedViewX = this.tabHeaderItemContent.Children[(int)this.SelectedIndex].Frame.X;

                if (this.tabHeaderItemContent?.Children[(int)this.SelectedIndex] is Grid)
                {
                    var child = this.tabHeaderItemContent?.Children[(int)this.SelectedIndex] as Grid;
                    if (child != null && child.Width > 0)
                    {
                        this.tabSelectionIndicator.WidthRequest = child.Width;
                    }
                }
                else
                {
                    this.tabSelectionIndicator.WidthRequest = 0.1d;
                }

                if (this.SelectedIndex < this.tabHeaderItemContent?.Children.Count)
                {
                    this.UpdateTabIndicatorPosition(selectedViewX);
                }
            }
        }

        void UpdateItemsSource()
        {
            if (this.ItemsSource != null && this.ItemDataTemplate != null)
            {
                if (this.ItemsSource is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged)this.ItemsSource).CollectionChanged -= TabBar_CollectionChanged;
                    ((INotifyCollectionChanged)this.ItemsSource).CollectionChanged += TabBar_CollectionChanged;
                }

                this.UpdateControlOnItemsSourceChange();
            }
        }

        private void TabBar_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateControlOnItemsSourceChange();
        }

        void AddTabViewItemFromTemplateToTabHeader(object? item, int index = -1)
        {
            if (this.ItemDataTemplate != null)
            {
                // Due to View limitation we have used as Frame. Once the issue resolved we need to replace as View
                var view = (Grid)this.ItemDataTemplate.CreateContent();
                view.BindingContext = item;
                this.AddTabHeaderItems(view, index);
            }
        }

        void UpdateHeaderItemTemplate()
        {
            if (this.ItemsSource != null)
            {
                for (int i = 0; i < this.ItemsSource.Count; i++)
                {
                    this.AddTabViewItemFromTemplateToTabHeader(this.ItemsSource[i], i);
                }
            }
        }

        #endregion

        #region internal methods

        internal void UpdateTabIndicatorPosition(double x)
        {
            if (this.tabSelectionIndicator != null)
            {
                if (this.tabSelectionIndicator.Width >= 0)
                {
                    this.tabSelectionIndicator.TranslateTo(x, 0, 100, Easing.Linear);
                    this.UpdateScrollViewPosition((int)this.SelectedIndex);
                }
            }
        }

        internal void UpdateControlOnItemsSourceChange()
        {
            if (this.tabHeaderItemContent != null)
            {
                this.tabHeaderItemContent.Children.Clear();
                this.UpdateHeaderItemTemplate();
            }
            this.UpdateTabIndicatorWidth();
        }

        internal void UpdateSelectedIndex(double newIndex, double oldIndex)
        {
            if (newIndex != -1)
            {
                this.UpdateTabIndicatorWidth();
            }
        }

        //TODO: Need to remove once dynamic grid height issue fixed.
        internal void UpdateTabHeaderItemHeight()
        {
            if (this.tabHeaderItemContent != null && this.tabHeaderItemContent.Height != this.Height)
            {
                this.tabHeaderItemContent.HeightRequest = this.Height;
            }
        }

        internal void AddTabHeaderItems(Grid item, int index = -1)
        {
            item.VerticalOptions = LayoutOptions.Fill;

            CustomGrid touchEffectGrid = new CustomGrid();
            touchEffectGrid.Touched += TouchEffectGrid_Touched;
            this.AddTouchEffects(touchEffectGrid);
            touchEffectGrid.Children.Add(item);

            if (this.tabHeaderItemContent != null)
            {
                if (index >= 0)
                {
                    this.tabHeaderItemContent.Children.Insert(index, touchEffectGrid);
                }
                else
                {
                    this.tabHeaderItemContent.Children.Add(touchEffectGrid);
                    var count = this.tabHeaderItemContent.Children.Count - 1;
                    item.SetValue(Grid.ColumnProperty, count);
                }
            }
        }

        private void TouchEffectGrid_Touched(object? sender, Syncfusion.Maui.Graphics.Internals.TouchListenerEventArgs e)
        {
            if(e.Action == Syncfusion.Maui.Graphics.Internals.TouchActions.Pressed)
            {
                if (sender is CustomGrid && this.tabHeaderItemContent != null)
                {
                    if (((CustomGrid)sender).Children.Count > 0 && ((CustomGrid)sender).Children[0] is SfEffectsView)
                    {
                        var effectsView = ((CustomGrid)sender).Children[0] as SfEffectsView;
                        if (effectsView != null)
                        {
                            effectsView.ApplyEffects();
                        }
                    }
                }
            }
            else if(e.Action == Syncfusion.Maui.Graphics.Internals.TouchActions.Released)
            {
                if (this.selectionIndexChangedEventArgs != null)
                {
                    this.selectionIndexChangedEventArgs.OldIndex = (int)this.SelectedIndex;
                    if (sender is CustomGrid && this.tabHeaderItemContent != null)
                    {
                        var index = this.tabHeaderItemContent.Children.IndexOf(sender as CustomGrid);
                        if (((CustomGrid)sender).Children.Count > 0 && ((CustomGrid)sender).Children[0] is SfEffectsView)
                        {
                            var effectsView = ((CustomGrid)sender).Children[0] as SfEffectsView;
                            if (effectsView != null)
                            {
                                effectsView.Reset();
                            }
                        }

                        if (index != this.SelectedIndex)
                        {
                            this.SelectedIndex = index;
                            this.selectionIndexChangedEventArgs.NewIndex = (int)this.SelectedIndex;
                            this.RaiseSelectedIndexChangedEvent(this.selectionIndexChangedEventArgs);
                        }
                    }
                }
            }
            else if(e.Action == Syncfusion.Maui.Graphics.Internals.TouchActions.Cancelled)
            {
                if (sender is CustomGrid && this.tabHeaderItemContent != null)
                {
                    var index = this.tabHeaderItemContent.Children.IndexOf(sender as CustomGrid);
                    if (((CustomGrid)sender).Children.Count > 0 && ((CustomGrid)sender).Children[0] is SfEffectsView)
                    {
                        var effectsView = ((CustomGrid)sender).Children[0] as SfEffectsView;
                        if (effectsView != null)
                        {
                            effectsView.Reset();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add the effects layout to the grid
        /// </summary>
        /// <param name="touchEffectGrid"></param>
        private void AddTouchEffects(Grid touchEffectGrid)
        {
            SfEffectsView effectsView = new SfEffectsView();
            touchEffectGrid.Children.Add(effectsView);
        }

        internal async void UpdateScrollViewPosition(int position)
        {
            if (this.tabHeaderScrollView != null)
            {
                await this.tabHeaderScrollView.ScrollToAsync(this.tabHeaderItemContent?.Children[position] as Element, ScrollToPosition.Center, true);
            }
        }

        #endregion
    }

    public class SelectedIndexChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public double OldIndex { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double NewIndex { get; internal set; }
    }

}
