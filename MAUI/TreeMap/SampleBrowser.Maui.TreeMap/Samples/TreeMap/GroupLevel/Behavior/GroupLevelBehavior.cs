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
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.TreeMap;

    /// <summary>
    /// Represents the behavior for managing the group level in a sample view.
    /// </summary>
    internal class GroupLevelBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The tree map instance.
        /// </summary>
        private SfTreeMap? treeMap;

        /// <summary>
        /// The segment colors info.
        /// </summary>
        private SfSegmentedControl? segmentColorsOption;

        /// <summary>
        /// The radio button for the no group level option.
        /// </summary>
        private SfRadioButton? optionButton;

        /// <summary>
        /// The radio button for the group level option.
        /// </summary>
        private SfRadioButton? groupOptionButton;

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
            this.segmentColorsOption = sampleView.FindByName<SfSegmentedControl>("segmentedControlColorOption");
            this.optionButton = sampleView.FindByName<SfRadioButton>("optionButton");
            this.groupOptionButton = sampleView.FindByName<SfRadioButton>("groupOptionButton");

            if (this.segmentColorsOption != null)
            {
                this.segmentColorsOption.SelectionChanged += this.OnSegmentedControlSelectionChanged;
            }

            if (this.optionButton != null)
            {
                this.optionButton.StateChanged += this.OnLevelsTypeButtonCheckedChanged;
            }

            if (this.groupOptionButton != null)
            {
                this.groupOptionButton.StateChanged += this.OnLevelsTypeButtonCheckedChanged;
                this.groupOptionButton.IsChecked = true;
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

            if (this.segmentColorsOption != null)
            {
                this.segmentColorsOption.SelectionChanged -= this.OnSegmentedControlSelectionChanged;
                this.segmentColorsOption = null;
            }

            if (this.optionButton != null)
            {
                this.optionButton.StateChanged -= this.OnLevelsTypeButtonCheckedChanged;
                this.optionButton = null;
            }

            if (this.groupOptionButton != null)
            {
                this.groupOptionButton.StateChanged -= this.OnLevelsTypeButtonCheckedChanged;
                this.groupOptionButton = null;
            }
        }

        #endregion

        #region Property changed

        /// <summary>
        /// Method invokes when the selection is changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSegmentedControlSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (this.treeMap == null || e.NewIndex == null)
            {
                return;
            }

            this.UpdateRangeBrushSettings((int)e.NewIndex);
        }

        /// <summary>
        /// Event handler for the checked changed event of levels type radio buttons.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnLevelsTypeButtonCheckedChanged(object? sender, StateChangedEventArgs e)
        {
            if (sender == null || this.treeMap == null)
            {
                return;
            }

            var radioButton = sender as SfRadioButton;
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                if (radioButton != null)
                {
                    // Uncheck the other radio button
                    if (radioButton == this.optionButton && this.groupOptionButton != null)
                    {
                        this.groupOptionButton.IsChecked = false;
                    }
                    else if (radioButton == this.groupOptionButton && this.optionButton != null)
                    {
                        this.optionButton.IsChecked = false;
                    }

                    if (radioButton.Text == "Group By Continents")
                    {
                        // Add a level to group by Continent.
                        this.treeMap.Levels.Add(new TreeMapLevel { GroupPath = "Continent", Stroke = Brush.Gray, Background = Brush.Transparent });
                    }
                    else if (radioButton.Text == "Without Grouping")
                    {
                        // Clear all levels to remove grouping.
                        this.treeMap.Levels.Clear();
                    }
                }
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to update the range brush settings.
        /// </summary>
        /// <param name="index">The selected index.</param>
        private void UpdateRangeBrushSettings(int index)
        {
            if (this.treeMap == null)
            {
                return;
            }

            // Gets the brush collections.
            List<Brush> brushes = this.GetRangeBrushes(index);

            // Create range brush settings.
            var rangeBrushSettings = new TreeMapRangeBrushSettings();

            // Define range brushes.
            var rangeBrushes = new List<TreeMapRangeBrush>
            {
                new TreeMapRangeBrush { LegendLabel = "50M - 1B", From = 50000000, To = 1000000000, Brush = brushes[0] },
                new TreeMapRangeBrush { LegendLabel = "10M - 50M", From = 10000000, To = 50000000, Brush = brushes[1] },
                new TreeMapRangeBrush { LegendLabel = "0.1M - 10M", From = 100000, To = 10000000, Brush = brushes[2] },
            };

            // Assign range brushes to range brush settings.
            rangeBrushSettings.RangeBrushes = rangeBrushes;

            // Assign leaf item brush settings to the SfTreeMap control.
            this.treeMap.LeafItemBrushSettings = rangeBrushSettings;
        }

        /// <summary>
        /// Method to get the range brushes.
        /// </summary>
        /// <param name="index">The selected index.</param>
        /// <returns>The range brushes.</returns>
        private List<Brush> GetRangeBrushes(int index)
        {
            if (index == -1)
            {
                return new List<Brush>();
            }

            var colorCodes = new List<string[]>
            {
                new string[] { "#3F8D71", "#5BA985", "#7DC59D" },
                new string[] { "#F0A868", "#F3BC8B", "#F8D7B9" },
                new string[] { "#9E79CD", "#A487CA", "#CAB8E0" },
                new string[] { "#0C869C", "#11B0BB", "#18dad6" },
                new string[] { "#5BC0BE", "#9CD9D8", "#B9E4E3" },
            };

            List<Brush> brushes = new List<Brush>();
            if (index >= 0 && index < colorCodes.Count)
            {
                brushes.AddRange(colorCodes[index].Select(code => new SolidColorBrush(Color.FromArgb(code))));
            }

            return brushes;
        }

        #endregion
    }
}