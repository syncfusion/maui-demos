using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;

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
            AnimateChildren(0);
        }

        CardViewExt? oldCardView = null;
        CardViewExt? currentCardView = null;


        private void ScrollView_Scrolled(object? sender, ScrolledEventArgs e)
        {
            AnimateChildren(e.ScrollY);


			if (currentCardView != null && oldCardView != currentCardView)
			{
                OnScrollingToNewCardViewExt(currentCardView);

			}

			oldCardView = currentCardView;
		}

        private void ScrollviewExt_ChildAdded(object? sender, ElementEventArgs e)
        {
            
        }

        List<CardViewExt> oldListItems = new List<CardViewExt>();


        private void AnimateChildren(double y)
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

        private List<CardViewExt> GetChildrenPosition(double y)
        {
            VerticalStackLayout? child = this.ScrollView!.Content as VerticalStackLayout;
            List<CardViewExt> positions = new List<CardViewExt>();
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
                            }
						}
					}
				}
			}
        }
    }
}