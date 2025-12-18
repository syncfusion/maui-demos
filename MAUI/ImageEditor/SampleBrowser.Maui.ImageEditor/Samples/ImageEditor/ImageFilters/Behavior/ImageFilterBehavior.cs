#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ImageEditor.SfImageEditor
{
    using Microsoft.Maui.Controls.Shapes;
    using System;
    using System.Collections.Generic;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Core;
    using Syncfusion.Maui.ImageEditor;
    using Syncfusion.Maui.ListView;

    /// <summary>
    /// Represents the behavior for applying image filter interactions within the sample view.
    /// </summary>
    public class ImageFilterBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// Holds the image editor instance.
        /// </summary>
        private SfImageEditor? imageEditor;

        /// <summary>
        /// Holds the list view instance.
        /// </summary>
        private SfListView? filtersList;

        /// <summary>
        /// Holds the effects view instance.
        /// </summary>
        private SfEffectsView? effectsView;

        /// <summary>
        /// Holds the border for custom toolbar item view.
        /// </summary>
        private Border? border;

        #endregion

        #region Override methods

        /// <summary>
        /// Invoked when behavior is attached to a view.
        /// </summary>
        /// <param name="sampleView">The sample view.</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.imageEditor = sampleView.Content.FindByName<SfImageEditor>("imageEditor");
            this.AddListViewItems();
        }

        /// <summary>
        /// Invoked when behavior is detached from a view.
        /// </summary>
        /// <param name="sampleView">The sample view.</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            if (this.filtersList != null)
            {
                this.filtersList.Loaded -= this.OnFilterListLoaded;
                this.filtersList.SelectionChanged -= this.OnFiltersListSelectionChanged;
            }

            if (this.effectsView != null)
            {
                this.effectsView.AnimationCompleted -= this.OnEffectsViewAnimationCompleted;
                this.effectsView = null;
            }

            this.filtersList = null;
            this.border = null;
            this.imageEditor = null;
            base.OnDetachingFrom(sampleView);
        }

        #endregion

        #region Property changed

        /// <summary>
        /// Invokes on filter image editor instance loaded.
        /// </summary>
        /// <param name="sender">The image editor.</param>
        /// <param name="e">The event arguments.</param>
        private void OnImageEditorImageLoaded(object? sender, EventArgs e)
        {
            var imageEditor = sender as SfImageEditor;
            var filterModel = imageEditor?.BindingContext as ImageFilterModel;
            if (imageEditor == null || filterModel == null)
            {
                return;
            }

            if (filterModel.Effect == "Brightness")
            {
                imageEditor.ImageEffect(ImageEffect.Brightness, -0.2);
            }
            else if (filterModel.Effect == "Hue")
            {
                imageEditor.ImageEffect(ImageEffect.Hue, 1);
            }
            else if (filterModel.Effect == "Grayscale")
            {
                imageEditor.ImageEffect(ImageEffect.Saturation, -1);
            }
            else if (filterModel.Effect == "Blur")
            {
                imageEditor.ImageEffect(ImageEffect.Blur, 0.5);
            }
            else if (filterModel.Effect == "Contrast")
            {
                imageEditor.ImageEffect(ImageEffect.Contrast, 1);
            }
            else if (filterModel.Effect == "Exposure")
            {
                imageEditor.ImageEffect(ImageEffect.Exposure, 0.3);
            }
            else if (filterModel.Effect == "Sharpen")
            {
                imageEditor.ImageEffect(ImageEffect.Sharpen, 1);
            }
            else if (filterModel.Effect == "Opacity")
            {
                imageEditor.ImageEffect(ImageEffect.Opacity, 0.5);
            }

            filterModel.IsBusy = false;
        }

        /// <summary>
        /// Called when the filters list is loaded and set preview image sources.
        /// </summary>
        /// <param name="sender">The list view.</param>
        /// <param name="e">The event arguments.</param>
        private void OnFilterListLoaded(object? sender, EventArgs e)
        {
            if (this.imageEditor != null && this.filtersList?.ItemsSource is IEnumerable<ImageFilterModel> previewImages)
            {
                foreach (var previewImage in previewImages)
                {
                    previewImage.ImageSource = this.imageEditor.Source;
                }
            }
        }

        /// <summary>
        /// Apply selected effect to the main image editor.
        /// </summary>
        /// <param name="sender">The list view.</param>
        /// <param name="e">The item selection changed event arguments.</param>
        private void OnFiltersListSelectionChanged(object? sender, ItemSelectionChangedEventArgs e)
        {
            if (this.imageEditor == null || e.AddedItems == null || e.AddedItems.Count == 0)
            {
                return;
            }

            if (e.AddedItems[0] is ImageFilterModel model && !string.IsNullOrWhiteSpace(model.Effect))
            {
                this.ApplyEffectToMainEditor(model.Effect);
            }
        }

        /// <summary>
        /// Called when the effects view animation gets completed.
        /// </summary>
        /// <param name="sender">The effects view.</param>
        /// <param name="e">The event arguments</param>
        private void OnEffectsViewAnimationCompleted(object? sender, System.EventArgs e)
        {
            this.effectsView?.Reset();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to initialize and add listview items as one of the image editor toolbar item.
        /// </summary>
        private void AddListViewItems()
        {
            if (this.imageEditor == null)
            {
                return;
            }

            Label label = new Label
            {
                Text = "\uE747",
                FontFamily = "MaterialAssets",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            if (Application.Current?.UserAppTheme == AppTheme.Dark)
            {
                label.TextColor = Color.FromArgb("#CAC4D0");
            }
            else
            {
                label.TextColor = Color.FromArgb("#49454F");
            }

            this.effectsView = new SfEffectsView();
            this.effectsView.ClipToBounds = true;
            this.effectsView.AnimationCompleted += this.OnEffectsViewAnimationCompleted;
            this.effectsView.Content = label;

            this.border = new Border
            {
                HeightRequest = 40,
                WidthRequest = 40,
                Stroke = Colors.Transparent,
                StrokeShape = new RoundRectangle { CornerRadius = 20 },
                Content = this.effectsView
            };

            VisualStateGroupList groups = new VisualStateGroupList();
            VisualStateGroup commonStates = new VisualStateGroup { Name = "CommonStates" };

            VisualState normal = new VisualState { Name = "Normal" };
            normal.Setters.Add(new Setter
            {
                Property = Border.BackgroundColorProperty,
                Value = Colors.Transparent
            });

            VisualState pointerOver = new VisualState { Name = "PointerOver" };
            pointerOver.Setters.Add(new Setter
            {
                Property = Border.BackgroundColorProperty,
                Value = Application.Current?.RequestedTheme == AppTheme.Dark
                    ? Color.FromArgb("#14cac4d0")
                    : Color.FromArgb("#1449454f")
            });

            commonStates.States.Add(normal);
            commonStates.States.Add(pointerOver);
            groups.Add(commonStates);
            VisualStateManager.SetVisualStateGroups(border, groups);

            this.border.SetAppThemeColor(Border.BackgroundColorProperty, Colors.Transparent, Colors.Transparent);

            ImageEditorToolbarItem effectsItem = new ImageEditorToolbarItem
            {
                Name = "Filters",
                SubToolbarOverlay = true,
                View = this.border
            };

            var effectsSubToolbar = new ImageEditorToolbar
            {
                Size = 80
            };

            var filtersItem = new ImageEditorToolbarItem
            {
                Name = "Filters",
                ShowTooltip = false
            };

            this.filtersList = new SfListView
            {
                SelectionMode = SelectionMode.Single,
                Margin = new Thickness(0),
                Orientation = ItemsLayoutOrientation.Horizontal,
                ItemSize = 80,
                HeightRequest = 80,
                BindingContext = new ImageFilterViewModel()
            };

            this.filtersList.SelectionBackground = Colors.Transparent;
            this.filtersList.Loaded += this.OnFilterListLoaded;
            this.filtersList.SelectionChanged += this.OnFiltersListSelectionChanged;
            this.filtersList.SetBinding(SfListView.ItemsSourceProperty, "ImageFilters");
            this.filtersList.ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    Margin = new Thickness(5, 0),
                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(0.75, GridUnitType.Star) },
                        new RowDefinition { Height = new GridLength(0.25, GridUnitType.Star) }
                    }
                };

                var activity = new ActivityIndicator
                {
                    Color = Colors.LightGray,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 20,
                    WidthRequest = 20
                };

                activity.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
                Grid.SetRowSpan(activity, 2);

                SfImageEditor previewEditor = new SfImageEditor
                {
                    AllowZoom = false,
                    ShowToolbar = false,
                    BackgroundColor = Colors.Transparent,
                    WidthRequest = 60,
                    MinimumWidthRequest = 60,
                    HeightRequest = 60,
                    MinimumHeightRequest = 60,
                    IsEnabled = false
                };

                previewEditor.SetBinding(SfImageEditor.SourceProperty, "ImageSource");
                previewEditor.ImageLoaded += OnImageEditorImageLoaded;

                Border border = new Border()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 64,
                    HeightRequest = 64,
                    Background = Colors.Transparent,
                    Content = previewEditor
                };

                var caption = new Label
                {
                    FontSize = 14,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                caption.SetBinding(Label.TextProperty, "Effect");
                Grid.SetRow(caption, 1);

                grid.Add(activity);
                grid.Add(border);
                grid.Add(caption);

                return grid;
            });

            filtersItem.View = this.filtersList;

            effectsSubToolbar.ToolbarItems.Add(new ImageEditorToolbarItem() { Name = "Back" });
            effectsSubToolbar.ToolbarItems.Add(new ImageEditorToolbarItem() { Name = "Separator", IsEnabled = false });
            effectsSubToolbar.ToolbarItems.Add(filtersItem);
            effectsItem.SubToolbars.Add(effectsSubToolbar);

            ImageEditorToolbar footer = this.imageEditor.Toolbars[1];
            footer.ToolbarItems.Insert(4, effectsItem);
        }

        /// <summary>
        /// Method to apply effect to the main image editor.
        /// </summary>
        /// <param name="effectName">The effect name.</param>
        private void ApplyEffectToMainEditor(string effectName)
        {
            if (this.imageEditor == null)
            {
                return;
            }

            this.imageEditor.CancelEdits();
            switch (effectName)
            {
                case "Brightness":
                    this.imageEditor.ImageEffect(ImageEffect.Brightness, -0.2);
                    break;
                case "Hue":
                    this.imageEditor.ImageEffect(ImageEffect.Hue, 1);
                    break;
                case "Grayscale":
                    this.imageEditor.ImageEffect(ImageEffect.Saturation, -1);
                    break;
                case "Blur":
                    this.imageEditor.ImageEffect(ImageEffect.Blur, 0.5);
                    break;
                case "Contrast":
                    this.imageEditor.ImageEffect(ImageEffect.Contrast, 1);
                    break;
                case "Exposure":
                    this.imageEditor.ImageEffect(ImageEffect.Exposure, 0.3);
                    break;
                case "Sharpen":
                    this.imageEditor.ImageEffect(ImageEffect.Sharpen, 1);
                    break;
                case "Opacity":
                    this.imageEditor.ImageEffect(ImageEffect.Opacity, 0.5);
                    break;
            }
        }

        #endregion
    }
}