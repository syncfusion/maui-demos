#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.TreeMap;

    /// <summary>
    /// Represents the behavior for managing color mapping in a sample view.
    /// </summary>
    internal class BrushSettingsBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The tree map instance.
        /// </summary>
        private SfTreeMap? treeMap;

        /// <summary>
        /// The brush setting picker.
        /// </summary>
        private SfComboBox? brushSettingsPicker;

        #endregion

        #region Override methods

        /// <summary>
        /// Invoked when behavior is attached to a view.
        /// </summary>
        /// <param name="sampleView">The sample view to which the behavior is attached.</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

            this.treeMap = sampleView.Content.FindByName<SfTreeMap>("treeMap");
            this.brushSettingsPicker = sampleView.FindByName<SfComboBox>("brushSettingsPicker");

            if (this.brushSettingsPicker != null)
            {
                this.brushSettingsPicker.SelectionChanged += this.OnBrushSettingsPickerSelectionChanged;
            }
        }

        /// <summary>
        /// Invoked when behavior is detached from a view.
        /// </summary>
        /// <param name="sampleView">The sample view from which the behavior is detached.</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.treeMap != null)
            {
                this.treeMap = null;
            }

            if (this.brushSettingsPicker != null)
            {
                this.brushSettingsPicker.SelectionChanged -= this.OnBrushSettingsPickerSelectionChanged;
                this.brushSettingsPicker = null;
            }
        }

        #endregion

        #region Property Changed

        /// <summary>
        /// Occurs when the tree map brush settings is changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void OnBrushSettingsPickerSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (this.treeMap == null || this.brushSettingsPicker == null || sender == null)
            {
                return;
            }

            switch (this.brushSettingsPicker.SelectedItem)
            {
                case "Uniform":
                    this.UpdateUniformBrushSettings();
                    break;
                case "Desaturation":
                    this.UpdateDesaturationBrushSettings();
                    break;
                case "Palette":
                    this.UpdatePaletteBrushSettings();
                    break;
                case "Range":
                    this.UpdateRangeBrushSettings();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to update the uniform brush settings.
        /// </summary>
        private void UpdateUniformBrushSettings()
        {
            if (this.treeMap == null)
            {
                return;
            }

            this.treeMap.LegendSettings.ShowLegend = false;
            this.treeMap.LeafItemBrushSettings = new TreeMapUniformBrushSettings() { Brush = new SolidColorBrush(Color.FromArgb("#D21243")) };
            this.treeMap.LeafItemSettings.TextStyle.TextColor = Color.FromArgb("#FFFFFF");
        }

        /// <summary>
        /// Method to update the desaturation brush settings.
        /// </summary>
        private void UpdateDesaturationBrushSettings()
        {
            if (this.treeMap == null)
            {
                return;
            }

            this.treeMap.LegendSettings.ShowLegend = false;
            this.treeMap.LeafItemBrushSettings = new TreeMapDesaturationBrushSettings() { Brush = new SolidColorBrush(Color.FromArgb("#02AEDC")), From = 1, To = 0.2 };
            this.treeMap.LeafItemSettings.TextStyle.TextColor = Color.FromArgb("#000000");
        }

        /// <summary>
        /// Method to update the palette brush settings.
        /// </summary>
        private void UpdatePaletteBrushSettings()
        {
            if (this.treeMap == null)
            {
                return;
            }

            this.treeMap.LegendSettings.ShowLegend = false;
            this.treeMap.LeafItemBrushSettings = this.GetPaletteColors();
            this.treeMap.LeafItemSettings.TextStyle.TextColor = Color.FromArgb("#000000");
        }

        /// <summary>
        /// Method to update the range brush settings.
        /// </summary>
        private void UpdateRangeBrushSettings()
        {
            if (this.treeMap == null)
            {
                return;

            }

            var leafItemBrushSettings = new TreeMapRangeBrushSettings()
            {
                RangeBrushes = new List<TreeMapRangeBrush>()
                {
                    new TreeMapRangeBrush() { LegendLabel = "1M km² - 3M km²", From = 1000000, To = 3000000, Brush = new SolidColorBrush(Color.FromArgb("#9215F3")) },
                    new TreeMapRangeBrush() { LegendLabel = "0.7M km² - 1M km²", From = 700000, To = 1000000, Brush = new SolidColorBrush(Color.FromArgb("#E2227E")) },
                    new TreeMapRangeBrush() { LegendLabel = "0.4M km² - 0.6M km", From = 400000, To = 600000, Brush = new SolidColorBrush(Color.FromArgb("#116DF9")) },
                    new TreeMapRangeBrush() { LegendLabel = "0.1M km² - 0.3M km²", From = 100000, To = 300000, Brush = new SolidColorBrush(Color.FromArgb("#FF4E4E")) },
                }
            };

            this.treeMap.LegendSettings.ShowLegend = true;
            this.treeMap.LeafItemBrushSettings = leafItemBrushSettings;
            this.treeMap.LeafItemSettings.TextStyle.TextColor = Color.FromArgb("#FFFFFF");
        }

        /// <summary>
        /// Method to get the palette colors.
        /// </summary>
        /// <returns>The palette colors.</returns>
        private TreeMapPaletteBrushSettings GetPaletteColors()
        {
            var brushSettings = new TreeMapPaletteBrushSettings();

            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#9191E9")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#D7907B")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#84DCCF")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#BBD686")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#6096BA")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#F0B67F")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#51D6FF")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#3DDC97")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#B49082")));
            brushSettings.Brushes.Add(new SolidColorBrush(Color.FromArgb("#FFACC0")));

            return brushSettings;
        }

        #endregion
    }
}