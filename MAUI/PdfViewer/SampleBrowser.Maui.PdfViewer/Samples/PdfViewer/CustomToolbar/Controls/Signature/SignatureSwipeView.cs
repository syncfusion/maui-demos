#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.Themes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureSwipeView : SwipeUpView
    {
        Grid? deleteContainer;
        Grid? createContainer;
        Grid mainGrid;
        Label? createLabel;
        SignatureItem? longPressedSignatureItem;
        SfListView listView;
        internal SignatureListBorder? SelectedBorder { get; set; }
        internal SignatureListBorder? LongPressedBorder { get; set; }

        private static readonly BindableProperty SignatureLabelTextColorProperty = BindableProperty.Create("SignatureLabelTextColorProperty", typeof(Color), typeof(SignatureSwipeView), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty SignatureDeleteButtonTextColorProperty = BindableProperty.Create("DeleteButtonTextColorProperty", typeof(Color), typeof(SignatureSwipeView), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCreateButtonTextColorProperty = BindableProperty.Create("CreateButtonTextColorProperty", typeof(Color), typeof(SignatureSwipeView), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureSwipeViewSeparatorColorProperty = BindableProperty.Create("SignatureSwipeViewSeparatorColorProperty", typeof(Color), typeof(SignatureSwipeView), defaultValue: Color.FromArgb("#49454F"));
        
        internal Color SignatureLabelTextColor
        {
            get => (Color)GetValue(SignatureLabelTextColorProperty);
            set => SetValue(SignatureLabelTextColorProperty, value);
        }

        internal Color DeleteButtonTextColor
        {
            get => (Color)GetValue(SignatureDeleteButtonTextColorProperty);
            set => SetValue(SignatureDeleteButtonTextColorProperty, value);
        }

        internal Color CreateButtonTextColor
        {
            get => (Color)GetValue(SignatureCreateButtonTextColorProperty);
            set => SetValue(SignatureCreateButtonTextColorProperty, value);
        }

        internal Color SignatureSwipeViewSeparatorColor
        {
            get => (Color)GetValue(SignatureSwipeViewSeparatorColorProperty);
            set => SetValue(SignatureSwipeViewSeparatorColorProperty, value);
        }

        internal event EventHandler? OnCreateRequested;
        internal event EventHandler? SignatureSelected;

        internal SignatureSwipeView(ObservableCollection<SignatureItem> signatureItems)
        {
            ApplyDynamicStyles();
            mainGrid = new Grid();
            this.Background = Colors.Transparent;
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = 48 });
            mainGrid.RowDefinitions.Add(new RowDefinition());
            CreateHeaderGrid();
            listView = CreateSignatureListview();
            listView.ItemsSource = signatureItems;
            CreateSignatureListViewContainer(listView);
            AddContent(mainGrid);
        }

        void CreateHeaderGrid()
        {
            Grid headerGrid = new Grid();
            mainGrid.Add(headerGrid, 0, 0);
            Grid headingContainer = new Grid() { Padding = 15 };
            headingContainer.HeightRequest = 48;
            var signatureLabel = new Label
            {
                Text = "Signature",
                FontSize = 16,
                FontFamily = "Roboto",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center
            };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                signatureLabel.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
            }
            headingContainer.Add(signatureLabel);

            deleteContainer = new Grid()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                IsVisible = false,
            };
            TapGestureRecognizer deleteButtonGesture = new TapGestureRecognizer();
            deleteButtonGesture.Tapped += DeleteButtonGesture_Tapped;
            deleteContainer.GestureRecognizers.Add(deleteButtonGesture);
            createContainer = new Grid()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
            };
            TapGestureRecognizer createContainerGesture = new TapGestureRecognizer();
            createContainerGesture.Tapped += CreateContainerGesture_Tapped;
            createContainer.GestureRecognizers.Add(createContainerGesture);
            createLabel = new Label()
            {
                WidthRequest = 50,
                Text = "Create",
                FontFamily = "Roboto",
                Margin = new Thickness(36, 0, 0, 0),
                FontSize = 14,
                VerticalTextAlignment = TextAlignment.Center
            };
            Label createButton = new Label()
            {
                Text = "\uE70D",
                FontSize = 16,
                Background = Colors.Transparent,
                FontFamily = "MauiMaterialAssets",
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(18, 0, 0, 0)
            };
            Label deleteLabel = new Label()
            {
                WidthRequest = 50,
                Text = "Delete",
                FontFamily = "Roboto",
                Margin = new Thickness(36, 0, 0, 0),
                FontSize = 14,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };
            Label deleteButton = new Label()
            {
                Text = "\uE70F",
                FontSize = 16,
                Background = Colors.Transparent,
                FontFamily = "MauiMaterialAssets",
                Margin = new Thickness(18, 0, 0, 0),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
            };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                createLabel.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["FlatButtonForegroundLight"],
                    (Color)Application.Current.Resources["FlatButtonForeground"]);
                createButton.SetAppThemeColor(Button.TextColorProperty,
                        (Color)Application.Current.Resources["FlatButtonForegroundLight"],
                        (Color)Application.Current.Resources["FlatButtonForeground"]);
                deleteLabel.SetAppThemeColor(Label.TextColorProperty,
                        (Color)Application.Current.Resources["IconColourLight"],
                        (Color)Application.Current.Resources["IconColour"]);
                deleteButton.SetAppThemeColor(Button.TextColorProperty,
                        (Color)Application.Current.Resources["IconColourLight"],
                        (Color)Application.Current.Resources["IconColour"]);
            }
            deleteContainer.Add(deleteLabel);
            deleteContainer.Add(deleteButton);
            createContainer.Add(createLabel);
            createContainer.Add(createButton);
            headingContainer.Add(createContainer);
            headingContainer.Add(deleteContainer);
            var separatorBorder = new Grid
            {
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Fill,
                Opacity = 0.3f
            };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                separatorBorder.SetAppThemeColor(Grid.BackgroundColorProperty,
                    (Color)Application.Current.Resources["BorderLight"],
                    (Color)Application.Current.Resources["Border"]);
            }
            headerGrid.Children.Add(headingContainer);
            headerGrid.Children.Add(separatorBorder);
        }

        private void CreateContainerGesture_Tapped(object? sender, TappedEventArgs e)
        {
            OnCreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteButtonGesture_Tapped(object? sender, TappedEventArgs e)
        {
            if (longPressedSignatureItem != null)
            {
                if (deleteContainer != null)
                    deleteContainer.IsVisible = false;
                if (createContainer != null)
                    createContainer.IsVisible = true;
                if (listView != null && listView.ItemsSource is ObservableCollection<SignatureItem> itemsSource && itemsSource.Contains(longPressedSignatureItem))
                    itemsSource.Remove(longPressedSignatureItem);
            }
        }

        void CreateSignatureListViewContainer(SfListView listView)
        {
            Grid listViewContainer = new Grid();
            listViewContainer.RowDefinitions.Add(new RowDefinition() { Height = 291 });
            listViewContainer.Margin = new Thickness(12, 10, 12, 0);
            listViewContainer.Add(listView);
            mainGrid.Add(listViewContainer, 0, 1);
        }

        SfListView CreateSignatureListview()
        {
            listView = new SfListView();
            listView.ItemSize = 75;
            listView.ItemTapped += ListView_ItemTapped;
            listView.ScrollBarVisibility = ScrollBarVisibility.Default;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                ViewCell view = new ViewCell();
                SignatureListBorder border = new SignatureListBorder();
                border.LongPressed += ListBorder_LongPressed;
                border.Tapped += ListBorder_Tapped;
                ContentView image = new ContentView();
                image.SetBinding(ContentView.ContentProperty, new Binding("View"));
                border.Content = image;
                view.View = border;

                return view;
            });
            return listView;
        }

        private void ApplyDynamicStyles()
        {
            
        }

        private void ListBorder_Tapped(object? sender, EventArgs e)
        {
            if (sender is SignatureListBorder border && border.BindingContext is SignatureItem)
            {
                if (createContainer != null)
                    createContainer.IsVisible = true;
                if (deleteContainer != null)
                    deleteContainer.IsVisible = false;
                SelectedBorder = border;
                SignatureHelper.CurrentSignatureItem = (SignatureItem)border.BindingContext;
                SignatureSelected?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ListBorder_LongPressed(object? sender, EventArgs e)
        {
            if (sender is SignatureListBorder border && border.BindingContext is SignatureItem)
            {
                if (createContainer != null)
                    createContainer.IsVisible = false;
                if (deleteContainer != null)
                    deleteContainer.IsVisible = true;
                longPressedSignatureItem = (SignatureItem)border.BindingContext;
                LongPressedBorder = border;
            }
        }

        private void ListView_ItemTapped(object? sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            if (listView != null && e.DataItem is SignatureItem tappedItem)
            {
                listView.SelectionBackground = Colors.Transparent;
                if (listView.ItemsSource is ObservableCollection<SignatureItem> itemsSource)
                {
                    foreach(SignatureItem item in itemsSource)
                    {
                        item.IsSelected = item == tappedItem;
                    }
                }
                SignatureHelper.CurrentSignatureItem = tappedItem;
                SignatureSelected?.Invoke(this, EventArgs.Empty);
            }
        }

        public new void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureLabelTextColorProperty, "SfPdfViewerSignatureListViewHeaderTextColor");
            this.SetDynamicResource(SignatureDeleteButtonTextColorProperty, "SfPdfViewerSignatureListViewDeleteButtonTextColor");
            this.SetDynamicResource(SignatureCreateButtonTextColorProperty, "SfPdfViewerSignatureListViewCreateButtonTextColor");
            this.SetDynamicResource(SignatureSwipeViewSeparatorColorProperty, "SfPdfViewerSignatureListViewDragIndicatorColor");
        }

        public new void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
