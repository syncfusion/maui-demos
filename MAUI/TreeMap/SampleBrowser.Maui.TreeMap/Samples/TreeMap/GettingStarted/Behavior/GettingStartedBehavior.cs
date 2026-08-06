namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.TreeMap;

    /// <summary>
    /// Behavior class for handling interactions in the GettingStarted sample view.
    /// </summary>
    internal class GettingStartedBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The tree map instance.
        /// </summary>
        private SfTreeMap? treeMap;

        /// <summary>
        /// The selection mode option.
        /// </summary>
        private Picker? selectionModePicker;

        /// <summary>
        /// The layout type option.
        /// </summary>
        private Picker? layoutTypePicker;

        /// <summary>
        /// The text format info picker.
        /// </summary>
        private Picker? textFormatOptionInfoPicker;

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
            this.selectionModePicker = sampleView.FindByName<Picker>("selectionModePicker");
            this.layoutTypePicker = sampleView.FindByName<Picker>("layoutTypePicker");
            this.textFormatOptionInfoPicker = sampleView.FindByName<Picker>("textFormatOptionInfoPicker");
            this.selectionModePicker.SelectedItem = treeMap.SelectionMode;
            this.textFormatOptionInfoPicker.SelectedItem = treeMap.LeafItemSettings.TextFormatOption;

            if (this.selectionModePicker != null)
            {
                this.selectionModePicker.SelectedIndexChanged += this.SelectionModePicker_SelectedIndexChanged;
            }

            if (this.layoutTypePicker != null)
            {
                this.layoutTypePicker.SelectedIndexChanged += this.LayoutTypePicker_SelectedIndexChanged;
            }

            if (this.textFormatOptionInfoPicker != null)
            {
                this.textFormatOptionInfoPicker.SelectedIndexChanged += this.TextFormatOptionInfoPicker_SelectedIndexChanged;
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

            if (this.selectionModePicker != null)
            {
                this.selectionModePicker.SelectedIndexChanged -= this.SelectionModePicker_SelectedIndexChanged;
                this.selectionModePicker = null;
            }

            if (this.layoutTypePicker != null)
            {
                this.layoutTypePicker.SelectedIndexChanged -= this.LayoutTypePicker_SelectedIndexChanged;
                this.layoutTypePicker = null;
            }

            if (this.textFormatOptionInfoPicker != null)
            {
                this.textFormatOptionInfoPicker.SelectedIndexChanged -= this.TextFormatOptionInfoPicker_SelectedIndexChanged;
                this.textFormatOptionInfoPicker = null;
            }
        }

        #endregion

        #region Property changed

        /// <summary>
        /// Occurs when the selection mode is changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void SelectionModePicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.treeMap == null || sender == null)
            {
                return;
            }

            var comboBox = (Picker)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                this.treeMap.SelectionMode = SelectionMode.None;
            }
            else if (selectedIndex == 1 || selectedIndex == -1)
            {
                this.treeMap.SelectionMode = SelectionMode.Single;
            }
            else
            {
                this.treeMap.SelectionMode = SelectionMode.Multiple;
            }
        }

        /// <summary>
        /// Occurs when the text format option is changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void TextFormatOptionInfoPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.treeMap == null || this.textFormatOptionInfoPicker == null || sender == null)
            {
                return;
            }

            switch (this.textFormatOptionInfoPicker.SelectedItem?.ToString())
            {
                case "Trim":
                    this.treeMap.LeafItemSettings.TextFormatOption = TextFormatOption.Trim;
                    break;
                case "Wrap":
                    this.treeMap.LeafItemSettings.TextFormatOption = TextFormatOption.Wrap;
                    break;
                case "Hide":
                    this.treeMap.LeafItemSettings.TextFormatOption = TextFormatOption.Hide;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Occurs when the layout type is changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">The event args.</param>
        private void LayoutTypePicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.treeMap == null || sender == null)
            {
                return;
            }

            var comboBox = (Picker)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                this.treeMap.LayoutType = LayoutType.Squarified;
            }
            else if (selectedIndex == 1)
            {
                this.treeMap.LayoutType = LayoutType.SliceAndDiceHorizontal;
            }
            else if (selectedIndex == 2)
            {
                this.treeMap.LayoutType = LayoutType.SliceAndDiceVertical;
            }
            else
            {
                this.treeMap.LayoutType = LayoutType.SliceAndDiceAuto;
            }
        }

        #endregion
    }
}