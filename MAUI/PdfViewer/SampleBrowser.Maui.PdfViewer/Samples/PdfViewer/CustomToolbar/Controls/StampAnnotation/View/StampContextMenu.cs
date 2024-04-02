#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.Core.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class StampContextMenu: ContentView
    {
        Color lightThemeColor = new Color();
        Color darkThemeColor = new Color();
        StackLayout stackLayout = new StackLayout();
        Border contextMenuborder = new Border()
        {
            Padding = new Thickness(0, 7, 0, 7),
            BackgroundColor = Color.FromArgb("#EEE8F4"),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = 5
            }
        };
        Grid? selectedItem = null;

        public event EventHandler<ItemTappedEventArgs>? ItemTapped;

        public StampContextMenu()
        {
            this.HorizontalOptions = LayoutOptions.Start;
            this.VerticalOptions = LayoutOptions.Start;
            contextMenuborder.Content = stackLayout;
            this.Content = contextMenuborder;
            stackLayout.Spacing = 0;
            if (Application.Current != null)
            {
                lightThemeColor = (Color)Application.Current.Resources["SampleBrowserBackgroundLight"];
                darkThemeColor = (Color)Application.Current.Resources["BackgroundDark"];
            }
            contextMenuborder.SetAppThemeColor(Label.BackgroundColorProperty, lightThemeColor, darkThemeColor);
        }

        public void AddSeparator()
        {
            Line line = new Line();
            line.BackgroundColor = Color.FromArgb("#FFCAC4D0");
            line.HeightRequest = 1;
            stackLayout.Children.Add(line);
        }

        Grid ChildRow(View view)
        {
            GestureGrid childRow = new GestureGrid();
            childRow.Children.Add(view);
            childRow.PointerPressed += ItemClicked;
            view.PropertyChanged += ChildRowPropertyChanged;
            return childRow;
        }

        public void AddItem(View view)
        {
            stackLayout.Children.Add(ChildRow(view));
        }

        private void ChildRowPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsEnabled")
            {
                if (sender is View view && view.Parent is GestureGrid grid)
                {
                    grid.ClearBackground();
                    grid.IsEnabled = view.IsEnabled;
                    grid.Opacity = grid.IsEnabled ? 1 : 0.25;
                }
            }
        }

        private void ItemClicked(object? sender, EventArgs e)
        {
            GestureGrid? grid = sender as GestureGrid;
            ItemTapped?.Invoke(this, new ItemTappedEventArgs(grid!.Children[0]));
        }

        public void Clear()
        {
            selectedItem!.BackgroundColor = Colors.Transparent;
        }
    }

    public class GestureGrid : Grid, Syncfusion.Maui.Core.Internals.ITouchListener
    {
        Color darkThemeHoverColor = new Color();
        Color lightThemeHoverColor = new Color();
        public GestureGrid()
        {
            this.AddTouchListener(this);
            if (Application.Current != null)
            {
                lightThemeHoverColor = (Color)Application.Current.Resources["BlackOverlayHoveredLight"];
                darkThemeHoverColor = (Color)Application.Current.Resources["FilledInputBackgroundFocused"];
            }
        }

        internal event EventHandler<EventArgs>? PointerPressed;
        Color normalColor = Colors.Transparent;
        Color mouseHoverColor = Color.FromArgb("#141C1B1F");
        Color pressedColor = Color.FromArgb("#261C1B1E");

        public void OnTouch(Syncfusion.Maui.Core.Internals.PointerEventArgs e)
        {
            if (this.IsEnabled == false)
                return;
            if (this.Children[0].InputTransparent)
                return;
            switch (e.Action)
            {
                case PointerActions.Pressed:
                    this.BackgroundColor = pressedColor;
                    PointerPressed?.Invoke(this, EventArgs.Empty);
                    break;
                case PointerActions.Entered:
                    this.SetAppThemeColor(Label.BackgroundColorProperty, lightThemeHoverColor, darkThemeHoverColor);
                    break;
                case PointerActions.Exited:
                    this.BackgroundColor = normalColor;
                    break;
#if ANDROID || IOS
                case PointerActions.Released:
                    this.BackgroundColor = normalColor;
                    break;
#endif
            }
        }

        public void ClearBackground()
        {
            this.BackgroundColor = normalColor;
        }
    }

    public class ItemTappedEventArgs : EventArgs
    {
        public View? TappedItem;

        public ItemTappedEventArgs(IView view)
        {
            TappedItem = view as View;
        }
    }
}
