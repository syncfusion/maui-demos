#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Core;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.Generic;
using Syncfusion.Maui.TabView;
using System;
using Syncfusion.Maui.Themes;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class IntegratedSignatureView : Grid, IIntegratedSigatureDialog
    {
        Button okButton;
        List<ISignatureCreateView> signatureCreateViews;
        SfTabView? tabView;
        ISignatureCreateView? selectedSignatureCreateView;
        SfTabItem? draw;
        SfTabItem? type;
        SfTabItem? upload;
        Grid? colorGrid;
        public event EventHandler? SignatureCreated;
        public event EventHandler? CloseRequested;

        private static readonly BindableProperty IntegratedSignatureViewBackgroundColorProperty = BindableProperty.Create("IntegratedSignatureViewBackgroundColor", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("FFFBFE"));
        private static readonly BindableProperty IntegratedViewToolbarBackgroundColorProperty = BindableProperty.Create("IntegratedViewToolbarBackgroundColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#F7F2FB"));
        private static readonly BindableProperty IntegratedViewHeaderTextColorProperty = BindableProperty.Create("IntegratedViewHeaderTextColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty IntegratedSignatureSaveTextColorProperty = BindableProperty.Create("IntegratedSignatureSaveTextColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#1C1B1F"));
        private static readonly BindableProperty IntegratedViewSaveCheckboxColorProperty = BindableProperty.Create("IntegratedViewSaveCheckboxColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty IntegratedViewClearTextColorProperty = BindableProperty.Create("IntegratedViewClearTextColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty IntegratedViewSaveLayoutBackgroundColorProperty = BindableProperty.Create("IntegratedViewSaveLayoutBackgroundColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#FFFBFE"));
        private static readonly BindableProperty IntegratedViewColorBarBackgroundProperty = BindableProperty.Create("IntegratedViewColorBarBackground", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#F7F2FB"));
        private static readonly BindableProperty BackButtonColorProperty = BindableProperty.Create("BackButtonColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty OkButtonColorProperty = BindableProperty.Create("OkButtonColor", typeof(Color), typeof(IntegratedSignatureView), defaultValue: Color.FromArgb("#49454F"));

        internal Color IntegratedSignatureViewBackgroundColor
        {
            get => (Color)GetValue(IntegratedSignatureViewBackgroundColorProperty);
            set => SetValue(IntegratedSignatureViewBackgroundColorProperty, value);
        }
        internal Color IntegratedViewToolbarBackgroundColor
        {
            get => (Color)GetValue(IntegratedViewToolbarBackgroundColorProperty);
            set => SetValue(IntegratedViewToolbarBackgroundColorProperty, value);
        }

        internal Color IntegratedViewHeaderTextColor
        {
            get => (Color)GetValue(IntegratedViewHeaderTextColorProperty);
            set => SetValue(IntegratedViewHeaderTextColorProperty, value);
        }

        internal Color IntegratedSignatureSaveTextColor
        {
            get => (Color)GetValue(IntegratedSignatureSaveTextColorProperty);
            set => SetValue(IntegratedSignatureSaveTextColorProperty, value);
        }

        internal Color IntegratedViewSaveCheckboxColor
        {
            get => (Color)GetValue(IntegratedViewSaveCheckboxColorProperty);
            set => SetValue(IntegratedViewSaveCheckboxColorProperty, value);
        }

        internal Color IntegratedViewClearTextColor
        {
            get => (Color)GetValue(IntegratedViewClearTextColorProperty);
            set => SetValue(IntegratedViewClearTextColorProperty, value);
        }

        internal Color IntegratedViewSaveLayoutBackgroundColor
        {
            get => (Color)GetValue(IntegratedViewSaveLayoutBackgroundColorProperty);
            set => SetValue(IntegratedViewSaveLayoutBackgroundColorProperty, value);
        }

        internal Color IntegratedViewColorBarBackground
        {
            get => (Color)GetValue(IntegratedViewColorBarBackgroundProperty);
            set => SetValue(IntegratedViewColorBarBackgroundProperty, value);
        }

        internal Color BackButtonColor
        {
            get => (Color)GetValue(BackButtonColorProperty);
            set => SetValue(BackButtonColorProperty, value);
        }

        internal Color OkButtonColor
        {
            get => (Color)GetValue(OkButtonColorProperty);
            set => SetValue(OkButtonColorProperty, value);
        }

        public IntegratedSignatureView()
        {
            ApplyDynamicStyles();
            if (Application.Current != null && Application.Current.Resources != null)
            {
                this.SetAppThemeColor(Grid.BackgroundColorProperty,
                    (Color)Application.Current.Resources["BackgroundLight"],
                    (Color)Application.Current.Resources["BackgroundDark"]);
            }
            signatureCreateViews = new List<ISignatureCreateView>();

            AddRowDefinition(new RowDefinition() { Height = 64 });
            AddRowDefinition(new RowDefinition());
            AddRowDefinition(new RowDefinition() { Height = GridLength.Auto });
            AddRowDefinition(new RowDefinition() { Height = 64 });

            okButton = new MaterialIconButton();
            tabView = new SfTabView();

            AddHeader();
            AddTabView();
            AddSaveLayout();
            AddColorBar();
        }

        private void ApplyDynamicStyles()
        {
            
        }

        internal void AddTabView()
        {
            tabView = new SfTabView();
            tabView.SelectionChanged += TabView_SelectionChanged;
            FreeHandSignatureCreateView freeHandTab = new FreeHandSignatureCreateView();
            selectedSignatureCreateView = freeHandTab;
            TypeSignatureCreateView typeTab = new TypeSignatureCreateView();
            ImageSignatureCreateView imageTab = new ImageSignatureCreateView();
         

            draw = new SfTabItem { Content = freeHandTab, Header = "Draw" };
            type = new SfTabItem { Content = typeTab, Header = "Type" };
            upload = new SfTabItem { Content = imageTab, Header = "Upload" };
            tabView.Items.Add(draw);
            tabView.Items.Add(type);
            tabView.Items.Add(upload);

            signatureCreateViews.Add(freeHandTab);
            signatureCreateViews.Add(typeTab);
            signatureCreateViews.Add(imageTab);
            this.Add(tabView, 0, 1);
        }

        private void TabView_SelectionChanged(object? sender, TabSelectionChangedEventArgs e)
        {
            selectedSignatureCreateView = signatureCreateViews[(int)e.NewIndex];
            signatureCreateViews?.ForEach(view => view.Reset());
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonVisible = false;
                viewModel.IsOkButtonVisible = false;
            }
            if(upload != null && colorGrid != null)
            {
                if (upload.IsSelected)
                {
                    colorGrid.IsVisible = false;
                }
                else
                {
                    colorGrid.IsVisible = true;
                }
            }          
        }

        internal void AddHeader()
        {
            Grid header = new Grid() { Padding = 10, ColumnSpacing = 10 };
            header.AddColumnDefinition(new ColumnDefinition() { Width = 50 });
            header.AddColumnDefinition(new ColumnDefinition());
            header.AddColumnDefinition(new ColumnDefinition() { Width = 50 });
            Label title = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Text = "Add Signature",
                FontFamily = "Roboto",
                BackgroundColor = Colors.Transparent,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            };

            okButton.Text = "\uE75B";
            okButton.FontFamily = "MauiMaterialAssets";
            okButton.Background = Colors.Transparent;
            okButton.FontSize = 24;
            okButton.SetBinding(Button.IsVisibleProperty, new Binding(nameof(SignatureViewModel.IsOkButtonVisible)));

            Button backButton = new MaterialIconButton
            {
                Text = "\uE72D",
                FontFamily = "MauiMaterialAssets",
                BackgroundColor = Colors.Transparent,
                FontSize = 24
            };

            okButton.Clicked += OkButton_Clicked;
            backButton.Clicked += BackButton_Clicked;
            header.Add(backButton, 0, 0);
            header.Add(title, 1, 0);
            header.Add(okButton, 2, 0);
            this.Add(header, 0, 0);

            if (Application.Current != null && Application.Current.Resources != null)
            {
                header.SetAppThemeColor(Grid.BackgroundColorProperty,
                    (Color)Application.Current.Resources["SampleBrowserBackgroundLight"],
                    (Color)Application.Current.Resources["SampleBrowserBackground"]);
                backButton.SetAppThemeColor(Button.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
                title.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
                okButton.SetAppThemeColor(Button.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
            }
        }

        private void OkButton_Clicked(object? sender, System.EventArgs e)
        {
            SignatureHelper.CurrentSignatureItem = selectedSignatureCreateView?.GetSignatureItem();
            SignatureCreated?.Invoke(this, EventArgs.Empty);
            Hide();
        }

        private void BackButton_Clicked(object? sender, System.EventArgs e)
        {
            Hide();
        }

        void Hide()
        {
            signatureCreateViews?.ForEach(view => view.Reset());
            CloseRequested?.Invoke(this, EventArgs.Empty);
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonVisible = false;
                viewModel.IsOkButtonVisible = false;
            }
        }

        void AddColorBar()
        {
            colorGrid = new Grid();
            if (Application.Current != null && Application.Current.Resources != null)
            {
                colorGrid.SetAppThemeColor(Grid.BackgroundColorProperty,
                    (Color)Application.Current.Resources["SampleBrowserBackgroundLight"],
                    (Color)Application.Current.Resources["SampleBrowserBackground"]);
            }
            ColorBar colorBar = new ColorBar();
            colorBar.VerticalOptions = LayoutOptions.Center;
            colorBar.HorizontalOptions = LayoutOptions.Center;
            colorGrid.Children.Add(colorBar);
            this.Add(colorGrid, 0, 3);
        }

        void AddSaveLayout()
        {
            Grid saveLayout = new Grid();
            saveLayout.HeightRequest = 44;
            saveLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = 32 });
            saveLayout.ColumnDefinitions.Add(new ColumnDefinition() {});
            saveLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = 100 });
            CheckBox saveBox = new CheckBox()
            {
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 18,
                WidthRequest = 18,
                Margin = new Thickness(18, 0, 0, 0)
            };
            saveBox.CheckedChanged += (s, e) => { SignatureHelper.ShouldSaveSignature = e.Value; };
            Label saveLabel = new Label()
            {
                Text = "Save Signature",
                FontFamily = "Roboto",
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(20, 0, 0, 0)
            };

            TapGestureRecognizer tapRecognizer = new TapGestureRecognizer();
            saveLabel.GestureRecognizers.Add(tapRecognizer);
            tapRecognizer.Tapped += (s, e) => { saveBox.IsChecked = !saveBox.IsChecked; };
            Button clearButton = new Button()
            {
                Text = "Clear",
                FontFamily = "Roboto",
                TextColor = IntegratedViewClearTextColor,
                Background = Colors.Transparent,
                Margin = new Thickness(30, 0, 0, 0),
            };
            clearButton.Clicked += ClearButton_Clicked;
            saveLayout.Add(saveBox, 0, 0);
            saveLayout.Add(saveLabel, 1, 0);
            saveLayout.Add(clearButton, 2, 0);
            saveLayout.SetBinding(Grid.IsVisibleProperty, new Binding(nameof(SignatureViewModel.IsClearButtonVisible)));
            this.Add(saveLayout, 0, 2);

            if (Application.Current != null && Application.Current.Resources != null)
            {
                saveLayout.SetAppThemeColor(Grid.BackgroundColorProperty,
                    (Color)Application.Current.Resources["BackgroundLight"],
                    (Color)Application.Current.Resources["BackgroundDark"]);
                saveBox.SetAppThemeColor(CheckBox.ColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
                saveLabel.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
                clearButton.SetAppThemeColor(Button.TextColorProperty,
                    (Color)Application.Current.Resources["FlatButtonForegroundLight"],
                    (Color)Application.Current.Resources["FlatButtonForeground"]);
            }
        }

        private void ClearButton_Clicked(object? sender, System.EventArgs e)
        {
            selectedSignatureCreateView?.Reset();
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonVisible = false;
                viewModel.IsOkButtonVisible = false;
            }
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(IntegratedSignatureViewBackgroundColorProperty, "SfPdfViewerSignaturePadBackgroundColor");
            this.SetDynamicResource(IntegratedViewToolbarBackgroundColorProperty, "SfPdfViewerSignatureViewHeaderBackgroundColor");
            this.SetDynamicResource(BackButtonColorProperty, "SfPdfViewerSignatureViewBackButtonTextColor");
            this.SetDynamicResource(OkButtonColorProperty, "SfPdfViewerSignatureViewOkButtonTextColor");
            this.SetDynamicResource(IntegratedViewHeaderTextColorProperty, "SfPdfViewerSignatureViewHeaderTextColor");
            this.SetDynamicResource(IntegratedSignatureSaveTextColorProperty, "SfPdfViewerSignatureViewSaveTextColor");
            this.SetDynamicResource(IntegratedViewSaveCheckboxColorProperty, "SfPdfViewerSignatureViewSaveCheckboxColor");
            this.SetDynamicResource(IntegratedViewClearTextColorProperty, "SfPdfViewerSignatureViewClearTextColor");
            this.SetDynamicResource(IntegratedViewSaveLayoutBackgroundColorProperty, "SfPdfViewerSignatureViewSaveLayoutBackgroundColor");
            this.SetDynamicResource(IntegratedViewColorBarBackgroundProperty, "SfPdfViewerSignatureViewColorBarBackgroundColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
