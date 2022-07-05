#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Core
{
    /// <summary>
    /// Content View from which all samples has been inherited
    /// </summary>
    public class SampleView : ContentView
    {
        CardViewExt? oldCardView = null;
        CardViewExt? currentCardView = null;
        View? previousContent = null;
        View? expandedCardViewContent = null;
        private TapGestureRecognizer? tapGestureRecognizer;
        private List<CardViewExt> oldListItems = new();
        private readonly List<CardViewExt> newCards = new();

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
            set
            {
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
            List<CardViewExt> positions = new();

            if (RunTimeDevice.IsMobileDevice())
            {
                foreach (var item in child!.Children)
                {
                    if (item is CardViewExt)
                    {
                        CardViewExt? card = item as CardViewExt;
                        int hitAreaValue = 50;
                        Point point = new(card!.Y, card!.Y + hitAreaValue);
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
                            Point point = new(card!.Y, card!.Y + hitAreaValue);
                            if (y <= point.X && (y + this.ScrollView.Height) >= point.Y)
                            {
                                foreach (var itemGrid in card.Children)
                                {
                                    if (itemGrid is Grid grid)
                                    {
                                        var cardloaded = grid.Children[0];
                                        positions.Add((CardViewExt)cardloaded);
                                    }
                                }
                            }
                            point = new Point(card.Y + (card.Height - hitAreaValue), card.Y + card.Height);
                            if (y <= point.X && (y + this.ScrollView.Height) >= point.Y)
                            {
                                foreach (var itemGrid in card.Children)
                                {
                                    if (itemGrid is Grid grid)
                                    {
                                        var cardloaded = grid.Children[0];
                                        positions.Add((CardViewExt)cardloaded);
                                    }
                                }
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
                if (this.Content is ScrollView content)
                {
                    if (content.Content is VerticalStackLayout item)
                    {
                        foreach (var view in item.Children)
                        {
                            if (view is CardViewExt ext)
                            {
                                ext?.OnAppearing();
                                if (!RunTimeDevice.IsMobileDevice() && view != null)
                                {
                                    var cardView = ext;
                                    if (cardView != null)
                                    {
                                        this.UpdateExpandIcon(cardView);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        private void LoadDesktopExpandedView(CardViewExt sampleViewContent)
        {
            this.previousContent = this.Content;

            var type = SamplePage.GetAssembliesType(SamplePage.CurrentBrowser!, SamplePage.CurrentSampleName!, SamplePage.CurrentControlName!);

            var sampleView = Activator.CreateInstance(type!) as SampleView;

            var scrollViewExt = (ScrollViewExt)sampleView!.Content;

            var sampleGrid = new Grid();
            sampleGrid.RowDefinitions.Add(new RowDefinition() { Height = 50 });
            sampleGrid.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Star });

            Label sampleTitle = new();
            sampleTitle.Text = sampleViewContent.Title;
            sampleTitle.TextColor = Colors.Black;
            sampleTitle.VerticalTextAlignment = Microsoft.Maui.TextAlignment.Center;
            sampleTitle.FontSize = 20;
            sampleTitle.Margin = new Microsoft.Maui.Thickness(10, 0);
            sampleTitle.HorizontalOptions = LayoutOptions.Start;

            Grid collapseGrid = new();

            Image collapseButton = new();
            collapseButton.Source = "Close";
            collapseButton.WidthRequest = 25;
            collapseButton.HeightRequest = 25;
            collapseButton.Margin = new Microsoft.Maui.Thickness(10, 0);
            collapseButton.Source = "collapse.png";
            collapseButton.HorizontalOptions = LayoutOptions.End;

            collapseGrid.Children.Add(collapseButton);

            TapGestureRecognizer buttonTapGestureRecognizer = new();
            buttonTapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped1;
            collapseGrid.GestureRecognizers.Add(buttonTapGestureRecognizer);

            if (scrollViewExt.Content is VerticalStackLayout wrapLayout)
            {
                foreach (var item in wrapLayout.Children)
                {
                    if (item is CardViewExt cardViewExt)
                    {
                        if (cardViewExt.Title == sampleViewContent.Title)
                        {
                            var sampleContentView = cardViewExt.MainContent;
                            expandedCardViewContent = sampleContentView;
                            Grid.SetRow(sampleContentView, 1);
                            this.OnExpandedViewAppearing(sampleContentView as View);
                            sampleGrid.Children.Add(sampleContentView);
                            this.Content = sampleGrid;
                        }
                    }
                }
            }

            sampleGrid.Add(sampleTitle);
            sampleGrid.Add(collapseGrid);
        }

        private void TapGestureRecognizer_Tapped1(object? sender, EventArgs e)
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


        public void UpdateExpandIcon(CardViewExt cardView)
        {
            this.tapGestureRecognizer = new TapGestureRecognizer();
            this.tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            cardView.titleLayout.GestureRecognizers.Add(this.tapGestureRecognizer);
        }

        private void TapGestureRecognizer_Tapped(object? sender, System.EventArgs e)
        {
            var parentStackLayout = ((Grid)sender!).Parent;
            var parentGrid = ((Grid)parentStackLayout).Parent;
            var expanedCardViewExt = ((Grid)parentGrid).Parent as CardViewExt;
            this.LoadDesktopExpandedView(expanedCardViewExt!);
        }
    }
}