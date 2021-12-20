#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SampleBrowser.Maui.Core
{
	/// <summary>
	/// Content View from which all samples has been inherited
	/// </summary>
	public class SampleView : ContentView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SampleView"/> class.
		/// </summary>
		public SampleView()
		{
			this.BackgroundColor = Color.FromArgb("#FFFFFF");

        }

        /// <summary>
        /// Gets or sets the view from which property view for samples has been included
        /// </summary>
        public View? PropertyView
		{
			get;
			set;
		}

        private ScrollViewExt? scrollView;

        public ScrollViewExt? ScrollView
        {
            get { return scrollView; }
            set {
				scrollView = value;
                scrollView!.Scrolled += ScrollView_Scrolled;
                scrollView.ChildAdded += ScrollView_ChildAdded;
			}
        }

        private void ScrollView_ChildAdded(object? sender, ElementEventArgs e)
        {
            if (RunTimeDevice.IsMobileDevice())
            {
                AnimateChildren(0);
            }
            else
            {
                AnimateChildrenDeskTop(0);
            }
            
        }

        CardViewExt? oldCardView = null;
        CardViewExt? currentCardView = null;


        private void ScrollView_Scrolled(object? sender, ScrolledEventArgs e)
        {
            if (RunTimeDevice.IsMobileDevice())
            {
                AnimateChildren(e.ScrollY);


                if (currentCardView != null && oldCardView != currentCardView)
                {
                    OnScrollingToNewCardViewExt(currentCardView);

                }

                oldCardView = currentCardView;
            }
            else
            {
                AnimateChildrenDeskTop(e.ScrollY);

                foreach (var item in newCards)
                {
                    OnScrollingToNewCardViewExt(item);
                }
            }
		}

        private void ScrollviewExt_ChildAdded(object? sender, ElementEventArgs e)
        {
            
        }

        List<CardViewExt> oldListItems = new List<CardViewExt>();

        List<CardViewExt> newCards = new List<CardViewExt>();

        internal void AnimateChildren(double y)
        {

            List<CardViewExt> positions = GetChildrenPosition(y);

            foreach (var item in positions)
            {
                if (!oldListItems.Contains(item))
                {
                    currentCardView = item;
                }
            }
            oldListItems = positions;
        }

        internal void AnimateChildrenDeskTop(double y)
        {

            List<CardViewExt> positions = GetChildrenPosition(y);

            foreach (var item in positions)
            {
                if (!oldListItems.Contains(item))
                {
                    currentCardView = item;
                    OnScrollingToNewCardViewExt(item);
                }
            }
            oldListItems = positions;
        }

        private List<CardViewExt> GetChildrenPosition(double y)
        {
            VerticalStackLayout? child = this.ScrollView!.Content as VerticalStackLayout;
            List<CardViewExt> positions = new List<CardViewExt>();

            if (RunTimeDevice.IsMobileDevice())
            {
                foreach (var item in child!.Children)
                {
                    if (item is CardViewExt)
                    {
                        CardViewExt? card = item as CardViewExt;
                        int hitAreaValue = 50;
                        Point point = new Point(card!.Y, card!.Y + hitAreaValue);
                        if (y <= point.X && (y + this.ScrollView.Height) >= point.Y)
                        {
                            positions.Add(card);
                        }
                        point = new Point(card.Y + (card.Height - hitAreaValue), card.Y + card.Height);
                        if (y <= point.X && (y + this.ScrollView.Height) >= point.Y)
                        {
                            positions.Add(card);
                        }
                    }
                }
            }
            else
            {
                if (child is WrapLayout)
                {
                    foreach (var item in child.Children)
                    {
                        if (item is HorizontalStackLayout)
                        {

                            var card = item as HorizontalStackLayout;
                            int hitAreaValue = 100;
                            Point point = new Point(card!.Y, card!.Y + hitAreaValue);
                            if (y <= point.X && (y + this.ScrollView.Height) >= point.Y)
                            {
                                //  positions.Add(card);
                                foreach (var itemGrid in card.Children)
                                {
                                    if (itemGrid is Grid)
                                    {
                                        var cardloaded = (itemGrid as Grid).Children[0];
                                        positions.Add(cardloaded as CardViewExt);
                                    }
                                }
                            }
                            point = new Point(card.Y + (card.Height - hitAreaValue), card.Y + card.Height);
                            if (y <= point.X && (y + this.ScrollView.Height) >= point.Y)
                            {
                                foreach (var itemGrid in card.Children)
                                {
                                    if(itemGrid is Grid)
                                    {
                                        var cardloaded = (itemGrid as Grid).Children[0];
                                        positions.Add(cardloaded as CardViewExt);
                                    }
                                }
                              //  positions.Add(card);
                            }

                        }
                    }
                }
            }
            return positions;
        }


        /// <summary>
        /// Hooked when sample view disappears
        /// </summary>
        public virtual void OnDisappearing()
        {
        }

		public virtual void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {

		}

        public virtual void OnExpandedViewAppearing(View view)
        {

        }

        public virtual void OnExpandedViewDisappearing(View view)
        {

        }

        /// <summary>
        /// Hooked when sample view appears
        /// </summary>
        public virtual void OnAppearing()
        {
			if (this.Content != null)
			{
				var content = this.Content as ScrollView;

				if (content != null)
				{
					var item = content.Content as VerticalStackLayout;
					if (item != null)
					{
						foreach (var view in item.Children)
						{
							if (view is CardViewExt)
                            {
                                ((CardViewExt)view)?.OnAppearing();
                                if (!RunTimeDevice.IsMobileDevice())
                                {
                                    this.UpdateExpandIcon(view as CardViewExt);
                                }
                            }
						}
					}
				}
			}
        }


        View previousContent = null;
        View expandedCardViewContent = null;
        private void LoadDesktopExpandedView(CardViewExt sampleViewContent)
        {
            this.previousContent = this.Content;

            var type =  SamplePage.GetAssembliesType(SamplePage.CurrentBrowser, SamplePage.CurrentSampleName, SamplePage.CurrentControlName);

            var sampleView = Activator.CreateInstance(type!) as SampleView;

            var wrapLayout = (sampleView.Content as ScrollViewExt).Content as VerticalStackLayout;

            var sampleGrid = new Grid();
            sampleGrid.RowDefinitions.Add(new RowDefinition() { Height = 50 });
            sampleGrid.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Star });

            Label sampleTitle = new Label();
            sampleTitle.Text = sampleViewContent.Title;
            sampleTitle.TextColor = Colors.Black;
            sampleTitle.VerticalTextAlignment = Microsoft.Maui.TextAlignment.Center;
            sampleTitle.FontSize = 20;
            sampleTitle.Margin = new Microsoft.Maui.Thickness(10, 0);
            sampleTitle.HorizontalOptions = LayoutOptions.Start;

            Grid collapseGrid = new Grid();

            Image collapseButton = new Image();
            collapseButton.Source = "Close";
            collapseButton.WidthRequest = 25;
            collapseButton.HeightRequest = 25;
            collapseButton.Margin = new Microsoft.Maui.Thickness(10, 0);
            collapseButton.Source = "collapse.png";
            collapseButton.HorizontalOptions = LayoutOptions.End;

            collapseGrid.Children.Add(collapseButton);

            TapGestureRecognizer buttonTapGestureRecognizer = new TapGestureRecognizer();
            buttonTapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped1;
            collapseGrid.GestureRecognizers.Add(buttonTapGestureRecognizer);

            foreach (var item in wrapLayout.Children)
            {
                if (item is CardViewExt)
                {
                    if ((item as CardViewExt).Title == sampleViewContent.Title)
                    {
                        var sampleContentView = (item as CardViewExt).MainContent;
                        expandedCardViewContent = sampleContentView;
                        GridLayout.SetRow(sampleContentView, 1);
                        this.OnExpandedViewAppearing(sampleContentView as View);
                        sampleGrid.Children.Add(sampleContentView);
                        this.Content = sampleGrid;
                    }
                }
            }

            sampleGrid.Add(sampleTitle);
            sampleGrid.Add(collapseGrid);
        }

        private void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            this.LoadDesktopCollapseView();
        }

        private void LoadDesktopCollapseView()
        {
            if (expandedCardViewContent != null)
            {
                this.OnExpandedViewDisappearing(expandedCardViewContent);
            }
            
            this.Content = previousContent;
        }

        private TapGestureRecognizer tapGestureRecognizer;
        public void UpdateExpandIcon(CardViewExt cardView)
        {
            this.tapGestureRecognizer = new TapGestureRecognizer();
            this.tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            cardView.titleLayout.GestureRecognizers.Add(this.tapGestureRecognizer);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var parentStackLayput = (sender as Grid).Parent;
            var parentGrid = (parentStackLayput as VerticalStackLayout).Parent;
            var expanedCardViewExt = (parentGrid as Grid).Parent as CardViewExt;
            this.LoadDesktopExpandedView(expanedCardViewExt);
        }
    }
}