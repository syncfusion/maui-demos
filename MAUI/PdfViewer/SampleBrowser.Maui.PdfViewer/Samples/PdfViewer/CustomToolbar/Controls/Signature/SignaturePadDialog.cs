#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.SignaturePad;
using Syncfusion.Maui.TabView;
using Syncfusion.Maui.Themes;
using System;
using System.Collections.Generic;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class SignaturePadDialog : Grid, IIntegratedSigatureDialog
    {
        double dialogWidth = 566;
        double dialogHeight = 544;
        Grid contentGrid;
        public event EventHandler? CloseRequested;
        public event EventHandler? SignatureCreated;
        List<ISignatureCreateView> signatureCreateViews;
        ISignatureCreateView? selectedSignatureCreateView;
        SfTabView tabView;
        Border mainContentBorder;
        ScrollView scrollView;
        SfTabItem draw;
        SfTabItem type;
        SfTabItem upload;       
        HorizontalStackLayout colorStack;
        private MaterialTextButton createButton;
        private static readonly BindableProperty SignaturePadBackgroundColorProperty = BindableProperty.Create("SignaturePadBackgroundColor", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#FFFBFE"));
        private static readonly BindableProperty SignaturePadLabelTextColorProperty = BindableProperty.Create("SignaturePadLabelTextColor", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#1C1B1F"));
        private static readonly BindableProperty SignaturePadCloseTextColorProperty = BindableProperty.Create("SignaturePadCloseTextColor", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty SignaturePadCreateTextColorProperty = BindableProperty.Create("SignaturePadCloseTextColor", typeof(Color), typeof(SignaturePadDialog), defaultValue: Colors.White);
        private static readonly BindableProperty DrawSignatureBorderColorProperty = BindableProperty.Create("DrawSignatureBorderColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#CAC4D0"));
        private static readonly BindableProperty SignatureClearButtonColorProperty = BindableProperty.Create("SignatureClearButtonColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureClearButtonHoverTextColorProperty = BindableProperty.Create("SignatureClearButtonHoverTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureClearButtonPressedTextColorProperty = BindableProperty.Create("SignatureClearButtonPressedTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureClearButtonHoverBackgroundColorProperty = BindableProperty.Create("SignatureClearButtonHoverBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromRgba(73, 69, 79, 20));
        private static readonly BindableProperty SignatureClearButtonPressedBackgroundColorProperty = BindableProperty.Create("SignatureClearButtonPressedBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromRgba(73, 69, 79, 20));
        private static readonly BindableProperty SignatureClearButtonDisabledTextColorProperty = BindableProperty.Create("SignatureClearButtonDisabledTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCreateButtonHoverTextColorProperty = BindableProperty.Create("SignatureCreateButtonHoverTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Colors.White);
        private static readonly BindableProperty SignatureCreateButtonPressedTextColorProperty = BindableProperty.Create("SignatureCreateButtonPressedTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Colors.White);
        private static readonly BindableProperty SignatureCreateButtonHoverBackgroundColorProperty = BindableProperty.Create("SignatureCreateButtonHoverBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCreateButtonPressedBackgroundColorProperty = BindableProperty.Create("SignatureCreateButtonPressedBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCreateButtonDisabledTextColorProperty = BindableProperty.Create("SignatureCreateButtonDisabledTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Colors.White);
        private static readonly BindableProperty SignatureCloseButtonHoverTextColorProperty = BindableProperty.Create("SignatureCloseButtonHoverTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty SignatureCloseButtonPressedTextColorProperty = BindableProperty.Create("SignatureCloseButtonPressedTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#49454F"));
        private static readonly BindableProperty SignatureCloseButtonHoverBackgroundColorProperty = BindableProperty.Create("SignatureCloseButtonHoverBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromRgba(73, 69, 79, 20));
        private static readonly BindableProperty SignatureCloseButtonPressedBackgroundColorProperty = BindableProperty.Create("SignatureCloseButtonPressedBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromRgba(73, 69, 79, 20));
        private static readonly BindableProperty SignatureCloseButtonDisabledTextColorProperty = BindableProperty.Create("SignatureCloseButtonDisabledTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCancelButtonDisabledTextColorProperty = BindableProperty.Create("SignatureCancelButtonDisabledTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCancelButtonHoverTextColorProperty = BindableProperty.Create("SignatureCancelButtonHoverTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCancelButtonPressedTextColorProperty = BindableProperty.Create("SignatureCancelButtonPressedTextColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromArgb("#6750A4"));
        private static readonly BindableProperty SignatureCancelHoverBackgroundColorProperty = BindableProperty.Create("SignatureCancelHoverBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromRgba(73, 69, 79, 20));
        private static readonly BindableProperty SignatureCancelPressedBackgroundColorProperty = BindableProperty.Create("SignatureCancelPressedBackgroundColorProperty", typeof(Color), typeof(SignaturePadDialog), defaultValue: Color.FromRgba(73, 69, 79, 20));

        internal Color SignaturePadBackgroundColor
        {
            get => (Color)GetValue(SignaturePadBackgroundColorProperty);
            set => SetValue(SignaturePadBackgroundColorProperty, value);
        }

        internal Color SignaturePadLabelTextColor
        {
            get => (Color)GetValue(SignaturePadLabelTextColorProperty);
            set => SetValue(SignaturePadLabelTextColorProperty, value);
        }

        internal Color SignaturePadCloseTextColor
        {
            get => (Color)GetValue(SignaturePadCloseTextColorProperty);
            set => SetValue(SignaturePadCloseTextColorProperty, value);
        }

        internal Color SignaturePadCreateTextColor
        {
            get => (Color)GetValue(SignaturePadCreateTextColorProperty);
            set => SetValue(SignaturePadCreateTextColorProperty, value);
        }

        internal Color DrawSignatureBorderColor
        {
            get => (Color)GetValue(DrawSignatureBorderColorProperty);
            set => SetValue(DrawSignatureBorderColorProperty, value);
        }

        internal Color SignatureTextButtonTextColor
        {
            get => (Color)GetValue(SignatureClearButtonColorProperty);
            set => SetValue(SignatureClearButtonColorProperty, value);
        }

        internal Color SignatureClearButtonHoverTextColor
        {
            get => (Color)GetValue(SignatureClearButtonHoverTextColorProperty);
            set => SetValue(SignatureClearButtonHoverTextColorProperty, value);
        }

        internal Color SignatureClearButtonPressedTextColor
        {
            get => (Color)GetValue(SignatureClearButtonPressedTextColorProperty);
            set => SetValue(SignatureClearButtonPressedTextColorProperty, value);
        }

        internal Color SignatureClearButtonHoverBackgroundColor
        {
            get => (Color)GetValue(SignatureClearButtonHoverBackgroundColorProperty);
            set => SetValue(SignatureClearButtonHoverBackgroundColorProperty, value);
        }

        internal Color SignatureClearButtonPressedBackgroundColor
        {
            get => (Color)GetValue(SignatureClearButtonPressedBackgroundColorProperty);
            set => SetValue(SignatureClearButtonPressedBackgroundColorProperty, value);
        }

        internal Color SignatureClearButtonDisabledTextColor
        {
            get => (Color)GetValue(SignatureClearButtonDisabledTextColorProperty);
            set => SetValue(SignatureClearButtonDisabledTextColorProperty, value);
        }
        internal Color SignatureCreateButtonHoverTextColor
        {
            get => (Color)GetValue(SignatureCreateButtonHoverTextColorProperty);
            set => SetValue(SignatureCreateButtonHoverTextColorProperty, value);
        }
        internal Color SignatureCreateButtonPressedTextColor
        {
            get => (Color)GetValue(SignatureCreateButtonPressedTextColorProperty);
            set => SetValue(SignatureCreateButtonPressedTextColorProperty, value);
        }
        internal Color SignatureCreateButtonHoverBackgroundColor
        {
            get => (Color)GetValue(SignatureCreateButtonHoverBackgroundColorProperty);
            set => SetValue(SignatureCreateButtonHoverBackgroundColorProperty, value);
        }
        internal Color SignatureCreateButtonPressedBackgroundColor
        {
            get => (Color)GetValue(SignatureCreateButtonPressedBackgroundColorProperty);
            set => SetValue(SignatureCreateButtonPressedBackgroundColorProperty, value);
        }
        internal Color SignatureCreateButtonDisabledTextColor
        {
            get => (Color)GetValue(SignatureCreateButtonDisabledTextColorProperty);
            set => SetValue(SignatureCreateButtonDisabledTextColorProperty, value);
        }
        internal Color SignatureCloseButtonHoverTextColor
        {
            get => (Color)GetValue(SignatureCloseButtonHoverTextColorProperty);
            set => SetValue(SignatureCloseButtonHoverTextColorProperty, value);
        }
        internal Color SignatureCloseButtonPressedTextColor
        {
            get => (Color)GetValue(SignatureCloseButtonPressedTextColorProperty);
            set => SetValue(SignatureCloseButtonPressedTextColorProperty, value);
        }
        internal Color SignatureCloseButtonHoverBackgroundColor
        {
            get => (Color)GetValue(SignatureCloseButtonHoverBackgroundColorProperty);
            set => SetValue(SignatureCloseButtonHoverBackgroundColorProperty, value);
        }
        internal Color SignatureCloseButtonPressedBackgroundColor
        {
            get => (Color)GetValue(SignatureCloseButtonPressedBackgroundColorProperty);
            set => SetValue(SignatureCloseButtonPressedBackgroundColorProperty, value);
        }

        internal Color SignatureCloseButtonDisabledTextColor
        {
            get => (Color)GetValue(SignatureCloseButtonDisabledTextColorProperty);
            set => SetValue(SignatureCloseButtonDisabledTextColorProperty, value);
        }

        internal Color SignatureCancelButtonDisabledTextColor
        {
            get => (Color)GetValue(SignatureCancelButtonDisabledTextColorProperty);
            set => SetValue(SignatureCancelButtonDisabledTextColorProperty, value);
        }

        internal Color SignatureCancelButtonHoverTextColor
        {
            get => (Color)GetValue(SignatureCancelButtonHoverTextColorProperty);
            set => SetValue(SignatureCancelButtonHoverTextColorProperty, value);
        }

        internal Color SignatureCancelButtonPressedTextColor
        {
            get => (Color)GetValue(SignatureCancelButtonPressedTextColorProperty);
            set => SetValue(SignatureCancelButtonPressedTextColorProperty, value);
        }

        internal Color SignatureCancelHoverBackgroundColor
        {
            get => (Color)GetValue(SignatureCancelHoverBackgroundColorProperty);
            set => SetValue(SignatureCancelHoverBackgroundColorProperty, value);
        }

        internal Color SignatureCancelPressedBackgroundColor
        {
            get => (Color)GetValue(SignatureCancelPressedBackgroundColorProperty);
            set => SetValue(SignatureCancelPressedBackgroundColorProperty, value);
        }

        internal SignaturePadDialog()
        {
            ApplyDynamicStyles();
            signatureCreateViews = new List<ISignatureCreateView>();
            Grid bgGrid = new Grid
            {
                BackgroundColor = Color.FromRgba(72, 70, 73, 80),
            };
            Children.Add(bgGrid);

            scrollView = new ScrollView();
            scrollView.HandlerChanged += ScrollView_HandlerChanged;
            mainContentBorder = new Border() { StrokeThickness = 0, StrokeShape = new RoundRectangle() { CornerRadius = 28 } };
            mainContentBorder.WidthRequest = dialogWidth;
            mainContentBorder.HeightRequest = dialogHeight;

            if (Application.Current != null && Application.Current.Resources != null)
            {
                mainContentBorder.SetAppThemeColor(Border.BackgroundColorProperty,
                    (Color)Application.Current.Resources["SampleBrowserBackgroundLight"],
                    (Color)Application.Current.Resources["BackgroundDark"]);
            }

            mainContentBorder.VerticalOptions = LayoutOptions.Center;
            mainContentBorder.HorizontalOptions = LayoutOptions.Center;
            scrollView.Content = mainContentBorder;
            Children.Add(scrollView);

            contentGrid = new Grid()
            {
                WidthRequest = dialogWidth,
                HeightRequest = dialogHeight,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            contentGrid.AddRowDefinition(new RowDefinition() { Height = 70 });
            contentGrid.AddRowDefinition(new RowDefinition() { Height = 330 });
            contentGrid.AddRowDefinition(new RowDefinition() { Height = 144 });

            mainContentBorder.Content = contentGrid;

            Grid header = new Grid() { Padding = 24 };
            Label title = new Label
            {
                FontSize = 24,
                TextColor = SignaturePadLabelTextColor,
                Text = "Add Signature",
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 32,
            };

            if (Application.Current != null && Application.Current.Resources != null)
            {
                title.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
            }

            Button closeButton = new MaterialIconButton
            {
                Text = "\uE70B",
                HorizontalOptions = LayoutOptions.End,
                FontSize = 24,
                NormalTextColor = SignaturePadCloseTextColor,
                VerticalOptions = LayoutOptions.Center,
                HoverTextColor = SignatureCloseButtonHoverTextColor,
                PressedTextColor = SignatureCloseButtonPressedTextColor,
                HoverBackgroundColor = SignatureCloseButtonHoverBackgroundColor,
                PressedBackgroundColor = SignatureCloseButtonPressedBackgroundColor,
                DisabledTextColor = SignatureCloseButtonDisabledTextColor,
            };

            closeButton.CornerRadius = 20;
            closeButton.Clicked += CloseButton_Clicked;
            header.Children.Add(closeButton);
            header.Children.Add(title);
            contentGrid.Add(header, 0, 0);

            Grid bottomGrid = new Grid() { Padding = 24 };
            bottomGrid.AddRowDefinition(new RowDefinition() { Height = 32 });
            bottomGrid.AddRowDefinition(new RowDefinition() { Height = 40 });
            bottomGrid.RowSpacing = 24;

            Grid colorGrid = new Grid();

            HorizontalStackLayout saveStack = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.Start, Spacing = 10 };
            SfCheckBox checkBox = new SfCheckBox();
            checkBox.StateChanged += (s, e) =>
            {
                if (e?.IsChecked != null)
                    SignatureHelper.ShouldSaveSignature = (bool)e.IsChecked;
            };
            Label saveLabel = new Label() { Text = "Save Signature", VerticalOptions = LayoutOptions.Center, FontSize = 14 };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                saveLabel.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
            }
            saveStack.Add(checkBox);
            saveStack.Add(saveLabel);
            colorGrid.Add(saveStack);

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => checkBox.IsChecked = !checkBox.IsChecked;
            saveLabel.GestureRecognizers.Add(tapGestureRecognizer);

            colorStack = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.End, Spacing = 10 };
            Label colorLabel = new Label() { Text = "Color", VerticalOptions = LayoutOptions.Center, TextColor = SignaturePadLabelTextColor, FontSize = 14 };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                colorLabel.SetAppThemeColor(Label.TextColorProperty,
                    (Color)Application.Current.Resources["IconColourLight"],
                    (Color)Application.Current.Resources["IconColour"]);
            }
            ColorBar colorBar = new ColorBar();
            colorStack.Add(colorLabel);
            colorStack.Add(colorBar);
            colorGrid.Add(colorStack);

            bottomGrid.Add(colorGrid, 0, 0);

            Grid buttonGrid = new Grid();
            bottomGrid.Add(buttonGrid, 0, 1);

            Button clearButton = new MaterialTextButton()
            {
                Text = "Clear",
                FontSize = 14,
                TextColor = SignatureTextButtonTextColor,
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 74,
                HeightRequest = 40,
                Padding = 0,
                HoverTextColor = SignatureClearButtonHoverTextColor,
                PressedTextColor = SignatureClearButtonPressedTextColor,
                HoverBackgroundColor = SignatureClearButtonHoverBackgroundColor,
                PressedBackgroundColor = SignatureClearButtonPressedBackgroundColor,
                DisabledTextColor = SignatureClearButtonDisabledTextColor
            };
            clearButton.Clicked += ClearButton_Clicked;
            clearButton.SetBinding(Button.IsEnabledProperty, new Binding(nameof(SignatureViewModel.IsClearButtonEnabled)));
            buttonGrid.Add(clearButton);

            HorizontalStackLayout confirmationButtonStack = new HorizontalStackLayout() { HorizontalOptions = LayoutOptions.End, Spacing = 10 };

            Button cancelButton = new MaterialTextButton()
            {
                Text = "Cancel",
                FontSize = 14,
                TextColor = SignatureTextButtonTextColor,
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 74,
                HeightRequest = 40,
                Padding = 0,
                HoverTextColor = SignatureTextButtonTextColor,
                PressedTextColor = SignatureTextButtonTextColor,
                HoverBackgroundColor = SignatureCancelHoverBackgroundColor,
                PressedBackgroundColor = SignatureCancelPressedBackgroundColor,
                DisabledTextColor = SignatureCancelButtonDisabledTextColor
            };
            cancelButton.Clicked += CloseButton_Clicked;
            confirmationButtonStack.Add(cancelButton);

            createButton = new MaterialTextButton()
            {
                Text = "Create",
                FontSize = 14,
                BackgroundColor = SignatureTextButtonTextColor,
                HoverBackgroundColor = SignatureCreateButtonHoverBackgroundColor,
                PressedBackgroundColor = SignatureCreateButtonPressedBackgroundColor,
                DisabledTextColor = SignatureCreateButtonDisabledTextColor,
                TextColor = SignaturePadCreateTextColor,
                HoverTextColor = SignatureCreateButtonHoverTextColor,
                PressedTextColor = SignatureCreateButtonPressedTextColor,
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 74,
                HeightRequest = 40,
                Padding = 0
            };
            createButton.Clicked += CreateButton_Clicked;
            createButton.SetBinding(Button.IsEnabledProperty, new Binding(nameof(SignatureViewModel.IsCreateButtonEnabled)));
            confirmationButtonStack.Add(createButton);

            buttonGrid.Add(confirmationButtonStack);

            contentGrid.Add(bottomGrid, 0, 2);

            Grid tabViewGrid = new Grid();
            tabView = new SfTabView();
            tabView.TabBarHeight = 48;
            tabView.TabWidthMode = TabWidthMode.SizeToContent;
            tabView.SelectionChanged += TabView_SelectionChanged;
            FreeHandSignatureCreateView freeHandTab = new FreeHandSignatureCreateView();
            selectedSignatureCreateView = freeHandTab;
            TypeSignatureCreateView typeTab = new TypeSignatureCreateView();
            ImageSignatureCreateView imageTab = new ImageSignatureCreateView();

            Border drawSignatureborder = new Border()
            {
                Margin = Margin = new Thickness(24, 24, 24, 0),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(8)
                },
                Content = freeHandTab,
                BackgroundColor = Colors.White
            };
            if (Application.Current != null && Application.Current.Resources != null)
            {
                drawSignatureborder.SetAppThemeColor(Border.StrokeProperty,
                    (Color)Application.Current.Resources["BorderLight"],
                    (Color)Application.Current.Resources["Border"]);
            }

            draw = new SfTabItem { Content = drawSignatureborder, Header = "Draw" };
            type = new SfTabItem { Content = typeTab, Header = "Type" };
            upload = new SfTabItem { Content = imageTab, Header = "Upload" };
            tabView.Items.Add(draw);
            tabView.Items.Add(type);
            tabView.Items.Add(upload);
            signatureCreateViews.Add(freeHandTab);
            signatureCreateViews.Add(typeTab);
            signatureCreateViews.Add(imageTab);
            tabViewGrid.Add(tabView);

            Grid thinBorder = new Grid() { BackgroundColor = DrawSignatureBorderColor, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Fill, HeightRequest = 1 };
            thinBorder.Margin = new Thickness(0, tabView.TabBarHeight, 0, 0);
            tabViewGrid.Add(thinBorder);

            contentGrid.Add(tabViewGrid, 0, 1);
            Margin = 0;
        }

        private void ApplyDynamicStyles()
        {
            
        }

        private void ScrollView_HandlerChanged(object? sender, EventArgs e)
        {
            if (sender is ScrollView scrollView && scrollView.Handler != null && scrollView.Handler.PlatformView != null)
            {
#if WINDOWS
                if (scrollView.Handler.PlatformView is Microsoft.UI.Xaml.Controls.ScrollViewer scrollViewer)
                {
                    scrollViewer.VerticalSnapPointsType = Microsoft.UI.Xaml.Controls.SnapPointsType.None;
                    scrollViewer.VerticalScrollBarVisibility = Microsoft.UI.Xaml.Controls.ScrollBarVisibility.Disabled;
                    scrollViewer.VerticalScrollMode = Microsoft.UI.Xaml.Controls.ScrollMode.Disabled;
                }
#endif
            }
        }

#if WINDOWS || MACCATALYST
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (height < dialogHeight)
            {
                //mainContentBorder.AnchorX = 0.5;
                mainContentBorder.AnchorY = 0;
                mainContentBorder.Scale = height / dialogHeight;
            }
            else
                mainContentBorder.Scale = 1;
        }
#endif

        private void ClearButton_Clicked(object? sender, EventArgs e)
        {
            selectedSignatureCreateView?.Reset();
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonEnabled = false;
                viewModel.IsCreateButtonEnabled = false;
            }
        }

        private void CloseButton_Clicked(object? sender, EventArgs e)
        {
            Hide();
        }

        private void CreateButton_Clicked(object? sender, EventArgs e)
        {
            SignatureHelper.CurrentSignatureItem = selectedSignatureCreateView?.GetSignatureItem();
            SignatureCreated?.Invoke(this, EventArgs.Empty);
            Hide();
        }

        void Hide()
        {
            signatureCreateViews?.ForEach(view => view.Reset());
            CloseRequested?.Invoke(this, EventArgs.Empty);
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonEnabled = false;
                viewModel.IsCreateButtonEnabled = false;
            }
        }

        private void TabView_SelectionChanged(object? sender, TabSelectionChangedEventArgs e)
        {
            selectedSignatureCreateView = signatureCreateViews[(int)e.NewIndex];
            signatureCreateViews?.ForEach(view => view.Reset());
            if (BindingContext is SignatureViewModel viewModel)
            {
                viewModel.IsClearButtonEnabled = false;
                viewModel.IsCreateButtonEnabled = false;
            }
            if(upload.IsSelected)
            {
                colorStack.IsVisible = false;
            }
            else
            {
                colorStack.IsVisible = true;
            }
        }

        public void OnControlThemeChanged(string oldTheme, string newTheme)
        {
            this.SetDynamicResource(SignaturePadBackgroundColorProperty, "SfPdfViewerSignaturePadBackgroundColor");
            this.SetDynamicResource(SignaturePadLabelTextColorProperty, "SfPdfViewerSignaturePadHeaderTextColor");
            this.SetDynamicResource(SignaturePadCloseTextColorProperty, "SfPdfViewerSignaturePadCloseButtonTextColor");
            this.SetDynamicResource(SignaturePadCreateTextColorProperty, "SfPdfViewerSignaturePadCreateButtonTextColor");
            this.SetDynamicResource(DrawSignatureBorderColorProperty, "SfPdfViewerSignaturePadBorderColor");
            this.SetDynamicResource(SignatureClearButtonColorProperty, "SfPdfViewerSignaturePadClearButtonTextColor");
            this.SetDynamicResource(SignatureClearButtonHoverTextColorProperty, "SfPdfViewerSignaturePadClearButtonHoverTextColor");
            this.SetDynamicResource(SignatureClearButtonPressedTextColorProperty, "SfPdfViewerSignaturePadClearButtonPressedTextColor");
            this.SetDynamicResource(SignatureClearButtonHoverBackgroundColorProperty, "SfPdfViewerSignaturePadClearButtonHoverBackgroundColor");
            this.SetDynamicResource(SignatureClearButtonPressedBackgroundColorProperty, "SfPdfViewerSignaturePadClearButtonPressedBackgroundColor");
            this.SetDynamicResource(SignatureClearButtonDisabledTextColorProperty, "SfPdfViewerSignaturePadClearButtonDisabledTextColor");
            this.SetDynamicResource(SignatureCreateButtonHoverTextColorProperty, "SfPdfViewerSignaturePadCreateButtonHoverTextColor");
            this.SetDynamicResource(SignatureCreateButtonPressedTextColorProperty, "SfPdfViewerSignaturePadCreateButtonPressedTextColor");
            this.SetDynamicResource(SignatureCreateButtonHoverBackgroundColorProperty, "SfPdfViewerSignaturePadCreateButtonHoverBackgroundColor");
            this.SetDynamicResource(SignatureCreateButtonPressedBackgroundColorProperty, "SfPdfViewerSignaturePadCreateButtonPressedBackgroundColor");
            this.SetDynamicResource(SignatureCreateButtonDisabledTextColorProperty, "SfPdfViewerSignaturePadCreateButtonDisabledTextColor");
            this.SetDynamicResource(SignatureCloseButtonHoverTextColorProperty, "SfPdfViewerSignaturePadCloseButtonHoverTextColor");
            this.SetDynamicResource(SignatureCloseButtonPressedTextColorProperty, "SfPdfViewerSignaturePadCloseButtonPressedTextColor");
            this.SetDynamicResource(SignatureCloseButtonHoverBackgroundColorProperty, "SfPdfViewerSignaturePadCloseButtonHoverBackgroundColor");
            this.SetDynamicResource(SignatureCloseButtonPressedBackgroundColorProperty, "SfPdfViewerSignaturePadCloseButtonPressedBackgroundColor");
            this.SetDynamicResource(SignatureCloseButtonDisabledTextColorProperty, "SfPdfViewerSignaturePadCloseButtonDisabledTextColor");
            this.SetDynamicResource(SignatureCancelButtonDisabledTextColorProperty, "SfPdfViewerSignaturePadCancelButtonDisabledTextColor");
            this.SetDynamicResource(SignatureCancelButtonHoverTextColorProperty, "SfPdfViewerSignaturePadCancelButtonHoverTextColor");
            this.SetDynamicResource(SignatureCancelButtonPressedTextColorProperty, "SfPdfViewerSignaturePadCancelButtonPressedTextColor");
            this.SetDynamicResource(SignatureCancelHoverBackgroundColorProperty, "SfPdfViewerSignaturePadCancelHoverBackgroundColor");
            this.SetDynamicResource(SignatureCancelPressedBackgroundColorProperty, "SfPdfViewerSignaturePadCancelPressedBackgroundColor");
        }

        public void OnCommonThemeChanged(string oldTheme, string newTheme)
        {

        }
    }
}
