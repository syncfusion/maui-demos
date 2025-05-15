#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
using System.Linq;
using Syncfusion.Maui.ListView;
using Syncfusion.Maui.Themes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignatureListDialog : Border
    {
        Grid? thinBorder;
        Grid? mainGrid;
        SfListView? ListView;
        ObservableCollection<SignatureItem> signatureItems;

        internal event EventHandler? OnCreateRequested;

        private static readonly BindableProperty SignatureListBackgroundColorProperty = BindableProperty.Create("SignatureListBackgroundColor", typeof(Color), typeof(SignatureListDialog), defaultValue: Color.FromArgb("#EEE8F4"));
        private static readonly BindableProperty SignatureCreateButtonTextColorProperty = BindableProperty.Create("SignatureCreateButtonTextColor", typeof(Color), typeof(SignatureListDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCreateButtonStrokeColorProperty = BindableProperty.Create("SignatureCreateButtonStrokeColor", typeof(Color), typeof(SignatureListDialog), defaultValue: Color.FromArgb("#CAC4D0"));
        private static readonly BindableProperty SignatureCreateButtonSeparatorBorderColorProperty = BindableProperty.Create("SignatureCreateButtonSeparatorBorderColor", typeof(Color), typeof(SignatureListDialog), defaultValue: Color.FromArgb("#CAC4D0"));

        internal Color SignatureListBackgroundColor
        {
            get => (Color)GetValue(SignatureListBackgroundColorProperty);
            set => SetValue(SignatureListBackgroundColorProperty, value);
        }

        internal Color SignatureCreateButtonTextColor
        {
            get => (Color)GetValue(SignatureCreateButtonTextColorProperty);
            set => SetValue(SignatureCreateButtonTextColorProperty, value);
        }

        internal Color SignatureCreateButtonStrokeColor
        {
            get => (Color)GetValue(SignatureCreateButtonStrokeColorProperty);
            set => SetValue(SignatureCreateButtonStrokeColorProperty, value);
        }

        internal Color SignatureCreateButtonSeparatorBorderColor
        {
            get => (Color)GetValue(SignatureCreateButtonSeparatorBorderColorProperty);
            set => SetValue(SignatureCreateButtonSeparatorBorderColorProperty, value);
        }
        public SignatureListDialog(ObservableCollection<SignatureItem> signatureItems)
        {
            ApplyDynamicStyles();
            Shadow = new Shadow
            {
                Brush = Colors.Black,
                Offset = new Point(0, 0),
                Radius = 4,
                Opacity = 0.30f
            };
            StrokeShape = new RoundRectangle { CornerRadius = 4 };
            Padding = 0;
            mainGrid = new Grid();
            this.signatureItems = signatureItems;
            this.signatureItems.CollectionChanged += SignatureItems_CollectionChanged;
            if (Application.Current != null && Application.Current.Resources != null)
            {
                this.SetAppThemeColor(Border.BackgroundColorProperty,
                    (Color)Application.Current.Resources["SampleBrowserBackgroundLight"],
                    (Color)Application.Current.Resources["BackgroundDark"]);
            }
            WidthRequest = 230;
            VerticalOptions = LayoutOptions.Start;
            HorizontalOptions= LayoutOptions.Start;
            mainGrid.RowSpacing = 0;
            mainGrid.AddRowDefinition(new RowDefinition());
            mainGrid.AddRowDefinition(new RowDefinition() { Height = 1 });
            mainGrid.AddRowDefinition(new RowDefinition() { Height = 56 });
            AddSignatureListview();
            AddBorder();
            AddCreateButton();
            Content = mainGrid;
        }

        private void SignatureItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (thinBorder != null)
                thinBorder.IsVisible = signatureItems.Any();
        }

        private void AddSignatureListview()
        {
            ListView = new SfListView();
            ListView.ItemsSource = signatureItems;
            ListView.ItemSize = 44;
            ListView.ItemTapped += ListView_ItemTapped;
            ListView.ScrollBarVisibility = ScrollBarVisibility.Default;
            ListView.ItemTemplate = new DataTemplate(() =>
            {
                ViewCell view = new ViewCell();
                SignatureListItemView grid = new SignatureListItemView();
                grid.DeleteClicked += Grid_DeleteClicked;
                view.View = grid;

                return view;
            });
            Grid.SetRow(ListView, 0);
            mainGrid?.Children?.Add(ListView);
        }

        private void Grid_DeleteClicked(object? sender, EventArgs e)
        {
            if (sender is Grid grid && grid.BindingContext is SignatureItem itemToDelete)
                signatureItems.Remove(itemToDelete);
        }

        private void AddBorder()
        {
            thinBorder = new Grid();
            thinBorder.IsVisible = false;
            thinBorder.BackgroundColor = SignatureCreateButtonSeparatorBorderColor;
            Grid.SetRow(thinBorder, 1);
            if (mainGrid != null)
                mainGrid.Children.Add(thinBorder);
        }
        private void AddCreateButton()
        {
            Button button = new Button
            {
                BorderWidth = 1,
                CornerRadius = 8,
                HeightRequest = 40,
                //934618 - [WinUI] No space between words in the Create Signature button in custom toolbar
                Text = "+ Create Signature",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Colors.Transparent,
                WidthRequest = 214,
            };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                button.SetAppThemeColor(Button.BorderColorProperty,
                    (Color)Application.Current.Resources["BorderLight"],
                    (Color)Application.Current.Resources["Border"]);
                button.SetAppTheme(Button.TextColorProperty, 
                    (Color)Application.Current.Resources["FlatButtonForegroundLight"],
                    (Color)Application.Current.Resources["FlatButtonForeground"]);
            }
            
            button.Clicked += CreateButton_Clicked;
            Grid.SetRow(button, 2);
            if (mainGrid != null)
                mainGrid.Children.Add(button);
        }

        private void CreateButton_Clicked(object? sender, System.EventArgs e)
        {
            OnCreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ListView_ItemTapped(object? sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            if (e.DataItem is SignatureItem tappedItem)
            {
                SignatureHelper.CurrentSignatureItem = tappedItem;
                if (Parent is SignatureViewOverlay parent)
                    parent.Hide();
            }
        }

        private void ApplyDynamicStyles()
        {
            
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignatureListBackgroundColorProperty, "SfPdfViewerSignatureListBackgroundColor");
            this.SetDynamicResource(SignatureCreateButtonTextColorProperty, "SfPdfViewerSignatureCreateButtonTextColor");
            this.SetDynamicResource(SignatureCreateButtonStrokeColorProperty, "SfPdfViewerSignatureCreateButtonStrokeColor");
            this.SetDynamicResource(SignatureCreateButtonSeparatorBorderColorProperty, "SfPdfViewerSignatureListViewCreateButtonSeparatorColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
